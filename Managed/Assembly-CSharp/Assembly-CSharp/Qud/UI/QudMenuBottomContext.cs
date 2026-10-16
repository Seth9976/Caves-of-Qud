using System;
using System.Collections.Generic;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000423 RID: 1059
	public class QudMenuBottomContext : MonoBehaviour
	{
		// Token: 0x06002826 RID: 10278 RVA: 0x000CFF9C File Offset: 0x000CE19C
		public void RefreshButtons()
		{
			while (this.buttons.Count > this.items.Count)
			{
				Component component = this.buttons[this.buttons.Count - 1];
				this.buttons.RemoveAt(this.buttons.Count - 1);
				component.gameObject.DestroyImmediate();
			}
			while (this.items.Count > this.buttons.Count)
			{
				GameObject gameObject = this.buttonPrefab.Instantiate<GameObject>();
				gameObject.transform.SetParent(this.rightBar.transform.parent, false);
				gameObject.transform.SetSiblingIndex(this.rightBar.transform.GetSiblingIndex());
				this.buttons.Add(gameObject.GetComponent<SelectableTextMenuItem>());
			}
			for (int i = 0; i < this.buttons.Count; i++)
			{
				this.buttons[i].data = this.items[i];
				this.buttons[i].controller = this.controller;
				this.buttons[i].Update();
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000D00CB File Offset: 0x000CE2CB
		public void Update()
		{
			this.RefreshButtons();
		}

		// Token: 0x040013F4 RID: 5108
		public GameObject leftBar;

		// Token: 0x040013F5 RID: 5109
		public GameObject rightBar;

		// Token: 0x040013F6 RID: 5110
		public GameObject buttonPrefab;

		// Token: 0x040013F7 RID: 5111
		public List<QudMenuItem> items;

		// Token: 0x040013F8 RID: 5112
		public List<SelectableTextMenuItem> buttons;

		// Token: 0x040013F9 RID: 5113
		public QudBaseMenuController controller;

		// Token: 0x040013FA RID: 5114
		private int _lastHash;
	}
}
