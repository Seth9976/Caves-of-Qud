using System;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000425 RID: 1061
	[ExecuteAlways]
	public class QudTextMenuController : QudBaseMenuController<QudMenuItem, SelectableTextMenuItem>
	{
		// Token: 0x0600282C RID: 10284 RVA: 0x000D0167 File Offset: 0x000CE367
		public override void Update()
		{
			base.Update();
			this.CheckKeyInteractions(this.menuData, new Action<QudMenuItem, QudBaseMenuController.MenuEventPipeline>(this.Activate));
		}
	}
}
