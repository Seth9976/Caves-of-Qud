using System;
using System.Collections.Generic;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003BB RID: 955
	public class ScrollViewControl : BaseControl
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x000B3B28 File Offset: 0x000B1D28
		public ScrollViewControl(GameObject ScrollView)
		{
			base.rootObject = ScrollView;
			base.Width = ScrollView.GetComponent<RectTransform>().sizeDelta.x;
			base.Height = ScrollView.GetComponent<RectTransform>().sizeDelta.y;
			base.rootObject.GetComponent<RectTransform>().sizeDelta = new Vector2(base.Width, base.Height);
			this.Viewport = base.rootObject.transform.GetChild(0).gameObject;
			this.Content = this.Viewport.transform.GetChild(0).gameObject;
			foreach (object obj in this.Content.transform)
			{
				GameObject gameObject = (GameObject)obj;
				this.ListChildren.Add(gameObject);
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000B3C64 File Offset: 0x000B1E64
		public void AddChild(GameObject NewChild)
		{
			NewChild.transform.parent = this.Content.transform;
			this.ListChildren.Add(NewChild);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000B3C88 File Offset: 0x000B1E88
		public void AddChild(GameObject NewChild, string NewID)
		{
			NewChild.name = NewID;
			this.AddChild(NewChild);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000B3C98 File Offset: 0x000B1E98
		public override void BeforeLayout()
		{
			Vector3 topCorner = this.TopCorner;
			for (int i = 0; i < this.ListChildren.Count; i++)
			{
				RectTransform component = this.ListChildren[i].GetComponent<RectTransform>();
				topCorner.y -= this.ChildBorder.Top;
				this.ListChildren[i].GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
				this.ListChildren[i].GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
				this.ListChildren[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(component.sizeDelta.x / 2f + topCorner.x, -component.sizeDelta.y / 2f + topCorner.y, 0f);
				topCorner.y -= component.sizeDelta.y;
				topCorner.y -= this.ChildBorder.Bottom;
			}
			this.Content.GetComponent<RectTransform>().sizeDelta = new Vector2(this.Content.GetComponent<RectTransform>().sizeDelta.x, -topCorner.y);
		}

		// Token: 0x040011D4 RID: 4564
		public GameObject Viewport;

		// Token: 0x040011D5 RID: 4565
		public GameObject Content;

		// Token: 0x040011D6 RID: 4566
		public Vector3 TopCorner = new Vector3(25f, -80f, 0f);

		// Token: 0x040011D7 RID: 4567
		public LayoutSides ChildBorder = new LayoutSides(10f, 0f, 0f, 0f);

		// Token: 0x040011D8 RID: 4568
		public List<GameObject> ListChildren = new List<GameObject>();
	}
}
