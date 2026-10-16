using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000C2 RID: 194
	internal class IntPtrStream : Stream
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x0001942F File Offset: 0x0001762F
		public unsafe IntPtrStream(IntPtr base_address, int size)
		{
			this.base_address = (byte*)(void*)base_address;
			this.size = size;
			this.owns = true;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00019454 File Offset: 0x00017654
		public IntPtrStream(Stream stream)
		{
			IntPtrStream intPtrStream = (IntPtrStream)stream;
			this.size = intPtrStream.size;
			this.base_address = intPtrStream.base_address;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00019486 File Offset: 0x00017686
		protected unsafe IntPtr BaseAddress
		{
			get
			{
				return (IntPtr)((void*)this.base_address);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00019493 File Offset: 0x00017693
		protected int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001949B File Offset: 0x0001769B
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x000194A4 File Offset: 0x000176A4
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (this.position < 0)
				{
					throw new ArgumentOutOfRangeException("Position", "Can not be negative");
				}
				if (this.position > this.size)
				{
					throw new ArgumentOutOfRangeException("Position", "Pointer falls out of range");
				}
				this.position = (int)value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000194F0 File Offset: 0x000176F0
		public override long Length
		{
			get
			{
				return (long)this.size;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000194FC File Offset: 0x000176FC
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (this.position >= this.size || count == 0)
			{
				return 0;
			}
			if (this.position > this.size - count)
			{
				count = this.size - this.position;
			}
			Marshal.Copy((IntPtr)((void*)(this.base_address + this.position)), buffer, offset, count);
			this.position += count;
			return count;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000195B4 File Offset: 0x000177B4
		public unsafe override int ReadByte()
		{
			if (this.position >= this.size)
			{
				return -1;
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			int num = this.base_address;
			int num2 = this.position;
			this.position = num2 + 1;
			return (int)(*(num + num2));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00019600 File Offset: 0x00017800
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("Offset out of range. " + offset.ToString());
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			int num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				num = 0;
				break;
			case SeekOrigin.Current:
				num = this.position;
				break;
			case SeekOrigin.End:
				num = this.size;
				break;
			default:
				throw new ArgumentException("loc", "Invalid SeekOrigin");
			}
			checked
			{
				try
				{
					num += (int)offset;
				}
				catch
				{
					throw new ArgumentOutOfRangeException("Too large seek destination");
				}
				if (num < 0)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				this.position = num;
			}
			return (long)this.position;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00019416 File Offset: 0x00017616
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00019416 File Offset: 0x00017616
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00019416 File Offset: 0x00017616
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Flush()
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000196D0 File Offset: 0x000178D0
		public unsafe override void Close()
		{
			if (this.owns)
			{
				IntPtr intPtr = (IntPtr)((void*)this.base_address);
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				this.base_address = null;
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001970C File Offset: 0x0001790C
		protected unsafe override void Finalize()
		{
			try
			{
				if (this.owns)
				{
					IntPtr intPtr = (IntPtr)((void*)this.base_address);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					this.base_address = null;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04001069 RID: 4201
		private unsafe byte* base_address;

		// Token: 0x0400106A RID: 4202
		private int size;

		// Token: 0x0400106B RID: 4203
		private int position;

		// Token: 0x0400106C RID: 4204
		private bool owns;
	}
}
