using System;

namespace Unity.Services.Core
{
	// Token: 0x02000010 RID: 16
	internal static class UnityServicesBuilder
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002621 File Offset: 0x00000821
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002628 File Offset: 0x00000828
		internal static UnityServicesBuilder.CreationDelegate InstanceCreationDelegate { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00002630 File Offset: 0x00000830
		public static IUnityServices Create(string servicesId)
		{
			if (UnityServicesBuilder.InstanceCreationDelegate == null)
			{
				throw new ServicesCreationException("Error creating services. The creation delegate has not been initialized.");
			}
			return UnityServicesBuilder.InstanceCreationDelegate(servicesId);
		}

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x06000051 RID: 81
		internal delegate IUnityServices CreationDelegate(string servicesId);
	}
}
