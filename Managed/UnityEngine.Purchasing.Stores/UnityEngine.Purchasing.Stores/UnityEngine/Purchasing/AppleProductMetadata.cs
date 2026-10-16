using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000058 RID: 88
	public class AppleProductMetadata : ProductMetadata
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000063ED File Offset: 0x000045ED
		public bool isFamilyShareable { get; }

		// Token: 0x060001C9 RID: 457 RVA: 0x000063F5 File Offset: 0x000045F5
		internal AppleProductMetadata(ProductMetadata baseProductMetadata, string isFamilyShareable)
			: base(baseProductMetadata)
		{
			this.isFamilyShareable = isFamilyShareable == "true";
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000640F File Offset: 0x0000460F
		internal AppleProductMetadata(string priceString, string title, string description, string currencyCode, decimal localizedPrice, string isFamilyShareable)
			: base(priceString, title, description, currencyCode, localizedPrice)
		{
			this.isFamilyShareable = isFamilyShareable == "true";
		}
	}
}
