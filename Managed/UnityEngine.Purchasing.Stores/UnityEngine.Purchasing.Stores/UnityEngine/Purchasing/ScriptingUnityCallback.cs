using System;
using Uniject;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000044 RID: 68
	internal class ScriptingUnityCallback : IUnityCallback
	{
		// Token: 0x06000169 RID: 361 RVA: 0x000056ED File Offset: 0x000038ED
		public ScriptingUnityCallback(IUnityCallback forwardTo, IUtil util)
		{
			this.forwardTo = forwardTo;
			this.util = util;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005704 File Offset: 0x00003904
		public void OnSetupFailed(string json)
		{
			this.util.RunOnMainThread(delegate
			{
				this.forwardTo.OnSetupFailed(json);
			});
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000573C File Offset: 0x0000393C
		public void OnProductsRetrieved(string json)
		{
			this.util.RunOnMainThread(delegate
			{
				this.forwardTo.OnProductsRetrieved(json);
			});
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005774 File Offset: 0x00003974
		public void OnPurchaseSucceeded(string id, string receipt, string transactionID)
		{
			this.util.RunOnMainThread(delegate
			{
				this.forwardTo.OnPurchaseSucceeded(id, receipt, transactionID);
			});
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000057BC File Offset: 0x000039BC
		public void OnPurchaseFailed(string json)
		{
			this.util.RunOnMainThread(delegate
			{
				this.forwardTo.OnPurchaseFailed(json);
			});
		}

		// Token: 0x04000099 RID: 153
		private readonly IUnityCallback forwardTo;

		// Token: 0x0400009A RID: 154
		private readonly IUtil util;
	}
}
