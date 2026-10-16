using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005ED RID: 1517
	internal abstract class PENamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x06007F46 RID: 32582 RVA: 0x001CA141 File Offset: 0x001C8341
		internal sealed override NamespaceExtent Extent
		{
			get
			{
				return new NamespaceExtent(this.ContainingPEModule);
			}
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x001CA150 File Offset: 0x001C8350
		public sealed override ImmutableArray<Symbol> GetMembers()
		{
			this.EnsureAllMembersLoaded();
			ImmutableArray<NamedTypeSymbol> memberTypesPrivate = this.GetMemberTypesPrivate();
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(memberTypesPrivate.Length + this.lazyNamespaces.Count);
			instance.AddRange<NamedTypeSymbol>(memberTypesPrivate);
			foreach (KeyValuePair<string, PENestedNamespaceSymbol> keyValuePair in this.lazyNamespaces)
			{
				instance.Add(keyValuePair.Value);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x001CA1DC File Offset: 0x001C83DC
		private ImmutableArray<NamedTypeSymbol> GetMemberTypesPrivate()
		{
			if (this._lazyFlattenedTypes.IsDefault)
			{
				ImmutableArray<PENamedTypeSymbol> immutableArray = this.lazyTypes.Flatten<string, PENamedTypeSymbol>(null);
				ImmutableInterlocked.InterlockedExchange<PENamedTypeSymbol>(ref this._lazyFlattenedTypes, immutableArray);
			}
			return StaticCast<NamedTypeSymbol>.From<PENamedTypeSymbol>(this._lazyFlattenedTypes);
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x001CA21C File Offset: 0x001C841C
		public sealed override ImmutableArray<Symbol> GetMembers(string name)
		{
			this.EnsureAllMembersLoaded();
			PENestedNamespaceSymbol penestedNamespaceSymbol = null;
			if (this.lazyNamespaces.TryGetValue(name, ref penestedNamespaceSymbol))
			{
				ImmutableArray<PENamedTypeSymbol> immutableArray;
				if (this.lazyTypes.TryGetValue(name, ref immutableArray))
				{
					return StaticCast<Symbol>.From<PENamedTypeSymbol>(immutableArray).Add(penestedNamespaceSymbol);
				}
				return ImmutableArray.Create<Symbol>(penestedNamespaceSymbol);
			}
			else
			{
				ImmutableArray<PENamedTypeSymbol> immutableArray;
				if (this.lazyTypes.TryGetValue(name, ref immutableArray))
				{
					return StaticCast<Symbol>.From<PENamedTypeSymbol>(immutableArray);
				}
				return ImmutableArray<Symbol>.Empty;
			}
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x001CA284 File Offset: 0x001C8484
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			this.EnsureAllMembersLoaded();
			return this.GetMemberTypesPrivate();
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x001CA294 File Offset: 0x001C8494
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			this.EnsureAllMembersLoaded();
			ImmutableArray<PENamedTypeSymbol> immutableArray;
			if (!this.lazyTypes.TryGetValue(name, ref immutableArray))
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			return StaticCast<NamedTypeSymbol>.From<PENamedTypeSymbol>(immutableArray);
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x001CA2C3 File Offset: 0x001C84C3
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.GetTypeMembers(name).WhereAsArray((NamedTypeSymbol type, int arity) => type.Arity == arity, arity);
		}

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x06007F4D RID: 32589 RVA: 0x001CA2F1 File Offset: 0x001C84F1
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return this.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x06007F4E RID: 32590 RVA: 0x001CA303 File Offset: 0x001C8503
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x06007F4F RID: 32591
		internal abstract PEModuleSymbol ContainingPEModule { get; }

		// Token: 0x06007F50 RID: 32592
		protected abstract void EnsureAllMembersLoaded();

		// Token: 0x06007F51 RID: 32593 RVA: 0x001CA30C File Offset: 0x001C850C
		protected void LoadAllMembers(IEnumerable<IGrouping<string, TypeDefinitionHandle>> typesByNS)
		{
			IEnumerable<IGrouping<string, TypeDefinitionHandle>> enumerable = null;
			IEnumerable<KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>> enumerable2 = null;
			bool isGlobalNamespace = this.IsGlobalNamespace;
			MetadataHelpers.GetInfoForImmediateNamespaceMembers(isGlobalNamespace, isGlobalNamespace ? 0 : this.GetQualifiedNameLength(), typesByNS, StringComparer.Ordinal, out enumerable, out enumerable2);
			this.LazyInitializeNamespaces(enumerable2);
			this.LazyInitializeTypes(enumerable);
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x001CA34C File Offset: 0x001C854C
		private int GetQualifiedNameLength()
		{
			int num = this.Name.Length;
			NamespaceSymbol namespaceSymbol = this.ContainingNamespace;
			while (namespaceSymbol != null && !namespaceSymbol.IsGlobalNamespace)
			{
				num += namespaceSymbol.Name.Length + 1;
				namespaceSymbol = namespaceSymbol.ContainingNamespace;
			}
			return num;
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x001CA394 File Offset: 0x001C8594
		private void LazyInitializeNamespaces(IEnumerable<KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>>> childNamespaces)
		{
			if (this.lazyNamespaces == null)
			{
				Dictionary<string, PENestedNamespaceSymbol> dictionary = new Dictionary<string, PENestedNamespaceSymbol>(StringOrdinalComparer.Instance);
				foreach (KeyValuePair<string, IEnumerable<IGrouping<string, TypeDefinitionHandle>>> keyValuePair in childNamespaces)
				{
					PENestedNamespaceSymbol penestedNamespaceSymbol = new PENestedNamespaceSymbol(keyValuePair.Key, this, keyValuePair.Value);
					dictionary.Add(penestedNamespaceSymbol.Name, penestedNamespaceSymbol);
				}
				Interlocked.CompareExchange<Dictionary<string, PENestedNamespaceSymbol>>(ref this.lazyNamespaces, dictionary, null);
			}
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x001CA418 File Offset: 0x001C8618
		private void LazyInitializeTypes(IEnumerable<IGrouping<string, TypeDefinitionHandle>> typeGroups)
		{
			if (this.lazyTypes == null)
			{
				PEModuleSymbol containingPEModule = this.ContainingPEModule;
				ArrayBuilder<PENamedTypeSymbol> instance = ArrayBuilder<PENamedTypeSymbol>.GetInstance();
				bool flag = !containingPEModule.Module.ContainsNoPiaLocalTypes();
				Dictionary<string, TypeDefinitionHandle> dictionary = null;
				foreach (IGrouping<string, TypeDefinitionHandle> grouping in typeGroups)
				{
					foreach (TypeDefinitionHandle typeDefinitionHandle in grouping)
					{
						if (flag || !containingPEModule.Module.IsNoPiaLocalType(typeDefinitionHandle))
						{
							instance.Add(PENamedTypeSymbol.Create(containingPEModule, this, typeDefinitionHandle, grouping.Key));
						}
						else
						{
							try
							{
								string typeDefNameOrThrow = containingPEModule.Module.GetTypeDefNameOrThrow(typeDefinitionHandle);
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, TypeDefinitionHandle>(StringOrdinalComparer.Instance);
								}
								dictionary[typeDefNameOrThrow] = typeDefinitionHandle;
							}
							catch (BadImageFormatException)
							{
							}
						}
					}
				}
				Dictionary<string, ImmutableArray<PENamedTypeSymbol>> dictionary2 = instance.ToDictionary<string>((PENamedTypeSymbol c) => c.Name, StringOrdinalComparer.Instance);
				instance.Free();
				if (dictionary != null)
				{
					Interlocked.CompareExchange<Dictionary<string, TypeDefinitionHandle>>(ref this._lazyNoPiaLocalTypes, dictionary, null);
				}
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<PENamedTypeSymbol>>>(ref this.lazyTypes, dictionary2, null) == null)
				{
					containingPEModule.OnNewTypeDeclarationsLoaded(dictionary2);
				}
			}
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x001CA584 File Offset: 0x001C8784
		internal NamedTypeSymbol LookupMetadataType(ref MetadataTypeName emittedTypeName, out bool isNoPiaLocalType)
		{
			NamedTypeSymbol namedTypeSymbol = this.LookupMetadataType(ref emittedTypeName);
			isNoPiaLocalType = false;
			if (namedTypeSymbol is MissingMetadataTypeSymbol)
			{
				this.EnsureAllMembersLoaded();
				TypeDefinitionHandle typeDefinitionHandle;
				if (this._lazyNoPiaLocalTypes != null && this._lazyNoPiaLocalTypes.TryGetValue(emittedTypeName.TypeName, ref typeDefinitionHandle))
				{
					namedTypeSymbol = (NamedTypeSymbol)new MetadataDecoder(this.ContainingPEModule).GetTypeOfToken(typeDefinitionHandle, out isNoPiaLocalType);
				}
			}
			return namedTypeSymbol;
		}

		// Token: 0x04001DFC RID: 7676
		protected Dictionary<string, PENestedNamespaceSymbol> lazyNamespaces;

		// Token: 0x04001DFD RID: 7677
		protected Dictionary<string, ImmutableArray<PENamedTypeSymbol>> lazyTypes;

		// Token: 0x04001DFE RID: 7678
		private Dictionary<string, TypeDefinitionHandle> _lazyNoPiaLocalTypes;

		// Token: 0x04001DFF RID: 7679
		private ImmutableArray<PENamedTypeSymbol> _lazyFlattenedTypes;
	}
}
