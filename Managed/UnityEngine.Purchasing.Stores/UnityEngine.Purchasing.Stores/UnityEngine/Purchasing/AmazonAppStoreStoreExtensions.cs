using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000008 RID: 8
	public class AmazonAppStoreStoreExtensions : IAmazonExtensions, IStoreExtension, IAmazonConfiguration, IStoreConfiguration
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002348 File Offset: 0x00000548
		public AmazonAppStoreStoreExtensions(AndroidJavaObject a)
		{
			this.android = a;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002357 File Offset: 0x00000557
		public void WriteSandboxJSON(HashSet<ProductDefinition> products)
		{
			this.android.Call("writeSandboxJSON", new object[] { JSONSerializer.SerializeProductDefs(products) });
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		public void NotifyUnableToFulfillUnavailableProduct(string transactionID)
		{
			this.android.Call("notifyUnableToFulfillUnavailableProduct", new object[] { transactionID });
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002394 File Offset: 0x00000594
		public string amazonUserId
		{
			get
			{
				return this.android.Call<string>("getAmazonUserId", Array.Empty<object>());
			}
		}

		// Token: 0x04000005 RID: 5
		private readonly AndroidJavaObject android;
	}
}
