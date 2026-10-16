using System;
using Unity.Services.Core;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000015 RID: 21
	internal class UnityServicesInitializationChecker : IUnityServicesInitializationChecker
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002578 File Offset: 0x00000778
		public UnityServicesInitializationChecker(ILogger logger)
		{
			this.m_Logger = logger;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002587 File Offset: 0x00000787
		public void CheckAndLogWarning()
		{
			if (this.IsUninitialized())
			{
				this.LogWarning();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002598 File Offset: 0x00000798
		private bool IsUninitialized()
		{
			bool flag;
			try
			{
				flag = UnityServices.State == ServicesInitializationState.Uninitialized;
			}
			catch (ServicesInitializationException ex)
			{
				this.m_Logger.LogIAPWarning("Exception while getting UnityServices.State: " + ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025E4 File Offset: 0x000007E4
		private void LogWarning()
		{
			this.m_Logger.LogIAPWarning("<b>Unity In-App Purchasing</b> requires <b>Unity Gaming Services</b> to have been initialized before use.\n- Find out how to initialize <b>Unity Gaming Services</b> by following the documentation <i>https://docs.unity.com/ugs-overview/services-core-api.html#InitializationExample</i>\nor download the <i>06 Initialize Gaming Services</i> sample from <i>Package Manager > In-App Purchasing > Samples</i>.\n- If you are using the codeless API, you may want to enable the enable <b>Unity Gaming Services</b> automatic initialization by checking the <b>Automatically initialize Unity Gaming Services</b> checkbox at the bottom of the <b>IAP Catalog</b> window");
		}

		// Token: 0x04000014 RID: 20
		private const string UgsUninitializedMessage = "<b>Unity In-App Purchasing</b> requires <b>Unity Gaming Services</b> to have been initialized before use.\n- Find out how to initialize <b>Unity Gaming Services</b> by following the documentation <i>https://docs.unity.com/ugs-overview/services-core-api.html#InitializationExample</i>\nor download the <i>06 Initialize Gaming Services</i> sample from <i>Package Manager > In-App Purchasing > Samples</i>.\n- If you are using the codeless API, you may want to enable the enable <b>Unity Gaming Services</b> automatic initialization by checking the <b>Automatically initialize Unity Gaming Services</b> checkbox at the bottom of the <b>IAP Catalog</b> window";

		// Token: 0x04000015 RID: 21
		private readonly ILogger m_Logger;
	}
}
