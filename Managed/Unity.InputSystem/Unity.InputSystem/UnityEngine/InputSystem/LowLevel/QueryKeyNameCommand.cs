using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Explicit, Size = 268)]
	public struct QueryKeyNameCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00044AFD File Offset: 0x00042CFD
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'Y', 'C', 'F');
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00044B0C File Offset: 0x00042D0C
		public unsafe string ReadKeyName()
		{
			fixed (QueryKeyNameCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00044B37 File Offset: 0x00042D37
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyNameCommand.Type;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00044B40 File Offset: 0x00042D40
		public static QueryKeyNameCommand Create(Key key)
		{
			return new QueryKeyNameCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyNameCommand.Type, 268),
				scanOrKeyCode = (int)key
			};
		}

		// Token: 0x04000517 RID: 1303
		internal const int kMaxNameLength = 256;

		// Token: 0x04000518 RID: 1304
		internal const int kSize = 268;

		// Token: 0x04000519 RID: 1305
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400051A RID: 1306
		[FieldOffset(8)]
		public int scanOrKeyCode;

		// Token: 0x0400051B RID: 1307
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(12)]
		public QueryKeyNameCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x02000208 RID: 520
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x04000B8C RID: 2956
			public byte FixedElementField;
		}
	}
}
