using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001FF RID: 511
	public class MenuBar : MonoBehaviour
	{
		// Token: 0x06001272 RID: 4722 RVA: 0x0006D7B0 File Offset: 0x0006B9B0
		public MenuBarButton addMenu(string text, Menu menu)
		{
			menu.DestroyOnClose = false;
			MenuBarButton component = global::UnityEngine.Object.Instantiate<MenuBarButton>(this.MenuBarButtonPrefab).GetComponent<MenuBarButton>();
			GameObject gameObject = base.transform.Find("Panel").gameObject;
			component.transform.SetParent(gameObject.transform, false);
			component.setText(text);
			component.Menu = menu;
			this._menus.Add(component);
			return component;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0006D81C File Offset: 0x0006BA1C
		public MenuBarButton insertMenu(string text, Menu menu, string before)
		{
			MenuBarButton menuBarButton = this.addMenu(text, menu);
			int num = this._menus.FindIndex((MenuBarButton b) => b.Text == text);
			if (num != -1)
			{
				int siblingIndex = this._menus[num].transform.GetSiblingIndex();
				menuBarButton.transform.SetSiblingIndex(siblingIndex);
			}
			return menuBarButton;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0006D884 File Offset: 0x0006BA84
		public MenuItem findItem(string text)
		{
			foreach (MenuBarButton menuBarButton in this._menus)
			{
				MenuItem menuItem = menuBarButton.Menu.findItem(text);
				if (menuItem)
				{
					return menuItem;
				}
			}
			return null;
		}

		// Token: 0x04000C4E RID: 3150
		public MenuBarButton MenuBarButtonPrefab;

		// Token: 0x04000C4F RID: 3151
		private readonly List<MenuBarButton> _menus = new List<MenuBarButton>();
	}
}
