using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("GUIEvent", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/IMGUI/Event.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Event
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[NativeProperty("type", false, TargetType.Field)]
		public EventType rawType
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_rawType_Injected(intPtr);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		[NativeProperty("mousePosition", false, TargetType.Field)]
		public Vector2 mousePosition
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_mousePosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_mousePosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		[NativeProperty("delta", false, TargetType.Field)]
		public Vector2 delta
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_delta_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_delta_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		[NativeProperty("pointerType", false, TargetType.Field)]
		public PointerType pointerType
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_pointerType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_pointerType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		[NativeProperty("button", false, TargetType.Field)]
		public int button
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_button_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_button_Injected(intPtr, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000219C File Offset: 0x0000039C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021C0 File Offset: 0x000003C0
		[NativeProperty("modifiers", false, TargetType.Field)]
		public EventModifiers modifiers
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_modifiers_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_modifiers_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021E4 File Offset: 0x000003E4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		[NativeProperty("pressure", false, TargetType.Field)]
		public float pressure
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_pressure_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_pressure_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000222C File Offset: 0x0000042C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002250 File Offset: 0x00000450
		[NativeProperty("twist", false, TargetType.Field)]
		public float twist
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_twist_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_twist_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		[NativeProperty("tilt", false, TargetType.Field)]
		public Vector2 tilt
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Event.get_tilt_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_tilt_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022C0 File Offset: 0x000004C0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022E4 File Offset: 0x000004E4
		[NativeProperty("penStatus", false, TargetType.Field)]
		public PenStatus penStatus
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_penStatus_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_penStatus_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002308 File Offset: 0x00000508
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000232C File Offset: 0x0000052C
		[NativeProperty("clickCount", false, TargetType.Field)]
		public int clickCount
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_clickCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_clickCount_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002350 File Offset: 0x00000550
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		[NativeProperty("character", false, TargetType.Field)]
		public char character
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_character_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_character_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023BC File Offset: 0x000005BC
		[NativeProperty("keycode", false, TargetType.Field)]
		private KeyCode Internal_keyCode
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_Internal_keyCode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_Internal_keyCode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002439 File Offset: 0x00000639
		public KeyCode keyCode
		{
			get
			{
				KeyCode keyCode = (this.isMouse ? (KeyCode.Mouse0 + this.button) : this.Internal_keyCode);
				bool isScrollWheel = this.isScrollWheel;
				if (isScrollWheel)
				{
					keyCode = ((this.delta.y < 0f) ? KeyCode.WheelUp : KeyCode.WheelDown);
				}
				return keyCode;
			}
			set
			{
				this.Internal_keyCode = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002468 File Offset: 0x00000668
		[NativeProperty("displayIndex", false, TargetType.Field)]
		public int displayIndex
		{
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_displayIndex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_displayIndex_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000024B0 File Offset: 0x000006B0
		public EventType type
		{
			[FreeFunction("GUIEvent::GetType", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Event.get_type_Injected(intPtr);
			}
			[FreeFunction("GUIEvent::SetType", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Event.set_type_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002514 File Offset: 0x00000714
		public unsafe string commandName
		{
			[FreeFunction("GUIEvent::GetCommandName", HasExplicitThis = true)]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					Event.get_commandName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction("GUIEvent::SetCommandName", HasExplicitThis = true)]
			set
			{
				try
				{
					IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Event.set_commandName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002578 File Offset: 0x00000778
		[NativeMethod("Use")]
		private void Internal_Use()
		{
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Event.Internal_Use_Injected(intPtr);
		}

		// Token: 0x06000023 RID: 35
		[FreeFunction("GUIEvent::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create(int displayIndex);

		// Token: 0x06000024 RID: 36
		[FreeFunction("GUIEvent::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000025 RID: 37
		[FreeFunction("GUIEvent::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Copy(IntPtr otherPtr);

		// Token: 0x06000026 RID: 38 RVA: 0x0000259C File Offset: 0x0000079C
		[FreeFunction("GUIEvent::GetTypeForControl", HasExplicitThis = true)]
		public EventType GetTypeForControl(int controlID)
		{
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Event.GetTypeForControl_Injected(intPtr, controlID);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C0 File Offset: 0x000007C0
		[FreeFunction("GUIEvent::CopyFromPtr", IsThreadSafe = true, HasExplicitThis = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void CopyFromPtr(IntPtr ptr)
		{
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Event.CopyFromPtr_Injected(intPtr, ptr);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025E4 File Offset: 0x000007E4
		public static bool PopEvent([NotNull] Event outEvent)
		{
			if (outEvent == null)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(outEvent);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			return Event.PopEvent_Injected(intPtr);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000261C File Offset: 0x0000081C
		internal static void QueueEvent([NotNull] Event outEvent)
		{
			if (outEvent == null)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(outEvent);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			Event.QueueEvent_Injected(intPtr);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002654 File Offset: 0x00000854
		[VisibleToOtherModules(new string[] { "UnityEngine.InputForUIModule" })]
		internal static void GetEventAtIndex(int index, [NotNull] Event outEvent)
		{
			if (outEvent == null)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			IntPtr intPtr = Event.BindingsMarshaller.ConvertToNative(outEvent);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(outEvent, "outEvent");
			}
			Event.GetEventAtIndex_Injected(index, intPtr);
		}

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEventCount();

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearEvents();

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNativeEvent(IntPtr ptr);

		// Token: 0x0600002E RID: 46 RVA: 0x0000268C File Offset: 0x0000088C
		[RequiredByNativeCode]
		internal static void Internal_MakeMasterEventCurrent(int displayIndex)
		{
			bool flag = Event.s_MasterEvent == null;
			if (flag)
			{
				Event.s_MasterEvent = new Event(displayIndex);
			}
			Event.s_MasterEvent.displayIndex = displayIndex;
			Event.s_Current = Event.s_MasterEvent;
			Event.Internal_SetNativeEvent(Event.s_MasterEvent.m_Ptr);
		}

		// Token: 0x0600002F RID: 47
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.InputForUIModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDoubleClickTime();

		// Token: 0x06000030 RID: 48 RVA: 0x000026D7 File Offset: 0x000008D7
		public Event()
		{
			this.m_Ptr = Event.Internal_Create(0);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026ED File Offset: 0x000008ED
		public Event(int displayIndex)
		{
			this.m_Ptr = Event.Internal_Create(displayIndex);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002704 File Offset: 0x00000904
		public Event(Event other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentException("Event to copy from is null.");
			}
			this.m_Ptr = Event.Internal_Copy(other.m_Ptr);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002740 File Offset: 0x00000940
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					Event.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002798 File Offset: 0x00000998
		internal static void CleanupRoots()
		{
			Event.s_Current = null;
			Event.s_MasterEvent = null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027A8 File Offset: 0x000009A8
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void CopyFrom(Event e)
		{
			bool flag = e.m_Ptr != this.m_Ptr;
			if (flag)
			{
				this.CopyFromPtr(e.m_Ptr);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027DC File Offset: 0x000009DC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027FD File Offset: 0x000009FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);", true)]
		public Ray mouseRay
		{
			get
			{
				return new Ray(Vector3.up, Vector3.up);
			}
			set
			{
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002820 File Offset: 0x00000A20
		public bool shift
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Shift;
				}
				else
				{
					this.modifiers |= EventModifiers.Shift;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002858 File Offset: 0x00000A58
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002878 File Offset: 0x00000A78
		public bool control
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Control;
				}
				else
				{
					this.modifiers |= EventModifiers.Control;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000028D0 File Offset: 0x00000AD0
		public bool alt
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Alt;
				}
				else
				{
					this.modifiers |= EventModifiers.Alt;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002908 File Offset: 0x00000B08
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002928 File Offset: 0x00000B28
		public bool command
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Command;
				}
				else
				{
					this.modifiers |= EventModifiers.Command;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002980 File Offset: 0x00000B80
		public bool capsLock
		{
			get
			{
				return (this.modifiers & EventModifiers.CapsLock) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.CapsLock;
				}
				else
				{
					this.modifiers |= EventModifiers.CapsLock;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000029D8 File Offset: 0x00000BD8
		public bool numeric
		{
			get
			{
				return (this.modifiers & EventModifiers.Numeric) > EventModifiers.None;
			}
			set
			{
				bool flag = !value;
				if (flag)
				{
					this.modifiers &= ~EventModifiers.Numeric;
				}
				else
				{
					this.modifiers |= EventModifiers.Numeric;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A10 File Offset: 0x00000C10
		public bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) > EventModifiers.None;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A20 File Offset: 0x00000C20
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002A37 File Offset: 0x00000C37
		public static Event current
		{
			get
			{
				return Event.s_Current;
			}
			set
			{
				Event.s_Current = value ?? Event.s_MasterEvent;
				Event.Internal_SetNativeEvent(Event.s_Current.m_Ptr);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A5C File Offset: 0x00000C5C
		public bool isKey
		{
			get
			{
				EventType type = this.type;
				return type == EventType.KeyDown || type == EventType.KeyUp;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A80 File Offset: 0x00000C80
		public bool isMouse
		{
			get
			{
				EventType type = this.type;
				return type == EventType.MouseMove || type == EventType.MouseDown || type == EventType.MouseUp || type == EventType.MouseDrag || type == EventType.ContextClick || type == EventType.MouseEnterWindow || type == EventType.MouseLeaveWindow;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002ABC File Offset: 0x00000CBC
		public bool isScrollWheel
		{
			get
			{
				EventType type = this.type;
				return type == EventType.ScrollWheel;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002ADC File Offset: 0x00000CDC
		internal bool isDirectManipulationDevice
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				return this.pointerType == PointerType.Pen || this.pointerType == PointerType.Touch;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B04 File Offset: 0x00000D04
		public static Event KeyboardEvent(string key)
		{
			Event @event = new Event(0)
			{
				type = EventType.KeyDown
			};
			bool flag = string.IsNullOrEmpty(key);
			Event event2;
			if (flag)
			{
				event2 = @event;
			}
			else
			{
				int num = 0;
				for (;;)
				{
					bool flag2 = true;
					bool flag3 = num >= key.Length;
					if (flag3)
					{
						break;
					}
					char c = key[num];
					char c2 = c;
					switch (c2)
					{
					case '#':
						@event.modifiers |= EventModifiers.Shift;
						num++;
						break;
					case '$':
						goto IL_00CA;
					case '%':
						@event.modifiers |= EventModifiers.Command;
						num++;
						break;
					case '&':
						@event.modifiers |= EventModifiers.Alt;
						num++;
						break;
					default:
						if (c2 != '^')
						{
							goto IL_00CA;
						}
						@event.modifiers |= EventModifiers.Control;
						num++;
						break;
					}
					IL_00CE:
					if (!flag2)
					{
						break;
					}
					continue;
					IL_00CA:
					flag2 = false;
					goto IL_00CE;
				}
				string text = key.Substring(num, key.Length - num).ToLowerInvariant();
				string text2 = text;
				string text3 = text2;
				uint num2 = global::<PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num2 <= 2028154341U)
				{
					if (num2 <= 1035581717U)
					{
						if (num2 <= 371355806U)
						{
							if (num2 <= 203579616U)
							{
								if (num2 != 3281354U)
								{
									if (num2 != 20058973U)
									{
										if (num2 == 203579616U)
										{
											if (text3 == "f9")
											{
												@event.keyCode = KeyCode.F9;
												@event.modifiers |= EventModifiers.FunctionKey;
												goto IL_11D1;
											}
										}
									}
									else if (text3 == "f17")
									{
										@event.keyCode = KeyCode.F17;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
								else if (text3 == "f16")
								{
									@event.keyCode = KeyCode.F16;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
							else if (num2 <= 306900080U)
							{
								if (num2 != 220357235U)
								{
									if (num2 == 306900080U)
									{
										if (text3 == "left")
										{
											@event.keyCode = KeyCode.LeftArrow;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_11D1;
										}
									}
								}
								else if (text3 == "f8")
								{
									@event.keyCode = KeyCode.F8;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
							else if (num2 != 337800568U)
							{
								if (num2 == 371355806U)
								{
									if (text3 == "f3")
									{
										@event.keyCode = KeyCode.F3;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "f1")
							{
								@event.keyCode = KeyCode.F1;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 <= 421688663U)
						{
							if (num2 != 388133425U)
							{
								if (num2 != 404911044U)
								{
									if (num2 == 421688663U)
									{
										if (text3 == "f4")
										{
											@event.keyCode = KeyCode.F4;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_11D1;
										}
									}
								}
								else if (text3 == "f5")
								{
									@event.keyCode = KeyCode.F5;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
							else if (text3 == "f2")
							{
								@event.keyCode = KeyCode.F2;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 <= 455243901U)
						{
							if (num2 != 438466282U)
							{
								if (num2 == 455243901U)
								{
									if (text3 == "f6")
									{
										@event.keyCode = KeyCode.F6;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "f7")
							{
								@event.keyCode = KeyCode.F7;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 != 894689925U)
						{
							if (num2 == 1035581717U)
							{
								if (text3 == "down")
								{
									@event.keyCode = KeyCode.DownArrow;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "space")
						{
							@event.keyCode = KeyCode.Space;
							@event.character = ' ';
							@event.modifiers &= ~EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 <= 1815705491U)
					{
						if (num2 <= 1193063839U)
						{
							if (num2 != 1113118030U)
							{
								if (num2 != 1128467232U)
								{
									if (num2 == 1193063839U)
									{
										if (text3 == "page up")
										{
											@event.keyCode = KeyCode.PageUp;
											@event.modifiers |= EventModifiers.FunctionKey;
											goto IL_11D1;
										}
									}
								}
								else if (text3 == "up")
								{
									@event.keyCode = KeyCode.UpArrow;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
							else if (text3 == "[equals]")
							{
								@event.character = '=';
								@event.keyCode = KeyCode.KeypadEquals;
								goto IL_11D1;
							}
						}
						else if (num2 <= 1787721130U)
						{
							if (num2 != 1740784714U)
							{
								if (num2 == 1787721130U)
								{
									if (text3 == "end")
									{
										@event.keyCode = KeyCode.End;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "delete")
							{
								@event.keyCode = KeyCode.Delete;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 != 1798927872U)
						{
							if (num2 == 1815705491U)
							{
								if (text3 == "f20")
								{
									@event.keyCode = KeyCode.F20;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "f21")
						{
							@event.keyCode = KeyCode.F21;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 <= 1980614408U)
					{
						if (num2 <= 1849260729U)
						{
							if (num2 != 1832483110U)
							{
								if (num2 == 1849260729U)
								{
									if (text3 == "f22")
									{
										@event.keyCode = KeyCode.F22;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "f23")
							{
								@event.keyCode = KeyCode.F23;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 != 1882815967U)
						{
							if (num2 == 1980614408U)
							{
								if (text3 == "[=]")
								{
									@event.character = '=';
									@event.keyCode = KeyCode.KeypadEquals;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "f24")
						{
							@event.keyCode = KeyCode.F24;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 <= 1981202788U)
					{
						if (num2 != 1980761503U)
						{
							if (num2 == 1981202788U)
							{
								if (text3 == "[1]")
								{
									@event.character = '1';
									@event.keyCode = KeyCode.Keypad1;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "[2]")
						{
							@event.character = '2';
							@event.keyCode = KeyCode.Keypad2;
							goto IL_11D1;
						}
					}
					else if (num2 != 1981894336U)
					{
						if (num2 == 2028154341U)
						{
							if (text3 == "right")
							{
								@event.keyCode = KeyCode.RightArrow;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
					}
					else if (text3 == "[5]")
					{
						@event.character = '5';
						@event.keyCode = KeyCode.Keypad5;
						goto IL_11D1;
					}
				}
				else if (num2 <= 3121933785U)
				{
					if (num2 <= 2566336076U)
					{
						if (num2 <= 2049990550U)
						{
							if (num2 != 2048857717U)
							{
								if (num2 != 2049299002U)
								{
									if (num2 == 2049990550U)
									{
										if (text3 == "[/]")
										{
											@event.character = '/';
											@event.keyCode = KeyCode.KeypadDivide;
											goto IL_11D1;
										}
									}
								}
								else if (text3 == "[+]")
								{
									@event.character = '+';
									@event.keyCode = KeyCode.KeypadPlus;
									goto IL_11D1;
								}
							}
							else if (text3 == "[4]")
							{
								@event.character = '4';
								@event.keyCode = KeyCode.Keypad4;
								goto IL_11D1;
							}
						}
						else if (num2 <= 2235328556U)
						{
							if (num2 != 2130866490U)
							{
								if (num2 == 2235328556U)
								{
									if (text3 == "backspace")
									{
										@event.keyCode = KeyCode.Backspace;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "page down")
							{
								@event.keyCode = KeyCode.PageDown;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (num2 != 2246981567U)
						{
							if (num2 == 2566336076U)
							{
								if (text3 == "tab")
								{
									@event.keyCode = KeyCode.Tab;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "return")
						{
							@event.character = '\n';
							@event.keyCode = KeyCode.Return;
							@event.modifiers &= ~EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 <= 3056397427U)
					{
						if (num2 != 3053690476U)
						{
							if (num2 != 3055117499U)
							{
								if (num2 == 3056397427U)
								{
									if (text3 == "[.]")
									{
										@event.character = '.';
										@event.keyCode = KeyCode.KeypadPeriod;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "[6]")
							{
								@event.character = '6';
								@event.keyCode = KeyCode.Keypad6;
								goto IL_11D1;
							}
						}
						else if (text3 == "[9]")
						{
							@event.character = '9';
							@event.keyCode = KeyCode.Keypad9;
							goto IL_11D1;
						}
					}
					else if (num2 <= 3120653857U)
					{
						if (num2 != 3056941880U)
						{
							if (num2 == 3120653857U)
							{
								if (text3 == "[8]")
								{
									@event.character = '8';
									@event.keyCode = KeyCode.Keypad8;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "[-]")
						{
							@event.character = '-';
							@event.keyCode = KeyCode.KeypadMinus;
							goto IL_11D1;
						}
					}
					else if (num2 != 3121786690U)
					{
						if (num2 == 3121933785U)
						{
							if (text3 == "[0]")
							{
								@event.character = '0';
								@event.keyCode = KeyCode.Keypad0;
								goto IL_11D1;
							}
						}
					}
					else if (text3 == "[3]")
					{
						@event.character = '3';
						@event.keyCode = KeyCode.Keypad3;
						goto IL_11D1;
					}
				}
				else if (num2 <= 4080139603U)
				{
					if (num2 <= 3536372366U)
					{
						if (num2 != 3122375070U)
						{
							if (num2 != 3332609576U)
							{
								if (num2 == 3536372366U)
								{
									if (text3 == "home")
									{
										@event.keyCode = KeyCode.Home;
										@event.modifiers |= EventModifiers.FunctionKey;
										goto IL_11D1;
									}
								}
							}
							else if (text3 == "insert")
							{
								@event.keyCode = KeyCode.Insert;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
						else if (text3 == "[7]")
						{
							@event.character = '7';
							@event.keyCode = KeyCode.Keypad7;
							goto IL_11D1;
						}
					}
					else if (num2 <= 3984432914U)
					{
						if (num2 != 3906143141U)
						{
							if (num2 == 3984432914U)
							{
								if (text3 == "[esc]")
								{
									@event.keyCode = KeyCode.Escape;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "pgup")
						{
							@event.keyCode = KeyCode.PageDown;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 != 4063361984U)
					{
						if (num2 == 4080139603U)
						{
							if (text3 == "f19")
							{
								@event.keyCode = KeyCode.F19;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
					}
					else if (text3 == "f18")
					{
						@event.keyCode = KeyCode.F18;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_11D1;
					}
				}
				else if (num2 <= 4227375619U)
				{
					if (num2 <= 4213014532U)
					{
						if (num2 != 4197582936U)
						{
							if (num2 == 4213014532U)
							{
								if (text3 == "pgdown")
								{
									@event.keyCode = KeyCode.PageUp;
									@event.modifiers |= EventModifiers.FunctionKey;
									goto IL_11D1;
								}
							}
						}
						else if (text3 == "f10")
						{
							@event.keyCode = KeyCode.F10;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
					else if (num2 != 4214360555U)
					{
						if (num2 == 4227375619U)
						{
							if (text3 == "[enter]")
							{
								@event.character = '\n';
								@event.keyCode = KeyCode.KeypadEnter;
								goto IL_11D1;
							}
						}
					}
					else if (text3 == "f11")
					{
						@event.keyCode = KeyCode.F11;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_11D1;
					}
				}
				else if (num2 <= 4247915793U)
				{
					if (num2 != 4231138174U)
					{
						if (num2 == 4247915793U)
						{
							if (text3 == "f13")
							{
								@event.keyCode = KeyCode.F13;
								@event.modifiers |= EventModifiers.FunctionKey;
								goto IL_11D1;
							}
						}
					}
					else if (text3 == "f12")
					{
						@event.keyCode = KeyCode.F12;
						@event.modifiers |= EventModifiers.FunctionKey;
						goto IL_11D1;
					}
				}
				else if (num2 != 4264693412U)
				{
					if (num2 == 4281471031U)
					{
						if (text3 == "f15")
						{
							@event.keyCode = KeyCode.F15;
							@event.modifiers |= EventModifiers.FunctionKey;
							goto IL_11D1;
						}
					}
				}
				else if (text3 == "f14")
				{
					@event.keyCode = KeyCode.F14;
					@event.modifiers |= EventModifiers.FunctionKey;
					goto IL_11D1;
				}
				bool flag4 = text.Length != 1;
				if (flag4)
				{
					try
					{
						@event.keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), text, true);
					}
					catch (ArgumentException)
					{
						Debug.LogError(UnityString.Format("Unable to find key name that matches '{0}'", new object[] { text }));
					}
				}
				else
				{
					@event.character = text.ToLower()[0];
					@event.keyCode = (KeyCode)@event.character;
					bool flag5 = @event.modifiers > EventModifiers.None;
					if (flag5)
					{
						@event.character = '\0';
					}
				}
				IL_11D1:
				event2 = @event;
			}
			return event2;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003CFC File Offset: 0x00001EFC
		public override int GetHashCode()
		{
			int num = 1;
			bool isKey = this.isKey;
			if (isKey)
			{
				num = (int)((ushort)this.keyCode);
			}
			bool isMouse = this.isMouse;
			if (isMouse)
			{
				num = this.mousePosition.GetHashCode();
			}
			return (num * 37) | (int)this.modifiers;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003D50 File Offset: 0x00001F50
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == obj;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = obj.GetType() != base.GetType();
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						Event @event = (Event)obj;
						bool flag5 = this.type != @event.type || (this.modifiers & ~EventModifiers.CapsLock) != (@event.modifiers & ~EventModifiers.CapsLock);
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							bool isKey = this.isKey;
							if (isKey)
							{
								flag2 = this.keyCode == @event.keyCode;
							}
							else
							{
								bool isMouse = this.isMouse;
								flag2 = isMouse && this.mousePosition == @event.mousePosition;
							}
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003E14 File Offset: 0x00002014
		public override string ToString()
		{
			bool isKey = this.isKey;
			string text;
			if (isKey)
			{
				bool flag = this.character == '\0';
				if (flag)
				{
					text = UnityString.Format("Event:{0}   Character:\\0   Modifiers:{1}   KeyCode:{2}", new object[] { this.type, this.modifiers, this.keyCode });
				}
				else
				{
					text = string.Concat(new string[]
					{
						"Event:",
						this.type.ToString(),
						"   Character:",
						((int)this.character).ToString(),
						"   Modifiers:",
						this.modifiers.ToString(),
						"   KeyCode:",
						this.keyCode.ToString()
					});
				}
			}
			else
			{
				bool isMouse = this.isMouse;
				if (isMouse)
				{
					text = UnityString.Format("Event: {0}   Position: {1} Modifiers: {2}", new object[] { this.type, this.mousePosition, this.modifiers });
				}
				else
				{
					bool flag2 = this.type == EventType.ExecuteCommand || this.type == EventType.ValidateCommand;
					if (flag2)
					{
						text = UnityString.Format("Event: {0}  \"{1}\"", new object[] { this.type, this.commandName });
					}
					else
					{
						text = this.type.ToString() ?? "";
					}
				}
			}
			return text;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void Use()
		{
			bool flag = this.type == EventType.Repaint || this.type == EventType.Layout;
			if (flag)
			{
				Debug.LogWarning(UnityString.Format("Event.Use() should not be called for events of type {0}", new object[] { this.type }));
			}
			this.Internal_Use();
		}

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventType get_rawType_Injected(IntPtr _unity_self);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mousePosition_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_delta_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delta_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PointerType get_pointerType_Injected(IntPtr _unity_self);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pointerType_Injected(IntPtr _unity_self, PointerType value);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_button_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_button_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventModifiers get_modifiers_Injected(IntPtr _unity_self);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_modifiers_Injected(IntPtr _unity_self, EventModifiers value);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pressure_Injected(IntPtr _unity_self);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pressure_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_twist_Injected(IntPtr _unity_self);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_twist_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tilt_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tilt_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PenStatus get_penStatus_Injected(IntPtr _unity_self);

		// Token: 0x06000062 RID: 98
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_penStatus_Injected(IntPtr _unity_self, PenStatus value);

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_clickCount_Injected(IntPtr _unity_self);

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clickCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern char get_character_Injected(IntPtr _unity_self);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_character_Injected(IntPtr _unity_self, char value);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern KeyCode get_Internal_keyCode_Injected(IntPtr _unity_self);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Internal_keyCode_Injected(IntPtr _unity_self, KeyCode value);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_displayIndex_Injected(IntPtr _unity_self);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_displayIndex_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventType get_type_Injected(IntPtr _unity_self);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_type_Injected(IntPtr _unity_self, EventType value);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_commandName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_commandName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Use_Injected(IntPtr _unity_self);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventType GetTypeForControl_Injected(IntPtr _unity_self, int controlID);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyFromPtr_Injected(IntPtr _unity_self, IntPtr ptr);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool PopEvent_Injected(IntPtr outEvent);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void QueueEvent_Injected(IntPtr outEvent);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetEventAtIndex_Injected(int index, IntPtr outEvent);

		// Token: 0x04000001 RID: 1
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000002 RID: 2
		internal const float scrollWheelDeltaPerTick = 3f;

		// Token: 0x04000003 RID: 3
		internal static bool ignoreGuiDepth;

		// Token: 0x04000004 RID: 4
		private static Event s_Current;

		// Token: 0x04000005 RID: 5
		private static Event s_MasterEvent;

		// Token: 0x02000003 RID: 3
		internal static class BindingsMarshaller
		{
			// Token: 0x06000075 RID: 117 RVA: 0x0000400A File Offset: 0x0000220A
			public static IntPtr ConvertToNative(Event e)
			{
				return e.m_Ptr;
			}
		}
	}
}
