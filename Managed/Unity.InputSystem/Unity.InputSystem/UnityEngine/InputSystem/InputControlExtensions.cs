using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000031 RID: 49
	public static class InputControlExtensions
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0001108C File Offset: 0x0000F28C
		public static TControl FindInParentChain<TControl>(this InputControl control) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			for (InputControl inputControl = control; inputControl != null; inputControl = inputControl.parent)
			{
				TControl tcontrol = inputControl as TControl;
				if (tcontrol != null)
				{
					return tcontrol;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public static bool IsPressed(this InputControl control, float buttonPressPoint = 0f)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (Mathf.Approximately(0f, buttonPressPoint))
			{
				ButtonControl buttonControl = control as ButtonControl;
				if (buttonControl != null)
				{
					buttonPressPoint = buttonControl.pressPointOrDefault;
				}
				else
				{
					buttonPressPoint = ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
			}
			return control.IsActuated(buttonPressPoint);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011120 File Offset: 0x0000F320
		public static bool IsActuated(this InputControl control, float threshold = 0f)
		{
			if (control.CheckStateIsAtDefault())
			{
				return false;
			}
			float magnitude = control.magnitude;
			if (magnitude < 0f)
			{
				return Mathf.Approximately(threshold, 0f);
			}
			if (Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011174 File Offset: 0x0000F374
		public static object ReadValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.currentStatePtr);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011190 File Offset: 0x0000F390
		public unsafe static void ReadValueIntoBuffer(this InputControl control, void* buffer, int bufferSize)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			control.ReadValueFromStateIntoBuffer(control.currentStatePtr, buffer, bufferSize);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000111BE File Offset: 0x0000F3BE
		public static object ReadDefaultValueAsObject(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.ReadValueFromStateAsObject(control.defaultStatePtr);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000111DC File Offset: 0x0000F3DC
		public static TValue ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue tvalue;
			if (!control.ReadValueFromEvent<TValue>(inputEvent, out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011210 File Offset: 0x0000F410
		public unsafe static bool ReadValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadValueFromState(statePtrFromStateEvent);
			return true;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011254 File Offset: 0x0000F454
		public unsafe static object ReadValueFromEventAsObject(this InputControl control, InputEventPtr inputEvent)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				return control.ReadDefaultValueAsObject();
			}
			return control.ReadValueFromStateAsObject(statePtrFromStateEvent);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001128C File Offset: 0x0000F48C
		public static TValue ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			TValue tvalue = default(TValue);
			control.ReadUnprocessedValueFromEvent<TValue>(eventPtr, out tvalue);
			return tvalue;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000112BC File Offset: 0x0000F4BC
		public unsafe static bool ReadUnprocessedValueFromEvent<TValue>(this InputControl<TValue> control, InputEventPtr inputEvent, out TValue value) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(inputEvent);
			if (statePtrFromStateEvent == null)
			{
				value = control.ReadDefaultValue();
				return false;
			}
			value = control.ReadUnprocessedValueFromState(statePtrFromStateEvent);
			return true;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011300 File Offset: 0x0000F500
		public unsafe static void WriteValueFromObjectIntoEvent(this InputControl control, InputEventPtr eventPtr, object value)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return;
			}
			control.WriteValueFromObjectIntoState(value, statePtrFromStateEvent);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011334 File Offset: 0x0000F534
		public unsafe static void WriteValueIntoState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int valueSizeInBytes = control.valueSizeInBytes;
			void* ptr = UnsafeUtility.Malloc((long)valueSizeInBytes, 8, Allocator.Temp);
			try
			{
				control.ReadValueFromStateIntoBuffer(control.currentStatePtr, ptr, valueSizeInBytes);
				control.WriteValueFromBufferIntoState(ptr, valueSizeInBytes, statePtr);
			}
			finally
			{
				UnsafeUtility.Free(ptr, Allocator.Temp);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public unsafe static void WriteValueIntoState<TValue>(this InputControl control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControl<TValue> inputControl = control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			inputControl.WriteValueIntoState(value, statePtr);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001141C File Offset: 0x0000F61C
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, TValue value, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref value);
			int num = UnsafeUtility.SizeOf<TValue>();
			control.WriteValueFromBufferIntoState(ptr, num, statePtr);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001145E File Offset: 0x0000F65E
		public unsafe static void WriteValueIntoState<TValue>(this InputControl<TValue> control, void* statePtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			control.WriteValueIntoState(control.ReadValue(), statePtr);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001147C File Offset: 0x0000F67C
		public unsafe static void WriteValueIntoState<TValue, TState>(this InputControl<TValue> control, TValue value, ref TState state) where TValue : struct where TState : struct, IInputStateTypeInfo
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int num = UnsafeUtility.SizeOf<TState>();
			if ((ulong)(control.stateOffsetRelativeToDeviceRoot + control.m_StateBlock.alignedSizeInBytes) >= (ulong)((long)num))
			{
				throw new ArgumentException(string.Format("Control {0} with offset {1} and size of {2} bits is out of bounds for state of type {3} with size {4}", new object[]
				{
					control.path,
					control.stateOffsetRelativeToDeviceRoot,
					control.m_StateBlock.sizeInBits,
					typeof(TState).Name,
					num
				}), "state");
			}
			byte* ptr = (byte*)UnsafeUtility.AddressOf<TState>(ref state);
			control.WriteValueIntoState(value, (void*)ptr);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011524 File Offset: 0x0000F724
		public static void WriteValueIntoEvent<TValue>(this InputControl control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			InputControl<TValue> inputControl = control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expecting control of type '",
					typeof(TValue).Name,
					"' but got '",
					control.GetType().Name,
					"'"
				}));
			}
			inputControl.WriteValueIntoEvent(value, eventPtr);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public unsafe static void WriteValueIntoEvent<TValue>(this InputControl<TValue> control, TValue value, InputEventPtr eventPtr) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return;
			}
			control.WriteValueIntoState(value, statePtrFromStateEvent);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000115F8 File Offset: 0x0000F7F8
		public unsafe static void CopyState(this InputDevice device, void* buffer, int bufferSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (bufferSizeInBytes <= 0)
			{
				throw new ArgumentException("bufferSizeInBytes must be positive", "bufferSizeInBytes");
			}
			InputStateBlock stateBlock = device.m_StateBlock;
			long num = Math.Min((long)bufferSizeInBytes, (long)((ulong)stateBlock.alignedSizeInBytes));
			UnsafeUtility.MemCpy(buffer, (void*)((byte*)device.currentStatePtr + stateBlock.byteOffset), num);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00011654 File Offset: 0x0000F854
		public unsafe static void CopyState<TState>(this InputDevice device, out TState state) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			state = default(TState);
			if (device.stateBlock.format != state.format)
			{
				throw new ArgumentException(string.Format("Struct '{0}' has state format '{1}' which doesn't match device '{2}' with state format '{3}'", new object[]
				{
					typeof(TState).Name,
					state.format,
					device,
					device.stateBlock.format
				}), "TState");
			}
			int num = UnsafeUtility.SizeOf<TState>();
			void* ptr = UnsafeUtility.AddressOf<TState>(ref state);
			device.CopyState(ptr, num);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00011707 File Offset: 0x0000F907
		public static bool CheckStateIsAtDefault(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefault(control.currentStatePtr, null);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00011725 File Offset: 0x0000F925
		public unsafe static bool CheckStateIsAtDefault(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(statePtr, control.defaultStatePtr, maskPtr);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00011753 File Offset: 0x0000F953
		public static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return control.CheckStateIsAtDefaultIgnoringNoise(control.currentStatePtr);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001176F File Offset: 0x0000F96F
		public unsafe static bool CheckStateIsAtDefaultIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CheckStateIsAtDefault(statePtr, InputStateBuffers.s_NoiseMaskBuffer);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001179B File Offset: 0x0000F99B
		public unsafe static bool CompareStateIgnoringNoise(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, control.noiseMaskPtr);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000117D0 File Offset: 0x0000F9D0
		public unsafe static bool CompareState(this InputControl control, void* firstStatePtr, void* secondStatePtr, void* maskPtr = null)
		{
			byte* ptr = (byte*)firstStatePtr + control.m_StateBlock.byteOffset;
			byte* ptr2 = (byte*)secondStatePtr + control.m_StateBlock.byteOffset;
			byte* ptr3 = ((maskPtr != null) ? ((byte*)maskPtr + control.m_StateBlock.byteOffset) : null);
			if (control.m_StateBlock.sizeInBits == 1U)
			{
				return (ptr3 != null && MemoryHelpers.ReadSingleBit((void*)ptr3, control.m_StateBlock.bitOffset)) || MemoryHelpers.ReadSingleBit((void*)ptr2, control.m_StateBlock.bitOffset) == MemoryHelpers.ReadSingleBit((void*)ptr, control.m_StateBlock.bitOffset);
			}
			return MemoryHelpers.MemCmpBitRegion((void*)ptr, (void*)ptr2, control.m_StateBlock.bitOffset, control.m_StateBlock.sizeInBits, (void*)ptr3);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001187B File Offset: 0x0000FA7B
		public unsafe static bool CompareState(this InputControl control, void* statePtr, void* maskPtr = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareState(control.currentStatePtr, statePtr, maskPtr);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000118A9 File Offset: 0x0000FAA9
		public unsafe static bool HasValueChangeInState(this InputControl control, void* statePtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return control.CompareValue(control.currentStatePtr, statePtr);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public unsafe static bool HasValueChangeInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			void* statePtrFromStateEvent = control.GetStatePtrFromStateEvent(eventPtr);
			return statePtrFromStateEvent != null && control.CompareValue(control.currentStatePtr, statePtrFromStateEvent);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011923 File Offset: 0x0000FB23
		public unsafe static void* GetStatePtrFromStateEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			return control.GetStatePtrFromStateEventUnchecked(eventPtr, eventPtr.type);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011958 File Offset: 0x0000FB58
		internal unsafe static void* GetStatePtrFromStateEventUnchecked(this InputControl control, InputEventPtr eventPtr, FourCC eventType)
		{
			uint num;
			FourCC fourCC;
			uint num2;
			void* ptr2;
			if (eventType == 1398030676)
			{
				StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
				num = 0U;
				fourCC = ptr->stateFormat;
				num2 = ptr->stateSizeInBytes;
				ptr2 = ptr->state;
			}
			else
			{
				if (!(eventType == 1145852993))
				{
					throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", eventType), "eventPtr");
				}
				DeltaStateEvent* ptr3 = DeltaStateEvent.FromUnchecked(eventPtr);
				num = ptr3->stateOffset;
				fourCC = ptr3->stateFormat;
				num2 = ptr3->deltaStateSizeInBytes;
				ptr2 = ptr3->deltaState;
			}
			InputDevice device = control.device;
			if (fourCC != device.m_StateBlock.format && (!device.hasStateCallbacks || !((IInputStateCallbackReceiver)device).GetStateOffsetForEvent(control, eventPtr, ref num)))
			{
				return null;
			}
			num += device.m_StateBlock.byteOffset;
			ref InputStateBlock ptr4 = ref control.m_StateBlock;
			long num3 = (long)ptr4.effectiveByteOffset - (long)((ulong)num);
			if (num3 < 0L || num3 + (long)((ulong)ptr4.alignedSizeInBytes) > (long)((ulong)num2))
			{
				return null;
			}
			return (void*)((byte*)ptr2 - num);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011A60 File Offset: 0x0000FC60
		public unsafe static bool ResetToDefaultStateInEvent(this InputControl control, InputEventPtr eventPtr)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new ArgumentException("Given event is not a StateEvent or a DeltaStateEvent", "eventPtr");
			}
			byte* statePtrFromStateEvent = (byte*)control.GetStatePtrFromStateEvent(eventPtr);
			if (statePtrFromStateEvent == null)
			{
				return false;
			}
			byte* defaultStatePtr = (byte*)control.defaultStatePtr;
			ref InputStateBlock ptr = ref control.m_StateBlock;
			uint byteOffset = ptr.byteOffset;
			MemoryHelpers.MemCpyBitRegion((void*)(statePtrFromStateEvent + byteOffset), (void*)(defaultStatePtr + byteOffset), ptr.bitOffset, ptr.sizeInBits);
			return true;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public static void QueueValueChange<TValue>(this InputControl<TValue> control, TValue value, double time = -1.0) where TValue : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputEventPtr inputEventPtr;
			using (StateEvent.From(control.device, out inputEventPtr, Allocator.Temp))
			{
				if (time >= 0.0)
				{
					inputEventPtr.time = time;
				}
				control.WriteValueIntoEvent(value, inputEventPtr);
				InputSystem.QueueEvent(inputEventPtr);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00011B78 File Offset: 0x0000FD78
		public unsafe static void AccumulateValueInEvent(this InputControl<float> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			float num;
			if (!control.ReadUnprocessedValueFromEvent<float>(newState, out num))
			{
				return;
			}
			float num2 = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(num2 + num, newState);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		internal unsafe static void AccumulateValueInEvent(this InputControl<Vector2> control, void* currentStatePtr, InputEventPtr newState)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Vector2 vector;
			if (!control.ReadUnprocessedValueFromEvent<Vector2>(newState, out vector))
			{
				return;
			}
			Vector2 vector2 = control.ReadUnprocessedValueFromState(currentStatePtr);
			control.WriteValueIntoEvent(vector2 + vector, newState);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		public static void FindControlsRecursive<TControl>(this InputControl parent, IList<TControl> controls, Func<TControl, bool> predicate) where TControl : InputControl
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			TControl tcontrol = parent as TControl;
			if (tcontrol != null && predicate(tcontrol))
			{
				controls.Add(tcontrol);
			}
			int count = parent.children.Count;
			for (int i = 0; i < count; i++)
			{
				parent.children[i].FindControlsRecursive<TControl>(controls, predicate);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011C84 File Offset: 0x0000FE84
		internal static string BuildPath(this InputControl control, string deviceLayout, StringBuilder builder = null)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(deviceLayout))
			{
				throw new ArgumentNullException("deviceLayout");
			}
			if (builder == null)
			{
				builder = new StringBuilder();
			}
			InputDevice device = control.device;
			builder.Append('<');
			builder.Append(deviceLayout.Escape("\\>", "\\>"));
			builder.Append('>');
			ReadOnlyArray<InternedString> usages = device.usages;
			for (int i = 0; i < usages.Count; i++)
			{
				builder.Append('{');
				builder.Append(usages[i].ToString().Escape("\\}", "\\}"));
				builder.Append('}');
			}
			builder.Append('/');
			string text = device.path.Replace("\\", "\\\\");
			string text2 = control.path.Replace("\\", "\\\\");
			builder.Append(text2, text.Length + 1, text2.Length - text.Length - 1);
			return builder.ToString();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public static InputControlExtensions.InputEventControlCollection EnumerateControls(this InputEventPtr eventPtr, InputControlExtensions.Enumerate flags, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr", "Given event pointer must not be null");
			}
			FourCC type = eventPtr.type;
			if (type != 1398030676 && type != 1145852993)
			{
				throw new ArgumentException(string.Format("Event must be a StateEvent or DeltaStateEvent but is a {0} instead", type), "eventPtr");
			}
			if (device == null)
			{
				int deviceId = eventPtr.deviceId;
				device = InputSystem.GetDeviceById(deviceId);
				if (device == null)
				{
					throw new ArgumentException(string.Format("Cannot find device with ID {0} referenced by event", deviceId), "eventPtr");
				}
			}
			return new InputControlExtensions.InputEventControlCollection
			{
				m_Device = device,
				m_EventPtr = eventPtr,
				m_Flags = flags,
				m_MagnitudeThreshold = magnitudeThreshold
			};
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011E6B File Offset: 0x0001006B
		public static InputControlExtensions.InputEventControlCollection EnumerateChangedControls(this InputEventPtr eventPtr, InputDevice device = null, float magnitudeThreshold = 0f)
		{
			return eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInCurrentState, device, magnitudeThreshold);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011E76 File Offset: 0x00010076
		public static bool HasButtonPress(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			return eventPtr.GetFirstButtonPressOrNull(magnitude, buttonControlsOnly) != null;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00011E84 File Offset: 0x00010084
		public static InputControl GetFirstButtonPressOrNull(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				return null;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (inputControl.HasValueChangeInEvent(eventPtr) && (!buttonControlsOnly || inputControl.isButton))
				{
					return inputControl;
				}
			}
			return null;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00011F38 File Offset: 0x00010138
		public static IEnumerable<InputControl> GetAllButtonPresses(this InputEventPtr eventPtr, float magnitude = -1f, bool buttonControlsOnly = true)
		{
			if (eventPtr.type != 1398030676 && eventPtr.type != 1145852993)
			{
				yield break;
			}
			if (magnitude < 0f)
			{
				magnitude = InputSystem.settings.defaultButtonPressPoint;
			}
			foreach (InputControl inputControl in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, null, magnitude))
			{
				if (!buttonControlsOnly || inputControl.isButton)
				{
					yield return inputControl;
				}
			}
			InputControlExtensions.InputEventControlEnumerator inputEventControlEnumerator = default(InputControlExtensions.InputEventControlEnumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00011F58 File Offset: 0x00010158
		public static InputControlExtensions.ControlBuilder Setup(this InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", control));
			}
			return new InputControlExtensions.ControlBuilder
			{
				control = control
			};
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00011FA0 File Offset: 0x000101A0
		public static InputControlExtensions.DeviceBuilder Setup(this InputDevice device, int controlCount, int usageCount, int aliasCount)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.isSetupFinished)
			{
				throw new InvalidOperationException(string.Format("The setup of {0} cannot be modified; control is already in use", device));
			}
			if (controlCount < 1)
			{
				throw new ArgumentOutOfRangeException("controlCount");
			}
			if (usageCount < 0)
			{
				throw new ArgumentOutOfRangeException("usageCount");
			}
			if (aliasCount < 0)
			{
				throw new ArgumentOutOfRangeException("aliasCount");
			}
			device.m_Device = device;
			device.m_ChildrenForEachControl = new InputControl[controlCount];
			if (usageCount > 0)
			{
				device.m_UsagesForEachControl = new InternedString[usageCount];
				device.m_UsageToControl = new InputControl[usageCount];
			}
			if (aliasCount > 0)
			{
				device.m_AliasesForEachControl = new InternedString[aliasCount];
			}
			return new InputControlExtensions.DeviceBuilder
			{
				device = device
			};
		}

		// Token: 0x0200018E RID: 398
		[Flags]
		public enum Enumerate
		{
			// Token: 0x040008B8 RID: 2232
			IgnoreControlsInDefaultState = 1,
			// Token: 0x040008B9 RID: 2233
			IgnoreControlsInCurrentState = 2,
			// Token: 0x040008BA RID: 2234
			IncludeSyntheticControls = 4,
			// Token: 0x040008BB RID: 2235
			IncludeNoisyControls = 8,
			// Token: 0x040008BC RID: 2236
			IncludeNonLeafControls = 16
		}

		// Token: 0x0200018F RID: 399
		public struct InputEventControlCollection : IEnumerable<InputControl>, IEnumerable
		{
			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0005CAE1 File Offset: 0x0005ACE1
			public InputEventPtr eventPtr
			{
				get
				{
					return this.m_EventPtr;
				}
			}

			// Token: 0x060013EA RID: 5098 RVA: 0x0005CAE9 File Offset: 0x0005ACE9
			public InputControlExtensions.InputEventControlEnumerator GetEnumerator()
			{
				return new InputControlExtensions.InputEventControlEnumerator(this.m_EventPtr, this.m_Device, this.m_Flags, this.m_MagnitudeThreshold);
			}

			// Token: 0x060013EB RID: 5099 RVA: 0x0005CB08 File Offset: 0x0005AD08
			IEnumerator<InputControl> IEnumerable<InputControl>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060013EC RID: 5100 RVA: 0x0005CB15 File Offset: 0x0005AD15
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040008BD RID: 2237
			internal InputDevice m_Device;

			// Token: 0x040008BE RID: 2238
			internal InputEventPtr m_EventPtr;

			// Token: 0x040008BF RID: 2239
			internal InputControlExtensions.Enumerate m_Flags;

			// Token: 0x040008C0 RID: 2240
			internal float m_MagnitudeThreshold;
		}

		// Token: 0x02000190 RID: 400
		public struct InputEventControlEnumerator : IEnumerator<InputControl>, IEnumerator, IDisposable
		{
			// Token: 0x060013ED RID: 5101 RVA: 0x0005CB24 File Offset: 0x0005AD24
			internal unsafe InputEventControlEnumerator(InputEventPtr eventPtr, InputDevice device, InputControlExtensions.Enumerate flags, float magnitudeThreshold = 0f)
			{
				this.m_Device = device;
				this.m_StateOffsetToControlIndex = device.m_StateOffsetToControlMap;
				this.m_StateOffsetToControlIndexLength = this.m_StateOffsetToControlIndex.LengthSafe<uint>();
				this.m_AllControls = device.m_ChildrenForEachControl;
				this.m_EventPtr = eventPtr;
				this.m_Flags = flags;
				this.m_CurrentControl = null;
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_EventState = default(byte*);
				this.m_CurrentBitOffset = 0U;
				this.m_EndBitOffset = 0U;
				this.m_MagnitudeThreshold = magnitudeThreshold;
				if ((flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0)
				{
					this.m_NoiseMask = (byte*)device.noiseMaskPtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_NoiseMask = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInDefaultState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_DefaultState = (byte*)device.defaultStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_DefaultState = default(byte*);
				}
				if ((flags & InputControlExtensions.Enumerate.IgnoreControlsInCurrentState) != (InputControlExtensions.Enumerate)0)
				{
					this.m_CurrentState = (byte*)device.currentStatePtr + device.m_StateBlock.byteOffset;
				}
				else
				{
					this.m_CurrentState = default(byte*);
				}
				this.Reset();
			}

			// Token: 0x060013EE RID: 5102 RVA: 0x0005CC30 File Offset: 0x0005AE30
			private unsafe bool CheckDefault(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x060013EF RID: 5103 RVA: 0x0005CC50 File Offset: 0x0005AE50
			private unsafe bool CheckCurrent(uint numBits)
			{
				return MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, this.m_CurrentBitOffset, numBits, (void*)this.m_NoiseMask);
			}

			// Token: 0x060013F0 RID: 5104 RVA: 0x0005CC70 File Offset: 0x0005AE70
			public unsafe bool MoveNext()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				if (this.m_CurrentControl != null && (this.m_Flags & InputControlExtensions.Enumerate.IncludeNonLeafControls) != (InputControlExtensions.Enumerate)0)
				{
					InputControl parent = this.m_CurrentControl.parent;
					if (parent != this.m_Device)
					{
						this.m_CurrentControl = parent;
						return true;
					}
				}
				bool flag = this.m_DefaultState != null;
				bool flag2 = this.m_CurrentState != null;
				for (;;)
				{
					this.m_CurrentControl = null;
					if (flag2 || flag)
					{
						if ((this.m_CurrentBitOffset & 7U) != 0U)
						{
							uint num = (this.m_CurrentBitOffset + 8U) & 7U;
							if ((flag2 && this.CheckCurrent(num)) || (flag && this.CheckDefault(num)))
							{
								this.m_CurrentBitOffset += num;
							}
						}
						while (this.m_CurrentBitOffset < this.m_EndBitOffset)
						{
							uint num2 = this.m_CurrentBitOffset >> 3;
							byte b = this.m_EventState[num2];
							int num3 = (int)((this.m_NoiseMask != null) ? this.m_NoiseMask[num2] : byte.MaxValue);
							if (flag2 && ((int)this.m_CurrentState[num2] & num3) == ((int)b & num3))
							{
								this.m_CurrentBitOffset += 8U;
							}
							else
							{
								if (!flag || ((int)this.m_DefaultState[num2] & num3) != ((int)b & num3))
								{
									break;
								}
								this.m_CurrentBitOffset += 8U;
							}
						}
					}
					if (this.m_CurrentBitOffset >= this.m_EndBitOffset || this.m_CurrentIndexInStateOffsetToControlIndexMap >= this.m_StateOffsetToControlIndexLength)
					{
						break;
					}
					while (this.m_CurrentIndexInStateOffsetToControlIndexMap < this.m_StateOffsetToControlIndexLength)
					{
						uint num4;
						uint num5;
						uint num6;
						InputDevice.DecodeStateOffsetToControlMapEntry(this.m_StateOffsetToControlIndex[this.m_CurrentIndexInStateOffsetToControlIndexMap], out num4, out num5, out num6);
						if (num5 >= this.m_CurrentControlStateBitOffset && this.m_CurrentBitOffset < num5 + num6 - this.m_CurrentControlStateBitOffset)
						{
							if (num5 - this.m_CurrentControlStateBitOffset >= this.m_CurrentBitOffset + 8U)
							{
								this.m_CurrentBitOffset = num5 - this.m_CurrentControlStateBitOffset;
								break;
							}
							if (num5 + num6 - this.m_CurrentControlStateBitOffset <= this.m_EndBitOffset)
							{
								if ((num5 & 7U) == 0U && (num6 & 7U) == 0U)
								{
									this.m_CurrentControl = this.m_AllControls[(int)num4];
								}
								else
								{
									if ((flag2 && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_CurrentState, num5 - this.m_CurrentControlStateBitOffset, num6, (void*)this.m_NoiseMask)) || (flag && MemoryHelpers.MemCmpBitRegion((void*)this.m_EventState, (void*)this.m_DefaultState, num5 - this.m_CurrentControlStateBitOffset, num6, (void*)this.m_NoiseMask)))
									{
										goto IL_02BF;
									}
									this.m_CurrentControl = this.m_AllControls[(int)num4];
								}
								if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeNoisyControls) == (InputControlExtensions.Enumerate)0 && this.m_CurrentControl.noisy)
								{
									this.m_CurrentControl = null;
								}
								else
								{
									if ((this.m_Flags & InputControlExtensions.Enumerate.IncludeSyntheticControls) != (InputControlExtensions.Enumerate)0 || (this.m_CurrentControl.m_ControlFlags & (InputControl.ControlFlags.IsSynthetic | InputControl.ControlFlags.UsesStateFromOtherControl)) <= (InputControl.ControlFlags)0)
									{
										this.m_CurrentIndexInStateOffsetToControlIndexMap++;
										break;
									}
									this.m_CurrentControl = null;
								}
							}
						}
						IL_02BF:
						this.m_CurrentIndexInStateOffsetToControlIndexMap++;
					}
					if (this.m_CurrentControl != null)
					{
						if (this.m_MagnitudeThreshold == 0f)
						{
							return true;
						}
						byte* ptr = this.m_EventState - (this.m_CurrentControlStateBitOffset >> 3) - this.m_Device.m_StateBlock.byteOffset;
						float num7 = this.m_CurrentControl.EvaluateMagnitude((void*)ptr);
						if (num7 < 0f || num7 >= this.m_MagnitudeThreshold)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060013F1 RID: 5105 RVA: 0x0005CFC0 File Offset: 0x0005B1C0
			public unsafe void Reset()
			{
				if (!this.m_EventPtr.valid)
				{
					throw new ObjectDisposedException("Enumerator has already been disposed");
				}
				FourCC type = this.m_EventPtr.type;
				FourCC fourCC;
				if (type == 1398030676)
				{
					StateEvent* ptr = StateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)ptr->state;
					this.m_EndBitOffset = ptr->stateSizeInBytes * 8U;
					this.m_CurrentBitOffset = 0U;
					fourCC = ptr->stateFormat;
				}
				else
				{
					if (!(type == 1145852993))
					{
						throw new NotSupportedException(string.Format("Cannot iterate over controls in event of type '{0}'", type));
					}
					DeltaStateEvent* ptr2 = DeltaStateEvent.FromUnchecked(this.m_EventPtr);
					this.m_EventState = (byte*)ptr2->deltaState - ptr2->stateOffset;
					this.m_CurrentBitOffset = ptr2->stateOffset * 8U;
					this.m_EndBitOffset = this.m_CurrentBitOffset + ptr2->deltaStateSizeInBytes * 8U;
					fourCC = ptr2->stateFormat;
				}
				this.m_CurrentIndexInStateOffsetToControlIndexMap = 0;
				this.m_CurrentControlStateBitOffset = 0U;
				this.m_CurrentControl = null;
				if (fourCC != this.m_Device.m_StateBlock.format)
				{
					uint num = 0U;
					if (this.m_Device.hasStateCallbacks && ((IInputStateCallbackReceiver)this.m_Device).GetStateOffsetForEvent(null, this.m_EventPtr, ref num))
					{
						this.m_CurrentControlStateBitOffset = num * 8U;
						if (this.m_CurrentState != null)
						{
							this.m_CurrentState += num;
						}
						if (this.m_DefaultState != null)
						{
							this.m_DefaultState += num;
						}
						if (this.m_NoiseMask != null)
						{
							this.m_NoiseMask += num;
							return;
						}
					}
					else if (!(this.m_Device is Touchscreen) || !this.m_EventPtr.IsA<StateEvent>() || !(StateEvent.FromUnchecked(this.m_EventPtr)->stateFormat == TouchState.Format))
					{
						throw new InvalidOperationException(string.Format("{0} event with state format {1} cannot be used with device '{2}'", type, fourCC, this.m_Device));
					}
				}
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x0005D1B9 File Offset: 0x0005B3B9
			public void Dispose()
			{
				this.m_EventPtr = default(InputEventPtr);
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0005D1C7 File Offset: 0x0005B3C7
			public InputControl Current
			{
				get
				{
					return this.m_CurrentControl;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0005D1CF File Offset: 0x0005B3CF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040008C1 RID: 2241
			private InputControlExtensions.Enumerate m_Flags;

			// Token: 0x040008C2 RID: 2242
			private readonly InputDevice m_Device;

			// Token: 0x040008C3 RID: 2243
			private readonly uint[] m_StateOffsetToControlIndex;

			// Token: 0x040008C4 RID: 2244
			private readonly int m_StateOffsetToControlIndexLength;

			// Token: 0x040008C5 RID: 2245
			private readonly InputControl[] m_AllControls;

			// Token: 0x040008C6 RID: 2246
			private unsafe byte* m_DefaultState;

			// Token: 0x040008C7 RID: 2247
			private unsafe byte* m_CurrentState;

			// Token: 0x040008C8 RID: 2248
			private unsafe byte* m_NoiseMask;

			// Token: 0x040008C9 RID: 2249
			private InputEventPtr m_EventPtr;

			// Token: 0x040008CA RID: 2250
			private InputControl m_CurrentControl;

			// Token: 0x040008CB RID: 2251
			private int m_CurrentIndexInStateOffsetToControlIndexMap;

			// Token: 0x040008CC RID: 2252
			private uint m_CurrentControlStateBitOffset;

			// Token: 0x040008CD RID: 2253
			private unsafe byte* m_EventState;

			// Token: 0x040008CE RID: 2254
			private uint m_CurrentBitOffset;

			// Token: 0x040008CF RID: 2255
			private uint m_EndBitOffset;

			// Token: 0x040008D0 RID: 2256
			private float m_MagnitudeThreshold;
		}

		// Token: 0x02000191 RID: 401
		public struct ControlBuilder
		{
			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0005D1D7 File Offset: 0x0005B3D7
			// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0005D1DF File Offset: 0x0005B3DF
			public InputControl control { readonly get; internal set; }

			// Token: 0x060013F7 RID: 5111 RVA: 0x0005D1E8 File Offset: 0x0005B3E8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder At(InputDevice device, int index)
			{
				device.m_ChildrenForEachControl[index] = this.control;
				this.control.m_Device = device;
				return this;
			}

			// Token: 0x060013F8 RID: 5112 RVA: 0x0005D20A File Offset: 0x0005B40A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithParent(InputControl parent)
			{
				this.control.m_Parent = parent;
				return this;
			}

			// Token: 0x060013F9 RID: 5113 RVA: 0x0005D21E File Offset: 0x0005B41E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithName(string name)
			{
				this.control.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x060013FA RID: 5114 RVA: 0x0005D237 File Offset: 0x0005B437
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDisplayName(string displayName)
			{
				this.control.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x0005D255 File Offset: 0x0005B455
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.control.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x0005D273 File Offset: 0x0005B473
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithLayout(InternedString layout)
			{
				this.control.m_Layout = layout;
				return this;
			}

			// Token: 0x060013FD RID: 5117 RVA: 0x0005D287 File Offset: 0x0005B487
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithUsages(int startIndex, int count)
			{
				this.control.m_UsageStartIndex = startIndex;
				this.control.m_UsageCount = count;
				return this;
			}

			// Token: 0x060013FE RID: 5118 RVA: 0x0005D2A7 File Offset: 0x0005B4A7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithAliases(int startIndex, int count)
			{
				this.control.m_AliasStartIndex = startIndex;
				this.control.m_AliasCount = count;
				return this;
			}

			// Token: 0x060013FF RID: 5119 RVA: 0x0005D2C7 File Offset: 0x0005B4C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithChildren(int startIndex, int count)
			{
				this.control.m_ChildStartIndex = startIndex;
				this.control.m_ChildCount = count;
				return this;
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x0005D2E7 File Offset: 0x0005B4E7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.control.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x0005D2FB File Offset: 0x0005B4FB
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithDefaultState(PrimitiveValue value)
			{
				this.control.m_DefaultState = value;
				this.control.m_Device.hasControlsWithDefaultState = true;
				return this;
			}

			// Token: 0x06001402 RID: 5122 RVA: 0x0005D320 File Offset: 0x0005B520
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithMinAndMax(PrimitiveValue min, PrimitiveValue max)
			{
				this.control.m_MinValue = min;
				this.control.m_MaxValue = max;
				return this;
			}

			// Token: 0x06001403 RID: 5123 RVA: 0x0005D340 File Offset: 0x0005B540
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder WithProcessor<TProcessor, TValue>(TProcessor processor) where TProcessor : InputProcessor<TValue> where TValue : struct
			{
				((InputControl<TValue>)this.control).m_ProcessorStack.Append(processor);
				return this;
			}

			// Token: 0x06001404 RID: 5124 RVA: 0x0005D364 File Offset: 0x0005B564
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsNoisy(bool value)
			{
				this.control.noisy = value;
				return this;
			}

			// Token: 0x06001405 RID: 5125 RVA: 0x0005D378 File Offset: 0x0005B578
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsSynthetic(bool value)
			{
				this.control.synthetic = value;
				return this;
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x0005D38C File Offset: 0x0005B58C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder DontReset(bool value)
			{
				this.control.dontReset = value;
				if (value)
				{
					this.control.m_Device.hasDontResetControls = true;
				}
				return this;
			}

			// Token: 0x06001407 RID: 5127 RVA: 0x0005D3B4 File Offset: 0x0005B5B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.ControlBuilder IsButton(bool value)
			{
				this.control.isButton = value;
				return this;
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x0005D3C8 File Offset: 0x0005B5C8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				this.control.isSetupFinished = true;
			}
		}

		// Token: 0x02000192 RID: 402
		public struct DeviceBuilder
		{
			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x06001409 RID: 5129 RVA: 0x0005D3D6 File Offset: 0x0005B5D6
			// (set) Token: 0x0600140A RID: 5130 RVA: 0x0005D3DE File Offset: 0x0005B5DE
			public InputDevice device { readonly get; internal set; }

			// Token: 0x0600140B RID: 5131 RVA: 0x0005D3E7 File Offset: 0x0005B5E7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithName(string name)
			{
				this.device.m_Name = new InternedString(name);
				return this;
			}

			// Token: 0x0600140C RID: 5132 RVA: 0x0005D400 File Offset: 0x0005B600
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithDisplayName(string displayName)
			{
				this.device.m_DisplayNameFromLayout = new InternedString(displayName);
				return this;
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x0005D41E File Offset: 0x0005B61E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithShortDisplayName(string shortDisplayName)
			{
				this.device.m_ShortDisplayNameFromLayout = new InternedString(shortDisplayName);
				return this;
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x0005D43C File Offset: 0x0005B63C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithLayout(InternedString layout)
			{
				this.device.m_Layout = layout;
				return this;
			}

			// Token: 0x0600140F RID: 5135 RVA: 0x0005D450 File Offset: 0x0005B650
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithChildren(int startIndex, int count)
			{
				this.device.m_ChildStartIndex = startIndex;
				this.device.m_ChildCount = count;
				return this;
			}

			// Token: 0x06001410 RID: 5136 RVA: 0x0005D470 File Offset: 0x0005B670
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateBlock(InputStateBlock stateBlock)
			{
				this.device.m_StateBlock = stateBlock;
				return this;
			}

			// Token: 0x06001411 RID: 5137 RVA: 0x0005D484 File Offset: 0x0005B684
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder IsNoisy(bool value)
			{
				this.device.noisy = value;
				return this;
			}

			// Token: 0x06001412 RID: 5138 RVA: 0x0005D498 File Offset: 0x0005B698
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlUsage(int controlIndex, InternedString usage, InputControl control)
			{
				this.device.m_UsagesForEachControl[controlIndex] = usage;
				this.device.m_UsageToControl[controlIndex] = control;
				return this;
			}

			// Token: 0x06001413 RID: 5139 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithControlAlias(int controlIndex, InternedString alias)
			{
				this.device.m_AliasesForEachControl[controlIndex] = alias;
				return this;
			}

			// Token: 0x06001414 RID: 5140 RVA: 0x0005D4DA File Offset: 0x0005B6DA
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public InputControlExtensions.DeviceBuilder WithStateOffsetToControlIndexMap(uint[] map)
			{
				this.device.m_StateOffsetToControlMap = map;
				return this;
			}

			// Token: 0x06001415 RID: 5141 RVA: 0x0005D4F0 File Offset: 0x0005B6F0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe InputControlExtensions.DeviceBuilder WithControlTree(byte[] controlTreeNodes, ushort[] controlTreeIndicies)
			{
				int num = UnsafeUtility.SizeOf<InputDevice.ControlBitRangeNode>();
				int num2 = controlTreeNodes.Length / num;
				this.device.m_ControlTreeNodes = new InputDevice.ControlBitRangeNode[num2];
				fixed (byte[] array = controlTreeNodes)
				{
					byte* ptr;
					if (controlTreeNodes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					for (int i = 0; i < num2; i++)
					{
						this.device.m_ControlTreeNodes[i] = *(InputDevice.ControlBitRangeNode*)(ptr + i * num);
					}
				}
				this.device.m_ControlTreeIndices = controlTreeIndicies;
				return this;
			}

			// Token: 0x06001416 RID: 5142 RVA: 0x0005D574 File Offset: 0x0005B774
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Finish()
			{
				int num = 0;
				using (ReadOnlyArray<InputControl>.Enumerator enumerator = this.device.allControls.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is ButtonControl)
						{
							num++;
						}
					}
				}
				this.device.m_ButtonControlsCheckingPressState = new List<ButtonControl>(num);
				this.device.m_UpdatedButtons = new HashSet<int>(num);
				this.device.isSetupFinished = true;
			}
		}
	}
}
