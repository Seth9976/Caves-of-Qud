using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class MergedNamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x06006B4C RID: 27468 RVA: 0x0018E794 File Offset: 0x0018C994
		internal static NamespaceSymbol Create(NamespaceExtent extent, NamespaceSymbol containingNamespace, ImmutableArray<NamespaceSymbol> namespacesToMerge, string nameOpt = null)
		{
			if (namespacesToMerge.Length != 1 || nameOpt != null)
			{
				return new MergedNamespaceSymbol(extent, containingNamespace, namespacesToMerge, nameOpt);
			}
			return namespacesToMerge[0];
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x0018E7B8 File Offset: 0x0018C9B8
		private MergedNamespaceSymbol(NamespaceExtent extent, NamespaceSymbol containingNamespace, ImmutableArray<NamespaceSymbol> namespacesToMerge, string nameOpt)
		{
			this._extent = extent;
			this._namespacesToMerge = namespacesToMerge;
			this._containingNamespace = containingNamespace;
			this._cachedLookup = new CachingDictionary<string, Symbol>(new Func<string, ImmutableArray<Symbol>>(this.SlowGetChildrenOfName), new Func<IEqualityComparer<string>, HashSet<string>>(this.SlowGetChildNames), EqualityComparer<string>.Default);
			this._nameOpt = nameOpt;
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x0018E810 File Offset: 0x0018CA10
		internal NamespaceSymbol GetConstituentForCompilation(CSharpCompilation compilation)
		{
			foreach (NamespaceSymbol namespaceSymbol in this._namespacesToMerge)
			{
				if (namespaceSymbol.IsFromCompilation(compilation))
				{
					return namespaceSymbol;
				}
			}
			return null;
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x0018E84C File Offset: 0x0018CA4C
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			foreach (Symbol symbol in this._namespacesToMerge)
			{
				cancellationToken.ThrowIfCancellationRequested();
				symbol.ForceComplete(locationOpt, cancellationToken);
			}
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x0018E888 File Offset: 0x0018CA88
		private ImmutableArray<Symbol> SlowGetChildrenOfName(string name)
		{
			ArrayBuilder<NamespaceSymbol> arrayBuilder = null;
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			foreach (NamespaceSymbol namespaceSymbol in this._namespacesToMerge)
			{
				foreach (Symbol symbol in namespaceSymbol.GetMembers(name))
				{
					if (symbol.Kind == SymbolKind.Namespace)
					{
						arrayBuilder = arrayBuilder ?? ArrayBuilder<NamespaceSymbol>.GetInstance();
						arrayBuilder.Add((NamespaceSymbol)symbol);
					}
					else
					{
						instance.Add(symbol);
					}
				}
			}
			if (arrayBuilder != null)
			{
				instance.Add(MergedNamespaceSymbol.Create(this._extent, this, arrayBuilder.ToImmutableAndFree(), null));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x0018E934 File Offset: 0x0018CB34
		private HashSet<string> SlowGetChildNames(IEqualityComparer<string> comparer)
		{
			HashSet<string> hashSet = new HashSet<string>(comparer);
			foreach (NamespaceSymbol namespaceSymbol in this._namespacesToMerge)
			{
				foreach (Symbol symbol in namespaceSymbol.GetMembersUnordered())
				{
					hashSet.Add(symbol.Name);
				}
			}
			return hashSet;
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06006B52 RID: 27474 RVA: 0x0018E99C File Offset: 0x0018CB9C
		public override string Name
		{
			get
			{
				return this._nameOpt ?? this._namespacesToMerge[0].Name;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06006B53 RID: 27475 RVA: 0x0018E9C7 File Offset: 0x0018CBC7
		internal override NamespaceExtent Extent
		{
			get
			{
				return this._extent;
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06006B54 RID: 27476 RVA: 0x0018E9CF File Offset: 0x0018CBCF
		public override ImmutableArray<NamespaceSymbol> ConstituentNamespaces
		{
			get
			{
				return this._namespacesToMerge;
			}
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x0018E9D8 File Offset: 0x0018CBD8
		public override ImmutableArray<Symbol> GetMembers()
		{
			if (this._allMembers.IsDefault)
			{
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
				this._cachedLookup.AddValues(instance);
				this._allMembers = instance.ToImmutableAndFree();
			}
			return this._allMembers;
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x0018EA16 File Offset: 0x0018CC16
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return this._cachedLookup[name];
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x0018EA24 File Offset: 0x0018CC24
		internal sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return ImmutableArray.CreateRange<NamedTypeSymbol>(this.GetMembersUnordered().OfType<NamedTypeSymbol>());
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x0018EA44 File Offset: 0x0018CC44
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray.CreateRange<NamedTypeSymbol>(this.GetMembers().OfType<NamedTypeSymbol>());
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x0018EA64 File Offset: 0x0018CC64
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray.CreateRange<NamedTypeSymbol>(this._cachedLookup[name].OfType<NamedTypeSymbol>());
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06006B5A RID: 27482 RVA: 0x0018EA8A File Offset: 0x0018CC8A
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingNamespace;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x06006B5B RID: 27483 RVA: 0x0018EA94 File Offset: 0x0018CC94
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				if (this._extent.Kind == NamespaceKind.Module)
				{
					return this._extent.Module.ContainingAssembly;
				}
				if (this._extent.Kind == NamespaceKind.Assembly)
				{
					return this._extent.Assembly;
				}
				return null;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06006B5C RID: 27484 RVA: 0x0018EAE7 File Offset: 0x0018CCE7
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return Enumerable.SelectMany<NamespaceSymbol, Location>(this._namespacesToMerge, (NamespaceSymbol namespaceSymbol) => namespaceSymbol.Locations).AsImmutable<Location>();
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x0018EB1D File Offset: 0x0018CD1D
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return Enumerable.SelectMany<NamespaceSymbol, SyntaxReference>(this._namespacesToMerge, (NamespaceSymbol namespaceSymbol) => namespaceSymbol.DeclaringSyntaxReferences).AsImmutable<SyntaxReference>();
			}
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x0018EB54 File Offset: 0x0018CD54
		internal override void GetExtensionMethods(ArrayBuilder<MethodSymbol> methods, string name, int arity, LookupOptions options)
		{
			foreach (NamespaceSymbol namespaceSymbol in this._namespacesToMerge)
			{
				namespaceSymbol.GetExtensionMethods(methods, name, arity, options);
			}
		}

		// Token: 0x04001AA2 RID: 6818
		private readonly NamespaceExtent _extent;

		// Token: 0x04001AA3 RID: 6819
		private readonly ImmutableArray<NamespaceSymbol> _namespacesToMerge;

		// Token: 0x04001AA4 RID: 6820
		private readonly NamespaceSymbol _containingNamespace;

		// Token: 0x04001AA5 RID: 6821
		private readonly string _nameOpt;

		// Token: 0x04001AA6 RID: 6822
		private readonly CachingDictionary<string, Symbol> _cachedLookup;

		// Token: 0x04001AA7 RID: 6823
		private ImmutableArray<Symbol> _allMembers;
	}
}
