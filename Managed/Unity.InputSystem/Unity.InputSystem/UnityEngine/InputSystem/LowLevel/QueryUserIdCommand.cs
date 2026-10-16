using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C2 RID: 194
	[StructLayout(LayoutKind.Explicit, Size = 520)]
	internal struct QueryUserIdCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00044DCD File Offset: 0x00042FCD
		public static FourCC Type
		{
			get
			{
				return new FourCC('U', 'S', 'E', 'R');
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00044DDC File Offset: 0x00042FDC
		public unsafe string ReadId()
		{
			fixed (QueryUserIdCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->idBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00044E07 File Offset: 0x00043007
		public FourCC typeStatic
		{
			get
			{
				return QueryUserIdCommand.Type;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00044E10 File Offset: 0x00043010
		public static QueryUserIdCommand Create()
		{
			return new QueryUserIdCommand
			{
				baseCommand = new InputDeviceCommand(QueryUserIdCommand.Type, 520)
			};
		}

		// Token: 0x04000529 RID: 1321
		public const int kMaxIdLength = 256;

		// Token: 0x0400052A RID: 1322
		internal const int kSize = 520;

		// Token: 0x0400052B RID: 1323
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400052C RID: 1324
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(8)]
		public QueryUserIdCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x0200020D RID: 525
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <idBuffer>e__FixedBuffer
		{
			// Token: 0x04000B95 RID: 2965
			public byte FixedElementField;
		}
	}
}
