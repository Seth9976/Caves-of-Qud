using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000038 RID: 56
	public class InputDevice : InputControl
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00013D86 File Offset: 0x00011F86
		public InputDeviceDescription description
		{
			get
			{
				return this.m_Description;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00013D8E File Offset: 0x00011F8E
		public bool enabled
		{
			get
			{
				return (this.m_DeviceFlags & (InputDevice.DeviceFlags.DisabledInFrontend | InputDevice.DeviceFlags.DisabledWhileInBackground)) == (InputDevice.DeviceFlags)0 && this.QueryEnabledStateFromRuntime();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00013DA6 File Offset: 0x00011FA6
		public bool canRunInBackground
		{
			get
			{
				return this.canDeviceRunInBackground;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00013DB0 File Offset: 0x00011FB0
		internal bool canDeviceRunInBackground
		{
			get
			{
				if ((this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried) != (InputDevice.DeviceFlags)0)
				{
					return (this.m_DeviceFlags & InputDevice.DeviceFlags.CanRunInBackground) > (InputDevice.DeviceFlags)0;
				}
				QueryCanRunInBackground queryCanRunInBackground = QueryCanRunInBackground.Create();
				this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
				if (this.ExecuteCommand<QueryCanRunInBackground>(ref queryCanRunInBackground) >= 0L && queryCanRunInBackground.canRunInBackground)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackground;
					return true;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.CanRunInBackground;
				return false;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00013E2E File Offset: 0x0001202E
		public bool added
		{
			get
			{
				return this.m_DeviceIndex != -1;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00013E3C File Offset: 0x0001203C
		public bool remote
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Remote) == InputDevice.DeviceFlags.Remote;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00013E49 File Offset: 0x00012049
		public bool native
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.Native) == InputDevice.DeviceFlags.Native;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00013E58 File Offset: 0x00012058
		public bool updateBeforeRender
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.UpdateBeforeRender) == InputDevice.DeviceFlags.UpdateBeforeRender;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00013E65 File Offset: 0x00012065
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00013E6D File Offset: 0x0001206D
		public double lastUpdateTime
		{
			get
			{
				return this.m_LastUpdateTimeInternal - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00013E7B File Offset: 0x0001207B
		public bool wasUpdatedThisFrame
		{
			get
			{
				return this.m_CurrentUpdateStepCount == InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00013E8A File Offset: 0x0001208A
		public ReadOnlyArray<InputControl> allControls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_ChildrenForEachControl);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00013E97 File Offset: 0x00012097
		public override Type valueType
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00013EA3 File Offset: 0x000120A3
		public override int valueSizeInBytes
		{
			get
			{
				return (int)this.m_StateBlock.alignedSizeInBytes;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00013EB0 File Offset: 0x000120B0
		[Obsolete("Use 'InputSystem.devices' instead. (UnityUpgradable) -> InputSystem.devices", false)]
		public static ReadOnlyArray<InputDevice> all
		{
			get
			{
				return InputSystem.devices;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013EB7 File Offset: 0x000120B7
		public InputDevice()
		{
			this.m_DeviceId = 0;
			this.m_ParticipantId = 0;
			this.m_DeviceIndex = -1;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013ED4 File Offset: 0x000120D4
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013EDC File Offset: 0x000120DC
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			if (this.m_DeviceIndex == -1)
			{
				return null;
			}
			uint alignedSizeInBytes = base.stateBlock.alignedSizeInBytes;
			byte[] array2;
			byte[] array = (array2 = new byte[alignedSizeInBytes]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte* ptr2 = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)ptr, (void*)ptr2, (long)((ulong)alignedSizeInBytes));
			array2 = null;
			return array;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013F40 File Offset: 0x00012140
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (bufferSize < this.valueSizeInBytes)
			{
				throw new ArgumentException(string.Format("Buffer too small (expected: {0}, actual: {1}", this.valueSizeInBytes, bufferSize));
			}
			byte* ptr = (byte*)statePtr + this.m_StateBlock.byteOffset;
			UnsafeUtility.MemCpy(bufferPtr, (void*)ptr, (long)((ulong)this.m_StateBlock.alignedSizeInBytes));
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00013FBC File Offset: 0x000121BC
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			if (firstStatePtr == null)
			{
				throw new ArgumentNullException("firstStatePtr");
			}
			if (secondStatePtr == null)
			{
				throw new ArgumentNullException("secondStatePtr");
			}
			void* ptr = (void*)((byte*)firstStatePtr + this.m_StateBlock.byteOffset);
			byte* ptr2 = (byte*)firstStatePtr + this.m_StateBlock.byteOffset;
			return UnsafeUtility.MemCmp(ptr, (void*)ptr2, (long)((ulong)this.m_StateBlock.alignedSizeInBytes)) == 0;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001401C File Offset: 0x0001221C
		internal void NotifyConfigurationChanged()
		{
			base.isConfigUpToDate = false;
			for (int i = 0; i < this.m_ChildrenForEachControl.Length; i++)
			{
				this.m_ChildrenForEachControl[i].isConfigUpToDate = false;
			}
			this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			this.OnConfigurationChanged();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00014066 File Offset: 0x00012266
		public virtual void MakeCurrent()
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00014068 File Offset: 0x00012268
		protected virtual void OnAdded()
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001406A File Offset: 0x0001226A
		protected virtual void OnRemoved()
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001406C File Offset: 0x0001226C
		protected virtual void OnConfigurationChanged()
		{
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00014070 File Offset: 0x00012270
		public unsafe long ExecuteCommand<TCommand>(ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			InputDeviceCommand* ptr = (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command);
			InputManager s_Manager = InputSystem.s_Manager;
			s_Manager.m_DeviceCommandCallbacks.LockForChanges();
			for (int i = 0; i < s_Manager.m_DeviceCommandCallbacks.length; i++)
			{
				try
				{
					long? num = s_Manager.m_DeviceCommandCallbacks[i](this, ptr);
					if (num != null)
					{
						return num.Value;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onDeviceCommand' callbacks");
					Debug.LogException(ex);
				}
			}
			s_Manager.m_DeviceCommandCallbacks.UnlockForChanges();
			return this.ExecuteCommand((InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00014124 File Offset: 0x00012324
		protected unsafe virtual long ExecuteCommand(InputDeviceCommand* commandPtr)
		{
			return InputRuntime.s_Instance.DeviceCommand(this.deviceId, commandPtr);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014138 File Offset: 0x00012338
		internal bool QueryEnabledStateFromRuntime()
		{
			if ((this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime) == (InputDevice.DeviceFlags)0)
			{
				QueryEnabledStateCommand queryEnabledStateCommand = QueryEnabledStateCommand.Create();
				if (this.ExecuteCommand<QueryEnabledStateCommand>(ref queryEnabledStateCommand) >= 0L)
				{
					if (queryEnabledStateCommand.isEnabled)
					{
						this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
					}
					else
					{
						this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					}
				}
				else
				{
					this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
				}
				this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
			}
			return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) == (InputDevice.DeviceFlags)0;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000141C2 File Offset: 0x000123C2
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x000141D0 File Offset: 0x000123D0
		internal bool disabledInFrontend
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInFrontend) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInFrontend;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInFrontend;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000141F4 File Offset: 0x000123F4
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00014205 File Offset: 0x00012405
		internal bool disabledInRuntime
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledInRuntime) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInRuntime;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInRuntime;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001422F File Offset: 0x0001242F
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00014240 File Offset: 0x00012440
		internal bool disabledWhileInBackground
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.DisabledWhileInBackground) > (InputDevice.DeviceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledWhileInBackground;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledWhileInBackground;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001426A File Offset: 0x0001246A
		internal static uint EncodeStateOffsetToControlMapEntry(uint controlIndex, uint stateOffsetInBits, uint stateSizeInBits)
		{
			return (stateOffsetInBits << 19) | (stateSizeInBits << 10) | controlIndex;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00014277 File Offset: 0x00012477
		internal static void DecodeStateOffsetToControlMapEntry(uint entry, out uint controlIndex, out uint stateOffset, out uint stateSize)
		{
			controlIndex = entry & 1023U;
			stateOffset = entry >> 19;
			stateSize = (entry >> 10) & 511U;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00014294 File Offset: 0x00012494
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000142A1 File Offset: 0x000124A1
		internal bool hasControlsWithDefaultState
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasControlsWithDefaultState) == InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasControlsWithDefaultState;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasControlsWithDefaultState;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000142C4 File Offset: 0x000124C4
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000142D9 File Offset: 0x000124D9
		internal bool hasDontResetControls
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasDontResetControls) == InputDevice.DeviceFlags.HasDontResetControls;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasDontResetControls;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasDontResetControls;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00014303 File Offset: 0x00012503
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00014310 File Offset: 0x00012510
		internal bool hasStateCallbacks
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasStateCallbacks) == InputDevice.DeviceFlags.HasStateCallbacks;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasStateCallbacks;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasStateCallbacks;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00014333 File Offset: 0x00012533
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00014348 File Offset: 0x00012548
		internal bool hasEventMerger
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventMerger) == InputDevice.DeviceFlags.HasEventMerger;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventMerger;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventMerger;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00014372 File Offset: 0x00012572
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00014387 File Offset: 0x00012587
		internal bool hasEventPreProcessor
		{
			get
			{
				return (this.m_DeviceFlags & InputDevice.DeviceFlags.HasEventPreProcessor) == InputDevice.DeviceFlags.HasEventPreProcessor;
			}
			set
			{
				if (value)
				{
					this.m_DeviceFlags |= InputDevice.DeviceFlags.HasEventPreProcessor;
					return;
				}
				this.m_DeviceFlags &= ~InputDevice.DeviceFlags.HasEventPreProcessor;
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000143B4 File Offset: 0x000125B4
		internal void AddDeviceUsage(InternedString usage)
		{
			int num = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = num;
			}
			ArrayHelpers.AppendWithCapacity<InternedString>(ref this.m_UsagesForEachControl, ref num, usage, 10);
			this.m_UsageCount++;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00014404 File Offset: 0x00012604
		internal void RemoveDeviceUsage(InternedString usage)
		{
			int num = this.m_UsageToControl.LengthSafe<InputControl>() + this.m_UsageCount;
			int num2 = this.m_UsagesForEachControl.IndexOfValue(usage, this.m_UsageStartIndex, num);
			if (num2 == -1)
			{
				return;
			}
			this.m_UsagesForEachControl.EraseAtWithCapacity<InternedString>(ref num, num2);
			this.m_UsageCount--;
			if (this.m_UsageCount == 0)
			{
				this.m_UsageStartIndex = 0;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00014468 File Offset: 0x00012668
		internal void ClearDeviceUsages()
		{
			for (int i = this.m_UsageStartIndex; i < this.m_UsageCount; i++)
			{
				this.m_UsagesForEachControl[i] = default(InternedString);
			}
			this.m_UsageCount = 0;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000144A4 File Offset: 0x000126A4
		internal bool RequestSync()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestSyncCommand requestSyncCommand = RequestSyncCommand.Create();
			return base.device.ExecuteCommand<RequestSyncCommand>(ref requestSyncCommand) >= 0L;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000144D4 File Offset: 0x000126D4
		internal bool RequestReset()
		{
			base.SetOptimizedControlDataTypeRecursively();
			RequestResetCommand requestResetCommand = RequestResetCommand.Create();
			return base.device.ExecuteCommand<RequestResetCommand>(ref requestResetCommand) >= 0L;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00014504 File Offset: 0x00012704
		internal bool ExecuteEnableCommand()
		{
			base.SetOptimizedControlDataTypeRecursively();
			EnableDeviceCommand enableDeviceCommand = EnableDeviceCommand.Create();
			return base.device.ExecuteCommand<EnableDeviceCommand>(ref enableDeviceCommand) >= 0L;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00014534 File Offset: 0x00012734
		internal bool ExecuteDisableCommand()
		{
			DisableDeviceCommand disableDeviceCommand = DisableDeviceCommand.Create();
			return base.device.ExecuteCommand<DisableDeviceCommand>(ref disableDeviceCommand) >= 0L;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001455B File Offset: 0x0001275B
		internal void NotifyAdded()
		{
			this.OnAdded();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00014563 File Offset: 0x00012763
		internal void NotifyRemoved()
		{
			this.OnRemoved();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001456C File Offset: 0x0001276C
		internal static TDevice Build<TDevice>(string layoutName = null, string layoutVariants = null, InputDeviceDescription deviceDescription = default(InputDeviceDescription), bool noPrecompiledLayouts = false) where TDevice : InputDevice
		{
			InternedString internedString = new InternedString(layoutName);
			if (internedString.IsEmpty())
			{
				internedString = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice));
				if (internedString.IsEmpty())
				{
					internedString = new InternedString(typeof(TDevice).Name);
				}
			}
			InputControlLayout.Collection.PrecompiledLayout precompiledLayout;
			if (!noPrecompiledLayouts && string.IsNullOrEmpty(layoutVariants) && InputControlLayout.s_Layouts.precompiledLayouts.TryGetValue(internedString, out precompiledLayout))
			{
				return (TDevice)((object)precompiledLayout.factoryMethod());
			}
			TDevice tdevice2;
			using (InputDeviceBuilder.Ref())
			{
				InputDeviceBuilder.instance.Setup(internedString, new InternedString(layoutVariants), deviceDescription);
				InputDevice inputDevice = InputDeviceBuilder.instance.Finish();
				TDevice tdevice = inputDevice as TDevice;
				if (tdevice == null)
				{
					throw new ArgumentException(string.Concat(new string[]
					{
						"Expected device of type '",
						typeof(TDevice).Name,
						"' but got device of type '",
						inputDevice.GetType().Name,
						"' instead"
					}), "TDevice");
				}
				tdevice2 = tdevice;
			}
			return tdevice2;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001469C File Offset: 0x0001289C
		internal unsafe void WriteChangedControlStates(byte* deviceStateBuffer, void* statePtr, uint stateSizeInBytes, uint stateOffsetInDevice)
		{
			if (this.m_ControlTreeNodes.Length == 0)
			{
				return;
			}
			this.m_UpdatedButtons.Clear();
			if (this.m_StateBlock.sizeInBits != stateSizeInBytes * 8U)
			{
				if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBytes * 8U, stateOffsetInDevice * 8U, this.m_ControlTreeNodes[0], 0U);
					return;
				}
			}
			else if (this.m_ControlTreeNodes[0].leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, deviceStateBuffer, this.m_ControlTreeNodes[0], 0U);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014728 File Offset: 0x00012928
		private void WritePartialChangedControlStatesInternal(uint stateSizeInBits, uint stateOffsetInDeviceInBits, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (Math.Max(stateOffsetInDeviceInBits, startOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)controlBitRangeNode.endBitOffset))
			{
				int num = (int)(controlBitRangeNode.controlStartIndex + (ushort)controlBitRangeNode.controlCount);
				for (int i = (int)controlBitRangeNode.controlStartIndex; i < num; i++)
				{
					ushort num2 = this.m_ControlTreeIndices[i];
					InputControl inputControl = this.m_ChildrenForEachControl[(int)num2];
					inputControl.MarkAsStale();
					if (inputControl.isButton && ((ButtonControl)inputControl).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)num2);
					}
				}
				if (controlBitRangeNode.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, controlBitRangeNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (Math.Max(stateOffsetInDeviceInBits, (uint)controlBitRangeNode.endBitOffset) <= Math.Min(stateOffsetInDeviceInBits + stateSizeInBits, (uint)controlBitRangeNode2.endBitOffset))
			{
				int num3 = (int)(controlBitRangeNode2.controlStartIndex + (ushort)controlBitRangeNode2.controlCount);
				for (int j = (int)controlBitRangeNode2.controlStartIndex; j < num3; j++)
				{
					ushort num4 = this.m_ControlTreeIndices[j];
					InputControl inputControl2 = this.m_ChildrenForEachControl[(int)num4];
					inputControl2.MarkAsStale();
					if (inputControl2.isButton && ((ButtonControl)inputControl2).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)num4);
					}
				}
				if (controlBitRangeNode2.leftChildIndex != -1)
				{
					this.WritePartialChangedControlStatesInternal(stateSizeInBits, stateOffsetInDeviceInBits, controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset);
				}
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00014884 File Offset: 0x00012A84
		private void DumpControlBitRangeNode(int nodeIndex, InputDevice.ControlBitRangeNode node, uint startOffset, uint sizeInBits, List<string> output)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < (int)node.controlCount; i++)
			{
				ushort num = this.m_ControlTreeIndices[(int)node.controlStartIndex + i];
				InputControl inputControl = this.m_ChildrenForEachControl[(int)num];
				list.Add(inputControl.path);
			}
			string text = string.Join(", ", list);
			string text2 = ((node.leftChildIndex != -1) ? string.Format(" <{0}, {1}>", node.leftChildIndex, (int)(node.leftChildIndex + 1)) : "");
			output.Add(string.Format("{0} [{1}, {2}]{3}->{4}", new object[]
			{
				nodeIndex,
				startOffset,
				startOffset + sizeInBits,
				text2,
				text
			}));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014950 File Offset: 0x00012B50
		private void DumpControlTree(InputDevice.ControlBitRangeNode parentNode, uint startOffset, List<string> output)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			this.DumpControlBitRangeNode((int)parentNode.leftChildIndex, controlBitRangeNode, startOffset, (uint)controlBitRangeNode.endBitOffset - startOffset, output);
			this.DumpControlBitRangeNode((int)(parentNode.leftChildIndex + 1), controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset, (uint)(controlBitRangeNode2.endBitOffset - controlBitRangeNode.endBitOffset), output);
			if (controlBitRangeNode.leftChildIndex != -1)
			{
				this.DumpControlTree(controlBitRangeNode, startOffset, output);
			}
			if (controlBitRangeNode2.leftChildIndex != -1)
			{
				this.DumpControlTree(controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset, output);
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000149E8 File Offset: 0x00012BE8
		internal string DumpControlTree()
		{
			List<string> list = new List<string>();
			this.DumpControlTree(this.m_ControlTreeNodes[0], 0U, list);
			return string.Join("\n", list);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00014A1C File Offset: 0x00012C1C
		private unsafe void WriteChangedControlStatesInternal(void* statePtr, byte* deviceStatePtr, InputDevice.ControlBitRangeNode parentNode, uint startOffset)
		{
			InputDevice.ControlBitRangeNode controlBitRangeNode = this.m_ControlTreeNodes[(int)parentNode.leftChildIndex];
			if (InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, startOffset, (uint)controlBitRangeNode.endBitOffset - startOffset + 1U))
			{
				int num = (int)(controlBitRangeNode.controlStartIndex + (ushort)controlBitRangeNode.controlCount);
				for (int i = (int)controlBitRangeNode.controlStartIndex; i < num; i++)
				{
					ushort num2 = this.m_ControlTreeIndices[i];
					InputControl inputControl = this.m_ChildrenForEachControl[(int)num2];
					if (!inputControl.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
					{
						inputControl.MarkAsStale();
						if (inputControl.isButton && ((ButtonControl)inputControl).needsToCheckFramePress)
						{
							this.m_UpdatedButtons.Add((int)num2);
						}
					}
				}
				if (controlBitRangeNode.leftChildIndex != -1)
				{
					this.WriteChangedControlStatesInternal(statePtr, deviceStatePtr, controlBitRangeNode, startOffset);
				}
			}
			InputDevice.ControlBitRangeNode controlBitRangeNode2 = this.m_ControlTreeNodes[(int)(parentNode.leftChildIndex + 1)];
			if (!InputDevice.HasDataChangedInRange(deviceStatePtr, statePtr, (uint)controlBitRangeNode.endBitOffset, (uint)(controlBitRangeNode2.endBitOffset - controlBitRangeNode.endBitOffset + 1)))
			{
				return;
			}
			int num3 = (int)(controlBitRangeNode2.controlStartIndex + (ushort)controlBitRangeNode2.controlCount);
			for (int j = (int)controlBitRangeNode2.controlStartIndex; j < num3; j++)
			{
				ushort num4 = this.m_ControlTreeIndices[j];
				InputControl inputControl2 = this.m_ChildrenForEachControl[(int)num4];
				if (!inputControl2.CompareState((void*)(deviceStatePtr - this.m_StateBlock.byteOffset), (void*)((byte*)statePtr - this.m_StateBlock.byteOffset), null))
				{
					inputControl2.MarkAsStale();
					if (inputControl2.isButton && ((ButtonControl)inputControl2).needsToCheckFramePress)
					{
						this.m_UpdatedButtons.Add((int)num4);
					}
				}
			}
			if (controlBitRangeNode2.leftChildIndex != -1)
			{
				this.WriteChangedControlStatesInternal(statePtr, deviceStatePtr, controlBitRangeNode2, (uint)controlBitRangeNode.endBitOffset);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00014BCB File Offset: 0x00012DCB
		private unsafe static bool HasDataChangedInRange(byte* deviceStatePtr, void* statePtr, uint startOffset, uint sizeInBits)
		{
			if (sizeInBits == 1U)
			{
				return MemoryHelpers.ReadSingleBit((void*)deviceStatePtr, startOffset) != MemoryHelpers.ReadSingleBit(statePtr, startOffset);
			}
			return !MemoryHelpers.MemCmpBitRegion((void*)deviceStatePtr, statePtr, startOffset, sizeInBits, null);
		}

		// Token: 0x04000161 RID: 353
		public const int InvalidDeviceId = 0;

		// Token: 0x04000162 RID: 354
		internal const int kLocalParticipantId = 0;

		// Token: 0x04000163 RID: 355
		internal const int kInvalidDeviceIndex = -1;

		// Token: 0x04000164 RID: 356
		internal InputDevice.DeviceFlags m_DeviceFlags;

		// Token: 0x04000165 RID: 357
		internal int m_DeviceId;

		// Token: 0x04000166 RID: 358
		internal int m_ParticipantId;

		// Token: 0x04000167 RID: 359
		internal int m_DeviceIndex;

		// Token: 0x04000168 RID: 360
		internal uint m_CurrentProcessedEventBytesOnUpdate;

		// Token: 0x04000169 RID: 361
		internal InputDeviceDescription m_Description;

		// Token: 0x0400016A RID: 362
		internal double m_LastUpdateTimeInternal;

		// Token: 0x0400016B RID: 363
		internal uint m_CurrentUpdateStepCount;

		// Token: 0x0400016C RID: 364
		internal InternedString[] m_AliasesForEachControl;

		// Token: 0x0400016D RID: 365
		internal InternedString[] m_UsagesForEachControl;

		// Token: 0x0400016E RID: 366
		internal InputControl[] m_UsageToControl;

		// Token: 0x0400016F RID: 367
		internal InputControl[] m_ChildrenForEachControl;

		// Token: 0x04000170 RID: 368
		internal HashSet<int> m_UpdatedButtons;

		// Token: 0x04000171 RID: 369
		internal List<ButtonControl> m_ButtonControlsCheckingPressState;

		// Token: 0x04000172 RID: 370
		internal bool m_UseCachePathForButtonPresses;

		// Token: 0x04000173 RID: 371
		internal uint[] m_StateOffsetToControlMap;

		// Token: 0x04000174 RID: 372
		internal InputDevice.ControlBitRangeNode[] m_ControlTreeNodes;

		// Token: 0x04000175 RID: 373
		internal ushort[] m_ControlTreeIndices;

		// Token: 0x04000176 RID: 374
		internal const int kControlIndexBits = 10;

		// Token: 0x04000177 RID: 375
		internal const int kStateOffsetBits = 13;

		// Token: 0x04000178 RID: 376
		internal const int kStateSizeBits = 9;

		// Token: 0x0200019C RID: 412
		[Flags]
		[Serializable]
		internal enum DeviceFlags
		{
			// Token: 0x040008FE RID: 2302
			UpdateBeforeRender = 1,
			// Token: 0x040008FF RID: 2303
			HasStateCallbacks = 2,
			// Token: 0x04000900 RID: 2304
			HasControlsWithDefaultState = 4,
			// Token: 0x04000901 RID: 2305
			HasDontResetControls = 1024,
			// Token: 0x04000902 RID: 2306
			HasEventMerger = 8192,
			// Token: 0x04000903 RID: 2307
			HasEventPreProcessor = 16384,
			// Token: 0x04000904 RID: 2308
			Remote = 8,
			// Token: 0x04000905 RID: 2309
			Native = 16,
			// Token: 0x04000906 RID: 2310
			DisabledInFrontend = 32,
			// Token: 0x04000907 RID: 2311
			DisabledInRuntime = 128,
			// Token: 0x04000908 RID: 2312
			DisabledWhileInBackground = 256,
			// Token: 0x04000909 RID: 2313
			DisabledStateHasBeenQueriedFromRuntime = 64,
			// Token: 0x0400090A RID: 2314
			CanRunInBackground = 2048,
			// Token: 0x0400090B RID: 2315
			CanRunInBackgroundHasBeenQueried = 4096
		}

		// Token: 0x0200019D RID: 413
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct ControlBitRangeNode
		{
			// Token: 0x0600143F RID: 5183 RVA: 0x0005E28B File Offset: 0x0005C48B
			public ControlBitRangeNode(ushort endOffset)
			{
				this.controlStartIndex = 0;
				this.controlCount = 0;
				this.endBitOffset = endOffset;
				this.leftChildIndex = -1;
			}

			// Token: 0x0400090C RID: 2316
			public ushort endBitOffset;

			// Token: 0x0400090D RID: 2317
			public short leftChildIndex;

			// Token: 0x0400090E RID: 2318
			public ushort controlStartIndex;

			// Token: 0x0400090F RID: 2319
			public byte controlCount;
		}
	}
}
