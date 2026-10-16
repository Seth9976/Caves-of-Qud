using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200004E RID: 78
	internal class BuildConfigInterface
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000060B8 File Offset: 0x000042B8
		internal static Type GetClassType()
		{
			if (BuildConfigInterface.s_typeCache == null)
			{
				BuildConfigInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.BuildConfig");
			}
			return BuildConfigInterface.s_typeCache;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000060DB File Offset: 0x000042DB
		private static FieldInfo GetApiEndpointField()
		{
			return BuildConfigInterface.GetClassType().GetField("API_ENDPOINT", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000060EE File Offset: 0x000042EE
		internal static string GetApiEndpoint()
		{
			return (string)BuildConfigInterface.GetApiEndpointField().GetValue(null);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006100 File Offset: 0x00004300
		private static FieldInfo GetIdEndpointField()
		{
			return BuildConfigInterface.GetClassType().GetField("ID_ENDPOINT", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006113 File Offset: 0x00004313
		internal static string GetIdEndpoint()
		{
			return (string)BuildConfigInterface.GetIdEndpointField().GetValue(null);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006125 File Offset: 0x00004325
		private static FieldInfo GetUdpEndpointField()
		{
			return BuildConfigInterface.GetClassType().GetField("UDP_ENDPOINT", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006138 File Offset: 0x00004338
		internal static string GetUdpEndpoint()
		{
			return (string)BuildConfigInterface.GetUdpEndpointField().GetValue(null);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000614A File Offset: 0x0000434A
		private static FieldInfo GetVersionField()
		{
			return BuildConfigInterface.GetClassType().GetField("VERSION", BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000615D File Offset: 0x0000435D
		internal static string GetVersion()
		{
			return (string)BuildConfigInterface.GetVersionField().GetValue(null);
		}

		// Token: 0x040000CD RID: 205
		private static Type s_typeCache;
	}
}
