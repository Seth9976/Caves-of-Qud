using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C5 RID: 453
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class TwoPaneSplitViewResizer : PointerManipulator
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0003F76B File Offset: 0x0003D96B
		private TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_SplitView.orientation;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003F778 File Offset: 0x0003D978
		private VisualElement fixedPane
		{
			get
			{
				return this.m_SplitView.fixedPane;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003F785 File Offset: 0x0003D985
		private VisualElement flexedPane
		{
			get
			{
				return this.m_SplitView.flexedPane;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0003F792 File Offset: 0x0003D992
		public float delta
		{
			get
			{
				return this.m_Delta;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003F79C File Offset: 0x0003D99C
		private float fixedPaneMinDimension
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.fixedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.fixedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		private float fixedPaneMargins
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.fixedPane.resolvedStyle.marginLeft + this.fixedPane.resolvedStyle.marginRight;
				}
				else
				{
					num = this.fixedPane.resolvedStyle.marginTop + this.fixedPane.resolvedStyle.marginBottom;
				}
				return num;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003F854 File Offset: 0x0003DA54
		private float flexedPaneMinDimension
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.flexedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.flexedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0003F8A8 File Offset: 0x0003DAA8
		private float flexedPaneMargin
		{
			get
			{
				bool flag = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.flexedPane.resolvedStyle.marginLeft + this.flexedPane.resolvedStyle.marginRight;
				}
				else
				{
					num = this.flexedPane.resolvedStyle.marginTop + this.flexedPane.resolvedStyle.marginBottom;
				}
				return num;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003F90C File Offset: 0x0003DB0C
		public TwoPaneSplitViewResizer(TwoPaneSplitView splitView, int dir)
		{
			this.m_SplitView = splitView;
			this.m_Direction = dir;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003F954 File Offset: 0x0003DB54
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003F9B0 File Offset: 0x0003DBB0
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003FA0C File Offset: 0x0003DC0C
		public void ApplyDelta(float delta)
		{
			float num = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.fixedPane.resolvedStyle.width : this.fixedPane.resolvedStyle.height);
			float num2 = num + delta;
			float num3 = this.fixedPaneMinDimension;
			bool flag = this.m_SplitView.fixedPaneIndex == 1;
			if (flag)
			{
				num3 += ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? (base.target.worldBound.width + Math.Abs(this.m_SplitView.dragLine.resolvedStyle.left)) : (base.target.worldBound.height + Math.Abs(this.m_SplitView.dragLine.resolvedStyle.top)));
			}
			bool flag2 = num2 < num && num2 < num3;
			if (flag2)
			{
				num2 = num3;
			}
			float num4 = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.resolvedStyle.width : this.m_SplitView.resolvedStyle.height);
			num4 -= this.flexedPaneMinDimension + this.flexedPaneMargin + this.fixedPaneMargins;
			bool flag3 = this.m_SplitView.fixedPaneIndex == 0;
			if (flag3)
			{
				num4 -= ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? Math.Abs(base.target.worldBound.width - (this.m_SplitView.dragLine.resolvedStyle.width - Math.Abs(this.m_SplitView.dragLine.resolvedStyle.left))) : Math.Abs(base.target.worldBound.height - (this.m_SplitView.dragLine.resolvedStyle.height - Math.Abs(this.m_SplitView.dragLine.resolvedStyle.top))));
			}
			bool flag4 = num2 > num && num2 > num4;
			if (flag4)
			{
				num2 = num4;
			}
			bool flag5 = this.orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag5)
			{
				this.fixedPane.style.width = num2;
				bool flag6 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag6)
				{
					float num5 = num2 + this.fixedPaneMargins;
					bool flag7 = num5 >= this.fixedPaneMinDimension;
					if (flag7)
					{
						base.target.style.left = num5;
					}
				}
				else
				{
					float num6 = this.m_SplitView.resolvedStyle.width - num2 - this.fixedPaneMargins;
					bool flag8 = num6 >= this.flexedPaneMinDimension + this.flexedPaneMargin;
					if (flag8)
					{
						base.target.style.left = num6;
					}
				}
			}
			else
			{
				this.fixedPane.style.height = num2;
				bool flag9 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag9)
				{
					float num7 = num2 + this.fixedPaneMargins;
					bool flag10 = num7 >= this.fixedPaneMinDimension;
					if (flag10)
					{
						base.target.style.top = num7;
					}
				}
				else
				{
					float num8 = this.m_SplitView.resolvedStyle.height - num2 - this.fixedPaneMargins;
					bool flag11 = num8 >= this.flexedPaneMinDimension + this.flexedPaneMargin;
					if (flag11)
					{
						base.target.style.top = num8;
					}
				}
			}
			this.m_SplitView.fixedPaneDimension = num2;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003FD80 File Offset: 0x0003DF80
		protected void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					this.m_Start = e.localPosition;
					this.m_Active = true;
					base.target.CapturePointer(e.pointerId);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003FDDC File Offset: 0x0003DFDC
		protected void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				bool flag2 = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? (this.m_SplitView.dragLine.worldBound.x < base.target.worldBound.x) : (this.m_SplitView.dragLine.worldBound.y < base.target.worldBound.y));
				float num = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? Math.Abs(base.target.worldBound.x - this.m_SplitView.dragLine.worldBound.x) : Math.Abs(base.target.worldBound.y - this.m_SplitView.dragLine.worldBound.y));
				float num2 = ((this.orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.dragLine.resolvedStyle.left : this.m_SplitView.dragLine.resolvedStyle.top);
				bool flag3 = flag2 && Math.Abs(num2) + 1f <= num;
				if (flag3)
				{
					this.InterruptPointerMove(e);
				}
				else
				{
					Vector2 vector = e.localPosition - this.m_Start;
					float num3 = vector.x;
					bool flag4 = this.orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag4)
					{
						num3 = vector.y;
					}
					this.m_Delta = (float)this.m_Direction * num3;
					this.ApplyDelta(this.m_Delta);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003FFA8 File Offset: 0x0003E1A8
		protected void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00040004 File Offset: 0x0003E204
		protected void InterruptPointerMove(PointerMoveEvent e)
		{
			bool flag = !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x040008D8 RID: 2264
		private const float k_DragLineTolerance = 1f;

		// Token: 0x040008D9 RID: 2265
		private Vector3 m_Start;

		// Token: 0x040008DA RID: 2266
		protected bool m_Active;

		// Token: 0x040008DB RID: 2267
		private TwoPaneSplitView m_SplitView;

		// Token: 0x040008DC RID: 2268
		private int m_Direction;

		// Token: 0x040008DD RID: 2269
		private float m_Delta;
	}
}
