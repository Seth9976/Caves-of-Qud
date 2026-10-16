using System;

namespace System.Web.Routing
{
	// Token: 0x020004D7 RID: 1239
	internal class BoundUrl
	{
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x00094B25 File Offset: 0x00092D25
		// (set) Token: 0x06003808 RID: 14344 RVA: 0x00094B2D File Offset: 0x00092D2D
		public string Url { get; set; }

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x00094B36 File Offset: 0x00092D36
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x00094B3E File Offset: 0x00092D3E
		public RouteValueDictionary Values { get; set; }
	}
}
