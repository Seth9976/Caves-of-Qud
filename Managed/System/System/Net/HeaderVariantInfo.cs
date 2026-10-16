using System;

namespace System.Net
{
	// Token: 0x02000655 RID: 1621
	internal struct HeaderVariantInfo
	{
		// Token: 0x060032F6 RID: 13046 RVA: 0x000B0FC1 File Offset: 0x000AF1C1
		internal HeaderVariantInfo(string name, CookieVariant variant)
		{
			this.m_name = name;
			this.m_variant = variant;
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000B0FD1 File Offset: 0x000AF1D1
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000B0FD9 File Offset: 0x000AF1D9
		internal CookieVariant Variant
		{
			get
			{
				return this.m_variant;
			}
		}

		// Token: 0x04001E0F RID: 7695
		private string m_name;

		// Token: 0x04001E10 RID: 7696
		private CookieVariant m_variant;
	}
}
