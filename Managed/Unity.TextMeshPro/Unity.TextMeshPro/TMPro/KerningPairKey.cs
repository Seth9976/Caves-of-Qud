using System;

namespace TMPro
{
	// Token: 0x0200002A RID: 42
	public struct KerningPairKey
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00008D6E File Offset: 0x00006F6E
		public KerningPairKey(uint ascii_left, uint ascii_right)
		{
			this.ascii_Left = ascii_left;
			this.ascii_Right = ascii_right;
			this.key = (ascii_right << 16) + ascii_left;
		}

		// Token: 0x04000115 RID: 277
		public uint ascii_Left;

		// Token: 0x04000116 RID: 278
		public uint ascii_Right;

		// Token: 0x04000117 RID: 279
		public uint key;
	}
}
