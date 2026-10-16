using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000A RID: 10
	public class FakeAmazonExtensions : IAmazonExtensions, IStoreExtension, IAmazonConfiguration, IStoreConfiguration
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023B3 File Offset: 0x000005B3
		public void WriteSandboxJSON(HashSet<ProductDefinition> products)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023B5 File Offset: 0x000005B5
		public void NotifyUnableToFulfillUnavailableProduct(string transactionID)
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023B7 File Offset: 0x000005B7
		public string amazonUserId
		{
			get
			{
				return "fakeid";
			}
		}
	}
}
