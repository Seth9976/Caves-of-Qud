using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000C4 RID: 196
	public class SampleHeader
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public override string ToString()
		{
			return this.SampleName;
		}

		// Token: 0x04000519 RID: 1305
		public string SampleName;

		// Token: 0x0400051A RID: 1306
		public uint Start;

		// Token: 0x0400051B RID: 1307
		public uint End;

		// Token: 0x0400051C RID: 1308
		public uint StartLoop;

		// Token: 0x0400051D RID: 1309
		public uint EndLoop;

		// Token: 0x0400051E RID: 1310
		public uint SampleRate;

		// Token: 0x0400051F RID: 1311
		public byte OriginalPitch;

		// Token: 0x04000520 RID: 1312
		public sbyte PitchCorrection;

		// Token: 0x04000521 RID: 1313
		public ushort SampleLink;

		// Token: 0x04000522 RID: 1314
		public SFSampleLink SFSampleLink;
	}
}
