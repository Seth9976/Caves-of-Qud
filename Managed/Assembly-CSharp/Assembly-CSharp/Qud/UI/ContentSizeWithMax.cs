using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x02000414 RID: 1044
	[AddComponentMenu("Layout/Content Size Fitter With Max", 141)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeWithMax : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000CDA2B File Offset: 0x000CBC2B
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x000CDA33 File Offset: 0x000CBC33
		public ContentSizeWithMax.FitMode HorizontalFit
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x000CDA4C File Offset: 0x000CBC4C
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x000CDA54 File Offset: 0x000CBC54
		public ContentSizeWithMax.FitMode VerticalFit
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

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x000CDA6D File Offset: 0x000CBC6D
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

		// Token: 0x060027AC RID: 10156 RVA: 0x000CDA8F File Offset: 0x000CBC8F
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000CDA9D File Offset: 0x000CBC9D
		protected override void OnDisable()
		{
			this.tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.RectTransform);
			base.OnDisable();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000CDABB File Offset: 0x000CBCBB
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000CDAC4 File Offset: 0x000CBCC4
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeWithMax.FitMode fitMode = ((axis == 0) ? this.HorizontalFit : this.VerticalFit);
			if (fitMode == ContentSizeWithMax.FitMode.Unconstrained)
			{
				this.tracker.Add(this, this.RectTransform, DrivenTransformProperties.None);
				return;
			}
			this.tracker.Add(this, this.RectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
			if (fitMode == ContentSizeWithMax.FitMode.MinSize)
			{
				this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.rectTransform, axis));
				return;
			}
			if (fitMode != ContentSizeWithMax.FitMode.PreferredSize)
			{
				return;
			}
			this.RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, Mathf.Clamp(LayoutUtility.GetPreferredSize(this.rectTransform, axis), 0f, (axis == 0) ? this.MaximumPreferredSize.x : this.MaximumPreferredSize.y));
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000CDB7A File Offset: 0x000CBD7A
		public virtual void SetLayoutHorizontal()
		{
			this.tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000CDB8E File Offset: 0x000CBD8E
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000CDB97 File Offset: 0x000CBD97
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.RectTransform);
		}

		// Token: 0x04001395 RID: 5013
		[SerializeField]
		protected ContentSizeWithMax.FitMode horizontalFit;

		// Token: 0x04001396 RID: 5014
		[SerializeField]
		protected ContentSizeWithMax.FitMode verticalFit;

		// Token: 0x04001397 RID: 5015
		[Tooltip("Maximum Preferred size when using Preferred Size")]
		public Vector2 MaximumPreferredSize;

		// Token: 0x04001398 RID: 5016
		[NonSerialized]
		private RectTransform rectTransform;

		// Token: 0x04001399 RID: 5017
		private DrivenRectTransformTracker tracker;

		// Token: 0x020017D9 RID: 6105
		public enum FitMode
		{
			// Token: 0x04006000 RID: 24576
			Unconstrained,
			// Token: 0x04006001 RID: 24577
			MinSize,
			// Token: 0x04006002 RID: 24578
			PreferredSize
		}
	}
}
