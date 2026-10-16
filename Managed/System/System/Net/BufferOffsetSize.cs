using System;

namespace System.Net
{
	// Token: 0x02000623 RID: 1571
	internal class BufferOffsetSize
	{
		// Token: 0x060031D0 RID: 12752 RVA: 0x000AC7F8 File Offset: 0x000AA9F8
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				global::System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000AC83B File Offset: 0x000AAA3B
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer)
			: this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x04001CFD RID: 7421
		internal byte[] Buffer;

		// Token: 0x04001CFE RID: 7422
		internal int Offset;

		// Token: 0x04001CFF RID: 7423
		internal int Size;
	}
}
