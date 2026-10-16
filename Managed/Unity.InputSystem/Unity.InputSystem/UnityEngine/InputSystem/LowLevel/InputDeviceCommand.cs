using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BA RID: 186
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InputDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000449BC File Offset: 0x00042BBC
		public int payloadSizeInBytes
		{
			get
			{
				return this.sizeInBytes - 8;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x000449C8 File Offset: 0x00042BC8
		public unsafe void* payloadPtr
		{
			get
			{
				fixed (InputDeviceCommand* ptr = &this)
				{
					void* ptr2 = (void*)ptr;
					return (void*)((byte*)ptr2 + 8);
				}
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000449DD File Offset: 0x00042BDD
		public InputDeviceCommand(FourCC type, int sizeInBytes = 8)
		{
			this.type = type;
			this.sizeInBytes = sizeInBytes;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000449F0 File Offset: 0x00042BF0
		public unsafe static NativeArray<byte> AllocateNative(FourCC type, int payloadSize)
		{
			int num = payloadSize + 8;
			NativeArray<byte> nativeArray = new NativeArray<byte>(num, Allocator.Temp, NativeArrayOptions.ClearMemory);
			InputDeviceCommand* unsafePtr = (InputDeviceCommand*)nativeArray.GetUnsafePtr<byte>();
			unsafePtr->type = type;
			unsafePtr->sizeInBytes = num;
			return nativeArray;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00044A20 File Offset: 0x00042C20
		public FourCC typeStatic
		{
			get
			{
				return default(FourCC);
			}
		}

		// Token: 0x04000508 RID: 1288
		internal const int kBaseCommandSize = 8;

		// Token: 0x04000509 RID: 1289
		public const int BaseCommandSize = 8;

		// Token: 0x0400050A RID: 1290
		public const long GenericFailure = -1L;

		// Token: 0x0400050B RID: 1291
		public const long GenericSuccess = 1L;

		// Token: 0x0400050C RID: 1292
		[FieldOffset(0)]
		public FourCC type;

		// Token: 0x0400050D RID: 1293
		[FieldOffset(4)]
		public int sizeInBytes;
	}
}
