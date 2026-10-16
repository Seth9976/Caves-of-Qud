using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003F RID: 63
	public readonly struct Blob
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000708F File Offset: 0x0000528F
		public int Length { get; }

		// Token: 0x0600023A RID: 570 RVA: 0x00007097 File Offset: 0x00005297
		[NullableContext(1)]
		internal Blob(byte[] buffer, int start, int length)
		{
			this.Buffer = buffer;
			this.Start = start;
			this.Length = length;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000070AE File Offset: 0x000052AE
		public bool IsDefault
		{
			get
			{
				return this.Buffer == null;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000070B9 File Offset: 0x000052B9
		public ArraySegment<byte> GetBytes()
		{
			return new ArraySegment<byte>(this.Buffer, this.Start, this.Length);
		}

		// Token: 0x0400019F RID: 415
		[Nullable(1)]
		internal readonly byte[] Buffer;

		// Token: 0x040001A0 RID: 416
		internal readonly int Start;
	}
}
