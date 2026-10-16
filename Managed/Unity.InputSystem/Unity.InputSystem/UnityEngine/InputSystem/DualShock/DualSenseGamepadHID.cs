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
	// Token: 0x020000A3 RID: 163
	[InputControlLayout(stateType = typeof(DualSenseHIDInputReport), displayName = "DualSense HID")]
	public class DualSenseGamepadHID : DualShockGamepad, IEventMerger, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00043654 File Offset: 0x00041854
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0004365C File Offset: 0x0004185C
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00043665 File Offset: 0x00041865
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0004366D File Offset: 0x0004186D
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00043676 File Offset: 0x00041876
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0004367E File Offset: 0x0004187E
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000CBA RID: 3258 RVA: 0x00043687 File Offset: 0x00041887
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000436C2 File Offset: 0x000418C2
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(new float?(0f), new float?(0f), this.m_LightBarColor);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00043700 File Offset: 0x00041900
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00043759 File Offset: 0x00041959
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0004378F File Offset: 0x0004198F
		public override void SetLightBarColor(Color color)
		{
			this.m_LightBarColor = new Color?(color);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000437B6 File Offset: 0x000419B6
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000437EC File Offset: 0x000419EC
		public bool SetMotorSpeedsAndLightBarColor(float? lowFrequency, float? highFrequency, Color? color)
		{
			float num = ((lowFrequency != null) ? lowFrequency.Value : 0f);
			float num2 = ((highFrequency != null) ? highFrequency.Value : 0f);
			Color color2 = ((color != null) ? color.Value : Color.black);
			DualSenseHIDUSBOutputReport dualSenseHIDUSBOutputReport = DualSenseHIDUSBOutputReport.Create(new DualSenseHIDOutputReportPayload
			{
				enableFlags1 = 3,
				enableFlags2 = 4,
				lowFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(num, 0U, 255U),
				highFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(num2, 0U, 255U),
				redColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.r, 0U, 255U),
				greenColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.g, 0U, 255U),
				blueColor = (byte)NumberHelpers.NormalizedFloatToUInt(color2.b, 0U, 255U)
			}, base.hidDescriptor.outputReportSize);
			return base.ExecuteCommand<DualSenseHIDUSBOutputReport>(ref dualSenseHIDUSBOutputReport) >= 0L;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000438EE File Offset: 0x00041AEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState, DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0004391C File Offset: 0x00041B1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState, DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0004394A File Offset: 0x00041B4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState, DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00043978 File Offset: 0x00041B78
		unsafe bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* ptr2 = StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || ptr2->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format)
			{
				return false;
			}
			if (ptr->stateSizeInBytes != ptr2->stateSizeInBytes)
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state2 = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr2->state;
			if (state->reportId != state2->reportId)
			{
				return false;
			}
			if (state->reportId == 1)
			{
				if ((ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state3 = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state;
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state4 = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr2->state;
					return DualSenseGamepadHID.MergeForward(state3, state4);
				}
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* state5 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state;
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* state6 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr2->state;
				return DualSenseGamepadHID.MergeForward(state5, state6);
			}
			else
			{
				if (state->reportId == 49)
				{
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state7 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state;
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state8 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr2->state;
					return DualSenseGamepadHID.MergeForward(state7, state8);
				}
				return false;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00043A9C File Offset: 0x00041C9C
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == DualSenseHIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(DualSenseHIDInputReport)))
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			if (state->reportId == 1)
			{
				if ((ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)ptr->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseHIDInputReport dualSenseHIDInputReport = ((DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport;
				}
				else
				{
					DualSenseHIDInputReport dualSenseHIDInputReport2 = ((DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport2;
				}
				ptr->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			if (state->reportId == 49)
			{
				DualSenseHIDInputReport dualSenseHIDInputReport3 = ((DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state)->ToHIDInputReport();
				*(DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport3;
				ptr->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			return false;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00043BBA File Offset: 0x00041DBA
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00043BBC File Offset: 0x00041DBC
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualSenseHIDInputReport.Format)
			{
				DualSenseHIDInputReport* ptr = (DualSenseHIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualSenseHIDInputReport* state = (DualSenseHIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons0 == ptr->buttons0 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00043CDF File Offset: 0x00041EDF
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x040004A9 RID: 1193
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x040004AA RID: 1194
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x040004AB RID: 1195
		protected Color? m_LightBarColor;

		// Token: 0x040004AC RID: 1196
		private byte outputSequenceId;

		// Token: 0x040004AD RID: 1197
		internal const byte JitterMaskLow = 120;

		// Token: 0x040004AE RID: 1198
		internal const byte JitterMaskHigh = 135;

		// Token: 0x020001FD RID: 509
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDGenericInputReport
		{
			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x06001508 RID: 5384 RVA: 0x000627BC File Offset: 0x000609BC
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x04000B55 RID: 2901
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x020001FE RID: 510
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDUSBInputReport
		{
			// Token: 0x06001509 RID: 5385 RVA: 0x000627CC File Offset: 0x000609CC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x04000B56 RID: 2902
			public const int ExpectedReportId = 1;

			// Token: 0x04000B57 RID: 2903
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000B58 RID: 2904
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x04000B59 RID: 2905
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x04000B5A RID: 2906
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x04000B5B RID: 2907
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x04000B5C RID: 2908
			[FieldOffset(5)]
			public byte leftTrigger;

			// Token: 0x04000B5D RID: 2909
			[FieldOffset(6)]
			public byte rightTrigger;

			// Token: 0x04000B5E RID: 2910
			[FieldOffset(8)]
			public byte buttons0;

			// Token: 0x04000B5F RID: 2911
			[FieldOffset(9)]
			public byte buttons1;

			// Token: 0x04000B60 RID: 2912
			[FieldOffset(10)]
			public byte buttons2;
		}

		// Token: 0x020001FF RID: 511
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDBluetoothInputReport
		{
			// Token: 0x0600150A RID: 5386 RVA: 0x0006285C File Offset: 0x00060A5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x04000B61 RID: 2913
			public const int ExpectedReportId = 49;

			// Token: 0x04000B62 RID: 2914
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000B63 RID: 2915
			[FieldOffset(2)]
			public byte leftStickX;

			// Token: 0x04000B64 RID: 2916
			[FieldOffset(3)]
			public byte leftStickY;

			// Token: 0x04000B65 RID: 2917
			[FieldOffset(4)]
			public byte rightStickX;

			// Token: 0x04000B66 RID: 2918
			[FieldOffset(5)]
			public byte rightStickY;

			// Token: 0x04000B67 RID: 2919
			[FieldOffset(6)]
			public byte leftTrigger;

			// Token: 0x04000B68 RID: 2920
			[FieldOffset(7)]
			public byte rightTrigger;

			// Token: 0x04000B69 RID: 2921
			[FieldOffset(9)]
			public byte buttons0;

			// Token: 0x04000B6A RID: 2922
			[FieldOffset(10)]
			public byte buttons1;

			// Token: 0x04000B6B RID: 2923
			[FieldOffset(11)]
			public byte buttons2;
		}

		// Token: 0x02000200 RID: 512
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDMinimalInputReport
		{
			// Token: 0x0600150B RID: 5387 RVA: 0x000628EC File Offset: 0x00060AEC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 3)
				};
			}

			// Token: 0x04000B6C RID: 2924
			public static int ExpectedSize1 = 10;

			// Token: 0x04000B6D RID: 2925
			public static int ExpectedSize2 = 78;

			// Token: 0x04000B6E RID: 2926
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x04000B6F RID: 2927
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x04000B70 RID: 2928
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x04000B71 RID: 2929
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x04000B72 RID: 2930
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x04000B73 RID: 2931
			[FieldOffset(5)]
			public byte buttons0;

			// Token: 0x04000B74 RID: 2932
			[FieldOffset(6)]
			public byte buttons1;

			// Token: 0x04000B75 RID: 2933
			[FieldOffset(7)]
			public byte buttons2;

			// Token: 0x04000B76 RID: 2934
			[FieldOffset(8)]
			public byte leftTrigger;

			// Token: 0x04000B77 RID: 2935
			[FieldOffset(9)]
			public byte rightTrigger;
		}
	}
}
