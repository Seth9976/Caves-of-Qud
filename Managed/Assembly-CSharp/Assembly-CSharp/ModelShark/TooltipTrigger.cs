using System;
using System.Collections.Generic;
using Qud.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModelShark
{
	// Token: 0x02000196 RID: 406
	public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00060C08 File Offset: 0x0005EE08
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00060C10 File Offset: 0x0005EE10
		public Tooltip Tooltip { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00060C19 File Offset: 0x0005EE19
		private float tooltipDelay
		{
			get
			{
				if (!this.CustomDelay)
				{
					return TooltipManager.Instance.tooltipDelay;
				}
				return this.CustomDelayTime;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00060C34 File Offset: 0x0005EE34
		public void Start()
		{
			this.Initialize();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00060C3C File Offset: 0x0005EE3C
		private void Initialize()
		{
			if (this.isInitialized)
			{
				return;
			}
			if (TooltipManager.Instance == null)
			{
				return;
			}
			if (this.tooltipStyle != null)
			{
				if (!TooltipManager.Instance.Tooltips.ContainsKey(this.tooltipStyle))
				{
					TooltipStyle tooltipStyle = global::UnityEngine.Object.Instantiate<TooltipStyle>(this.tooltipStyle);
					tooltipStyle.name = this.tooltipStyle.name;
					tooltipStyle.transform.SetParent(TooltipManager.Instance.TooltipContainer.transform, false);
					Tooltip tooltip = new Tooltip
					{
						GameObject = tooltipStyle.gameObject
					};
					tooltip.Initialize();
					tooltip.Deactivate();
					TooltipManager.Instance.Tooltips.Add(this.tooltipStyle, tooltip);
				}
				this.Tooltip = TooltipManager.Instance.Tooltips[this.tooltipStyle];
			}
			this.isInitialized = true;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00060D18 File Offset: 0x0005EF18
		private void Update()
		{
			if (CursorManager.instance.cursorHidden)
			{
				return;
			}
			if (this.hoverTimer > 0f)
			{
				this.hoverTimer += Time.unscaledDeltaTime;
			}
			if (this.hoverTimer > this.tooltipDelay)
			{
				this.hoverTimer = 0f;
				if (!this.isManuallyTriggered)
				{
					this.StartHover();
				}
			}
			if (this.popupTimer > 0f)
			{
				this.popupTimer += Time.unscaledDeltaTime;
			}
			if (!this.IsDisplayed())
			{
				return;
			}
			if (this.popupTimer > this.popupTime && this.Tooltip != null && !this.Tooltip.StaysOpen)
			{
				this.HidePopup();
			}
			if (Input.mousePresent && this.dismissOnMouseMove && Vector3.Distance(Input.mousePosition, this.lastMousePosition) > 10f)
			{
				this.HidePopup();
				this.lastMousePosition = Input.mousePosition;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00060E00 File Offset: 0x0005F000
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (TooltipManager.Instance.touchSupport)
			{
				return;
			}
			if (this.isRemotelyActivated || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			if (this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.hoverTimer = 0.001f;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00060E50 File Offset: 0x0005F050
		public void OnMouseOver()
		{
			if (this.isMouseOver)
			{
				return;
			}
			if (TooltipManager.Instance.touchSupport)
			{
				return;
			}
			if (this.isRemotelyActivated || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			if (this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.hoverTimer = 0.001f;
			this.isMouseOver = true;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00060EB0 File Offset: 0x0005F0B0
		public void OnMouseDown()
		{
			if (this.isMouseDown)
			{
				return;
			}
			if (!TooltipManager.Instance.touchSupport)
			{
				return;
			}
			if (this.isRemotelyActivated || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			if (this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.hoverTimer = 0.001f;
			this.isMouseDown = true;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00060F0D File Offset: 0x0005F10D
		public void OnMouseExit()
		{
			if (TooltipManager.Instance.touchSupport)
			{
				return;
			}
			this.isMouseOver = false;
			this.StopHover();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00060F2C File Offset: 0x0005F12C
		public void OnPointerDown(PointerEventData eventData)
		{
			if (!TooltipManager.Instance.touchSupport)
			{
				return;
			}
			if (this.isRemotelyActivated || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			if (this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.hoverTimer = 0.001f;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00060F7C File Offset: 0x0005F17C
		public void OnSelect(BaseEventData eventData)
		{
			if (TooltipManager.Instance.touchSupport)
			{
				return;
			}
			if (this.isRemotelyActivated || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			if (this.Tooltip == null)
			{
				return;
			}
			if (this.Tooltip.GameObject == null)
			{
				return;
			}
			if (this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.hoverTimer = 0.001f;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00060FE6 File Offset: 0x0005F1E6
		public void OnPointerExit(PointerEventData eventData)
		{
			if (TooltipManager.Instance.touchSupport)
			{
				return;
			}
			this.StopHover();
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00060FFB File Offset: 0x0005F1FB
		public void OnPointerUp(PointerEventData eventData)
		{
			if (!TooltipManager.Instance.touchSupport)
			{
				return;
			}
			this.StopHover();
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00061010 File Offset: 0x0005F210
		public void OnMouseUp()
		{
			if (!TooltipManager.Instance.touchSupport)
			{
				return;
			}
			this.isMouseDown = false;
			this.StopHover();
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0006102C File Offset: 0x0005F22C
		public void OnDeselect(BaseEventData eventData)
		{
			if (TooltipManager.Instance != null && TooltipManager.Instance.touchSupport)
			{
				return;
			}
			this.StopHover();
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00061050 File Offset: 0x0005F250
		public virtual void StartHover()
		{
			if (this.isManuallyTriggered && this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			if (this.minTextWidth > this.maxTextWidth)
			{
				this.maxTextWidth = this.minTextWidth;
			}
			this.Tooltip.WarmUp();
			this.Tooltip.StaysOpen = this.staysOpen;
			this.Tooltip.NeverRotate = this.neverRotate;
			this.Tooltip.IsBlocking = this.isBlocking;
			TooltipManager.Instance.SetTextAndSize(this);
			base.StartCoroutine(TooltipManager.Instance.Show(this, false));
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000610F0 File Offset: 0x0005F2F0
		public void Deactivate()
		{
			if (this.onHideAction != null)
			{
				this.onHideAction();
				this.onHideAction = null;
			}
			if (TooltipManager.Instance.activeTooltipTriggers.Contains(this))
			{
				TooltipManager.Instance.activeTooltipTriggers.Remove(this);
			}
			if (this.Tooltip != null && this.Tooltip.GameObject != null)
			{
				this.Tooltip.Deactivate();
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00061160 File Offset: 0x0005F360
		public void ForceHideTooltip()
		{
			this.hoverTimer = (this.popupTimer = 0f);
			this.Deactivate();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00061188 File Offset: 0x0005F388
		public void StopHover()
		{
			if (this.Tooltip == null || this.Tooltip.GameObject == null)
			{
				return;
			}
			if (this.isRemotelyActivated || this.Tooltip.StaysOpen)
			{
				return;
			}
			this.hoverTimer = 0f;
			this.Deactivate();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000611D8 File Offset: 0x0005F3D8
		public void HidePopup()
		{
			if (this.Tooltip == null || this.Tooltip.GameObject == null || !this.Tooltip.GameObject.activeInHierarchy)
			{
				return;
			}
			this.popupTimer = 0f;
			this.remoteTrigger = null;
			this.Deactivate();
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0006122C File Offset: 0x0005F42C
		public bool IsDisplayed()
		{
			return this.Tooltip != null && this.Tooltip.GameObject != null && this.Tooltip.GameObject.activeInHierarchy && TooltipManager.Instance.activeTooltipTriggers.Contains(this);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00061278 File Offset: 0x0005F478
		public void ShowManually(bool bForceDisplay = false)
		{
			this.ShowManually(bForceDisplay, Vector2.zero, false, false);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00061290 File Offset: 0x0005F490
		public void ShowManually(bool bForceDisplay, Vector3 pos, bool usePosOverride = false, bool alwaysStayOpen = false)
		{
			if (!bForceDisplay && this.IsDisplayed())
			{
				return;
			}
			if (this.IsDisplayed())
			{
				this.Deactivate();
			}
			this.Initialize();
			if (this.minTextWidth > this.maxTextWidth)
			{
				this.maxTextWidth = this.minTextWidth;
			}
			this.Tooltip.WarmUp();
			this.Tooltip.StaysOpen = this.staysOpen;
			if (alwaysStayOpen)
			{
				this.Tooltip.StaysOpen = true;
			}
			this.Tooltip.NeverRotate = this.neverRotate;
			this.Tooltip.IsBlocking = this.isBlocking;
			this.dismissOnMouseMove = !usePosOverride;
			if (this.staysOpen && this.isManuallyTriggered)
			{
				this.dismissOnMouseMove = false;
			}
			TooltipManager.Instance.SetTextAndSize(this);
			this.lastMousePosition = Input.mousePosition;
			if (usePosOverride)
			{
				TooltipManager.Instance.positionOverride = pos;
			}
			base.StartCoroutine(TooltipManager.Instance.Show(this, usePosOverride));
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00061380 File Offset: 0x0005F580
		public void Popup(float duration, GameObject triggeredBy)
		{
			if (this.popupTimer > 0f || TooltipManager.Instance.BlockingTooltip != null)
			{
				return;
			}
			this.Initialize();
			this.remoteTrigger = triggeredBy;
			if (this.minTextWidth > this.maxTextWidth)
			{
				this.maxTextWidth = this.minTextWidth;
			}
			this.Tooltip.WarmUp();
			this.Tooltip.StaysOpen = this.staysOpen;
			this.Tooltip.NeverRotate = this.neverRotate;
			this.Tooltip.IsBlocking = this.isBlocking;
			TooltipManager.Instance.SetTextAndSize(this);
			base.StartCoroutine(TooltipManager.Instance.Show(this, false));
			this.popupTimer = 0.001f;
			this.popupTime = duration;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0006143C File Offset: 0x0005F63C
		public void SetText(string parameterName, string text)
		{
			if (this.parameterizedTextFields == null)
			{
				this.parameterizedTextFields = new List<ParameterizedTextField>();
			}
			bool flag = false;
			for (int i = 0; i < this.parameterizedTextFields.Count; i++)
			{
				if (this.parameterizedTextFields[i].name == parameterName)
				{
					this.parameterizedTextFields[i].value = text;
					flag = true;
				}
			}
			if (!flag)
			{
				string textFieldDelimiter = TooltipManager.Instance.textFieldDelimiter;
				this.parameterizedTextFields.Add(new ParameterizedTextField
				{
					name = parameterName,
					placeholder = string.Format("{0}{1}{0}", textFieldDelimiter, parameterName),
					value = text
				});
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000614E0 File Offset: 0x0005F6E0
		public void SetImage(string parameterName, Sprite sprite)
		{
			if (this.dynamicImageFields == null)
			{
				this.dynamicImageFields = new List<DynamicImageField>();
			}
			bool flag = false;
			for (int i = 0; i < this.dynamicImageFields.Count; i++)
			{
				if (this.dynamicImageFields[i].name == parameterName)
				{
					this.dynamicImageFields[i].replacementSprite = sprite;
					flag = true;
				}
			}
			if (!flag)
			{
				this.dynamicImageFields.Add(new DynamicImageField
				{
					name = parameterName,
					placeholderSprite = null,
					replacementSprite = sprite
				});
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0006156D File Offset: 0x0005F76D
		public void TurnSectionOn(string parameterName)
		{
			this.ToggleSection(parameterName, true);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00061577 File Offset: 0x0005F777
		public void TurnSectionOff(string parameterName)
		{
			this.ToggleSection(parameterName, false);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00061584 File Offset: 0x0005F784
		public void ToggleSection(string parameterName, bool isOn)
		{
			if (this.dynamicSectionFields == null)
			{
				this.dynamicSectionFields = new List<DynamicSectionField>();
			}
			bool flag = false;
			for (int i = 0; i < this.dynamicSectionFields.Count; i++)
			{
				if (this.dynamicSectionFields[i].name == parameterName)
				{
					this.dynamicSectionFields[i].isOn = isOn;
					flag = true;
				}
			}
			if (!flag)
			{
				this.dynamicSectionFields.Add(new DynamicSectionField
				{
					name = parameterName,
					isOn = isOn
				});
			}
		}

		// Token: 0x04000A97 RID: 2711
		public TooltipStyle tooltipStyle;

		// Token: 0x04000A98 RID: 2712
		public List<ParameterizedTextField> parameterizedTextFields;

		// Token: 0x04000A99 RID: 2713
		public List<DynamicImageField> dynamicImageFields;

		// Token: 0x04000A9A RID: 2714
		public List<DynamicSectionField> dynamicSectionFields;

		// Token: 0x04000A9B RID: 2715
		public object AdditionalData;

		// Token: 0x04000A9C RID: 2716
		public bool isRemotelyActivated;

		// Token: 0x04000A9D RID: 2717
		[HideInInspector]
		public GameObject remoteTrigger;

		// Token: 0x04000A9E RID: 2718
		public bool isManuallyTriggered;

		// Token: 0x04000AA0 RID: 2720
		[Tooltip("Controls the color and fade amount of the tooltip background.")]
		public Color backgroundTint = Color.white;

		// Token: 0x04000AA1 RID: 2721
		public TipPosition tipPosition;

		// Token: 0x04000AA2 RID: 2722
		public int minTextWidth = 100;

		// Token: 0x04000AA3 RID: 2723
		public int maxTextWidth = 200;

		// Token: 0x04000AA4 RID: 2724
		[HideInInspector]
		[Tooltip("Once open, this tooltip will stay open until something manually closes it. No other tooltips that use this style will open, since there is only one instance of a tooltip style at a time.")]
		public bool staysOpen;

		// Token: 0x04000AA5 RID: 2725
		[HideInInspector]
		[Tooltip("If true, this tooltip will not be angled/rotated along with other tooltips (see MatchRotationTo on TooltipManager).")]
		public bool neverRotate;

		// Token: 0x04000AA6 RID: 2726
		[HideInInspector]
		[Tooltip("While open, this tooltip will prevent any other tooltips from triggering.")]
		public bool isBlocking;

		// Token: 0x04000AA7 RID: 2727
		private float hoverTimer;

		// Token: 0x04000AA8 RID: 2728
		private float popupTimer;

		// Token: 0x04000AA9 RID: 2729
		public bool CustomDelay;

		// Token: 0x04000AAA RID: 2730
		public float CustomDelayTime = 1f;

		// Token: 0x04000AAB RID: 2731
		private float popupTime = 2f;

		// Token: 0x04000AAC RID: 2732
		private bool isInitialized;

		// Token: 0x04000AAD RID: 2733
		private bool isMouseOver;

		// Token: 0x04000AAE RID: 2734
		private bool isMouseDown;

		// Token: 0x04000AAF RID: 2735
		public bool dismissOnMouseMove;

		// Token: 0x04000AB0 RID: 2736
		public Vector3 lastMousePosition = new Vector2(0f, 0f);

		// Token: 0x04000AB1 RID: 2737
		public Action onHideAction;
	}
}
