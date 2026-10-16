using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C7 RID: 1479
	internal sealed class NonErrorNamedTypeSymbol : NamedTypeSymbol
	{
		// Token: 0x06007BDE RID: 31710 RVA: 0x001C0D76 File Offset: 0x001BEF76
		public NonErrorNamedTypeSymbol(NamedTypeSymbol underlying, NullableAnnotation nullableAnnotation)
			: base(nullableAnnotation)
		{
			this._underlying = underlying;
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x001C0D86 File Offset: 0x001BEF86
		protected override ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			return new NonErrorNamedTypeSymbol(this._underlying, nullableAnnotation);
		}

		// Token: 0x17002410 RID: 9232
		// (get) Token: 0x06007BE0 RID: 31712 RVA: 0x001C0D94 File Offset: 0x001BEF94
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002411 RID: 9233
		// (get) Token: 0x06007BE1 RID: 31713 RVA: 0x001C0D9C File Offset: 0x001BEF9C
		internal override NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x06007BE2 RID: 31714 RVA: 0x001C0DA4 File Offset: 0x001BEFA4
		internal override TypeSymbol UnderlyingTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x06007BE3 RID: 31715 RVA: 0x001C0DAC File Offset: 0x001BEFAC
		internal override NamedTypeSymbol UnderlyingNamedTypeSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x04001D49 RID: 7497
		private readonly NamedTypeSymbol _underlying;
	}
}
