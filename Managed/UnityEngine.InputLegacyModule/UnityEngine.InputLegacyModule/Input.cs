using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000267D File Offset: 0x0000087D
		public static float GetAxis(string axisName)
		{
			return InputUnsafeUtility.GetAxis(axisName);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002685 File Offset: 0x00000885
		public static float GetAxisRaw(string axisName)
		{
			return InputUnsafeUtility.GetAxisRaw(axisName);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000268D File Offset: 0x0000088D
		public static bool GetButton(string buttonName)
		{
			return InputUnsafeUtility.GetButton(buttonName);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002695 File Offset: 0x00000895
		public static bool GetButtonDown(string buttonName)
		{
			return InputUnsafeUtility.GetButtonDown(buttonName);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000269D File Offset: 0x0000089D
		public static bool GetButtonUp(string buttonName)
		{
			return InputUnsafeUtility.GetButtonUp(buttonName);
		}

		// Token: 0x06000063 RID: 99
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(KeyCode key);

		// Token: 0x06000064 RID: 100
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(KeyCode key);

		// Token: 0x06000065 RID: 101
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(KeyCode key);

		// Token: 0x06000066 RID: 102
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x06000067 RID: 103
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x06000068 RID: 104
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x06000069 RID: 105
		[FreeFunction("ResetInput")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();

		// Token: 0x0600006A RID: 106
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		// Token: 0x0600006B RID: 107 RVA: 0x000026A8 File Offset: 0x000008A8
		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			Touch touch;
			Input.GetTouch_Injected(index, out touch);
			return touch;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000026C0 File Offset: 0x000008C0
		[NativeThrows]
		public static PenData GetPenEvent(int index)
		{
			PenData penData;
			Input.GetPenEvent_Injected(index, out penData);
			return penData;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000026D8 File Offset: 0x000008D8
		[NativeThrows]
		public static PenData GetLastPenContactEvent()
		{
			PenData penData;
			Input.GetLastPenContactEvent_Injected(out penData);
			return penData;
		}

		// Token: 0x0600006E RID: 110
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetPenEvents();

		// Token: 0x0600006F RID: 111
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearLastPenContactEvent();

		// Token: 0x06000070 RID: 112 RVA: 0x000026F0 File Offset: 0x000008F0
		[NativeThrows]
		public static AccelerationEvent GetAccelerationEvent(int index)
		{
			AccelerationEvent accelerationEvent;
			Input.GetAccelerationEvent_Injected(index, out accelerationEvent);
			return accelerationEvent;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002706 File Offset: 0x00000906
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt(key);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000270E File Offset: 0x0000090E
		public static bool GetKey(string name)
		{
			return InputUnsafeUtility.GetKeyString(name);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002716 File Offset: 0x00000916
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt(key);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000271E File Offset: 0x0000091E
		public static bool GetKeyUp(string name)
		{
			return InputUnsafeUtility.GetKeyUpString(name);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002726 File Offset: 0x00000926
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt(key);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000272E File Offset: 0x0000092E
		public static bool GetKeyDown(string name)
		{
			return InputUnsafeUtility.GetKeyDownString(name);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002736 File Offset: 0x00000936
		[Conditional("UNITY_EDITOR")]
		internal static void SimulateTouch(Touch touch)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000273C File Offset: 0x0000093C
		[FreeFunction("SimulateTouch")]
		[NativeConditional("UNITY_EDITOR")]
		[Conditional("UNITY_EDITOR")]
		private static void SimulateTouchInternal(Touch touch, long timestamp)
		{
			Input.SimulateTouchInternal_Injected(ref touch, timestamp);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000079 RID: 121
		// (set) Token: 0x0600007A RID: 122
		public static extern bool simulateMouseWithTouches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123
		[NativeThrows]
		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124
		[NativeThrows]
		public static extern bool anyKeyDown
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002754 File Offset: 0x00000954
		[NativeThrows]
		public static string inputString
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Input.get_inputString_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002784 File Offset: 0x00000984
		[NativeThrows]
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 vector;
				Input.get_mousePosition_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000279C File Offset: 0x0000099C
		[NativeThrows]
		public static Vector3 mousePositionDelta
		{
			get
			{
				Vector3 vector;
				Input.get_mousePositionDelta_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000027B4 File Offset: 0x000009B4
		[NativeThrows]
		public static Vector2 mouseScrollDelta
		{
			get
			{
				Vector2 vector;
				Input.get_mouseScrollDelta_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		public static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000027CC File Offset: 0x000009CC
		public static string compositionString
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Input.get_compositionString_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000084 RID: 132
		public static extern bool imeIsSelected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002814 File Offset: 0x00000A14
		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 vector;
				Input.get_compositionCursorPos_Injected(out vector);
				return vector;
			}
			set
			{
				Input.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000282F File Offset: 0x00000A2F
		internal static bool simulateTouchEnabled { get; set; }

		// Token: 0x0600008B RID: 139
		[FreeFunction("GetMousePresent")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMousePresentInternal();

		// Token: 0x0600008C RID: 140
		[FreeFunction("IsTouchSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetTouchSupportedInternal();

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002837 File Offset: 0x00000A37
		public static bool mousePresent
		{
			get
			{
				return !Input.simulateTouchEnabled && Input.GetMousePresentInternal();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002848 File Offset: 0x00000A48
		public static bool touchSupported
		{
			get
			{
				return Input.simulateTouchEnabled || Input.GetTouchSupportedInternal();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143
		public static extern int penEventCount
		{
			[FreeFunction("GetPenEventCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144
		public static extern int touchCount
		{
			[FreeFunction("GetTouchCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145
		public static extern bool touchPressureSupported
		{
			[FreeFunction("IsTouchPressureSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000092 RID: 146
		public static extern bool stylusTouchSupported
		{
			[FreeFunction("IsStylusTouchSupported")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		public static extern bool multiTouchEnabled
		{
			[FreeFunction("IsMultiTouchEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetMultiTouchEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000095 RID: 149
		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable
		{
			[FreeFunction("IsGyroAvailable")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000096 RID: 150
		public static extern DeviceOrientation deviceOrientation
		{
			[FreeFunction("GetDeviceOrientation")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000285C File Offset: 0x00000A5C
		public static Vector3 acceleration
		{
			[FreeFunction("GetAcceleration")]
			get
			{
				Vector3 vector;
				Input.get_acceleration_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000098 RID: 152
		// (set) Token: 0x06000099 RID: 153
		public static extern bool compensateSensors
		{
			[FreeFunction("IsCompensatingSensors")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetCompensatingSensors")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009A RID: 154
		public static extern int accelerationEventCount
		{
			[FreeFunction("GetAccelerationCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		public static extern bool backButtonLeavesApp
		{
			[FreeFunction("GetBackButtonLeavesApp")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction("SetBackButtonLeavesApp")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002874 File Offset: 0x00000A74
		public static LocationService location
		{
			get
			{
				bool flag = Input.locationServiceInstance == null;
				if (flag)
				{
					Input.locationServiceInstance = new LocationService();
				}
				return Input.locationServiceInstance;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000028A4 File Offset: 0x00000AA4
		public static Compass compass
		{
			get
			{
				bool flag = Input.compassInstance == null;
				if (flag)
				{
					Input.compassInstance = new Compass();
				}
				return Input.compassInstance;
			}
		}

		// Token: 0x0600009F RID: 159
		[FreeFunction("GetGyro")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGyroInternal();

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static Gyroscope gyro
		{
			get
			{
				bool flag = Input.s_MainGyro == null;
				if (flag)
				{
					Input.s_MainGyro = new Gyroscope(Input.GetGyroInternal());
				}
				return Input.s_MainGyro;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002908 File Offset: 0x00000B08
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002948 File Offset: 0x00000B48
		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int accelerationEventCount = Input.accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[accelerationEventCount];
				for (int i = 0; i < accelerationEventCount; i++)
				{
					array[i] = Input.GetAccelerationEvent(i);
				}
				return array;
			}
		}

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckDisabled();

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPenEvent_Injected(int index, out PenData ret);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastPenContactEvent_Injected(out PenData ret);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccelerationEvent_Injected(int index, out AccelerationEvent ret);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SimulateTouchInternal_Injected([In] ref Touch touch, long timestamp);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inputString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mousePositionDelta_Injected(out Vector3 ret);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositionCursorPos_Injected([In] ref Vector2 value);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_acceleration_Injected(out Vector3 ret);

		// Token: 0x0400004C RID: 76
		private static LocationService locationServiceInstance;

		// Token: 0x0400004D RID: 77
		private static Compass compassInstance;

		// Token: 0x0400004E RID: 78
		private static Gyroscope s_MainGyro;
	}
}
