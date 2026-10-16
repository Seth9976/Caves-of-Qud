using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000EF RID: 239
	public class TempoEvent : MetaEvent
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00011718 File Offset: 0x0000F918
		public TempoEvent(BinaryReader br, int length)
		{
			if (length != 3)
			{
				throw new FormatException("Invalid tempo length");
			}
			this.microsecondsPerQuarterNote = ((int)br.ReadByte() << 16) + ((int)br.ReadByte() << 8) + (int)br.ReadByte();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001174E File Offset: 0x0000F94E
		public TempoEvent(int microsecondsPerQuarterNote, long absoluteTime)
			: base(MetaEventType.SetTempo, 3, absoluteTime)
		{
			this.microsecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00011761 File Offset: 0x0000F961
		public override string ToString()
		{
			return string.Format("{0} {2}bpm ({1})", base.ToString(), this.microsecondsPerQuarterNote, 60000000 / this.microsecondsPerQuarterNote);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001178F File Offset: 0x0000F98F
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00011797 File Offset: 0x0000F997
		public int MicrosecondsPerQuarterNote
		{
			get
			{
				return this.microsecondsPerQuarterNote;
			}
			set
			{
				this.microsecondsPerQuarterNote = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000117A0 File Offset: 0x0000F9A0
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x000117B3 File Offset: 0x0000F9B3
		public double Tempo
		{
			get
			{
				return 60000000.0 / (double)this.microsecondsPerQuarterNote;
			}
			set
			{
				this.microsecondsPerQuarterNote = (int)(60000000.0 / value);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000117C8 File Offset: 0x0000F9C8
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)((this.microsecondsPerQuarterNote >> 16) & 255));
			writer.Write((byte)((this.microsecondsPerQuarterNote >> 8) & 255));
			writer.Write((byte)(this.microsecondsPerQuarterNote & 255));
		}

		// Token: 0x040005D7 RID: 1495
		private int microsecondsPerQuarterNote;
	}
}
