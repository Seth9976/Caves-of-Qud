using System;
using System.Collections.Generic;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000025 RID: 37
	internal class InputEventPartialProvider : IEventProviderImpl
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000352A File Offset: 0x0000172A
		public void Initialize()
		{
			this._operatingSystemFamily = SystemInfo.operatingSystemFamily;
			this._keyboardButtonsState.Reset();
			this._eventModifiers.Reset();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003550 File Offset: 0x00001750
		public void Shutdown()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003554 File Offset: 0x00001754
		public void Update()
		{
			int eventCount = Event.GetEventCount();
			for (int i = 0; i < eventCount; i++)
			{
				Event.GetEventAtIndex(i, this._ev);
				this.UpdateEventModifiers(in this._ev);
				EventType type = this._ev.type;
				EventType eventType = type;
				if (eventType - EventType.KeyDown > 1)
				{
					if (eventType - EventType.ValidateCommand <= 1)
					{
						Event @event = Event.From(this.ToCommandEvent(in this._ev));
						EventProvider.Dispatch(in @event);
					}
				}
				else
				{
					bool flag = this._ev.keyCode > KeyCode.None;
					if (flag)
					{
						Event @event = Event.From(this.ToKeyEvent(in this._ev));
						EventProvider.Dispatch(in @event);
						bool sendNavigationEventOnTabKey = this._sendNavigationEventOnTabKey;
						if (sendNavigationEventOnTabKey)
						{
							this.SendNextOrPreviousNavigationEventOnTabKeyDownEvent(in this._ev);
						}
						bool flag2 = this._ev.character != '\0' && this._ev.type == EventType.KeyDown;
						if (flag2)
						{
							@event = Event.From(this.ToTextInputEvent(in this._ev));
							EventProvider.Dispatch(in @event);
						}
					}
					else
					{
						bool flag3 = this._ev.type == EventType.KeyDown;
						if (flag3)
						{
							Event @event = Event.From(this.ToTextInputEvent(in this._ev));
							EventProvider.Dispatch(in @event);
						}
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000036A0 File Offset: 0x000018A0
		public void OnFocusChanged(bool focus)
		{
			bool flag = !focus;
			if (flag)
			{
				this._eventModifiers.Reset();
				this._keyboardButtonsState.Reset();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000036D0 File Offset: 0x000018D0
		public bool RequestCurrentState(Event.Type type)
		{
			bool flag;
			if (type != Event.Type.KeyEvent)
			{
				flag = false;
			}
			else
			{
				Event @event = Event.From(new KeyEvent
				{
					type = KeyEvent.Type.State,
					keyCode = KeyCode.None,
					buttonsState = this._keyboardButtonsState,
					timestamp = (DiscreteTime)Time.timeAsRational,
					eventSource = EventSource.Keyboard,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
				flag = true;
			}
			return flag;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000375A File Offset: 0x0000195A
		public uint playerCount
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003760 File Offset: 0x00001960
		private DiscreteTime GetTimestamp(in Event ev)
		{
			return (DiscreteTime)Time.timeAsRational;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000377C File Offset: 0x0000197C
		private void UpdateEventModifiers(in Event ev)
		{
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.CapsLock, ev.capsLock);
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.FunctionKey, ev.functionKey);
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.Numeric, ev.numeric);
			bool flag = ev.isKey && ev.keyCode > KeyCode.None;
			if (flag)
			{
				bool flag2 = ev.type == EventType.KeyDown;
				switch (ev.keyCode)
				{
				case KeyCode.Numlock:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.Numlock, flag2);
					break;
				case KeyCode.RightShift:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightShift, flag2);
					break;
				case KeyCode.LeftShift:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftShift, flag2);
					break;
				case KeyCode.RightControl:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightCtrl, flag2);
					break;
				case KeyCode.LeftControl:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftCtrl, flag2);
					break;
				case KeyCode.RightAlt:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightAlt, flag2);
					break;
				case KeyCode.LeftAlt:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftAlt, flag2);
					break;
				case KeyCode.RightMeta:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightMeta, flag2);
					break;
				case KeyCode.LeftMeta:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftMeta, flag2);
					break;
				}
			}
			bool flag3 = ev.shift != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Shift);
			if (flag3)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Shift, ev.shift);
			}
			bool flag4 = ev.control != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Ctrl);
			if (flag4)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Ctrl, ev.control);
			}
			bool flag5 = ev.alt != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Alt);
			if (flag5)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Alt, ev.alt);
			}
			bool flag6 = ev.command != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Meta);
			if (flag6)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Meta, ev.command);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000039B8 File Offset: 0x00001BB8
		private KeyEvent ToKeyEvent(in Event ev)
		{
			bool flag = this._keyboardButtonsState.IsPressed(ev.keyCode);
			bool flag2 = ev.type == EventType.KeyDown;
			this._keyboardButtonsState.SetPressed(ev.keyCode, flag2);
			return new KeyEvent
			{
				type = (flag2 ? (flag ? KeyEvent.Type.KeyRepeated : KeyEvent.Type.KeyPressed) : KeyEvent.Type.KeyReleased),
				keyCode = ev.keyCode,
				buttonsState = this._keyboardButtonsState,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Keyboard,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A64 File Offset: 0x00001C64
		private TextInputEvent ToTextInputEvent(in Event ev)
		{
			return new TextInputEvent
			{
				character = ev.character,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Keyboard,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003ABC File Offset: 0x00001CBC
		private void SendNextOrPreviousNavigationEventOnTabKeyDownEvent(in Event ev)
		{
			bool flag = this._ev.type == EventType.KeyDown && this._ev.keyCode == KeyCode.Tab;
			if (flag)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = (this._ev.shift ? NavigationEvent.Direction.Previous : NavigationEvent.Direction.Next),
					timestamp = this.GetTimestamp(in this._ev),
					eventSource = EventSource.Keyboard,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B5C File Offset: 0x00001D5C
		private CommandEvent ToCommandEvent(in Event ev)
		{
			CommandEvent.Command command;
			bool flag = !this._IMGUICommandToInputForUICommandType.TryGetValue(ev.commandName, out command);
			if (flag)
			{
				Debug.LogWarning("Unsupported command name '" + ev.commandName + "'");
			}
			return new CommandEvent
			{
				type = ((ev.type == EventType.ValidateCommand) ? CommandEvent.Type.Validate : CommandEvent.Type.Execute),
				command = command,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Unspecified,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x040000B5 RID: 181
		private const int kDefaultPlayerId = 0;

		// Token: 0x040000B6 RID: 182
		private Event _ev = new Event();

		// Token: 0x040000B7 RID: 183
		private OperatingSystemFamily _operatingSystemFamily;

		// Token: 0x040000B8 RID: 184
		private KeyEvent.ButtonsState _keyboardButtonsState;

		// Token: 0x040000B9 RID: 185
		internal EventModifiers _eventModifiers;

		// Token: 0x040000BA RID: 186
		internal bool _sendNavigationEventOnTabKey;

		// Token: 0x040000BB RID: 187
		private IDictionary<string, CommandEvent.Command> _IMGUICommandToInputForUICommandType = new Dictionary<string, CommandEvent.Command>
		{
			{
				"Cut",
				CommandEvent.Command.Cut
			},
			{
				"Copy",
				CommandEvent.Command.Copy
			},
			{
				"Paste",
				CommandEvent.Command.Paste
			},
			{
				"SelectAll",
				CommandEvent.Command.SelectAll
			},
			{
				"DeselectAll",
				CommandEvent.Command.DeselectAll
			},
			{
				"InvertSelection",
				CommandEvent.Command.InvertSelection
			},
			{
				"Duplicate",
				CommandEvent.Command.Duplicate
			},
			{
				"Rename",
				CommandEvent.Command.Rename
			},
			{
				"Delete",
				CommandEvent.Command.Delete
			},
			{
				"SoftDelete",
				CommandEvent.Command.SoftDelete
			},
			{
				"Find",
				CommandEvent.Command.Find
			},
			{
				"SelectChildren",
				CommandEvent.Command.SelectChildren
			},
			{
				"SelectPrefabRoot",
				CommandEvent.Command.SelectPrefabRoot
			},
			{
				"UndoRedoPerformed",
				CommandEvent.Command.UndoRedoPerformed
			},
			{
				"OnLostFocus",
				CommandEvent.Command.OnLostFocus
			},
			{
				"NewKeyboardFocus",
				CommandEvent.Command.NewKeyboardFocus
			},
			{
				"ModifierKeysChanged",
				CommandEvent.Command.ModifierKeysChanged
			},
			{
				"EyeDropperUpdate",
				CommandEvent.Command.EyeDropperUpdate
			},
			{
				"EyeDropperClicked",
				CommandEvent.Command.EyeDropperClicked
			},
			{
				"EyeDropperCancelled",
				CommandEvent.Command.EyeDropperCancelled
			},
			{
				"ColorPickerChanged",
				CommandEvent.Command.ColorPickerChanged
			},
			{
				"FrameSelected",
				CommandEvent.Command.FrameSelected
			},
			{
				"FrameSelectedWithLock",
				CommandEvent.Command.FrameSelectedWithLock
			}
		};
	}
}
