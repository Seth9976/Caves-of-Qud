using System;

namespace NAudio.Wave
{
	// Token: 0x020000A5 RID: 165
	public interface IMp3FrameDecompressor : IDisposable
	{
		// Token: 0x06000394 RID: 916
		int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset);

		// Token: 0x06000395 RID: 917
		void Reset();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000396 RID: 918
		WaveFormat OutputFormat { get; }
	}
}
