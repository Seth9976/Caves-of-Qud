using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000055 RID: 85
	public class Clickable : PointerManipulator
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002B6 RID: 694 RVA: 0x0000CD54 File Offset: 0x0000AF54
		// (remove) Token: 0x060002B7 RID: 695 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<EventBase> clickedWithEventInfo;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002B8 RID: 696 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		// (remove) Token: 0x060002B9 RID: 697 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action clicked;

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000CE31 File Offset: 0x0000B031
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000CE39 File Offset: 0x0000B039
		protected bool active { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000CE42 File Offset: 0x0000B042
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000CE4A File Offset: 0x0000B04A
		public Vector2 lastMousePosition { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000CE53 File Offset: 0x0000B053
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000CE5C File Offset: 0x0000B05C
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_AcceptClicksIfDisabled;
			}
			set
			{
				bool flag = this.m_AcceptClicksIfDisabled == value;
				if (!flag)
				{
					bool flag2 = base.target != null;
					if (flag2)
					{
						this.UnregisterCallbacksFromTarget();
					}
					this.m_AcceptClicksIfDisabled = value;
					bool flag3 = base.target != null;
					if (flag3)
					{
						this.RegisterCallbacksOnTarget();
					}
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		private InvokePolicy invokePolicy
		{
			get
			{
				return this.acceptClicksIfDisabled ? InvokePolicy.IncludeDisabled : InvokePolicy.Default;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000CEB6 File Offset: 0x0000B0B6
		public Clickable(Action handler, long delay, long interval)
			: this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public Clickable(Action<EventBase> handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public Clickable(Action handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000CF68 File Offset: 0x0000B168
		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = this.ContainsPointer(this.m_ActivePointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D024 File Offset: 0x0000B224
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
			this.ResetActivePseudoState();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D154 File Offset: 0x0000B354
		[Obsolete("OnMouseDown has been removed and replaced by its pointer-based equivalent. Please use OnPointerDown.", false)]
		protected void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.ProcessDownEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D180 File Offset: 0x0000B380
		[Obsolete("OnMouseMove has been removed and replaced by its pointer-based equivalent. Please use OnPointerMove.", false)]
		protected void OnMouseMove(MouseMoveEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessMoveEvent(evt, evt.localMousePosition);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		[Obsolete("OnMouseUp has been removed and replaced by its pointer-based equivalent. Please use OnPointerUp.", false)]
		protected void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = this.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		protected void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D218 File Offset: 0x0000B418
		protected void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D248 File Offset: 0x0000B448
		protected void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D28C File Offset: 0x0000B48C
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				this.ProcessCancelEvent(evt, evt.pointerId);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		private bool ContainsPointer(int pointerId)
		{
			VisualElement topElementUnderPointer = base.target.elementPanel.GetTopElementUnderPointer(pointerId);
			return base.target == topElementUnderPointer || base.target.Contains(topElementUnderPointer);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D32C File Offset: 0x0000B52C
		protected void Invoke(EventBase evt)
		{
			Action action = this.clicked;
			if (action != null)
			{
				action();
			}
			Action<EventBase> action2 = this.clickedWithEventInfo;
			if (action2 != null)
			{
				action2(evt);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D354 File Offset: 0x0000B554
		internal void SimulateSingleClick(EventBase evt, int delayMs = 100)
		{
			base.target.pseudoStates |= PseudoStates.Active;
			this.m_PendingActivePseudoStateReset = base.target.schedule.Execute(new Action(this.ResetActivePseudoState));
			this.m_PendingActivePseudoStateReset.ExecuteLater((long)delayMs);
			this.Invoke(evt);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		private void ResetActivePseudoState()
		{
			bool flag = this.m_PendingActivePseudoStateReset == null;
			if (!flag)
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
				this.m_PendingActivePseudoStateReset = null;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D3EC File Offset: 0x0000B5EC
		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			this.m_ActivePointerId = pointerId;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = this.ContainsPointer(pointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = this.ContainsPointer(this.m_ActivePointerId);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D554 File Offset: 0x0000B754
		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			else
			{
				bool flag3 = this.ContainsPointer(pointerId) && (base.target.enabledInHierarchy || this.acceptClicksIfDisabled);
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D610 File Offset: 0x0000B810
		protected virtual void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x040001A1 RID: 417
		private readonly long m_Delay;

		// Token: 0x040001A2 RID: 418
		private readonly long m_Interval;

		// Token: 0x040001A5 RID: 421
		private int m_ActivePointerId;

		// Token: 0x040001A6 RID: 422
		private bool m_AcceptClicksIfDisabled;

		// Token: 0x040001A7 RID: 423
		private IVisualElementScheduledItem m_Repeater;

		// Token: 0x040001A8 RID: 424
		private IVisualElementScheduledItem m_PendingActivePseudoStateReset;
	}
}
