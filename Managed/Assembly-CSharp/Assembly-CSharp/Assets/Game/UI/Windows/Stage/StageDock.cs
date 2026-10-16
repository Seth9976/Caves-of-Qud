using System;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;

namespace Assets.Game.UI.Windows.Stage
{
	// Token: 0x020003E7 RID: 999
	public class StageDock : MonoBehaviour
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000BE545 File Offset: 0x000BC745
		public float calculatedWidth
		{
			get
			{
				return Math.Min(this.maxWidth, Math.Max(this.preferredWidth, this.minWidth));
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000BE563 File Offset: 0x000BC763
		public float topPadding
		{
			get
			{
				return 90f;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000BE56A File Offset: 0x000BC76A
		public float bottomPadding
		{
			get
			{
				return 90f;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x000BE571 File Offset: 0x000BC771
		public static StageDock instance
		{
			get
			{
				return StageDock._instance;
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000BE578 File Offset: 0x000BC778
		public void Awake()
		{
			StageDock._instance = this;
			this.preferredWidth = PlayerPrefs.GetFloat("DockPreferredWith", this.preferredWidth);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000BE598 File Offset: 0x000BC798
		private float CalculateNearbyItemsHeight(float totalDockSize, float alreadyUsed)
		{
			float num = (this.nearbyItemsContent.transform as RectTransform).rect.height;
			num = Mathf.Max(num, 0f);
			float num2 = Mathf.Max(totalDockSize / 3f, 200f);
			num = Mathf.Min(totalDockSize - alreadyUsed - num2, num);
			if (num != this._lastNearbyItemsHeight)
			{
				MessageLogWindow._needsScrollToBottom = 2;
				this._lastNearbyItemsHeight = num;
			}
			return num;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000BE604 File Offset: 0x000BC804
		public void LateUpdate()
		{
			bool flag = false;
			if (Options.ShiftHidesSidebar && Keyboard.bShift)
			{
				GameManager instance = GameManager.Instance;
				if (((instance != null) ? instance.CurrentGameView : null) == "Stage")
				{
					flag = true;
				}
			}
			if (this.ours == null)
			{
				this.ours = base.GetComponent<RectTransform>();
			}
			if (!this.backgroundImage.isActiveAndEnabled && MessageLogWindow.Shown)
			{
				this.backgroundImage.enabled = true;
			}
			if (this.backgroundImage.color.a != Options.DockOpacity)
			{
				this.backgroundImage.color = new Color(this.backgroundImage.color.r, this.backgroundImage.color.g, this.backgroundImage.color.b, Options.DockOpacity);
				this.horizontalDragHandleBackground.color = new Color(this.horizontalDragHandleBackground.color.r, this.horizontalDragHandleBackground.color.g, this.horizontalDragHandleBackground.color.b, Options.DockOpacity);
			}
			bool flag2 = GameManager.Instance.DockMovable == 1;
			if (GameManager.Instance.DockMovable == 3)
			{
				flag2 = GameManager.Instance.currentSidebarPosition == GameManager.PreferredSidebarPosition.Left;
			}
			float num = 16f;
			float num2 = 0f;
			float num3 = this.ours.rect.width - num + num2;
			float num4 = 0f;
			if (GameManager.Instance.DockMovable > 0 && GameManager.MainCameraLetterbox && MessageLogWindow.Shown && !flag)
			{
				if (!flag2)
				{
					num4 = num + num2;
				}
				if (!this.content.isActiveAndEnabled)
				{
					this.content.enabled = true;
				}
				if (this.ours.sizeDelta.x != this.calculatedWidth)
				{
					this.ours.sizeDelta = new Vector2(this.calculatedWidth, this.ours.sizeDelta.y);
				}
				float height = this.ours.rect.height;
				float num5 = 0f;
				if (Options.OverlayMinimap)
				{
					float num6 = 128f;
					if (this.ours.rect.height < 500f)
					{
						num6 = 64f;
					}
					float num7 = num3;
					RectTransform rectTransform = this.minimapPane.content.transform as RectTransform;
					rectTransform.sizeDelta = new Vector2(num7, num6);
					rectTransform.anchoredPosition = new Vector2(num4, -num5);
					rectTransform.anchorMin = new Vector2(0f, 1f);
					rectTransform.anchorMax = new Vector2(0f, 1f);
					num5 += num6;
					this.minimapPane.contentCanvas.enabled = true;
					(this.minimapManager.transform as RectTransform).CopyFrom(this.minimapPane.content.transform as RectTransform);
				}
				else if (this.minimapPane.contentCanvas.enabled)
				{
					this.minimapPane.contentCanvas.enabled = false;
				}
				if (Options.OverlayNearbyObjects)
				{
					float num8 = this.CalculateNearbyItemsHeight(this.ours.rect.height, num5) + 24f;
					RectTransform rectTransform2 = this.itemsPane.content.transform as RectTransform;
					rectTransform2.sizeDelta = new Vector2(num3, num8);
					rectTransform2.anchoredPosition = new Vector2(num4, -num5);
					rectTransform2.anchorMin = new Vector2(0f, 1f);
					rectTransform2.anchorMax = new Vector2(0f, 1f);
					num5 += num8;
					this.itemsPane.contentCanvas.enabled = true;
					(this.nearbyItemsManager.transform as RectTransform).CopyFrom(this.itemsPane.content.transform as RectTransform);
				}
				else if (this.itemsPane.contentCanvas.enabled)
				{
					this.itemsPane.contentCanvas.enabled = false;
				}
				this.messagelogPane.GetComponent<RectTransform>().sizeDelta = new Vector2(num3, this.ours.rect.height - num5);
				this.messagelogPane.GetComponent<RectTransform>().anchoredPosition = new Vector2(num4, -num5);
				if (flag2)
				{
					this.ours.anchorMin = new Vector2(0f, 0f);
					this.ours.anchorMax = new Vector2(0f, 1f);
					this.ours.pivot = new Vector2(0f, 0f);
					(this.safeArea.transform as RectTransform).anchoredPosition = new Vector2(this.ours.rect.width, -this.topPadding);
					(this.safeArea.transform as RectTransform).sizeDelta = new Vector2(this.stage.rect.width - this.ours.rect.width, this.stage.rect.height - this.topPadding - this.bottomPadding);
					(this.horizontalDragHandle.transform as RectTransform).anchoredPosition = new Vector2(this.ours.rect.width - num - num2, 0f);
					(this.horizontalDragHandle.transform as RectTransform).sizeDelta = new Vector2(num, this.ours.rect.height);
				}
				else
				{
					this.ours.anchorMin = new Vector2(1f, 0f);
					this.ours.anchorMax = new Vector2(1f, 1f);
					this.ours.pivot = new Vector2(1f, 0f);
					(this.safeArea.transform as RectTransform).anchoredPosition = new Vector2(0f, -this.topPadding);
					(this.safeArea.transform as RectTransform).sizeDelta = new Vector2(this.stage.rect.width - this.ours.rect.width, this.stage.rect.height - this.topPadding - this.bottomPadding);
					(this.horizontalDragHandle.transform as RectTransform).anchoredPosition = new Vector2(num2, 0f);
					(this.horizontalDragHandle.transform as RectTransform).sizeDelta = new Vector2(num, this.ours.rect.height);
				}
				if (GameManager.Instance.DockMovable == 3)
				{
					(this.safeArea.transform as RectTransform).anchoredPosition = new Vector2(0f, -this.topPadding);
					(this.safeArea.transform as RectTransform).sizeDelta = new Vector2(this.stage.rect.width, this.stage.rect.height - this.topPadding - this.bottomPadding);
				}
				if (!this.LastShown)
				{
					GameManager.Instance.RefreshLayout(false);
					GameManager.MainCameraLetterbox.OnUpdate();
				}
				this.LastShown = true;
				return;
			}
			(this.safeArea.transform as RectTransform).anchoredPosition = new Vector2(0f, -this.topPadding);
			(this.safeArea.transform as RectTransform).sizeDelta = new Vector2(this.stage.rect.width, this.stage.rect.height - this.topPadding - this.bottomPadding);
			if (this.content.isActiveAndEnabled)
			{
				this.content.enabled = false;
			}
			if (this.backgroundImage.isActiveAndEnabled)
			{
				this.backgroundImage.enabled = false;
			}
			if (this.LastShown)
			{
				GameManager.Instance.RefreshLayout(false);
				GameManager.MainCameraLetterbox.OnUpdate();
			}
			this.LastShown = false;
		}

		// Token: 0x040012B4 RID: 4788
		public Canvas content;

		// Token: 0x040012B5 RID: 4789
		public NearbyItemsWindow itemsPane;

		// Token: 0x040012B6 RID: 4790
		public MinimapWindow minimapPane;

		// Token: 0x040012B7 RID: 4791
		public RectTransform minimapManager;

		// Token: 0x040012B8 RID: 4792
		public RectTransform nearbyItemsManager;

		// Token: 0x040012B9 RID: 4793
		public GameObject messagelogPane;

		// Token: 0x040012BA RID: 4794
		public GameObject horizontalDragHandle;

		// Token: 0x040012BB RID: 4795
		public Image horizontalDragHandleBackground;

		// Token: 0x040012BC RID: 4796
		public RectTransform safeArea;

		// Token: 0x040012BD RID: 4797
		public RectTransform stage;

		// Token: 0x040012BE RID: 4798
		public Image backgroundImage;

		// Token: 0x040012BF RID: 4799
		public ContentSizeFitter nearbyItemsContent;

		// Token: 0x040012C0 RID: 4800
		public float minWidth = 300f;

		// Token: 0x040012C1 RID: 4801
		public float preferredWidth = 500f;

		// Token: 0x040012C2 RID: 4802
		public float maxWidth = 633.6f;

		// Token: 0x040012C3 RID: 4803
		public RectTransform ours;

		// Token: 0x040012C4 RID: 4804
		private static StageDock _instance;

		// Token: 0x040012C5 RID: 4805
		private float _lastNearbyItemsHeight;

		// Token: 0x040012C6 RID: 4806
		private bool LastShown;
	}
}
