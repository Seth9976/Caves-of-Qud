using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D4 RID: 212
	[StructLayout(LayoutKind.Explicit, Size = 36)]
	public struct PenState : IInputStateTypeInfo
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x000451D3 File Offset: 0x000433D3
		public static FourCC Format
		{
			get
			{
				return new FourCC('P', 'E', 'N', ' ');
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000451E4 File Offset: 0x000433E4
		public PenState WithButton(PenButton button, bool state = true)
		{
			uint num = 1U << (int)button;
			if (state)
			{
				this.buttons |= (ushort)num;
			}
			else
			{
				this.buttons &= (ushort)(~(ushort)num);
			}
			return this;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00045224 File Offset: 0x00043424
		public FourCC format
		{
			get
			{
				return PenState.Format;
			}
		}

		// Token: 0x04000571 RID: 1393
		[InputControl(usage = "Point", dontReset = true)]
		[FieldOffset(0)]
		public Vector2 position;

		// Token: 0x04000572 RID: 1394
		[InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		[FieldOffset(8)]
		public Vector2 delta;

		// Token: 0x04000573 RID: 1395
		[InputControl(layout = "Vector2", displayName = "Tilt", usage = "Tilt")]
		[FieldOffset(16)]
		public Vector2 tilt;

		// Token: 0x04000574 RID: 1396
		[InputControl(layout = "Analog", usage = "Pressure", defaultState = 0f)]
		[FieldOffset(24)]
		public float pressure;

		// Token: 0x04000575 RID: 1397
		[InputControl(layout = "Axis", displayName = "Twist", usage = "Twist")]
		[FieldOffset(28)]
		public float twist;

		// Token: 0x04000576 RID: 1398
		[InputControl(name = "tip", displayName = "Tip", layout = "Button", bit = 0U, usage = "PrimaryAction")]
		[InputControl(name = "press", useStateFrom = "tip", synthetic = true, usages = new string[] { })]
		[InputControl(name = "eraser", displayName = "Eraser", layout = "Button", bit = 1U)]
		[InputControl(name = "inRange", displayName = "In Range?", layout = "Button", bit = 4U, synthetic = true)]
		[InputControl(name = "barrel1", displayName = "Barrel Button #1", layout = "Button", bit = 2U, alias = "barrelFirst", usage = "SecondaryAction")]
		[InputControl(name = "barrel2", displayName = "Barrel Button #2", layout = "Button", bit = 3U, alias = "barrelSecond")]
		[InputControl(name = "barrel3", displayName = "Barrel Button #3", layout = "Button", bit = 5U, alias = "barrelThird")]
		[InputControl(name = "barrel4", displayName = "Barrel Button #4", layout = "Button", bit = 6U, alias = "barrelFourth")]
		[InputControl(name = "radius", layout = "Vector2", format = "VEC2", sizeInBits = 64U, usage = "Radius", offset = 4294967294U)]
		[InputControl(name = "pointerId", layout = "Digital", format = "UINT", sizeInBits = 32U, offset = 4294967294U)]
		[FieldOffset(32)]
		public ushort buttons;

		// Token: 0x04000577 RID: 1399
		[InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		[FieldOffset(34)]
		private ushort displayIndex;
	}
}
