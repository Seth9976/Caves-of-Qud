using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000A2 RID: 162
internal class ScreenAwareTargetedContentFitter : UIBehaviour, ILayoutSelfController, ILayoutController
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000554 RID: 1364 RVA: 0x0002299A File Offset: 0x00020B9A
	// (set) Token: 0x06000555 RID: 1365 RVA: 0x000229A2 File Offset: 0x00020BA2
	public ScreenAwareTargetedContentFitter.FitMode horizontalFit
	{
		get
		{
			return this.m_HorizontalFit;
		}
		set
		{
			if (ScreenAwareTargetedContentFitter.SetPropertyUtility.SetStruct<ScreenAwareTargetedContentFitter.FitMode>(ref this.m_HorizontalFit, value))
			{
				this.SetDirty();
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000556 RID: 1366 RVA: 0x000229B8 File Offset: 0x00020BB8
	// (set) Token: 0x06000557 RID: 1367 RVA: 0x000229C0 File Offset: 0x00020BC0
	public ScreenAwareTargetedContentFitter.FitMode verticalFit
	{
		get
		{
			return this.m_VerticalFit;
		}
		set
		{
			if (ScreenAwareTargetedContentFitter.SetPropertyUtility.SetStruct<ScreenAwareTargetedContentFitter.FitMode>(ref this.m_VerticalFit, value))
			{
				this.SetDirty();
			}
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000558 RID: 1368 RVA: 0x000229D6 File Offset: 0x00020BD6
	private RectTransform rectTransform
	{
		get
		{
			if (this.m_Rect == null)
			{
				this.m_Rect = base.GetComponent<RectTransform>();
			}
			return this.m_Rect;
		}
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x000229F8 File Offset: 0x00020BF8
	protected ScreenAwareTargetedContentFitter()
	{
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00022A16 File Offset: 0x00020C16
	protected override void OnEnable()
	{
		base.OnEnable();
		this.SetDirty();
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00022A24 File Offset: 0x00020C24
	protected override void OnDisable()
	{
		this.m_Tracker.Clear();
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		base.OnDisable();
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00022A42 File Offset: 0x00020C42
	protected override void OnRectTransformDimensionsChange()
	{
		this.SetDirty();
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x00022A4C File Offset: 0x00020C4C
	private void HandleSelfFittingAlongAxis(int axis)
	{
		ScreenAwareTargetedContentFitter.FitMode fitMode = ((axis == 0) ? this.horizontalFit : this.verticalFit);
		if (fitMode == ScreenAwareTargetedContentFitter.FitMode.Unconstrained)
		{
			return;
		}
		this.m_Tracker.Add(this, this.rectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
		float num = ((axis == 0) ? (CanvasSize.width * this.screenWidthMax) : (CanvasSize.height * this.screenHeightMax));
		if (fitMode == ScreenAwareTargetedContentFitter.FitMode.MinSize)
		{
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, Mathf.Min(num, LayoutUtility.GetMinSize(this.target, axis)));
			return;
		}
		this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, Mathf.Min(num, LayoutUtility.GetPreferredSize(this.target, axis)));
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00022AEF File Offset: 0x00020CEF
	public virtual void SetLayoutHorizontal()
	{
		this.m_Tracker.Clear();
		this.HandleSelfFittingAlongAxis(0);
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00022B03 File Offset: 0x00020D03
	public virtual void SetLayoutVertical()
	{
		this.HandleSelfFittingAlongAxis(1);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00022B0C File Offset: 0x00020D0C
	protected void SetDirty()
	{
		if (!this.IsActive())
		{
			return;
		}
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	// Token: 0x04000424 RID: 1060
	[SerializeField]
	protected ScreenAwareTargetedContentFitter.FitMode m_HorizontalFit;

	// Token: 0x04000425 RID: 1061
	[SerializeField]
	protected ScreenAwareTargetedContentFitter.FitMode m_VerticalFit;

	// Token: 0x04000426 RID: 1062
	public float screenHeightMax = 100f;

	// Token: 0x04000427 RID: 1063
	public float screenWidthMax = 100f;

	// Token: 0x04000428 RID: 1064
	[NonSerialized]
	private RectTransform m_Rect;

	// Token: 0x04000429 RID: 1065
	public RectTransform target;

	// Token: 0x0400042A RID: 1066
	private DrivenRectTransformTracker m_Tracker;

	// Token: 0x020015AA RID: 5546
	internal static class SetPropertyUtility
	{
		// Token: 0x0600D5E8 RID: 54760 RVA: 0x004F729C File Offset: 0x004F549C
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600D5E9 RID: 54761 RVA: 0x004F72EB File Offset: 0x004F54EB
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600D5EA RID: 54762 RVA: 0x004F730C File Offset: 0x004F550C
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}

	// Token: 0x020015AB RID: 5547
	public enum FitMode
	{
		// Token: 0x04005727 RID: 22311
		Unconstrained,
		// Token: 0x04005728 RID: 22312
		MinSize,
		// Token: 0x04005729 RID: 22313
		PreferredSize
	}
}
