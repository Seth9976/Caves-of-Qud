using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005BB RID: 1467
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DynamicTypeSymbol : TypeSymbol, IDynamicTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B27 RID: 31527 RVA: 0x001C0141 File Offset: 0x001BE341
		public DynamicTypeSymbol(DynamicTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x001C0151 File Offset: 0x001BE351
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new DynamicTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x06007B29 RID: 31529 RVA: 0x001C015F File Offset: 0x001BE35F
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x06007B2A RID: 31530 RVA: 0x001C0167 File Offset: 0x001BE367
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06007B2B RID: 31531 RVA: 0x001C016F File Offset: 0x001BE36F
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x001C0177 File Offset: 0x001BE377
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitDynamicType(this);
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x001C0180 File Offset: 0x001BE380
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitDynamicType(this);
		}

		// Token: 0x04001D38 RID: 7480
		private readonly DynamicTypeSymbol _underlying;
	}
}
