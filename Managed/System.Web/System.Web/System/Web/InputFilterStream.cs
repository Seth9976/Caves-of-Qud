using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C1 RID: 193
	internal class InputFilterStream : Stream
	{
		// Token: 0x170003CC RID: 972
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x000193B9 File Offset: 0x000175B9
		internal Stream BaseStream
		{
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000193C2 File Offset: 0x000175C2
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000193CF File Offset: 0x000175CF
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000193DD File Offset: 0x000175DD
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000193EA File Offset: 0x000175EA
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000193FA File Offset: 0x000175FA
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00019407 File Offset: 0x00017607
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this.stream.Seek(offset, loc);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00019416 File Offset: 0x00017616
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00019416 File Offset: 0x00017616
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00019416 File Offset: 0x00017616
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Flush()
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00019422 File Offset: 0x00017622
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x04001068 RID: 4200
		private Stream stream;
	}
}
