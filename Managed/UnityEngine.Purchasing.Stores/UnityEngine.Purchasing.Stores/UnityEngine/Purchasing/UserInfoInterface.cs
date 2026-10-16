using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000053 RID: 83
	internal class UserInfoInterface
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00006360 File Offset: 0x00004560
		internal static Type GetClassType()
		{
			if (UserInfoInterface.s_typeCache == null)
			{
				UserInfoInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.UserInfo");
			}
			return UserInfoInterface.s_typeCache;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006383 File Offset: 0x00004583
		internal static PropertyInfo GetChannelProp()
		{
			return UserInfoInterface.GetClassType().GetProperty("Channel", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006396 File Offset: 0x00004596
		internal static PropertyInfo GetIdProp()
		{
			return UserInfoInterface.GetClassType().GetProperty("UserId", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000063A9 File Offset: 0x000045A9
		internal static PropertyInfo GetLoginTokenProp()
		{
			return UserInfoInterface.GetClassType().GetProperty("UserLoginToken", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x040000D2 RID: 210
		private static Type s_typeCache;
	}
}
