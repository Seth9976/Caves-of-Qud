using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000C RID: 12
	public interface IAmazonExtensions : IStoreExtension
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26
		string amazonUserId { get; }

		// Token: 0x0600001B RID: 27
		void NotifyUnableToFulfillUnavailableProduct(string transactionID);
	}
}
