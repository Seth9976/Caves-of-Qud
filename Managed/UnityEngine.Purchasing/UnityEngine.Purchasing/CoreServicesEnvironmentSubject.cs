using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000012 RID: 18
	internal class CoreServicesEnvironmentSubject
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000024B0 File Offset: 0x000006B0
		internal static CoreServicesEnvironmentSubject Instance()
		{
			if (CoreServicesEnvironmentSubject.s_Instance == null)
			{
				CoreServicesEnvironmentSubject.s_Instance = new CoreServicesEnvironmentSubject();
			}
			return CoreServicesEnvironmentSubject.s_Instance;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000024C8 File Offset: 0x000006C8
		public void SubscribeToUpdatesAndGetCurrent(ICoreServicesEnvironmentObserver newObserver)
		{
			if (!this.m_Observers.Contains(newObserver))
			{
				this.m_Observers.Add(newObserver);
				newObserver.OnUpdatedCoreServicesEnvironment(this.m_LastKnownEnvironment);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000024F0 File Offset: 0x000006F0
		internal void UpdateCurrentEnvironment(string currentEnvironment)
		{
			this.m_LastKnownEnvironment = currentEnvironment;
			this.NotifyObservers();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002500 File Offset: 0x00000700
		private void NotifyObservers()
		{
			foreach (ICoreServicesEnvironmentObserver coreServicesEnvironmentObserver in this.m_Observers)
			{
				coreServicesEnvironmentObserver.OnUpdatedCoreServicesEnvironment(this.m_LastKnownEnvironment);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002558 File Offset: 0x00000758
		internal bool IsDefaultLiveEnvironment(string environment)
		{
			return environment == "production";
		}

		// Token: 0x04000010 RID: 16
		private static CoreServicesEnvironmentSubject s_Instance;

		// Token: 0x04000011 RID: 17
		private const string k_DefaultLiveEnvironment = "production";

		// Token: 0x04000012 RID: 18
		private string m_LastKnownEnvironment;

		// Token: 0x04000013 RID: 19
		private List<ICoreServicesEnvironmentObserver> m_Observers = new List<ICoreServicesEnvironmentObserver>();
	}
}
