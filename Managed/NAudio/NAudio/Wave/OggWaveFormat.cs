using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200019C RID: 412
	[StructLayout(0, Pack = 2)]
	internal class OggWaveFormat : WaveFormat
	{
		// Token: 0x040009A0 RID: 2464
		public uint dwVorbisACMVersion;

		// Token: 0x040009A1 RID: 2465
		public uint dwLibVorbisVersion;
	}
}
