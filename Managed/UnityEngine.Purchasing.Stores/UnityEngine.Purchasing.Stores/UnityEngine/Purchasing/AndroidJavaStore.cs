using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000D RID: 13
	internal class AndroidJavaStore : INativeStore
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023C6 File Offset: 0x000005C6
		protected AndroidJavaObject GetStore()
		{
			return this.m_Store;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023CE File Offset: 0x000005CE
		public AndroidJavaStore(AndroidJavaObject store)
		{
			this.m_Store = store;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023DD File Offset: 0x000005DD
		public void RetrieveProducts(string json)
		{
			this.m_Store.Call("RetrieveProducts", new object[] { json });
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023F9 File Offset: 0x000005F9
		public virtual void Purchase(string productJSON, string developerPayload)
		{
			this.m_Store.Call("Purchase", new object[] { productJSON, developerPayload });
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002419 File Offset: 0x00000619
		public virtual void FinishTransaction(string productJSON, string transactionID)
		{
			this.m_Store.Call("FinishTransaction", new object[] { productJSON, transactionID });
		}

		// Token: 0x04000007 RID: 7
		private readonly AndroidJavaObject m_Store;
	}
}
