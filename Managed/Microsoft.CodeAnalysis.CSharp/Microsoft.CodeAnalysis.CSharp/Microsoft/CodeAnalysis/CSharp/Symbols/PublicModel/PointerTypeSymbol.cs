using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CA RID: 1482
	internal sealed class PointerTypeSymbol : TypeSymbol, IPointerTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007BF8 RID: 31736 RVA: 0x001C0EE5 File Offset: 0x001BF0E5
		public PointerTypeSymbol(PointerTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x001C0EF5 File Offset: 0x001BF0F5
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new PointerTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x06007BFA RID: 31738 RVA: 0x001C0F03 File Offset: 0x001BF103
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x06007BFB RID: 31739 RVA: 0x001C0F0B File Offset: 0x001BF10B
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x06007BFC RID: 31740 RVA: 0x001C0F13 File Offset: 0x001BF113
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06007BFD RID: 31741 RVA: 0x001C0F1B File Offset: 0x001BF11B
		ITypeSymbol IPointerTypeSymbol.PointedAtType
		{
			get
			{
				if (this._lazyPointedAtType == null)
				{
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyPointedAtType, this._underlying.PointedAtTypeWithAnnotations.GetPublicSymbol(), null);
				}
				return this._lazyPointedAtType;
			}
		}

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06007BFE RID: 31742 RVA: 0x001C0F48 File Offset: 0x001BF148
		ImmutableArray<CustomModifier> IPointerTypeSymbol.CustomModifiers
		{
			get
			{
				return this._underlying.PointedAtTypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x001C0F68 File Offset: 0x001BF168
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitPointerType(this);
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x001C0F71 File Offset: 0x001BF171
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitPointerType(this);
		}

		// Token: 0x04001D4D RID: 7501
		private readonly PointerTypeSymbol _underlying;

		// Token: 0x04001D4E RID: 7502
		private ITypeSymbol _lazyPointedAtType;
	}
}
