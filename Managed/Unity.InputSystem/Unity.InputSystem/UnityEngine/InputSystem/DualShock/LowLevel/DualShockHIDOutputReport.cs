using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000AE RID: 174
	[StructLayout(LayoutKind.Explicit, Size = 40)]
	internal struct DualShockHIDOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00044580 File Offset: 0x00042780
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0004458F File Offset: 0x0004278F
		public FourCC typeStatic
		{
			get
			{
				return DualShockHIDOutputReport.Type;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00044598 File Offset: 0x00042798
		public void SetMotorSpeeds(float lowFreq, float highFreq)
		{
			this.flags |= 1;
			this.lowFrequencyMotorSpeed = (byte)Mathf.Clamp(lowFreq * 255f, 0f, 255f);
			this.highFrequencyMotorSpeed = (byte)Mathf.Clamp(highFreq * 255f, 0f, 255f);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000445F0 File Offset: 0x000427F0
		public void SetColor(Color color)
		{
			this.flags |= 2;
			this.redColor = (byte)Mathf.Clamp(color.r * 255f, 0f, 255f);
			this.greenColor = (byte)Mathf.Clamp(color.g * 255f, 0f, 255f);
			this.blueColor = (byte)Mathf.Clamp(color.b * 255f, 0f, 255f);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00044674 File Offset: 0x00042874
		public static DualShockHIDOutputReport Create(int outputReportSize)
		{
			return new DualShockHIDOutputReport
			{
				baseCommand = new InputDeviceCommand(DualShockHIDOutputReport.Type, 8 + outputReportSize),
				reportId = 5
			};
		}

		// Token: 0x040004ED RID: 1261
		internal const int kSize = 40;

		// Token: 0x040004EE RID: 1262
		internal const int kReportId = 5;

		// Token: 0x040004EF RID: 1263
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004F0 RID: 1264
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x040004F1 RID: 1265
		[FieldOffset(9)]
		public byte flags;

		// Token: 0x040004F2 RID: 1266
		[FixedBuffer(typeof(byte), 2)]
		[FieldOffset(10)]
		public DualShockHIDOutputReport.<unknown1>e__FixedBuffer unknown1;

		// Token: 0x040004F3 RID: 1267
		[FieldOffset(12)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x040004F4 RID: 1268
		[FieldOffset(13)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x040004F5 RID: 1269
		[FieldOffset(14)]
		public byte redColor;

		// Token: 0x040004F6 RID: 1270
		[FieldOffset(15)]
		public byte greenColor;

		// Token: 0x040004F7 RID: 1271
		[FieldOffset(16)]
		public byte blueColor;

		// Token: 0x040004F8 RID: 1272
		[FixedBuffer(typeof(byte), 23)]
		[FieldOffset(17)]
		public DualShockHIDOutputReport.<unknown2>e__FixedBuffer unknown2;

		// Token: 0x02000204 RID: 516
		[Flags]
		public enum Flags
		{
			// Token: 0x04000B84 RID: 2948
			Rumble = 1,
			// Token: 0x04000B85 RID: 2949
			Color = 2
		}

		// Token: 0x02000205 RID: 517
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 2)]
		public struct <unknown1>e__FixedBuffer
		{
			// Token: 0x04000B86 RID: 2950
			public byte FixedElementField;
		}

		// Token: 0x02000206 RID: 518
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 23)]
		public struct <unknown2>e__FixedBuffer
		{
			// Token: 0x04000B87 RID: 2951
			public byte FixedElementField;
		}
	}
}
