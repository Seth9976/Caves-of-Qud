using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FA RID: 1274
	internal sealed class MutableTypeMap : AbstractTypeParameterMap
	{
		// Token: 0x06006BED RID: 27629 RVA: 0x0018F88F File Offset: 0x0018DA8F
		internal MutableTypeMap()
			: base(new SmallDictionary<TypeParameterSymbol, TypeWithAnnotations>())
		{
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x0018F89C File Offset: 0x0018DA9C
		internal void Add(TypeParameterSymbol key, TypeWithAnnotations value)
		{
			this.Mapping.Add(key, value);
		}
	}
}
