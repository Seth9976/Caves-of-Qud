using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200051E RID: 1310
	internal interface IAttributeTargetSymbol
	{
		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x06006DEB RID: 28139
		IAttributeTargetSymbol AttributesOwner { get; }

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x06006DEC RID: 28140
		AttributeLocation AllowedAttributeLocations { get; }

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06006DED RID: 28141
		AttributeLocation DefaultAttributeLocation { get; }
	}
}
