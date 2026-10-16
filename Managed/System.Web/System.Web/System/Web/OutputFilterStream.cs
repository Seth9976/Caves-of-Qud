using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C7 RID: 199
	internal sealed class OutputFilterStream : Stream
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0001C36B File Offset: 0x0001A56B
		public OutputFilterStream(HttpResponseStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override int ReadByte()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override long Seek(long offset, SeekOrigin loc)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00019416 File Offset: 0x00017616
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001C37A File Offset: 0x0001A57A
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Flush()
		{
		}

		// Token: 0x04001070 RID: 4208
		private HttpResponseStream stream;
	}
}
