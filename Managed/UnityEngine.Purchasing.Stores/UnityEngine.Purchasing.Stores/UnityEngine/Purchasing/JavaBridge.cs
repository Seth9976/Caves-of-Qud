using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000042 RID: 66
	internal class JavaBridge : AndroidJavaProxy, IUnityCallback
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000054F1 File Offset: 0x000036F1
		public JavaBridge(IUnityCallback forwardTo)
			: base("com.unity.purchasing.common.IUnityCallback")
		{
			this.forwardTo = forwardTo;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005505 File Offset: 0x00003705
		public JavaBridge(IUnityCallback forwardTo, string javaInterface)
			: base(javaInterface)
		{
			this.forwardTo = forwardTo;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005515 File Offset: 0x00003715
		public void OnSetupFailed(string json)
		{
			this.forwardTo.OnSetupFailed(json);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005523 File Offset: 0x00003723
		public void OnProductsRetrieved(string json)
		{
			this.forwardTo.OnProductsRetrieved(json);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005531 File Offset: 0x00003731
		public void OnPurchaseSucceeded(string id, string receipt, string transactionID)
		{
			this.forwardTo.OnPurchaseSucceeded(id, receipt, transactionID);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005541 File Offset: 0x00003741
		public void OnPurchaseFailed(string json)
		{
			this.forwardTo.OnPurchaseFailed(json);
		}

		// Token: 0x04000095 RID: 149
		private readonly IUnityCallback forwardTo;
	}
}
