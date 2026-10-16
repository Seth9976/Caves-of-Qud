using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DD RID: 221
	[StructLayout(LayoutKind.Explicit, Size = 560)]
	internal struct TouchscreenState : IInputStateTypeInfo
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000454EF File Offset: 0x000436EF
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'S', 'C', 'R');
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00045500 File Offset: 0x00043700
		public unsafe TouchState* primaryTouch
		{
			get
			{
				fixed (byte* ptr = &this.primaryTouchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0004551C File Offset: 0x0004371C
		public unsafe TouchState* touches
		{
			get
			{
				fixed (byte* ptr = &this.touchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00045537 File Offset: 0x00043737
		public FourCC format
		{
			get
			{
				return TouchscreenState.Format;
			}
		}

		// Token: 0x04000598 RID: 1432
		public const int MaxTouches = 10;

		// Token: 0x04000599 RID: 1433
		[FixedBuffer(typeof(byte), 56)]
		[InputControl(name = "primaryTouch", displayName = "Primary Touch", layout = "Touch", synthetic = true)]
		[InputControl(name = "primaryTouch/tap", usage = "PrimaryAction")]
		[InputControl(name = "position", useStateFrom = "primaryTouch/position")]
		[InputControl(name = "delta", useStateFrom = "primaryTouch/delta", layout = "Delta")]
		[InputControl(name = "pressure", useStateFrom = "primaryTouch/pressure")]
		[InputControl(name = "radius", useStateFrom = "primaryTouch/radius")]
		[InputControl(name = "press", useStateFrom = "primaryTouch/phase", layout = "TouchPress", synthetic = true, usages = new string[] { })]
		[InputControl(name = "displayIndex", useStateFrom = "primaryTouch/displayIndex", format = "BYTE")]
		[FieldOffset(0)]
		public TouchscreenState.<primaryTouchData>e__FixedBuffer primaryTouchData;

		// Token: 0x0400059A RID: 1434
		internal const int kTouchDataOffset = 56;

		// Token: 0x0400059B RID: 1435
		[FixedBuffer(typeof(byte), 560)]
		[InputControl(layout = "Touch", name = "touch", displayName = "Touch", arraySize = 10)]
		[FieldOffset(56)]
		public TouchscreenState.<touchData>e__FixedBuffer touchData;

		// Token: 0x02000210 RID: 528
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 56)]
		public struct <primaryTouchData>e__FixedBuffer
		{
			// Token: 0x04000B9D RID: 2973
			public byte FixedElementField;
		}

		// Token: 0x02000211 RID: 529
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 560)]
		public struct <touchData>e__FixedBuffer
		{
			// Token: 0x04000B9E RID: 2974
			public byte FixedElementField;
		}
	}
}
