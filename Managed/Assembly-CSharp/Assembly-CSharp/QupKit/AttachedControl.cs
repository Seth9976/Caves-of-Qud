using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003B6 RID: 950
	public class AttachedControl : BaseControl
	{
		// Token: 0x06002365 RID: 9061 RVA: 0x000B389C File Offset: 0x000B1A9C
		public AttachedControl(GameObject GO)
		{
			base.rootObject = GO;
			base.Name = GO.name;
			base.Width = GO.GetComponent<RectTransform>().sizeDelta.x;
			base.Height = GO.GetComponent<RectTransform>().sizeDelta.y;
		}
	}
}
