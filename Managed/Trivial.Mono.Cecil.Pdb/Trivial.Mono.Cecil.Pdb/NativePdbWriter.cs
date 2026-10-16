using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x0200000B RID: 11
	public class NativePdbWriter : ISymbolWriter, IDisposable, IMetadataSymbolWriter
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00002F4B File Offset: 0x0000114B
		internal NativePdbWriter(ModuleDefinition module, SymWriter writer)
		{
			this.module = module;
			this.writer = writer;
			this.documents = new Dictionary<string, SymDocumentWriter>();
			this.import_info_to_parent = new Dictionary<ImportDebugInformation, MetadataToken>();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00002F77 File Offset: 0x00001177
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new NativePdbReaderProvider();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002F80 File Offset: 0x00001180
		public ImageDebugHeader GetDebugHeader()
		{
			ImageDebugDirectory imageDebugDirectory;
			byte[] debugInfo = this.writer.GetDebugInfo(out imageDebugDirectory);
			imageDebugDirectory.TimeDateStamp = (int)this.module.timestamp;
			return new ImageDebugHeader(new ImageDebugHeaderEntry(imageDebugDirectory, debugInfo));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002FBC File Offset: 0x000011BC
		public void Write(MethodDebugInformation info)
		{
			int num = info.method.MetadataToken.ToInt32();
			if (info.HasSequencePoints || info.scope != null || info.HasCustomDebugInformations || info.StateMachineKickOffMethod != null)
			{
				this.writer.OpenMethod(num);
				if (!info.sequence_points.IsNullOrEmpty<SequencePoint>())
				{
					this.DefineSequencePoints(info.sequence_points);
				}
				MetadataToken metadataToken = default(MetadataToken);
				if (info.scope != null)
				{
					this.DefineScope(info.scope, info, out metadataToken);
				}
				this.DefineCustomMetadata(info, metadataToken);
				this.writer.CloseMethod();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003054 File Offset: 0x00001254
		void IMetadataSymbolWriter.SetMetadata(MetadataBuilder metadata)
		{
			this.metadata = metadata;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000305D File Offset: 0x0000125D
		void IMetadataSymbolWriter.WriteModule()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003060 File Offset: 0x00001260
		private void DefineCustomMetadata(MethodDebugInformation info, MetadataToken import_parent)
		{
			CustomMetadataWriter customMetadataWriter = new CustomMetadataWriter(this.writer);
			if (import_parent.RID != 0U)
			{
				customMetadataWriter.WriteForwardInfo(import_parent);
			}
			else if (info.scope != null && info.scope.Import != null && info.scope.Import.HasTargets)
			{
				customMetadataWriter.WriteUsingInfo(info.scope.Import);
			}
			if (info.Method.HasCustomAttributes)
			{
				foreach (CustomAttribute customAttribute in info.Method.CustomAttributes)
				{
					TypeReference attributeType = customAttribute.AttributeType;
					if (attributeType.IsTypeOf("System.Runtime.CompilerServices", "IteratorStateMachineAttribute") || attributeType.IsTypeOf("System.Runtime.CompilerServices", "AsyncStateMachineAttribute"))
					{
						TypeReference typeReference = customAttribute.ConstructorArguments[0].Value as TypeReference;
						if (typeReference != null)
						{
							customMetadataWriter.WriteForwardIterator(typeReference);
						}
					}
				}
			}
			if (info.HasCustomDebugInformations)
			{
				StateMachineScopeDebugInformation[] array = Enumerable.ToArray<StateMachineScopeDebugInformation>(Enumerable.OfType<StateMachineScopeDebugInformation>(info.CustomDebugInformations));
				if (array.Length != 0)
				{
					customMetadataWriter.WriteIteratorScopes(array, info);
				}
			}
			customMetadataWriter.WriteCustomMetadata();
			this.DefineAsyncCustomMetadata(info);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000031A0 File Offset: 0x000013A0
		private void DefineAsyncCustomMetadata(MethodDebugInformation info)
		{
			if (info.HasCustomDebugInformations)
			{
				foreach (CustomDebugInformation customDebugInformation in info.CustomDebugInformations)
				{
					AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = customDebugInformation as AsyncMethodBodyDebugInformation;
					if (asyncMethodBodyDebugInformation != null)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							BinaryStreamWriter binaryStreamWriter = new BinaryStreamWriter(memoryStream);
							binaryStreamWriter.WriteUInt32((info.StateMachineKickOffMethod != null) ? info.StateMachineKickOffMethod.MetadataToken.ToUInt32() : 0U);
							binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.CatchHandler.Offset);
							binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Resumes.Count);
							for (int i = 0; i < asyncMethodBodyDebugInformation.Resumes.Count; i++)
							{
								binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Yields[i].Offset);
								binaryStreamWriter.WriteUInt32((asyncMethodBodyDebugInformation.MoveNextMethod != null) ? asyncMethodBodyDebugInformation.MoveNextMethod.MetadataToken.ToUInt32() : 0U);
								binaryStreamWriter.WriteUInt32((uint)asyncMethodBodyDebugInformation.Resumes[i].Offset);
							}
							this.writer.DefineCustomMetadata("asyncMethodInfo", memoryStream.ToArray());
						}
					}
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003328 File Offset: 0x00001528
		private void DefineScope(ScopeDebugInformation scope, MethodDebugInformation info, out MetadataToken import_parent)
		{
			int offset = scope.Start.Offset;
			int num = (scope.End.IsEndOfMethod ? info.code_size : scope.End.Offset);
			import_parent = new MetadataToken(0U);
			this.writer.OpenScope(offset);
			if (scope.Import != null && scope.Import.HasTargets && !this.import_info_to_parent.TryGetValue(info.scope.Import, ref import_parent))
			{
				foreach (ImportTarget importTarget in scope.Import.Targets)
				{
					ImportTargetKind kind = importTarget.Kind;
					if (kind <= ImportTargetKind.ImportType)
					{
						if (kind != ImportTargetKind.ImportNamespace)
						{
							if (kind == ImportTargetKind.ImportType)
							{
								this.writer.UsingNamespace("T" + TypeParser.ToParseable(importTarget.type));
							}
						}
						else
						{
							this.writer.UsingNamespace("U" + importTarget.@namespace);
						}
					}
					else if (kind != ImportTargetKind.DefineNamespaceAlias)
					{
						if (kind == ImportTargetKind.DefineTypeAlias)
						{
							this.writer.UsingNamespace("A" + importTarget.Alias + " T" + TypeParser.ToParseable(importTarget.type));
						}
					}
					else
					{
						this.writer.UsingNamespace("A" + importTarget.Alias + " U" + importTarget.@namespace);
					}
				}
				this.import_info_to_parent.Add(info.scope.Import, info.method.MetadataToken);
			}
			int num2 = info.local_var_token.ToInt32();
			if (!scope.variables.IsNullOrEmpty<VariableDebugInformation>())
			{
				for (int i = 0; i < scope.variables.Count; i++)
				{
					VariableDebugInformation variableDebugInformation = scope.variables[i];
					this.DefineLocalVariable(variableDebugInformation, num2, offset, num);
				}
			}
			if (!scope.constants.IsNullOrEmpty<ConstantDebugInformation>())
			{
				for (int j = 0; j < scope.constants.Count; j++)
				{
					ConstantDebugInformation constantDebugInformation = scope.constants[j];
					this.DefineConstant(constantDebugInformation);
				}
			}
			if (!scope.scopes.IsNullOrEmpty<ScopeDebugInformation>())
			{
				for (int k = 0; k < scope.scopes.Count; k++)
				{
					MetadataToken metadataToken;
					this.DefineScope(scope.scopes[k], info, out metadataToken);
				}
			}
			this.writer.CloseScope(num);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000035C4 File Offset: 0x000017C4
		private void DefineSequencePoints(Collection<SequencePoint> sequence_points)
		{
			for (int i = 0; i < sequence_points.Count; i++)
			{
				SequencePoint sequencePoint = sequence_points[i];
				this.writer.DefineSequencePoints(this.GetDocument(sequencePoint.Document), new int[] { sequencePoint.Offset }, new int[] { sequencePoint.StartLine }, new int[] { sequencePoint.StartColumn }, new int[] { sequencePoint.EndLine }, new int[] { sequencePoint.EndColumn });
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000364C File Offset: 0x0000184C
		private void DefineLocalVariable(VariableDebugInformation variable, int local_var_token, int start_offset, int end_offset)
		{
			this.writer.DefineLocalVariable2(variable.Name, variable.Attributes, local_var_token, variable.Index, 0, 0, start_offset, end_offset);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000367C File Offset: 0x0000187C
		private void DefineConstant(ConstantDebugInformation constant)
		{
			uint num = this.metadata.AddStandAloneSignature(this.metadata.GetConstantTypeBlobIndex(constant.ConstantType));
			MetadataToken metadataToken = new MetadataToken(TokenType.Signature, num);
			this.writer.DefineConstant2(constant.Name, constant.Value, metadataToken.ToInt32());
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000036D4 File Offset: 0x000018D4
		private SymDocumentWriter GetDocument(Document document)
		{
			if (document == null)
			{
				return null;
			}
			SymDocumentWriter symDocumentWriter;
			if (this.documents.TryGetValue(document.Url, ref symDocumentWriter))
			{
				return symDocumentWriter;
			}
			symDocumentWriter = this.writer.DefineDocument(document.Url, document.Language.ToGuid(), document.LanguageVendor.ToGuid(), document.Type.ToGuid());
			this.documents[document.Url] = symDocumentWriter;
			return symDocumentWriter;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003744 File Offset: 0x00001944
		public void Dispose()
		{
			MethodDefinition entryPoint = this.module.EntryPoint;
			if (entryPoint != null)
			{
				this.writer.SetUserEntryPoint(entryPoint.MetadataToken.ToInt32());
			}
			this.writer.Close();
		}

		// Token: 0x04000014 RID: 20
		private readonly ModuleDefinition module;

		// Token: 0x04000015 RID: 21
		private readonly SymWriter writer;

		// Token: 0x04000016 RID: 22
		private readonly Dictionary<string, SymDocumentWriter> documents;

		// Token: 0x04000017 RID: 23
		private readonly Dictionary<ImportDebugInformation, MetadataToken> import_info_to_parent;

		// Token: 0x04000018 RID: 24
		private MetadataBuilder metadata;
	}
}
