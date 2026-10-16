using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000179 RID: 377
	internal sealed class NativeHeapMemoryBlock : AbstractMemoryBlock
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00022E62 File Offset: 0x00021062
		internal NativeHeapMemoryBlock(int size)
		{
			this._data = new NativeHeapMemoryBlock.DisposableData(size);
			this._size = size;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00022E7D File Offset: 0x0002107D
		public override void Dispose()
		{
			this._data.Dispose();
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00022E8A File Offset: 0x0002108A
		public unsafe override byte* Pointer
		{
			get
			{
				return this._data.Pointer;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00022E97 File Offset: 0x00021097
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0400094F RID: 2383
		private readonly NativeHeapMemoryBlock.DisposableData _data;

		// Token: 0x04000950 RID: 2384
		private readonly int _size;

		// Token: 0x02000205 RID: 517
		private sealed class DisposableData : CriticalDisposableObject
		{
			// Token: 0x06000D55 RID: 3413 RVA: 0x00027024 File Offset: 0x00025224
			public DisposableData(int size)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._pointer = Marshal.AllocHGlobal(size);
				}
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x0002705C File Offset: 0x0002525C
			protected override void Release()
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					IntPtr intPtr = Interlocked.Exchange(ref this._pointer, IntPtr.Zero);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			}

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000D57 RID: 3415 RVA: 0x000270A8 File Offset: 0x000252A8
			public unsafe byte* Pointer
			{
				get
				{
					return (byte*)(void*)this._pointer;
				}
			}

			// Token: 0x04000BE3 RID: 3043
			private IntPtr _pointer;
		}
	}
}
