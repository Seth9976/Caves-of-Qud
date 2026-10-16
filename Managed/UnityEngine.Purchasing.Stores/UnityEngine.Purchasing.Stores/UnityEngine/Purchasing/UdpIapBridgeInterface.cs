using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000052 RID: 82
	internal class UdpIapBridgeInterface
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000062E9 File Offset: 0x000044E9
		internal static Type GetClassType()
		{
			if (UdpIapBridgeInterface.s_typeCache == null)
			{
				UdpIapBridgeInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.UdpIapBridge");
			}
			return UdpIapBridgeInterface.s_typeCache;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000630C File Offset: 0x0000450C
		internal static MethodInfo GetInitMethod()
		{
			return UdpIapBridgeInterface.GetClassType().GetMethod("Initialize", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000631F File Offset: 0x0000451F
		internal static MethodInfo GetPurchaseMethod()
		{
			return UdpIapBridgeInterface.GetClassType().GetMethod("Purchase", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006332 File Offset: 0x00004532
		internal static MethodInfo GetRetrieveProductsMethod()
		{
			return UdpIapBridgeInterface.GetClassType().GetMethod("RetrieveProducts", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006345 File Offset: 0x00004545
		internal static MethodInfo GetFinishTransactionMethod()
		{
			return UdpIapBridgeInterface.GetClassType().GetMethod("FinishTransaction", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x040000D1 RID: 209
		private static Type s_typeCache;
	}
}
