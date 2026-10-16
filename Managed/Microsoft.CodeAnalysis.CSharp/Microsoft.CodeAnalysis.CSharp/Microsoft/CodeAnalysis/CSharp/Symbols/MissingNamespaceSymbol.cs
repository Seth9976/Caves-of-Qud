using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F8 RID: 1272
	internal class MissingNamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x06006BB5 RID: 27573 RVA: 0x0018F5C9 File Offset: 0x0018D7C9
		public MissingNamespaceSymbol(MissingModuleSymbol containingModule)
		{
			this._containingSymbol = containingModule;
			this._name = string.Empty;
		}

		// Token: 0x06006BB6 RID: 27574 RVA: 0x0018F5E3 File Offset: 0x0018D7E3
		public MissingNamespaceSymbol(NamespaceSymbol containingNamespace, string name)
		{
			this._containingSymbol = containingNamespace;
			this._name = name;
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x0018F5F9 File Offset: 0x0018D7F9
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x0018F601 File Offset: 0x0018D801
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x0018F609 File Offset: 0x0018D809
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._containingSymbol.ContainingAssembly;
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0018F616 File Offset: 0x0018D816
		internal override NamespaceExtent Extent
		{
			get
			{
				if (this._containingSymbol.Kind == SymbolKind.NetModule)
				{
					return new NamespaceExtent((ModuleSymbol)this._containingSymbol);
				}
				return ((NamespaceSymbol)this._containingSymbol).Extent;
			}
		}

		// Token: 0x06006BBB RID: 27579 RVA: 0x0018F648 File Offset: 0x0018D848
		public override int GetHashCode()
		{
			return Hash.Combine(this._containingSymbol.GetHashCode(), this._name.GetHashCode());
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x0018F668 File Offset: 0x0018D868
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			MissingNamespaceSymbol missingNamespaceSymbol = obj as MissingNamespaceSymbol;
			return missingNamespaceSymbol != null && this._name.Equals(missingNamespaceSymbol._name) && this._containingSymbol.Equals(missingNamespaceSymbol._containingSymbol, compareKind);
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06006BBD RID: 27581 RVA: 0x0018F6AC File Offset: 0x0018D8AC
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x06006BBE RID: 27582 RVA: 0x0018F6B3 File Offset: 0x0018D8B3
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x0018F6BA File Offset: 0x0018D8BA
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x0018F6C1 File Offset: 0x0018D8C1
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x0018F6C8 File Offset: 0x0018D8C8
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x0018F6CF File Offset: 0x0018D8CF
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x0018F6D6 File Offset: 0x0018D8D6
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x04001ABB RID: 6843
		private readonly string _name;

		// Token: 0x04001ABC RID: 6844
		private readonly Symbol _containingSymbol;
	}
}
