using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C8 RID: 200
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct GamepadState : IInputStateTypeInfo
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00044F9D File Offset: 0x0004319D
		public static FourCC Format
		{
			get
			{
				return new FourCC('G', 'P', 'A', 'D');
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00044FAC File Offset: 0x000431AC
		public FourCC format
		{
			get
			{
				return GamepadState.Format;
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00044FB4 File Offset: 0x000431B4
		public GamepadState(params GamepadButton[] buttons)
		{
			this = default(GamepadState);
			if (buttons == null)
			{
				throw new ArgumentNullException("buttons");
			}
			foreach (GamepadButton gamepadButton in buttons)
			{
				uint num = 1U << (int)gamepadButton;
				this.buttons |= num;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00045000 File Offset: 0x00043200
		public GamepadState WithButton(GamepadButton button, bool value = true)
		{
			uint num = 1U << (int)button;
			if (value)
			{
				this.buttons |= num;
			}
			else
			{
				this.buttons &= ~num;
			}
			return this;
		}

		// Token: 0x0400053A RID: 1338
		internal const string ButtonSouthShortDisplayName = "A";

		// Token: 0x0400053B RID: 1339
		internal const string ButtonNorthShortDisplayName = "Y";

		// Token: 0x0400053C RID: 1340
		internal const string ButtonWestShortDisplayName = "X";

		// Token: 0x0400053D RID: 1341
		internal const string ButtonEastShortDisplayName = "B";

		// Token: 0x0400053E RID: 1342
		[InputControl(name = "dpad", layout = "Dpad", usage = "Hatswitch", displayName = "D-Pad", format = "BIT", sizeInBits = 4U, bit = 0U)]
		[InputControl(name = "buttonSouth", layout = "Button", bit = 6U, usages = new string[] { "PrimaryAction", "Submit" }, aliases = new string[] { "a", "cross" }, displayName = "Button South", shortDisplayName = "A")]
		[InputControl(name = "buttonWest", layout = "Button", bit = 7U, usage = "SecondaryAction", aliases = new string[] { "x", "square" }, displayName = "Button West", shortDisplayName = "X")]
		[InputControl(name = "buttonNorth", layout = "Button", bit = 4U, aliases = new string[] { "y", "triangle" }, displayName = "Button North", shortDisplayName = "Y")]
		[InputControl(name = "buttonEast", layout = "Button", bit = 5U, usages = new string[] { "Back", "Cancel" }, aliases = new string[] { "b", "circle" }, displayName = "Button East", shortDisplayName = "B")]
		[InputControl(name = "leftStickPress", layout = "Button", bit = 8U, displayName = "Left Stick Press")]
		[InputControl(name = "rightStickPress", layout = "Button", bit = 9U, displayName = "Right Stick Press")]
		[InputControl(name = "leftShoulder", layout = "Button", bit = 10U, displayName = "Left Shoulder", shortDisplayName = "LB")]
		[InputControl(name = "rightShoulder", layout = "Button", bit = 11U, displayName = "Right Shoulder", shortDisplayName = "RB")]
		[InputControl(name = "start", layout = "Button", bit = 12U, usage = "Menu", displayName = "Start")]
		[InputControl(name = "select", layout = "Button", bit = 13U, displayName = "Select")]
		[FieldOffset(0)]
		public uint buttons;

		// Token: 0x0400053F RID: 1343
		[InputControl(layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Stick", shortDisplayName = "LS")]
		[FieldOffset(4)]
		public Vector2 leftStick;

		// Token: 0x04000540 RID: 1344
		[InputControl(layout = "Stick", usage = "Secondary2DMotion", processors = "stickDeadzone", displayName = "Right Stick", shortDisplayName = "RS")]
		[FieldOffset(12)]
		public Vector2 rightStick;

		// Token: 0x04000541 RID: 1345
		[InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Left Trigger", shortDisplayName = "LT")]
		[FieldOffset(20)]
		public float leftTrigger;

		// Token: 0x04000542 RID: 1346
		[InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Right Trigger", shortDisplayName = "RT")]
		[FieldOffset(24)]
		public float rightTrigger;
	}
}
