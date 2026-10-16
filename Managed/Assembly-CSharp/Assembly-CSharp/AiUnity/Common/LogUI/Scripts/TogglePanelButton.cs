using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002D0 RID: 720
	public class TogglePanelButton : MonoBehaviour
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x000802EE File Offset: 0x0007E4EE
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x000802F6 File Offset: 0x0007E4F6
		public Button Button { get; set; }

		// Token: 0x060018E1 RID: 6369 RVA: 0x000802FF File Offset: 0x0007E4FF
		public void TogglePanel()
		{
			this.TogglePanel(!this.panelOpen, true);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00080311 File Offset: 0x0007E511
		public void TogglePanel(bool panelOpen, bool animate = true)
		{
			this.panelOpen = panelOpen;
			this.buttonImage.overrideSprite = (this.panelOpen ? this.pressedSprite : null);
			base.StartCoroutine(this.TogglePanelAnimate(animate));
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00080344 File Offset: 0x0007E544
		public IEnumerator TogglePanelAnimate(bool animate = true)
		{
			float num = (this.panelOpen ? this.openHeight : 0f);
			this.MenuPanel.SetActive(true);
			yield return base.StartCoroutine(this.AnimateGameConsole(num, animate));
			this.MenuPanel.SetActive(this.panelOpen);
			yield break;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0008035A File Offset: 0x0007E55A
		private IEnumerator AnimateGameConsole(float targetHeight, bool animate)
		{
			float timeToLerp = 1f;
			float timeLerped = (animate ? 0f : timeToLerp);
			float startHeight = this.MenuPanelLayoutElement.preferredHeight;
			while (timeLerped <= timeToLerp)
			{
				timeLerped += Time.deltaTime;
				this.MenuPanelLayoutElement.preferredHeight = Mathf.Lerp(startHeight, targetHeight, timeLerped / timeToLerp);
				if (animate)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00080378 File Offset: 0x0007E578
		private void Start()
		{
			this.MenuPanelLayoutElement = this.MenuPanel.GetComponent<LayoutElement>();
			this.openHeight = this.MenuPanelLayoutElement.preferredHeight;
			this.Button = base.GetComponent<Button>();
			this.Button.onClick.AddListener(new UnityAction(this.TogglePanel));
			this.buttonImage = base.GetComponent<Image>();
			this.panelOpen = true;
			this.TogglePanel(this.panelOpen, false);
		}

		// Token: 0x04000EC6 RID: 3782
		public bool panelOpen;

		// Token: 0x04000EC7 RID: 3783
		public GameObject MenuPanel;

		// Token: 0x04000EC8 RID: 3784
		private Sprite normalSprite;

		// Token: 0x04000EC9 RID: 3785
		public Sprite pressedSprite;

		// Token: 0x04000ECA RID: 3786
		private LayoutElement MenuPanelLayoutElement;

		// Token: 0x04000ECB RID: 3787
		private float openHeight;

		// Token: 0x04000ECC RID: 3788
		private Image buttonImage;
	}
}
