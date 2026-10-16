using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch
{
	// Token: 0x02000093 RID: 147
	[InputControlLayout(stateType = typeof(SwitchProControllerHIDInputState), displayName = "Switch Pro Controller")]
	public class SwitchProControllerHID : Gamepad, IInputStateCallbackReceiver, IEventPreProcessor
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0003FB11 File Offset: 0x0003DD11
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0003FB19 File Offset: 0x0003DD19
		[InputControl(name = "capture", displayName = "Capture")]
		public ButtonControl captureButton { get; protected set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0003FB22 File Offset: 0x0003DD22
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0003FB2A File Offset: 0x0003DD2A
		[InputControl(name = "home", displayName = "Home")]
		public ButtonControl homeButton { get; protected set; }

		// Token: 0x06000BEF RID: 3055 RVA: 0x0003FB33 File Offset: 0x0003DD33
		protected override void OnAdded()
		{
			base.OnAdded();
			this.captureButton = base.GetChildControl<ButtonControl>("capture");
			this.homeButton = base.GetChildControl<ButtonControl>("home");
			this.HandshakeRestart();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003FB63 File Offset: 0x0003DD63
		private void HandshakeRestart()
		{
			this.m_HandshakeStepIndex = -1;
			this.m_HandshakeTimer = InputRuntime.s_Instance.currentTime;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003FB7C File Offset: 0x0003DD7C
		private void HandshakeTick()
		{
			double currentTime = InputRuntime.s_Instance.currentTime;
			if (currentTime >= this.m_LastUpdateTimeInternal + 2.0 && currentTime >= this.m_HandshakeTimer + 2.0)
			{
				this.m_HandshakeStepIndex = 0;
			}
			else
			{
				if (this.m_HandshakeStepIndex + 1 >= SwitchProControllerHID.s_HandshakeSequence.Length)
				{
					return;
				}
				if (currentTime <= this.m_HandshakeTimer + 0.1)
				{
					return;
				}
				this.m_HandshakeStepIndex++;
			}
			this.m_HandshakeTimer = currentTime;
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType commandIdType = SwitchProControllerHID.s_HandshakeSequence[this.m_HandshakeStepIndex];
			SwitchProControllerHID.SwitchMagicOutputHIDBluetooth switchMagicOutputHIDBluetooth = SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Create(commandIdType);
			if (base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDBluetooth>(ref switchMagicOutputHIDBluetooth) > 0L)
			{
				return;
			}
			SwitchProControllerHID.SwitchMagicOutputHIDUSB switchMagicOutputHIDUSB = SwitchProControllerHID.SwitchMagicOutputHIDUSB.Create(commandIdType);
			base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDUSB>(ref switchMagicOutputHIDUSB);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003FC31 File Offset: 0x0003DE31
		public void OnNextUpdate()
		{
			this.HandshakeTick();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003FC3C File Offset: 0x0003DE3C
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == SwitchProControllerHIDInputState.Format)
			{
				SwitchProControllerHIDInputState* ptr = (SwitchProControllerHIDInputState*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				SwitchProControllerHIDInputState* state = (SwitchProControllerHIDInputState*)StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003FD30 File Offset: 0x0003DF30
		public unsafe bool PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat == SwitchProControllerHIDInputState.Format;
			}
			if (eventPtr.type != 1398030676)
			{
				return true;
			}
			StateEvent* ptr = StateEvent.FromUnchecked(eventPtr);
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat == SwitchProControllerHIDInputState.Format)
			{
				return true;
			}
			if (ptr->stateFormat != SwitchProControllerHID.SwitchHIDGenericInputReport.Format || (ulong)stateSizeInBytes < (ulong)((long)sizeof(SwitchProControllerHID.SwitchHIDGenericInputReport)))
			{
				return false;
			}
			SwitchProControllerHID.SwitchHIDGenericInputReport* state = (SwitchProControllerHID.SwitchHIDGenericInputReport*)ptr->state;
			if (state->reportId == 63 && stateSizeInBytes >= 12U)
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState = ((SwitchProControllerHID.SwitchSimpleInputReport*)ptr->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (state->reportId == 48 && stateSizeInBytes >= 25U)
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState2 = ((SwitchProControllerHID.SwitchFullInputReport*)ptr->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState2;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (stateSizeInBytes == 8U || stateSizeInBytes == 9U)
			{
				int num = ((stateSizeInBytes == 9U) ? 1 : 0);
				SwitchProControllerHIDInputState switchProControllerHIDInputState3 = ((SwitchProControllerHID.SwitchInputOnlyReport*)((byte*)ptr->state + num))->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState3;
				ptr->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			return false;
		}

		// Token: 0x0400045D RID: 1117
		private static readonly SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[] s_HandshakeSequence = new SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[]
		{
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Status,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Highspeed,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.ForceUSB
		};

		// Token: 0x0400045E RID: 1118
		private int m_HandshakeStepIndex;

		// Token: 0x0400045F RID: 1119
		private double m_HandshakeTimer;

		// Token: 0x04000460 RID: 1120
		internal const byte JitterMaskLow = 120;

		// Token: 0x04000461 RID: 1121
		internal const byte JitterMaskHigh = 135;

		// Token: 0x020001DC RID: 476
		[StructLayout(LayoutKind.Explicit, Size = 7)]
		private struct SwitchInputOnlyReport
		{
			// Token: 0x060014BF RID: 5311 RVA: 0x00060200 File Offset: 0x0005E400
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = this.leftX,
					leftStickY = this.leftY,
					rightStickX = this.rightX,
					rightStickY = this.rightY
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				switch (this.hat)
				{
				case 0:
					flag2 = true;
					break;
				case 1:
					flag2 = true;
					flag3 = true;
					break;
				case 2:
					flag3 = true;
					break;
				case 3:
					flag4 = true;
					flag3 = true;
					break;
				case 4:
					flag4 = true;
					break;
				case 5:
					flag4 = true;
					flag = true;
					break;
				case 6:
					flag = true;
					break;
				case 7:
					flag2 = true;
					flag = true;
					break;
				}
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, flag);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, flag2);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, flag3);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, flag4);
				return switchProControllerHIDInputState;
			}

			// Token: 0x040009DF RID: 2527
			public const int kSize = 7;

			// Token: 0x040009E0 RID: 2528
			[FieldOffset(0)]
			public byte buttons0;

			// Token: 0x040009E1 RID: 2529
			[FieldOffset(1)]
			public byte buttons1;

			// Token: 0x040009E2 RID: 2530
			[FieldOffset(2)]
			public byte hat;

			// Token: 0x040009E3 RID: 2531
			[FieldOffset(3)]
			public byte leftX;

			// Token: 0x040009E4 RID: 2532
			[FieldOffset(4)]
			public byte leftY;

			// Token: 0x040009E5 RID: 2533
			[FieldOffset(5)]
			public byte rightX;

			// Token: 0x040009E6 RID: 2534
			[FieldOffset(6)]
			public byte rightY;
		}

		// Token: 0x020001DD RID: 477
		[StructLayout(LayoutKind.Explicit, Size = 12)]
		private struct SwitchSimpleInputReport
		{
			// Token: 0x060014C0 RID: 5312 RVA: 0x000603F4 File Offset: 0x0005E5F4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				byte b = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftX, 16U, 8U);
				byte b2 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftY, 16U, 8U);
				byte b3 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightX, 16U, 8U);
				byte b4 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightY, 16U, 8U);
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = b,
					leftStickY = b2,
					rightStickX = b3,
					rightStickY = b4
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				switch (this.hat)
				{
				case 0:
					flag2 = true;
					break;
				case 1:
					flag2 = true;
					flag3 = true;
					break;
				case 2:
					flag3 = true;
					break;
				case 3:
					flag4 = true;
					flag3 = true;
					break;
				case 4:
					flag4 = true;
					break;
				case 5:
					flag4 = true;
					flag = true;
					break;
				case 6:
					flag = true;
					break;
				case 7:
					flag2 = true;
					flag = true;
					break;
				}
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, flag);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, flag2);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, flag3);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, flag4);
				return switchProControllerHIDInputState;
			}

			// Token: 0x040009E7 RID: 2535
			public const int kSize = 12;

			// Token: 0x040009E8 RID: 2536
			public const byte ExpectedReportId = 63;

			// Token: 0x040009E9 RID: 2537
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040009EA RID: 2538
			[FieldOffset(1)]
			public byte buttons0;

			// Token: 0x040009EB RID: 2539
			[FieldOffset(2)]
			public byte buttons1;

			// Token: 0x040009EC RID: 2540
			[FieldOffset(3)]
			public byte hat;

			// Token: 0x040009ED RID: 2541
			[FieldOffset(4)]
			public ushort leftX;

			// Token: 0x040009EE RID: 2542
			[FieldOffset(6)]
			public ushort leftY;

			// Token: 0x040009EF RID: 2543
			[FieldOffset(8)]
			public ushort rightX;

			// Token: 0x040009F0 RID: 2544
			[FieldOffset(10)]
			public ushort rightY;
		}

		// Token: 0x020001DE RID: 478
		[StructLayout(LayoutKind.Explicit, Size = 25)]
		private struct SwitchFullInputReport
		{
			// Token: 0x060014C1 RID: 5313 RVA: 0x00060624 File Offset: 0x0005E824
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				uint num = (uint)((int)this.left0 | ((int)(this.left1 & 15) << 8));
				uint num2 = (uint)(((this.left1 & 240) >> 4) | ((int)this.left2 << 4));
				uint num3 = (uint)((int)this.right0 | ((int)(this.right1 & 15) << 8));
				uint num4 = (uint)(((this.right1 & 240) >> 4) | ((int)this.right2 << 4));
				byte b = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num, 12U, 8U);
				byte b2 = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num2, 12U, 8U);
				byte b3 = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num3, 12U, 8U);
				byte b4 = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num4, 12U, 8U);
				SwitchProControllerHIDInputState switchProControllerHIDInputState = new SwitchProControllerHIDInputState
				{
					leftStickX = b,
					leftStickY = b2,
					rightStickX = b3,
					rightStickY = b4
				};
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Down, (this.buttons2 & 1) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Up, (this.buttons2 & 2) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Right, (this.buttons2 & 4) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.Left, (this.buttons2 & 8) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons2 & 64) > 0);
				switchProControllerHIDInputState.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons2 & 128) > 0);
				return switchProControllerHIDInputState;
			}

			// Token: 0x040009F1 RID: 2545
			public const int kSize = 25;

			// Token: 0x040009F2 RID: 2546
			public const byte ExpectedReportId = 48;

			// Token: 0x040009F3 RID: 2547
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040009F4 RID: 2548
			[FieldOffset(3)]
			public byte buttons0;

			// Token: 0x040009F5 RID: 2549
			[FieldOffset(4)]
			public byte buttons1;

			// Token: 0x040009F6 RID: 2550
			[FieldOffset(5)]
			public byte buttons2;

			// Token: 0x040009F7 RID: 2551
			[FieldOffset(6)]
			public byte left0;

			// Token: 0x040009F8 RID: 2552
			[FieldOffset(7)]
			public byte left1;

			// Token: 0x040009F9 RID: 2553
			[FieldOffset(8)]
			public byte left2;

			// Token: 0x040009FA RID: 2554
			[FieldOffset(9)]
			public byte right0;

			// Token: 0x040009FB RID: 2555
			[FieldOffset(10)]
			public byte right1;

			// Token: 0x040009FC RID: 2556
			[FieldOffset(11)]
			public byte right2;
		}

		// Token: 0x020001DF RID: 479
		[StructLayout(LayoutKind.Explicit)]
		private struct SwitchHIDGenericInputReport
		{
			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0006085E File Offset: 0x0005EA5E
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x040009FD RID: 2557
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x020001E0 RID: 480
		[StructLayout(LayoutKind.Explicit, Size = 49)]
		internal struct SwitchMagicOutputReport
		{
			// Token: 0x040009FE RID: 2558
			public const int kSize = 49;

			// Token: 0x040009FF RID: 2559
			public const byte ExpectedReplyInputReportId = 129;

			// Token: 0x04000A00 RID: 2560
			[FieldOffset(0)]
			public byte reportType;

			// Token: 0x04000A01 RID: 2561
			[FieldOffset(1)]
			public byte commandId;

			// Token: 0x0200027D RID: 637
			internal enum ReportType
			{
				// Token: 0x04000D1B RID: 3355
				Magic = 128
			}

			// Token: 0x0200027E RID: 638
			public enum CommandIdType
			{
				// Token: 0x04000D1D RID: 3357
				Status = 1,
				// Token: 0x04000D1E RID: 3358
				Handshake,
				// Token: 0x04000D1F RID: 3359
				Highspeed,
				// Token: 0x04000D20 RID: 3360
				ForceUSB
			}
		}

		// Token: 0x020001E1 RID: 481
		[StructLayout(LayoutKind.Explicit, Size = 57)]
		internal struct SwitchMagicOutputHIDBluetooth : IInputDeviceCommandInfo
		{
			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0006086D File Offset: 0x0005EA6D
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0006087C File Offset: 0x0005EA7C
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type;
				}
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x00060884 File Offset: 0x0005EA84
			public static SwitchProControllerHID.SwitchMagicOutputHIDBluetooth Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDBluetooth
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type, 57),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x04000A02 RID: 2562
			public const int kSize = 57;

			// Token: 0x04000A03 RID: 2563
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x04000A04 RID: 2564
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}

		// Token: 0x020001E2 RID: 482
		[StructLayout(LayoutKind.Explicit, Size = 72)]
		internal struct SwitchMagicOutputHIDUSB : IInputDeviceCommandInfo
		{
			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000608D2 File Offset: 0x0005EAD2
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000608E1 File Offset: 0x0005EAE1
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type;
				}
			}

			// Token: 0x060014C8 RID: 5320 RVA: 0x000608E8 File Offset: 0x0005EAE8
			public static SwitchProControllerHID.SwitchMagicOutputHIDUSB Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDUSB
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type, 72),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x04000A05 RID: 2565
			public const int kSize = 72;

			// Token: 0x04000A06 RID: 2566
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x04000A07 RID: 2567
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}
	}
}
