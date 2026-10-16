using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000413 RID: 1043
	[AddComponentMenu("Layout/Content Size Fitter using Parent", 142)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeFitParent : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000CD814 File Offset: 0x000CBA14
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x000CD81C File Offset: 0x000CBA1C
		public ContentSizeFitParent.FitMode HorizontalFit
		{
			get
			{
				return this.horizontalFit;
			}
			set
			{
				if (this.horizontalFit == value)
				{
					return;
				}
				this.horizontalFit = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000CD835 File Offset: 0x000CBA35
		// (set) Token: 0x0600279D RID: 10141 RVA: 0x000CD83D File Offset: 0x000CBA3D
		public ContentSizeFitParent.FitMode VerticalFit
		{
			get
			{
				return this.verticalFit;
			}
			set
			{
				if (this.verticalFit == value)
				{
					return;
				}
				this.verticalFit = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000CD856 File Offset: 0x000CBA56
		private RectTransform RectTransform
		{
			get
			{
				if (this.rectTransform == null)
				{
					this.rectTransform = base.GetComponent<RectTransform>();
				}
				return this.rectTransform;
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000CD878 File Offset: 0x000CBA78
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000CD886 File Offset: 0x000CBA86
		protected override void OnDisable()
		{
			this.tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.RectTransform);
			base.OnDisable();
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000CD8A4 File Offset: 0x000CBAA4
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000CD8AC File Offset: 0x000CBAAC
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitParent.FitMode fitMode = ((axis == 0) ? this.HorizontalFit : this.VerticalFit);
			if (fitMode == ContentSizeFitParent.FitMode.Unconstrained)
			{
				this.tracker.Add(this, this.RectTransform, DrivenTransformProperties.None);
				return;
			}
			this.tracker.Add(this, this.RectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			switch (fitMode)
			{
			case ContentSizeFitParent.FitMode.MinSize:
				this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.rectTransform, axis));
				return;
			case ContentSizeFitParent.FitMode.PreferredSize:
				this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.rectTransform, axis));
				return;
			case ContentSizeFitParent.FitMode.PreferredLimitParent:
				this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, Mathf.Clamp(LayoutUtility.GetPreferredSize(this.rectTransform, axis), 0f, (axis == 0) ? rectTransform.rect.width : rectTransform.rect.height));
				return;
			case ContentSizeFitParent.FitMode.PreferredLimitArbitraryTarget:
				this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, Mathf.Clamp(LayoutUtility.GetPreferredSize(this.rectTransform, axis), 0f, (axis == 0) ? this.arbitraryTarget.rect.width : this.arbitraryTarget.rect.height));
				return;
			default:
				return;
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000CD9F0 File Offset: 0x000CBBF0
		public virtual void SetLayoutHorizontal()
		{
			this.tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000CDA04 File Offset: 0x000CBC04
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000CDA0D File Offset: 0x000CBC0D
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.RectTransform);
		}

		// Token: 0x04001390 RID: 5008
		public RectTransform arbitraryTarget;

		// Token: 0x04001391 RID: 5009
		[SerializeField]
		protected ContentSizeFitParent.FitMode horizontalFit;

		// Token: 0x04001392 RID: 5010
		[SerializeField]
		protected ContentSizeFitParent.FitMode verticalFit;

		// Token: 0x04001393 RID: 5011
		[NonSerialized]
		private RectTransform rectTransform;

		// Token: 0x04001394 RID: 5012
		private DrivenRectTransformTracker tracker;

		// Token: 0x020017D8 RID: 6104
		public enum FitMode
		{
			// Token: 0x04005FFA RID: 24570
			Unconstrained,
			// Token: 0x04005FFB RID: 24571
			MinSize,
			// Token: 0x04005FFC RID: 24572
			PreferredSize,
			// Token: 0x04005FFD RID: 24573
			PreferredLimitParent,
			// Token: 0x04005FFE RID: 24574
			PreferredLimitArbitraryTarget
		}
	}
}
