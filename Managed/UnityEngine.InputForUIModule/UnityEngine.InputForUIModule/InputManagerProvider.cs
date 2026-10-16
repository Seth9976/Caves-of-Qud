using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000026 RID: 38
	internal class InputManagerProvider : IEventProviderImpl
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003D60 File Offset: 0x00001F60
		private EventModifiers _eventModifiers
		{
			get
			{
				return this._inputEventPartialProvider._eventModifiers;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D70 File Offset: 0x00001F70
		public InputManagerProvider()
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal InputManagerProvider(InputManagerProvider.IInput inputOverride, InputManagerProvider.ITime timeOverride)
		{
			this._input = inputOverride;
			this._time = timeOverride;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E30 File Offset: 0x00002030
		public void Initialize()
		{
			if (this._inputEventPartialProvider == null)
			{
				this._inputEventPartialProvider = new InputEventPartialProvider();
			}
			this._inputEventPartialProvider.Initialize();
			this._inputEventPartialProvider._sendNavigationEventOnTabKey = true;
			this._mouseState.Reset();
			this._isPenPresent = false;
			this._seenAtLeastOnePenPosition = false;
			this._lastSeenPenPositionForDetection = default(Vector2);
			this._penState.Reset();
			this._lastPenData = default(PenData);
			this._touchFingerIdToFingerIndex.Clear();
			this._touchNextFingerIndex = 0;
			this._touchState.Reset();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003550 File Offset: 0x00001750
		public void Shutdown()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003EC8 File Offset: 0x000020C8
		public void Update()
		{
			this._inputEventPartialProvider.Update();
			DiscreteTime discreteTime = (DiscreteTime)this._time.timeAsRational;
			this.DetectPen();
			bool flag = false;
			bool touchSupported = this._input.touchSupported;
			if (touchSupported)
			{
				flag = this.CheckTouchEvents(discreteTime);
			}
			bool flag2 = false;
			bool flag3 = !flag && this._isPenPresent;
			if (flag3)
			{
				DiscreteTime discreteTime2 = discreteTime;
				PenData lastPenContactEvent = this._input.GetLastPenContactEvent();
				flag2 = this.CheckPenEvent(discreteTime2, in lastPenContactEvent);
			}
			else
			{
				this._penState.Reset();
			}
			bool flag4 = !flag2 && !flag && this._input.mousePresent;
			if (flag4)
			{
				this.CheckMouseEvents(discreteTime, false);
			}
			else
			{
				this.CheckMouseEvents(discreteTime, true);
				this._mouseState.LastPositionValid = false;
			}
			bool mousePresent = this._input.mousePresent;
			if (mousePresent)
			{
				this.CheckMouseScroll(discreteTime);
			}
			this.CheckIfIMEChanged(discreteTime);
			this.DirectionNavigation(discreteTime);
			this.SubmitCancelNavigation(discreteTime);
			this.NextPreviousNavigation(discreteTime);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003FC4 File Offset: 0x000021C4
		private bool CheckTouchEvents(DiscreteTime currentTime)
		{
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < this._input.touchCount; i++)
			{
				Touch touch = this._input.GetTouch(i);
				bool flag3 = touch.type == TouchType.Indirect || touch.phase == TouchPhase.Stationary;
				if (!flag3)
				{
					int num;
					bool flag4 = !this._touchFingerIdToFingerIndex.TryGetValue(touch.fingerId, out num);
					if (flag4)
					{
						int touchNextFingerIndex = this._touchNextFingerIndex;
						this._touchNextFingerIndex = touchNextFingerIndex + 1;
						num = touchNextFingerIndex;
						this._touchFingerIdToFingerIndex.Add(touch.fingerId, num);
					}
					int num2;
					Vector2 vector = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(touch.position, out num2);
					Vector2 vector2 = InputManagerProvider.ScreenBottomLeftToPanelDelta(touch.deltaPosition);
					PointerEvent.Type type = PointerEvent.Type.PointerMoved;
					PointerEvent.Button button = PointerEvent.Button.None;
					switch (touch.phase)
					{
					case TouchPhase.Began:
						type = PointerEvent.Type.ButtonPressed;
						button = PointerEvent.Button.Primary;
						flag = false;
						this._touchState.OnButtonDown(currentTime, button);
						break;
					case TouchPhase.Moved:
						flag = false;
						break;
					case TouchPhase.Ended:
						type = PointerEvent.Type.ButtonReleased;
						button = PointerEvent.Button.Primary;
						this._touchState.OnButtonUp(currentTime, button);
						break;
					case TouchPhase.Canceled:
						type = PointerEvent.Type.TouchCanceled;
						button = PointerEvent.Button.Primary;
						this._touchState.OnButtonUp(currentTime, button);
						break;
					}
					Event @event = Event.From(new PointerEvent
					{
						type = type,
						pointerIndex = num,
						position = vector,
						deltaPosition = vector2,
						scroll = Vector2.zero,
						displayIndex = num2,
						tilt = InputManagerProvider.AzimuthAndAlitutudeToTilt(touch.altitudeAngle, touch.azimuthAngle),
						twist = 0f,
						pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > Mathf.Epsilon) ? (touch.pressure / touch.maximumPossiblePressure) : 1f),
						isInverted = false,
						button = button,
						buttonsState = this._touchState.ButtonsState,
						clickCount = this._touchState.ClickCount,
						timestamp = currentTime,
						eventSource = EventSource.Touch,
						playerId = 0U,
						eventModifiers = this._eventModifiers
					});
					EventProvider.Dispatch(in @event);
					flag2 = true;
				}
			}
			bool flag5 = flag;
			if (flag5)
			{
				this._touchNextFingerIndex = 0;
				this._touchFingerIdToFingerIndex.Clear();
			}
			return flag2;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000423C File Offset: 0x0000243C
		private void DetectPen()
		{
			bool isPenPresent = this._isPenPresent;
			if (!isPenPresent)
			{
				Vector2 position = this._input.GetLastPenContactEvent().position;
				bool seenAtLeastOnePenPosition = this._seenAtLeastOnePenPosition;
				if (seenAtLeastOnePenPosition)
				{
					float sqrMagnitude = (position - this._lastSeenPenPositionForDetection).sqrMagnitude;
					this._isPenPresent = sqrMagnitude >= 0.01f;
				}
				else
				{
					this._lastSeenPenPositionForDetection = position;
					this._seenAtLeastOnePenPosition = true;
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000042AC File Offset: 0x000024AC
		private static PointerEvent.Button PenStatusToButton(PenStatus status)
		{
			bool flag = (status & PenStatus.Eraser) > PenStatus.None;
			PointerEvent.Button button;
			if (flag)
			{
				button = PointerEvent.Button.PenEraserInTouch;
			}
			else
			{
				bool flag2 = (status & PenStatus.Barrel) > PenStatus.None;
				if (flag2)
				{
					button = PointerEvent.Button.PenBarrelButton;
				}
				else
				{
					button = PointerEvent.Button.Primary;
				}
			}
			return button;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000042DC File Offset: 0x000024DC
		private bool CheckPenEvent(DiscreteTime currentTime, in PenData currentPenData)
		{
			Vector2 position = currentPenData.position;
			int num = 0;
			Vector2 vector = (this._penState.LastPositionValid ? (position - this._penState.LastPosition) : Vector2.zero);
			PointerEvent.Button button = PointerEvent.Button.None;
			bool flag = currentPenData.contactType != this._lastPenData.contactType;
			PointerEvent.Type type;
			if (flag)
			{
				PenEventType contactType = currentPenData.contactType;
				PenEventType penEventType = contactType;
				if (penEventType != PenEventType.PenDown)
				{
					if (penEventType != PenEventType.PenUp)
					{
						type = PointerEvent.Type.PointerMoved;
					}
					else
					{
						type = PointerEvent.Type.ButtonReleased;
						button = InputManagerProvider.PenStatusToButton(this._lastPenData.penStatus);
						this._penState.OnButtonUp(currentTime, button);
					}
				}
				else
				{
					type = PointerEvent.Type.ButtonPressed;
					button = InputManagerProvider.PenStatusToButton(currentPenData.penStatus);
					this._penState.OnButtonDown(currentTime, button);
				}
			}
			else
			{
				type = PointerEvent.Type.PointerMoved;
			}
			this._lastPenData = currentPenData;
			bool flag2 = false;
			bool flag3 = type != PointerEvent.Type.PointerMoved || !this._penState.LastPositionValid || vector.sqrMagnitude >= 0.01f;
			if (flag3)
			{
				Event @event = Event.From(new PointerEvent
				{
					type = type,
					pointerIndex = 0,
					position = position,
					deltaPosition = vector,
					scroll = Vector2.zero,
					displayIndex = num,
					tilt = currentPenData.tilt,
					twist = currentPenData.twist,
					pressure = currentPenData.pressure,
					isInverted = ((currentPenData.penStatus & PenStatus.Inverted) > PenStatus.None),
					button = button,
					buttonsState = this._penState.ButtonsState,
					clickCount = this._penState.ClickCount,
					timestamp = currentTime,
					eventSource = EventSource.Pen,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
				flag2 = true;
			}
			this._penState.OnMove(currentTime, position, num);
			return flag2;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000044D8 File Offset: 0x000026D8
		private void CheckMouseEvents(DiscreteTime currentTime, bool muted = false)
		{
			int num;
			Vector2 vector = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(this._input.mousePosition, out num);
			bool lastPositionValid = this._mouseState.LastPositionValid;
			if (lastPositionValid)
			{
				Vector2 vector2 = vector - this._mouseState.LastPosition;
				bool flag = vector2.sqrMagnitude >= 0.01f;
				if (flag)
				{
					bool flag2 = !muted;
					if (flag2)
					{
						PointerEvent pointerEvent = new PointerEvent
						{
							type = PointerEvent.Type.PointerMoved,
							pointerIndex = 0,
							position = vector,
							deltaPosition = vector2,
							scroll = Vector2.zero,
							displayIndex = num,
							tilt = Vector2.zero,
							twist = 0f,
							pressure = 0f,
							isInverted = false,
							button = PointerEvent.Button.None,
							buttonsState = this._mouseState.ButtonsState,
							clickCount = 0,
							timestamp = currentTime,
							eventSource = EventSource.Mouse,
							playerId = 0U,
							eventModifiers = this._eventModifiers
						};
						Event @event = Event.From(pointerEvent);
						EventProvider.Dispatch(in @event);
					}
					this._mouseState.OnMove(currentTime, vector, num);
				}
			}
			else
			{
				this._mouseState.OnMove(currentTime, vector, num);
			}
			for (int i = 0; i < 5; i++)
			{
				PointerEvent.Button button = PointerEvent.ButtonFromButtonIndex(i);
				bool flag3 = this._mouseState.ButtonsState.Get(button);
				bool mouseButtonDown = this._input.GetMouseButtonDown(i);
				bool mouseButtonUp = this._input.GetMouseButtonUp(i);
				bool mouseButton = this._input.GetMouseButton(i);
				InputManagerProvider.ButtonEventsIterator buttonEventsIterator = InputManagerProvider.ButtonEventsIterator.FromState(flag3, mouseButtonDown, mouseButtonUp, mouseButton);
				bool flag4 = flag3;
				while (buttonEventsIterator.MoveNext())
				{
					bool flag5 = buttonEventsIterator.Current;
					this._mouseState.OnButtonChange(currentTime, button, flag4, flag5);
					flag4 = buttonEventsIterator.Current;
					bool flag6 = !muted;
					if (flag6)
					{
						PointerEvent pointerEvent = new PointerEvent
						{
							type = (buttonEventsIterator.Current ? PointerEvent.Type.ButtonPressed : PointerEvent.Type.ButtonReleased),
							pointerIndex = 0,
							position = this._mouseState.LastPosition,
							deltaPosition = Vector2.zero,
							scroll = Vector2.zero,
							displayIndex = this._mouseState.LastDisplayIndex,
							tilt = Vector2.zero,
							twist = 0f,
							pressure = 0f,
							isInverted = false,
							button = button,
							buttonsState = this._mouseState.ButtonsState,
							clickCount = this._mouseState.ClickCount,
							timestamp = currentTime,
							eventSource = EventSource.Mouse,
							playerId = 0U,
							eventModifiers = this._eventModifiers
						};
						Event @event = Event.From(pointerEvent);
						EventProvider.Dispatch(in @event);
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000047E8 File Offset: 0x000029E8
		private void CheckMouseScroll(DiscreteTime currentTime)
		{
			Vector2 mouseScrollDelta = this._input.mouseScrollDelta;
			bool flag = mouseScrollDelta.sqrMagnitude < 0.01f;
			if (!flag)
			{
				int num = 0;
				bool lastPositionValid = this._mouseState.LastPositionValid;
				Vector2 vector;
				if (lastPositionValid)
				{
					vector = this._mouseState.LastPosition;
					num = this._mouseState.LastDisplayIndex;
				}
				else
				{
					vector = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(this._input.mousePosition, out num);
				}
				mouseScrollDelta.x *= 3f;
				mouseScrollDelta.y *= -3f;
				Event @event = Event.From(new PointerEvent
				{
					type = PointerEvent.Type.Scroll,
					pointerIndex = 0,
					position = vector,
					deltaPosition = Vector2.zero,
					scroll = mouseScrollDelta,
					displayIndex = num,
					tilt = Vector2.zero,
					twist = 0f,
					pressure = 0f,
					isInverted = false,
					button = PointerEvent.Button.None,
					buttonsState = this._mouseState.ButtonsState,
					clickCount = 0,
					timestamp = currentTime,
					eventSource = EventSource.Mouse,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004944 File Offset: 0x00002B44
		private PointerEvent ToPointerStateEvent(DiscreteTime currentTime, in PointerState state, EventSource eventSource)
		{
			PointerEvent pointerEvent = default(PointerEvent);
			pointerEvent.type = PointerEvent.Type.State;
			pointerEvent.pointerIndex = 0;
			pointerEvent.position = state.LastPosition;
			pointerEvent.deltaPosition = Vector2.zero;
			pointerEvent.scroll = Vector2.zero;
			pointerEvent.displayIndex = state.LastDisplayIndex;
			pointerEvent.tilt = ((eventSource == EventSource.Pen) ? this._lastPenData.tilt : Vector2.zero);
			pointerEvent.twist = ((eventSource == EventSource.Pen) ? this._lastPenData.twist : 0f);
			pointerEvent.pressure = ((eventSource == EventSource.Pen) ? this._lastPenData.pressure : 0f);
			pointerEvent.isInverted = eventSource == EventSource.Pen && (this._lastPenData.penStatus & PenStatus.Inverted) > PenStatus.None;
			pointerEvent.button = PointerEvent.Button.None;
			PointerState pointerState = state;
			pointerEvent.buttonsState = pointerState.ButtonsState;
			pointerEvent.clickCount = 0;
			pointerEvent.timestamp = currentTime;
			pointerEvent.eventSource = eventSource;
			pointerEvent.playerId = 0U;
			pointerEvent.eventModifiers = this._eventModifiers;
			return pointerEvent;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A64 File Offset: 0x00002C64
		private void NextPreviousNavigation(DiscreteTime currentTime)
		{
			int num = (this.InputManagerGetButtonDownOrDefault(this._configuration.NavigateNextButton) ? 1 : 0) + (this.InputManagerGetButtonDownOrDefault(this._configuration.NavigatePreviousButton) ? (-1) : 0);
			bool flag = num != 0;
			if (flag)
			{
				bool isShiftPressed = this._eventModifiers.isShiftPressed;
				if (isShiftPressed)
				{
					num = -num;
				}
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = ((num >= 0) ? NavigationEvent.Direction.Next : NavigationEvent.Direction.Previous),
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004B20 File Offset: 0x00002D20
		private void SubmitCancelNavigation(DiscreteTime currentTime)
		{
			bool flag = this.InputManagerGetButtonDownOrDefault(this._configuration.SubmitButton);
			if (flag)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Submit,
					direction = NavigationEvent.Direction.None,
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
			bool flag2 = this.InputManagerGetButtonDownOrDefault(this._configuration.CancelButton);
			if (flag2)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Cancel,
					direction = NavigationEvent.Direction.None,
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004C08 File Offset: 0x00002E08
		private void DirectionNavigation(DiscreteTime currentTime)
		{
			ValueTuple<Vector2, bool> valueTuple = this.ReadCurrentNavigationMoveVector();
			Vector2 item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			NavigationEvent.Direction direction = NavigationEvent.DetermineMoveDirection(item, 0.6f);
			bool flag = direction == NavigationEvent.Direction.None;
			if (flag)
			{
				this._navigationEventRepeatHelper.Reset();
			}
			else
			{
				bool flag2 = this._navigationEventRepeatHelper.ShouldSendMoveEvent(currentTime, direction, item2);
				if (flag2)
				{
					EventSource eventSource = this.GetEventSourceFromPressedKey();
					bool flag3 = eventSource == EventSource.Unspecified && !item2;
					if (flag3)
					{
						eventSource = EventSource.Gamepad;
					}
					Event @event = Event.From(new NavigationEvent
					{
						type = NavigationEvent.Type.Move,
						direction = direction,
						timestamp = currentTime,
						eventSource = eventSource,
						playerId = 0U,
						eventModifiers = this._eventModifiers
					});
					EventProvider.Dispatch(in @event);
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004CD4 File Offset: 0x00002ED4
		private void CheckIfIMEChanged(DiscreteTime currentTime)
		{
			string compositionString = this._input.compositionString;
			bool flag = this._compositionString != compositionString;
			if (flag)
			{
				this._compositionString = compositionString;
				Event @event = Event.From(this.ToIMECompositionEvent(currentTime, this._compositionString));
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D22 File Offset: 0x00002F22
		public void OnFocusChanged(bool focus)
		{
			this._inputEventPartialProvider.OnFocusChanged(focus);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D34 File Offset: 0x00002F34
		public bool RequestCurrentState(Event.Type type)
		{
			bool flag = this._inputEventPartialProvider.RequestCurrentState(type);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				DiscreteTime discreteTime = (DiscreteTime)this._time.timeAsRational;
				if (type != Event.Type.PointerEvent)
				{
					if (type != Event.Type.IMECompositionEvent)
					{
						flag2 = false;
					}
					else
					{
						Event @event = Event.From(this.ToIMECompositionEvent(discreteTime, this._compositionString));
						EventProvider.Dispatch(in @event);
						flag2 = true;
					}
				}
				else
				{
					bool lastPositionValid = this._touchState.LastPositionValid;
					if (lastPositionValid)
					{
						Event @event = Event.From(this.ToPointerStateEvent(discreteTime, in this._touchState, EventSource.Touch));
						EventProvider.Dispatch(in @event);
					}
					bool lastPositionValid2 = this._penState.LastPositionValid;
					if (lastPositionValid2)
					{
						Event @event = Event.From(this.ToPointerStateEvent(discreteTime, in this._penState, EventSource.Pen));
						EventProvider.Dispatch(in @event);
					}
					bool lastPositionValid3 = this._mouseState.LastPositionValid;
					if (lastPositionValid3)
					{
						Event @event = Event.From(this.ToPointerStateEvent(discreteTime, in this._mouseState, EventSource.Mouse));
						EventProvider.Dispatch(in @event);
					}
					flag2 = this._touchState.LastPositionValid || this._penState.LastPositionValid || this._mouseState.LastPositionValid;
				}
			}
			return flag2;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004E61 File Offset: 0x00003061
		public uint playerCount
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E64 File Offset: 0x00003064
		private EventSource GetEventSourceFromPressedKey()
		{
			bool flag = this.InputManagerKeyboardWasPressed();
			EventSource eventSource;
			if (flag)
			{
				eventSource = EventSource.Keyboard;
			}
			else
			{
				bool flag2 = this.InputManagerJoystickWasPressed();
				if (flag2)
				{
					eventSource = EventSource.Gamepad;
				}
				else
				{
					eventSource = EventSource.Unspecified;
				}
			}
			return eventSource;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004E94 File Offset: 0x00003094
		private bool InputManagerJoystickWasPressed()
		{
			for (KeyCode keyCode = KeyCode.Joystick1Button0; keyCode <= KeyCode.Joystick8Button19; keyCode++)
			{
				bool key = this._input.GetKey(keyCode);
				if (key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004EDC File Offset: 0x000030DC
		private bool InputManagerKeyboardWasPressed()
		{
			for (KeyCode keyCode = KeyCode.None; keyCode <= KeyCode.Menu; keyCode++)
			{
				bool key = this._input.GetKey(keyCode);
				if (key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004F20 File Offset: 0x00003120
		private float InputManagerGetAxisRawOrDefault(string axisName)
		{
			float num;
			try
			{
				num = ((!string.IsNullOrEmpty(axisName)) ? this._input.GetAxisRaw(axisName) : 0f);
			}
			catch
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004F68 File Offset: 0x00003168
		private bool InputManagerGetButtonDownOrDefault(string axisName)
		{
			bool flag;
			try
			{
				flag = !string.IsNullOrEmpty(axisName) && this._input.GetButtonDown(axisName);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004FB0 File Offset: 0x000031B0
		private ValueTuple<Vector2, bool> ReadCurrentNavigationMoveVector()
		{
			Vector2 vector = new Vector2(this.InputManagerGetAxisRawOrDefault(this._configuration.HorizontalAxis), this.InputManagerGetAxisRawOrDefault(this._configuration.VerticalAxis));
			bool flag = false;
			bool flag2 = this.InputManagerGetButtonDownOrDefault(this._configuration.HorizontalAxis);
			if (flag2)
			{
				bool flag3 = vector.x < 0f;
				if (flag3)
				{
					vector.x = -1f;
				}
				else
				{
					bool flag4 = vector.x > 0f;
					if (flag4)
					{
						vector.x = 1f;
					}
				}
				flag = true;
			}
			bool flag5 = this.InputManagerGetButtonDownOrDefault(this._configuration.VerticalAxis);
			if (flag5)
			{
				bool flag6 = vector.y < 0f;
				if (flag6)
				{
					vector.y = -1f;
				}
				else
				{
					bool flag7 = vector.y > 0f;
					if (flag7)
					{
						vector.y = 1f;
					}
				}
				flag = true;
			}
			return new ValueTuple<Vector2, bool>(vector, flag);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000050A8 File Offset: 0x000032A8
		private IMECompositionEvent ToIMECompositionEvent(DiscreteTime currentTime, string compositionString)
		{
			return new IMECompositionEvent
			{
				compositionString = compositionString,
				timestamp = currentTime,
				eventSource = EventSource.Unspecified,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000050F4 File Offset: 0x000032F4
		internal static float TiltToAzimuth(Vector2 tilt)
		{
			float num = 0f;
			bool flag = tilt.x != 0f;
			if (flag)
			{
				num = 1.5707964f - Mathf.Atan2(-Mathf.Cos(tilt.x) * Mathf.Sin(tilt.y), Mathf.Cos(tilt.y) * Mathf.Sin(tilt.x));
				bool flag2 = num < 0f;
				if (flag2)
				{
					num += 6.2831855f;
				}
				bool flag3 = num >= 1.5707964f;
				if (flag3)
				{
					num -= 1.5707964f;
				}
				else
				{
					num += 4.712389f;
				}
			}
			return num;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005198 File Offset: 0x00003398
		internal static Vector2 AzimuthAndAlitutudeToTilt(float altitude, float azimuth)
		{
			return new Vector2(0f, 0f)
			{
				x = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Cos(altitude) / Mathf.Sin(azimuth)),
				y = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Sin(altitude) / Mathf.Sin(azimuth))
			};
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000051FC File Offset: 0x000033FC
		internal static float TiltToAltitude(Vector2 tilt)
		{
			return 1.5707964f - Mathf.Acos(Mathf.Cos(tilt.x) * Mathf.Cos(tilt.y));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005230 File Offset: 0x00003430
		private static Vector2 MultiDisplayBottomLeftToPanelPosition(Vector2 position, out int targetDisplay)
		{
			int? num;
			Vector2 vector = InputManagerProvider.MultiDisplayToLocalScreenPosition(position, out num);
			targetDisplay = num.GetValueOrDefault();
			return InputManagerProvider.ScreenBottomLeftToPanelPosition(vector, targetDisplay);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000525C File Offset: 0x0000345C
		private static Vector2 MultiDisplayToLocalScreenPosition(Vector2 position, out int? targetDisplay)
		{
			Vector3 vector = Display.RelativeMouseAt(position);
			bool flag = vector != Vector3.zero;
			Vector2 vector2;
			if (flag)
			{
				targetDisplay = new int?((int)vector.z);
				vector2 = vector;
			}
			else
			{
				targetDisplay = null;
				vector2 = position;
			}
			return vector2;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000052B0 File Offset: 0x000034B0
		private static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			int num = Screen.height;
			bool flag = targetDisplay > 0 && targetDisplay < Display.displays.Length;
			if (flag)
			{
				num = Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)num - position.y;
			return position;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000052FC File Offset: 0x000034FC
		private static Vector2 ScreenBottomLeftToPanelDelta(Vector2 delta)
		{
			delta.y = -delta.y;
			return delta;
		}

		// Token: 0x040000BC RID: 188
		private InputEventPartialProvider _inputEventPartialProvider;

		// Token: 0x040000BD RID: 189
		private const int kDefaultPlayerId = 0;

		// Token: 0x040000BE RID: 190
		private string _compositionString = string.Empty;

		// Token: 0x040000BF RID: 191
		private InputManagerProvider.Configuration _configuration = InputManagerProvider.Configuration.GetDefaultConfiguration();

		// Token: 0x040000C0 RID: 192
		private InputManagerProvider.IInput _input = new InputManagerProvider.Input();

		// Token: 0x040000C1 RID: 193
		private InputManagerProvider.ITime _time = new InputManagerProvider.Time();

		// Token: 0x040000C2 RID: 194
		private NavigationEventRepeatHelper _navigationEventRepeatHelper = new NavigationEventRepeatHelper();

		// Token: 0x040000C3 RID: 195
		private const int kMaxMouseButtons = 5;

		// Token: 0x040000C4 RID: 196
		private PointerState _mouseState;

		// Token: 0x040000C5 RID: 197
		private bool _isPenPresent;

		// Token: 0x040000C6 RID: 198
		private bool _seenAtLeastOnePenPosition;

		// Token: 0x040000C7 RID: 199
		private Vector2 _lastSeenPenPositionForDetection;

		// Token: 0x040000C8 RID: 200
		private PointerState _penState;

		// Token: 0x040000C9 RID: 201
		private PenData _lastPenData;

		// Token: 0x040000CA RID: 202
		private Dictionary<int, int> _touchFingerIdToFingerIndex = new Dictionary<int, int>();

		// Token: 0x040000CB RID: 203
		private int _touchNextFingerIndex;

		// Token: 0x040000CC RID: 204
		private PointerState _touchState;

		// Token: 0x040000CD RID: 205
		private const float kSmallestReportedMovementSqrDist = 0.01f;

		// Token: 0x040000CE RID: 206
		private const float kScrollUGUIScaleFactor = 3f;

		// Token: 0x02000027 RID: 39
		private struct ButtonEventsIterator : IEnumerator
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000531D File Offset: 0x0000351D
			public bool Current
			{
				get
				{
					return this._bit % 2 == 0;
				}
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x0000532C File Offset: 0x0000352C
			public bool MoveNext()
			{
				for (;;)
				{
					this._bit++;
					bool flag = (this._mask & (1U << this._bit)) > 0U;
					if (flag)
					{
						break;
					}
					if (this._bit >= 4)
					{
						goto Block_1;
					}
				}
				return true;
				Block_1:
				return false;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00005379 File Offset: 0x00003579
			public void Reset()
			{
				this._bit = -1;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005383 File Offset: 0x00003583
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00005390 File Offset: 0x00003590
			public static InputManagerProvider.ButtonEventsIterator FromState(bool previous, bool down, bool up, bool current)
			{
				uint num = ((!previous && current) ? 1U : ((previous && !current) ? 2U : 0U));
				return new InputManagerProvider.ButtonEventsIterator
				{
					_mask = num,
					_bit = -1
				};
			}

			// Token: 0x040000CF RID: 207
			private uint _mask;

			// Token: 0x040000D0 RID: 208
			private int _bit;

			// Token: 0x040000D1 RID: 209
			private const uint kWasPressed = 1U;

			// Token: 0x040000D2 RID: 210
			private const uint kWasReleased = 2U;

			// Token: 0x040000D3 RID: 211
			private const int kMaxBits = 4;
		}

		// Token: 0x02000028 RID: 40
		public struct Configuration
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x000053D0 File Offset: 0x000035D0
			public static InputManagerProvider.Configuration GetDefaultConfiguration()
			{
				return new InputManagerProvider.Configuration
				{
					HorizontalAxis = "Horizontal",
					VerticalAxis = "Vertical",
					SubmitButton = "Submit",
					CancelButton = "Cancel",
					NavigateNextButton = "Next",
					NavigatePreviousButton = "Previous",
					InputActionsPerSecond = 10f,
					RepeatDelay = 0.5f
				};
			}

			// Token: 0x040000D4 RID: 212
			public string HorizontalAxis;

			// Token: 0x040000D5 RID: 213
			public string VerticalAxis;

			// Token: 0x040000D6 RID: 214
			public string SubmitButton;

			// Token: 0x040000D7 RID: 215
			public string CancelButton;

			// Token: 0x040000D8 RID: 216
			public string NavigateNextButton;

			// Token: 0x040000D9 RID: 217
			public string NavigatePreviousButton;

			// Token: 0x040000DA RID: 218
			public float InputActionsPerSecond;

			// Token: 0x040000DB RID: 219
			public float RepeatDelay;
		}

		// Token: 0x02000029 RID: 41
		internal interface IInput
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000D7 RID: 215
			string compositionString { get; }

			// Token: 0x060000D8 RID: 216
			bool GetKey(KeyCode keyCode);

			// Token: 0x060000D9 RID: 217
			bool GetKeyDown(KeyCode keyCode);

			// Token: 0x060000DA RID: 218
			bool GetButtonDown(string button);

			// Token: 0x060000DB RID: 219
			float GetAxisRaw(string axis);

			// Token: 0x060000DC RID: 220
			PenData GetPenEvent(int index);

			// Token: 0x060000DD RID: 221
			PenData GetLastPenContactEvent();

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000DE RID: 222
			bool touchSupported { get; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000DF RID: 223
			int touchCount { get; }

			// Token: 0x060000E0 RID: 224
			Touch GetTouch(int index);

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000E1 RID: 225
			bool mousePresent { get; }

			// Token: 0x060000E2 RID: 226
			bool GetMouseButton(int button);

			// Token: 0x060000E3 RID: 227
			bool GetMouseButtonDown(int button);

			// Token: 0x060000E4 RID: 228
			bool GetMouseButtonUp(int button);

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000E5 RID: 229
			Vector3 mousePosition { get; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000E6 RID: 230
			Vector2 mouseScrollDelta { get; }
		}

		// Token: 0x0200002A RID: 42
		private class Input : InputManagerProvider.IInput
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000544B File Offset: 0x0000364B
			public string compositionString
			{
				get
				{
					return UnityEngine.Input.compositionString;
				}
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x00005452 File Offset: 0x00003652
			public bool GetKey(KeyCode key)
			{
				return UnityEngine.Input.GetKey(key);
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x0000545A File Offset: 0x0000365A
			public bool GetKeyDown(KeyCode key)
			{
				return UnityEngine.Input.GetKeyDown(key);
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00005462 File Offset: 0x00003662
			public bool GetButtonDown(string button)
			{
				return UnityEngine.Input.GetButtonDown(button);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x0000546A File Offset: 0x0000366A
			public float GetAxisRaw(string axis)
			{
				return UnityEngine.Input.GetAxisRaw(axis);
			}

			// Token: 0x060000EC RID: 236 RVA: 0x00005472 File Offset: 0x00003672
			public PenData GetPenEvent(int index)
			{
				return UnityEngine.Input.GetPenEvent(index);
			}

			// Token: 0x060000ED RID: 237 RVA: 0x0000547A File Offset: 0x0000367A
			public PenData GetLastPenContactEvent()
			{
				return UnityEngine.Input.GetLastPenContactEvent();
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000EE RID: 238 RVA: 0x00005481 File Offset: 0x00003681
			public bool touchSupported
			{
				get
				{
					return UnityEngine.Input.touchSupported;
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000EF RID: 239 RVA: 0x00005488 File Offset: 0x00003688
			public int touchCount
			{
				get
				{
					return UnityEngine.Input.touchCount;
				}
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x0000548F File Offset: 0x0000368F
			public Touch GetTouch(int index)
			{
				return UnityEngine.Input.GetTouch(index);
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005497 File Offset: 0x00003697
			public bool mousePresent
			{
				get
				{
					return UnityEngine.Input.mousePresent;
				}
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000549E File Offset: 0x0000369E
			public bool GetMouseButton(int button)
			{
				return UnityEngine.Input.GetMouseButton(button);
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x000054A6 File Offset: 0x000036A6
			public bool GetMouseButtonDown(int button)
			{
				return UnityEngine.Input.GetMouseButtonDown(button);
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x000054AE File Offset: 0x000036AE
			public bool GetMouseButtonUp(int button)
			{
				return UnityEngine.Input.GetMouseButtonUp(button);
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000054B6 File Offset: 0x000036B6
			public Vector3 mousePosition
			{
				get
				{
					return UnityEngine.Input.mousePosition;
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x000054BD File Offset: 0x000036BD
			public Vector2 mouseScrollDelta
			{
				get
				{
					return UnityEngine.Input.mouseScrollDelta;
				}
			}
		}

		// Token: 0x0200002B RID: 43
		internal interface ITime
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000F8 RID: 248
			RationalTime timeAsRational { get; }
		}

		// Token: 0x0200002C RID: 44
		private class Time : InputManagerProvider.ITime
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x000054C4 File Offset: 0x000036C4
			public RationalTime timeAsRational
			{
				get
				{
					return UnityEngine.Time.timeAsRational;
				}
			}
		}
	}
}
