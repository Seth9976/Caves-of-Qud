using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000B0F RID: 2831
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06006534 RID: 25908 RVA: 0x0015856C File Offset: 0x0015676C
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			this._array = array;
			this._pinningHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			int num = array.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(array))
			{
				byte* ptr = reference;
				base.Initialize(ptr, (long)num, (long)num, FileAccess.Read);
			}
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001585B5 File Offset: 0x001567B5
		public override int Read(Span<byte> buffer)
		{
			return base.ReadCore(buffer);
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001585BE File Offset: 0x001567BE
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			base.WriteCore(buffer);
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x001585C8 File Offset: 0x001567C8
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x001585F8 File Offset: 0x001567F8
		protected override void Dispose(bool disposing)
		{
			if (this._pinningHandle.IsAllocated)
			{
				this._pinningHandle.Free();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003B67 RID: 15207
		private byte[] _array;

		// Token: 0x04003B68 RID: 15208
		private GCHandle _pinningHandle;
	}
}
