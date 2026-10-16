using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001EE RID: 494
	public class MenuBarButton : MonoBehaviour
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0006A1F7 File Offset: 0x000683F7
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x0006A1FF File Offset: 0x000683FF
		public string Text { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0006A208 File Offset: 0x00068408
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x0006A210 File Offset: 0x00068410
		public Menu Menu { get; internal set; }

		// Token: 0x060011AB RID: 4523 RVA: 0x0006A219 File Offset: 0x00068419
		public MenuBarButton setText(string text)
		{
			this.Text = text;
			base.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;
			return this;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0006A240 File Offset: 0x00068440
		public void onMouseEnter()
		{
			DialogBase topmost = DialogBase.getTopmost();
			if (topmost is Menu)
			{
				((Menu)topmost).cancel();
				this.onClick();
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0006A26C File Offset: 0x0006846C
		public void Start()
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.onClick));
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0006A28A File Offset: 0x0006848A
		private void onClick()
		{
			this.Menu.gameObject.SetActive(true);
			this.Menu.show(base.GetComponent<RectTransform>());
		}
	}
}
