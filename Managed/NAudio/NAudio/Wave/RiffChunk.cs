using System;
using System.Text;

namespace NAudio.Wave
{
	// Token: 0x020001EE RID: 494
	public class RiffChunk
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x00020806 File Offset: 0x0001EA06
		public RiffChunk(int identifier, int length, long streamPosition)
		{
			this.Identifier = identifier;
			this.Length = length;
			this.StreamPosition = streamPosition;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00020823 File Offset: 0x0001EA23
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0002082B File Offset: 0x0001EA2B
		public int Identifier { get; private set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00020834 File Offset: 0x0001EA34
		public string IdentifierAsString
		{
			get
			{
				return Encoding.UTF8.GetString(BitConverter.GetBytes(this.Identifier));
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002084B File Offset: 0x0001EA4B
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00020853 File Offset: 0x0001EA53
		public int Length { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002085C File Offset: 0x0001EA5C
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00020864 File Offset: 0x0001EA64
		public long StreamPosition { get; private set; }
	}
}
