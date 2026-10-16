using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XInput.LowLevel
{
	// Token: 0x0200007F RID: 127
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	internal struct XInputControllerWindowsState : IInputStateTypeInfo
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0003957F File Offset: 0x0003777F
		public FourCC format
		{
			get
			{
				return new FourCC('X', 'I', 'N', 'P');
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0003958E File Offset: 0x0003778E
		public XInputControllerWindowsState WithButton(XInputControllerWindowsState.Button button)
		{
			this.buttons |= (ushort)(1 << (int)button);
			return this;
		}

		// Token: 0x040003BB RID: 955
		[InputControl(name = "dpad", layout = "Dpad", sizeInBits = 4U, bit = 0U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/down", bit = 1U)]
		[InputControl(name = "dpad/left", bit = 2U)]
		[InputControl(name = "dpad/right", bit = 3U)]
		[InputControl(name = "start", bit = 4U, displayName = "Start")]
		[InputControl(name = "select", bit = 5U, displayName = "Select")]
		[InputControl(name = "leftStickPress", bit = 6U)]
		[InputControl(name = "rightStickPress", bit = 7U)]
		[InputControl(name = "leftShoulder", bit = 8U)]
		[InputControl(name = "rightShoulder", bit = 9U)]
		[InputControl(name = "buttonSouth", bit = 12U, displayName = "A")]
		[InputControl(name = "buttonEast", bit = 13U, displayName = "B")]
		[InputControl(name = "buttonWest", bit = 14U, displayName = "X")]
		[InputControl(name = "buttonNorth", bit = 15U, displayName = "Y")]
		[FieldOffset(0)]
		public ushort buttons;

		// Token: 0x040003BC RID: 956
		[InputControl(name = "leftTrigger", format = "BYTE")]
		[FieldOffset(2)]
		public byte leftTrigger;

		// Token: 0x040003BD RID: 957
		[InputControl(name = "rightTrigger", format = "BYTE")]
		[FieldOffset(3)]
		public byte rightTrigger;

		// Token: 0x040003BE RID: 958
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "leftStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(4)]
		public short leftStickX;

		// Token: 0x040003BF RID: 959
		[FieldOffset(6)]
		public short leftStickY;

		// Token: 0x040003C0 RID: 960
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "rightStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(8)]
		public short rightStickX;

		// Token: 0x040003C1 RID: 961
		[FieldOffset(10)]
		public short rightStickY;

		// Token: 0x020001CE RID: 462
		public enum Button
		{
			// Token: 0x0400098C RID: 2444
			DPadUp,
			// Token: 0x0400098D RID: 2445
			DPadDown,
			// Token: 0x0400098E RID: 2446
			DPadLeft,
			// Token: 0x0400098F RID: 2447
			DPadRight,
			// Token: 0x04000990 RID: 2448
			Start,
			// Token: 0x04000991 RID: 2449
			Select,
			// Token: 0x04000992 RID: 2450
			LeftThumbstickPress,
			// Token: 0x04000993 RID: 2451
			RightThumbstickPress,
			// Token: 0x04000994 RID: 2452
			LeftShoulder,
			// Token: 0x04000995 RID: 2453
			RightShoulder,
			// Token: 0x04000996 RID: 2454
			A = 12,
			// Token: 0x04000997 RID: 2455
			B,
			// Token: 0x04000998 RID: 2456
			X,
			// Token: 0x04000999 RID: 2457
			Y
		}
	}
}
