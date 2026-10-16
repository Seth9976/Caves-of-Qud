using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QupKit
{
	// Token: 0x020003B7 RID: 951
	public class ButtonControl : BaseControl
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x000B38F0 File Offset: 0x000B1AF0
		public ButtonControl(string ID, string Text, int Width, int Height, Action<PointerEventData> OnClick = null)
		{
			base.rootObject = PrefabManager.Create("Button", "Prefabs/");
			base.rootObject.name = ID;
			base.rootObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)Width, (float)Height);
			base.Label = Text;
			base.Width = base.rootObject.GetComponent<RectTransform>().sizeDelta.x;
			base.Height = base.rootObject.GetComponent<RectTransform>().sizeDelta.y;
			this.OnClicked = OnClick;
		}
	}
}
