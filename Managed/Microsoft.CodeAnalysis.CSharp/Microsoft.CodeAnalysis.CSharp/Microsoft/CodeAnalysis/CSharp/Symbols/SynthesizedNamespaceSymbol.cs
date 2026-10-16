using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056A RID: 1386
	internal sealed class SynthesizedNamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x060074DD RID: 29917 RVA: 0x001B3E33 File Offset: 0x001B2033
		public SynthesizedNamespaceSymbol(NamespaceSymbol containingNamespace, string name)
		{
			this._containingSymbol = containingNamespace;
			this._name = name;
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x001B3E49 File Offset: 0x001B2049
		public override int GetHashCode()
		{
			return Hash.Combine(this._containingSymbol.GetHashCode(), this._name.GetHashCode());
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x001B3E68 File Offset: 0x001B2068
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			SynthesizedNamespaceSymbol synthesizedNamespaceSymbol = obj as SynthesizedNamespaceSymbol;
			return synthesizedNamespaceSymbol != null && this.Equals(synthesizedNamespaceSymbol, compareKind);
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x001B3E89 File Offset: 0x001B2089
		public bool Equals(SynthesizedNamespaceSymbol other, TypeCompareKind compareKind)
		{
			return this == other || (other != null && this._name.Equals(other._name) && this._containingSymbol.Equals(other._containingSymbol));
		}

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x060074E1 RID: 29921 RVA: 0x001B3EBA File Offset: 0x001B20BA
		internal override NamespaceExtent Extent
		{
			get
			{
				return this._containingSymbol.Extent;
			}
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x060074E2 RID: 29922 RVA: 0x001B3EC7 File Offset: 0x001B20C7
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x060074E3 RID: 29923 RVA: 0x001B3ECF File Offset: 0x001B20CF
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x060074E4 RID: 29924 RVA: 0x001B3ED7 File Offset: 0x001B20D7
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._containingSymbol.ContainingAssembly;
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x001B3EE4 File Offset: 0x001B20E4
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x060074E6 RID: 29926 RVA: 0x001B3EEB File Offset: 0x001B20EB
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x060074E7 RID: 29927 RVA: 0x001B3EF2 File Offset: 0x001B20F2
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060074E8 RID: 29928 RVA: 0x001B3EF9 File Offset: 0x001B20F9
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060074E9 RID: 29929 RVA: 0x001B3F00 File Offset: 0x001B2100
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060074EA RID: 29930 RVA: 0x001B3F07 File Offset: 0x001B2107
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x060074EB RID: 29931 RVA: 0x001B3F0E File Offset: 0x001B210E
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x04001C6C RID: 7276
		private readonly string _name;

		// Token: 0x04001C6D RID: 7277
		private readonly NamespaceSymbol _containingSymbol;
	}
}
