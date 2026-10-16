using System;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000019 RID: 25
	internal class BillingClientStateListener : AndroidJavaProxy, IBillingClientStateListener
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002F47 File Offset: 0x00001147
		internal BillingClientStateListener()
			: base("com.android.billingclient.api.BillingClientStateListener")
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F54 File Offset: 0x00001154
		public void RegisterOnConnected(Action onConnected)
		{
			this.m_OnConnected = onConnected;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F5D File Offset: 0x0000115D
		public void RegisterOnDisconnected(Action<GoogleBillingResponseCode> onDisconnected)
		{
			this.m_Disconnect = onDisconnected;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F68 File Offset: 0x00001168
		[Preserve]
		public void onBillingSetupFinished(AndroidJavaObject billingResult)
		{
			IGoogleBillingResult googleBillingResult = new GoogleBillingResult(billingResult);
			if (googleBillingResult.responseCode == GoogleBillingResponseCode.Ok)
			{
				this.m_OnConnected();
			}
			else
			{
				this.m_Disconnect(googleBillingResult.responseCode);
			}
			billingResult.Dispose();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FA8 File Offset: 0x000011A8
		[Preserve]
		public void onBillingServiceDisconnected()
		{
			this.m_Disconnect(GoogleBillingResponseCode.ServiceDisconnected);
		}

		// Token: 0x0400002C RID: 44
		private const string k_AndroidBillingClientStateListenerClassName = "com.android.billingclient.api.BillingClientStateListener";

		// Token: 0x0400002D RID: 45
		private Action m_OnConnected;

		// Token: 0x0400002E RID: 46
		private Action<GoogleBillingResponseCode> m_Disconnect;
	}
}
