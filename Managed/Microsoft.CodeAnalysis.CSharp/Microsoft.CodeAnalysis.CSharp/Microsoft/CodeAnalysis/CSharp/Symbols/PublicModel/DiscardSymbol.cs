using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BA RID: 1466
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DiscardSymbol : Symbol, IDiscardSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B21 RID: 31521 RVA: 0x001C00D9 File Offset: 0x001BE2D9
		public DiscardSymbol(DiscardSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06007B22 RID: 31522 RVA: 0x001C00E8 File Offset: 0x001BE2E8
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x06007B23 RID: 31523 RVA: 0x001C00F0 File Offset: 0x001BE2F0
		ITypeSymbol IDiscardSymbol.Type
		{
			get
			{
				if (this._lazyType == null)
				{
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyType, this._underlying.TypeWithAnnotations.GetPublicSymbol(), null);
				}
				return this._lazyType;
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x06007B24 RID: 31524 RVA: 0x001C011D File Offset: 0x001BE31D
		NullableAnnotation IDiscardSymbol.NullableAnnotation
		{
			get
			{
				return this._underlying.TypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x001C012F File Offset: 0x001BE32F
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitDiscard(this);
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x001C0138 File Offset: 0x001BE338
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitDiscard(this);
		}

		// Token: 0x04001D36 RID: 7478
		private readonly DiscardSymbol _underlying;

		// Token: 0x04001D37 RID: 7479
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ITypeSymbol _lazyType;
	}
}
