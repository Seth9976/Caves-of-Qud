using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000051 RID: 81
	internal class StoreServiceInterface
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00006278 File Offset: 0x00004478
		internal static Type GetClassType()
		{
			if (StoreServiceInterface.s_typeCache == null)
			{
				StoreServiceInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.StoreService");
			}
			return StoreServiceInterface.s_typeCache;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000629B File Offset: 0x0000449B
		private static PropertyInfo GetNameProp()
		{
			Type classType = StoreServiceInterface.GetClassType();
			if (classType == null)
			{
				return null;
			}
			return classType.GetProperty("StoreName", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000062B4 File Offset: 0x000044B4
		internal static string GetName()
		{
			PropertyInfo nameProp = StoreServiceInterface.GetNameProp();
			return (string)((nameProp != null) ? nameProp.GetValue(null, null) : null);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000062CE File Offset: 0x000044CE
		internal static MethodInfo GetEnableDebugLoggingMethod()
		{
			return StoreServiceInterface.GetClassType().GetMethod("EnableDebugLogging", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x040000D0 RID: 208
		private static Type s_typeCache;
	}
}
