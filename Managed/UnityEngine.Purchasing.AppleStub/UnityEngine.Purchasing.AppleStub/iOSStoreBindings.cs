using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000003 RID: 3
	internal class iOSStoreBindings : INativeAppleStore, INativeStore
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public void SetUnityPurchasingCallback(UnityPurchasingCallback AsyncCallback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020BF File Offset: 0x000002BF
		public void RestoreTransactions()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C6 File Offset: 0x000002C6
		public void RefreshAppReceipt()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020CD File Offset: 0x000002CD
		public void AddTransactionObserver()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D4 File Offset: 0x000002D4
		public void SetApplicationUsername(string applicationUsername)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020DB File Offset: 0x000002DB
		public void RetrieveProducts(string json)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E2 File Offset: 0x000002E2
		public void Purchase(string productJSON, string developerPayload)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020E9 File Offset: 0x000002E9
		public void FinishTransaction(string productJSON, string transactionID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020F0 File Offset: 0x000002F0
		public string appReceipt
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020F7 File Offset: 0x000002F7
		public double appReceiptModificationDate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020FE File Offset: 0x000002FE
		public bool canMakePayments
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000210C File Offset: 0x0000030C
		public bool simulateAskToBuy
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002113 File Offset: 0x00000313
		public void FetchStorePromotionOrder()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000211A File Offset: 0x0000031A
		public void SetStorePromotionOrder(string json)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002121 File Offset: 0x00000321
		public void FetchStorePromotionVisibility(string productId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002128 File Offset: 0x00000328
		public void SetStorePromotionVisibility(string productId, string visibility)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000212F File Offset: 0x0000032F
		public string GetTransactionReceiptForProductId(string productId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002136 File Offset: 0x00000336
		public void InterceptPromotionalPurchases()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000213D File Offset: 0x0000033D
		public void ContinuePromotionalPurchases()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002144 File Offset: 0x00000344
		public void PresentCodeRedemptionSheet()
		{
			throw new NotImplementedException();
		}
	}
}
