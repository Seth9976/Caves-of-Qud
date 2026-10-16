using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CD RID: 1485
	internal sealed class RangeVariableSymbol : Symbol, IRangeVariableSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007C3C RID: 31804 RVA: 0x001C120E File Offset: 0x001BF40E
		public RangeVariableSymbol(RangeVariableSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x06007C3D RID: 31805 RVA: 0x001C121D File Offset: 0x001BF41D
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x001C1225 File Offset: 0x001BF425
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitRangeVariable(this);
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x001C122E File Offset: 0x001BF42E
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitRangeVariable(this);
		}

		// Token: 0x04001D52 RID: 7506
		private readonly RangeVariableSymbol _underlying;
	}
}
