using System;
using UnityEngine;
using UnityEngine.UI;

namespace QupKit
{
	// Token: 0x020003B8 RID: 952
	public class SpriteControl : BaseControl
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000B3984 File Offset: 0x000B1B84
		public SpriteControl(string Name, string Sprite, float Scale)
		{
			base.rootObject = new GameObject();
			base.rootObject.name = Name;
			base.rootObject.AddComponent<RectTransform>();
			base.rootObject.AddComponent<CanvasRenderer>();
			base.rootObject.AddComponent<Image>();
			base.MainColor = new Color(1f, 1f, 1f, 1f);
			Debug.Log(Sprite);
			base.Sprite = Sprite;
			base.Width = base.rootObject.GetComponent<Image>().preferredWidth * Scale;
			base.Height = base.rootObject.GetComponent<Image>().preferredHeight * Scale;
		}
	}
}
