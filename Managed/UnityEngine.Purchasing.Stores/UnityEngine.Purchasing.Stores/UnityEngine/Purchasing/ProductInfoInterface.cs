using System;
using System.Reflection;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000050 RID: 80
	internal class ProductInfoInterface
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000061DB File Offset: 0x000043DB
		private static Type GetClassType()
		{
			if (ProductInfoInterface.s_typeCache == null)
			{
				ProductInfoInterface.s_typeCache = UDPReflectionUtils.GetTypeByName("UnityEngine.UDP.ProductInfo");
			}
			return ProductInfoInterface.s_typeCache;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000061FE File Offset: 0x000043FE
		public static PropertyInfo GetCurrencyProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("Currency", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006211 File Offset: 0x00004411
		public static PropertyInfo GetDescriptionProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("Description", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006224 File Offset: 0x00004424
		public static PropertyInfo GetPriceProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("Price", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006237 File Offset: 0x00004437
		public static PropertyInfo GetPriceAmountMicrosProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("PriceAmountMicros", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000624A File Offset: 0x0000444A
		public static PropertyInfo GetProductIdProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("ProductId", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000625D File Offset: 0x0000445D
		public static PropertyInfo GetTitleProp()
		{
			return ProductInfoInterface.GetClassType().GetProperty("Title", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x040000CF RID: 207
		private static Type s_typeCache;
	}
}
