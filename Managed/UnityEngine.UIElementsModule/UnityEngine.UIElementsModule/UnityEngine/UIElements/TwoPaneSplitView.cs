using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BF RID: 447
	public class TwoPaneSplitView : VisualElement
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		public VisualElement fixedPane
		{
			get
			{
				return this.m_FixedPane;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0003E2DC File Offset: 0x0003C4DC
		public VisualElement flexedPane
		{
			get
			{
				return this.m_FlexedPane;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		internal VisualElement dragLine
		{
			get
			{
				return this.m_DragLine;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003E2EC File Offset: 0x0003C4EC
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		[CreateProperty]
		public int fixedPaneIndex
		{
			get
			{
				return this.m_FixedPaneIndex;
			}
			set
			{
				bool flag = value == this.m_FixedPaneIndex;
				if (!flag)
				{
					this.Init(value, this.m_FixedPaneInitialDimension, this.m_Orientation);
					base.NotifyPropertyChanged(in TwoPaneSplitView.fixedPaneIndexProperty);
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003E331 File Offset: 0x0003C531
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0003E33C File Offset: 0x0003C53C
		[CreateProperty]
		public float fixedPaneInitialDimension
		{
			get
			{
				return this.m_FixedPaneInitialDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneInitialDimension;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, value, this.m_Orientation);
					base.NotifyPropertyChanged(in TwoPaneSplitView.fixedPaneInitialDimensionProperty);
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003E379 File Offset: 0x0003C579
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0003E384 File Offset: 0x0003C584
		[CreateProperty]
		public TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_Orientation;
			}
			set
			{
				bool flag = value == this.m_Orientation;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, value);
					base.NotifyPropertyChanged(in TwoPaneSplitView.orientationProperty);
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0003E3C1 File Offset: 0x0003C5C1
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		internal float fixedPaneDimension
		{
			get
			{
				return string.IsNullOrEmpty(base.viewDataKey) ? this.m_FixedPaneInitialDimension : this.m_FixedPaneDimension;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			set
			{
				bool flag = value == this.m_FixedPaneDimension;
				if (!flag)
				{
					this.m_FixedPaneDimension = value;
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0003E40C File Offset: 0x0003C60C
		public TwoPaneSplitView()
		{
			this.SetupSplitView();
			this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003E45E File Offset: 0x0003C65E
		public TwoPaneSplitView(int fixedPaneIndex, float fixedPaneStartDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.SetupSplitView();
			this.Init(fixedPaneIndex, fixedPaneStartDimension, orientation);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003E498 File Offset: 0x0003C698
		private void SetupSplitView()
		{
			base.AddToClassList(TwoPaneSplitView.s_UssClassName);
			this.m_Content = new VisualElement();
			this.m_Content.name = "unity-content-container";
			this.m_Content.AddToClassList(TwoPaneSplitView.s_ContentContainerClassName);
			base.hierarchy.Add(this.m_Content);
			this.m_DragLineAnchor = new VisualElement();
			this.m_DragLineAnchor.name = "unity-dragline-anchor";
			this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorClassName);
			base.hierarchy.Add(this.m_DragLineAnchor);
			this.m_DragLine = new VisualElement();
			this.m_DragLine.name = "unity-dragline";
			this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineClassName);
			this.m_DragLineAnchor.Add(this.m_DragLine);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003E578 File Offset: 0x0003C778
		public void CollapseChild(int index)
		{
			bool flag = index != 0 && index != 1;
			if (flag)
			{
				Debug.LogError("Invalid index. Must be 0 or 1.");
			}
			else
			{
				bool flag2 = this.m_LeftPane == null;
				if (flag2)
				{
					this.m_PendingCollapseToExecute = true;
					this.m_CollapsedChildIndex = index;
				}
				else
				{
					this.m_DragLine.style.display = DisplayStyle.None;
					this.m_DragLineAnchor.style.display = DisplayStyle.None;
					bool flag3 = index == 0;
					if (flag3)
					{
						this.m_RightPane.style.width = StyleKeyword.Initial;
						this.m_RightPane.style.height = StyleKeyword.Initial;
						this.m_RightPane.style.flexGrow = 1f;
						this.m_LeftPane.style.display = DisplayStyle.None;
					}
					else
					{
						this.m_LeftPane.style.width = StyleKeyword.Initial;
						this.m_LeftPane.style.height = StyleKeyword.Initial;
						this.m_LeftPane.style.flexGrow = 1f;
						this.m_RightPane.style.display = DisplayStyle.None;
					}
					this.m_CollapseMode = true;
					this.AdjustPanesBasedOnAnchor();
				}
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
		public void UnCollapse()
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				VisualElement visualElement = null;
				bool flag2 = this.m_LeftPane.style.display == DisplayStyle.None;
				if (flag2)
				{
					visualElement = this.m_LeftPane;
				}
				else
				{
					bool flag3 = this.m_RightPane.style.display == DisplayStyle.None;
					if (flag3)
					{
						visualElement = this.m_RightPane;
					}
				}
				bool flag4 = visualElement == null;
				if (!flag4)
				{
					this.m_LeftPane.style.display = DisplayStyle.Flex;
					this.m_RightPane.style.display = DisplayStyle.Flex;
					this.m_DragLine.style.display = DisplayStyle.Flex;
					this.m_DragLineAnchor.style.display = DisplayStyle.Flex;
					this.m_LeftPane.style.flexGrow = 0f;
					this.m_RightPane.style.flexGrow = 0f;
					this.m_CollapseMode = false;
					this.m_PendingCollapseToExecute = false;
					this.m_CollapsedChildIndex = -1;
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
					this.AdjustPanesBasedOnAnchor();
					visualElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnUncollapsedPaneResized), TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003E82E File Offset: 0x0003CA2E
		private void OnUncollapsedPaneResized(GeometryChangedEvent evt)
		{
			this.UpdateDragLineAnchorOffset();
			evt.elementTarget.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnUncollapsedPaneResized), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003E854 File Offset: 0x0003CA54
		internal virtual void Init(int fixedPaneIndex, float fixedPaneInitialDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_FixedPaneIndex = fixedPaneIndex;
			this.m_FixedPaneInitialDimension = fixedPaneInitialDimension;
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_HorizontalClassName);
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_VerticalClassName);
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_HorizontalClassName);
			}
			else
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_VerticalClassName);
			}
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag2)
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			}
			else
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			}
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			}
			else
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			}
			bool flag4 = this.m_Resizer != null;
			if (flag4)
			{
				this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				this.m_Resizer = null;
			}
			bool flag5 = this.m_Content.childCount != 2;
			if (flag5)
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
			else
			{
				this.PostDisplaySetup();
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003E9CC File Offset: 0x0003CBCC
		private void OnPostDisplaySetup(GeometryChangedEvent evt)
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool flag2 = this.m_LeftPane == null;
				this.PostDisplaySetup();
				bool flag3 = flag2 && this.m_PendingCollapseToExecute;
				if (flag3)
				{
					this.CollapseChild(this.m_CollapsedChildIndex);
					this.m_PendingCollapseToExecute = false;
				}
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
				this.AdjustPanesBasedOnAnchor();
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003EA50 File Offset: 0x0003CC50
		private void AdjustPanesBasedOnAnchor()
		{
			bool flag = this.m_LeftPane.style.display == DisplayStyle.None || this.m_RightPane.style.display == DisplayStyle.None;
			if (flag)
			{
				bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag2)
				{
					this.m_RightPane.style.left = 0f;
					this.m_Content.style.paddingRight = 0f;
				}
				else
				{
					this.m_RightPane.style.top = 0f;
					this.m_Content.style.paddingBottom = 0f;
				}
			}
			else
			{
				bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag3)
				{
					this.m_RightPane.style.left = this.m_DragLineAnchor.layout.width;
					this.m_Content.style.paddingRight = this.m_DragLineAnchor.layout.width;
				}
				else
				{
					this.m_RightPane.style.top = this.m_DragLineAnchor.layout.height;
					this.m_Content.style.paddingBottom = this.m_DragLineAnchor.layout.height;
				}
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003EBE0 File Offset: 0x0003CDE0
		private void IdentifyLeftAndRightPane()
		{
			this.m_LeftPane = this.m_Content[0];
			bool flag = this.m_FixedPaneIndex == 0;
			if (flag)
			{
				this.m_FixedPane = this.m_LeftPane;
			}
			else
			{
				this.m_FlexedPane = this.m_LeftPane;
			}
			this.m_RightPane = this.m_Content[1];
			bool flag2 = this.m_FixedPaneIndex == 1;
			if (flag2)
			{
				this.m_FixedPane = this.m_RightPane;
			}
			else
			{
				this.m_FlexedPane = this.m_RightPane;
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003EC60 File Offset: 0x0003CE60
		private void PostDisplaySetup()
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool flag2 = this.fixedPaneDimension < 0f;
				if (flag2)
				{
					this.fixedPaneDimension = this.m_FixedPaneInitialDimension;
				}
				float fixedPaneDimension = this.fixedPaneDimension;
				this.IdentifyLeftAndRightPane();
				this.m_FixedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FixedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FlexedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.width = StyleKeyword.Null;
				this.m_FixedPane.style.height = StyleKeyword.Null;
				this.m_FlexedPane.style.width = StyleKeyword.Null;
				this.m_FlexedPane.style.height = StyleKeyword.Null;
				bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag3)
				{
					this.m_FixedPane.style.width = fixedPaneDimension;
					this.m_FixedPane.style.height = StyleKeyword.Null;
				}
				else
				{
					this.m_FixedPane.style.width = StyleKeyword.Null;
					this.m_FixedPane.style.height = fixedPaneDimension;
				}
				this.m_FixedPane.style.flexShrink = 0f;
				this.m_FixedPane.style.flexGrow = 0f;
				this.m_FlexedPane.style.flexGrow = 1f;
				this.m_FlexedPane.style.flexShrink = 0f;
				this.m_FlexedPane.style.flexBasis = 0f;
				this.m_DragLineAnchor.style.left = 0f;
				this.m_DragLineAnchor.style.top = 0f;
				bool flag4 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag4)
				{
					float num = this.m_FixedPane.resolvedStyle.marginLeft + this.m_FixedPane.resolvedStyle.marginRight;
					bool flag5 = this.m_FixedPaneIndex == 0;
					if (flag5)
					{
						this.m_DragLineAnchor.style.left = num + fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.left = base.resolvedStyle.width - num - fixedPaneDimension - this.m_DragLineAnchor.resolvedStyle.width;
					}
				}
				else
				{
					float num2 = this.m_FixedPane.resolvedStyle.marginTop + this.m_FixedPane.resolvedStyle.marginBottom;
					bool flag6 = this.m_FixedPaneIndex == 0;
					if (flag6)
					{
						this.m_DragLineAnchor.style.top = num2 + fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.top = base.resolvedStyle.height - num2 - fixedPaneDimension - this.m_DragLineAnchor.resolvedStyle.height;
					}
				}
				bool flag7 = this.m_FixedPaneIndex == 0;
				int num3;
				if (flag7)
				{
					num3 = 1;
				}
				else
				{
					num3 = -1;
				}
				bool flag8 = this.m_Resizer != null;
				if (flag8)
				{
					this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				}
				this.m_Resizer = new TwoPaneSplitViewResizer(this, num3);
				this.m_DragLineAnchor.AddManipulator(this.m_Resizer);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003F064 File Offset: 0x0003D264
		private void OnSizeChange(GeometryChangedEvent evt)
		{
			this.UpdateLayout(true, true);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003F070 File Offset: 0x0003D270
		private void UpdateDragLineAnchorOffset()
		{
			this.UpdateLayout(false, true);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003F07C File Offset: 0x0003D27C
		private void UpdateLayout(bool updateFixedPane, bool updateDragLine)
		{
			bool collapseMode = this.m_CollapseMode;
			if (!collapseMode)
			{
				bool flag = base.resolvedStyle.display == DisplayStyle.None || base.resolvedStyle.visibility == Visibility.Hidden;
				if (!flag)
				{
					float num = base.resolvedStyle.width;
					float num2 = this.m_FixedPane.resolvedStyle.width;
					float num3 = this.m_FixedPane.resolvedStyle.marginLeft + this.m_FixedPane.resolvedStyle.marginRight;
					float num4 = this.m_FixedPane.resolvedStyle.minWidth.value;
					float num5 = this.m_FlexedPane.resolvedStyle.marginLeft + this.m_FlexedPane.resolvedStyle.marginRight;
					float num6 = this.m_FlexedPane.resolvedStyle.minWidth.value;
					bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag2)
					{
						num = base.resolvedStyle.height;
						num2 = this.m_FixedPane.resolvedStyle.height;
						num3 = this.m_FixedPane.resolvedStyle.marginTop + this.m_FixedPane.resolvedStyle.marginBottom;
						num4 = this.m_FixedPane.resolvedStyle.minHeight.value;
						num5 = this.m_FlexedPane.resolvedStyle.marginTop + this.m_FlexedPane.resolvedStyle.marginBottom;
						num6 = this.m_FlexedPane.resolvedStyle.minHeight.value;
					}
					bool flag3 = num >= num2 + num3 + num6 + num5;
					if (flag3)
					{
						if (updateDragLine)
						{
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (num2 + num3) : (num - num2 - num3));
						}
					}
					else
					{
						bool flag4 = num >= num4 + num3 + num6 + num5;
						if (flag4)
						{
							float num7 = num - num6 - num5 - num3;
							float num8 = ((this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_DragLineAnchor.layout.width : this.m_DragLineAnchor.layout.height);
							num7 -= num8;
							bool flag5 = num7 < num4;
							bool flag6 = num2 > num4;
							if (updateFixedPane)
							{
								bool flag7 = !flag5;
								if (flag7)
								{
									this.SetFixedPaneDimension(num7);
								}
								else
								{
									bool flag8 = flag6;
									if (flag8)
									{
										this.SetFixedPaneDimension(num4);
									}
								}
							}
							if (updateDragLine)
							{
								bool flag9 = flag5;
								if (flag9)
								{
									this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num4 : (num - num4 - num3));
								}
								else
								{
									this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (num7 + num3) : (num6 + num5));
								}
							}
						}
						else
						{
							if (updateFixedPane)
							{
								this.SetFixedPaneDimension(num4);
							}
							if (updateDragLine)
							{
								this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? (num4 + num3) : (num6 + num5));
							}
						}
					}
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0003F358 File Offset: 0x0003D558
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Content;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003F370 File Offset: 0x0003D570
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.PostDisplaySetup();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003F39C File Offset: 0x0003D59C
		private void SetDragLineOffset(float offset)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_DragLineAnchor.style.left = offset;
			}
			else
			{
				this.m_DragLineAnchor.style.top = offset;
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
		private void SetFixedPaneDimension(float dimension)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_FixedPane.style.width = dimension;
			}
			else
			{
				this.m_FixedPane.style.height = dimension;
			}
		}

		// Token: 0x040008AB RID: 2219
		internal static readonly BindingId fixedPaneIndexProperty = "fixedPaneIndex";

		// Token: 0x040008AC RID: 2220
		internal static readonly BindingId fixedPaneInitialDimensionProperty = "fixedPaneInitialDimension";

		// Token: 0x040008AD RID: 2221
		internal static readonly BindingId orientationProperty = "orientation";

		// Token: 0x040008AE RID: 2222
		private const float k_FixedPaneInitialDimension = 100f;

		// Token: 0x040008AF RID: 2223
		private static readonly string s_UssClassName = "unity-two-pane-split-view";

		// Token: 0x040008B0 RID: 2224
		private static readonly string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

		// Token: 0x040008B1 RID: 2225
		private static readonly string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

		// Token: 0x040008B2 RID: 2226
		private static readonly string s_HandleDragLineVerticalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--vertical";

		// Token: 0x040008B3 RID: 2227
		private static readonly string s_HandleDragLineHorizontalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--horizontal";

		// Token: 0x040008B4 RID: 2228
		private static readonly string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

		// Token: 0x040008B5 RID: 2229
		private static readonly string s_HandleDragLineAnchorVerticalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--vertical";

		// Token: 0x040008B6 RID: 2230
		private static readonly string s_HandleDragLineAnchorHorizontalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--horizontal";

		// Token: 0x040008B7 RID: 2231
		private static readonly string s_VerticalClassName = "unity-two-pane-split-view--vertical";

		// Token: 0x040008B8 RID: 2232
		private static readonly string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

		// Token: 0x040008B9 RID: 2233
		private VisualElement m_LeftPane;

		// Token: 0x040008BA RID: 2234
		private VisualElement m_RightPane;

		// Token: 0x040008BB RID: 2235
		private VisualElement m_FixedPane;

		// Token: 0x040008BC RID: 2236
		private VisualElement m_FlexedPane;

		// Token: 0x040008BD RID: 2237
		[DontCreateProperty]
		[SerializeField]
		private float m_FixedPaneDimension = -1f;

		// Token: 0x040008BE RID: 2238
		private VisualElement m_DragLine;

		// Token: 0x040008BF RID: 2239
		private VisualElement m_DragLineAnchor;

		// Token: 0x040008C0 RID: 2240
		private bool m_CollapseMode;

		// Token: 0x040008C1 RID: 2241
		private bool m_PendingCollapseToExecute;

		// Token: 0x040008C2 RID: 2242
		private int m_CollapsedChildIndex = -1;

		// Token: 0x040008C3 RID: 2243
		private VisualElement m_Content;

		// Token: 0x040008C4 RID: 2244
		private TwoPaneSplitViewOrientation m_Orientation;

		// Token: 0x040008C5 RID: 2245
		private int m_FixedPaneIndex;

		// Token: 0x040008C6 RID: 2246
		private float m_FixedPaneInitialDimension = 100f;

		// Token: 0x040008C7 RID: 2247
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal TwoPaneSplitViewResizer m_Resizer;

		// Token: 0x020001C0 RID: 448
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000E02 RID: 3586 RVA: 0x0003F4FC File Offset: 0x0003D6FC
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TwoPaneSplitView.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("fixedPaneIndex", "fixed-pane-index", null, Array.Empty<string>()),
					new UxmlAttributeNames("fixedPaneInitialDimension", "fixed-pane-initial-dimension", null, Array.Empty<string>()),
					new UxmlAttributeNames("orientation", "orientation", null, Array.Empty<string>())
				});
			}

			// Token: 0x06000E03 RID: 3587 RVA: 0x0003F574 File Offset: 0x0003D774
			public override object CreateInstance()
			{
				return new TwoPaneSplitView();
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x0003F57C File Offset: 0x0003D77C
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.fixedPaneIndex_UxmlAttributeFlags) || UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.fixedPaneInitialDimension_UxmlAttributeFlags) || UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.orientation_UxmlAttributeFlags);
				if (flag)
				{
					TwoPaneSplitView twoPaneSplitView = (TwoPaneSplitView)obj;
					twoPaneSplitView.Init(this.fixedPaneIndex, this.fixedPaneInitialDimension, this.orientation);
				}
			}

			// Token: 0x040008C8 RID: 2248
			[SerializeField]
			private int fixedPaneIndex;

			// Token: 0x040008C9 RID: 2249
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags fixedPaneIndex_UxmlAttributeFlags;

			// Token: 0x040008CA RID: 2250
			[SerializeField]
			private float fixedPaneInitialDimension;

			// Token: 0x040008CB RID: 2251
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags fixedPaneInitialDimension_UxmlAttributeFlags;

			// Token: 0x040008CC RID: 2252
			[SerializeField]
			private TwoPaneSplitViewOrientation orientation;

			// Token: 0x040008CD RID: 2253
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags orientation_UxmlAttributeFlags;
		}

		// Token: 0x020001C1 RID: 449
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, TwoPaneSplitView.UxmlTraits>
		{
		}

		// Token: 0x020001C2 RID: 450
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003F5EC File Offset: 0x0003D7EC
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000E08 RID: 3592 RVA: 0x0003F60C File Offset: 0x0003D80C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int valueFromBag = this.m_FixedPaneIndex.GetValueFromBag(bag, cc);
				int valueFromBag2 = this.m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
				TwoPaneSplitViewOrientation valueFromBag3 = this.m_Orientation.GetValueFromBag(bag, cc);
				((TwoPaneSplitView)ve).Init(valueFromBag, (float)valueFromBag2, valueFromBag3);
			}

			// Token: 0x040008CE RID: 2254
			private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-index",
				defaultValue = 0
			};

			// Token: 0x040008CF RID: 2255
			private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-initial-dimension",
				defaultValue = 100
			};

			// Token: 0x040008D0 RID: 2256
			private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
			{
				name = "orientation",
				defaultValue = TwoPaneSplitViewOrientation.Horizontal
			};
		}
	}
}
