using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x0200019C RID: 412
	public class TooltipManager : MonoBehaviour
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0006299C File Offset: 0x00060B9C
		public static TooltipManager Instance
		{
			get
			{
				if (TooltipManager.instance == null)
				{
					TooltipManager.instance = global::UnityEngine.Object.FindFirstObjectByType<TooltipManager>();
				}
				if (TooltipManager.instance == null)
				{
					return null;
				}
				if (!TooltipManager.instance.isInitialized)
				{
					TooltipManager.instance.Initialize();
				}
				return TooltipManager.instance;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000629EA File Offset: 0x00060BEA
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000629F2 File Offset: 0x00060BF2
		public Canvas RootCanvas { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000629FB File Offset: 0x00060BFB
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x00062A03 File Offset: 0x00060C03
		public GameObject TooltipContainer { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00062A0C File Offset: 0x00060C0C
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00062A14 File Offset: 0x00060C14
		public GameObject TooltipContainerNoAngle { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00062A1D File Offset: 0x00060C1D
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00062A25 File Offset: 0x00060C25
		public Dictionary<TooltipStyle, Tooltip> Tooltips { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00062A2E File Offset: 0x00060C2E
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00062A36 File Offset: 0x00060C36
		public Tooltip BlockingTooltip { get; set; }

		// Token: 0x06000F95 RID: 3989 RVA: 0x00062A3F File Offset: 0x00060C3F
		private void Awake()
		{
			TooltipManager.instance = this;
			if (!this.isInitialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00062A58 File Offset: 0x00060C58
		private void Initialize()
		{
			if (this.isInitialized)
			{
				return;
			}
			this.RootCanvas = CanvasHelper.GetRootCanvas();
			if (this.GuiCanvas == null)
			{
				this.GuiCanvas = this.RootCanvas;
			}
			if (this.guiCamera == null)
			{
				this.guiCamera = Camera.main;
			}
			this.Tooltips = new Dictionary<TooltipStyle, Tooltip>();
			this.TooltipContainer = this.CreateTooltipContainer("Tooltip Container");
			this.TooltipContainerNoAngle = this.CreateTooltipContainer("Tooltip Container (No Angle)");
			this.ResetTooltipRotation();
			this.isInitialized = true;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00062AE8 File Offset: 0x00060CE8
		private GameObject CreateTooltipContainer(string containerName)
		{
			GameObject gameObject = GameObject.Find(containerName);
			if (gameObject == null)
			{
				gameObject = new GameObject(containerName);
				gameObject.transform.SetParent(this.GuiCanvas.transform, false);
				RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
				rectTransform.anchorMin = (rectTransform.offsetMin = (rectTransform.offsetMax = (rectTransform.anchoredPosition = Vector2.zero)));
				rectTransform.anchorMax = (rectTransform.localScale = Vector3.one);
				if (this.tooltipContainerPosition == null)
				{
					gameObject.transform.SetAsLastSibling();
				}
				else
				{
					gameObject.transform.SetSiblingIndex(this.tooltipContainerPosition.transform.GetSiblingIndex());
				}
			}
			return gameObject;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00062BA4 File Offset: 0x00060DA4
		public void ResetTooltipRotation()
		{
			this.TooltipContainer.transform.rotation = ((this.matchRotationTo != null) ? this.matchRotationTo.transform.rotation : this.GuiCanvas.transform.rotation);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00062BF4 File Offset: 0x00060DF4
		public void SetTextAndSize(TooltipTrigger trigger)
		{
			Tooltip tooltip = trigger.Tooltip;
			if (tooltip == null || trigger.parameterizedTextFields == null)
			{
				return;
			}
			if ((tooltip.TextFields == null || tooltip.TextFields.Count == 0) && (tooltip.TMPFields == null || tooltip.TMPFields.Count == 0))
			{
				return;
			}
			LayoutElement mainTextContainer = tooltip.TooltipStyle.mainTextContainer;
			if (mainTextContainer == null)
			{
				Debug.LogWarning(string.Format("No main text container defined on tooltip style \"{0}\". Note: This LayoutElement is needed in order to resize text appropriately.", trigger.Tooltip.GameObject.name));
			}
			else
			{
				mainTextContainer.preferredWidth = (float)trigger.minTextWidth;
			}
			for (int i = 0; i < tooltip.TextFields.Count; i++)
			{
				Text text = tooltip.TextFields[i].Text;
				if (text.text.Length >= 3)
				{
					for (int j = 0; j < trigger.parameterizedTextFields.Count; j++)
					{
						if (!string.IsNullOrEmpty(trigger.parameterizedTextFields[j].value))
						{
							text.text = text.text.Replace(trigger.parameterizedTextFields[j].placeholder, trigger.parameterizedTextFields[j].value);
							text.gameObject.SetActive(true);
						}
						else
						{
							text.gameObject.SetActive(false);
						}
					}
					if (mainTextContainer != null)
					{
						if (text.preferredWidth > (float)trigger.maxTextWidth)
						{
							mainTextContainer.preferredWidth = (float)trigger.maxTextWidth;
						}
						else if (text.preferredWidth > (float)trigger.minTextWidth && text.preferredWidth > mainTextContainer.preferredWidth)
						{
							mainTextContainer.preferredWidth = text.preferredWidth;
						}
					}
				}
			}
			for (int k = 0; k < tooltip.TMPFields.Count; k++)
			{
				TextMeshProUGUI text2 = tooltip.TMPFields[k].Text;
				if (text2.text.Length >= 3)
				{
					for (int l = 0; l < trigger.parameterizedTextFields.Count; l++)
					{
						text2.text = text2.text.Replace(trigger.parameterizedTextFields[l].placeholder, trigger.parameterizedTextFields[l].value);
					}
					if (string.IsNullOrWhiteSpace(text2.text))
					{
						text2.gameObject.SetActive(false);
					}
					else
					{
						text2.gameObject.SetActive(true);
					}
					if (mainTextContainer != null)
					{
						if (text2.preferredWidth > (float)trigger.maxTextWidth)
						{
							mainTextContainer.preferredWidth = (float)trigger.maxTextWidth;
						}
						else if (text2.preferredWidth > (float)trigger.minTextWidth && text2.preferredWidth > mainTextContainer.preferredWidth)
						{
							mainTextContainer.preferredWidth = text2.preferredWidth;
						}
					}
				}
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00062EA8 File Offset: 0x000610A8
		public IEnumerator Show(TooltipTrigger trigger, bool bUsePositionOverride = false)
		{
			if (trigger.tooltipStyle == null)
			{
				Debug.LogWarning("TooltipTrigger \"" + trigger.name + "\" has no associated TooltipStyle. Cannot show tooltip.");
				yield break;
			}
			if (!this.activeTooltipTriggers.Contains(trigger))
			{
				this.activeTooltipTriggers.Add(trigger);
			}
			Tooltip tooltip = trigger.Tooltip;
			Image backgroundImage = tooltip.BackgroundImage;
			if (tooltip.NeverRotate)
			{
				tooltip.GameObject.transform.SetParent(this.TooltipContainerNoAngle.transform, false);
			}
			if (trigger.dynamicImageFields != null)
			{
				for (int i = 0; i < trigger.dynamicImageFields.Count; i++)
				{
					for (int j = 0; j < tooltip.ImageFields.Count; j++)
					{
						if (tooltip.ImageFields[j].Name == trigger.dynamicImageFields[i].name)
						{
							if (trigger.dynamicImageFields[i].replacementSprite == null)
							{
								tooltip.ImageFields[j].Image.sprite = tooltip.ImageFields[j].Original;
							}
							else
							{
								tooltip.ImageFields[j].Image.sprite = trigger.dynamicImageFields[i].replacementSprite;
							}
						}
					}
				}
			}
			if (trigger.dynamicSectionFields != null)
			{
				for (int k = 0; k < trigger.dynamicSectionFields.Count; k++)
				{
					for (int l = 0; l < tooltip.SectionFields.Count; l++)
					{
						if (tooltip.SectionFields[l].Name == trigger.dynamicSectionFields[k].name)
						{
							tooltip.SectionFields[l].GameObject.SetActive(trigger.dynamicSectionFields[k].isOn);
						}
					}
				}
			}
			if (tooltip.SetupHelpers != null)
			{
				for (int m = 0; m < tooltip.SetupHelpers.Count; m++)
				{
					tooltip.SetupHelpers[m].BeforeShow(trigger, tooltip);
				}
			}
			Vector2 vector = this.positionOverride;
			Canvas.ForceUpdateCanvases();
			this.TooltipContainer.transform.SetParent(this.GuiCanvas.transform, false);
			if (bUsePositionOverride)
			{
				tooltip.SetPositionManual(vector, TipPosition.TopRightCorner, trigger.tooltipStyle, tooltip.BackgroundImage, tooltip.RectTransform, this.GuiCanvas, this.guiCamera);
			}
			else
			{
				tooltip.SetPosition(trigger, this.GuiCanvas, this.guiCamera);
			}
			backgroundImage.color = trigger.backgroundTint;
			if (tooltip.IsBlocking)
			{
				this.BlockingTooltip = tooltip;
			}
			tooltip.Display(this.fadeDuration);
			if (Input.mousePresent)
			{
				trigger.lastMousePosition = Input.mousePosition;
			}
			yield break;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00062EC8 File Offset: 0x000610C8
		public void Update()
		{
			for (int i = 0; i < this.activeTooltipTriggers.Count; i++)
			{
				if (!this.activeTooltipTriggers[i].gameObject.activeInHierarchy || ((!this.activeTooltipTriggers[i].isManuallyTriggered || !this.activeTooltipTriggers[i].staysOpen) && !ControlManager.LastInputFromMouse))
				{
					this.activeTooltipTriggers[i].ForceHideTooltip();
				}
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00062F44 File Offset: 0x00061144
		public void CloseAll()
		{
			TooltipTrigger[] array = global::UnityEngine.Object.FindObjectsByType<TooltipTrigger>(FindObjectsSortMode.None);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ForceHideTooltip();
			}
		}

		// Token: 0x04000AC3 RID: 2755
		[Tooltip("If you have multiple cameras in your scene, this is the one that will be used by ProTips.")]
		public Camera guiCamera;

		// Token: 0x04000AC4 RID: 2756
		[Tooltip("The RectTransform to match if you have an angled UI and you want tooltips to match the RectTransform's angle.")]
		public RectTransform matchRotationTo;

		// Token: 0x04000AC5 RID: 2757
		[Tooltip("When enabled, tooltips will be triggered by pressing-and-holding, not hovering over. They will be dismissed by releasing the hold, instead of hover off.")]
		public bool touchSupport;

		// Token: 0x04000AC6 RID: 2758
		[Tooltip("How long to wait before beginning to display a tooltip.")]
		public float tooltipDelay = 0.33f;

		// Token: 0x04000AC7 RID: 2759
		[Tooltip("How long the tooltip fade-in transition will last. Set to 0 for increased performance.")]
		public float fadeDuration = 0.2f;

		// Token: 0x04000AC8 RID: 2760
		[Tooltip("For tooltip prefabs, the start/end character that signifies a dynamic field.")]
		public string textFieldDelimiter = "%";

		// Token: 0x04000AC9 RID: 2761
		[Tooltip("Determines whether tooltips are repositioned when they would flow off the canvas. Disable for increased performance.")]
		public bool overflowProtection = true;

		// Token: 0x04000ACA RID: 2762
		public GameObject tooltipContainerPosition;

		// Token: 0x04000ACB RID: 2763
		public Canvas GuiCanvas;

		// Token: 0x04000ACC RID: 2764
		private static TooltipManager instance;

		// Token: 0x04000ACD RID: 2765
		private bool isInitialized;

		// Token: 0x04000AD3 RID: 2771
		public Vector3 positionOverride;

		// Token: 0x04000AD4 RID: 2772
		public List<TooltipTrigger> activeTooltipTriggers = new List<TooltipTrigger>();
	}
}
