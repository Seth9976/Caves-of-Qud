using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000367 RID: 871
	internal interface IDataControlButton : IButtonControl
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060020B9 RID: 8377
		// (set) Token: 0x060020BA RID: 8378
		Control Container { get; set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060020BB RID: 8379
		// (set) Token: 0x060020BC RID: 8380
		string ImageUrl { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060020BD RID: 8381
		// (set) Token: 0x060020BE RID: 8382
		bool AllowCallback { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060020BF RID: 8383
		ButtonType ButtonType { get; }
	}
}
