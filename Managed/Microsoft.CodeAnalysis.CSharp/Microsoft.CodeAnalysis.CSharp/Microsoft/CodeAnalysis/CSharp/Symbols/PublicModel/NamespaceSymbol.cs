using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C6 RID: 1478
	internal sealed class NamespaceSymbol : NamespaceOrTypeSymbol, INamespaceSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007BD1 RID: 31697 RVA: 0x001C0CCD File Offset: 0x001BEECD
		public NamespaceSymbol(NamespaceSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x06007BD2 RID: 31698 RVA: 0x001C0CDC File Offset: 0x001BEEDC
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x06007BD3 RID: 31699 RVA: 0x001C0CE4 File Offset: 0x001BEEE4
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x06007BD4 RID: 31700 RVA: 0x001C0CEC File Offset: 0x001BEEEC
		internal NamespaceSymbol UnderlyingNamespaceSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x06007BD5 RID: 31701 RVA: 0x001C0CF4 File Offset: 0x001BEEF4
		bool INamespaceSymbol.IsGlobalNamespace
		{
			get
			{
				return this._underlying.IsGlobalNamespace;
			}
		}

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x06007BD6 RID: 31702 RVA: 0x001C0D01 File Offset: 0x001BEF01
		NamespaceKind INamespaceSymbol.NamespaceKind
		{
			get
			{
				return this._underlying.NamespaceKind;
			}
		}

		// Token: 0x1700240E RID: 9230
		// (get) Token: 0x06007BD7 RID: 31703 RVA: 0x001C0D0E File Offset: 0x001BEF0E
		Compilation INamespaceSymbol.ContainingCompilation
		{
			get
			{
				return this._underlying.ContainingCompilation;
			}
		}

		// Token: 0x1700240F RID: 9231
		// (get) Token: 0x06007BD8 RID: 31704 RVA: 0x001C0D1B File Offset: 0x001BEF1B
		ImmutableArray<INamespaceSymbol> INamespaceSymbol.ConstituentNamespaces
		{
			get
			{
				return this._underlying.ConstituentNamespaces.GetPublicSymbols();
			}
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x001C0D2D File Offset: 0x001BEF2D
		IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers()
		{
			foreach (Symbol symbol in this._underlying.GetMembers())
			{
				yield return ((NamespaceOrTypeSymbol)symbol).GetPublicSymbol();
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x001C0D3D File Offset: 0x001BEF3D
		IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers(string name)
		{
			foreach (Symbol symbol in this._underlying.GetMembers(name))
			{
				yield return ((NamespaceOrTypeSymbol)symbol).GetPublicSymbol();
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x001C0D54 File Offset: 0x001BEF54
		IEnumerable<INamespaceSymbol> INamespaceSymbol.GetNamespaceMembers()
		{
			foreach (NamespaceSymbol namespaceSymbol in this._underlying.GetNamespaceMembers())
			{
				yield return namespaceSymbol.GetPublicSymbol();
			}
			IEnumerator<NamespaceSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x001C0D64 File Offset: 0x001BEF64
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitNamespace(this);
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x001C0D6D File Offset: 0x001BEF6D
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitNamespace(this);
		}

		// Token: 0x04001D48 RID: 7496
		private readonly NamespaceSymbol _underlying;
	}
}
