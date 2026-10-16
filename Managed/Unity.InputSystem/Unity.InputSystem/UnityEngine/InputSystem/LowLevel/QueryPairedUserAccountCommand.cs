using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct QueryPairedUserAccountCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00044C18 File Offset: 0x00042E18
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'C', 'C');
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00044C28 File Offset: 0x00042E28
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x00044C54 File Offset: 0x00042E54
		public unsafe string id
		{
			get
			{
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("ID '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					byte* ptr2 = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)ptr2), 256);
				}
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00044CC0 File Offset: 0x00042EC0
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00044CEC File Offset: 0x00042EEC
		public unsafe string name
		{
			get
			{
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("Name '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					byte* ptr2 = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)ptr2), 256);
				}
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00044D58 File Offset: 0x00042F58
		public FourCC typeStatic
		{
			get
			{
				return QueryPairedUserAccountCommand.Type;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00044D60 File Offset: 0x00042F60
		public static QueryPairedUserAccountCommand Create()
		{
			return new QueryPairedUserAccountCommand
			{
				baseCommand = new InputDeviceCommand(QueryPairedUserAccountCommand.Type, 1040)
			};
		}

		// Token: 0x0400051F RID: 1311
		internal const int kMaxNameLength = 256;

		// Token: 0x04000520 RID: 1312
		internal const int kMaxIdLength = 256;

		// Token: 0x04000521 RID: 1313
		internal const int kSize = 1040;

		// Token: 0x04000522 RID: 1314
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000523 RID: 1315
		[FieldOffset(8)]
		public ulong handle;

		// Token: 0x04000524 RID: 1316
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(16)]
		internal QueryPairedUserAccountCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x04000525 RID: 1317
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(528)]
		internal QueryPairedUserAccountCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x0200020A RID: 522
		[Flags]
		public enum Result : long
		{
			// Token: 0x04000B8F RID: 2959
			DevicePairedToUserAccount = 2L,
			// Token: 0x04000B90 RID: 2960
			UserAccountSelectionInProgress = 4L,
			// Token: 0x04000B91 RID: 2961
			UserAccountSelectionComplete = 8L,
			// Token: 0x04000B92 RID: 2962
			UserAccountSelectionCanceled = 16L
		}

		// Token: 0x0200020B RID: 523
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <idBuffer>e__FixedBuffer
		{
			// Token: 0x04000B93 RID: 2963
			public byte FixedElementField;
		}

		// Token: 0x0200020C RID: 524
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x04000B94 RID: 2964
			public byte FixedElementField;
		}
	}
}
