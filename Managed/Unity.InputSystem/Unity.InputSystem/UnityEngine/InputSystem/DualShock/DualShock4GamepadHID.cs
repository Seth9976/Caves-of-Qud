using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x020000A4 RID: 164
	[InputControlLayout(stateType = typeof(DualShock4HIDInputReport), hideInUI = true, isNoisy = true)]
	public class DualShock4GamepadHID : DualShockGamepad, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00043CEA File Offset: 0x00041EEA
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00043CF2 File Offset: 0x00041EF2
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00043CFB File Offset: 0x00041EFB
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00043D03 File Offset: 0x00041F03
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00043D0C File Offset: 0x00041F0C
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00043D14 File Offset: 0x00041F14
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00043D1D File Offset: 0x00041F1D
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00043D58 File Offset: 0x00041F58
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			dualShockHIDOutputReport.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00043DD4 File Offset: 0x00041FD4
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			dualShockHIDOutputReport.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.m_LightBarColor = null;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00043E74 File Offset: 0x00042074
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			if (this.m_LowFrequencyMotorSpeed != null || this.m_HighFrequenceyMotorSpeed != null)
			{
				dualShockHIDOutputReport.SetMotorSpeeds(this.m_LowFrequencyMotorSpeed.Value, this.m_HighFrequenceyMotorSpeed.Value);
			}
			if (this.m_LightBarColor != null)
			{
				dualShockHIDOutputReport.SetColor(this.m_LightBarColor.Value);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00043F1C File Offset: 0x0004211C
		public override void SetLightBarColor(Color color)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			dualShockHIDOutputReport.SetColor(color);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LightBarColor = new Color?(color);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00043F58 File Offset: 0x00042158
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			dualShockHIDOutputReport.SetMotorSpeeds(lowFrequency, highFrequency);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00043FA0 File Offset: 0x000421A0
		public bool SetMotorSpeedsAndLightBarColor(float lowFrequency, float highFrequency, Color color)
		{
			DualShockHIDOutputReport dualShockHIDOutputReport = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			dualShockHIDOutputReport.SetMotorSpeeds(lowFrequency, highFrequency);
			dualShockHIDOutputReport.SetColor(color);
			long num = base.ExecuteCommand<DualShockHIDOutputReport>(ref dualShockHIDOutputReport);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.m_LightBarColor = new Color?(color);
			return num >= 0L;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00044004 File Offset: 0x00042204
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == DualShock4HIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != DualShock4GamepadHID.DualShock4HIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport)))
			{
				return false;
			}
			byte* state = (byte*)ptr->state;
			byte b = *state;
			if (b != 1)
			{
				if (b - 17 > 8)
				{
					return false;
				}
				if ((state[1] & 128) == 0)
				{
					return false;
				}
				if ((ulong)stateSizeInBytes < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 3)))
				{
					return false;
				}
				DualShock4HIDInputReport dualShock4HIDInputReport = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 3))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport;
				ptr->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
			else
			{
				if ((ulong)stateSizeInBytes < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 1)))
				{
					return false;
				}
				DualShock4HIDInputReport dualShock4HIDInputReport2 = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 1))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport2;
				ptr->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0004410D File Offset: 0x0004230D
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00044110 File Offset: 0x00042310
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualShock4HIDInputReport.Format)
			{
				DualShock4HIDInputReport* ptr = (DualShock4HIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualShock4HIDInputReport* state = (DualShock4HIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2 && state->buttons3 == ptr->buttons3)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00044233 File Offset: 0x00042433
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x040004B2 RID: 1202
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x040004B3 RID: 1203
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x040004B4 RID: 1204
		private Color? m_LightBarColor;

		// Token: 0x040004B5 RID: 1205
		internal const byte JitterMaskLow = 120;

		// Token: 0x040004B6 RID: 1206
		internal const byte JitterMaskHigh = 135;

		// Token: 0x02000201 RID: 513
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualShock4HIDGenericInputReport
		{
			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x0600150D RID: 5389 RVA: 0x0006298A File Offset: 0x00060B8A
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x0006299C File Offset: 0x00060B9C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualShock4HIDInputReport ToHIDInputReport()
			{
				return new DualShock4HIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons1 = this.buttons0,
					buttons2 = this.buttons1,
					buttons3 = this.buttons2
				};
			}

			// Token: 0x04000B78 RID: 2936
			[FieldOffset(0)]
			public byte leftStickX;

			// Token: 0x04000B79 RID: 2937
			[FieldOffset(1)]
			public byte leftStickY;

			// Token: 0x04000B7A RID: 2938
			[FieldOffset(2)]
			public byte rightStickX;

			// Token: 0x04000B7B RID: 2939
			[FieldOffset(3)]
			public byte rightStickY;

			// Token: 0x04000B7C RID: 2940
			[FieldOffset(4)]
			public byte buttons0;

			// Token: 0x04000B7D RID: 2941
			[FieldOffset(5)]
			public byte buttons1;

			// Token: 0x04000B7E RID: 2942
			[FieldOffset(6)]
			public byte buttons2;

			// Token: 0x04000B7F RID: 2943
			[FieldOffset(7)]
			public byte leftTrigger;

			// Token: 0x04000B80 RID: 2944
			[FieldOffset(8)]
			public byte rightTrigger;
		}
	}
}
