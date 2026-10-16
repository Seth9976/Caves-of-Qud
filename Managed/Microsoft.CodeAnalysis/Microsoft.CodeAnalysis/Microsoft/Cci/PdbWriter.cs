using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.DiaSymReader;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x02000078 RID: 120
	internal sealed class PdbWriter : IDisposable
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B7FC File Offset: 0x000099FC
		private bool IsDeterministic
		{
			get
			{
				return this._hashAlgorithmNameOpt.Name != null;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B81A File Offset: 0x00009A1A
		public PdbWriter(string fileName, Func<ISymWriterMetadataProvider, SymUnmanagedWriter> symWriterFactory, HashAlgorithmName hashAlgorithmNameOpt)
		{
			this._fileName = fileName;
			this._symWriterFactory = symWriterFactory;
			this._hashAlgorithmNameOpt = hashAlgorithmNameOpt;
			this._documentIndex = new Dictionary<DebugSourceDocument, int>();
			this._qualifiedNameCache = new Dictionary<object, string>(ReferenceEqualityComparer.Instance);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B852 File Offset: 0x00009A52
		public void WriteTo(Stream stream)
		{
			this._symWriter.WriteTo(stream);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B860 File Offset: 0x00009A60
		public void Dispose()
		{
			SymUnmanagedWriter symWriter = this._symWriter;
			if (symWriter == null)
			{
				return;
			}
			symWriter.Dispose();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B872 File Offset: 0x00009A72
		private CommonPEModuleBuilder Module
		{
			get
			{
				return this.Context.Module;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B87F File Offset: 0x00009A7F
		private EmitContext Context
		{
			get
			{
				return this._metadataWriter.Context;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B88C File Offset: 0x00009A8C
		public void SerializeDebugInfo(IMethodBody methodBody, StandaloneSignatureHandle localSignatureHandleOpt, CustomDebugInfoWriter customDebugInfoWriter)
		{
			bool flag = methodBody.StateMachineTypeName != null;
			if (!flag && methodBody.SequencePoints.IsEmpty && methodBody.MethodDefinition != (this.Context.Module.DebugEntryPoint ?? this.Context.Module.PEEntryPoint))
			{
				return;
			}
			MethodDefinitionHandle methodDefinitionHandle = (MethodDefinitionHandle)this._metadataWriter.GetMethodHandle(methodBody.MethodDefinition);
			int token = MetadataTokens.GetToken(methodDefinitionHandle);
			this.OpenMethod(token, methodBody.MethodDefinition);
			ImmutableArray<LocalScope> localScopes = methodBody.LocalScopes;
			if (localScopes.Length > 0)
			{
				this.DefineScopeLocals(localScopes[0], localSignatureHandleOpt);
			}
			if (!flag && methodBody.ImportScope != null)
			{
				IMethodDefinition methodDefinition;
				if (customDebugInfoWriter.ShouldForwardNamespaceScopes(this.Context, methodBody, methodDefinitionHandle, out methodDefinition))
				{
					if (methodDefinition != null)
					{
						this.UsingNamespace("@" + MetadataTokens.GetToken(this._metadataWriter.GetMethodHandle(methodDefinition)).ToString(), methodBody.MethodDefinition);
					}
				}
				else
				{
					this.DefineNamespaceScopes(methodBody);
				}
			}
			this.DefineLocalScopes(localScopes, localSignatureHandleOpt);
			this.EmitSequencePoints(methodBody.SequencePoints);
			AsyncMoveNextBodyDebugInfo asyncMoveNextBodyDebugInfo = methodBody.MoveNextBodyInfo as AsyncMoveNextBodyDebugInfo;
			if (asyncMoveNextBodyDebugInfo != null)
			{
				this._symWriter.SetAsyncInfo(token, MetadataTokens.GetToken(this._metadataWriter.GetMethodHandle(asyncMoveNextBodyDebugInfo.KickoffMethod)), asyncMoveNextBodyDebugInfo.CatchHandlerOffset, asyncMoveNextBodyDebugInfo.YieldOffsets.AsSpan(), asyncMoveNextBodyDebugInfo.ResumeOffsets.AsSpan());
			}
			CompilationOptions options = this.Context.Module.CommonCompilation.Options;
			bool flag2 = options.OutputKind == OutputKind.WindowsRuntimeMetadata;
			bool flag3 = options.EnableEditAndContinue && this._metadataWriter.IsFullMetadata;
			bool flag4;
			byte[] array = customDebugInfoWriter.SerializeMethodDebugInfo(this.Context, methodBody, methodDefinitionHandle, flag3, flag2, out flag4);
			if (array != null)
			{
				this._symWriter.DefineCustomMetadata(array);
			}
			if (flag4)
			{
				this.DefineAssemblyReferenceAliases();
			}
			this.CloseMethod(methodBody.IL.Length);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BA80 File Offset: 0x00009C80
		private void DefineNamespaceScopes(IMethodBody methodBody)
		{
			CommonPEModuleBuilder module = this.Module;
			bool generateVisualBasicStylePdb = module.GenerateVisualBasicStylePdb;
			IMethodDefinition methodDefinition = methodBody.MethodDefinition;
			IImportScope importScope = methodBody.ImportScope;
			PooledHashSet<string> pooledHashSet = null;
			if (!generateVisualBasicStylePdb)
			{
				for (IImportScope importScope2 = importScope; importScope2 != null; importScope2 = importScope2.Parent)
				{
					foreach (UsedNamespaceOrType usedNamespaceOrType in importScope2.GetUsedNamespaces())
					{
						if (usedNamespaceOrType.TargetNamespaceOpt == null && usedNamespaceOrType.TargetTypeOpt == null)
						{
							if (pooledHashSet == null)
							{
								pooledHashSet = PooledHashSet<string>.GetInstance();
							}
							pooledHashSet.Add(usedNamespaceOrType.AliasOpt);
						}
					}
				}
			}
			for (IImportScope importScope3 = importScope; importScope3 != null; importScope3 = importScope3.Parent)
			{
				foreach (UsedNamespaceOrType usedNamespaceOrType2 in importScope3.GetUsedNamespaces())
				{
					string text = this.TryEncodeImport(usedNamespaceOrType2, pooledHashSet, false);
					if (text != null)
					{
						this.UsingNamespace(text, methodDefinition);
					}
				}
			}
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
			if (generateVisualBasicStylePdb)
			{
				string defaultNamespace = module.DefaultNamespace;
				if (!string.IsNullOrEmpty(defaultNamespace))
				{
					this.UsingNamespace("*" + defaultNamespace, module);
				}
				foreach (string text2 in module.LinkedAssembliesDebugInfo)
				{
					this.UsingNamespace("&" + text2, module);
				}
				foreach (UsedNamespaceOrType usedNamespaceOrType3 in module.GetImports())
				{
					string text3 = this.TryEncodeImport(usedNamespaceOrType3, null, true);
					if (text3 != null)
					{
						this.UsingNamespace(text3, methodDefinition);
					}
				}
				this.UsingNamespace(this.GetOrCreateSerializedNamespaceName(methodDefinition.ContainingNamespace), methodDefinition);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BC40 File Offset: 0x00009E40
		private void DefineAssemblyReferenceAliases()
		{
			foreach (AssemblyReferenceAlias assemblyReferenceAlias in this.Module.GetAssemblyReferenceAliases(this.Context))
			{
				this.UsingNamespace("Z" + assemblyReferenceAlias.Name + " " + assemblyReferenceAlias.Assembly.Identity.GetDisplayName(false), this.Module);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BCAC File Offset: 0x00009EAC
		private string TryEncodeImport(UsedNamespaceOrType import, HashSet<string> declaredExternAliasesOpt, bool isProjectLevel)
		{
			if (this.Module.GenerateVisualBasicStylePdb)
			{
				if (import.TargetTypeOpt != null)
				{
					if (import.TargetTypeOpt.IsTypeSpecification())
					{
						return null;
					}
					string orCreateSerializedTypeName = this.GetOrCreateSerializedTypeName(import.TargetTypeOpt);
					if (import.AliasOpt != null)
					{
						return (isProjectLevel ? "@PA:" : "@FA:") + import.AliasOpt + "=" + orCreateSerializedTypeName;
					}
					return (isProjectLevel ? "@PT:" : "@FT:") + orCreateSerializedTypeName;
				}
				else
				{
					if (import.TargetNamespaceOpt == null)
					{
						return (isProjectLevel ? "@PX:" : "@FX:") + import.AliasOpt + "=" + import.TargetXmlNamespaceOpt;
					}
					string orCreateSerializedNamespaceName = this.GetOrCreateSerializedNamespaceName(import.TargetNamespaceOpt);
					if (import.AliasOpt == null)
					{
						return (isProjectLevel ? "@P:" : "@F:") + orCreateSerializedNamespaceName;
					}
					return (isProjectLevel ? "@PA:" : "@FA:") + import.AliasOpt + "=" + orCreateSerializedNamespaceName;
				}
			}
			else if (import.TargetTypeOpt != null)
			{
				string orCreateSerializedTypeName2 = this.GetOrCreateSerializedTypeName(import.TargetTypeOpt);
				if (import.AliasOpt == null)
				{
					return "T" + orCreateSerializedTypeName2;
				}
				return "A" + import.AliasOpt + " T" + orCreateSerializedTypeName2;
			}
			else
			{
				if (import.TargetNamespaceOpt == null)
				{
					return "X" + import.AliasOpt;
				}
				string orCreateSerializedNamespaceName2 = this.GetOrCreateSerializedNamespaceName(import.TargetNamespaceOpt);
				if (import.AliasOpt != null)
				{
					if (import.TargetAssemblyOpt == null)
					{
						return "A" + import.AliasOpt + " U" + orCreateSerializedNamespaceName2;
					}
					return string.Concat(new string[]
					{
						"A",
						import.AliasOpt,
						" E",
						orCreateSerializedNamespaceName2,
						" ",
						this.GetAssemblyReferenceAlias(import.TargetAssemblyOpt, declaredExternAliasesOpt)
					});
				}
				else
				{
					if (import.TargetAssemblyOpt == null)
					{
						return "U" + orCreateSerializedNamespaceName2;
					}
					return "E" + orCreateSerializedNamespaceName2 + " " + this.GetAssemblyReferenceAlias(import.TargetAssemblyOpt, declaredExternAliasesOpt);
				}
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		internal string GetOrCreateSerializedNamespaceName(INamespace @namespace)
		{
			string text;
			if (!this._qualifiedNameCache.TryGetValue(@namespace, ref text))
			{
				text = TypeNameSerializer.BuildQualifiedNamespaceName(@namespace);
				this._qualifiedNameCache.Add(@namespace, text);
			}
			return text;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		internal string GetOrCreateSerializedTypeName(ITypeReference typeReference)
		{
			string text;
			if (!this._qualifiedNameCache.TryGetValue(typeReference, ref text))
			{
				if (this.Module.GenerateVisualBasicStylePdb)
				{
					text = this.SerializeVisualBasicImportTypeReference(typeReference);
				}
				else
				{
					text = typeReference.GetSerializedTypeName(this.Context);
				}
				this._qualifiedNameCache.Add(typeReference, text);
			}
			return text;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BF2C File Offset: 0x0000A12C
		private string SerializeVisualBasicImportTypeReference(ITypeReference typeReference)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			INestedTypeReference nestedTypeReference = typeReference.AsNestedTypeReference;
			ArrayBuilder<string> arrayBuilder;
			if (nestedTypeReference != null)
			{
				arrayBuilder = ArrayBuilder<string>.GetInstance();
				while (nestedTypeReference != null)
				{
					arrayBuilder.Add(nestedTypeReference.Name);
					typeReference = nestedTypeReference.GetContainingType(this._metadataWriter.Context);
					nestedTypeReference = typeReference.AsNestedTypeReference;
				}
			}
			else
			{
				arrayBuilder = null;
			}
			INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
			string namespaceName = asNamespaceTypeReference.NamespaceName;
			if (namespaceName.Length != 0)
			{
				instance.Builder.Append(namespaceName);
				instance.Builder.Append('.');
			}
			instance.Builder.Append(asNamespaceTypeReference.Name);
			if (arrayBuilder != null)
			{
				for (int i = arrayBuilder.Count - 1; i >= 0; i--)
				{
					instance.Builder.Append('.');
					instance.Builder.Append(arrayBuilder[i]);
				}
				arrayBuilder.Free();
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C00C File Offset: 0x0000A20C
		private string GetAssemblyReferenceAlias(IAssemblyReference assembly, HashSet<string> declaredExternAliases)
		{
			foreach (AssemblyReferenceAlias assemblyReferenceAlias in this._metadataWriter.Context.Module.GetAssemblyReferenceAliases(this._metadataWriter.Context))
			{
				if (assembly == assemblyReferenceAlias.Assembly && declaredExternAliases.Contains(assemblyReferenceAlias.Name))
				{
					return assemblyReferenceAlias.Name;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C078 File Offset: 0x0000A278
		private void DefineLocalScopes(ImmutableArray<LocalScope> scopes, StandaloneSignatureHandle localSignatureHandleOpt)
		{
			bool generateVisualBasicStylePdb = this.Module.GenerateVisualBasicStylePdb;
			ArrayBuilder<LocalScope> instance = ArrayBuilder<LocalScope>.GetInstance();
			for (int i = 1; i < scopes.Length; i++)
			{
				LocalScope localScope = scopes[i];
				while (instance.Count > 0)
				{
					LocalScope localScope2 = instance.Last();
					if (localScope.StartOffset < localScope2.StartOffset + localScope2.Length)
					{
						break;
					}
					instance.RemoveLast();
					this._symWriter.CloseScope(generateVisualBasicStylePdb ? (localScope2.EndOffset - 1) : localScope2.EndOffset);
				}
				instance.Add(localScope);
				this._symWriter.OpenScope(localScope.StartOffset);
				this.DefineScopeLocals(localScope, localSignatureHandleOpt);
			}
			for (int j = instance.Count - 1; j >= 0; j--)
			{
				LocalScope localScope3 = instance[j];
				this._symWriter.CloseScope(generateVisualBasicStylePdb ? (localScope3.EndOffset - 1) : localScope3.EndOffset);
			}
			instance.Free();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C16C File Offset: 0x0000A36C
		private void DefineScopeLocals(LocalScope currentScope, StandaloneSignatureHandle localSignatureHandleOpt)
		{
			foreach (ILocalDefinition localDefinition in currentScope.Constants)
			{
				StandaloneSignatureHandle standaloneSignatureHandle = this._metadataWriter.SerializeLocalConstantStandAloneSignature(localDefinition);
				if (!this._metadataWriter.IsLocalNameTooLong(localDefinition))
				{
					this._symWriter.DefineLocalConstant(localDefinition.Name, localDefinition.CompileTimeValue.Value, MetadataTokens.GetToken(standaloneSignatureHandle));
				}
			}
			foreach (ILocalDefinition localDefinition2 in currentScope.Variables)
			{
				if (!this._metadataWriter.IsLocalNameTooLong(localDefinition2))
				{
					this._symWriter.DefineLocalVariable(localDefinition2.SlotIndex, localDefinition2.Name, (int)localDefinition2.PdbAttributes, localSignatureHandleOpt.IsNil ? 0 : MetadataTokens.GetToken(localSignatureHandleOpt));
				}
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C24C File Offset: 0x0000A44C
		public void SetMetadataEmitter(MetadataWriter metadataWriter)
		{
			SymUnmanagedWriterCreationOptions symUnmanagedWriterCreationOptions = (this.IsDeterministic ? SymUnmanagedWriterCreationOptions.Deterministic : SymUnmanagedWriterCreationOptions.UseComRegistry) | SymUnmanagedWriterCreationOptions.UseAlternativeLoadPath;
			SymWriterMetadataProvider symWriterMetadataProvider = new SymWriterMetadataProvider(metadataWriter);
			SymUnmanagedWriter symUnmanagedWriter;
			try
			{
				symUnmanagedWriter = ((this._symWriterFactory != null) ? this._symWriterFactory.Invoke(symWriterMetadataProvider) : SymUnmanagedWriterFactory.CreateWriter(symWriterMetadataProvider, symUnmanagedWriterCreationOptions));
			}
			catch (DllNotFoundException ex)
			{
				throw new SymUnmanagedWriterException(ex.Message);
			}
			catch (SymUnmanagedWriterException ex2) when (ex2.InnerException is NotSupportedException)
			{
				throw new SymUnmanagedWriterException(string.Format(this.IsDeterministic ? CodeAnalysisResources.SymWriterNotDeterministic : CodeAnalysisResources.SymWriterOlderVersionThanRequired, ex2.ImplementationModuleName));
			}
			this._metadataWriter = metadataWriter;
			this._symWriter = symUnmanagedWriter;
			this._sequencePointsWriter = new SymUnmanagedSequencePointsWriter(symUnmanagedWriter, 64);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C318 File Offset: 0x0000A518
		public BlobContentId GetContentId()
		{
			BlobContentId blobContentId;
			if (this.IsDeterministic)
			{
				blobContentId = BlobContentId.FromHash(CryptographicHashProvider.ComputeHash(this._hashAlgorithmNameOpt, this._symWriter.GetUnderlyingData()));
				this._symWriter.UpdateSignature(blobContentId.Guid, blobContentId.Stamp, 1);
			}
			else
			{
				Guid guid;
				uint num;
				int num2;
				this._symWriter.GetSignature(out guid, out num, out num2);
				blobContentId = new BlobContentId(guid, num);
			}
			this._symWriter.Dispose();
			return blobContentId;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C38B File Offset: 0x0000A58B
		public void SetEntryPoint(int entryMethodToken)
		{
			this._symWriter.SetEntryPoint(entryMethodToken);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C39C File Offset: 0x0000A59C
		private int GetDocumentIndex(DebugSourceDocument document)
		{
			int num;
			if (this._documentIndex.TryGetValue(document, ref num))
			{
				return num;
			}
			return this.AddDocumentIndex(document);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private int AddDocumentIndex(DebugSourceDocument document)
		{
			DebugSourceInfo sourceInfo = document.GetSourceInfo();
			Guid guid;
			ReadOnlySpan<byte> readOnlySpan;
			if (!sourceInfo.Checksum.IsDefault)
			{
				guid = sourceInfo.ChecksumAlgorithmId;
				readOnlySpan = sourceInfo.Checksum.AsSpan();
			}
			else
			{
				guid = default(Guid);
				readOnlySpan = null;
			}
			ReadOnlySpan<byte> readOnlySpan2;
			if (!sourceInfo.EmbeddedTextBlob.IsDefault)
			{
				readOnlySpan2 = sourceInfo.EmbeddedTextBlob.AsSpan();
			}
			else
			{
				readOnlySpan2 = null;
			}
			int num = this._symWriter.DefineDocument(document.Location, document.Language, document.LanguageVendor, document.DocumentType, guid, readOnlySpan, readOnlySpan2);
			this._documentIndex.Add(document, num);
			return num;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C473 File Offset: 0x0000A673
		private void OpenMethod(int methodToken, IMethodDefinition method)
		{
			this._symWriter.OpenMethod(methodToken);
			this._symWriter.OpenScope(0);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C48D File Offset: 0x0000A68D
		private void CloseMethod(int ilLength)
		{
			this._symWriter.CloseScope(ilLength);
			this._symWriter.CloseMethod();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		private void UsingNamespace(string fullName, INamedEntity errorEntity)
		{
			if (!this._metadataWriter.IsUsingStringTooLong(fullName, errorEntity))
			{
				this._symWriter.UsingNamespace(fullName);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		private void EmitSequencePoints(ImmutableArray<SequencePoint> sequencePoints)
		{
			int num = -1;
			DebugSourceDocument debugSourceDocument = null;
			foreach (SequencePoint sequencePoint in sequencePoints)
			{
				DebugSourceDocument document = sequencePoint.Document;
				int num2;
				if (debugSourceDocument == document)
				{
					num2 = num;
				}
				else
				{
					debugSourceDocument = document;
					num = (num2 = this.GetDocumentIndex(debugSourceDocument));
				}
				this._sequencePointsWriter.Add(num2, sequencePoint.Offset, sequencePoint.StartLine, (int)sequencePoint.StartColumn, sequencePoint.EndLine, (int)sequencePoint.EndColumn);
			}
			this._sequencePointsWriter.Flush();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C548 File Offset: 0x0000A748
		[Conditional("DEBUG")]
		public void AssertAllDefinitionsHaveTokens(MultiDictionary<DebugSourceDocument, DefinitionWithLocation> file2definitions)
		{
			foreach (KeyValuePair<DebugSourceDocument, MultiDictionary<DebugSourceDocument, DefinitionWithLocation>.ValueSet> keyValuePair in file2definitions)
			{
				foreach (DefinitionWithLocation definitionWithLocation in keyValuePair.Value)
				{
					this._metadataWriter.GetDefinitionHandle(definitionWithLocation.Definition);
				}
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		public void WriteDefinitionLocations(MultiDictionary<DebugSourceDocument, DefinitionWithLocation> file2definitions)
		{
			bool flag = false;
			foreach (KeyValuePair<DebugSourceDocument, MultiDictionary<DebugSourceDocument, DefinitionWithLocation>.ValueSet> keyValuePair in file2definitions)
			{
				foreach (DefinitionWithLocation definitionWithLocation in keyValuePair.Value)
				{
					if (!flag)
					{
						this._symWriter.OpenTokensToSourceSpansMap();
						flag = true;
					}
					int token = MetadataTokens.GetToken(this._metadataWriter.GetDefinitionHandle(definitionWithLocation.Definition));
					this._symWriter.MapTokenToSourceSpan(token, this.GetDocumentIndex(keyValuePair.Key), definitionWithLocation.StartLine + 1, definitionWithLocation.StartColumn + 1, definitionWithLocation.EndLine + 1, definitionWithLocation.EndColumn + 1);
				}
			}
			if (flag)
			{
				this._symWriter.CloseTokensToSourceSpansMap();
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public void EmbedSourceLink(Stream stream)
		{
			byte[] array;
			try
			{
				array = stream.ReadAllBytes();
			}
			catch (Exception ex)
			{
				throw new SymUnmanagedWriterException(ex.Message, ex);
			}
			try
			{
				this._symWriter.SetSourceLinkData(array);
			}
			catch (SymUnmanagedWriterException ex2) when (ex2.InnerException is NotSupportedException)
			{
				throw new SymUnmanagedWriterException(string.Format(CodeAnalysisResources.SymWriterDoesNotSupportSourceLink, ex2.ImplementationModuleName));
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C774 File Offset: 0x0000A974
		public void WriteRemainingDebugDocuments(IReadOnlyDictionary<string, DebugSourceDocument> documents)
		{
			foreach (KeyValuePair<string, DebugSourceDocument> keyValuePair in Enumerable.OrderBy<KeyValuePair<string, DebugSourceDocument>, string>(Enumerable.Where<KeyValuePair<string, DebugSourceDocument>>(documents, (KeyValuePair<string, DebugSourceDocument> kvp) => !this._documentIndex.ContainsKey(kvp.Value)), (KeyValuePair<string, DebugSourceDocument> kvp) => kvp.Key))
			{
				this.AddDocumentIndex(keyValuePair.Value);
			}
		}

		// Token: 0x04000100 RID: 256
		internal const uint Age = 1U;

		// Token: 0x04000101 RID: 257
		private readonly HashAlgorithmName _hashAlgorithmNameOpt;

		// Token: 0x04000102 RID: 258
		private readonly string _fileName;

		// Token: 0x04000103 RID: 259
		private readonly Func<ISymWriterMetadataProvider, SymUnmanagedWriter> _symWriterFactory;

		// Token: 0x04000104 RID: 260
		private readonly Dictionary<DebugSourceDocument, int> _documentIndex;

		// Token: 0x04000105 RID: 261
		private MetadataWriter _metadataWriter;

		// Token: 0x04000106 RID: 262
		private SymUnmanagedWriter _symWriter;

		// Token: 0x04000107 RID: 263
		private SymUnmanagedSequencePointsWriter _sequencePointsWriter;

		// Token: 0x04000108 RID: 264
		private readonly Dictionary<object, string> _qualifiedNameCache;
	}
}
