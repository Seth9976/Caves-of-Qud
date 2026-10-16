using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.InputForUI;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DD RID: 477
	internal class DefaultEventSystem
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00040D1D File Offset: 0x0003EF1D
		private bool isAppFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00040D24 File Offset: 0x0003EF24
		private bool ShouldIgnoreEventsOnAppNotFocused()
		{
			OperatingSystemFamily operatingSystemFamily = SystemInfo.operatingSystemFamily;
			OperatingSystemFamily operatingSystemFamily2 = operatingSystemFamily;
			return operatingSystemFamily2 - OperatingSystemFamily.MacOSX <= 2;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00040D4B File Offset: 0x0003EF4B
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x00040D54 File Offset: 0x0003EF54
		public BaseRuntimePanel focusedPanel
		{
			get
			{
				return this.m_FocusedPanel;
			}
			set
			{
				bool flag = this.m_FocusedPanel != value;
				if (flag)
				{
					BaseRuntimePanel focusedPanel = this.m_FocusedPanel;
					if (focusedPanel != null)
					{
						focusedPanel.Blur();
					}
					this.m_FocusedPanel = value;
					BaseRuntimePanel focusedPanel2 = this.m_FocusedPanel;
					if (focusedPanel2 != null)
					{
						focusedPanel2.Focus();
					}
				}
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00040D9F File Offset: 0x0003EF9F
		public void Reset()
		{
			DefaultEventSystem.LegacyInputProcessor legacyInputProcessor = this.m_LegacyInputProcessor;
			if (legacyInputProcessor != null)
			{
				legacyInputProcessor.Reset();
			}
			DefaultEventSystem.InputForUIProcessor inputForUIProcessor = this.m_InputForUIProcessor;
			if (inputForUIProcessor != null)
			{
				inputForUIProcessor.Reset();
			}
			this.m_FocusedPanel = null;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		public void Update(DefaultEventSystem.UpdateMode updateMode = DefaultEventSystem.UpdateMode.Always)
		{
			bool flag = !this.isAppFocused && this.ShouldIgnoreEventsOnAppNotFocused() && updateMode == DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused;
			if (!flag)
			{
				bool isInputForUIActive = this.m_IsInputForUIActive;
				if (isInputForUIActive)
				{
					this.inputForUIProcessor.ProcessInputForUIEvents();
				}
				else
				{
					this.legacyInputProcessor.ProcessLegacyInputEvents();
				}
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00040E24 File Offset: 0x0003F024
		internal DefaultEventSystem.LegacyInputProcessor legacyInputProcessor
		{
			get
			{
				DefaultEventSystem.LegacyInputProcessor legacyInputProcessor;
				if ((legacyInputProcessor = this.m_LegacyInputProcessor) == null)
				{
					legacyInputProcessor = (this.m_LegacyInputProcessor = new DefaultEventSystem.LegacyInputProcessor(this));
				}
				return legacyInputProcessor;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00040E4C File Offset: 0x0003F04C
		private DefaultEventSystem.InputForUIProcessor inputForUIProcessor
		{
			get
			{
				DefaultEventSystem.InputForUIProcessor inputForUIProcessor;
				if ((inputForUIProcessor = this.m_InputForUIProcessor) == null)
				{
					inputForUIProcessor = (this.m_InputForUIProcessor = new DefaultEventSystem.InputForUIProcessor(this));
				}
				return inputForUIProcessor;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00040E72 File Offset: 0x0003F072
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00040E7C File Offset: 0x0003F07C
		internal bool isInputReady
		{
			get
			{
				return this.m_IsInputReady;
			}
			set
			{
				bool flag = this.m_IsInputReady == value;
				if (!flag)
				{
					this.m_IsInputReady = value;
					bool isInputReady = this.m_IsInputReady;
					if (isInputReady)
					{
						this.InitInputProcessor();
					}
					else
					{
						this.RemoveInputProcessor();
					}
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00040EBE File Offset: 0x0003F0BE
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00040EC8 File Offset: 0x0003F0C8
		internal bool useInputForUI
		{
			get
			{
				return this.m_UseInputForUI;
			}
			set
			{
				bool flag = this.m_UseInputForUI == value;
				if (!flag)
				{
					bool isInputReady = this.m_IsInputReady;
					if (isInputReady)
					{
						this.RemoveInputProcessor();
						this.m_UseInputForUI = value;
						this.InitInputProcessor();
					}
					else
					{
						this.m_UseInputForUI = value;
					}
				}
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00040F14 File Offset: 0x0003F114
		internal DefaultEventSystem.FocusBasedEventSequenceContext FocusBasedEventSequence()
		{
			return new DefaultEventSystem.FocusBasedEventSequenceContext(this);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00040F2C File Offset: 0x0003F12C
		private void RemoveInputProcessor()
		{
			bool isInputForUIActive = this.m_IsInputForUIActive;
			if (isInputForUIActive)
			{
				EventProvider.Unsubscribe(new EventConsumer(this.inputForUIProcessor.OnEvent));
				EventProvider.SetEnabled(false);
				this.m_IsInputForUIActive = false;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00040F6C File Offset: 0x0003F16C
		private void InitInputProcessor()
		{
			bool useInputForUI = this.m_UseInputForUI;
			if (useInputForUI)
			{
				this.m_IsInputForUIActive = true;
				EventProvider.SetEnabled(true);
				EventProvider.Subscribe(new EventConsumer(this.inputForUIProcessor.OnEvent), 0, null, Array.Empty<Event.Type>());
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00040FBA File Offset: 0x0003F1BA
		internal void OnFocusEvent(RuntimePanel panel, FocusEvent evt)
		{
			this.focusedPanel = panel;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00040FC8 File Offset: 0x0003F1C8
		internal void SendFocusBasedEvent<TArg>(Func<TArg, EventBase> evtFactory, TArg arg)
		{
			bool flag = this.m_PreviousFocusedPanel != null;
			if (flag)
			{
				using (EventBase eventBase = evtFactory(arg))
				{
					eventBase.elementTarget = ((VisualElement)this.m_PreviousFocusedElement) ?? this.m_PreviousFocusedPanel.visualTree;
					this.m_PreviousFocusedPanel.visualTree.SendEvent(eventBase);
					this.UpdateFocusedPanel(this.m_PreviousFocusedPanel);
					return;
				}
			}
			List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
			for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
			{
				Panel panel = sortedPlayerPanels[i];
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					using (EventBase eventBase2 = evtFactory(arg))
					{
						eventBase2.elementTarget = baseRuntimePanel.visualTree;
						baseRuntimePanel.visualTree.SendEvent(eventBase2);
						bool flag3 = baseRuntimePanel.focusController.focusedElement != null;
						if (flag3)
						{
							this.focusedPanel = baseRuntimePanel;
							break;
						}
						bool isPropagationStopped = eventBase2.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004110C File Offset: 0x0003F30C
		private void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, int? targetDisplay, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				this.UpdateFocusedPanel(this.focusedPanel);
			}
			IPanel panel = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId);
			IEventHandler capturingElement = RuntimePanel.s_EventDispatcher.pointerState.GetCapturingElement(pointerId);
			VisualElement visualElement = capturingElement as VisualElement;
			bool flag2 = visualElement != null;
			if (flag2)
			{
				panel = visualElement.panel;
			}
			BaseRuntimePanel baseRuntimePanel = null;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			BaseRuntimePanel baseRuntimePanel2 = panel as BaseRuntimePanel;
			bool flag3 = baseRuntimePanel2 != null;
			if (flag3)
			{
				baseRuntimePanel = baseRuntimePanel2;
				baseRuntimePanel.ScreenToPanel(mousePosition, delta, out zero, out zero2, false);
			}
			else
			{
				List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
				{
					BaseRuntimePanel baseRuntimePanel3 = sortedPlayerPanels[i] as BaseRuntimePanel;
					bool flag4;
					if (baseRuntimePanel3 != null)
					{
						if (targetDisplay != null)
						{
							int targetDisplay2 = baseRuntimePanel3.targetDisplay;
							int? num = targetDisplay;
							flag4 = (targetDisplay2 == num.GetValueOrDefault()) & (num != null);
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						bool flag6 = baseRuntimePanel3.ScreenToPanel(mousePosition, delta, out zero, out zero2, false) && baseRuntimePanel3.Pick(zero) != null;
						if (flag6)
						{
							baseRuntimePanel = baseRuntimePanel3;
							break;
						}
					}
				}
			}
			BaseRuntimePanel baseRuntimePanel4 = PointerDeviceState.GetPanel(pointerId, ContextType.Player) as BaseRuntimePanel;
			bool flag7 = baseRuntimePanel4 != baseRuntimePanel;
			if (flag7)
			{
				if (baseRuntimePanel4 != null)
				{
					baseRuntimePanel4.PointerLeavesPanel(pointerId, baseRuntimePanel4.ScreenToPanel(mousePosition));
				}
				if (baseRuntimePanel != null)
				{
					baseRuntimePanel.PointerEntersPanel(pointerId, zero);
				}
			}
			bool flag8 = baseRuntimePanel != null;
			if (flag8)
			{
				using (EventBase eventBase = evtFactory(zero, zero2, arg))
				{
					baseRuntimePanel.visualTree.SendEvent(eventBase);
					bool processedByFocusController = eventBase.processedByFocusController;
					if (processedByFocusController)
					{
						this.UpdateFocusedPanel(baseRuntimePanel);
					}
					bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId();
					if (flag9)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, baseRuntimePanel);
					}
					else
					{
						bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && ((PointerUpEvent)eventBase).pressedButtons == 0;
						if (flag10)
						{
							PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, null);
						}
					}
				}
			}
			else if (deselectIfNoTarget)
			{
				this.focusedPanel = null;
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00041378 File Offset: 0x0003F578
		private void UpdateFocusedPanel(BaseRuntimePanel runtimePanel)
		{
			bool flag = runtimePanel.focusController.focusedElement != null;
			if (flag)
			{
				this.focusedPanel = runtimePanel;
			}
			else
			{
				bool flag2 = this.focusedPanel == runtimePanel;
				if (flag2)
				{
					this.focusedPanel = null;
				}
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000413BC File Offset: 0x0003F5BC
		private static EventBase MakeTouchEvent(Touch touch, EventModifiers modifiers, int targetDisplay)
		{
			switch (touch.phase)
			{
			case TouchPhase.Began:
				return PointerEventBase<PointerDownEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Moved:
				return PointerEventBase<PointerMoveEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Ended:
				return PointerEventBase<PointerUpEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Canceled:
				return PointerEventBase<PointerCancelEvent>.GetPooled(touch, modifiers, targetDisplay);
			}
			return null;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00041424 File Offset: 0x0003F624
		private static EventBase MakePenEvent(PenData pen, EventModifiers modifiers, int targetDisplay)
		{
			EventBase eventBase;
			switch (pen.contactType)
			{
			case PenEventType.NoContact:
				eventBase = PointerEventBase<PointerMoveEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			case PenEventType.PenDown:
				eventBase = PointerEventBase<PointerDownEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			case PenEventType.PenUp:
				eventBase = PointerEventBase<PointerUpEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			default:
				eventBase = null;
				break;
			}
			return eventBase;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00041478 File Offset: 0x0003F678
		private void Log(object o)
		{
			Debug.Log(o);
			bool flag = this.logToGameScreen;
			if (flag)
			{
				this.LogToGameScreen(((o != null) ? o.ToString() : null) ?? "");
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000414B4 File Offset: 0x0003F6B4
		private void LogWarning(object o)
		{
			Debug.LogWarning(o);
			bool flag = this.logToGameScreen;
			if (flag)
			{
				this.LogToGameScreen("Warning! " + ((o != null) ? o.ToString() : null));
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000414F0 File Offset: 0x0003F6F0
		private void LogToGameScreen(string s)
		{
			bool flag = this.m_LogLabel == null;
			if (flag)
			{
				this.m_LogLabel = new Label
				{
					style = 
					{
						position = Position.Absolute,
						bottom = 0f,
						color = Color.white
					}
				};
				Object.FindFirstObjectByType<UIDocument>().rootVisualElement.Add(this.m_LogLabel);
			}
			this.m_LogLines.Add(s + "\n");
			bool flag2 = this.m_LogLines.Count > 10;
			if (flag2)
			{
				this.m_LogLines.RemoveAt(0);
			}
			this.m_LogLabel.text = string.Concat(this.m_LogLines);
		}

		// Token: 0x040008FC RID: 2300
		internal static Func<bool> IsEditorRemoteConnected = () => false;

		// Token: 0x040008FD RID: 2301
		private BaseRuntimePanel m_FocusedPanel;

		// Token: 0x040008FE RID: 2302
		private BaseRuntimePanel m_PreviousFocusedPanel;

		// Token: 0x040008FF RID: 2303
		private Focusable m_PreviousFocusedElement;

		// Token: 0x04000900 RID: 2304
		private DefaultEventSystem.LegacyInputProcessor m_LegacyInputProcessor;

		// Token: 0x04000901 RID: 2305
		private DefaultEventSystem.InputForUIProcessor m_InputForUIProcessor;

		// Token: 0x04000902 RID: 2306
		private bool m_IsInputReady = false;

		// Token: 0x04000903 RID: 2307
		private bool m_UseInputForUI = true;

		// Token: 0x04000904 RID: 2308
		private bool m_IsInputForUIActive = false;

		// Token: 0x04000905 RID: 2309
		internal bool verbose = false;

		// Token: 0x04000906 RID: 2310
		internal bool logToGameScreen = false;

		// Token: 0x04000907 RID: 2311
		private Label m_LogLabel;

		// Token: 0x04000908 RID: 2312
		private List<string> m_LogLines = new List<string>();

		// Token: 0x020001DE RID: 478
		public enum UpdateMode
		{
			// Token: 0x0400090A RID: 2314
			Always,
			// Token: 0x0400090B RID: 2315
			IgnoreIfAppNotFocused
		}

		// Token: 0x020001DF RID: 479
		internal struct FocusBasedEventSequenceContext : IDisposable
		{
			// Token: 0x06000EA7 RID: 3751 RVA: 0x00041608 File Offset: 0x0003F808
			public FocusBasedEventSequenceContext(DefaultEventSystem es)
			{
				this.es = es;
				es.m_PreviousFocusedPanel = es.focusedPanel;
				BaseRuntimePanel focusedPanel = es.focusedPanel;
				es.m_PreviousFocusedElement = ((focusedPanel != null) ? focusedPanel.focusController.GetLeafFocusedElement() : null);
			}

			// Token: 0x06000EA8 RID: 3752 RVA: 0x0004163B File Offset: 0x0003F83B
			public void Dispose()
			{
				this.es.m_PreviousFocusedPanel = null;
				this.es.m_PreviousFocusedElement = null;
			}

			// Token: 0x0400090C RID: 2316
			private DefaultEventSystem es;
		}

		// Token: 0x020001E0 RID: 480
		private class InputForUIProcessor
		{
			// Token: 0x06000EA9 RID: 3753 RVA: 0x00041656 File Offset: 0x0003F856
			public InputForUIProcessor(DefaultEventSystem eventSystem)
			{
				this.m_EventSystem = eventSystem;
			}

			// Token: 0x06000EAA RID: 3754 RVA: 0x00041688 File Offset: 0x0003F888
			public void Reset()
			{
				this.m_LastPointerTimestamp = DiscreteTime.Zero;
				this.m_NextPointerTimestamp = DiscreteTime.Zero;
				this.m_EventList.Clear();
			}

			// Token: 0x06000EAB RID: 3755 RVA: 0x000416B0 File Offset: 0x0003F8B0
			public bool OnEvent(in Event ev)
			{
				this.m_EventList.Enqueue(ev);
				return true;
			}

			// Token: 0x06000EAC RID: 3756 RVA: 0x000416D8 File Offset: 0x0003F8D8
			public void ProcessInputForUIEvents()
			{
				bool flag = this.m_EventList.Count == 0;
				if (!flag)
				{
					DefaultEventSystem.FocusBasedEventSequenceContext? focusBasedEventSequenceContext = null;
					while (this.m_EventList.Count > 0)
					{
						Event @event = this.m_EventList.Dequeue();
						switch (@event.type)
						{
						case Event.Type.KeyEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext2 = focusBasedEventSequenceContext.GetValueOrDefault();
							if (focusBasedEventSequenceContext == null)
							{
								focusBasedEventSequenceContext2 = this.m_EventSystem.FocusBasedEventSequence();
								focusBasedEventSequenceContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext2);
							}
							this.ProcessKeyEvent(@event.asKeyEvent);
							break;
						}
						case Event.Type.PointerEvent:
							this.ProcessPointerEvent(@event.asPointerEvent);
							break;
						case Event.Type.TextInputEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext2 = focusBasedEventSequenceContext.GetValueOrDefault();
							if (focusBasedEventSequenceContext == null)
							{
								focusBasedEventSequenceContext2 = this.m_EventSystem.FocusBasedEventSequence();
								focusBasedEventSequenceContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext2);
							}
							this.ProcessTextInputEvent(@event.asTextInputEvent);
							break;
						}
						case Event.Type.IMECompositionEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext2 = focusBasedEventSequenceContext.GetValueOrDefault();
							if (focusBasedEventSequenceContext == null)
							{
								focusBasedEventSequenceContext2 = this.m_EventSystem.FocusBasedEventSequence();
								focusBasedEventSequenceContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext2);
							}
							this.ProcessIMECompositionEvent(@event.asIMECompositionEvent);
							break;
						}
						case Event.Type.CommandEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext2 = focusBasedEventSequenceContext.GetValueOrDefault();
							if (focusBasedEventSequenceContext == null)
							{
								focusBasedEventSequenceContext2 = this.m_EventSystem.FocusBasedEventSequence();
								focusBasedEventSequenceContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext2);
							}
							this.ProcessCommandEvent(@event.asCommandEvent);
							break;
						}
						case Event.Type.NavigationEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext2 = focusBasedEventSequenceContext.GetValueOrDefault();
							if (focusBasedEventSequenceContext == null)
							{
								focusBasedEventSequenceContext2 = this.m_EventSystem.FocusBasedEventSequence();
								focusBasedEventSequenceContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext2);
							}
							this.ProcessNavigationEvent(@event.asNavigationEvent);
							break;
						}
						default:
						{
							bool verbose = this.m_EventSystem.verbose;
							if (verbose)
							{
								DefaultEventSystem eventSystem = this.m_EventSystem;
								string text = "Unsupported event (";
								string text2 = ((int)@event.type).ToString();
								string text3 = "): ";
								Event event2 = @event;
								eventSystem.Log(text + text2 + text3 + event2.ToString());
							}
							break;
						}
						}
					}
					if (focusBasedEventSequenceContext != null)
					{
						focusBasedEventSequenceContext.GetValueOrDefault().Dispose();
					}
					this.m_LastPointerTimestamp = this.m_NextPointerTimestamp;
				}
			}

			// Token: 0x06000EAD RID: 3757 RVA: 0x00041918 File Offset: 0x0003FB18
			private EventModifiers GetModifiers(EventModifiers eventModifiers)
			{
				EventModifiers eventModifiers2 = EventModifiers.None;
				bool isShiftPressed = eventModifiers.isShiftPressed;
				if (isShiftPressed)
				{
					eventModifiers2 |= EventModifiers.Shift;
				}
				bool isCtrlPressed = eventModifiers.isCtrlPressed;
				if (isCtrlPressed)
				{
					eventModifiers2 |= EventModifiers.Control;
				}
				bool isAltPressed = eventModifiers.isAltPressed;
				if (isAltPressed)
				{
					eventModifiers2 |= EventModifiers.Alt;
				}
				bool isMetaPressed = eventModifiers.isMetaPressed;
				if (isMetaPressed)
				{
					eventModifiers2 |= EventModifiers.Command;
				}
				bool isCapsLockEnabled = eventModifiers.isCapsLockEnabled;
				if (isCapsLockEnabled)
				{
					eventModifiers2 |= EventModifiers.CapsLock;
				}
				bool isNumericPressed = eventModifiers.isNumericPressed;
				if (isNumericPressed)
				{
					eventModifiers2 |= EventModifiers.Numeric;
				}
				bool isFunctionKeyPressed = eventModifiers.isFunctionKeyPressed;
				if (isFunctionKeyPressed)
				{
					eventModifiers2 |= EventModifiers.FunctionKey;
				}
				return eventModifiers2;
			}

			// Token: 0x06000EAE RID: 3758 RVA: 0x000419B4 File Offset: 0x0003FBB4
			private void ProcessPointerEvent(PointerEvent pointerEvent)
			{
				Vector2 position = pointerEvent.position;
				int displayIndex = pointerEvent.displayIndex;
				Vector2 deltaPosition = pointerEvent.deltaPosition;
				int num = ((pointerEvent.eventSource == EventSource.Touch) ? PointerId.touchPointerIdBase : ((pointerEvent.eventSource == EventSource.Pen) ? PointerId.penPointerIdBase : PointerId.mousePointerId));
				int num2 = num + pointerEvent.pointerIndex;
				float num3 = ((this.m_LastPointerTimestamp != DiscreteTime.Zero) ? ((float)(pointerEvent.timestamp - this.m_LastPointerTimestamp)) : 0f);
				this.m_NextPointerTimestamp = pointerEvent.timestamp;
				bool flag = pointerEvent.type == PointerEvent.Type.PointerMoved;
				if (flag)
				{
					bool flag2 = !Mathf.Approximately(deltaPosition.x, 0f) || !Mathf.Approximately(deltaPosition.y, 0f);
					if (flag2)
					{
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, num2, new int?(displayIndex), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerMoveEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, num2, num3), false);
					}
				}
				else
				{
					bool flag3 = pointerEvent.type == PointerEvent.Type.ButtonPressed;
					if (flag3)
					{
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, num2, new int?(displayIndex), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerDownEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, num2, num3), false);
					}
					else
					{
						bool flag4 = pointerEvent.type == PointerEvent.Type.ButtonReleased;
						if (flag4)
						{
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, num2, new int?(displayIndex), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerUpEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, num2, num3), true);
						}
						else
						{
							bool flag5 = pointerEvent.type == PointerEvent.Type.TouchCanceled;
							if (flag5)
							{
								this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, num2, new int?(displayIndex), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerCancelEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, num2, num3), false);
							}
							else
							{
								bool flag6 = pointerEvent.type == PointerEvent.Type.Scroll;
								if (flag6)
								{
									Vector2 scroll = pointerEvent.scroll;
									bool verbose = this.m_EventSystem.verbose;
									if (verbose)
									{
										DefaultEventSystem eventSystem = this.m_EventSystem;
										string text = "ScrollDelta: ";
										Vector2 vector = scroll;
										eventSystem.Log(text + vector.ToString());
									}
									this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, Vector2>>(pointerEvent.position, pointerEvent.deltaPosition, PointerId.mousePointerId, new int?(displayIndex), (Vector3 panelPosition, Vector3 _, [TupleElementNames(new string[] { "modifiers", "scrollDelta" })] ValueTuple<EventModifiers, Vector2> t) => WheelEvent.GetPooled(t.Item2, panelPosition, t.Item1), new ValueTuple<EventModifiers, Vector2>(this.GetModifiers(pointerEvent.eventModifiers), scroll), false);
								}
								else
								{
									bool verbose2 = this.m_EventSystem.verbose;
									if (verbose2)
									{
										DefaultEventSystem eventSystem2 = this.m_EventSystem;
										string text2 = "Unsupported event ";
										PointerEvent pointerEvent2 = pointerEvent;
										eventSystem2.Log(text2 + pointerEvent2.ToString());
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06000EAF RID: 3759 RVA: 0x00041CEC File Offset: 0x0003FEEC
			private void ProcessNavigationEvent(NavigationEvent navigationEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(navigationEvent);
				}
				EventModifiers modifiers = this.GetModifiers(navigationEvent.eventModifiers);
				NavigationDeviceType navigationDeviceType = ((navigationEvent.eventSource == EventSource.Keyboard) ? NavigationDeviceType.Keyboard : ((navigationEvent.eventSource == EventSource.Unspecified) ? NavigationDeviceType.Unknown : NavigationDeviceType.NonKeyboard));
				bool flag = navigationEvent.type == NavigationEvent.Type.Move;
				if (flag)
				{
					Vector2 zero = Vector2.zero;
					bool flag2 = navigationEvent.direction == NavigationEvent.Direction.Left;
					if (flag2)
					{
						zero.x = -1f;
					}
					else
					{
						bool flag3 = navigationEvent.direction == NavigationEvent.Direction.Right;
						if (flag3)
						{
							zero.x = 1f;
						}
						else
						{
							bool flag4 = navigationEvent.direction == NavigationEvent.Direction.Up;
							if (flag4)
							{
								zero.y = 1f;
							}
							else
							{
								bool flag5 = navigationEvent.direction == NavigationEvent.Direction.Down;
								if (flag5)
								{
									zero.y = -1f;
								}
							}
						}
					}
					bool flag6 = zero != Vector2.zero;
					if (flag6)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<Vector2, NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "move", "deviceType", "mod" })] ValueTuple<Vector2, NavigationDeviceType, EventModifiers> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item2, t.Item3), new ValueTuple<Vector2, NavigationDeviceType, EventModifiers>(zero, navigationDeviceType, modifiers));
					}
					else
					{
						NavigationMoveEvent.Direction direction = ((navigationEvent.direction == NavigationEvent.Direction.Previous) ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next);
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "direction", "deviceType", "mod" })] ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item2, t.Item3), new ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers>(direction, navigationDeviceType, modifiers));
					}
				}
				else
				{
					bool flag7 = navigationEvent.type == NavigationEvent.Type.Submit;
					if (flag7)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "deviceType", "mod" })] ValueTuple<NavigationDeviceType, EventModifiers> t) => NavigationEventBase<NavigationSubmitEvent>.GetPooled(t.Item1, t.Item2), new ValueTuple<NavigationDeviceType, EventModifiers>(navigationDeviceType, modifiers));
					}
					else
					{
						bool flag8 = navigationEvent.type == NavigationEvent.Type.Cancel;
						if (flag8)
						{
							this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "deviceType", "mod" })] ValueTuple<NavigationDeviceType, EventModifiers> t) => NavigationEventBase<NavigationCancelEvent>.GetPooled(t.Item1, t.Item2), new ValueTuple<NavigationDeviceType, EventModifiers>(navigationDeviceType, modifiers));
						}
					}
				}
			}

			// Token: 0x06000EB0 RID: 3760 RVA: 0x00041EEC File Offset: 0x000400EC
			private void ProcessKeyEvent(KeyEvent keyEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(keyEvent);
				}
				bool flag = keyEvent.type == KeyEvent.Type.KeyPressed || keyEvent.type == KeyEvent.Type.KeyRepeated;
				if (flag)
				{
					this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, KeyCode>>(([TupleElementNames(new string[] { "modifiers", "keyCode" })] ValueTuple<EventModifiers, KeyCode> t) => KeyboardEventBase<KeyDownEvent>.GetPooled('\0', t.Item2, t.Item1), new ValueTuple<EventModifiers, KeyCode>(this.GetModifiers(keyEvent.eventModifiers), keyEvent.keyCode));
				}
				else
				{
					bool flag2 = keyEvent.type == KeyEvent.Type.KeyReleased;
					if (flag2)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, KeyCode>>(([TupleElementNames(new string[] { "modifiers", "keyCode" })] ValueTuple<EventModifiers, KeyCode> t) => KeyboardEventBase<KeyUpEvent>.GetPooled('\0', t.Item2, t.Item1), new ValueTuple<EventModifiers, KeyCode>(this.GetModifiers(keyEvent.eventModifiers), keyEvent.keyCode));
					}
				}
			}

			// Token: 0x06000EB1 RID: 3761 RVA: 0x00041FD0 File Offset: 0x000401D0
			private void ProcessTextInputEvent(TextInputEvent textInputEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(textInputEvent);
				}
				this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, char>>(([TupleElementNames(new string[] { "modifiers", "character" })] ValueTuple<EventModifiers, char> t) => KeyboardEventBase<KeyDownEvent>.GetPooled(t.Item2, KeyCode.None, t.Item1), new ValueTuple<EventModifiers, char>(this.GetModifiers(textInputEvent.eventModifiers), textInputEvent.character));
			}

			// Token: 0x06000EB2 RID: 3762 RVA: 0x00042044 File Offset: 0x00040244
			private void ProcessCommandEvent(CommandEvent commandEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(commandEvent);
				}
			}

			// Token: 0x06000EB3 RID: 3763 RVA: 0x00042074 File Offset: 0x00040274
			private void ProcessIMECompositionEvent(IMECompositionEvent compositionEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(compositionEvent);
				}
				this.m_EventSystem.SendFocusBasedEvent<int>((int _) => IMEEvent.GetPooled(compositionEvent.compositionString), 0);
			}

			// Token: 0x0400090D RID: 2317
			private readonly DefaultEventSystem m_EventSystem;

			// Token: 0x0400090E RID: 2318
			private DiscreteTime m_LastPointerTimestamp = DiscreteTime.Zero;

			// Token: 0x0400090F RID: 2319
			private DiscreteTime m_NextPointerTimestamp = DiscreteTime.Zero;

			// Token: 0x04000910 RID: 2320
			private readonly Queue<Event> m_EventList = new Queue<Event>();
		}

		// Token: 0x020001E3 RID: 483
		internal class LegacyInputProcessor
		{
			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0004222D File Offset: 0x0004042D
			private EventModifiers m_CurrentPointerModifiers
			{
				get
				{
					return this.m_CurrentModifiers & (EventModifiers.Shift | EventModifiers.Control | EventModifiers.Alt | EventModifiers.Command);
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00042238 File Offset: 0x00040438
			// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0004225E File Offset: 0x0004045E
			public DefaultEventSystem.LegacyInputProcessor.IInput input
			{
				get
				{
					DefaultEventSystem.LegacyInputProcessor.IInput input;
					if ((input = this.m_Input) == null)
					{
						input = (this.m_Input = this.GetDefaultInput());
					}
					return input;
				}
				set
				{
					this.m_Input = value;
				}
			}

			// Token: 0x06000EC7 RID: 3783 RVA: 0x00042267 File Offset: 0x00040467
			public LegacyInputProcessor(DefaultEventSystem eventSystem)
			{
				this.m_EventSystem = eventSystem;
			}

			// Token: 0x06000EC8 RID: 3784 RVA: 0x000422A8 File Offset: 0x000404A8
			public DefaultEventSystem.LegacyInputProcessor.IInput GetDefaultInput()
			{
				DefaultEventSystem.LegacyInputProcessor.IInput input = new DefaultEventSystem.LegacyInputProcessor.Input();
				try
				{
					input.GetAxisRaw("Horizontal");
				}
				catch (InvalidOperationException)
				{
					input = new DefaultEventSystem.LegacyInputProcessor.NoInput();
					this.m_EventSystem.LogWarning("UI Toolkit is currently relying on the legacy Input Manager for its active input source, but the legacy Input Manager is not available using your current Project Settings. Some UI Toolkit functionality might be missing or not working properly as a result. To fix this problem, you can enable \"Input Manager (old)\" or \"Both\" in the Active Input Source setting of the Player section. UI Toolkit is using its internal default event system to process input. Alternatively, you may activate new Input System support with UI Toolkit by adding an EventSystem component to your active scene.");
				}
				return input;
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x00042300 File Offset: 0x00040500
			public void Reset()
			{
				this.m_SendingTouchEvents = false;
				this.m_SendingPenEvent = false;
				this.m_CurrentModifiers = EventModifiers.None;
				this.m_LastMousePressButton = -1;
				this.m_NextMousePressTime = 0f;
				this.m_LastMouseClickCount = 0;
				this.m_LastMousePosition = Vector2.zero;
				this.m_MouseProcessedAtLeastOnce = false;
				this.m_ConsecutiveMoveCount = 0;
				this.m_IsMoveFromKeyboard = false;
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x0004235C File Offset: 0x0004055C
			public void ProcessLegacyInputEvents()
			{
				this.m_SendingPenEvent = this.ProcessPenEvents();
				bool flag = !this.m_SendingPenEvent;
				if (flag)
				{
					this.m_SendingTouchEvents = this.ProcessTouchEvents();
				}
				bool flag2 = !this.m_SendingPenEvent && !this.m_SendingTouchEvents;
				if (flag2)
				{
					this.ProcessMouseEvents();
				}
				else
				{
					this.m_MouseProcessedAtLeastOnce = false;
				}
				using (this.m_EventSystem.FocusBasedEventSequence())
				{
					this.SendIMGUIEvents();
					this.SendInputEvents();
				}
			}

			// Token: 0x06000ECB RID: 3787 RVA: 0x000423F4 File Offset: 0x000405F4
			private void SendIMGUIEvents()
			{
				bool flag = true;
				while (Event.PopEvent(this.m_Event))
				{
					bool flag2 = this.m_Event.type == EventType.Ignore || this.m_Event.type == EventType.Repaint || this.m_Event.type == EventType.Layout;
					if (!flag2)
					{
						this.m_CurrentModifiers = (flag ? this.m_Event.modifiers : (this.m_CurrentModifiers | this.m_Event.modifiers));
						flag = false;
						bool flag3 = this.m_Event.type == EventType.KeyUp || this.m_Event.type == EventType.KeyDown;
						if (flag3)
						{
							this.m_EventSystem.SendFocusBasedEvent<Event>((Event e) => UIElementsRuntimeUtility.CreateEvent(e), this.m_Event);
							this.ProcessTabEvent(this.m_Event, this.m_CurrentModifiers);
						}
						else
						{
							bool flag4 = this.m_Event.type == EventType.ScrollWheel;
							if (flag4)
							{
								int? num;
								Vector2 vector = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(this.input.mousePosition, out num);
								Vector2 vector2 = vector - this.m_LastMousePosition;
								Vector2 delta = this.m_Event.delta;
								this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, Vector2>>(vector, vector2, PointerId.mousePointerId, num, (Vector3 panelPosition, Vector3 _, [TupleElementNames(new string[] { "modifiers", "scrollDelta" })] ValueTuple<EventModifiers, Vector2> t) => WheelEvent.GetPooled(t.Item2, panelPosition, t.Item1), new ValueTuple<EventModifiers, Vector2>(this.m_CurrentPointerModifiers, delta), false);
							}
							else
							{
								bool flag5 = (!this.m_SendingTouchEvents && !this.m_SendingPenEvent && this.m_Event.pointerType != PointerType.Mouse) || this.m_Event.type == EventType.MouseEnterWindow || this.m_Event.type == EventType.MouseLeaveWindow;
								if (flag5)
								{
									int num2 = ((this.m_Event.pointerType == PointerType.Mouse) ? PointerId.mousePointerId : ((this.m_Event.pointerType == PointerType.Touch) ? PointerId.touchPointerIdBase : PointerId.penPointerIdBase));
									int? num3;
									Vector3 vector3 = UIElementsRuntimeUtility.MultiDisplayToLocalScreenPosition(this.m_Event.mousePosition, out num3);
									Vector2 delta2 = this.m_Event.delta;
									this.m_EventSystem.SendPositionBasedEvent<Event>(vector3, delta2, num2, num3, delegate(Vector3 panelPosition, Vector3 panelDelta, Event evt)
									{
										evt.mousePosition = panelPosition;
										evt.delta = panelDelta;
										return UIElementsRuntimeUtility.CreateEvent(evt);
									}, this.m_Event, this.m_Event.type == EventType.MouseDown || this.m_Event.type == EventType.TouchDown);
								}
							}
						}
					}
				}
			}

			// Token: 0x06000ECC RID: 3788 RVA: 0x0004268C File Offset: 0x0004088C
			private void ProcessMouseEvents()
			{
				bool flag = !this.input.mousePresent;
				if (!flag)
				{
					int? num;
					Vector2 vector = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(this.input.mousePosition, out num);
					Vector2 vector2 = vector - this.m_LastMousePosition;
					bool flag2 = !this.m_MouseProcessedAtLeastOnce;
					if (flag2)
					{
						vector2 = Vector2.zero;
						this.m_LastMousePosition = vector;
						this.m_MouseProcessedAtLeastOnce = true;
					}
					else
					{
						bool flag3 = !Mathf.Approximately(vector2.x, 0f) || !Mathf.Approximately(vector2.y, 0f);
						if (flag3)
						{
							this.m_LastMousePosition = vector;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, int?>>(vector, vector2, PointerId.mousePointerId, num, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "modifiers", "targetDisplay" })] ValueTuple<EventModifiers, int?> t) => PointerEventBase<PointerMoveEvent>.GetPooled(EventType.MouseMove, panelPosition, panelDelta, -1, 0, t.Item1, t.Item2.GetValueOrDefault()), new ValueTuple<EventModifiers, int?>(this.m_CurrentPointerModifiers, num), false);
						}
					}
					int mouseButtonCount = this.input.mouseButtonCount;
					for (int i = 0; i < mouseButtonCount; i++)
					{
						bool mouseButtonDown = this.input.GetMouseButtonDown(i);
						if (mouseButtonDown)
						{
							bool flag4 = this.m_LastMousePressButton != i || this.input.unscaledTime >= this.m_NextMousePressTime;
							if (flag4)
							{
								this.m_LastMousePressButton = i;
								this.m_LastMouseClickCount = 0;
							}
							int num2 = this.m_LastMouseClickCount + 1;
							this.m_LastMouseClickCount = num2;
							int num3 = num2;
							this.m_NextMousePressTime = this.input.unscaledTime + this.input.doubleClickTime;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<int, int, EventModifiers, int?>>(vector, vector2, PointerId.mousePointerId, num, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "button", "clickCount", "modifiers", "targetDisplay" })] ValueTuple<int, int, EventModifiers, int?> t) => PointerEventHelper.GetPooled(EventType.MouseDown, panelPosition, panelDelta, t.Item1, t.Item2, t.Item3, t.Item4.GetValueOrDefault()), new ValueTuple<int, int, EventModifiers, int?>(i, num3, this.m_CurrentPointerModifiers, num), true);
						}
						bool mouseButtonUp = this.input.GetMouseButtonUp(i);
						if (mouseButtonUp)
						{
							int lastMouseClickCount = this.m_LastMouseClickCount;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<int, int, EventModifiers, int?>>(vector, vector2, PointerId.mousePointerId, num, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "button", "clickCount", "modifiers", "targetDisplay" })] ValueTuple<int, int, EventModifiers, int?> t) => PointerEventHelper.GetPooled(EventType.MouseUp, panelPosition, panelDelta, t.Item1, t.Item2, t.Item3, t.Item4.GetValueOrDefault()), new ValueTuple<int, int, EventModifiers, int?>(i, lastMouseClickCount, this.m_CurrentPointerModifiers, num), false);
						}
					}
				}
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x000428F0 File Offset: 0x00040AF0
			private void SendInputEvents()
			{
				bool flag = this.ShouldSendMoveFromInput();
				bool flag2 = flag;
				if (flag2)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationMoveEvent.GetPooled(self.GetRawMoveVector(), self.m_IsMoveFromKeyboard ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
				bool buttonDown = this.input.GetButtonDown("Submit");
				if (buttonDown)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationEventBase<NavigationSubmitEvent>.GetPooled(self.input.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
				bool buttonDown2 = this.input.GetButtonDown("Cancel");
				if (buttonDown2)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationEventBase<NavigationCancelEvent>.GetPooled(self.input.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
			}

			// Token: 0x06000ECE RID: 3790 RVA: 0x000429BC File Offset: 0x00040BBC
			private bool ProcessTouchEvents()
			{
				for (int i = 0; i < this.input.touchCount; i++)
				{
					Touch touch = this.input.GetTouch(i);
					bool flag = touch.type == TouchType.Indirect || touch.phase == TouchPhase.Stationary;
					if (!flag)
					{
						int? num;
						touch.position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.position, out num);
						int? num2;
						touch.rawPosition = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.rawPosition, out num2);
						touch.deltaPosition = UIElementsRuntimeUtility.ScreenBottomLeftToPanelDelta(touch.deltaPosition);
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<Touch, int?>>(touch.position, touch.deltaPosition, PointerId.touchPointerIdBase + touch.fingerId, num, delegate(Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "touch", "targetDisplay" })] ValueTuple<Touch, int?> t)
						{
							t.Item1.position = panelPosition;
							t.Item1.deltaPosition = panelDelta;
							return DefaultEventSystem.MakeTouchEvent(t.Item1, EventModifiers.None, t.Item2.GetValueOrDefault());
						}, new ValueTuple<Touch, int?>(touch, num), false);
					}
				}
				return this.input.touchCount > 0;
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x00042AC8 File Offset: 0x00040CC8
			private bool ProcessPenEvents()
			{
				PenData lastPenContactEvent = this.input.GetLastPenContactEvent();
				bool flag = lastPenContactEvent.contactType == PenEventType.NoContact;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					this.m_EventSystem.SendPositionBasedEvent<PenData>(lastPenContactEvent.position, lastPenContactEvent.deltaPos, PointerId.penPointerIdBase, null, delegate(Vector3 panelPosition, Vector3 panelDelta, PenData _pen)
					{
						_pen.position = panelPosition;
						_pen.deltaPos = panelDelta;
						return DefaultEventSystem.MakePenEvent(_pen, EventModifiers.None, 0);
					}, lastPenContactEvent, false);
					this.input.ClearLastPenContactEvent();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06000ED0 RID: 3792 RVA: 0x00042B58 File Offset: 0x00040D58
			private Vector2 GetRawMoveVector()
			{
				Vector2 zero = Vector2.zero;
				zero.x = this.input.GetAxisRaw("Horizontal");
				zero.y = this.input.GetAxisRaw("Vertical");
				bool buttonDown = this.input.GetButtonDown("Horizontal");
				if (buttonDown)
				{
					bool flag = zero.x < 0f;
					if (flag)
					{
						zero.x = -1f;
					}
					bool flag2 = zero.x > 0f;
					if (flag2)
					{
						zero.x = 1f;
					}
				}
				bool buttonDown2 = this.input.GetButtonDown("Vertical");
				if (buttonDown2)
				{
					bool flag3 = zero.y < 0f;
					if (flag3)
					{
						zero.y = -1f;
					}
					bool flag4 = zero.y > 0f;
					if (flag4)
					{
						zero.y = 1f;
					}
				}
				return zero;
			}

			// Token: 0x06000ED1 RID: 3793 RVA: 0x00042C48 File Offset: 0x00040E48
			private bool ShouldSendMoveFromInput()
			{
				float unscaledTime = this.input.unscaledTime;
				Vector2 rawMoveVector = this.GetRawMoveVector();
				bool flag = Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f);
				bool flag2;
				if (flag)
				{
					this.m_ConsecutiveMoveCount = 0;
					this.m_IsMoveFromKeyboard = false;
					flag2 = false;
				}
				else
				{
					bool flag3 = this.input.GetButtonDown("Horizontal") || this.input.GetButtonDown("Vertical");
					bool flag4 = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
					bool flag5 = !flag3;
					if (flag5)
					{
						bool flag6 = flag4 && this.m_ConsecutiveMoveCount == 1;
						if (flag6)
						{
							flag3 = unscaledTime > this.m_PrevActionTime + 0.5f;
						}
						else
						{
							flag3 = unscaledTime > this.m_PrevActionTime + 0.1f;
						}
					}
					bool flag7 = !flag3;
					if (flag7)
					{
						flag2 = false;
					}
					else
					{
						NavigationMoveEvent.Direction direction = NavigationMoveEvent.DetermineMoveDirection(rawMoveVector.x, rawMoveVector.y, 0.6f);
						bool flag8 = direction > NavigationMoveEvent.Direction.None;
						if (flag8)
						{
							bool flag9 = !flag4;
							if (flag9)
							{
								this.m_ConsecutiveMoveCount = 0;
							}
							this.m_ConsecutiveMoveCount++;
							this.m_PrevActionTime = unscaledTime;
							this.m_LastMoveVector = rawMoveVector;
							this.m_IsMoveFromKeyboard |= this.input.anyKey;
						}
						else
						{
							this.m_ConsecutiveMoveCount = 0;
							this.m_IsMoveFromKeyboard = false;
						}
						flag2 = direction > NavigationMoveEvent.Direction.None;
					}
				}
				return flag2;
			}

			// Token: 0x06000ED2 RID: 3794 RVA: 0x00042DC4 File Offset: 0x00040FC4
			private void ProcessTabEvent(Event e, EventModifiers modifiers)
			{
				bool flag = e.ShouldSendNavigationMoveEventRuntime();
				if (flag)
				{
					NavigationMoveEvent.Direction direction = (e.shift ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next);
					this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput>>(([TupleElementNames(new string[] { "direction", "modifiers", "input" })] ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item3.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, t.Item2), new ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput>(direction, modifiers, this.input));
				}
			}

			// Token: 0x0400091F RID: 2335
			private const string m_HorizontalAxis = "Horizontal";

			// Token: 0x04000920 RID: 2336
			private const string m_VerticalAxis = "Vertical";

			// Token: 0x04000921 RID: 2337
			private const string m_SubmitButton = "Submit";

			// Token: 0x04000922 RID: 2338
			private const string m_CancelButton = "Cancel";

			// Token: 0x04000923 RID: 2339
			private const float m_InputActionsPerSecond = 10f;

			// Token: 0x04000924 RID: 2340
			private const float m_RepeatDelay = 0.5f;

			// Token: 0x04000925 RID: 2341
			private bool m_SendingTouchEvents;

			// Token: 0x04000926 RID: 2342
			private bool m_SendingPenEvent;

			// Token: 0x04000927 RID: 2343
			private EventModifiers m_CurrentModifiers;

			// Token: 0x04000928 RID: 2344
			private int m_LastMousePressButton = -1;

			// Token: 0x04000929 RID: 2345
			private float m_NextMousePressTime = 0f;

			// Token: 0x0400092A RID: 2346
			private int m_LastMouseClickCount = 0;

			// Token: 0x0400092B RID: 2347
			private Vector2 m_LastMousePosition = Vector2.zero;

			// Token: 0x0400092C RID: 2348
			private bool m_MouseProcessedAtLeastOnce;

			// Token: 0x0400092D RID: 2349
			private DefaultEventSystem.LegacyInputProcessor.IInput m_Input;

			// Token: 0x0400092E RID: 2350
			private readonly Event m_Event = new Event();

			// Token: 0x0400092F RID: 2351
			private readonly DefaultEventSystem m_EventSystem;

			// Token: 0x04000930 RID: 2352
			private int m_ConsecutiveMoveCount;

			// Token: 0x04000931 RID: 2353
			private Vector2 m_LastMoveVector;

			// Token: 0x04000932 RID: 2354
			private float m_PrevActionTime;

			// Token: 0x04000933 RID: 2355
			private bool m_IsMoveFromKeyboard;

			// Token: 0x020001E4 RID: 484
			internal interface IInput
			{
				// Token: 0x06000ED3 RID: 3795
				bool GetButtonDown(string button);

				// Token: 0x06000ED4 RID: 3796
				float GetAxisRaw(string axis);

				// Token: 0x06000ED5 RID: 3797
				void ResetPenEvents();

				// Token: 0x06000ED6 RID: 3798
				void ClearLastPenContactEvent();

				// Token: 0x17000281 RID: 641
				// (get) Token: 0x06000ED7 RID: 3799
				int penEventCount { get; }

				// Token: 0x06000ED8 RID: 3800
				PenData GetPenEvent(int index);

				// Token: 0x06000ED9 RID: 3801
				PenData GetLastPenContactEvent();

				// Token: 0x17000282 RID: 642
				// (get) Token: 0x06000EDA RID: 3802
				int touchCount { get; }

				// Token: 0x06000EDB RID: 3803
				Touch GetTouch(int index);

				// Token: 0x17000283 RID: 643
				// (get) Token: 0x06000EDC RID: 3804
				bool mousePresent { get; }

				// Token: 0x06000EDD RID: 3805
				bool GetMouseButtonDown(int button);

				// Token: 0x06000EDE RID: 3806
				bool GetMouseButtonUp(int button);

				// Token: 0x17000284 RID: 644
				// (get) Token: 0x06000EDF RID: 3807
				Vector3 mousePosition { get; }

				// Token: 0x17000285 RID: 645
				// (get) Token: 0x06000EE0 RID: 3808
				Vector2 mouseScrollDelta { get; }

				// Token: 0x17000286 RID: 646
				// (get) Token: 0x06000EE1 RID: 3809
				int mouseButtonCount { get; }

				// Token: 0x17000287 RID: 647
				// (get) Token: 0x06000EE2 RID: 3810
				bool anyKey { get; }

				// Token: 0x17000288 RID: 648
				// (get) Token: 0x06000EE3 RID: 3811
				float unscaledTime { get; }

				// Token: 0x17000289 RID: 649
				// (get) Token: 0x06000EE4 RID: 3812
				float doubleClickTime { get; }
			}

			// Token: 0x020001E5 RID: 485
			private class Input : DefaultEventSystem.LegacyInputProcessor.IInput
			{
				// Token: 0x06000EE5 RID: 3813 RVA: 0x00042E23 File Offset: 0x00041023
				public bool GetButtonDown(string button)
				{
					return UnityEngine.Input.GetButtonDown(button);
				}

				// Token: 0x06000EE6 RID: 3814 RVA: 0x00042E2B File Offset: 0x0004102B
				public float GetAxisRaw(string axis)
				{
					return UnityEngine.Input.GetAxis(axis);
				}

				// Token: 0x06000EE7 RID: 3815 RVA: 0x00042E33 File Offset: 0x00041033
				public void ResetPenEvents()
				{
					UnityEngine.Input.ResetPenEvents();
				}

				// Token: 0x06000EE8 RID: 3816 RVA: 0x00042E3B File Offset: 0x0004103B
				public void ClearLastPenContactEvent()
				{
					UnityEngine.Input.ClearLastPenContactEvent();
				}

				// Token: 0x1700028A RID: 650
				// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00042E43 File Offset: 0x00041043
				public int penEventCount
				{
					get
					{
						return UnityEngine.Input.penEventCount;
					}
				}

				// Token: 0x06000EEA RID: 3818 RVA: 0x00042E4A File Offset: 0x0004104A
				public PenData GetPenEvent(int index)
				{
					return UnityEngine.Input.GetPenEvent(index);
				}

				// Token: 0x06000EEB RID: 3819 RVA: 0x00042E52 File Offset: 0x00041052
				public PenData GetLastPenContactEvent()
				{
					return UnityEngine.Input.GetLastPenContactEvent();
				}

				// Token: 0x1700028B RID: 651
				// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00042E59 File Offset: 0x00041059
				public int touchCount
				{
					get
					{
						return UnityEngine.Input.touchCount;
					}
				}

				// Token: 0x06000EED RID: 3821 RVA: 0x00042E60 File Offset: 0x00041060
				public Touch GetTouch(int index)
				{
					return UnityEngine.Input.GetTouch(index);
				}

				// Token: 0x1700028C RID: 652
				// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00042E68 File Offset: 0x00041068
				public bool mousePresent
				{
					get
					{
						return UnityEngine.Input.mousePresent;
					}
				}

				// Token: 0x06000EEF RID: 3823 RVA: 0x00042E6F File Offset: 0x0004106F
				public bool GetMouseButtonDown(int button)
				{
					return UnityEngine.Input.GetMouseButtonDown(button);
				}

				// Token: 0x06000EF0 RID: 3824 RVA: 0x00042E77 File Offset: 0x00041077
				public bool GetMouseButtonUp(int button)
				{
					return UnityEngine.Input.GetMouseButtonUp(button);
				}

				// Token: 0x1700028D RID: 653
				// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00042E7F File Offset: 0x0004107F
				public Vector3 mousePosition
				{
					get
					{
						return UnityEngine.Input.mousePosition;
					}
				}

				// Token: 0x1700028E RID: 654
				// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00042E86 File Offset: 0x00041086
				public Vector2 mouseScrollDelta
				{
					get
					{
						return UnityEngine.Input.mouseScrollDelta;
					}
				}

				// Token: 0x1700028F RID: 655
				// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00042E8D File Offset: 0x0004108D
				public int mouseButtonCount
				{
					get
					{
						return 3;
					}
				}

				// Token: 0x17000290 RID: 656
				// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00042E90 File Offset: 0x00041090
				public bool anyKey
				{
					get
					{
						return UnityEngine.Input.anyKey;
					}
				}

				// Token: 0x17000291 RID: 657
				// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00042E97 File Offset: 0x00041097
				public float unscaledTime
				{
					get
					{
						return Time.unscaledTime;
					}
				}

				// Token: 0x17000292 RID: 658
				// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00042E9E File Offset: 0x0004109E
				public float doubleClickTime
				{
					get
					{
						return (float)Event.GetDoubleClickTime() * 0.001f;
					}
				}
			}

			// Token: 0x020001E6 RID: 486
			private class NoInput : DefaultEventSystem.LegacyInputProcessor.IInput
			{
				// Token: 0x06000EF8 RID: 3832 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public bool GetButtonDown(string button)
				{
					return false;
				}

				// Token: 0x06000EF9 RID: 3833 RVA: 0x00042EAC File Offset: 0x000410AC
				public float GetAxisRaw(string axis)
				{
					return 0f;
				}

				// Token: 0x17000293 RID: 659
				// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public int touchCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06000EFB RID: 3835 RVA: 0x00042EB4 File Offset: 0x000410B4
				public Touch GetTouch(int index)
				{
					return default(Touch);
				}

				// Token: 0x06000EFC RID: 3836 RVA: 0x00002152 File Offset: 0x00000352
				public void ResetPenEvents()
				{
				}

				// Token: 0x06000EFD RID: 3837 RVA: 0x00002152 File Offset: 0x00000352
				public void ClearLastPenContactEvent()
				{
				}

				// Token: 0x17000294 RID: 660
				// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public int penEventCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06000EFF RID: 3839 RVA: 0x00042ECC File Offset: 0x000410CC
				public PenData GetPenEvent(int index)
				{
					return default(PenData);
				}

				// Token: 0x06000F00 RID: 3840 RVA: 0x00042EE4 File Offset: 0x000410E4
				public PenData GetLastPenContactEvent()
				{
					return default(PenData);
				}

				// Token: 0x17000295 RID: 661
				// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public bool mousePresent
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06000F02 RID: 3842 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public bool GetMouseButtonDown(int button)
				{
					return false;
				}

				// Token: 0x06000F03 RID: 3843 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public bool GetMouseButtonUp(int button)
				{
					return false;
				}

				// Token: 0x17000296 RID: 662
				// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00042EFC File Offset: 0x000410FC
				public Vector3 mousePosition
				{
					get
					{
						return default(Vector3);
					}
				}

				// Token: 0x17000297 RID: 663
				// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00042F14 File Offset: 0x00041114
				public Vector2 mouseScrollDelta
				{
					get
					{
						return default(Vector2);
					}
				}

				// Token: 0x17000298 RID: 664
				// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public int mouseButtonCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x17000299 RID: 665
				// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00011DCA File Offset: 0x0000FFCA
				public bool anyKey
				{
					get
					{
						return false;
					}
				}

				// Token: 0x1700029A RID: 666
				// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00042EAC File Offset: 0x000410AC
				public float unscaledTime
				{
					get
					{
						return 0f;
					}
				}

				// Token: 0x1700029B RID: 667
				// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00042F2A File Offset: 0x0004112A
				public float doubleClickTime
				{
					get
					{
						return float.PositiveInfinity;
					}
				}
			}
		}
	}
}
