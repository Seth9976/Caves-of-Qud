using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F6 RID: 246
	internal class NativeInputRuntime : IInputRuntime
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00047E5C File Offset: 0x0004605C
		public int AllocateDeviceId()
		{
			return NativeInputSystem.AllocateDeviceId();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00047E63 File Offset: 0x00046063
		public void Update(InputUpdateType updateType)
		{
			NativeInputSystem.Update((NativeInputUpdateType)updateType);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00047E6B File Offset: 0x0004606B
		public unsafe void QueueEvent(InputEvent* ptr)
		{
			NativeInputSystem.QueueInputEvent((IntPtr)((void*)ptr));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00047E78 File Offset: 0x00046078
		public unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr)
		{
			if (commandPtr == null)
			{
				throw new ArgumentNullException("commandPtr");
			}
			return NativeInputSystem.IOCTL(deviceId, commandPtr->type, new IntPtr(commandPtr->payloadPtr), commandPtr->payloadSizeInBytes);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00047EAC File Offset: 0x000460AC
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00047EB4 File Offset: 0x000460B4
		public unsafe InputUpdateDelegate onUpdate
		{
			get
			{
				return this.m_OnUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onUpdate = delegate(NativeInputUpdateType updateType, NativeInputEventBuffer* eventBufferPtr)
					{
						InputEventBuffer inputEventBuffer = new InputEventBuffer((InputEvent*)eventBufferPtr->eventBuffer, eventBufferPtr->eventCount, eventBufferPtr->sizeInBytes, eventBufferPtr->capacityInBytes);
						try
						{
							value((InputUpdateType)updateType, ref inputEventBuffer);
						}
						catch (Exception ex)
						{
							Debug.LogException(ex);
							Debug.LogError(string.Format("{0} during event processing of {1} update; resetting event buffer", ex.GetType().Name, updateType));
							inputEventBuffer.Reset();
						}
						if (inputEventBuffer.eventCount > 0)
						{
							eventBufferPtr->eventCount = inputEventBuffer.eventCount;
							eventBufferPtr->sizeInBytes = (int)inputEventBuffer.sizeInBytes;
							eventBufferPtr->capacityInBytes = (int)inputEventBuffer.capacityInBytes;
							eventBufferPtr->eventBuffer = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(inputEventBuffer.data);
							return;
						}
						eventBufferPtr->eventCount = 0;
						eventBufferPtr->sizeInBytes = 0;
					};
				}
				else
				{
					NativeInputSystem.onUpdate = null;
				}
				this.m_OnUpdate = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00047EFB File Offset: 0x000460FB
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00047F04 File Offset: 0x00046104
		public Action<InputUpdateType> onBeforeUpdate
		{
			get
			{
				return this.m_OnBeforeUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onBeforeUpdate = delegate(NativeInputUpdateType updateType)
					{
						value((InputUpdateType)updateType);
					};
				}
				else
				{
					NativeInputSystem.onBeforeUpdate = null;
				}
				this.m_OnBeforeUpdate = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00047F4B File Offset: 0x0004614B
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x00047F54 File Offset: 0x00046154
		public Func<InputUpdateType, bool> onShouldRunUpdate
		{
			get
			{
				return this.m_OnShouldRunUpdate;
			}
			set
			{
				if (value != null)
				{
					NativeInputSystem.onShouldRunUpdate = (NativeInputUpdateType updateType) => value((InputUpdateType)updateType);
				}
				else
				{
					NativeInputSystem.onShouldRunUpdate = null;
				}
				this.m_OnShouldRunUpdate = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00047F9B File Offset: 0x0004619B
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00047FA2 File Offset: 0x000461A2
		public Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.onDeviceDiscovered;
			}
			set
			{
				NativeInputSystem.onDeviceDiscovered = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00047FAA File Offset: 0x000461AA
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00047FB2 File Offset: 0x000461B2
		public Action onShutdown
		{
			get
			{
				return this.m_ShutdownMethod;
			}
			set
			{
				if (value == null)
				{
					Application.quitting -= this.OnShutdown;
				}
				else if (this.m_ShutdownMethod == null)
				{
					Application.quitting += this.OnShutdown;
				}
				this.m_ShutdownMethod = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00047FEA File Offset: 0x000461EA
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00047FF2 File Offset: 0x000461F2
		public Action<bool> onPlayerFocusChanged
		{
			get
			{
				return this.m_FocusChangedMethod;
			}
			set
			{
				if (value == null)
				{
					Application.focusChanged -= this.OnFocusChanged;
				}
				else if (this.m_FocusChangedMethod == null)
				{
					Application.focusChanged += this.OnFocusChanged;
				}
				this.m_FocusChangedMethod = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0004802A File Offset: 0x0004622A
		public bool isPlayerFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00048031 File Offset: 0x00046231
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00048039 File Offset: 0x00046239
		public float pollingFrequency
		{
			get
			{
				return this.m_PollingFrequency;
			}
			set
			{
				this.m_PollingFrequency = value;
				NativeInputSystem.SetPollingFrequency(value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00048048 File Offset: 0x00046248
		public double currentTime
		{
			get
			{
				return NativeInputSystem.currentTime;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0004804F File Offset: 0x0004624F
		public double currentTimeForFixedUpdate
		{
			get
			{
				return (double)Time.fixedUnscaledTime + this.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0004805E File Offset: 0x0004625E
		public double currentTimeOffsetToRealtimeSinceStartup
		{
			get
			{
				return NativeInputSystem.currentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00048065 File Offset: 0x00046265
		public float unscaledGameTime
		{
			get
			{
				return Time.unscaledTime;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0004806C File Offset: 0x0004626C
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0004807D File Offset: 0x0004627D
		public bool runInBackground
		{
			get
			{
				return Application.runInBackground || this.m_RunInBackground;
			}
			set
			{
				this.m_RunInBackground = value;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00048086 File Offset: 0x00046286
		private void OnShutdown()
		{
			this.m_ShutdownMethod();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00048093 File Offset: 0x00046293
		private bool OnWantsToShutdown()
		{
			if (!this.m_DidCallOnShutdown)
			{
				this.OnShutdown();
				this.m_DidCallOnShutdown = true;
			}
			return true;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000480AB File Offset: 0x000462AB
		private void OnFocusChanged(bool focus)
		{
			this.m_FocusChangedMethod(focus);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x000480B9 File Offset: 0x000462B9
		public Vector2 screenSize
		{
			get
			{
				return new Vector2((float)Screen.width, (float)Screen.height);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000480CC File Offset: 0x000462CC
		public ScreenOrientation screenOrientation
		{
			get
			{
				return Screen.orientation;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x000480D3 File Offset: 0x000462D3
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x000480DA File Offset: 0x000462DA
		public bool normalizeScrollWheelDelta
		{
			get
			{
				return NativeInputSystem.normalizeScrollWheelDelta;
			}
			set
			{
				NativeInputSystem.normalizeScrollWheelDelta = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000480E2 File Offset: 0x000462E2
		public float scrollWheelDeltaPerTick
		{
			get
			{
				return NativeInputSystem.GetScrollWheelDeltaPerTick();
			}
		}

		// Token: 0x04000604 RID: 1540
		public static readonly NativeInputRuntime instance = new NativeInputRuntime();

		// Token: 0x04000605 RID: 1541
		private bool m_RunInBackground;

		// Token: 0x04000606 RID: 1542
		private Action m_ShutdownMethod;

		// Token: 0x04000607 RID: 1543
		private InputUpdateDelegate m_OnUpdate;

		// Token: 0x04000608 RID: 1544
		private Action<InputUpdateType> m_OnBeforeUpdate;

		// Token: 0x04000609 RID: 1545
		private Func<InputUpdateType, bool> m_OnShouldRunUpdate;

		// Token: 0x0400060A RID: 1546
		private float m_PollingFrequency = 60f;

		// Token: 0x0400060B RID: 1547
		private bool m_DidCallOnShutdown;

		// Token: 0x0400060C RID: 1548
		private Action<bool> m_FocusChangedMethod;
	}
}
