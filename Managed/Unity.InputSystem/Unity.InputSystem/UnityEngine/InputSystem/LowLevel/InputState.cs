using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FA RID: 250
	public static class InputState
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00048108 File Offset: 0x00046308
		public static InputUpdateType currentUpdateType
		{
			get
			{
				return InputUpdate.s_LatestUpdateType;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0004810F File Offset: 0x0004630F
		public static uint updateCount
		{
			get
			{
				return InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00048116 File Offset: 0x00046316
		public static double currentTime
		{
			get
			{
				return InputRuntime.s_Instance.currentTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000EBE RID: 3774 RVA: 0x00048128 File Offset: 0x00046328
		// (remove) Token: 0x06000EBF RID: 3775 RVA: 0x00048135 File Offset: 0x00046335
		public static event Action<InputDevice, InputEventPtr> onChange
		{
			add
			{
				InputSystem.s_Manager.onDeviceStateChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onDeviceStateChange -= value;
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00048144 File Offset: 0x00046344
		public unsafe static void Change(InputDevice device, InputEventPtr eventPtr, InputUpdateType updateType = InputUpdateType.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC type = eventPtr.type;
			FourCC fourCC;
			if (type == 1398030676)
			{
				fourCC = StateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			else
			{
				if (!(type == 1145852993))
				{
					return;
				}
				fourCC = DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			if (fourCC != device.stateBlock.format)
			{
				throw new ArgumentException(string.Format("State format {0} from event does not match state format {1} of device {2}", fourCC, device.stateBlock.format, device), "eventPtr");
			}
			InputSystem.s_Manager.UpdateState(device, eventPtr, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00048223 File Offset: 0x00046423
		public static void Change<TState>(InputControl control, TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			InputState.Change<TState>(control, ref state, updateType, eventPtr);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00048230 File Offset: 0x00046430
		public unsafe static void Change<TState>(InputControl control, ref TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U || control.stateBlock.sizeInBits % 8U != 0U)
			{
				throw new ArgumentException(string.Format("Cannot change state of bitfield control '{0}' using this method", control), "control");
			}
			InputDevice device = control.device;
			long num = Math.Min((long)UnsafeUtility.SizeOf<TState>(), (long)((ulong)control.m_StateBlock.alignedSizeInBytes));
			void* ptr = UnsafeUtility.AddressOf<TState>(ref state);
			uint num2 = control.stateBlock.byteOffset - device.stateBlock.byteOffset;
			InputSystem.s_Manager.UpdateState(device, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType, ptr, num2, (uint)num, eventPtr.valid ? eventPtr.internalTime : InputRuntime.s_Instance.currentTime, eventPtr);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00048308 File Offset: 0x00046508
		public static bool IsIntegerFormat(this FourCC format)
		{
			return format == InputStateBlock.FormatBit || format == InputStateBlock.FormatInt || format == InputStateBlock.FormatByte || format == InputStateBlock.FormatShort || format == InputStateBlock.FormatSBit || format == InputStateBlock.FormatUInt || format == InputStateBlock.FormatUShort || format == InputStateBlock.FormatLong || format == InputStateBlock.FormatULong;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004838C File Offset: 0x0004658C
		public static void AddChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L, uint groupIndex = 0U)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			if (!control.device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control));
			}
			InputSystem.s_Manager.AddStateChangeMonitor(control, monitor, monitorIndex, groupIndex);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000483E4 File Offset: 0x000465E4
		public static IInputStateChangeMonitor AddChangeMonitor(InputControl control, Action<InputControl, double, InputEventPtr, long> valueChangeCallback, int monitorIndex = -1, Action<InputControl, double, long, int> timerExpiredCallback = null)
		{
			if (valueChangeCallback == null)
			{
				throw new ArgumentNullException("valueChangeCallback");
			}
			InputState.StateChangeMonitorDelegate stateChangeMonitorDelegate = new InputState.StateChangeMonitorDelegate
			{
				valueChangeCallback = valueChangeCallback,
				timerExpiredCallback = timerExpiredCallback
			};
			InputState.AddChangeMonitor(control, stateChangeMonitorDelegate, (long)monitorIndex, 0U);
			return stateChangeMonitorDelegate;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004841E File Offset: 0x0004661E
		public static void RemoveChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitor(control, monitor, monitorIndex);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00048449 File Offset: 0x00046649
		public static void AddChangeMonitorTimeout(InputControl control, IInputStateChangeMonitor monitor, double time, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.AddStateChangeMonitorTimeout(control, monitor, time, monitorIndex, timerIndex);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00048469 File Offset: 0x00046669
		public static void RemoveChangeMonitorTimeout(IInputStateChangeMonitor monitor, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(monitor, monitorIndex, timerIndex);
		}

		// Token: 0x02000223 RID: 547
		private class StateChangeMonitorDelegate : IInputStateChangeMonitor
		{
			// Token: 0x06001550 RID: 5456 RVA: 0x00063624 File Offset: 0x00061824
			public void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
			{
				this.valueChangeCallback(control, time, eventPtr, monitorIndex);
			}

			// Token: 0x06001551 RID: 5457 RVA: 0x00063636 File Offset: 0x00061836
			public void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
			{
				Action<InputControl, double, long, int> action = this.timerExpiredCallback;
				if (action == null)
				{
					return;
				}
				action(control, time, monitorIndex, timerIndex);
			}

			// Token: 0x04000BCC RID: 3020
			public Action<InputControl, double, InputEventPtr, long> valueChangeCallback;

			// Token: 0x04000BCD RID: 3021
			public Action<InputControl, double, long, int> timerExpiredCallback;
		}
	}
}
