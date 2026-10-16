using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch.LowLevel
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Explicit, Size = 7)]
	internal struct SwitchProControllerHIDInputState : IInputStateTypeInfo
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000402B8 File Offset: 0x0003E4B8
		public FourCC format
		{
			get
			{
				return SwitchProControllerHIDInputState.Format;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000402BF File Offset: 0x0003E4BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SwitchProControllerHIDInputState WithButton(SwitchProControllerHIDInputState.Button button, bool value = true)
		{
			this.Set(button, value);
			return this;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000402D0 File Offset: 0x0003E4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(SwitchProControllerHIDInputState.Button button, bool state)
		{
			if (button >= SwitchProControllerHIDInputState.Button.Capture)
			{
				if (button < (SwitchProControllerHIDInputState.Button)18)
				{
					byte b = (byte)(1 << button - SwitchProControllerHIDInputState.Button.Capture);
					if (state)
					{
						this.buttons2 |= b;
						return;
					}
					this.buttons2 &= ~b;
				}
				return;
			}
			ushort num = (ushort)(1 << (int)button);
			if (state)
			{
				this.buttons1 |= num;
				return;
			}
			this.buttons1 &= ~num;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00040343 File Offset: 0x0003E543
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Press(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, true);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0004034D File Offset: 0x0003E54D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Release(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, false);
		}

		// Token: 0x04000462 RID: 1122
		public static FourCC Format = new FourCC('S', 'P', 'V', 'S');

		// Token: 0x04000463 RID: 1123
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85")]
		[InputControl(name = "leftStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(0)]
		public byte leftStickX;

		// Token: 0x04000464 RID: 1124
		[FieldOffset(1)]
		public byte leftStickY;

		// Token: 0x04000465 RID: 1125
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "rightStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(2)]
		public byte rightStickX;

		// Token: 0x04000466 RID: 1126
		[FieldOffset(3)]
		public byte rightStickY;

		// Token: 0x04000467 RID: 1127
		[InputControl(name = "dpad", format = "BIT", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/right", bit = 1U)]
		[InputControl(name = "dpad/down", bit = 2U)]
		[InputControl(name = "dpad/left", bit = 3U)]
		[InputControl(name = "buttonWest", displayName = "Y", shortDisplayName = "Y", bit = 4U, usage = "SecondaryAction")]
		[InputControl(name = "buttonNorth", displayName = "X", shortDisplayName = "X", bit = 5U)]
		[InputControl(name = "buttonSouth", displayName = "B", shortDisplayName = "B", bit = 6U, usages = new string[] { "Back", "Cancel" })]
		[InputControl(name = "buttonEast", displayName = "A", shortDisplayName = "A", bit = 7U, usages = new string[] { "PrimaryAction", "Submit" })]
		[InputControl(name = "leftShoulder", displayName = "L", shortDisplayName = "L", bit = 8U)]
		[InputControl(name = "rightShoulder", displayName = "R", shortDisplayName = "R", bit = 9U)]
		[InputControl(name = "leftStickPress", displayName = "Left Stick", bit = 10U)]
		[InputControl(name = "rightStickPress", displayName = "Right Stick", bit = 11U)]
		[InputControl(name = "leftTrigger", displayName = "ZL", shortDisplayName = "ZL", format = "BIT", bit = 12U)]
		[InputControl(name = "rightTrigger", displayName = "ZR", shortDisplayName = "ZR", format = "BIT", bit = 13U)]
		[InputControl(name = "start", displayName = "Plus", bit = 14U, usage = "Menu")]
		[InputControl(name = "select", displayName = "Minus", bit = 15U)]
		[FieldOffset(4)]
		public ushort buttons1;

		// Token: 0x04000468 RID: 1128
		[InputControl(name = "capture", layout = "Button", displayName = "Capture", bit = 0U)]
		[InputControl(name = "home", layout = "Button", displayName = "Home", bit = 1U)]
		[FieldOffset(6)]
		public byte buttons2;

		// Token: 0x020001E3 RID: 483
		public enum Button
		{
			// Token: 0x04000A09 RID: 2569
			Up,
			// Token: 0x04000A0A RID: 2570
			Right,
			// Token: 0x04000A0B RID: 2571
			Down,
			// Token: 0x04000A0C RID: 2572
			Left,
			// Token: 0x04000A0D RID: 2573
			West,
			// Token: 0x04000A0E RID: 2574
			North,
			// Token: 0x04000A0F RID: 2575
			South,
			// Token: 0x04000A10 RID: 2576
			East,
			// Token: 0x04000A11 RID: 2577
			L,
			// Token: 0x04000A12 RID: 2578
			R,
			// Token: 0x04000A13 RID: 2579
			StickL,
			// Token: 0x04000A14 RID: 2580
			StickR,
			// Token: 0x04000A15 RID: 2581
			ZL,
			// Token: 0x04000A16 RID: 2582
			ZR,
			// Token: 0x04000A17 RID: 2583
			Plus,
			// Token: 0x04000A18 RID: 2584
			Minus,
			// Token: 0x04000A19 RID: 2585
			Capture,
			// Token: 0x04000A1A RID: 2586
			Home,
			// Token: 0x04000A1B RID: 2587
			X = 5,
			// Token: 0x04000A1C RID: 2588
			B,
			// Token: 0x04000A1D RID: 2589
			Y = 4,
			// Token: 0x04000A1E RID: 2590
			A = 7
		}
	}
}
