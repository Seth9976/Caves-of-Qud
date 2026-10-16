using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005B8 RID: 1464
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArrayTypeSymbol : TypeSymbol, IArrayTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B02 RID: 31490 RVA: 0x001BFE2E File Offset: 0x001BE02E
		public ArrayTypeSymbol(ArrayTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001BFE3E File Offset: 0x001BE03E
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new ArrayTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x06007B04 RID: 31492 RVA: 0x001BFE4C File Offset: 0x001BE04C
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06007B05 RID: 31493 RVA: 0x001BFE54 File Offset: 0x001BE054
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x06007B06 RID: 31494 RVA: 0x001BFE5C File Offset: 0x001BE05C
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x06007B07 RID: 31495 RVA: 0x001BFE64 File Offset: 0x001BE064
		int IArrayTypeSymbol.Rank
		{
			get
			{
				return this._underlying.Rank;
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x06007B08 RID: 31496 RVA: 0x001BFE71 File Offset: 0x001BE071
		bool IArrayTypeSymbol.IsSZArray
		{
			get
			{
				return this._underlying.IsSZArray;
			}
		}

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x06007B09 RID: 31497 RVA: 0x001BFE7E File Offset: 0x001BE07E
		[global::System.Runtime.CompilerServices.Nullable(0)]
		ImmutableArray<int> IArrayTypeSymbol.LowerBounds
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this._underlying.LowerBounds;
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x06007B0A RID: 31498 RVA: 0x001BFE8B File Offset: 0x001BE08B
		[global::System.Runtime.CompilerServices.Nullable(0)]
		ImmutableArray<int> IArrayTypeSymbol.Sizes
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this._underlying.Sizes;
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06007B0B RID: 31499 RVA: 0x001BFE98 File Offset: 0x001BE098
		ITypeSymbol IArrayTypeSymbol.ElementType
		{
			get
			{
				if (this._lazyElementType == null)
				{
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyElementType, this._underlying.ElementTypeWithAnnotations.GetPublicSymbol(), null);
				}
				return this._lazyElementType;
			}
		}

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06007B0C RID: 31500 RVA: 0x001BFEC5 File Offset: 0x001BE0C5
		NullableAnnotation IArrayTypeSymbol.ElementNullableAnnotation
		{
			get
			{
				return this._underlying.ElementTypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06007B0D RID: 31501 RVA: 0x001BFED8 File Offset: 0x001BE0D8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> IArrayTypeSymbol.CustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlying.ElementTypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x001BFEF8 File Offset: 0x001BE0F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		bool IArrayTypeSymbol.Equals(IArrayTypeSymbol other)
		{
			return base.Equals(other as ArrayTypeSymbol, SymbolEqualityComparer.Default);
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x001BFF0B File Offset: 0x001BE10B
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitArrayType(this);
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x001BFF14 File Offset: 0x001BE114
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitArrayType(this);
		}

		// Token: 0x04001D34 RID: 7476
		private readonly ArrayTypeSymbol _underlying;

		// Token: 0x04001D35 RID: 7477
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ITypeSymbol _lazyElementType;
	}
}
