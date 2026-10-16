using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003BA RID: 954
	public class PrefabControl : BaseControl
	{
		// Token: 0x06002369 RID: 9065 RVA: 0x000B3AC0 File Offset: 0x000B1CC0
		public PrefabControl(string Name, string ID)
		{
			base.rootObject = PrefabManager.Create(ID, "Prefabs/");
			base.rootObject.name = Name;
			base.Width = base.rootObject.GetComponent<RectTransform>().sizeDelta.x;
			base.Height = base.rootObject.GetComponent<RectTransform>().sizeDelta.y;
		}
	}
}
