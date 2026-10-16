using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000029 RID: 41
	internal class GooglePlayProductCallback : IGoogleProductCallback
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003EC1 File Offset: 0x000020C1
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public void SetStoreConfiguration(IGooglePlayConfigurationInternal configuration)
		{
			this.m_GooglePlayConfigurationInternal = configuration;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003ECA File Offset: 0x000020CA
		public void NotifyQueryProductDetailsFailed(int retryCount)
		{
			IGooglePlayConfigurationInternal googlePlayConfigurationInternal = this.m_GooglePlayConfigurationInternal;
			if (googlePlayConfigurationInternal == null)
			{
				return;
			}
			googlePlayConfigurationInternal.NotifyQueryProductDetailsFailed(retryCount);
		}

		// Token: 0x0400005D RID: 93
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IGooglePlayConfigurationInternal m_GooglePlayConfigurationInternal;
	}
}
