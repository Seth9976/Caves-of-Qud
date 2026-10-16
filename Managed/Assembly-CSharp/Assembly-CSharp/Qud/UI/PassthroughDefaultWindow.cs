using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200049E RID: 1182
	[UIView("PassthroughDefault", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "PassthroughDefault", UICanvasHost = 1)]
	public class PassthroughDefaultWindow : SingletonWindowBase<StageWindow>
	{
		// Token: 0x06002D59 RID: 11609 RVA: 0x000F556F File Offset: 0x000F376F
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000F5572 File Offset: 0x000F3772
		public void Start()
		{
		}
	}
}
