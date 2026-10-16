using System;
using Unity.Collections;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000097 RID: 151
	public abstract class OnScreenControl : MonoBehaviour
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0004039E File Offset: 0x0003E59E
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x000403A6 File Offset: 0x0003E5A6
		public string controlPath
		{
			get
			{
				return this.controlPathInternal;
			}
			set
			{
				this.controlPathInternal = value;
				if (base.isActiveAndEnabled)
				{
					this.SetupInputControl();
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x000403BD File Offset: 0x0003E5BD
		public InputControl control
		{
			get
			{
				return this.m_Control;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C07 RID: 3079
		// (set) Token: 0x06000C08 RID: 3080
		protected abstract string controlPathInternal { get; set; }

		// Token: 0x06000C09 RID: 3081 RVA: 0x000403C8 File Offset: 0x0003E5C8
		private void SetupInputControl()
		{
			string controlPathInternal = this.controlPathInternal;
			if (string.IsNullOrEmpty(controlPathInternal))
			{
				return;
			}
			string text = InputControlPath.TryGetDeviceLayout(controlPathInternal);
			if (text == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot determine device layout to use based on control path '",
					controlPathInternal,
					"' used in ",
					base.GetType().Name,
					" component"
				}), this);
				return;
			}
			InternedString internedString = new InternedString(text);
			int num = -1;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device.m_Layout == internedString)
				{
					num = i;
					break;
				}
			}
			InputDevice inputDevice;
			if (num == -1)
			{
				try
				{
					inputDevice = InputSystem.AddDevice(text, null, null);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Could not create device with layout '",
						text,
						"' used in '",
						base.GetType().Name,
						"' component"
					}));
					Debug.LogException(ex);
					return;
				}
				InputSystem.AddDeviceUsage(inputDevice, "OnScreen");
				InputEventPtr inputEventPtr;
				NativeArray<byte> nativeArray = StateEvent.From(inputDevice, out inputEventPtr, Allocator.Persistent);
				num = OnScreenControl.s_OnScreenDevices.Append(new OnScreenControl.OnScreenDeviceInfo
				{
					eventPtr = inputEventPtr,
					buffer = nativeArray,
					device = inputDevice
				});
			}
			else
			{
				inputDevice = OnScreenControl.s_OnScreenDevices[num].device;
			}
			this.m_Control = InputControlPath.TryFindControl(inputDevice, controlPathInternal, 0);
			if (this.m_Control == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot find control with path '",
					controlPathInternal,
					"' on device of type '",
					text,
					"' referenced by component '",
					base.GetType().Name,
					"'"
				}), this);
				if (OnScreenControl.s_OnScreenDevices[num].firstControl == null)
				{
					OnScreenControl.s_OnScreenDevices[num].Destroy();
					OnScreenControl.s_OnScreenDevices.RemoveAt(num);
				}
				return;
			}
			this.m_InputEventPtr = OnScreenControl.s_OnScreenDevices[num].eventPtr;
			OnScreenControl.s_OnScreenDevices[num] = OnScreenControl.s_OnScreenDevices[num].AddControl(this);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00040604 File Offset: 0x0003E804
		protected void SendValueToControl<TValue>(TValue value) where TValue : struct
		{
			if (this.m_Control == null)
			{
				return;
			}
			InputControl<TValue> inputControl = this.m_Control as InputControl<TValue>;
			if (inputControl == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The control path ",
					this.controlPath,
					" yields a control of type ",
					this.m_Control.GetType().Name,
					" which is not an InputControl with value type ",
					typeof(TValue).Name
				}), "value");
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			inputControl.WriteValueIntoEvent(value, this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000406B2 File Offset: 0x0003E8B2
		protected void SentDefaultValueToControl()
		{
			if (this.m_Control == null)
			{
				return;
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			this.m_Control.ResetToDefaultStateInEvent(this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000406EF File Offset: 0x0003E8EF
		internal static bool HasAnyActive
		{
			get
			{
				return OnScreenControl.s_nbActiveInstances != 0;
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000406FC File Offset: 0x0003E8FC
		protected virtual void OnEnable()
		{
			OnScreenControl.s_nbActiveInstances++;
			this.SetupInputControl();
			if (this.m_Control == null)
			{
				return;
			}
			if (OnScreenControl.s_nbActiveInstances == 1 && PlayerInput.isSinglePlayer)
			{
				PlayerInput playerByIndex = PlayerInput.GetPlayerByIndex(0);
				if (playerByIndex != null && !playerByIndex.neverAutoSwitchControlSchemes)
				{
					ReadOnlyArray<InputDevice> devices = playerByIndex.devices;
					bool flag = false;
					foreach (InputDevice inputDevice in devices)
					{
						if (this.m_Control.device.deviceId == inputDevice.deviceId)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						playerByIndex.SwitchCurrentControlScheme(new InputDevice[] { this.m_Control.device });
					}
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000407CC File Offset: 0x0003E9CC
		protected virtual void OnDisable()
		{
			OnScreenControl.s_nbActiveInstances--;
			if (this.m_Control == null)
			{
				return;
			}
			InputDevice device = this.m_Control.device;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device == device)
				{
					OnScreenControl.OnScreenDeviceInfo onScreenDeviceInfo = OnScreenControl.s_OnScreenDevices[i].RemoveControl(this);
					if (onScreenDeviceInfo.firstControl == null)
					{
						OnScreenControl.s_OnScreenDevices[i].Destroy();
						OnScreenControl.s_OnScreenDevices.RemoveAt(i);
					}
					else
					{
						OnScreenControl.s_OnScreenDevices[i] = onScreenDeviceInfo;
						if (!this.m_Control.CheckStateIsAtDefault())
						{
							this.SentDefaultValueToControl();
						}
					}
					this.m_Control = null;
					this.m_InputEventPtr = default(InputEventPtr);
					return;
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0004089E File Offset: 0x0003EA9E
		internal string GetWarningMessage()
		{
			return string.Format("{0} needs to be attached as a child to a UI Canvas and have a RectTransform component to function properly.", base.GetType());
		}

		// Token: 0x0400046A RID: 1130
		private InputControl m_Control;

		// Token: 0x0400046B RID: 1131
		private OnScreenControl m_NextControlOnDevice;

		// Token: 0x0400046C RID: 1132
		private InputEventPtr m_InputEventPtr;

		// Token: 0x0400046D RID: 1133
		private static int s_nbActiveInstances;

		// Token: 0x0400046E RID: 1134
		private static InlinedArray<OnScreenControl.OnScreenDeviceInfo> s_OnScreenDevices;

		// Token: 0x020001E4 RID: 484
		private struct OnScreenDeviceInfo
		{
			// Token: 0x060014C9 RID: 5321 RVA: 0x00060936 File Offset: 0x0005EB36
			public OnScreenControl.OnScreenDeviceInfo AddControl(OnScreenControl control)
			{
				control.m_NextControlOnDevice = this.firstControl;
				this.firstControl = control;
				return this;
			}

			// Token: 0x060014CA RID: 5322 RVA: 0x00060954 File Offset: 0x0005EB54
			public OnScreenControl.OnScreenDeviceInfo RemoveControl(OnScreenControl control)
			{
				if (this.firstControl == control)
				{
					this.firstControl = control.m_NextControlOnDevice;
				}
				else
				{
					OnScreenControl onScreenControl = this.firstControl.m_NextControlOnDevice;
					OnScreenControl onScreenControl2 = this.firstControl;
					while (onScreenControl != null)
					{
						if (!(onScreenControl != control))
						{
							onScreenControl2.m_NextControlOnDevice = onScreenControl.m_NextControlOnDevice;
							break;
						}
						onScreenControl2 = onScreenControl;
						onScreenControl = onScreenControl.m_NextControlOnDevice;
					}
				}
				control.m_NextControlOnDevice = null;
				return this;
			}

			// Token: 0x060014CB RID: 5323 RVA: 0x000609C8 File Offset: 0x0005EBC8
			public void Destroy()
			{
				if (this.buffer.IsCreated)
				{
					this.buffer.Dispose();
				}
				if (this.device != null)
				{
					InputSystem.RemoveDevice(this.device);
				}
				this.device = null;
				this.buffer = default(NativeArray<byte>);
			}

			// Token: 0x04000A1F RID: 2591
			public InputEventPtr eventPtr;

			// Token: 0x04000A20 RID: 2592
			public NativeArray<byte> buffer;

			// Token: 0x04000A21 RID: 2593
			public InputDevice device;

			// Token: 0x04000A22 RID: 2594
			public OnScreenControl firstControl;
		}
	}
}
