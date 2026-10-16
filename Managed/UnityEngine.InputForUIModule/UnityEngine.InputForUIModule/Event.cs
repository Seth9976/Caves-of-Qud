using System;
using System.Runtime.InteropServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000005 RID: 5
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[StructLayout(LayoutKind.Explicit)]
	internal struct Event : IEventProperties
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020C8 File Offset: 0x000002C8
		internal static int CompareType(Event a, Event b)
		{
			bool flag = a.type == Event.Type.PointerEvent && b.type == Event.Type.PointerEvent;
			int num;
			if (flag)
			{
				int eventSource = (int)a.eventSource;
				num = ((int)b.eventSource).CompareTo(eventSource);
			}
			else
			{
				int type = (int)a.type;
				int type2 = (int)b.type;
				num = type.CompareTo(type2);
			}
			return num;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212D File Offset: 0x0000032D
		public Event.Type type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002135 File Offset: 0x00000335
		private IEventProperties asObject
		{
			get
			{
				return this.Map<IEventProperties, Event.MapAsObject>();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213D File Offset: 0x0000033D
		public DiscreteTime timestamp
		{
			get
			{
				return this.Map<DiscreteTime, Event.MapAsTimestamp>();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002145 File Offset: 0x00000345
		public EventSource eventSource
		{
			get
			{
				return this.Map<EventSource, Event.MapAsEventSource>();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000214D File Offset: 0x0000034D
		public uint playerId
		{
			get
			{
				return this.Map<uint, Event.MapAsPlayerId>();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002155 File Offset: 0x00000355
		public EventModifiers eventModifiers
		{
			get
			{
				return this.Map<EventModifiers, Event.MapAsEventModifiers>();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000215D File Offset: 0x0000035D
		private void Ensure(Event.Type t)
		{
			Debug.Assert(this.type == t);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002170 File Offset: 0x00000370
		public override string ToString()
		{
			string text = this.eventModifiers.ToString();
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				text = " ev:" + text;
			}
			return (this.type == Event.Type.Invalid) ? "Invalid" : string.Format("{0}{1} src:{2}", this.asObject, text, this.eventSource.ToString());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E8 File Offset: 0x000003E8
		public static Event From(KeyEvent keyEvent)
		{
			return new Event
			{
				_type = Event.Type.KeyEvent,
				_keyEvent = keyEvent
			};
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002214 File Offset: 0x00000414
		public KeyEvent asKeyEvent
		{
			get
			{
				this.Ensure(Event.Type.KeyEvent);
				return this._keyEvent;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002234 File Offset: 0x00000434
		public static Event From(PointerEvent pointerEvent)
		{
			return new Event
			{
				_type = Event.Type.PointerEvent,
				_pointerEvent = pointerEvent
			};
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002260 File Offset: 0x00000460
		public PointerEvent asPointerEvent
		{
			get
			{
				this.Ensure(Event.Type.PointerEvent);
				return this._pointerEvent;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002280 File Offset: 0x00000480
		public static Event From(TextInputEvent textInputEvent)
		{
			return new Event
			{
				_type = Event.Type.TextInputEvent,
				_textInputEvent = textInputEvent
			};
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022AC File Offset: 0x000004AC
		public TextInputEvent asTextInputEvent
		{
			get
			{
				this.Ensure(Event.Type.TextInputEvent);
				return this._textInputEvent;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022CC File Offset: 0x000004CC
		public static Event From(IMECompositionEvent imeCompositionEvent)
		{
			return new Event
			{
				_type = Event.Type.IMECompositionEvent,
				_managedEvent = imeCompositionEvent
			};
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022FC File Offset: 0x000004FC
		public IMECompositionEvent asIMECompositionEvent
		{
			get
			{
				this.Ensure(Event.Type.IMECompositionEvent);
				return (IMECompositionEvent)this._managedEvent;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002324 File Offset: 0x00000524
		public static Event From(CommandEvent commandEvent)
		{
			return new Event
			{
				_type = Event.Type.CommandEvent,
				_commandEvent = commandEvent
			};
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002350 File Offset: 0x00000550
		public CommandEvent asCommandEvent
		{
			get
			{
				this.Ensure(Event.Type.CommandEvent);
				return this._commandEvent;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002370 File Offset: 0x00000570
		public static Event From(NavigationEvent navigationEvent)
		{
			return new Event
			{
				_type = Event.Type.NavigationEvent,
				_navigationEvent = navigationEvent
			};
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000239C File Offset: 0x0000059C
		public NavigationEvent asNavigationEvent
		{
			get
			{
				this.Ensure(Event.Type.NavigationEvent);
				return this._navigationEvent;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023BC File Offset: 0x000005BC
		private TOutputType Map<TOutputType, TMapType>(TMapType fn) where TMapType : Event.IMapFn<TOutputType>
		{
			TOutputType toutputType;
			switch (this.type)
			{
			case Event.Type.Invalid:
				toutputType = default(TOutputType);
				break;
			case Event.Type.KeyEvent:
				toutputType = fn.Map<KeyEvent>(ref this._keyEvent);
				break;
			case Event.Type.PointerEvent:
				toutputType = fn.Map<PointerEvent>(ref this._pointerEvent);
				break;
			case Event.Type.TextInputEvent:
				toutputType = fn.Map<TextInputEvent>(ref this._textInputEvent);
				break;
			case Event.Type.IMECompositionEvent:
			{
				IMECompositionEvent imecompositionEvent = (IMECompositionEvent)this._managedEvent;
				toutputType = fn.Map<IMECompositionEvent>(ref imecompositionEvent);
				break;
			}
			case Event.Type.CommandEvent:
				toutputType = fn.Map<CommandEvent>(ref this._commandEvent);
				break;
			case Event.Type.NavigationEvent:
				toutputType = fn.Map<NavigationEvent>(ref this._navigationEvent);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return toutputType;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000249E File Offset: 0x0000069E
		private TOutputType Map<TOutputType, TMapType>() where TMapType : Event.IMapFn<TOutputType>, new()
		{
			return this.Map<TOutputType, TMapType>(new TMapType());
		}

		// Token: 0x04000023 RID: 35
		public static Event.Type[] TypesWithState = new Event.Type[]
		{
			Event.Type.KeyEvent,
			Event.Type.PointerEvent,
			Event.Type.IMECompositionEvent
		};

		// Token: 0x04000024 RID: 36
		private const int kManagedOffset = 8;

		// Token: 0x04000025 RID: 37
		private const int kUnmanagedOffset = 16;

		// Token: 0x04000026 RID: 38
		[FieldOffset(0)]
		private Event.Type _type;

		// Token: 0x04000027 RID: 39
		[FieldOffset(8)]
		private object _managedEvent;

		// Token: 0x04000028 RID: 40
		[FieldOffset(16)]
		private KeyEvent _keyEvent;

		// Token: 0x04000029 RID: 41
		[FieldOffset(16)]
		private PointerEvent _pointerEvent;

		// Token: 0x0400002A RID: 42
		[FieldOffset(16)]
		private TextInputEvent _textInputEvent;

		// Token: 0x0400002B RID: 43
		[FieldOffset(16)]
		private CommandEvent _commandEvent;

		// Token: 0x0400002C RID: 44
		[FieldOffset(16)]
		private NavigationEvent _navigationEvent;

		// Token: 0x02000006 RID: 6
		public enum Type
		{
			// Token: 0x0400002E RID: 46
			Invalid,
			// Token: 0x0400002F RID: 47
			KeyEvent,
			// Token: 0x04000030 RID: 48
			PointerEvent,
			// Token: 0x04000031 RID: 49
			TextInputEvent,
			// Token: 0x04000032 RID: 50
			IMECompositionEvent,
			// Token: 0x04000033 RID: 51
			CommandEvent,
			// Token: 0x04000034 RID: 52
			NavigationEvent
		}

		// Token: 0x02000007 RID: 7
		private interface IMapFn<TOutputType>
		{
			// Token: 0x06000022 RID: 34
			TOutputType Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties;
		}

		// Token: 0x02000008 RID: 8
		private struct MapAsObject : Event.IMapFn<IEventProperties>
		{
			// Token: 0x06000023 RID: 35 RVA: 0x000024C3 File Offset: 0x000006C3
			public IEventProperties Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties
			{
				return ev;
			}
		}

		// Token: 0x02000009 RID: 9
		private struct MapAsTimestamp : Event.IMapFn<DiscreteTime>
		{
			// Token: 0x06000024 RID: 36 RVA: 0x000024D0 File Offset: 0x000006D0
			public DiscreteTime Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties
			{
				return ev.timestamp;
			}
		}

		// Token: 0x0200000A RID: 10
		private struct MapAsEventSource : Event.IMapFn<EventSource>
		{
			// Token: 0x06000025 RID: 37 RVA: 0x000024DE File Offset: 0x000006DE
			public EventSource Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties
			{
				return ev.eventSource;
			}
		}

		// Token: 0x0200000B RID: 11
		private struct MapAsPlayerId : Event.IMapFn<uint>
		{
			// Token: 0x06000026 RID: 38 RVA: 0x000024EC File Offset: 0x000006EC
			public uint Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties
			{
				return ev.playerId;
			}
		}

		// Token: 0x0200000C RID: 12
		private struct MapAsEventModifiers : Event.IMapFn<EventModifiers>
		{
			// Token: 0x06000027 RID: 39 RVA: 0x000024FA File Offset: 0x000006FA
			public EventModifiers Map<TEventType>(ref TEventType ev) where TEventType : IEventProperties
			{
				return ev.eventModifiers;
			}
		}
	}
}
