using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020003EC RID: 1004
	[AddComponentMenu("Layout/Extensions/Flow Layout Group")]
	public class FlowLayoutGroup : LayoutGroup
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000BF7C4 File Offset: 0x000BD9C4
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x000BF7CC File Offset: 0x000BD9CC
		public FlowLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<FlowLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000BF7DC File Offset: 0x000BD9DC
		public override void CalculateLayoutInputHorizontal()
		{
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				base.CalculateLayoutInputHorizontal();
				float num = this.GetGreatestMinimumChildWidth() + (float)base.padding.left + (float)base.padding.right;
				base.SetLayoutInputForAxis(num, -1f, -1f, 0);
				return;
			}
			this._layoutWidth = this.SetLayout(0, true);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000BF839 File Offset: 0x000BDA39
		public override void SetLayoutHorizontal()
		{
			this.SetLayout(0, false);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000BF844 File Offset: 0x000BDA44
		public override void SetLayoutVertical()
		{
			this.SetLayout(1, false);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000BF850 File Offset: 0x000BDA50
		public override void CalculateLayoutInputVertical()
		{
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				this._layoutHeight = this.SetLayout(1, true);
				return;
			}
			base.CalculateLayoutInputHorizontal();
			float num = this.GetGreatestMinimumChildHeigth() + (float)base.padding.bottom + (float)base.padding.top;
			base.SetLayoutInputForAxis(num, -1f, -1f, 1);
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000BF8AD File Offset: 0x000BDAAD
		protected bool IsCenterAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerCenter || base.childAlignment == TextAnchor.MiddleCenter || base.childAlignment == TextAnchor.UpperCenter;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000BF8CC File Offset: 0x000BDACC
		protected bool IsRightAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerRight || base.childAlignment == TextAnchor.MiddleRight || base.childAlignment == TextAnchor.UpperRight;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x000BF8EB File Offset: 0x000BDAEB
		protected bool IsMiddleAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.MiddleLeft || base.childAlignment == TextAnchor.MiddleRight || base.childAlignment == TextAnchor.MiddleCenter;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000BF90A File Offset: 0x000BDB0A
		protected bool IsLowerAlign
		{
			get
			{
				return base.childAlignment == TextAnchor.LowerLeft || base.childAlignment == TextAnchor.LowerRight || base.childAlignment == TextAnchor.LowerCenter;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000BF92C File Offset: 0x000BDB2C
		public float SetLayout(int axis, bool layoutInput)
		{
			float height = base.rectTransform.rect.height;
			float width = base.rectTransform.rect.width;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
			{
				num5 = height;
				num6 = width - (float)base.padding.left - (float)base.padding.right;
				if (this.IsLowerAlign)
				{
					num3 = (float)base.padding.bottom;
					num4 = (float)base.padding.top;
				}
				else
				{
					num3 = (float)base.padding.top;
					num4 = (float)base.padding.bottom;
				}
				num = this.SpacingY;
				num2 = this.SpacingX;
			}
			else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
			{
				num5 = width;
				num6 = height - (float)base.padding.top - (float)base.padding.bottom;
				if (this.IsRightAlign)
				{
					num3 = (float)base.padding.right;
					num4 = (float)base.padding.left;
				}
				else
				{
					num3 = (float)base.padding.left;
					num4 = (float)base.padding.right;
				}
				num = this.SpacingX;
				num2 = this.SpacingY;
			}
			float num7 = 0f;
			float num8 = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				int num9 = i;
				RectTransform rectTransform = base.rectChildren[num9];
				float num10 = 0f;
				float num11 = 0f;
				if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
				{
					if (this.invertOrder)
					{
						num9 = (this.IsLowerAlign ? (base.rectChildren.Count - 1 - i) : i);
					}
					rectTransform = base.rectChildren[num9];
					num10 = LayoutUtility.GetPreferredSize(rectTransform, 0);
					num10 = Mathf.Min(num10, num6);
					num11 = LayoutUtility.GetPreferredSize(rectTransform, 1);
					num11 = Mathf.Min(num11, num6);
				}
				else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
				{
					if (this.invertOrder)
					{
						num9 = (this.IsRightAlign ? (base.rectChildren.Count - 1 - i) : i);
					}
					rectTransform = base.rectChildren[num9];
					num10 = LayoutUtility.GetPreferredSize(rectTransform, 1);
					num10 = Mathf.Min(num10, num6);
					num11 = LayoutUtility.GetPreferredSize(rectTransform, 0);
					num11 = Mathf.Min(num11, num6);
				}
				if (num7 + num10 > num6)
				{
					num7 -= num2;
					if (!layoutInput)
					{
						if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
						{
							float num12 = this.CalculateRowVerticalOffset(num5, num3, num8);
							this.LayoutRow(this._itemList, num7, num8, num6, (float)base.padding.left, num12, axis);
						}
						else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
						{
							float num13 = this.CalculateColHorizontalOffset(num5, num3, num8);
							this.LayoutCol(this._itemList, num8, num7, num6, num13, (float)base.padding.top, axis);
						}
					}
					this._itemList.Clear();
					num3 += num8;
					num3 += num;
					num8 = 0f;
					num7 = 0f;
				}
				num7 += num10;
				this._itemList.Add(rectTransform);
				if (num11 > num8)
				{
					num8 = num11;
				}
				if (i < base.rectChildren.Count - 1)
				{
					num7 += num2;
				}
			}
			if (!layoutInput)
			{
				if (this.startAxis == FlowLayoutGroup.Axis.Horizontal)
				{
					float num14 = this.CalculateRowVerticalOffset(height, num3, num8);
					num7 -= num2;
					this.LayoutRow(this._itemList, num7, num8, num6 - (this.ChildForceExpandWidth ? 0f : num2), (float)base.padding.left, num14, axis);
				}
				else if (this.startAxis == FlowLayoutGroup.Axis.Vertical)
				{
					float num15 = this.CalculateColHorizontalOffset(width, num3, num8);
					num7 -= num2;
					this.LayoutCol(this._itemList, num8, num7, num6 - (this.ChildForceExpandHeight ? 0f : num2), num15, (float)base.padding.top, axis);
				}
			}
			this._itemList.Clear();
			num3 += num8;
			num3 += num4;
			if (layoutInput)
			{
				base.SetLayoutInputForAxis(num3, num3, -1f, axis);
			}
			return num3;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000BFD50 File Offset: 0x000BDF50
		private float CalculateRowVerticalOffset(float groupHeight, float yOffset, float currentRowHeight)
		{
			if (this.IsLowerAlign)
			{
				return groupHeight - yOffset - currentRowHeight;
			}
			if (this.IsMiddleAlign)
			{
				return groupHeight * 0.5f - this._layoutHeight * 0.5f + yOffset;
			}
			return yOffset;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000BFD80 File Offset: 0x000BDF80
		private float CalculateColHorizontalOffset(float groupWidth, float xOffset, float currentColWidth)
		{
			if (this.IsRightAlign)
			{
				return groupWidth - xOffset - currentColWidth;
			}
			if (this.IsCenterAlign)
			{
				return groupWidth * 0.5f - this._layoutWidth * 0.5f + xOffset;
			}
			return xOffset;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000BFDB0 File Offset: 0x000BDFB0
		protected void LayoutRow(IList<RectTransform> contents, float rowWidth, float rowHeight, float maxWidth, float xOffset, float yOffset, int axis)
		{
			float num = xOffset;
			if (!this.ChildForceExpandWidth && this.IsCenterAlign)
			{
				num += (maxWidth - rowWidth) * 0.5f;
			}
			else if (!this.ChildForceExpandWidth && this.IsRightAlign)
			{
				num += maxWidth - rowWidth;
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.ChildForceExpandWidth)
			{
				num2 = (maxWidth - rowWidth) / (float)this._itemList.Count;
			}
			else if (this.ExpandHorizontalSpacing)
			{
				num3 = (maxWidth - rowWidth) / (float)(this._itemList.Count - 1);
				if (this._itemList.Count > 1)
				{
					if (this.IsCenterAlign)
					{
						num -= num3 * 0.5f * (float)(this._itemList.Count - 1);
					}
					else if (this.IsRightAlign)
					{
						num -= num3 * (float)(this._itemList.Count - 1);
					}
				}
			}
			for (int i = 0; i < this._itemList.Count; i++)
			{
				int num4 = (this.IsLowerAlign ? (this._itemList.Count - 1 - i) : i);
				RectTransform rectTransform = this._itemList[num4];
				float num5 = LayoutUtility.GetPreferredSize(rectTransform, 0) + num2;
				float num6 = LayoutUtility.GetPreferredSize(rectTransform, 1);
				if (this.ChildForceExpandHeight)
				{
					num6 = rowHeight;
				}
				num5 = Mathf.Min(num5, maxWidth);
				float num7 = yOffset;
				if (this.IsMiddleAlign)
				{
					num7 += (rowHeight - num6) * 0.5f;
				}
				else if (this.IsLowerAlign)
				{
					num7 += rowHeight - num6;
				}
				if (this.ExpandHorizontalSpacing && i > 0)
				{
					num += num3;
				}
				if (axis == 0)
				{
					base.SetChildAlongAxis(rectTransform, 0, num, num5);
				}
				else
				{
					base.SetChildAlongAxis(rectTransform, 1, num7, num6);
				}
				if (i < this._itemList.Count - 1)
				{
					num += num5 + this.SpacingX;
				}
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000BFF74 File Offset: 0x000BE174
		protected void LayoutCol(IList<RectTransform> contents, float colWidth, float colHeight, float maxHeight, float xOffset, float yOffset, int axis)
		{
			float num = yOffset;
			if (!this.ChildForceExpandHeight && this.IsMiddleAlign)
			{
				num += (maxHeight - colHeight) * 0.5f;
			}
			else if (!this.ChildForceExpandHeight && this.IsLowerAlign)
			{
				num += maxHeight - colHeight;
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.ChildForceExpandHeight)
			{
				num2 = (maxHeight - colHeight) / (float)this._itemList.Count;
			}
			else if (this.ExpandHorizontalSpacing)
			{
				num3 = (maxHeight - colHeight) / (float)(this._itemList.Count - 1);
				if (this._itemList.Count > 1)
				{
					if (this.IsMiddleAlign)
					{
						num -= num3 * 0.5f * (float)(this._itemList.Count - 1);
					}
					else if (this.IsLowerAlign)
					{
						num -= num3 * (float)(this._itemList.Count - 1);
					}
				}
			}
			for (int i = 0; i < this._itemList.Count; i++)
			{
				int num4 = (this.IsRightAlign ? (this._itemList.Count - 1 - i) : i);
				RectTransform rectTransform = this._itemList[num4];
				float num5 = LayoutUtility.GetPreferredSize(rectTransform, 0);
				float num6 = LayoutUtility.GetPreferredSize(rectTransform, 1) + num2;
				if (this.ChildForceExpandWidth)
				{
					num5 = colWidth;
				}
				num6 = Mathf.Min(num6, maxHeight);
				float num7 = xOffset;
				if (this.IsCenterAlign)
				{
					num7 += (colWidth - num5) * 0.5f;
				}
				else if (this.IsRightAlign)
				{
					num7 += colWidth - num5;
				}
				if (this.ExpandHorizontalSpacing && i > 0)
				{
					num += num3;
				}
				if (axis == 0)
				{
					base.SetChildAlongAxis(rectTransform, 0, num7, num5);
				}
				else
				{
					base.SetChildAlongAxis(rectTransform, 1, num, num6);
				}
				if (i < this._itemList.Count - 1)
				{
					num += num6 + this.SpacingY;
				}
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000C0138 File Offset: 0x000BE338
		public float GetGreatestMinimumChildWidth()
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				num = Mathf.Max(LayoutUtility.GetMinWidth(base.rectChildren[i]), num);
			}
			return num;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000C017C File Offset: 0x000BE37C
		public float GetGreatestMinimumChildHeigth()
		{
			float num = 0f;
			for (int i = 0; i < base.rectChildren.Count; i++)
			{
				num = Mathf.Max(LayoutUtility.GetMinHeight(base.rectChildren[i]), num);
			}
			return num;
		}

		// Token: 0x040012D2 RID: 4818
		public float SpacingX;

		// Token: 0x040012D3 RID: 4819
		public float SpacingY;

		// Token: 0x040012D4 RID: 4820
		public bool ExpandHorizontalSpacing;

		// Token: 0x040012D5 RID: 4821
		public bool ChildForceExpandWidth;

		// Token: 0x040012D6 RID: 4822
		public bool ChildForceExpandHeight;

		// Token: 0x040012D7 RID: 4823
		public bool invertOrder;

		// Token: 0x040012D8 RID: 4824
		private float _layoutHeight;

		// Token: 0x040012D9 RID: 4825
		private float _layoutWidth;

		// Token: 0x040012DA RID: 4826
		[SerializeField]
		protected FlowLayoutGroup.Axis m_StartAxis;

		// Token: 0x040012DB RID: 4827
		private readonly IList<RectTransform> _itemList = new List<RectTransform>();

		// Token: 0x020017B0 RID: 6064
		public enum Axis
		{
			// Token: 0x04005F57 RID: 24407
			Horizontal,
			// Token: 0x04005F58 RID: 24408
			Vertical
		}
	}
}
