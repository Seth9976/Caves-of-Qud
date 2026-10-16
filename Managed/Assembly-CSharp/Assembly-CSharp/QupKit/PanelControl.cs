using System;
using UnityEngine;
using UnityEngine.UI;

namespace QupKit
{
	// Token: 0x020003B9 RID: 953
	public class PanelControl : BaseControl
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000B3A30 File Offset: 0x000B1C30
		public PanelControl(string Name, float Width, float Height)
		{
			base.rootObject = new GameObject();
			base.rootObject.AddComponent<RectTransform>();
			base.rootObject.AddComponent<CanvasRenderer>();
			base.rootObject.AddComponent<Image>();
			base.rootObject.AddComponent<Mask>();
			base.MainColor = new Color(0f, 0f, 0f, 1f);
			base.rootObject.name = Name;
			base.Width = Width;
			base.Height = Height;
			base.Name = Name;
		}
	}
}
