using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000510 RID: 1296
	internal class RangeVariableSymbol : Symbol
	{
		// Token: 0x06006CC2 RID: 27842 RVA: 0x00191D9C File Offset: 0x0018FF9C
		internal RangeVariableSymbol(string Name, Symbol containingSymbol, Location location, bool isTransparent = false)
		{
			this._name = Name;
			this._containingSymbol = containingSymbol;
			this._locations = ImmutableArray.Create<Location>(location);
			this.IsTransparent = isTransparent;
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x00191DC6 File Offset: 0x0018FFC6
		internal bool IsTransparent { get; }

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06006CC4 RID: 27844 RVA: 0x00191DCE File Offset: 0x0018FFCE
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x00191DD6 File Offset: 0x0018FFD6
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.RangeVariable;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06006CC6 RID: 27846 RVA: 0x00191DDA File Offset: 0x0018FFDA
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x06006CC7 RID: 27847 RVA: 0x00191DE4 File Offset: 0x0018FFE4
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray.Create<SyntaxReference>(((CSharpSyntaxNode)this._locations[0].SourceTree.GetRoot(default(CancellationToken)).FindToken(this._locations[0].SourceSpan.Start, false).Parent).GetReference());
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x06006CC8 RID: 27848 RVA: 0x00191E4C File Offset: 0x0019004C
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06006CC9 RID: 27849 RVA: 0x00191E4F File Offset: 0x0019004F
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x06006CCA RID: 27850 RVA: 0x00191E52 File Offset: 0x00190052
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06006CCB RID: 27851 RVA: 0x00191E55 File Offset: 0x00190055
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06006CCC RID: 27852 RVA: 0x00191E58 File Offset: 0x00190058
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06006CCD RID: 27853 RVA: 0x00191E5B File Offset: 0x0019005B
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06006CCE RID: 27854 RVA: 0x00191E5E File Offset: 0x0019005E
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06006CCF RID: 27855 RVA: 0x00191E61 File Offset: 0x00190061
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06006CD0 RID: 27856 RVA: 0x00191E64 File Offset: 0x00190064
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x00191E6C File Offset: 0x0019006C
		internal override TResult Accept<TArg, TResult>(CSharpSymbolVisitor<TArg, TResult> visitor, TArg a)
		{
			return visitor.VisitRangeVariable(this, a);
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x00191E76 File Offset: 0x00190076
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitRangeVariable(this);
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00191E7F File Offset: 0x0019007F
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitRangeVariable(this);
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x00191E88 File Offset: 0x00190088
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (obj == this)
			{
				return true;
			}
			RangeVariableSymbol rangeVariableSymbol = obj as RangeVariableSymbol;
			return rangeVariableSymbol != null && rangeVariableSymbol._locations[0].Equals(this._locations[0]) && this._containingSymbol.Equals(rangeVariableSymbol.ContainingSymbol, compareKind);
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x00191EE0 File Offset: 0x001900E0
		public override int GetHashCode()
		{
			return Hash.Combine(this._locations[0].GetHashCode(), this._containingSymbol.GetHashCode());
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x00191F11 File Offset: 0x00190111
		protected override ISymbol CreateISymbol()
		{
			return new RangeVariableSymbol(this);
		}

		// Token: 0x04001AEB RID: 6891
		private readonly string _name;

		// Token: 0x04001AEC RID: 6892
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001AED RID: 6893
		private readonly Symbol _containingSymbol;
	}
}
