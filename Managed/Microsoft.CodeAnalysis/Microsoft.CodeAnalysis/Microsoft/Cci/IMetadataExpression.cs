using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	internal interface IMetadataExpression
	{
		// Token: 0x06000370 RID: 880
		void Dispatch(MetadataVisitor visitor);

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000371 RID: 881
		ITypeReference Type { get; }
	}
}
