using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001FE RID: 510
	public class Menu : DialogBase
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0006D479 File Offset: 0x0006B679
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0006D481 File Offset: 0x0006B681
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0006D48A File Offset: 0x0006B68A
		public IReadOnlyList<MenuItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0006D494 File Offset: 0x0006B694
		public override void Update()
		{
			base.Update();
			if (base.isTop() && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
			{
				RectTransform component = base.GetComponent<RectTransform>();
				Vector2 vector = Input.mousePosition;
				Vector2 vector2;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(component, vector, null, out vector2);
				if (!component.rect.Contains(vector2))
				{
					this.cancel();
				}
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
		public void show(Vector2 pos)
		{
			base.gameObject.SetActive(true);
			base.GetComponent<RectTransform>().position = pos;
			bool flag = false;
			foreach (MenuItem menuItem in this._items)
			{
				flag |= menuItem.Checkable;
			}
			foreach (MenuItem menuItem2 in this._items)
			{
				menuItem2.showCheckbox(flag);
			}
			Canvas.ForceUpdateCanvases();
			base.ensureVisible();
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0006D5C8 File Offset: 0x0006B7C8
		public void show(RectTransform parent)
		{
			Vector3[] array = new Vector3[4];
			parent.GetWorldCorners(array);
			this.show(array[0]);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0006D5F8 File Offset: 0x0006B7F8
		public MenuItem addItem(Sprite icon, string text)
		{
			MenuItem component = global::UnityEngine.Object.Instantiate<MenuItem>(this.MenuItemPrefab).GetComponent<MenuItem>();
			component.transform.SetParent(base.transform, false);
			component.setText(text);
			component.setIcon(icon);
			component.setCallback(delegate
			{
				this.onItemClick(text);
			});
			this._items.Add(component);
			return component;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0006D671 File Offset: 0x0006B871
		public void addSeparator()
		{
			global::UnityEngine.Object.Instantiate<GameObject>(this.MenuSeparatorPrefab).transform.SetParent(base.transform, false);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0006D690 File Offset: 0x0006B890
		public MenuItem findItem(string text)
		{
			return this._items.Find((MenuItem i) => i.Text == text);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0006D6C1 File Offset: 0x0006B8C1
		public bool Remove(MenuItem Item)
		{
			if (this._items.Remove(Item))
			{
				global::UnityEngine.Object.Destroy(Item.gameObject);
				return true;
			}
			return false;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0006D6DF File Offset: 0x0006B8DF
		public bool RemoveAt(int Index)
		{
			if (Index < this._items.Count)
			{
				global::UnityEngine.Object.Destroy(this._items[Index].gameObject);
				this._items.RemoveAt(Index);
				return true;
			}
			return false;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006D714 File Offset: 0x0006B914
		public void clear()
		{
			this._items.Clear();
			foreach (object obj in base.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0006D77C File Offset: 0x0006B97C
		private void onItemClick(string text)
		{
			this._onClick.Invoke(text);
			this.hide();
		}

		// Token: 0x04000C49 RID: 3145
		public MenuItem MenuItemPrefab;

		// Token: 0x04000C4A RID: 3146
		public GameObject MenuSeparatorPrefab;

		// Token: 0x04000C4B RID: 3147
		private readonly List<MenuItem> _items = new List<MenuItem>();

		// Token: 0x04000C4C RID: 3148
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private MenuItemEvent _onClick = new MenuItemEvent();

		// Token: 0x04000C4D RID: 3149
		private Canvas canvas;
	}
}
