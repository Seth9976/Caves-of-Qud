using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000258 RID: 600
	[EventCategory(EventCategory.Pointer)]
	public abstract class MouseEventBase<T> : EventBase<T>, IMouseEvent, IMouseEventInternal, IPointerOrMouseEvent where T : MouseEventBase<T>, new()
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004AE4C File Offset: 0x0004904C
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0004AE54 File Offset: 0x00049054
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004AE5D File Offset: 0x0004905D
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0004AE65 File Offset: 0x00049065
		public Vector2 mousePosition { get; protected set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004AE6E File Offset: 0x0004906E
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0004AE76 File Offset: 0x00049076
		public Vector2 localMousePosition { get; internal set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004AE7F File Offset: 0x0004907F
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0004AE87 File Offset: 0x00049087
		public Vector2 mouseDelta { get; protected set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004AE90 File Offset: 0x00049090
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x0004AE98 File Offset: 0x00049098
		public int clickCount { get; protected set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0004AEA1 File Offset: 0x000490A1
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0004AEA9 File Offset: 0x000490A9
		public int button { get; protected set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004AEB2 File Offset: 0x000490B2
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0004AEBA File Offset: 0x000490BA
		public int pressedButtons { get; protected set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0004AEC4 File Offset: 0x000490C4
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004AEE4 File Offset: 0x000490E4
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004AF04 File Offset: 0x00049104
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004AF24 File Offset: 0x00049124
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004AF44 File Offset: 0x00049144
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004AF7D File Offset: 0x0004917D
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x0004AF85 File Offset: 0x00049185
		bool IMouseEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004AF8E File Offset: 0x0004918E
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0004AF96 File Offset: 0x00049196
		IPointerEvent IMouseEventInternal.sourcePointerEvent { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004AF9F File Offset: 0x0004919F
		int IPointerOrMouseEvent.pointerId
		{
			get
			{
				return PointerId.mousePointerId;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004AFA6 File Offset: 0x000491A6
		Vector3 IPointerOrMouseEvent.position
		{
			get
			{
				return this.mousePosition;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004AFB3 File Offset: 0x000491B3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004AFC4 File Offset: 0x000491C4
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.modifiers = EventModifiers.None;
			this.mousePosition = Vector2.zero;
			this.localMousePosition = Vector2.zero;
			this.mouseDelta = Vector2.zero;
			this.clickCount = 0;
			this.button = 0;
			this.pressedButtons = 0;
			((IMouseEventInternal)this).triggeredByOS = false;
			((IMouseEventInternal)this).sourcePointerEvent = null;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0004B030 File Offset: 0x00049230
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x0004B048 File Offset: 0x00049248
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localMousePosition = visualElement.WorldToLocal(this.mousePosition);
				}
				else
				{
					this.localMousePosition = this.mousePosition;
				}
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004B098 File Offset: 0x00049298
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = ((IMouseEventInternal)this).sourcePointerEvent == null && ((IMouseEventInternal)this).triggeredByOS;
			if (flag)
			{
				PointerDeviceState.SavePointerPosition(PointerId.mousePointerId, this.mousePosition, panel, panel.contextType);
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004B0E0 File Offset: 0x000492E0
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase != null;
			if (flag)
			{
				Debug.Assert(!eventBase.processed, "!pointerEvent.processed");
				bool isPropagationStopped = base.isPropagationStopped;
				if (isPropagationStopped)
				{
					eventBase.StopPropagation();
				}
				bool isImmediatePropagationStopped = base.isImmediatePropagationStopped;
				if (isImmediatePropagationStopped)
				{
					eventBase.StopImmediatePropagation();
				}
				eventBase.processedByFocusController |= base.processedByFocusController;
			}
			base.PostDispatch(panel);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004B15A File Offset: 0x0004935A
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCapturingElementOrElementUnderPointer(this, panel, PointerId.mousePointerId, this.mousePosition);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004B170 File Offset: 0x00049370
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.mousePosition = systemEvent.mousePosition;
				pooled.localMousePosition = systemEvent.mousePosition;
				pooled.mouseDelta = systemEvent.delta;
				pooled.button = systemEvent.button;
				pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
				pooled.clickCount = systemEvent.clickCount;
				pooled.triggeredByOS = true;
			}
			return pooled;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004B234 File Offset: 0x00049434
		public static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers = EventModifiers.None)
		{
			return MouseEventBase<T>.GetPooled(position, button, clickCount, delta, modifiers, false);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004B254 File Offset: 0x00049454
		internal static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers, bool fromOS)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.mousePosition = position;
			pooled.localMousePosition = position;
			pooled.mouseDelta = delta;
			pooled.button = button;
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
			pooled.clickCount = clickCount;
			pooled.triggeredByOS = fromOS;
			return pooled;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004B2E0 File Offset: 0x000494E0
		internal static T GetPooled(IMouseEvent triggerEvent, Vector2 mousePosition)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = MouseEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.mousePosition = mousePosition;
				pooled.localMousePosition = mousePosition;
				t = pooled;
			}
			return t;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004B328 File Offset: 0x00049528
		public static T GetPooled(IMouseEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled(triggerEvent as EventBase);
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.modifiers = triggerEvent.modifiers;
				pooled.mousePosition = triggerEvent.mousePosition;
				pooled.localMousePosition = triggerEvent.mousePosition;
				pooled.mouseDelta = triggerEvent.mouseDelta;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.clickCount = triggerEvent.clickCount;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = mouseEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004B3F8 File Offset: 0x000495F8
		protected static T GetPooled(IPointerEvent pointerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			EventBase eventBase = pooled;
			EventBase eventBase2 = pointerEvent as EventBase;
			eventBase.elementTarget = ((eventBase2 != null) ? eventBase2.elementTarget : null);
			EventBase eventBase3 = pooled;
			EventBase eventBase4 = pointerEvent as EventBase;
			eventBase3.imguiEvent = ((eventBase4 != null) ? eventBase4.imguiEvent : null);
			pooled.modifiers = pointerEvent.modifiers;
			pooled.mousePosition = pointerEvent.position;
			pooled.localMousePosition = pointerEvent.position;
			pooled.mouseDelta = pointerEvent.deltaPosition;
			pooled.button = ((pointerEvent.button == -1) ? 0 : pointerEvent.button);
			pooled.pressedButtons = pointerEvent.pressedButtons;
			pooled.clickCount = pointerEvent.clickCount;
			IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
			bool flag = pointerEventInternal != null;
			if (flag)
			{
				pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				pooled.sourcePointerEvent = pointerEvent;
			}
			return pooled;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004B516 File Offset: 0x00049716
		protected MouseEventBase()
		{
			this.LocalInit();
		}
	}
}
