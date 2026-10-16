using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C0 RID: 1472
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LabelSymbol : Symbol, ILabelSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B5D RID: 31581 RVA: 0x001C0453 File Offset: 0x001BE653
		public LabelSymbol(LabelSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x06007B5E RID: 31582 RVA: 0x001C0462 File Offset: 0x001BE662
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x06007B5F RID: 31583 RVA: 0x001C046A File Offset: 0x001BE66A
		IMethodSymbol ILabelSymbol.ContainingMethod
		{
			get
			{
				return this._underlying.ContainingMethod.GetPublicSymbol();
			}
		}

		// Token: 0x06007B60 RID: 31584 RVA: 0x001C047C File Offset: 0x001BE67C
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitLabel(this);
		}

		// Token: 0x06007B61 RID: 31585 RVA: 0x001C0485 File Offset: 0x001BE685
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitLabel(this);
		}

		// Token: 0x04001D3F RID: 7487
		private readonly LabelSymbol _underlying;
	}
}
