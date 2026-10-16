using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RedShadow.CommonDialogs
{
	// Token: 0x02000207 RID: 519
	public class ToolBar : MonoBehaviour
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0006E567 File Offset: 0x0006C767
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x0006E56F File Offset: 0x0006C76F
		public MenuItemEvent onClick
		{
			get
			{
				return this._onClick;
			}
			set
			{
				this._onClick = value;
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006E578 File Offset: 0x0006C778
		public ToolBarButton addButton(Sprite icon, string text)
		{
			ToolBarButton component = global::UnityEngine.Object.Instantiate<ToolBarButton>(this.ToolBarButtonPrefab).GetComponent<ToolBarButton>();
			GameObject gameObject = base.transform.Find("Panel").gameObject;
			component.transform.SetParent(gameObject.transform, false);
			component.setText(text);
			component.setIcon(icon);
			component.setCallback(delegate
			{
				this.onItemClick(text);
			});
			this._buttons.Add(component);
			return component;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0006E608 File Offset: 0x0006C808
		public GameObject addSeparator()
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.ToolBarSeparatorPrefab);
			GameObject gameObject2 = base.transform.Find("Panel").gameObject;
			gameObject.transform.SetParent(gameObject2.transform, false);
			return gameObject;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0006E648 File Offset: 0x0006C848
		public ToolBarButton findItem(string text)
		{
			return this._buttons.Find((ToolBarButton i) => i.Text == text);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0006E67C File Offset: 0x0006C87C
		public void clear()
		{
			this._buttons.Clear();
			foreach (object obj in base.transform.Find("Panel").gameObject.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		private void onItemClick(string text)
		{
			this._onClick.Invoke(text);
		}

		// Token: 0x04000C84 RID: 3204
		public ToolBarButton ToolBarButtonPrefab;

		// Token: 0x04000C85 RID: 3205
		public GameObject ToolBarSeparatorPrefab;

		// Token: 0x04000C86 RID: 3206
		private readonly List<ToolBarButton> _buttons = new List<ToolBarButton>();

		// Token: 0x04000C87 RID: 3207
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private MenuItemEvent _onClick = new MenuItemEvent();
	}
}
