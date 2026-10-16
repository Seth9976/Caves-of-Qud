using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BF RID: 191
	[StructLayout(LayoutKind.Explicit, Size = 264)]
	public struct QueryKeyboardLayoutCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00044B74 File Offset: 0x00042D74
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'B', 'L', 'T');
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00044B84 File Offset: 0x00042D84
		public unsafe string ReadLayoutName()
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00044BB0 File Offset: 0x00042DB0
		public unsafe void WriteLayoutName(string name)
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				QueryKeyboardLayoutCommand* ptr2 = ptr;
				StringHelpers.WriteStringToBuffer(name, new IntPtr((void*)(&ptr2->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00044BE2 File Offset: 0x00042DE2
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyboardLayoutCommand.Type;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00044BEC File Offset: 0x00042DEC
		public static QueryKeyboardLayoutCommand Create()
		{
			return new QueryKeyboardLayoutCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyboardLayoutCommand.Type, 264)
			};
		}

		// Token: 0x0400051C RID: 1308
		internal const int kMaxNameLength = 256;

		// Token: 0x0400051D RID: 1309
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400051E RID: 1310
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(8)]
		public QueryKeyboardLayoutCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x02000209 RID: 521
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x04000B8D RID: 2957
			public byte FixedElementField;
		}
	}
}
