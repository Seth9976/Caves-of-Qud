using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200004F RID: 79
	internal class InventoryInterface
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006177 File Offset: 0x00004377
		internal static Type GetClassType()
		{
			if (InventoryInterface.s_typeCache == null)
			{
				InventoryInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.Inventory");
			}
			return InventoryInterface.s_typeCache;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000619A File Offset: 0x0000439A
		internal static MethodInfo GetProductListMethod()
		{
			return InventoryInterface.GetClassType().GetMethod("GetProductList", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000061AD File Offset: 0x000043AD
		internal static MethodInfo GetPurchaseInfoMethod()
		{
			return InventoryInterface.GetClassType().GetMethod("GetPurchaseInfo", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000061C0 File Offset: 0x000043C0
		internal static MethodInfo HasPurchaseMethod()
		{
			return InventoryInterface.GetClassType().GetMethod("HasPurchase", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x040000CE RID: 206
		private static Type s_typeCache;
	}
}
