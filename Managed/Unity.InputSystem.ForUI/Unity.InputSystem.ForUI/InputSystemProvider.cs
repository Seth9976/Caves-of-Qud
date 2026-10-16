using System;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine.InputForUI;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Plugins.InputForUI
{
	// Token: 0x02000003 RID: 3
	internal class InputSystemProvider : IEventProviderImpl
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		static InputSystemProvider()
		{
			EventProvider.SetInputSystemProvider(new InputSystemProvider());
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Bootstrap()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C9 File Offset: 0x000002C9
		private EventModifiers m_EventModifiers
		{
			get
			{
				return this.m_InputEventPartialProvider._eventModifiers;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		private DiscreteTime m_CurrentTime
		{
			get
			{
				return (DiscreteTime)Time.timeAsRational;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Initialize()
		{
			if (this.m_InputEventPartialProvider == null)
			{
				this.m_InputEventPartialProvider = new InputEventPartialProvider();
			}
			this.m_InputEventPartialProvider.Initialize();
			this.m_Events.Clear();
			this.m_MouseState.Reset();
			this.m_PenState.Reset();
			this.m_SeenPenEvents = false;
			this.m_TouchState.Reset();
			this.m_SeenTouchEvents = false;
			this.SelectInputActionAsset();
			this.RegisterActions();
			this.RegisterFixedActions();
			InputSystem.onActionsChange += this.OnActionsChange;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public void Shutdown()
		{
			this.UnregisterActions();
			this.UnregisterFixedActions();
			this.m_InputEventPartialProvider.Shutdown();
			this.m_InputEventPartialProvider = null;
			if (this.m_DefaultInputActions != null)
			{
				this.m_DefaultInputActions.Dispose();
				this.m_DefaultInputActions = null;
			}
			InputSystem.onActionsChange -= this.OnActionsChange;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C2 File Offset: 0x000003C2
		public void OnActionsChange()
		{
			this.UnregisterActions();
			this.SelectInputActionAsset();
			this.RegisterActions();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D8 File Offset: 0x000003D8
		public void Update()
		{
			this.m_InputEventPartialProvider.Update();
			this.m_Events.Sort((Event a, Event b) => InputSystemProvider.SortEvents(a, b));
			DiscreteTime discreteTime = (DiscreteTime)Time.timeAsRational;
			this.DirectionNavigation(discreteTime);
			foreach (Event @event in this.m_Events)
			{
				if (this.m_SeenTouchEvents && @event.type == Event.Type.PointerEvent && @event.eventSource == EventSource.Pen)
				{
					this.m_PenState.Reset();
				}
				else if ((this.m_SeenTouchEvents || this.m_SeenPenEvents) && @event.type == Event.Type.PointerEvent && (@event.eventSource == EventSource.Mouse || @event.eventSource == EventSource.Unspecified))
				{
					this.m_MouseState.Reset();
				}
				else
				{
					EventProvider.Dispatch(in @event);
				}
			}
			if (this.m_ResetSeenEventsOnUpdate)
			{
				this.ResetSeenEvents();
				this.m_ResetSeenEventsOnUpdate = false;
			}
			this.m_Events.Clear();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022F8 File Offset: 0x000004F8
		private void ResetSeenEvents()
		{
			this.m_SeenTouchEvents = false;
			this.m_SeenPenEvents = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002308 File Offset: 0x00000508
		public bool ActionAssetIsNotNull()
		{
			return this.m_InputActionAsset != null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
		private void DirectionNavigation(DiscreteTime currentTime)
		{
			ValueTuple<Vector2, bool> valueTuple = this.ReadCurrentNavigationMoveVector();
			Vector2 item = valueTuple.Item1;
			bool flag = valueTuple.Item2;
			NavigationEvent.Direction direction = NavigationEvent.DetermineMoveDirection(item, 0.6f);
			if (direction == NavigationEvent.Direction.None)
			{
				direction = this.ReadNextPreviousDirection();
				flag = this.m_NextPreviousAction.WasPressedThisFrame();
			}
			if (direction == NavigationEvent.Direction.None)
			{
				this.m_RepeatHelper.Reset();
				return;
			}
			if (this.m_RepeatHelper.ShouldSendMoveEvent(currentTime, direction, flag))
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = direction,
					timestamp = currentTime,
					eventSource = this.GetEventSource(this.GetActiveDeviceFromDirection(direction)),
					playerId = 0U,
					eventModifiers = this.m_EventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023D4 File Offset: 0x000005D4
		private InputDevice GetActiveDeviceFromDirection(NavigationEvent.Direction direction)
		{
			switch (direction)
			{
			case NavigationEvent.Direction.Left:
			case NavigationEvent.Direction.Up:
			case NavigationEvent.Direction.Right:
			case NavigationEvent.Direction.Down:
				if (this.m_MoveAction != null)
				{
					return this.m_MoveAction.activeControl.device;
				}
				break;
			case NavigationEvent.Direction.Next:
			case NavigationEvent.Direction.Previous:
				if (this.m_NextPreviousAction != null)
				{
					return this.m_NextPreviousAction.activeControl.device;
				}
				break;
			}
			return Keyboard.current;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000243C File Offset: 0x0000063C
		private ValueTuple<Vector2, bool> ReadCurrentNavigationMoveVector()
		{
			if (this.m_MoveAction == null)
			{
				return new ValueTuple<Vector2, bool>(default(Vector2), false);
			}
			Vector2 vector = this.m_MoveAction.ReadValue<Vector2>();
			bool flag = this.m_MoveAction.WasPressedThisFrame();
			return new ValueTuple<Vector2, bool>(vector, flag);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002480 File Offset: 0x00000680
		private NavigationEvent.Direction ReadNextPreviousDirection()
		{
			if (!this.m_NextPreviousAction.IsPressed() || !(this.m_NextPreviousAction.activeControl.device is Keyboard))
			{
				return NavigationEvent.Direction.None;
			}
			if (!(this.m_NextPreviousAction.activeControl.device as Keyboard).shiftKey.isPressed)
			{
				return NavigationEvent.Direction.Next;
			}
			return NavigationEvent.Direction.Previous;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024D7 File Offset: 0x000006D7
		private static int SortEvents(Event a, Event b)
		{
			return Event.CompareType(a, b);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024E0 File Offset: 0x000006E0
		public void OnFocusChanged(bool focus)
		{
			this.m_InputEventPartialProvider.OnFocusChanged(focus);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024F0 File Offset: 0x000006F0
		public bool RequestCurrentState(Event.Type type)
		{
			if (this.m_InputEventPartialProvider.RequestCurrentState(type))
			{
				return true;
			}
			if (type != Event.Type.PointerEvent)
			{
				if (type != Event.Type.IMECompositionEvent)
				{
				}
				return false;
			}
			if (this.m_TouchState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_TouchState, EventSource.Touch));
				EventProvider.Dispatch(in @event);
			}
			if (this.m_PenState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_PenState, EventSource.Pen));
				EventProvider.Dispatch(in @event);
			}
			if (this.m_MouseState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_MouseState, EventSource.Mouse));
				EventProvider.Dispatch(in @event);
			}
			return this.m_TouchState.LastPositionValid || this.m_PenState.LastPositionValid || this.m_MouseState.LastPositionValid;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025CD File Offset: 0x000007CD
		public uint playerCount
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025D0 File Offset: 0x000007D0
		internal static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			int num = Screen.height;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				num = Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)num - position.y;
			return position;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002610 File Offset: 0x00000810
		private PointerEvent ToPointerStateEvent(DiscreteTime currentTime, in PointerState state, EventSource eventSource)
		{
			PointerEvent pointerEvent = default(PointerEvent);
			pointerEvent.type = PointerEvent.Type.State;
			pointerEvent.pointerIndex = 0;
			pointerEvent.position = state.LastPosition;
			pointerEvent.deltaPosition = Vector2.zero;
			pointerEvent.scroll = Vector2.zero;
			pointerEvent.displayIndex = state.LastDisplayIndex;
			pointerEvent.button = PointerEvent.Button.None;
			PointerState pointerState = state;
			pointerEvent.buttonsState = pointerState.ButtonsState;
			pointerEvent.clickCount = 0;
			pointerEvent.timestamp = currentTime;
			pointerEvent.eventSource = eventSource;
			pointerEvent.playerId = 0U;
			pointerEvent.eventModifiers = this.m_EventModifiers;
			return pointerEvent;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026B4 File Offset: 0x000008B4
		private EventSource GetEventSource(InputAction.CallbackContext ctx)
		{
			InputDevice device = ctx.control.device;
			return this.GetEventSource(device);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026D5 File Offset: 0x000008D5
		private EventSource GetEventSource(InputDevice device)
		{
			if (device is Touchscreen)
			{
				return EventSource.Touch;
			}
			if (device is Pen)
			{
				return EventSource.Pen;
			}
			if (device is Mouse)
			{
				return EventSource.Mouse;
			}
			if (device is Keyboard)
			{
				return EventSource.Keyboard;
			}
			if (device is Gamepad)
			{
				return EventSource.Gamepad;
			}
			return EventSource.Unspecified;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000270A File Offset: 0x0000090A
		private ref PointerState GetPointerStateForSource(EventSource eventSource)
		{
			if (eventSource == EventSource.Pen)
			{
				return ref this.m_PenState;
			}
			if (eventSource == EventSource.Touch)
			{
				return ref this.m_TouchState;
			}
			return ref this.m_MouseState;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002728 File Offset: 0x00000928
		private void DispatchFromCallback(in Event ev)
		{
			this.m_Events.Add(ev);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000273C File Offset: 0x0000093C
		private static int FindTouchFingerIndex(Touchscreen touchscreen, InputAction.CallbackContext ctx)
		{
			if (touchscreen == null)
			{
				return 0;
			}
			Vector2Control vector2Control = ((ctx.control is Vector2Control) ? ((Vector2Control)ctx.control) : null);
			TouchPressControl touchPressControl = ((ctx.control is TouchPressControl) ? ((TouchPressControl)ctx.control) : null);
			TouchControl touchControl = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null);
			for (int i = 0; i < touchscreen.touches.Count; i++)
			{
				if (vector2Control != null && vector2Control == touchscreen.touches[i].position)
				{
					return i;
				}
				if (touchPressControl != null && touchPressControl == touchscreen.touches[i].press)
				{
					return i;
				}
				if (touchControl != null && touchControl == touchscreen.touches[i])
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002818 File Offset: 0x00000A18
		private void OnPointerPerformed(InputAction.CallbackContext ctx)
		{
			EventSource eventSource = this.GetEventSource(ctx);
			ref PointerState pointerStateForSource = ref this.GetPointerStateForSource(eventSource);
			Pointer pointer = ((ctx.control.device is Pointer) ? ((Pointer)ctx.control.device) : null);
			Pen pen = ((ctx.control.device is Pen) ? ((Pen)ctx.control.device) : null);
			Touchscreen touchscreen = ((ctx.control.device is Touchscreen) ? ((Touchscreen)ctx.control.device) : null);
			TouchControl touchControl = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null);
			int num = InputSystemProvider.FindTouchFingerIndex(touchscreen, ctx);
			this.m_ResetSeenEventsOnUpdate = false;
			if (touchControl != null || touchscreen != null)
			{
				this.m_SeenTouchEvents = true;
			}
			else if (pen != null)
			{
				this.m_SeenPenEvents = true;
			}
			Vector2 vector = ctx.ReadValue<Vector2>();
			int num2 = ((pointer != null) ? pointer.displayIndex.ReadValue() : ((touchscreen != null) ? touchscreen.displayIndex.ReadValue() : ((pen != null) ? pen.displayIndex.ReadValue() : 0)));
			Vector2 vector2 = InputSystemProvider.ScreenBottomLeftToPanelPosition(vector, num2);
			Vector2 vector3 = (pointerStateForSource.LastPositionValid ? (vector2 - pointerStateForSource.LastPosition) : Vector2.zero);
			Vector2 vector4 = ((pen != null) ? pen.tilt.ReadValue() : Vector2.zero);
			float num3 = ((pen != null) ? pen.twist.ReadValue() : 0f);
			float num4 = ((pen != null) ? pen.pressure.ReadValue() : ((touchControl != null) ? touchControl.pressure.ReadValue() : 0f));
			bool flag = pen != null && pen.eraser.isPressed;
			if (vector3.sqrMagnitude >= 0.01f)
			{
				Event @event = Event.From(new PointerEvent
				{
					type = PointerEvent.Type.PointerMoved,
					pointerIndex = num,
					position = vector2,
					deltaPosition = vector3,
					scroll = Vector2.zero,
					displayIndex = num2,
					tilt = vector4,
					twist = num3,
					pressure = num4,
					isInverted = flag,
					button = PointerEvent.Button.None,
					buttonsState = pointerStateForSource.ButtonsState,
					clickCount = 0,
					timestamp = this.m_CurrentTime,
					eventSource = eventSource,
					playerId = 0U,
					eventModifiers = this.m_EventModifiers
				});
				this.DispatchFromCallback(in @event);
				pointerStateForSource.OnMove(this.m_CurrentTime, vector2, num2);
				return;
			}
			if (!pointerStateForSource.LastPositionValid)
			{
				pointerStateForSource.OnMove(this.m_CurrentTime, vector2, num2);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002ABC File Offset: 0x00000CBC
		private void OnSubmitPerformed(InputAction.CallbackContext ctx)
		{
			Event @event = Event.From(new NavigationEvent
			{
				type = NavigationEvent.Type.Submit,
				direction = NavigationEvent.Direction.None,
				timestamp = this.m_CurrentTime,
				eventSource = this.GetEventSource(ctx),
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B20 File Offset: 0x00000D20
		private void OnCancelPerformed(InputAction.CallbackContext ctx)
		{
			Event @event = Event.From(new NavigationEvent
			{
				type = NavigationEvent.Type.Cancel,
				direction = NavigationEvent.Direction.None,
				timestamp = this.m_CurrentTime,
				eventSource = this.GetEventSource(ctx),
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B84 File Offset: 0x00000D84
		private void OnClickPerformed(InputAction.CallbackContext ctx, EventSource eventSource, PointerEvent.Button button)
		{
			ref PointerState pointerStateForSource = ref this.GetPointerStateForSource(eventSource);
			Touchscreen touchscreen = ((ctx.control.device is Touchscreen) ? ((Touchscreen)ctx.control.device) : null);
			bool flag = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null) != null;
			int num = InputSystemProvider.FindTouchFingerIndex(touchscreen, ctx);
			this.m_ResetSeenEventsOnUpdate = true;
			if (flag || touchscreen != null)
			{
				this.m_SeenTouchEvents = true;
			}
			bool flag2 = pointerStateForSource.ButtonsState.Get(button);
			bool flag3 = ctx.ReadValueAsButton();
			pointerStateForSource.OnButtonChange(this.m_CurrentTime, button, flag2, flag3);
			Event @event = Event.From(new PointerEvent
			{
				type = (flag3 ? PointerEvent.Type.ButtonPressed : PointerEvent.Type.ButtonReleased),
				pointerIndex = num,
				position = pointerStateForSource.LastPosition,
				deltaPosition = Vector2.zero,
				scroll = Vector2.zero,
				displayIndex = pointerStateForSource.LastDisplayIndex,
				tilt = Vector2.zero,
				twist = 0f,
				pressure = 0f,
				isInverted = false,
				button = button,
				buttonsState = pointerStateForSource.ButtonsState,
				clickCount = pointerStateForSource.ClickCount,
				timestamp = this.m_CurrentTime,
				eventSource = eventSource,
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CFD File Offset: 0x00000EFD
		private void OnLeftClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.Primary);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D0E File Offset: 0x00000F0E
		private void OnMiddleClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.PenBarrelButton);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D1F File Offset: 0x00000F1F
		private void OnRightClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.PenEraserInTouch);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D30 File Offset: 0x00000F30
		private void OnScrollWheelPerformed(InputAction.CallbackContext ctx)
		{
			Vector2 vector = ctx.ReadValue<Vector2>() / InputSystem.scrollWheelDeltaPerTick;
			if (vector.sqrMagnitude < 0.01f)
			{
				return;
			}
			EventSource eventSource = this.GetEventSource(ctx);
			ref PointerState pointerStateForSource = ref this.GetPointerStateForSource(eventSource);
			Vector2 vector2 = Vector2.zero;
			int num = 0;
			if (pointerStateForSource.LastPositionValid)
			{
				vector2 = pointerStateForSource.LastPosition;
				num = pointerStateForSource.LastDisplayIndex;
			}
			else if (eventSource == EventSource.Mouse && Mouse.current != null)
			{
				vector2 = Mouse.current.position.ReadValue();
				num = Mouse.current.displayIndex.ReadValue();
			}
			Vector2 vector3 = new Vector2
			{
				x = vector.x * 3f,
				y = -vector.y * 3f
			};
			Event @event = Event.From(new PointerEvent
			{
				type = PointerEvent.Type.Scroll,
				pointerIndex = 0,
				position = vector2,
				deltaPosition = Vector2.zero,
				scroll = vector3,
				displayIndex = num,
				tilt = Vector2.zero,
				twist = 0f,
				pressure = 0f,
				isInverted = false,
				button = PointerEvent.Button.None,
				buttonsState = pointerStateForSource.ButtonsState,
				clickCount = 0,
				timestamp = this.m_CurrentTime,
				eventSource = EventSource.Mouse,
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002EB1 File Offset: 0x000010B1
		private void RegisterFixedActions()
		{
			this.m_NextPreviousAction = new InputAction("nextPreviousAction", InputActionType.Button, null, null, null, null);
			this.m_NextPreviousAction.AddBinding("<Keyboard>/tab", null, null, null);
			this.m_NextPreviousAction.Enable();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EE7 File Offset: 0x000010E7
		private void UnregisterFixedActions()
		{
			if (this.m_NextPreviousAction != null)
			{
				this.m_NextPreviousAction.Disable();
				this.m_NextPreviousAction = null;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F04 File Offset: 0x00001104
		private InputAction FindActionAndRegisterCallback(string actionNameOrId, Action<InputAction.CallbackContext> callback = null)
		{
			InputAction inputAction = this.m_InputActionAsset.FindAction(actionNameOrId, false);
			if (inputAction != null && callback != null)
			{
				inputAction.performed += callback;
			}
			return inputAction;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F30 File Offset: 0x00001130
		private void RegisterActions()
		{
			Action<InputActionAsset> action = InputSystemProvider.s_OnRegisterActions;
			if (action != null)
			{
				action(this.m_InputActionAsset);
			}
			this.m_PointAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.PointAction, new Action<InputAction.CallbackContext>(this.OnPointerPerformed));
			this.m_MoveAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.MoveAction, null);
			this.m_SubmitAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.SubmitAction, new Action<InputAction.CallbackContext>(this.OnSubmitPerformed));
			this.m_CancelAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.CancelAction, new Action<InputAction.CallbackContext>(this.OnCancelPerformed));
			this.m_LeftClickAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.LeftClickAction, new Action<InputAction.CallbackContext>(this.OnLeftClickPerformed));
			this.m_MiddleClickAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.MiddleClickAction, new Action<InputAction.CallbackContext>(this.OnMiddleClickPerformed));
			this.m_RightClickAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.RightClickAction, new Action<InputAction.CallbackContext>(this.OnRightClickPerformed));
			this.m_ScrollWheelAction = this.FindActionAndRegisterCallback(InputSystemProvider.Actions.ScrollWheelAction, new Action<InputAction.CallbackContext>(this.OnScrollWheelPerformed));
			if (InputSystem.actions == null)
			{
				this.m_InputActionAsset.FindActionMap("UI", true).Enable();
				return;
			}
			this.m_InputActionAsset.Enable();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000305F File Offset: 0x0000125F
		private void UnregisterAction(ref InputAction action, Action<InputAction.CallbackContext> callback = null)
		{
			if (action != null && callback != null)
			{
				action.performed -= callback;
			}
			action = null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003074 File Offset: 0x00001274
		private void UnregisterActions()
		{
			this.UnregisterAction(ref this.m_PointAction, new Action<InputAction.CallbackContext>(this.OnPointerPerformed));
			this.UnregisterAction(ref this.m_MoveAction, null);
			this.UnregisterAction(ref this.m_SubmitAction, new Action<InputAction.CallbackContext>(this.OnSubmitPerformed));
			this.UnregisterAction(ref this.m_CancelAction, new Action<InputAction.CallbackContext>(this.OnCancelPerformed));
			this.UnregisterAction(ref this.m_LeftClickAction, new Action<InputAction.CallbackContext>(this.OnLeftClickPerformed));
			this.UnregisterAction(ref this.m_MiddleClickAction, new Action<InputAction.CallbackContext>(this.OnMiddleClickPerformed));
			this.UnregisterAction(ref this.m_RightClickAction, new Action<InputAction.CallbackContext>(this.OnRightClickPerformed));
			this.UnregisterAction(ref this.m_ScrollWheelAction, new Action<InputAction.CallbackContext>(this.OnScrollWheelPerformed));
			if (this.m_InputActionAsset != null)
			{
				this.m_InputActionAsset.Disable();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003150 File Offset: 0x00001350
		private void SelectInputActionAsset()
		{
			InputActionAsset actions = InputSystem.actions;
			if (actions != null && actions.FindActionMap("UI", false) != null)
			{
				this.m_InputActionAsset = InputSystem.actions;
				return;
			}
			if (this.m_DefaultInputActions == null)
			{
				this.m_DefaultInputActions = new DefaultInputActions();
			}
			this.m_InputActionAsset = this.m_DefaultInputActions.asset;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000031B0 File Offset: 0x000013B0
		internal static void SetOnRegisterActions(Action<InputActionAsset> callback)
		{
			InputSystemProvider.s_OnRegisterActions = callback;
		}

		// Token: 0x04000001 RID: 1
		private InputEventPartialProvider m_InputEventPartialProvider;

		// Token: 0x04000002 RID: 2
		private DefaultInputActions m_DefaultInputActions;

		// Token: 0x04000003 RID: 3
		private InputActionAsset m_InputActionAsset;

		// Token: 0x04000004 RID: 4
		private InputAction m_PointAction;

		// Token: 0x04000005 RID: 5
		private InputAction m_MoveAction;

		// Token: 0x04000006 RID: 6
		private InputAction m_SubmitAction;

		// Token: 0x04000007 RID: 7
		private InputAction m_CancelAction;

		// Token: 0x04000008 RID: 8
		private InputAction m_LeftClickAction;

		// Token: 0x04000009 RID: 9
		private InputAction m_MiddleClickAction;

		// Token: 0x0400000A RID: 10
		private InputAction m_RightClickAction;

		// Token: 0x0400000B RID: 11
		private InputAction m_ScrollWheelAction;

		// Token: 0x0400000C RID: 12
		private InputAction m_NextPreviousAction;

		// Token: 0x0400000D RID: 13
		private List<Event> m_Events = new List<Event>();

		// Token: 0x0400000E RID: 14
		private PointerState m_MouseState;

		// Token: 0x0400000F RID: 15
		private PointerState m_PenState;

		// Token: 0x04000010 RID: 16
		private bool m_SeenPenEvents;

		// Token: 0x04000011 RID: 17
		private PointerState m_TouchState;

		// Token: 0x04000012 RID: 18
		private bool m_SeenTouchEvents;

		// Token: 0x04000013 RID: 19
		private const float k_SmallestReportedMovementSqrDist = 0.01f;

		// Token: 0x04000014 RID: 20
		private NavigationEventRepeatHelper m_RepeatHelper = new NavigationEventRepeatHelper();

		// Token: 0x04000015 RID: 21
		private bool m_ResetSeenEventsOnUpdate;

		// Token: 0x04000016 RID: 22
		private const float kScrollUGUIScaleFactor = 3f;

		// Token: 0x04000017 RID: 23
		private static Action<InputActionAsset> s_OnRegisterActions;

		// Token: 0x04000018 RID: 24
		private const uint k_DefaultPlayerId = 0U;

		// Token: 0x02000006 RID: 6
		public static class Actions
		{
			// Token: 0x04000020 RID: 32
			public static readonly string PointAction = "UI/Point";

			// Token: 0x04000021 RID: 33
			public static readonly string MoveAction = "UI/Navigate";

			// Token: 0x04000022 RID: 34
			public static readonly string SubmitAction = "UI/Submit";

			// Token: 0x04000023 RID: 35
			public static readonly string CancelAction = "UI/Cancel";

			// Token: 0x04000024 RID: 36
			public static readonly string LeftClickAction = "UI/Click";

			// Token: 0x04000025 RID: 37
			public static readonly string MiddleClickAction = "UI/MiddleClick";

			// Token: 0x04000026 RID: 38
			public static readonly string RightClickAction = "UI/RightClick";

			// Token: 0x04000027 RID: 39
			public static readonly string ScrollWheelAction = "UI/ScrollWheel";
		}
	}
}
