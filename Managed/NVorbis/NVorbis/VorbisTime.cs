using System;

namespace NVorbis
{
	// Token: 0x0200002D RID: 45
	internal abstract class VorbisTime
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000A700 File Offset: 0x00008900
		internal static VorbisTime Init(VorbisStreamDecoder vorbis, DataPacket packet)
		{
			int num = (int)packet.ReadBits(16);
			VorbisTime vorbisTime = null;
			int num2 = num;
			if (num2 == 0)
			{
				vorbisTime = new VorbisTime.Time0(vorbis);
			}
			if (vorbisTime == null)
			{
				throw new InvalidDataException();
			}
			vorbisTime.Init(packet);
			return vorbisTime;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000A737 File Offset: 0x00008937
		protected VorbisTime(VorbisStreamDecoder vorbis)
		{
			this._vorbis = vorbis;
		}

		// Token: 0x06000185 RID: 389
		protected abstract void Init(DataPacket packet);

		// Token: 0x04000106 RID: 262
		private VorbisStreamDecoder _vorbis;

		// Token: 0x0200002E RID: 46
		private class Time0 : VorbisTime
		{
			// Token: 0x06000186 RID: 390 RVA: 0x0000A746 File Offset: 0x00008946
			internal Time0(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000A74F File Offset: 0x0000894F
			protected override void Init(DataPacket packet)
			{
			}
		}
	}
}
