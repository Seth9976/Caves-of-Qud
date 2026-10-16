using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001AB RID: 427
	internal sealed class PEModule : IDisposable
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x00033A0C File Offset: 0x00031C0C
		internal PEModule(ModuleMetadata owner, PEReader peReader, IntPtr metadataOpt, int metadataSizeOpt, bool includeEmbeddedInteropTypes, bool ignoreAssemblyRefs)
		{
			this._owner = owner;
			this._peReaderOpt = peReader;
			this._metadataPointerOpt = metadataOpt;
			this._metadataSizeOpt = metadataSizeOpt;
			this._lazyTypeNameCollection = new Lazy<IdentifierCollection>(new Func<IdentifierCollection>(this.ComputeTypeNameCollection));
			this._lazyNamespaceNameCollection = new Lazy<IdentifierCollection>(new Func<IdentifierCollection>(this.ComputeNamespaceNameCollection));
			this._hashesOpt = ((peReader != null) ? new PEModule.PEHashProvider(peReader) : null);
			this._lazyContainsNoPiaLocalTypes = (includeEmbeddedInteropTypes ? ThreeState.False : ThreeState.Unknown);
			if (ignoreAssemblyRefs)
			{
				this._lazyAssemblyReferences = ImmutableArray<AssemblyIdentity>.Empty;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00033A99 File Offset: 0x00031C99
		internal bool IsDisposed
		{
			get
			{
				return this._isDisposed;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00033AA1 File Offset: 0x00031CA1
		public void Dispose()
		{
			this._isDisposed = true;
			PEReader peReaderOpt = this._peReaderOpt;
			if (peReaderOpt == null)
			{
				return;
			}
			peReaderOpt.Dispose();
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00033ABA File Offset: 0x00031CBA
		internal PEReader PEReaderOpt
		{
			get
			{
				return this._peReaderOpt;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00033AC2 File Offset: 0x00031CC2
		internal MetadataReader MetadataReader
		{
			get
			{
				if (this._lazyMetadataReader == null)
				{
					this.InitializeMetadataReader();
				}
				if (this._isDisposed)
				{
					PEModule.ThrowMetadataDisposed();
				}
				return this._lazyMetadataReader;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00033AE8 File Offset: 0x00031CE8
		private unsafe void InitializeMetadataReader()
		{
			MetadataReader metadataReader;
			if (this._metadataPointerOpt != IntPtr.Zero)
			{
				metadataReader = new MetadataReader((byte*)(void*)this._metadataPointerOpt, this._metadataSizeOpt, MetadataReaderOptions.Default, PEModule.StringTableDecoder.Instance);
			}
			else
			{
				bool flag;
				try
				{
					flag = this._peReaderOpt.HasMetadata;
				}
				catch
				{
					flag = false;
				}
				if (!flag)
				{
					throw new BadImageFormatException(CodeAnalysisResources.PEImageDoesntContainManagedMetadata);
				}
				metadataReader = this._peReaderOpt.GetMetadataReader(MetadataReaderOptions.Default, PEModule.StringTableDecoder.Instance);
			}
			Interlocked.CompareExchange<MetadataReader>(ref this._lazyMetadataReader, metadataReader, null);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00033B78 File Offset: 0x00031D78
		private static void ThrowMetadataDisposed()
		{
			throw new ObjectDisposedException("ModuleMetadata");
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00033B84 File Offset: 0x00031D84
		internal bool IsManifestModule
		{
			get
			{
				return this.MetadataReader.IsAssembly;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00033B91 File Offset: 0x00031D91
		internal bool IsLinkedModule
		{
			get
			{
				return !this.MetadataReader.IsAssembly;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00033BA1 File Offset: 0x00031DA1
		internal bool IsCOFFOnly
		{
			get
			{
				return this._peReaderOpt != null && this._peReaderOpt.PEHeaders.IsCoffOnly;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00033BBD File Offset: 0x00031DBD
		internal Machine Machine
		{
			get
			{
				if (this._peReaderOpt == null)
				{
					return Machine.I386;
				}
				return this._peReaderOpt.PEHeaders.CoffHeader.Machine;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00033BE2 File Offset: 0x00031DE2
		internal bool Bit32Required
		{
			get
			{
				return this._peReaderOpt != null && (this._peReaderOpt.PEHeaders.CorHeader.Flags & CorFlags.Requires32Bit) > (CorFlags)0;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00033C08 File Offset: 0x00031E08
		internal ImmutableArray<byte> GetHash(AssemblyHashAlgorithm algorithmId)
		{
			return this._hashesOpt.GetHash(algorithmId);
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00033C18 File Offset: 0x00031E18
		internal string Name
		{
			get
			{
				if (this._lazyName == null)
				{
					this._lazyName = this.MetadataReader.GetString(this.MetadataReader.GetModuleDefinition().Name);
				}
				return this._lazyName;
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00033C57 File Offset: 0x00031E57
		internal Guid GetModuleVersionIdOrThrow()
		{
			return this.MetadataReader.GetModuleVersionIdOrThrow();
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00033C64 File Offset: 0x00031E64
		internal ImmutableArray<string> GetMetadataModuleNamesOrThrow()
		{
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			ImmutableArray<string> immutableArray;
			try
			{
				foreach (AssemblyFileHandle assemblyFileHandle in this.MetadataReader.AssemblyFiles)
				{
					AssemblyFile assemblyFile = this.MetadataReader.GetAssemblyFile(assemblyFileHandle);
					if (assemblyFile.ContainsMetadata)
					{
						string @string = this.MetadataReader.GetString(assemblyFile.Name);
						if (!MetadataHelpers.IsValidMetadataFileName(@string))
						{
							throw new BadImageFormatException(string.Format(CodeAnalysisResources.InvalidModuleName, this.Name, @string));
						}
						instance.Add(@string);
					}
				}
				immutableArray = instance.ToImmutable();
			}
			finally
			{
				instance.Free();
			}
			return immutableArray;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00033D34 File Offset: 0x00031F34
		internal IEnumerable<string> GetReferencedManagedModulesOrThrow()
		{
			HashSet<EntityHandle> hashSet = new HashSet<EntityHandle>();
			foreach (TypeReferenceHandle typeReferenceHandle in this.MetadataReader.TypeReferences)
			{
				EntityHandle resolutionScope = this.MetadataReader.GetTypeReference(typeReferenceHandle).ResolutionScope;
				if (resolutionScope.Kind == HandleKind.ModuleReference)
				{
					hashSet.Add(resolutionScope);
				}
			}
			foreach (EntityHandle entityHandle in hashSet)
			{
				yield return this.GetModuleRefNameOrThrow((ModuleReferenceHandle)entityHandle);
			}
			HashSet<EntityHandle>.Enumerator enumerator2 = default(HashSet<EntityHandle>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00033D44 File Offset: 0x00031F44
		internal ImmutableArray<EmbeddedResource> GetEmbeddedResourcesOrThrow()
		{
			if (this.MetadataReader.ManifestResources.Count == 0)
			{
				return ImmutableArray<EmbeddedResource>.Empty;
			}
			ImmutableArray<EmbeddedResource>.Builder builder = ImmutableArray.CreateBuilder<EmbeddedResource>();
			foreach (ManifestResourceHandle manifestResourceHandle in this.MetadataReader.ManifestResources)
			{
				ManifestResource manifestResource = this.MetadataReader.GetManifestResource(manifestResourceHandle);
				if (manifestResource.Implementation.IsNil)
				{
					string @string = this.MetadataReader.GetString(manifestResource.Name);
					builder.Add(new EmbeddedResource((uint)manifestResource.Offset, manifestResource.Attributes, @string));
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00033E10 File Offset: 0x00032010
		public string GetModuleRefNameOrThrow(ModuleReferenceHandle moduleRef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetModuleReference(moduleRef).Name);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00033E3C File Offset: 0x0003203C
		public ImmutableArray<AssemblyIdentity> ReferencedAssemblies
		{
			get
			{
				if (new ImmutableArray<AssemblyIdentity>?(this._lazyAssemblyReferences) == default(ImmutableArray<AssemblyIdentity>?))
				{
					this._lazyAssemblyReferences = this.MetadataReader.GetReferencedAssembliesOrThrow();
				}
				return this._lazyAssemblyReferences;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00033E7B File Offset: 0x0003207B
		internal string MetadataVersion
		{
			get
			{
				return this.MetadataReader.MetadataVersion;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00033E88 File Offset: 0x00032088
		internal BlobReader GetMemoryReaderOrThrow(BlobHandle blob)
		{
			return this.MetadataReader.GetBlobReader(blob);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00033E98 File Offset: 0x00032098
		internal string GetFullNameOrThrow(StringHandle namespaceHandle, StringHandle nameHandle)
		{
			string @string = this.MetadataReader.GetString(nameHandle);
			return MetadataHelpers.BuildQualifiedName(this.MetadataReader.GetString(namespaceHandle), @string);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00033EC4 File Offset: 0x000320C4
		internal AssemblyIdentity ReadAssemblyIdentityOrThrow()
		{
			return this.MetadataReader.ReadAssemblyIdentityOrThrow();
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00033ED4 File Offset: 0x000320D4
		public TypeDefinitionHandle GetContainingTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetDeclaringType();
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00033EF8 File Offset: 0x000320F8
		public string GetTypeDefNameOrThrow(TypeDefinitionHandle typeDef)
		{
			TypeDefinition typeDefinition = this.MetadataReader.GetTypeDefinition(typeDef);
			string text = this.MetadataReader.GetString(typeDefinition.Name);
			if (this.IsNestedTypeDefOrThrow(typeDef))
			{
				string @string = this.MetadataReader.GetString(typeDefinition.Namespace);
				if (@string.Length > 0)
				{
					text = @string + "." + text;
				}
			}
			return text;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00033F58 File Offset: 0x00032158
		public string GetTypeDefNamespaceOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetTypeDefinition(typeDef).Namespace);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00033F84 File Offset: 0x00032184
		public EntityHandle GetTypeDefExtendsOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).BaseType;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00033FA8 File Offset: 0x000321A8
		public TypeAttributes GetTypeDefFlagsOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).Attributes;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00033FCC File Offset: 0x000321CC
		public GenericParameterHandleCollection GetTypeDefGenericParamsOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetGenericParameters();
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00033FF0 File Offset: 0x000321F0
		public bool HasGenericParametersOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetGenericParameters().Count > 0;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0003401C File Offset: 0x0003221C
		public void GetTypeDefPropsOrThrow(TypeDefinitionHandle typeDef, out string name, out string @namespace, out TypeAttributes flags, out EntityHandle extends)
		{
			TypeDefinition typeDefinition = this.MetadataReader.GetTypeDefinition(typeDef);
			name = this.MetadataReader.GetString(typeDefinition.Name);
			@namespace = this.MetadataReader.GetString(typeDefinition.Namespace);
			flags = typeDefinition.Attributes;
			extends = typeDefinition.BaseType;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00034076 File Offset: 0x00032276
		internal bool IsNestedTypeDefOrThrow(TypeDefinitionHandle typeDef)
		{
			return PEModule.IsNestedTypeDefOrThrow(this.MetadataReader, typeDef);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00034084 File Offset: 0x00032284
		private static bool IsNestedTypeDefOrThrow(MetadataReader metadataReader, TypeDefinitionHandle typeDef)
		{
			return PEModule.IsNested(metadataReader.GetTypeDefinition(typeDef).Attributes);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000340A8 File Offset: 0x000322A8
		internal bool IsInterfaceOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).Attributes.IsInterface();
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000340CE File Offset: 0x000322CE
		private IEnumerable<PEModule.TypeDefToNamespace> GetTypeDefsOrThrow(bool topLevelOnly)
		{
			foreach (TypeDefinitionHandle typeDefinitionHandle in this.MetadataReader.TypeDefinitions)
			{
				TypeDefinition typeDefinition = this.MetadataReader.GetTypeDefinition(typeDefinitionHandle);
				if (!topLevelOnly || !PEModule.IsNested(typeDefinition.Attributes))
				{
					yield return new PEModule.TypeDefToNamespace(typeDefinitionHandle, typeDefinition.NamespaceDefinition);
				}
			}
			TypeDefinitionHandleCollection.Enumerator enumerator = default(TypeDefinitionHandleCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000340E8 File Offset: 0x000322E8
		internal IEnumerable<IGrouping<string, TypeDefinitionHandle>> GroupTypesByNamespaceOrThrow(StringComparer nameComparer)
		{
			Dictionary<string, ArrayBuilder<TypeDefinitionHandle>> dictionary = new Dictionary<string, ArrayBuilder<TypeDefinitionHandle>>();
			this.GetTypeNamespaceNamesOrThrow(dictionary);
			this.GetForwardedTypeNamespaceNamesOrThrow(dictionary);
			ArrayBuilder<IGrouping<string, TypeDefinitionHandle>> arrayBuilder = new ArrayBuilder<IGrouping<string, TypeDefinitionHandle>>(dictionary.Count);
			foreach (KeyValuePair<string, ArrayBuilder<TypeDefinitionHandle>> keyValuePair in dictionary)
			{
				ArrayBuilder<IGrouping<string, TypeDefinitionHandle>> arrayBuilder2 = arrayBuilder;
				string key = keyValuePair.Key;
				IEnumerable<TypeDefinitionHandle> value = keyValuePair.Value;
				arrayBuilder2.Add(new Grouping<string, TypeDefinitionHandle>(key, value ?? SpecializedCollections.EmptyEnumerable<TypeDefinitionHandle>()));
			}
			arrayBuilder.Sort(new PEModule.TypesByNamespaceSortComparer(nameComparer));
			return arrayBuilder;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00034184 File Offset: 0x00032384
		private void GetTypeNamespaceNamesOrThrow(Dictionary<string, ArrayBuilder<TypeDefinitionHandle>> namespaces)
		{
			Dictionary<NamespaceDefinitionHandle, ArrayBuilder<TypeDefinitionHandle>> dictionary = new Dictionary<NamespaceDefinitionHandle, ArrayBuilder<TypeDefinitionHandle>>(PEModule.NamespaceHandleEqualityComparer.Singleton);
			foreach (PEModule.TypeDefToNamespace typeDefToNamespace in this.GetTypeDefsOrThrow(true))
			{
				NamespaceDefinitionHandle namespaceHandle = typeDefToNamespace.NamespaceHandle;
				TypeDefinitionHandle typeDef = typeDefToNamespace.TypeDef;
				ArrayBuilder<TypeDefinitionHandle> arrayBuilder;
				if (dictionary.TryGetValue(namespaceHandle, ref arrayBuilder))
				{
					arrayBuilder.Add(typeDef);
				}
				else
				{
					dictionary.Add(namespaceHandle, new ArrayBuilder<TypeDefinitionHandle> { typeDef });
				}
			}
			foreach (KeyValuePair<NamespaceDefinitionHandle, ArrayBuilder<TypeDefinitionHandle>> keyValuePair in dictionary)
			{
				string @string = this.MetadataReader.GetString(keyValuePair.Key);
				ArrayBuilder<TypeDefinitionHandle> arrayBuilder2;
				if (namespaces.TryGetValue(@string, ref arrayBuilder2))
				{
					arrayBuilder2.AddRange(keyValuePair.Value);
				}
				else
				{
					namespaces.Add(@string, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00034284 File Offset: 0x00032484
		private void GetForwardedTypeNamespaceNamesOrThrow(Dictionary<string, ArrayBuilder<TypeDefinitionHandle>> namespaces)
		{
			this.EnsureForwardTypeToAssemblyMap();
			foreach (string text in this._lazyForwardedTypesToAssemblyIndexMap.Keys)
			{
				int num = text.LastIndexOf('.');
				string text2 = ((num >= 0) ? text.Substring(0, num) : "");
				if (!namespaces.ContainsKey(text2))
				{
					namespaces.Add(text2, null);
				}
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003430C File Offset: 0x0003250C
		private IdentifierCollection ComputeTypeNameCollection()
		{
			IdentifierCollection identifierCollection;
			try
			{
				identifierCollection = new IdentifierCollection(Enumerable.Select(Enumerable.Select(Enumerable.Select(this.GetTypeDefsOrThrow(false), (PEModule.TypeDefToNamespace typeDef) => new
				{
					typeDef = typeDef,
					metadataName = this.GetTypeDefNameOrThrow(typeDef.TypeDef)
				}), <>h__TransparentIdentifier0 => new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					backtickIndex = <>h__TransparentIdentifier0.metadataName.IndexOf('`')
				}), delegate(<>h__TransparentIdentifier1)
				{
					if (<>h__TransparentIdentifier1.backtickIndex >= 0)
					{
						return <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.metadataName.Substring(0, <>h__TransparentIdentifier1.backtickIndex);
					}
					return <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.metadataName;
				}));
			}
			catch (BadImageFormatException)
			{
				identifierCollection = new IdentifierCollection();
			}
			return identifierCollection;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003439C File Offset: 0x0003259C
		private IdentifierCollection ComputeNamespaceNameCollection()
		{
			IdentifierCollection identifierCollection;
			try
			{
				identifierCollection = new IdentifierCollection(Enumerable.SelectMany<string, string, string>(Enumerable.Distinct<string>(Enumerable.Select<PEModule.TypeDefToNamespace, string>(Enumerable.Where<PEModule.TypeDefToNamespace>(this.GetTypeDefsOrThrow(true), delegate(PEModule.TypeDefToNamespace id)
				{
					PEModule.TypeDefToNamespace typeDefToNamespace = id;
					return !typeDefToNamespace.NamespaceHandle.IsNil;
				}), (PEModule.TypeDefToNamespace id) => this.MetadataReader.GetString(id.NamespaceHandle))), (string fullName) => fullName.Split(new char[] { '.' }, 1), (string fullName, string name) => name));
			}
			catch (BadImageFormatException)
			{
				identifierCollection = new IdentifierCollection();
			}
			return identifierCollection;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00034450 File Offset: 0x00032650
		internal ImmutableArray<TypeDefinitionHandle> GetNestedTypeDefsOrThrow(TypeDefinitionHandle container)
		{
			return this.MetadataReader.GetTypeDefinition(container).GetNestedTypes();
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00034474 File Offset: 0x00032674
		internal MethodImplementationHandleCollection GetMethodImplementationsOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetMethodImplementations();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00034498 File Offset: 0x00032698
		internal InterfaceImplementationHandleCollection GetInterfaceImplementationsOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetInterfaceImplementations();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000344BC File Offset: 0x000326BC
		internal MethodDefinitionHandleCollection GetMethodsOfTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetMethods();
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000344E0 File Offset: 0x000326E0
		internal PropertyDefinitionHandleCollection GetPropertiesOfTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetProperties();
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00034504 File Offset: 0x00032704
		internal EventDefinitionHandleCollection GetEventsOfTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetEvents();
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00034528 File Offset: 0x00032728
		internal FieldDefinitionHandleCollection GetFieldsOfTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).GetFields();
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003454C File Offset: 0x0003274C
		internal EntityHandle GetBaseTypeOfTypeOrThrow(TypeDefinitionHandle typeDef)
		{
			return this.MetadataReader.GetTypeDefinition(typeDef).BaseType;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00034570 File Offset: 0x00032770
		internal TypeLayout GetTypeLayout(TypeDefinitionHandle typeDef)
		{
			TypeLayout typeLayout;
			try
			{
				TypeDefinition typeDefinition = this.MetadataReader.GetTypeDefinition(typeDef);
				TypeAttributes typeAttributes = typeDefinition.Attributes & 24;
				if (typeAttributes != null)
				{
					LayoutKind layoutKind;
					if (typeAttributes != 8)
					{
						if (typeAttributes != 16)
						{
							return default(TypeLayout);
						}
						layoutKind = 2;
					}
					else
					{
						layoutKind = 0;
					}
					TypeLayout layout = typeDefinition.GetLayout();
					int num = layout.Size;
					int num2 = layout.PackingSize;
					if (num2 > 255)
					{
						num2 = 0;
					}
					if (num < 0)
					{
						num = 0;
					}
					typeLayout = new TypeLayout(layoutKind, num, (byte)num2);
				}
				else
				{
					typeLayout = default(TypeLayout);
					typeLayout = typeLayout;
				}
			}
			catch (BadImageFormatException)
			{
				typeLayout = default(TypeLayout);
			}
			return typeLayout;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00034628 File Offset: 0x00032828
		internal bool IsNoPiaLocalType(TypeDefinitionHandle typeDef)
		{
			PEModule.AttributeInfo attributeInfo;
			return this.IsNoPiaLocalType(typeDef, out attributeInfo);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00034640 File Offset: 0x00032840
		internal bool HasParamsAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.ParamArrayAttribute).HasValue;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00034664 File Offset: 0x00032864
		internal bool HasIsReadOnlyAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.IsReadOnlyAttribute).HasValue;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00034688 File Offset: 0x00032888
		internal bool HasDoesNotReturnAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.DoesNotReturnAttribute).HasValue;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000346AC File Offset: 0x000328AC
		internal bool HasIsUnmanagedAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.IsUnmanagedAttribute).HasValue;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000346D0 File Offset: 0x000328D0
		internal bool HasExtensionAttribute(EntityHandle token, bool ignoreCase)
		{
			return this.FindTargetAttribute(token, ignoreCase ? AttributeDescription.CaseInsensitiveExtensionAttribute : AttributeDescription.CaseSensitiveExtensionAttribute).HasValue;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000346FC File Offset: 0x000328FC
		internal bool HasVisualBasicEmbeddedAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.VisualBasicEmbeddedAttribute).HasValue;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00034720 File Offset: 0x00032920
		internal bool HasCodeAnalysisEmbeddedAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.CodeAnalysisEmbeddedAttribute).HasValue;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00034741 File Offset: 0x00032941
		internal bool HasDefaultMemberAttribute(EntityHandle token, out string memberName)
		{
			return this.HasStringValuedAttribute(token, AttributeDescription.DefaultMemberAttribute, out memberName);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00034750 File Offset: 0x00032950
		internal bool HasGuidAttribute(EntityHandle token, out string guidValue)
		{
			return this.HasStringValuedAttribute(token, AttributeDescription.GuidAttribute, out guidValue);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003475F File Offset: 0x0003295F
		internal bool HasFixedBufferAttribute(EntityHandle token, out string elementTypeName, out int bufferSize)
		{
			return this.HasStringAndIntValuedAttribute(token, AttributeDescription.FixedBufferAttribute, out elementTypeName, out bufferSize);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003476F File Offset: 0x0003296F
		internal bool HasAccessedThroughPropertyAttribute(EntityHandle token, out string propertyName)
		{
			return this.HasStringValuedAttribute(token, AttributeDescription.AccessedThroughPropertyAttribute, out propertyName);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00034780 File Offset: 0x00032980
		internal bool HasRequiredAttributeAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.RequiredAttributeAttribute).HasValue;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000347A4 File Offset: 0x000329A4
		internal bool HasAttribute(EntityHandle token, AttributeDescription description)
		{
			return this.FindTargetAttribute(token, description).HasValue;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000347C1 File Offset: 0x000329C1
		internal CustomAttributeHandle GetAttributeHandle(EntityHandle token, AttributeDescription description)
		{
			return this.FindTargetAttribute(token, description).Handle;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000347D0 File Offset: 0x000329D0
		internal bool HasDynamicAttribute(EntityHandle token, out ImmutableArray<bool> transformFlags)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.DynamicAttribute);
			if (!attributeInfo.HasValue)
			{
				transformFlags = default(ImmutableArray<bool>);
				return false;
			}
			if (attributeInfo.SignatureIndex == 0)
			{
				transformFlags = PEModule.s_simpleTransformFlags;
				return true;
			}
			return this.TryExtractBoolArrayValueFromAttribute(attributeInfo.Handle, out transformFlags);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00034820 File Offset: 0x00032A20
		internal bool HasNativeIntegerAttribute(EntityHandle token, out ImmutableArray<bool> transformFlags)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.NativeIntegerAttribute);
			if (!attributeInfo.HasValue)
			{
				transformFlags = default(ImmutableArray<bool>);
				return false;
			}
			if (attributeInfo.SignatureIndex == 0)
			{
				transformFlags = PEModule.s_simpleTransformFlags;
				return true;
			}
			return this.TryExtractBoolArrayValueFromAttribute(attributeInfo.Handle, out transformFlags);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00034870 File Offset: 0x00032A70
		internal bool HasTupleElementNamesAttribute(EntityHandle token, out ImmutableArray<string> tupleElementNames)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.TupleElementNamesAttribute);
			if (!attributeInfo.HasValue)
			{
				tupleElementNames = default(ImmutableArray<string>);
				return false;
			}
			return this.TryExtractStringArrayValueFromAttribute(attributeInfo.Handle, out tupleElementNames);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000348AC File Offset: 0x00032AAC
		internal bool HasIsByRefLikeAttribute(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.IsByRefLikeAttribute).HasValue;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000348D0 File Offset: 0x00032AD0
		internal ObsoleteAttributeData TryGetDeprecatedOrExperimentalOrObsoleteAttribute(EntityHandle token, IAttributeNamedArgumentDecoder decoder, bool ignoreByRefLikeMarker)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.DeprecatedAttribute);
			if (attributeInfo.HasValue)
			{
				return this.TryExtractDeprecatedDataFromAttribute(attributeInfo);
			}
			attributeInfo = this.FindTargetAttribute(token, AttributeDescription.ObsoleteAttribute);
			if (attributeInfo.HasValue)
			{
				ObsoleteAttributeData obsoleteAttributeData = this.TryExtractObsoleteDataFromAttribute(attributeInfo, decoder);
				if (((obsoleteAttributeData != null) ? obsoleteAttributeData.Message : null) == "Types with embedded references are not supported in this version of your compiler." && ignoreByRefLikeMarker)
				{
					return null;
				}
				return obsoleteAttributeData;
			}
			else
			{
				attributeInfo = this.FindTargetAttribute(token, AttributeDescription.ExperimentalAttribute);
				if (attributeInfo.HasValue)
				{
					return this.TryExtractExperimentalDataFromAttribute(attributeInfo);
				}
				return null;
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00034958 File Offset: 0x00032B58
		[NullableContext(1)]
		[return: Nullable(2)]
		internal UnmanagedCallersOnlyAttributeData TryGetUnmanagedCallersOnlyAttribute(EntityHandle token, IAttributeNamedArgumentDecoder attributeArgumentDecoder, [TupleElementNames(new string[] { "IsCallConvs", "CallConvs" })] [Nullable(new byte[] { 1, 1, 0, 2, 1 })] Func<string, TypedConstant, bool, ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>>> unmanagedCallersOnlyDecoder)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.UnmanagedCallersOnlyAttribute);
			BlobReader blobReader;
			if (!attributeInfo.HasValue || attributeInfo.SignatureIndex != 0 || !this.TryGetAttributeReader(attributeInfo.Handle, out blobReader))
			{
				return null;
			}
			ImmutableHashSet<INamedTypeSymbolInternal> immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
			if (blobReader.RemainingBytes > 0)
			{
				try
				{
					ushort num = blobReader.ReadUInt16();
					for (int i = 0; i < (int)num; i++)
					{
						ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode> valueTuple = attributeArgumentDecoder.DecodeCustomAttributeNamedArgumentOrThrow(ref blobReader);
						string text;
						TypedConstant typedConstant;
						valueTuple.Item1.Deconstruct<string, TypedConstant>(out text, out typedConstant);
						string text2 = text;
						TypedConstant typedConstant2 = typedConstant;
						bool item = valueTuple.Item2;
						SerializationTypeCode item2 = valueTuple.Item3;
						SerializationTypeCode item3 = valueTuple.Item4;
						if (item2 == SerializationTypeCode.SZArray && item3 == SerializationTypeCode.Type)
						{
							ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>> valueTuple2 = unmanagedCallersOnlyDecoder.Invoke(text2, typedConstant2, !item);
							if (valueTuple2.Item1)
							{
								immutableHashSet = valueTuple2.Item2;
								break;
							}
						}
					}
				}
				catch (Exception ex) when (ex is BadImageFormatException || ex is UnsupportedSignatureContent)
				{
				}
			}
			return UnmanagedCallersOnlyAttributeData.Create(immutableHashSet);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00034A6C File Offset: 0x00032C6C
		internal bool HasMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(EntityHandle token, AttributeDescription description, out bool when)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, description);
			if (attributeInfo.HasValue && attributeInfo.SignatureIndex == 0)
			{
				return this.TryExtractValueFromAttribute<bool>(attributeInfo.Handle, out when, PEModule.s_attributeBooleanValueExtractor);
			}
			when = false;
			return false;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00034AAC File Offset: 0x00032CAC
		internal ImmutableHashSet<string> GetStringValuesOfNotNullIfNotNullAttribute(EntityHandle token)
		{
			List<PEModule.AttributeInfo> list = this.FindTargetAttributes(token, AttributeDescription.NotNullIfNotNullAttribute);
			ImmutableHashSet<string> immutableHashSet = ImmutableHashSet<string>.Empty;
			if (list == null)
			{
				return immutableHashSet;
			}
			foreach (PEModule.AttributeInfo attributeInfo in list)
			{
				string text;
				if (this.TryExtractStringValueFromAttribute(attributeInfo.Handle, out text))
				{
					immutableHashSet = immutableHashSet.Add(text);
				}
			}
			return immutableHashSet;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00034B28 File Offset: 0x00032D28
		internal CustomAttributeHandle GetAttributeUsageAttributeHandle(EntityHandle token)
		{
			return this.FindTargetAttribute(token, AttributeDescription.AttributeUsageAttribute).Handle;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00034B3C File Offset: 0x00032D3C
		internal bool HasInterfaceTypeAttribute(EntityHandle token, out ComInterfaceType interfaceType)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.InterfaceTypeAttribute);
			if (attributeInfo.HasValue && this.TryExtractInterfaceTypeFromAttribute(attributeInfo, out interfaceType))
			{
				return true;
			}
			interfaceType = 0;
			return false;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00034B70 File Offset: 0x00032D70
		internal bool HasTypeLibTypeAttribute(EntityHandle token, out TypeLibTypeFlags flags)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.TypeLibTypeAttribute);
			if (attributeInfo.HasValue && this.TryExtractTypeLibTypeFromAttribute(attributeInfo, out flags))
			{
				return true;
			}
			flags = (TypeLibTypeFlags)0;
			return false;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00034BA4 File Offset: 0x00032DA4
		internal bool HasDateTimeConstantAttribute(EntityHandle token, out ConstantValue defaultValue)
		{
			PEModule.AttributeInfo attributeInfo = this.FindLastTargetAttribute(token, AttributeDescription.DateTimeConstantAttribute);
			long num;
			if (attributeInfo.HasValue && this.TryExtractLongValueFromAttribute(attributeInfo.Handle, out num))
			{
				long num2 = num;
				DateTime dateTime = DateTime.MinValue;
				if (num2 >= dateTime.Ticks)
				{
					long num3 = num;
					dateTime = DateTime.MaxValue;
					if (num3 <= dateTime.Ticks)
					{
						defaultValue = ConstantValue.Create(new DateTime(num));
						return true;
					}
				}
				defaultValue = ConstantValue.Bad;
				return true;
			}
			defaultValue = null;
			return false;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00034C14 File Offset: 0x00032E14
		internal bool HasDecimalConstantAttribute(EntityHandle token, out ConstantValue defaultValue)
		{
			PEModule.AttributeInfo attributeInfo = this.FindLastTargetAttribute(token, AttributeDescription.DecimalConstantAttribute);
			decimal num;
			if (attributeInfo.HasValue && this.TryExtractDecimalValueFromDecimalConstantAttribute(attributeInfo.Handle, out num))
			{
				defaultValue = ConstantValue.Create(num);
				return true;
			}
			defaultValue = null;
			return false;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00034C58 File Offset: 0x00032E58
		internal bool HasNullablePublicOnlyAttribute(EntityHandle token, out bool includesInternals)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.NullablePublicOnlyAttribute);
			bool flag;
			if (attributeInfo.HasValue && this.TryExtractValueFromAttribute<bool>(attributeInfo.Handle, out flag, PEModule.s_attributeBooleanValueExtractor))
			{
				includesInternals = flag;
				return true;
			}
			includesInternals = false;
			return false;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00034C9C File Offset: 0x00032E9C
		internal ImmutableArray<string> GetInternalsVisibleToAttributeValues(EntityHandle token)
		{
			List<PEModule.AttributeInfo> list = this.FindTargetAttributes(token, AttributeDescription.InternalsVisibleToAttribute);
			ArrayBuilder<string> arrayBuilder = this.ExtractStringValuesFromAttributes(list);
			if (arrayBuilder == null)
			{
				return ImmutableArray<string>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00034CCC File Offset: 0x00032ECC
		internal ImmutableArray<string> GetConditionalAttributeValues(EntityHandle token)
		{
			List<PEModule.AttributeInfo> list = this.FindTargetAttributes(token, AttributeDescription.ConditionalAttribute);
			ArrayBuilder<string> arrayBuilder = this.ExtractStringValuesFromAttributes(list);
			if (arrayBuilder == null)
			{
				return ImmutableArray<string>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00034CFC File Offset: 0x00032EFC
		internal ImmutableArray<string> GetMemberNotNullAttributeValues(EntityHandle token)
		{
			List<PEModule.AttributeInfo> list = this.FindTargetAttributes(token, AttributeDescription.MemberNotNullAttribute);
			if (list == null || list.Count == 0)
			{
				return ImmutableArray<string>.Empty;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(list.Count);
			foreach (PEModule.AttributeInfo attributeInfo in list)
			{
				ImmutableArray<string> immutableArray;
				if (attributeInfo.SignatureIndex == 0)
				{
					string text;
					if (this.TryExtractStringValueFromAttribute(attributeInfo.Handle, out text) && text != null)
					{
						instance.Add(text);
					}
				}
				else if (this.TryExtractStringArrayValueFromAttribute(attributeInfo.Handle, out immutableArray))
				{
					foreach (string text2 in immutableArray)
					{
						if (text2 != null)
						{
							instance.Add(text2);
						}
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00034DD4 File Offset: 0x00032FD4
		[return: TupleElementNames(new string[] { "whenTrue", "whenFalse" })]
		internal ValueTuple<ImmutableArray<string>, ImmutableArray<string>> GetMemberNotNullWhenAttributeValues(EntityHandle token)
		{
			List<PEModule.AttributeInfo> list = this.FindTargetAttributes(token, AttributeDescription.MemberNotNullWhenAttribute);
			if (list == null || list.Count == 0)
			{
				return new ValueTuple<ImmutableArray<string>, ImmutableArray<string>>(ImmutableArray<string>.Empty, ImmutableArray<string>.Empty);
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(list.Count);
			ArrayBuilder<string> instance2 = ArrayBuilder<string>.GetInstance(list.Count);
			foreach (PEModule.AttributeInfo attributeInfo in list)
			{
				PEModule.BoolAndStringArrayData boolAndStringArrayData;
				if (attributeInfo.SignatureIndex == 0)
				{
					PEModule.BoolAndStringData boolAndStringData;
					if (this.TryExtractValueFromAttribute<PEModule.BoolAndStringData>(attributeInfo.Handle, out boolAndStringData, PEModule.s_attributeBoolAndStringValueExtractor) && boolAndStringData.String != null)
					{
						(boolAndStringData.Sense ? instance : instance2).Add(boolAndStringData.String);
					}
				}
				else if (this.TryExtractValueFromAttribute<PEModule.BoolAndStringArrayData>(attributeInfo.Handle, out boolAndStringArrayData, PEModule.s_attributeBoolAndStringArrayValueExtractor))
				{
					ArrayBuilder<string> arrayBuilder = (boolAndStringArrayData.Sense ? instance : instance2);
					foreach (string text in boolAndStringArrayData.Strings)
					{
						if (text != null)
						{
							arrayBuilder.Add(text);
						}
					}
				}
			}
			return new ValueTuple<ImmutableArray<string>, ImmutableArray<string>>(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree());
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00034F10 File Offset: 0x00033110
		private ArrayBuilder<string> ExtractStringValuesFromAttributes(List<PEModule.AttributeInfo> attrInfos)
		{
			if (attrInfos == null)
			{
				return null;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(attrInfos.Count);
			foreach (PEModule.AttributeInfo attributeInfo in attrInfos)
			{
				string text;
				if (this.TryExtractStringValueFromAttribute(attributeInfo.Handle, out text) && text != null)
				{
					instance.Add(text);
				}
			}
			return instance;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00034F84 File Offset: 0x00033184
		[NullableContext(1)]
		[return: Nullable(2)]
		private ObsoleteAttributeData TryExtractObsoleteDataFromAttribute(PEModule.AttributeInfo attributeInfo, IAttributeNamedArgumentDecoder decoder)
		{
			BlobReader blobReader;
			if (!this.TryGetAttributeReader(attributeInfo.Handle, out blobReader))
			{
				return null;
			}
			string text = null;
			bool flag = false;
			switch (attributeInfo.SignatureIndex)
			{
			case 0:
				break;
			case 1:
				if (blobReader.RemainingBytes <= 0 || !PEModule.CrackStringInAttributeValue(out text, ref blobReader))
				{
					return null;
				}
				break;
			case 2:
				if (blobReader.RemainingBytes <= 0 || !PEModule.CrackStringInAttributeValue(out text, ref blobReader) || blobReader.RemainingBytes <= 0 || !PEModule.CrackBooleanInAttributeValue(out flag, ref blobReader))
				{
					return null;
				}
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(attributeInfo.SignatureIndex);
			}
			string text2;
			string text3;
			if (blobReader.RemainingBytes <= 0)
			{
				text2 = null;
				text3 = null;
			}
			else
			{
				ValueTuple<string, string> valueTuple = PEModule.CrackObsoleteProperties(ref blobReader, decoder);
				text2 = valueTuple.Item1;
				text3 = valueTuple.Item2;
			}
			return new ObsoleteAttributeData(ObsoleteAttributeKind.Obsolete, text, flag, text2, text3);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00035050 File Offset: 0x00033250
		private bool TryGetAttributeReader(CustomAttributeHandle handle, out BlobReader blobReader)
		{
			try
			{
				BlobHandle customAttributeValueOrThrow = this.GetCustomAttributeValueOrThrow(handle);
				if (!customAttributeValueOrThrow.IsNil)
				{
					blobReader = this.MetadataReader.GetBlobReader(customAttributeValueOrThrow);
					if (blobReader.Length >= 4 && blobReader.ReadInt16() == 1)
					{
						return true;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			blobReader = default(BlobReader);
			return false;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000350B8 File Offset: 0x000332B8
		private ObsoleteAttributeData TryExtractDeprecatedDataFromAttribute(PEModule.AttributeInfo attributeInfo)
		{
			byte signatureIndex = attributeInfo.SignatureIndex;
			if (signatureIndex > 3)
			{
				throw ExceptionUtilities.UnexpectedValue(attributeInfo.SignatureIndex);
			}
			ObsoleteAttributeData obsoleteAttributeData;
			if (!this.TryExtractValueFromAttribute<ObsoleteAttributeData>(attributeInfo.Handle, out obsoleteAttributeData, PEModule.s_attributeDeprecatedDataExtractor))
			{
				return null;
			}
			return obsoleteAttributeData;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000350F9 File Offset: 0x000332F9
		private ObsoleteAttributeData TryExtractExperimentalDataFromAttribute(PEModule.AttributeInfo attributeInfo)
		{
			if (attributeInfo.SignatureIndex == 0)
			{
				return ObsoleteAttributeData.Experimental;
			}
			throw ExceptionUtilities.UnexpectedValue(attributeInfo.SignatureIndex);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003511C File Offset: 0x0003331C
		private bool TryExtractInterfaceTypeFromAttribute(PEModule.AttributeInfo attributeInfo, out ComInterfaceType interfaceType)
		{
			byte signatureIndex = attributeInfo.SignatureIndex;
			short num2;
			if (signatureIndex != 0)
			{
				if (signatureIndex != 1)
				{
					throw ExceptionUtilities.UnexpectedValue(attributeInfo.SignatureIndex);
				}
				int num;
				if (this.TryExtractValueFromAttribute<int>(attributeInfo.Handle, out num, PEModule.s_attributeIntValueExtractor) && PEModule.IsValidComInterfaceType(num))
				{
					interfaceType = num;
					return true;
				}
			}
			else if (this.TryExtractValueFromAttribute<short>(attributeInfo.Handle, out num2, PEModule.s_attributeShortValueExtractor) && PEModule.IsValidComInterfaceType((int)num2))
			{
				interfaceType = (int)num2;
				return true;
			}
			interfaceType = 0;
			return false;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00035192 File Offset: 0x00033392
		private static bool IsValidComInterfaceType(int comInterfaceType)
		{
			return comInterfaceType <= 3;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003519C File Offset: 0x0003339C
		private bool TryExtractTypeLibTypeFromAttribute(PEModule.AttributeInfo info, out TypeLibTypeFlags flags)
		{
			byte signatureIndex = info.SignatureIndex;
			short num2;
			if (signatureIndex != 0)
			{
				if (signatureIndex != 1)
				{
					throw ExceptionUtilities.UnexpectedValue(info.SignatureIndex);
				}
				int num;
				if (this.TryExtractValueFromAttribute<int>(info.Handle, out num, PEModule.s_attributeIntValueExtractor))
				{
					flags = (TypeLibTypeFlags)num;
					return true;
				}
			}
			else if (this.TryExtractValueFromAttribute<short>(info.Handle, out num2, PEModule.s_attributeShortValueExtractor))
			{
				flags = (TypeLibTypeFlags)num2;
				return true;
			}
			flags = (TypeLibTypeFlags)0;
			return false;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00035202 File Offset: 0x00033402
		internal bool TryExtractStringValueFromAttribute(CustomAttributeHandle handle, out string value)
		{
			return this.TryExtractValueFromAttribute<string>(handle, out value, PEModule.s_attributeStringValueExtractor);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00035211 File Offset: 0x00033411
		internal bool TryExtractLongValueFromAttribute(CustomAttributeHandle handle, out long value)
		{
			return this.TryExtractValueFromAttribute<long>(handle, out value, PEModule.s_attributeLongValueExtractor);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00035220 File Offset: 0x00033420
		private bool TryExtractDecimalValueFromDecimalConstantAttribute(CustomAttributeHandle handle, out decimal value)
		{
			return this.TryExtractValueFromAttribute<decimal>(handle, out value, PEModule.s_decimalValueInDecimalConstantAttributeExtractor);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00035230 File Offset: 0x00033430
		private bool TryExtractStringAndIntValueFromAttribute(CustomAttributeHandle handle, out string stringValue, out int intValue)
		{
			PEModule.StringAndInt stringAndInt;
			bool flag = this.TryExtractValueFromAttribute<PEModule.StringAndInt>(handle, out stringAndInt, PEModule.s_attributeStringAndIntValueExtractor);
			stringValue = stringAndInt.StringValue;
			intValue = stringAndInt.IntValue;
			return flag;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003525B File Offset: 0x0003345B
		private bool TryExtractBoolArrayValueFromAttribute(CustomAttributeHandle handle, out ImmutableArray<bool> value)
		{
			return this.TryExtractValueFromAttribute<ImmutableArray<bool>>(handle, out value, PEModule.s_attributeBoolArrayValueExtractor);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003526A File Offset: 0x0003346A
		private bool TryExtractByteArrayValueFromAttribute(CustomAttributeHandle handle, out ImmutableArray<byte> value)
		{
			return this.TryExtractValueFromAttribute<ImmutableArray<byte>>(handle, out value, PEModule.s_attributeByteArrayValueExtractor);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00035279 File Offset: 0x00033479
		private bool TryExtractStringArrayValueFromAttribute(CustomAttributeHandle handle, out ImmutableArray<string> value)
		{
			return this.TryExtractValueFromAttribute<ImmutableArray<string>>(handle, out value, PEModule.s_attributeStringArrayValueExtractor);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00035288 File Offset: 0x00033488
		private bool TryExtractValueFromAttribute<T>(CustomAttributeHandle handle, out T value, PEModule.AttributeValueExtractor<T> valueExtractor)
		{
			try
			{
				BlobHandle customAttributeValueOrThrow = this.GetCustomAttributeValueOrThrow(handle);
				if (!customAttributeValueOrThrow.IsNil)
				{
					BlobReader blobReader = this.MetadataReader.GetBlobReader(customAttributeValueOrThrow);
					if (blobReader.Length > 4 && blobReader.ReadByte() == 1 && blobReader.ReadByte() == 0)
					{
						return valueExtractor(out value, ref blobReader);
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000352FC File Offset: 0x000334FC
		internal bool HasStringValuedAttribute(EntityHandle token, AttributeDescription description, out string value)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, description);
			if (attributeInfo.HasValue)
			{
				return this.TryExtractStringValueFromAttribute(attributeInfo.Handle, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00035330 File Offset: 0x00033530
		private bool HasStringAndIntValuedAttribute(EntityHandle token, AttributeDescription description, out string stringValue, out int intValue)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, description);
			if (attributeInfo.HasValue)
			{
				return this.TryExtractStringAndIntValueFromAttribute(attributeInfo.Handle, out stringValue, out intValue);
			}
			stringValue = null;
			intValue = 0;
			return false;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00035368 File Offset: 0x00033568
		internal bool IsNoPiaLocalType(TypeDefinitionHandle typeDef, out string interfaceGuid, out string scope, out string identifier)
		{
			PEModule.AttributeInfo attributeInfo;
			if (!this.IsNoPiaLocalType(typeDef, out attributeInfo))
			{
				interfaceGuid = null;
				scope = null;
				identifier = null;
				return false;
			}
			interfaceGuid = null;
			scope = null;
			identifier = null;
			bool flag;
			try
			{
				if (this.GetTypeDefFlagsOrThrow(typeDef).IsInterface())
				{
					this.HasGuidAttribute(typeDef, out interfaceGuid);
				}
				if (attributeInfo.SignatureIndex == 1)
				{
					BlobHandle customAttributeValueOrThrow = this.GetCustomAttributeValueOrThrow(attributeInfo.Handle);
					if (!customAttributeValueOrThrow.IsNil)
					{
						BlobReader blobReader = this.MetadataReader.GetBlobReader(customAttributeValueOrThrow);
						if (blobReader.Length > 4 && blobReader.ReadInt16() == 1 && (!PEModule.CrackStringInAttributeValue(out scope, ref blobReader) || !PEModule.CrackStringInAttributeValue(out identifier, ref blobReader)))
						{
							return false;
						}
					}
				}
				flag = true;
			}
			catch (BadImageFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00035428 File Offset: 0x00033628
		[NullableContext(1)]
		[return: TupleElementNames(new string[] { "diagnosticId", "urlFormat" })]
		[return: Nullable(new byte[] { 0, 2, 2 })]
		private static ValueTuple<string, string> CrackObsoleteProperties(ref BlobReader sig, IAttributeNamedArgumentDecoder decoder)
		{
			string text = null;
			string text2 = null;
			try
			{
				ushort num = sig.ReadUInt16();
				int num2 = 0;
				while (num2 < (int)num && (text == null || text2 == null))
				{
					ValueTuple<KeyValuePair<string, TypedConstant>, bool, SerializationTypeCode, SerializationTypeCode> valueTuple = decoder.DecodeCustomAttributeNamedArgumentOrThrow(ref sig);
					string text3;
					TypedConstant typedConstant;
					valueTuple.Item1.Deconstruct<string, TypedConstant>(out text3, out typedConstant);
					string text4 = text3;
					TypedConstant typedConstant2 = typedConstant;
					bool item = valueTuple.Item2;
					if (valueTuple.Item3 == SerializationTypeCode.String && item)
					{
						string text5 = typedConstant2.ValueInternal as string;
						if (text5 != null)
						{
							if (text == null && text4 == "DiagnosticId")
							{
								text = text5;
							}
							else if (text2 == null && text4 == "UrlFormat")
							{
								text2 = text5;
							}
						}
					}
					num2++;
				}
			}
			catch (BadImageFormatException)
			{
			}
			catch (UnsupportedSignatureContent)
			{
			}
			return new ValueTuple<string, string>(text, text2);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000354F0 File Offset: 0x000336F0
		private static bool CrackDeprecatedAttributeData(out ObsoleteAttributeData value, ref BlobReader sig)
		{
			PEModule.StringAndInt stringAndInt;
			if (PEModule.CrackStringAndIntInAttributeValue(out stringAndInt, ref sig))
			{
				value = new ObsoleteAttributeData(ObsoleteAttributeKind.Deprecated, stringAndInt.StringValue, stringAndInt.IntValue == 1, null, null);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00035526 File Offset: 0x00033726
		private static bool CrackStringAndIntInAttributeValue(out PEModule.StringAndInt value, ref BlobReader sig)
		{
			value = default(PEModule.StringAndInt);
			return PEModule.CrackStringInAttributeValue(out value.StringValue, ref sig) && PEModule.CrackIntInAttributeValue(out value.IntValue, ref sig);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003554C File Offset: 0x0003374C
		internal static bool CrackStringInAttributeValue(out string value, ref BlobReader sig)
		{
			bool flag;
			try
			{
				int num;
				if (sig.TryReadCompressedInteger(out num) && sig.RemainingBytes >= num)
				{
					value = sig.ReadUTF8(num);
					value = value.TrimEnd(new char[1]);
					flag = true;
				}
				else
				{
					value = null;
					flag = sig.RemainingBytes >= 1 && sig.ReadByte() == byte.MaxValue;
				}
			}
			catch (BadImageFormatException)
			{
				value = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000355C0 File Offset: 0x000337C0
		internal static bool CrackStringArrayInAttributeValue(out ImmutableArray<string> value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 4)
			{
				uint num = sig.ReadUInt32();
				string[] array = new string[num];
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					if (!PEModule.CrackStringInAttributeValue(out array[num2], ref sig))
					{
						value = array.AsImmutableOrNull<string>();
						return false;
					}
					num2++;
				}
				value = array.AsImmutableOrNull<string>();
				return true;
			}
			value = default(ImmutableArray<string>);
			return false;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00035628 File Offset: 0x00033828
		internal static bool CrackBoolAndStringArrayInAttributeValue(out PEModule.BoolAndStringArrayData value, ref BlobReader sig)
		{
			bool flag;
			ImmutableArray<string> immutableArray;
			if (PEModule.CrackBooleanInAttributeValue(out flag, ref sig) && PEModule.CrackStringArrayInAttributeValue(out immutableArray, ref sig))
			{
				value = new PEModule.BoolAndStringArrayData(flag, immutableArray);
				return true;
			}
			value = default(PEModule.BoolAndStringArrayData);
			return false;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00035660 File Offset: 0x00033860
		internal static bool CrackBoolAndStringInAttributeValue(out PEModule.BoolAndStringData value, ref BlobReader sig)
		{
			bool flag;
			string text;
			if (PEModule.CrackBooleanInAttributeValue(out flag, ref sig) && PEModule.CrackStringInAttributeValue(out text, ref sig))
			{
				value = new PEModule.BoolAndStringData(flag, text);
				return true;
			}
			value = default(PEModule.BoolAndStringData);
			return false;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00035698 File Offset: 0x00033898
		internal static bool CrackBooleanInAttributeValue(out bool value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 1)
			{
				value = sig.ReadBoolean();
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000356B1 File Offset: 0x000338B1
		internal static bool CrackByteInAttributeValue(out byte value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 1)
			{
				value = sig.ReadByte();
				return true;
			}
			value = byte.MaxValue;
			return false;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000356CE File Offset: 0x000338CE
		internal static bool CrackShortInAttributeValue(out short value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 2)
			{
				value = sig.ReadInt16();
				return true;
			}
			value = -1;
			return false;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000356E7 File Offset: 0x000338E7
		internal static bool CrackIntInAttributeValue(out int value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 4)
			{
				value = sig.ReadInt32();
				return true;
			}
			value = -1;
			return false;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00035700 File Offset: 0x00033900
		internal static bool CrackLongInAttributeValue(out long value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 8)
			{
				value = sig.ReadInt64();
				return true;
			}
			value = -1L;
			return false;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003571C File Offset: 0x0003391C
		private static bool CrackDecimalInDecimalConstantAttribute(out decimal value, ref BlobReader sig)
		{
			byte b;
			byte b2;
			int num;
			int num2;
			int num3;
			if (PEModule.CrackByteInAttributeValue(out b, ref sig) && PEModule.CrackByteInAttributeValue(out b2, ref sig) && PEModule.CrackIntInAttributeValue(out num, ref sig) && PEModule.CrackIntInAttributeValue(out num2, ref sig) && PEModule.CrackIntInAttributeValue(out num3, ref sig))
			{
				value = new decimal(num3, num2, num, b2 > 0, b);
				return true;
			}
			value = -1m;
			return false;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00035780 File Offset: 0x00033980
		internal static bool CrackBoolArrayInAttributeValue(out ImmutableArray<bool> value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 4)
			{
				uint num = sig.ReadUInt32();
				if ((long)sig.RemainingBytes >= (long)((ulong)num))
				{
					ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance((int)num);
					int num2 = 0;
					while ((long)num2 < (long)((ulong)num))
					{
						instance.Add(sig.ReadByte() == 1);
						num2++;
					}
					value = instance.ToImmutableAndFree();
					return true;
				}
			}
			value = default(ImmutableArray<bool>);
			return false;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000357E4 File Offset: 0x000339E4
		internal static bool CrackByteArrayInAttributeValue(out ImmutableArray<byte> value, ref BlobReader sig)
		{
			if (sig.RemainingBytes >= 4)
			{
				uint num = sig.ReadUInt32();
				if ((long)sig.RemainingBytes >= (long)((ulong)num))
				{
					ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance((int)num);
					int num2 = 0;
					while ((long)num2 < (long)((ulong)num))
					{
						instance.Add(sig.ReadByte());
						num2++;
					}
					value = instance.ToImmutableAndFree();
					return true;
				}
			}
			value = default(ImmutableArray<byte>);
			return false;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00035844 File Offset: 0x00033A44
		internal List<PEModule.AttributeInfo> FindTargetAttributes(EntityHandle hasAttribute, AttributeDescription description)
		{
			List<PEModule.AttributeInfo> list = null;
			try
			{
				foreach (CustomAttributeHandle customAttributeHandle in this.MetadataReader.GetCustomAttributes(hasAttribute))
				{
					int targetAttributeSignatureIndex = this.GetTargetAttributeSignatureIndex(customAttributeHandle, description);
					if (targetAttributeSignatureIndex != -1)
					{
						if (list == null)
						{
							list = new List<PEModule.AttributeInfo>();
						}
						list.Add(new PEModule.AttributeInfo(customAttributeHandle, targetAttributeSignatureIndex));
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return list;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000358D4 File Offset: 0x00033AD4
		internal PEModule.AttributeInfo FindTargetAttribute(EntityHandle hasAttribute, AttributeDescription description)
		{
			return PEModule.FindTargetAttribute(this.MetadataReader, hasAttribute, description);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000358E4 File Offset: 0x00033AE4
		internal static PEModule.AttributeInfo FindTargetAttribute(MetadataReader metadataReader, EntityHandle hasAttribute, AttributeDescription description)
		{
			try
			{
				foreach (CustomAttributeHandle customAttributeHandle in metadataReader.GetCustomAttributes(hasAttribute))
				{
					int targetAttributeSignatureIndex = PEModule.GetTargetAttributeSignatureIndex(metadataReader, customAttributeHandle, description);
					if (targetAttributeSignatureIndex != -1)
					{
						return new PEModule.AttributeInfo(customAttributeHandle, targetAttributeSignatureIndex);
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return default(PEModule.AttributeInfo);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0003596C File Offset: 0x00033B6C
		internal PEModule.AttributeInfo FindLastTargetAttribute(EntityHandle hasAttribute, AttributeDescription description)
		{
			try
			{
				PEModule.AttributeInfo attributeInfo = default(PEModule.AttributeInfo);
				foreach (CustomAttributeHandle customAttributeHandle in this.MetadataReader.GetCustomAttributes(hasAttribute))
				{
					int targetAttributeSignatureIndex = this.GetTargetAttributeSignatureIndex(customAttributeHandle, description);
					if (targetAttributeSignatureIndex != -1)
					{
						attributeInfo = new PEModule.AttributeInfo(customAttributeHandle, targetAttributeSignatureIndex);
					}
				}
				return attributeInfo;
			}
			catch (BadImageFormatException)
			{
			}
			return default(PEModule.AttributeInfo);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00035A04 File Offset: 0x00033C04
		internal int GetParamArrayCountOrThrow(EntityHandle hasAttribute)
		{
			int num = 0;
			foreach (CustomAttributeHandle customAttributeHandle in this.MetadataReader.GetCustomAttributes(hasAttribute))
			{
				if (this.GetTargetAttributeSignatureIndex(customAttributeHandle, AttributeDescription.ParamArrayAttribute) != -1)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00035A70 File Offset: 0x00033C70
		private bool IsNoPiaLocalType(TypeDefinitionHandle typeDef, out PEModule.AttributeInfo attributeInfo)
		{
			if (this._lazyContainsNoPiaLocalTypes == ThreeState.False)
			{
				attributeInfo = default(PEModule.AttributeInfo);
				return false;
			}
			if (this._lazyNoPiaLocalTypeCheckBitMap != null && this._lazyTypeDefToTypeIdentifierMap != null)
			{
				int rowNumber = this.MetadataReader.GetRowNumber(typeDef);
				int num = rowNumber / 32;
				int num2 = 1 << rowNumber % 32;
				if ((this._lazyNoPiaLocalTypeCheckBitMap[num] & num2) != 0)
				{
					return this._lazyTypeDefToTypeIdentifierMap.TryGetValue(typeDef, ref attributeInfo);
				}
			}
			try
			{
				foreach (CustomAttributeHandle customAttributeHandle in this.MetadataReader.GetCustomAttributes(typeDef))
				{
					int num3 = this.IsTypeIdentifierAttribute(customAttributeHandle);
					if (num3 != -1)
					{
						this._lazyContainsNoPiaLocalTypes = ThreeState.True;
						this.RegisterNoPiaLocalType(typeDef, customAttributeHandle, num3);
						attributeInfo = new PEModule.AttributeInfo(customAttributeHandle, num3);
						return true;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			this.RecordNoPiaLocalTypeCheck(typeDef);
			attributeInfo = default(PEModule.AttributeInfo);
			return false;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00035B84 File Offset: 0x00033D84
		private void RegisterNoPiaLocalType(TypeDefinitionHandle typeDef, CustomAttributeHandle customAttribute, int signatureIndex)
		{
			if (this._lazyNoPiaLocalTypeCheckBitMap == null)
			{
				Interlocked.CompareExchange<int[]>(ref this._lazyNoPiaLocalTypeCheckBitMap, new int[(this.MetadataReader.TypeDefinitions.Count + 32) / 32], null);
			}
			if (this._lazyTypeDefToTypeIdentifierMap == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<TypeDefinitionHandle, PEModule.AttributeInfo>>(ref this._lazyTypeDefToTypeIdentifierMap, new ConcurrentDictionary<TypeDefinitionHandle, PEModule.AttributeInfo>(), null);
			}
			this._lazyTypeDefToTypeIdentifierMap.TryAdd(typeDef, new PEModule.AttributeInfo(customAttribute, signatureIndex));
			this.RecordNoPiaLocalTypeCheck(typeDef);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00035BFC File Offset: 0x00033DFC
		private void RecordNoPiaLocalTypeCheck(TypeDefinitionHandle typeDef)
		{
			if (this._lazyNoPiaLocalTypeCheckBitMap == null)
			{
				return;
			}
			int rowNumber = MetadataTokens.GetRowNumber(typeDef);
			int num = rowNumber / 32;
			int num2 = 1 << rowNumber % 32;
			int num3;
			do
			{
				num3 = this._lazyNoPiaLocalTypeCheckBitMap[num];
			}
			while (Interlocked.CompareExchange(ref this._lazyNoPiaLocalTypeCheckBitMap[num], num3 | num2, num3) != num3);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00035C50 File Offset: 0x00033E50
		private int IsTypeIdentifierAttribute(CustomAttributeHandle customAttribute)
		{
			int num;
			try
			{
				if (this.MetadataReader.GetCustomAttribute(customAttribute).Parent.Kind != HandleKind.TypeDefinition)
				{
					num = -1;
				}
				else
				{
					num = this.GetTargetAttributeSignatureIndex(customAttribute, AttributeDescription.TypeIdentifierAttribute);
				}
			}
			catch (BadImageFormatException)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00035CA8 File Offset: 0x00033EA8
		internal bool IsTargetAttribute(CustomAttributeHandle customAttribute, string namespaceName, string typeName, out EntityHandle ctor, bool ignoreCase = false)
		{
			return PEModule.IsTargetAttribute(this.MetadataReader, customAttribute, namespaceName, typeName, out ctor, ignoreCase);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00035CBC File Offset: 0x00033EBC
		private static bool IsTargetAttribute(MetadataReader metadataReader, CustomAttributeHandle customAttribute, string namespaceName, string typeName, out EntityHandle ctor, bool ignoreCase)
		{
			EntityHandle entityHandle;
			if (!PEModule.GetTypeAndConstructor(metadataReader, customAttribute, out entityHandle, out ctor))
			{
				return false;
			}
			StringHandle stringHandle;
			StringHandle stringHandle2;
			if (!PEModule.GetAttributeNamespaceAndName(metadataReader, entityHandle, out stringHandle, out stringHandle2))
			{
				return false;
			}
			bool flag;
			try
			{
				flag = PEModule.StringEquals(metadataReader, stringHandle2, typeName, ignoreCase) && PEModule.StringEquals(metadataReader, stringHandle, namespaceName, ignoreCase);
			}
			catch (BadImageFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00035D1C File Offset: 0x00033F1C
		internal AssemblyReferenceHandle GetAssemblyRef(string assemblyName)
		{
			try
			{
				foreach (AssemblyReferenceHandle assemblyReferenceHandle in this.MetadataReader.AssemblyReferences)
				{
					if (this.MetadataReader.StringComparer.Equals(this.MetadataReader.GetAssemblyReference(assemblyReferenceHandle).Name, assemblyName))
					{
						return assemblyReferenceHandle;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return default(AssemblyReferenceHandle);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00035DC0 File Offset: 0x00033FC0
		internal EntityHandle GetTypeRef(EntityHandle resolutionScope, string namespaceName, string typeName)
		{
			try
			{
				foreach (TypeReferenceHandle typeReferenceHandle in this.MetadataReader.TypeReferences)
				{
					TypeReference typeReference = this.MetadataReader.GetTypeReference(typeReferenceHandle);
					if (!(typeReference.ResolutionScope != resolutionScope) && this.MetadataReader.StringComparer.Equals(typeReference.Name, typeName) && this.MetadataReader.StringComparer.Equals(typeReference.Namespace, namespaceName))
					{
						return typeReferenceHandle;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return default(TypeReferenceHandle);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00035E9C File Offset: 0x0003409C
		public void GetTypeRefPropsOrThrow(TypeReferenceHandle handle, out string name, out string @namespace, out EntityHandle resolutionScope)
		{
			TypeReference typeReference = this.MetadataReader.GetTypeReference(handle);
			resolutionScope = typeReference.ResolutionScope;
			name = this.MetadataReader.GetString(typeReference.Name);
			@namespace = this.MetadataReader.GetString(typeReference.Namespace);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00035EEC File Offset: 0x000340EC
		internal int GetTargetAttributeSignatureIndex(CustomAttributeHandle customAttribute, AttributeDescription description)
		{
			return PEModule.GetTargetAttributeSignatureIndex(this.MetadataReader, customAttribute, description);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00035EFC File Offset: 0x000340FC
		private static int GetTargetAttributeSignatureIndex(MetadataReader metadataReader, CustomAttributeHandle customAttribute, AttributeDescription description)
		{
			EntityHandle entityHandle;
			if (!PEModule.IsTargetAttribute(metadataReader, customAttribute, description.Namespace, description.Name, out entityHandle, description.MatchIgnoringCase))
			{
				return -1;
			}
			try
			{
				BlobReader blobReader = metadataReader.GetBlobReader(PEModule.GetMethodSignatureOrThrow(metadataReader, entityHandle));
				for (int i = 0; i < description.Signatures.Length; i++)
				{
					byte[] array = description.Signatures[i];
					blobReader.Reset();
					if (blobReader.RemainingBytes >= 3 && blobReader.ReadByte() == array[0] && blobReader.ReadByte() == array[1] && blobReader.ReadByte() == array[2])
					{
						int num = 3;
						while (num < array.Length && blobReader.RemainingBytes != 0)
						{
							SignatureTypeCode signatureTypeCode = blobReader.ReadSignatureTypeCode();
							if (array[num] != (byte)signatureTypeCode)
							{
								break;
							}
							if (signatureTypeCode != SignatureTypeCode.SZArray && signatureTypeCode == SignatureTypeCode.TypeHandle)
							{
								EntityHandle entityHandle2 = blobReader.ReadTypeHandle();
								HandleKind kind = entityHandle2.Kind;
								StringHandle stringHandle;
								StringHandle stringHandle2;
								if (kind == HandleKind.TypeDefinition)
								{
									TypeDefinitionHandle typeDefinitionHandle = (TypeDefinitionHandle)entityHandle2;
									if (PEModule.IsNestedTypeDefOrThrow(metadataReader, typeDefinitionHandle))
									{
										break;
									}
									TypeDefinition typeDefinition = metadataReader.GetTypeDefinition(typeDefinitionHandle);
									stringHandle = typeDefinition.Name;
									stringHandle2 = typeDefinition.Namespace;
								}
								else
								{
									if (kind != HandleKind.TypeReference)
									{
										break;
									}
									TypeReference typeReference = metadataReader.GetTypeReference((TypeReferenceHandle)entityHandle2);
									if (typeReference.ResolutionScope.Kind == HandleKind.TypeReference)
									{
										break;
									}
									stringHandle = typeReference.Name;
									stringHandle2 = typeReference.Namespace;
								}
								AttributeDescription.TypeHandleTargetInfo typeHandleTargetInfo = AttributeDescription.TypeHandleTargets[(int)array[num + 1]];
								if (!PEModule.StringEquals(metadataReader, stringHandle2, typeHandleTargetInfo.Namespace, false) || !PEModule.StringEquals(metadataReader, stringHandle, typeHandleTargetInfo.Name, false))
								{
									break;
								}
								num++;
							}
							num++;
						}
						if (blobReader.RemainingBytes == 0 && num == array.Length)
						{
							return i;
						}
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			return -1;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000360E4 File Offset: 0x000342E4
		internal bool GetTypeAndConstructor(CustomAttributeHandle customAttribute, out EntityHandle ctorType, out EntityHandle attributeCtor)
		{
			return PEModule.GetTypeAndConstructor(this.MetadataReader, customAttribute, out ctorType, out attributeCtor);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000360F4 File Offset: 0x000342F4
		private static bool GetTypeAndConstructor(MetadataReader metadataReader, CustomAttributeHandle customAttribute, out EntityHandle ctorType, out EntityHandle attributeCtor)
		{
			bool flag;
			try
			{
				ctorType = default(EntityHandle);
				attributeCtor = metadataReader.GetCustomAttribute(customAttribute).Constructor;
				if (attributeCtor.Kind == HandleKind.MemberReference)
				{
					MemberReference memberReference = metadataReader.GetMemberReference((MemberReferenceHandle)attributeCtor);
					StringHandle name = memberReference.Name;
					if (!metadataReader.StringComparer.Equals(name, ".ctor"))
					{
						return false;
					}
					ctorType = memberReference.Parent;
				}
				else
				{
					if (attributeCtor.Kind != HandleKind.MethodDefinition)
					{
						return false;
					}
					MethodDefinition methodDefinition = metadataReader.GetMethodDefinition((MethodDefinitionHandle)attributeCtor);
					if (!metadataReader.StringComparer.Equals(methodDefinition.Name, ".ctor"))
					{
						return false;
					}
					ctorType = methodDefinition.GetDeclaringType();
				}
				flag = true;
			}
			catch (BadImageFormatException)
			{
				ctorType = default(EntityHandle);
				attributeCtor = default(EntityHandle);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000361EC File Offset: 0x000343EC
		internal bool GetAttributeNamespaceAndName(EntityHandle typeDefOrRef, out StringHandle namespaceHandle, out StringHandle nameHandle)
		{
			return PEModule.GetAttributeNamespaceAndName(this.MetadataReader, typeDefOrRef, out namespaceHandle, out nameHandle);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000361FC File Offset: 0x000343FC
		private static bool GetAttributeNamespaceAndName(MetadataReader metadataReader, EntityHandle typeDefOrRef, out StringHandle namespaceHandle, out StringHandle nameHandle)
		{
			nameHandle = default(StringHandle);
			namespaceHandle = default(StringHandle);
			bool flag;
			try
			{
				if (typeDefOrRef.Kind == HandleKind.TypeReference)
				{
					TypeReference typeReference = metadataReader.GetTypeReference((TypeReferenceHandle)typeDefOrRef);
					HandleKind kind = typeReference.ResolutionScope.Kind;
					if (kind == HandleKind.TypeReference || kind == HandleKind.TypeDefinition)
					{
						return false;
					}
					nameHandle = typeReference.Name;
					namespaceHandle = typeReference.Namespace;
				}
				else
				{
					if (typeDefOrRef.Kind != HandleKind.TypeDefinition)
					{
						return false;
					}
					TypeDefinition typeDefinition = metadataReader.GetTypeDefinition((TypeDefinitionHandle)typeDefOrRef);
					if (PEModule.IsNested(typeDefinition.Attributes))
					{
						return false;
					}
					nameHandle = typeDefinition.Name;
					namespaceHandle = typeDefinition.Namespace;
				}
				flag = true;
			}
			catch (BadImageFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000362CC File Offset: 0x000344CC
		internal void PretendThereArentNoPiaLocalTypes()
		{
			this._lazyContainsNoPiaLocalTypes = ThreeState.False;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000362D8 File Offset: 0x000344D8
		internal bool ContainsNoPiaLocalTypes()
		{
			if (this._lazyContainsNoPiaLocalTypes == ThreeState.Unknown)
			{
				try
				{
					foreach (CustomAttributeHandle customAttributeHandle in this.MetadataReader.CustomAttributes)
					{
						int num = this.IsTypeIdentifierAttribute(customAttributeHandle);
						if (num != -1)
						{
							this._lazyContainsNoPiaLocalTypes = ThreeState.True;
							TypeDefinitionHandle typeDefinitionHandle = (TypeDefinitionHandle)this.MetadataReader.GetCustomAttribute(customAttributeHandle).Parent;
							this.RegisterNoPiaLocalType(typeDefinitionHandle, customAttributeHandle, num);
							return true;
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				this._lazyContainsNoPiaLocalTypes = ThreeState.False;
			}
			return this._lazyContainsNoPiaLocalTypes == ThreeState.True;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003639C File Offset: 0x0003459C
		internal bool HasNullableContextAttribute(EntityHandle token, out byte value)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.NullableContextAttribute);
			if (!attributeInfo.HasValue)
			{
				value = 0;
				return false;
			}
			return this.TryExtractValueFromAttribute<byte>(attributeInfo.Handle, out value, PEModule.s_attributeByteValueExtractor);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000363D8 File Offset: 0x000345D8
		internal bool HasNullableAttribute(EntityHandle token, out byte defaultTransform, out ImmutableArray<byte> nullableTransforms)
		{
			PEModule.AttributeInfo attributeInfo = this.FindTargetAttribute(token, AttributeDescription.NullableAttribute);
			defaultTransform = 0;
			nullableTransforms = default(ImmutableArray<byte>);
			if (!attributeInfo.HasValue)
			{
				return false;
			}
			if (attributeInfo.SignatureIndex == 0)
			{
				return this.TryExtractValueFromAttribute<byte>(attributeInfo.Handle, out defaultTransform, PEModule.s_attributeByteValueExtractor);
			}
			return this.TryExtractByteArrayValueFromAttribute(attributeInfo.Handle, out nullableTransforms);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00036430 File Offset: 0x00034630
		internal BlobReader GetTypeSpecificationSignatureReaderOrThrow(TypeSpecificationHandle typeSpec)
		{
			BlobHandle signature = this.MetadataReader.GetTypeSpecification(typeSpec).Signature;
			return this.MetadataReader.GetBlobReader(signature);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00036460 File Offset: 0x00034660
		internal void GetMethodSpecificationOrThrow(MethodSpecificationHandle handle, out EntityHandle method, out BlobHandle instantiation)
		{
			MethodSpecification methodSpecification = this.MetadataReader.GetMethodSpecification(handle);
			method = methodSpecification.Method;
			instantiation = methodSpecification.Signature;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00036494 File Offset: 0x00034694
		internal void GetGenericParamPropsOrThrow(GenericParameterHandle handle, out string name, out GenericParameterAttributes flags)
		{
			GenericParameter genericParameter = this.MetadataReader.GetGenericParameter(handle);
			name = this.MetadataReader.GetString(genericParameter.Name);
			flags = genericParameter.Attributes;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000364CC File Offset: 0x000346CC
		internal string GetMethodDefNameOrThrow(MethodDefinitionHandle methodDef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetMethodDefinition(methodDef).Name);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000364F8 File Offset: 0x000346F8
		internal BlobHandle GetMethodSignatureOrThrow(MethodDefinitionHandle methodDef)
		{
			return PEModule.GetMethodSignatureOrThrow(this.MetadataReader, methodDef);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00036508 File Offset: 0x00034708
		private static BlobHandle GetMethodSignatureOrThrow(MetadataReader metadataReader, MethodDefinitionHandle methodDef)
		{
			return metadataReader.GetMethodDefinition(methodDef).Signature;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00036524 File Offset: 0x00034724
		internal BlobHandle GetMethodSignatureOrThrow(EntityHandle methodDefOrRef)
		{
			return PEModule.GetMethodSignatureOrThrow(this.MetadataReader, methodDefOrRef);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00036534 File Offset: 0x00034734
		private static BlobHandle GetMethodSignatureOrThrow(MetadataReader metadataReader, EntityHandle methodDefOrRef)
		{
			HandleKind kind = methodDefOrRef.Kind;
			if (kind == HandleKind.MethodDefinition)
			{
				return PEModule.GetMethodSignatureOrThrow(metadataReader, (MethodDefinitionHandle)methodDefOrRef);
			}
			if (kind != HandleKind.MemberReference)
			{
				throw ExceptionUtilities.UnexpectedValue(methodDefOrRef.Kind);
			}
			return PEModule.GetSignatureOrThrow(metadataReader, (MemberReferenceHandle)methodDefOrRef);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00036580 File Offset: 0x00034780
		public MethodAttributes GetMethodDefFlagsOrThrow(MethodDefinitionHandle methodDef)
		{
			return this.MetadataReader.GetMethodDefinition(methodDef).Attributes;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000365A4 File Offset: 0x000347A4
		internal TypeDefinitionHandle FindContainingTypeOrThrow(MethodDefinitionHandle methodDef)
		{
			return this.MetadataReader.GetMethodDefinition(methodDef).GetDeclaringType();
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000365C8 File Offset: 0x000347C8
		internal TypeDefinitionHandle FindContainingTypeOrThrow(FieldDefinitionHandle fieldDef)
		{
			return this.MetadataReader.GetFieldDefinition(fieldDef).GetDeclaringType();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000365EC File Offset: 0x000347EC
		internal EntityHandle GetContainingTypeOrThrow(MemberReferenceHandle memberRef)
		{
			return this.MetadataReader.GetMemberReference(memberRef).Parent;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00036610 File Offset: 0x00034810
		public void GetMethodDefPropsOrThrow(MethodDefinitionHandle methodDef, out string name, out MethodImplAttributes implFlags, out MethodAttributes flags, out int rva)
		{
			MethodDefinition methodDefinition = this.MetadataReader.GetMethodDefinition(methodDef);
			name = this.MetadataReader.GetString(methodDefinition.Name);
			implFlags = methodDefinition.ImplAttributes;
			flags = methodDefinition.Attributes;
			rva = methodDefinition.RelativeVirtualAddress;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003665C File Offset: 0x0003485C
		internal void GetMethodImplPropsOrThrow(MethodImplementationHandle methodImpl, out EntityHandle body, out EntityHandle declaration)
		{
			global::System.Reflection.Metadata.MethodImplementation methodImplementation = this.MetadataReader.GetMethodImplementation(methodImpl);
			body = methodImplementation.MethodBody;
			declaration = methodImplementation.MethodDeclaration;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00036690 File Offset: 0x00034890
		internal GenericParameterHandleCollection GetGenericParametersForMethodOrThrow(MethodDefinitionHandle methodDef)
		{
			return this.MetadataReader.GetMethodDefinition(methodDef).GetGenericParameters();
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000366B4 File Offset: 0x000348B4
		internal ParameterHandleCollection GetParametersOfMethodOrThrow(MethodDefinitionHandle methodDef)
		{
			return this.MetadataReader.GetMethodDefinition(methodDef).GetParameters();
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000366D8 File Offset: 0x000348D8
		internal DllImportData GetDllImportData(MethodDefinitionHandle methodDef)
		{
			DllImportData dllImportData;
			try
			{
				MethodImport import = this.MetadataReader.GetMethodDefinition(methodDef).GetImport();
				if (import.Module.IsNil)
				{
					dllImportData = null;
				}
				else
				{
					string moduleRefNameOrThrow = this.GetModuleRefNameOrThrow(import.Module);
					string @string = this.MetadataReader.GetString(import.Name);
					MethodImportAttributes attributes = import.Attributes;
					dllImportData = new DllImportData(moduleRefNameOrThrow, @string, attributes);
				}
			}
			catch (BadImageFormatException)
			{
				dllImportData = null;
			}
			return dllImportData;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003675C File Offset: 0x0003495C
		public string GetMemberRefNameOrThrow(MemberReferenceHandle memberRef)
		{
			return PEModule.GetMemberRefNameOrThrow(this.MetadataReader, memberRef);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003676C File Offset: 0x0003496C
		private static string GetMemberRefNameOrThrow(MetadataReader metadataReader, MemberReferenceHandle memberRef)
		{
			return metadataReader.GetString(metadataReader.GetMemberReference(memberRef).Name);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003678E File Offset: 0x0003498E
		internal BlobHandle GetSignatureOrThrow(MemberReferenceHandle memberRef)
		{
			return PEModule.GetSignatureOrThrow(this.MetadataReader, memberRef);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003679C File Offset: 0x0003499C
		private static BlobHandle GetSignatureOrThrow(MetadataReader metadataReader, MemberReferenceHandle memberRef)
		{
			return metadataReader.GetMemberReference(memberRef).Signature;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000367B8 File Offset: 0x000349B8
		public void GetMemberRefPropsOrThrow(MemberReferenceHandle memberRef, out EntityHandle @class, out string name, out byte[] signature)
		{
			MemberReference memberReference = this.MetadataReader.GetMemberReference(memberRef);
			@class = memberReference.Parent;
			name = this.MetadataReader.GetString(memberReference.Name);
			signature = this.MetadataReader.GetBlobBytes(memberReference.Signature);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00036808 File Offset: 0x00034A08
		internal void GetParamPropsOrThrow(ParameterHandle parameterDef, out string name, out ParameterAttributes flags)
		{
			Parameter parameter = this.MetadataReader.GetParameter(parameterDef);
			name = this.MetadataReader.GetString(parameter.Name);
			flags = parameter.Attributes;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00036840 File Offset: 0x00034A40
		internal string GetParamNameOrThrow(ParameterHandle parameterDef)
		{
			Parameter parameter = this.MetadataReader.GetParameter(parameterDef);
			return this.MetadataReader.GetString(parameter.Name);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003686C File Offset: 0x00034A6C
		internal int GetParameterSequenceNumberOrThrow(ParameterHandle param)
		{
			return this.MetadataReader.GetParameter(param).SequenceNumber;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00036890 File Offset: 0x00034A90
		internal string GetPropertyDefNameOrThrow(PropertyDefinitionHandle propertyDef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetPropertyDefinition(propertyDef).Name);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000368BC File Offset: 0x00034ABC
		internal BlobHandle GetPropertySignatureOrThrow(PropertyDefinitionHandle propertyDef)
		{
			return this.MetadataReader.GetPropertyDefinition(propertyDef).Signature;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000368E0 File Offset: 0x00034AE0
		internal void GetPropertyDefPropsOrThrow(PropertyDefinitionHandle propertyDef, out string name, out PropertyAttributes flags)
		{
			PropertyDefinition propertyDefinition = this.MetadataReader.GetPropertyDefinition(propertyDef);
			name = this.MetadataReader.GetString(propertyDefinition.Name);
			flags = propertyDefinition.Attributes;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00036918 File Offset: 0x00034B18
		internal string GetEventDefNameOrThrow(EventDefinitionHandle eventDef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetEventDefinition(eventDef).Name);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00036944 File Offset: 0x00034B44
		internal void GetEventDefPropsOrThrow(EventDefinitionHandle eventDef, out string name, out EventAttributes flags, out EntityHandle type)
		{
			EventDefinition eventDefinition = this.MetadataReader.GetEventDefinition(eventDef);
			name = this.MetadataReader.GetString(eventDefinition.Name);
			flags = eventDefinition.Attributes;
			type = eventDefinition.Type;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003698C File Offset: 0x00034B8C
		public string GetFieldDefNameOrThrow(FieldDefinitionHandle fieldDef)
		{
			return this.MetadataReader.GetString(this.MetadataReader.GetFieldDefinition(fieldDef).Name);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000369B8 File Offset: 0x00034BB8
		internal BlobHandle GetFieldSignatureOrThrow(FieldDefinitionHandle fieldDef)
		{
			return this.MetadataReader.GetFieldDefinition(fieldDef).Signature;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000369DC File Offset: 0x00034BDC
		public FieldAttributes GetFieldDefFlagsOrThrow(FieldDefinitionHandle fieldDef)
		{
			return this.MetadataReader.GetFieldDefinition(fieldDef).Attributes;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00036A00 File Offset: 0x00034C00
		public void GetFieldDefPropsOrThrow(FieldDefinitionHandle fieldDef, out string name, out FieldAttributes flags)
		{
			FieldDefinition fieldDefinition = this.MetadataReader.GetFieldDefinition(fieldDef);
			name = this.MetadataReader.GetString(fieldDefinition.Name);
			flags = fieldDefinition.Attributes;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00036A38 File Offset: 0x00034C38
		internal ConstantValue GetParamDefaultValue(ParameterHandle param)
		{
			ConstantValue constantValue;
			try
			{
				ConstantHandle defaultValue = this.MetadataReader.GetParameter(param).GetDefaultValue();
				constantValue = (defaultValue.IsNil ? ConstantValue.Bad : this.GetConstantValueOrThrow(defaultValue));
			}
			catch (BadImageFormatException)
			{
				constantValue = ConstantValue.Bad;
			}
			return constantValue;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00036A90 File Offset: 0x00034C90
		internal ConstantValue GetConstantFieldValue(FieldDefinitionHandle fieldDef)
		{
			ConstantValue constantValue;
			try
			{
				ConstantHandle defaultValue = this.MetadataReader.GetFieldDefinition(fieldDef).GetDefaultValue();
				constantValue = (defaultValue.IsNil ? ConstantValue.Bad : this.GetConstantValueOrThrow(defaultValue));
			}
			catch (BadImageFormatException)
			{
				constantValue = ConstantValue.Bad;
			}
			return constantValue;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00036AE8 File Offset: 0x00034CE8
		public CustomAttributeHandleCollection GetCustomAttributesOrThrow(EntityHandle handle)
		{
			return this.MetadataReader.GetCustomAttributes(handle);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00036AF8 File Offset: 0x00034CF8
		public BlobHandle GetCustomAttributeValueOrThrow(CustomAttributeHandle handle)
		{
			return this.MetadataReader.GetCustomAttribute(handle).Value;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00036B1C File Offset: 0x00034D1C
		private BlobHandle GetMarshallingDescriptorHandleOrThrow(EntityHandle fieldOrParameterToken)
		{
			if (fieldOrParameterToken.Kind != HandleKind.FieldDefinition)
			{
				return this.MetadataReader.GetParameter((ParameterHandle)fieldOrParameterToken).GetMarshallingDescriptor();
			}
			return this.MetadataReader.GetFieldDefinition((FieldDefinitionHandle)fieldOrParameterToken).GetMarshallingDescriptor();
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00036B68 File Offset: 0x00034D68
		internal UnmanagedType GetMarshallingType(EntityHandle fieldOrParameterToken)
		{
			UnmanagedType unmanagedType;
			try
			{
				BlobHandle marshallingDescriptorHandleOrThrow = this.GetMarshallingDescriptorHandleOrThrow(fieldOrParameterToken);
				if (marshallingDescriptorHandleOrThrow.IsNil)
				{
					unmanagedType = 0;
				}
				else
				{
					byte b = this.MetadataReader.GetBlobReader(marshallingDescriptorHandleOrThrow).ReadByte();
					unmanagedType = ((b <= 80) ? b : 0);
				}
			}
			catch (BadImageFormatException)
			{
				unmanagedType = 0;
			}
			return unmanagedType;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00036BC4 File Offset: 0x00034DC4
		internal ImmutableArray<byte> GetMarshallingDescriptor(EntityHandle fieldOrParameterToken)
		{
			ImmutableArray<byte> immutableArray;
			try
			{
				BlobHandle marshallingDescriptorHandleOrThrow = this.GetMarshallingDescriptorHandleOrThrow(fieldOrParameterToken);
				if (marshallingDescriptorHandleOrThrow.IsNil)
				{
					immutableArray = ImmutableArray<byte>.Empty;
				}
				else
				{
					immutableArray = this.MetadataReader.GetBlobBytes(marshallingDescriptorHandleOrThrow).AsImmutableOrNull<byte>();
				}
			}
			catch (BadImageFormatException)
			{
				immutableArray = ImmutableArray<byte>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00036C18 File Offset: 0x00034E18
		internal int? GetFieldOffset(FieldDefinitionHandle fieldDef)
		{
			int? num;
			try
			{
				int offset = this.MetadataReader.GetFieldDefinition(fieldDef).GetOffset();
				if (offset == -1)
				{
					num = default(int?);
					num = num;
				}
				else
				{
					num = new int?(offset);
				}
			}
			catch (BadImageFormatException)
			{
				num = default(int?);
			}
			return num;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00036C74 File Offset: 0x00034E74
		private ConstantValue GetConstantValueOrThrow(ConstantHandle handle)
		{
			Constant constant = this.MetadataReader.GetConstant(handle);
			BlobReader blobReader = this.MetadataReader.GetBlobReader(constant.Value);
			switch (constant.TypeCode)
			{
			case ConstantTypeCode.Boolean:
				return ConstantValue.Create(blobReader.ReadBoolean());
			case ConstantTypeCode.Char:
				return ConstantValue.Create(blobReader.ReadChar());
			case ConstantTypeCode.SByte:
				return ConstantValue.Create(blobReader.ReadSByte());
			case ConstantTypeCode.Byte:
				return ConstantValue.Create(blobReader.ReadByte());
			case ConstantTypeCode.Int16:
				return ConstantValue.Create(blobReader.ReadInt16());
			case ConstantTypeCode.UInt16:
				return ConstantValue.Create(blobReader.ReadUInt16());
			case ConstantTypeCode.Int32:
				return ConstantValue.Create(blobReader.ReadInt32());
			case ConstantTypeCode.UInt32:
				return ConstantValue.Create(blobReader.ReadUInt32());
			case ConstantTypeCode.Int64:
				return ConstantValue.Create(blobReader.ReadInt64());
			case ConstantTypeCode.UInt64:
				return ConstantValue.Create(blobReader.ReadUInt64());
			case ConstantTypeCode.Single:
				return ConstantValue.Create(blobReader.ReadSingle());
			case ConstantTypeCode.Double:
				return ConstantValue.Create(blobReader.ReadDouble());
			case ConstantTypeCode.String:
				return ConstantValue.Create(blobReader.ReadUTF16(blobReader.Length));
			case ConstantTypeCode.NullReference:
				if (blobReader.ReadUInt32() == 0U)
				{
					return ConstantValue.Null;
				}
				break;
			}
			return ConstantValue.Bad;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00036DC0 File Offset: 0x00034FC0
		[return: TupleElementNames(new string[] { "FirstIndex", "SecondIndex" })]
		internal ValueTuple<int, int> GetAssemblyRefsForForwardedType(string fullName, bool ignoreCase, out string matchedName)
		{
			this.EnsureForwardTypeToAssemblyMap();
			if (ignoreCase)
			{
				using (Dictionary<string, ValueTuple<int, int>>.Enumerator enumerator = this._lazyForwardedTypesToAssemblyIndexMap.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, ValueTuple<int, int>> keyValuePair = enumerator.Current;
						if (string.Equals(keyValuePair.Key, fullName, 5))
						{
							matchedName = keyValuePair.Key;
							return keyValuePair.Value;
						}
					}
					goto IL_0070;
				}
			}
			ValueTuple<int, int> valueTuple;
			if (this._lazyForwardedTypesToAssemblyIndexMap.TryGetValue(fullName, ref valueTuple))
			{
				matchedName = fullName;
				return valueTuple;
			}
			IL_0070:
			matchedName = null;
			return new ValueTuple<int, int>(-1, -1);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00036E5C File Offset: 0x0003505C
		[return: TupleElementNames(new string[] { "FirstIndex", "SecondIndex" })]
		internal IEnumerable<KeyValuePair<string, ValueTuple<int, int>>> GetForwardedTypes()
		{
			this.EnsureForwardTypeToAssemblyMap();
			return this._lazyForwardedTypesToAssemblyIndexMap;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00036E6C File Offset: 0x0003506C
		private void EnsureForwardTypeToAssemblyMap()
		{
			if (this._lazyForwardedTypesToAssemblyIndexMap == null)
			{
				Dictionary<string, ValueTuple<int, int>> dictionary = new Dictionary<string, ValueTuple<int, int>>();
				try
				{
					foreach (ExportedTypeHandle exportedTypeHandle in this.MetadataReader.ExportedTypes)
					{
						global::System.Reflection.Metadata.ExportedType exportedType = this.MetadataReader.GetExportedType(exportedTypeHandle);
						if (exportedType.IsForwarder)
						{
							AssemblyReferenceHandle assemblyReferenceHandle = (AssemblyReferenceHandle)exportedType.Implementation;
							if (!assemblyReferenceHandle.IsNil)
							{
								int assemblyReferenceIndexOrThrow;
								try
								{
									assemblyReferenceIndexOrThrow = this.GetAssemblyReferenceIndexOrThrow(assemblyReferenceHandle);
								}
								catch (BadImageFormatException)
								{
									continue;
								}
								if (assemblyReferenceIndexOrThrow >= 0 && assemblyReferenceIndexOrThrow < this.ReferencedAssemblies.Length)
								{
									string text = this.MetadataReader.GetString(exportedType.Name);
									StringHandle @namespace = exportedType.Namespace;
									if (!@namespace.IsNil)
									{
										string @string = this.MetadataReader.GetString(@namespace);
										if (@string.Length > 0)
										{
											text = @string + "." + text;
										}
									}
									ValueTuple<int, int> valueTuple;
									if (dictionary.TryGetValue(text, ref valueTuple))
									{
										if (valueTuple.Item1 != assemblyReferenceIndexOrThrow && valueTuple.Item2 < 0)
										{
											valueTuple.Item2 = assemblyReferenceIndexOrThrow;
											dictionary[text] = valueTuple;
										}
									}
									else
									{
										dictionary.Add(text, new ValueTuple<int, int>(assemblyReferenceIndexOrThrow, -1));
									}
								}
							}
						}
					}
				}
				catch (BadImageFormatException)
				{
				}
				this._lazyForwardedTypesToAssemblyIndexMap = dictionary;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0003701C File Offset: 0x0003521C
		internal IdentifierCollection TypeNames
		{
			get
			{
				return this._lazyTypeNameCollection.Value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00037029 File Offset: 0x00035229
		internal IdentifierCollection NamespaceNames
		{
			get
			{
				return this._lazyNamespaceNameCollection.Value;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00037038 File Offset: 0x00035238
		internal PropertyAccessors GetPropertyMethodsOrThrow(PropertyDefinitionHandle propertyDef)
		{
			return this.MetadataReader.GetPropertyDefinition(propertyDef).GetAccessors();
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003705C File Offset: 0x0003525C
		internal EventAccessors GetEventMethodsOrThrow(EventDefinitionHandle eventDef)
		{
			return this.MetadataReader.GetEventDefinition(eventDef).GetAccessors();
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003707D File Offset: 0x0003527D
		internal int GetAssemblyReferenceIndexOrThrow(AssemblyReferenceHandle assemblyRef)
		{
			return this.MetadataReader.GetRowNumber(assemblyRef) - 1;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00037092 File Offset: 0x00035292
		internal static bool IsNested(TypeAttributes flags)
		{
			return (flags & 6) > 0;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0003709A File Offset: 0x0003529A
		internal bool HasIL
		{
			get
			{
				return this.IsEntireImageAvailable;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x000370A2 File Offset: 0x000352A2
		internal bool IsEntireImageAvailable
		{
			get
			{
				return this._peReaderOpt != null && this._peReaderOpt.IsEntireImageAvailable;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000370BC File Offset: 0x000352BC
		internal MethodBodyBlock GetMethodBodyOrThrow(MethodDefinitionHandle methodHandle)
		{
			MethodDefinition methodDefinition = this.MetadataReader.GetMethodDefinition(methodHandle);
			if ((methodDefinition.ImplAttributes & 3) != null || methodDefinition.RelativeVirtualAddress == 0)
			{
				return null;
			}
			return this._peReaderOpt.GetMethodBody(methodDefinition.RelativeVirtualAddress);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00037100 File Offset: 0x00035300
		private static bool StringEquals(MetadataReader metadataReader, StringHandle nameHandle, string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				return string.Equals(metadataReader.GetString(nameHandle), name, 5);
			}
			return metadataReader.StringComparer.Equals(nameHandle, name);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003712F File Offset: 0x0003532F
		public ModuleMetadata GetNonDisposableMetadata()
		{
			return this._owner.Copy();
		}

		// Token: 0x0400054A RID: 1354
		private readonly ModuleMetadata _owner;

		// Token: 0x0400054B RID: 1355
		private readonly PEReader _peReaderOpt;

		// Token: 0x0400054C RID: 1356
		private readonly IntPtr _metadataPointerOpt;

		// Token: 0x0400054D RID: 1357
		private readonly int _metadataSizeOpt;

		// Token: 0x0400054E RID: 1358
		private MetadataReader _lazyMetadataReader;

		// Token: 0x0400054F RID: 1359
		private ImmutableArray<AssemblyIdentity> _lazyAssemblyReferences;

		// Token: 0x04000550 RID: 1360
		[TupleElementNames(new string[] { "FirstIndex", "SecondIndex" })]
		private Dictionary<string, ValueTuple<int, int>> _lazyForwardedTypesToAssemblyIndexMap;

		// Token: 0x04000551 RID: 1361
		private readonly Lazy<IdentifierCollection> _lazyTypeNameCollection;

		// Token: 0x04000552 RID: 1362
		private readonly Lazy<IdentifierCollection> _lazyNamespaceNameCollection;

		// Token: 0x04000553 RID: 1363
		private string _lazyName;

		// Token: 0x04000554 RID: 1364
		private bool _isDisposed;

		// Token: 0x04000555 RID: 1365
		private ThreeState _lazyContainsNoPiaLocalTypes;

		// Token: 0x04000556 RID: 1366
		private int[] _lazyNoPiaLocalTypeCheckBitMap;

		// Token: 0x04000557 RID: 1367
		private ConcurrentDictionary<TypeDefinitionHandle, PEModule.AttributeInfo> _lazyTypeDefToTypeIdentifierMap;

		// Token: 0x04000558 RID: 1368
		private readonly CryptographicHashProvider _hashesOpt;

		// Token: 0x04000559 RID: 1369
		private static readonly PEModule.AttributeValueExtractor<string> s_attributeStringValueExtractor = new PEModule.AttributeValueExtractor<string>(PEModule.CrackStringInAttributeValue);

		// Token: 0x0400055A RID: 1370
		private static readonly PEModule.AttributeValueExtractor<PEModule.StringAndInt> s_attributeStringAndIntValueExtractor = new PEModule.AttributeValueExtractor<PEModule.StringAndInt>(PEModule.CrackStringAndIntInAttributeValue);

		// Token: 0x0400055B RID: 1371
		private static readonly PEModule.AttributeValueExtractor<bool> s_attributeBooleanValueExtractor = new PEModule.AttributeValueExtractor<bool>(PEModule.CrackBooleanInAttributeValue);

		// Token: 0x0400055C RID: 1372
		private static readonly PEModule.AttributeValueExtractor<byte> s_attributeByteValueExtractor = new PEModule.AttributeValueExtractor<byte>(PEModule.CrackByteInAttributeValue);

		// Token: 0x0400055D RID: 1373
		private static readonly PEModule.AttributeValueExtractor<short> s_attributeShortValueExtractor = new PEModule.AttributeValueExtractor<short>(PEModule.CrackShortInAttributeValue);

		// Token: 0x0400055E RID: 1374
		private static readonly PEModule.AttributeValueExtractor<int> s_attributeIntValueExtractor = new PEModule.AttributeValueExtractor<int>(PEModule.CrackIntInAttributeValue);

		// Token: 0x0400055F RID: 1375
		private static readonly PEModule.AttributeValueExtractor<long> s_attributeLongValueExtractor = new PEModule.AttributeValueExtractor<long>(PEModule.CrackLongInAttributeValue);

		// Token: 0x04000560 RID: 1376
		private static readonly PEModule.AttributeValueExtractor<decimal> s_decimalValueInDecimalConstantAttributeExtractor = new PEModule.AttributeValueExtractor<decimal>(PEModule.CrackDecimalInDecimalConstantAttribute);

		// Token: 0x04000561 RID: 1377
		private static readonly PEModule.AttributeValueExtractor<ImmutableArray<bool>> s_attributeBoolArrayValueExtractor = new PEModule.AttributeValueExtractor<ImmutableArray<bool>>(PEModule.CrackBoolArrayInAttributeValue);

		// Token: 0x04000562 RID: 1378
		private static readonly PEModule.AttributeValueExtractor<ImmutableArray<byte>> s_attributeByteArrayValueExtractor = new PEModule.AttributeValueExtractor<ImmutableArray<byte>>(PEModule.CrackByteArrayInAttributeValue);

		// Token: 0x04000563 RID: 1379
		private static readonly PEModule.AttributeValueExtractor<ImmutableArray<string>> s_attributeStringArrayValueExtractor = new PEModule.AttributeValueExtractor<ImmutableArray<string>>(PEModule.CrackStringArrayInAttributeValue);

		// Token: 0x04000564 RID: 1380
		private static readonly PEModule.AttributeValueExtractor<ObsoleteAttributeData> s_attributeDeprecatedDataExtractor = new PEModule.AttributeValueExtractor<ObsoleteAttributeData>(PEModule.CrackDeprecatedAttributeData);

		// Token: 0x04000565 RID: 1381
		private static readonly PEModule.AttributeValueExtractor<PEModule.BoolAndStringArrayData> s_attributeBoolAndStringArrayValueExtractor = new PEModule.AttributeValueExtractor<PEModule.BoolAndStringArrayData>(PEModule.CrackBoolAndStringArrayInAttributeValue);

		// Token: 0x04000566 RID: 1382
		private static readonly PEModule.AttributeValueExtractor<PEModule.BoolAndStringData> s_attributeBoolAndStringValueExtractor = new PEModule.AttributeValueExtractor<PEModule.BoolAndStringData>(PEModule.CrackBoolAndStringInAttributeValue);

		// Token: 0x04000567 RID: 1383
		private static readonly ImmutableArray<bool> s_simpleTransformFlags = ImmutableArray.Create<bool>(true);

		// Token: 0x04000568 RID: 1384
		internal const string ByRefLikeMarker = "Types with embedded references are not supported in this version of your compiler.";

		// Token: 0x020005B8 RID: 1464
		// (Invoke) Token: 0x0600361F RID: 13855
		private delegate bool AttributeValueExtractor<T>(out T value, ref BlobReader sigReader);

		// Token: 0x020005B9 RID: 1465
		internal struct BoolAndStringArrayData
		{
			// Token: 0x06003622 RID: 13858 RVA: 0x00098B8C File Offset: 0x00096D8C
			public BoolAndStringArrayData(bool sense, ImmutableArray<string> strings)
			{
				this.Sense = sense;
				this.Strings = strings;
			}

			// Token: 0x04001679 RID: 5753
			public readonly bool Sense;

			// Token: 0x0400167A RID: 5754
			public readonly ImmutableArray<string> Strings;
		}

		// Token: 0x020005BA RID: 1466
		internal struct BoolAndStringData
		{
			// Token: 0x06003623 RID: 13859 RVA: 0x00098B9C File Offset: 0x00096D9C
			public BoolAndStringData(bool sense, string @string)
			{
				this.Sense = sense;
				this.String = @string;
			}

			// Token: 0x0400167B RID: 5755
			public readonly bool Sense;

			// Token: 0x0400167C RID: 5756
			public readonly string String;
		}

		// Token: 0x020005BB RID: 1467
		private sealed class PEHashProvider : CryptographicHashProvider
		{
			// Token: 0x06003624 RID: 13860 RVA: 0x00098BAC File Offset: 0x00096DAC
			public PEHashProvider(PEReader peReader)
			{
				this._peReader = peReader;
			}

			// Token: 0x06003625 RID: 13861 RVA: 0x00098BBC File Offset: 0x00096DBC
			internal unsafe override ImmutableArray<byte> ComputeHash(HashAlgorithm algorithm)
			{
				PEMemoryBlock entireImage = this._peReader.GetEntireImage();
				byte[] array;
				using (ReadOnlyUnmanagedMemoryStream readOnlyUnmanagedMemoryStream = new ReadOnlyUnmanagedMemoryStream(this._peReader, (IntPtr)((void*)entireImage.Pointer), entireImage.Length))
				{
					array = algorithm.ComputeHash(readOnlyUnmanagedMemoryStream);
				}
				return ImmutableArray.Create<byte>(array);
			}

			// Token: 0x0400167D RID: 5757
			private readonly PEReader _peReader;
		}

		// Token: 0x020005BC RID: 1468
		private struct TypeDefToNamespace
		{
			// Token: 0x06003626 RID: 13862 RVA: 0x00098C20 File Offset: 0x00096E20
			internal TypeDefToNamespace(TypeDefinitionHandle typeDef, NamespaceDefinitionHandle namespaceHandle)
			{
				this.TypeDef = typeDef;
				this.NamespaceHandle = namespaceHandle;
			}

			// Token: 0x0400167E RID: 5758
			internal readonly TypeDefinitionHandle TypeDef;

			// Token: 0x0400167F RID: 5759
			internal readonly NamespaceDefinitionHandle NamespaceHandle;
		}

		// Token: 0x020005BD RID: 1469
		internal class TypesByNamespaceSortComparer : IComparer<IGrouping<string, TypeDefinitionHandle>>
		{
			// Token: 0x06003627 RID: 13863 RVA: 0x00098C30 File Offset: 0x00096E30
			public TypesByNamespaceSortComparer(StringComparer nameComparer)
			{
				this._nameComparer = nameComparer;
			}

			// Token: 0x06003628 RID: 13864 RVA: 0x00098C40 File Offset: 0x00096E40
			public int Compare(IGrouping<string, TypeDefinitionHandle> left, IGrouping<string, TypeDefinitionHandle> right)
			{
				if (left == right)
				{
					return 0;
				}
				int num = this._nameComparer.Compare(left.Key, right.Key);
				if (num == 0)
				{
					TypeDefinitionHandle typeDefinitionHandle = Enumerable.FirstOrDefault<TypeDefinitionHandle>(left);
					TypeDefinitionHandle typeDefinitionHandle2 = Enumerable.FirstOrDefault<TypeDefinitionHandle>(right);
					if (typeDefinitionHandle.IsNil ^ typeDefinitionHandle2.IsNil)
					{
						num = (typeDefinitionHandle.IsNil ? 1 : (-1));
					}
					else
					{
						num = HandleComparer.Default.Compare(typeDefinitionHandle, typeDefinitionHandle2);
					}
					if (num == 0)
					{
						num = string.CompareOrdinal(left.Key, right.Key);
					}
				}
				return num;
			}

			// Token: 0x04001680 RID: 5760
			private readonly StringComparer _nameComparer;
		}

		// Token: 0x020005BE RID: 1470
		private class NamespaceHandleEqualityComparer : IEqualityComparer<NamespaceDefinitionHandle>
		{
			// Token: 0x06003629 RID: 13865 RVA: 0x00098CCA File Offset: 0x00096ECA
			private NamespaceHandleEqualityComparer()
			{
			}

			// Token: 0x0600362A RID: 13866 RVA: 0x00098CD2 File Offset: 0x00096ED2
			public bool Equals(NamespaceDefinitionHandle x, NamespaceDefinitionHandle y)
			{
				return x == y;
			}

			// Token: 0x0600362B RID: 13867 RVA: 0x00098CDB File Offset: 0x00096EDB
			public int GetHashCode(NamespaceDefinitionHandle obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x04001681 RID: 5761
			public static readonly PEModule.NamespaceHandleEqualityComparer Singleton = new PEModule.NamespaceHandleEqualityComparer();
		}

		// Token: 0x020005BF RID: 1471
		private struct StringAndInt
		{
			// Token: 0x04001682 RID: 5762
			public string StringValue;

			// Token: 0x04001683 RID: 5763
			public int IntValue;
		}

		// Token: 0x020005C0 RID: 1472
		internal struct AttributeInfo
		{
			// Token: 0x0600362D RID: 13869 RVA: 0x00098CF6 File Offset: 0x00096EF6
			public AttributeInfo(CustomAttributeHandle handle, int signatureIndex)
			{
				this.Handle = handle;
				this.SignatureIndex = (byte)signatureIndex;
			}

			// Token: 0x17000F4D RID: 3917
			// (get) Token: 0x0600362E RID: 13870 RVA: 0x00098D07 File Offset: 0x00096F07
			public bool HasValue
			{
				get
				{
					return !this.Handle.IsNil;
				}
			}

			// Token: 0x04001684 RID: 5764
			public readonly CustomAttributeHandle Handle;

			// Token: 0x04001685 RID: 5765
			public readonly byte SignatureIndex;
		}

		// Token: 0x020005C1 RID: 1473
		private sealed class StringTableDecoder : MetadataStringDecoder
		{
			// Token: 0x0600362F RID: 13871 RVA: 0x00098D17 File Offset: 0x00096F17
			private StringTableDecoder()
				: base(Encoding.UTF8)
			{
			}

			// Token: 0x06003630 RID: 13872 RVA: 0x00098D24 File Offset: 0x00096F24
			public unsafe override string GetString(byte* bytes, int byteCount)
			{
				return StringTable.AddSharedUTF8(new ReadOnlySpan<byte>((void*)bytes, byteCount));
			}

			// Token: 0x04001686 RID: 5766
			public static readonly PEModule.StringTableDecoder Instance = new PEModule.StringTableDecoder();
		}
	}
}
