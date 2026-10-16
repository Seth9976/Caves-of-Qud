using System;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009A RID: 154
	internal interface IBillingClientStateListener
	{
		// Token: 0x0600037D RID: 893
		void RegisterOnConnected(Action onConnected);

		// Token: 0x0600037E RID: 894
		void RegisterOnDisconnected(Action<GoogleBillingResponseCode> onDisconnected);
	}
}
