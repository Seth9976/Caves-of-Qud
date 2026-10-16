using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200004D RID: 77
	internal class AppStoreSettingsInterface
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00006054 File Offset: 0x00004254
		internal static Type GetClassType()
		{
			if (AppStoreSettingsInterface.s_typeCache == null)
			{
				AppStoreSettingsInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.AppStoreSettings");
			}
			return AppStoreSettingsInterface.s_typeCache;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006077 File Offset: 0x00004277
		internal static FieldInfo GetClientIDField()
		{
			return AppStoreSettingsInterface.GetClassType().GetField("UnityClientID", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000608A File Offset: 0x0000428A
		internal static FieldInfo GetAppSlugField()
		{
			return AppStoreSettingsInterface.GetClassType().GetField("AppSlug", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000609D File Offset: 0x0000429D
		internal static FieldInfo GetAssetPathField()
		{
			return AppStoreSettingsInterface.GetClassType().GetField("appStoreSettingsAssetPath", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x040000CC RID: 204
		private static Type s_typeCache;
	}
}
