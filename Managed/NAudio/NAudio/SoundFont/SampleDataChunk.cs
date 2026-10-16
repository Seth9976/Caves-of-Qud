using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000C3 RID: 195
	internal class SampleDataChunk
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0000E794 File Offset: 0x0000C994
		public SampleDataChunk(RiffChunk chunk)
		{
			string text = chunk.ReadChunkID();
			if (text != "sdta")
			{
				throw new InvalidDataException(string.Format("Not a sample data chunk ({0})", text));
			}
			this.sampleData = chunk.GetData();
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		public byte[] SampleData
		{
			get
			{
				return this.sampleData;
			}
		}

		// Token: 0x04000518 RID: 1304
		private byte[] sampleData;
	}
}
