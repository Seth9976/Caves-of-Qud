using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DB RID: 1243
	internal sealed class DynamicTypeEraser : AbstractTypeMap
	{
		// Token: 0x0600690C RID: 26892 RVA: 0x0018A659 File Offset: 0x00188859
		public DynamicTypeEraser(TypeSymbol objectType)
		{
			this._objectType = objectType;
		}

		// Token: 0x0600690D RID: 26893 RVA: 0x0018A668 File Offset: 0x00188868
		public TypeSymbol EraseDynamic(TypeSymbol type)
		{
			return base.SubstituteType(type).AsTypeSymbolOnly();
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x0018A684 File Offset: 0x00188884
		protected override TypeSymbol SubstituteDynamicType()
		{
			return this._objectType;
		}

		// Token: 0x04001A3E RID: 6718
		private readonly TypeSymbol _objectType;
	}
}
