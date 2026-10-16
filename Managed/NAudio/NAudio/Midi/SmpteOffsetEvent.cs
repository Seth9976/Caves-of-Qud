using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000ED RID: 237
	internal class SmpteOffsetEvent : MetaEvent
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public SmpteOffsetEvent(BinaryReader br, int length)
		{
			if (length != 5)
			{
				throw new FormatException(string.Format("Invalid SMPTE Offset length: Got {0}, expected 5", length));
			}
			this.hours = br.ReadByte();
			this.minutes = br.ReadByte();
			this.seconds = br.ReadByte();
			this.frames = br.ReadByte();
			this.subFrames = br.ReadByte();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00011549 File Offset: 0x0000F749
		public int Hours
		{
			get
			{
				return (int)this.hours;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00011551 File Offset: 0x0000F751
		public int Minutes
		{
			get
			{
				return (int)this.minutes;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00011559 File Offset: 0x0000F759
		public int Seconds
		{
			get
			{
				return (int)this.seconds;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00011561 File Offset: 0x0000F761
		public int Frames
		{
			get
			{
				return (int)this.frames;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00011569 File Offset: 0x0000F769
		public int SubFrames
		{
			get
			{
				return (int)this.subFrames;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011574 File Offset: 0x0000F774
		public override string ToString()
		{
			return string.Format("{0} {1}:{2}:{3}:{4}:{5}", new object[]
			{
				base.ToString(),
				this.hours,
				this.minutes,
				this.seconds,
				this.frames,
				this.subFrames
			});
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000115E4 File Offset: 0x0000F7E4
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.hours);
			writer.Write(this.minutes);
			writer.Write(this.seconds);
			writer.Write(this.frames);
			writer.Write(this.subFrames);
		}

		// Token: 0x040005D1 RID: 1489
		private byte hours;

		// Token: 0x040005D2 RID: 1490
		private byte minutes;

		// Token: 0x040005D3 RID: 1491
		private byte seconds;

		// Token: 0x040005D4 RID: 1492
		private byte frames;

		// Token: 0x040005D5 RID: 1493
		private byte subFrames;
	}
}
