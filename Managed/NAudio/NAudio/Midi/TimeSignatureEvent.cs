using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000F1 RID: 241
	public class TimeSignatureEvent : MetaEvent
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x000118CC File Offset: 0x0000FACC
		public TimeSignatureEvent(BinaryReader br, int length)
		{
			if (length != 4)
			{
				throw new FormatException(string.Format("Invalid time signature length: Got {0}, expected 4", length));
			}
			this.numerator = br.ReadByte();
			this.denominator = br.ReadByte();
			this.ticksInMetronomeClick = br.ReadByte();
			this.no32ndNotesInQuarterNote = br.ReadByte();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00011929 File Offset: 0x0000FB29
		public TimeSignatureEvent(long absoluteTime, int numerator, int denominator, int ticksInMetronomeClick, int no32ndNotesInQuarterNote)
			: base(MetaEventType.TimeSignature, 4, absoluteTime)
		{
			this.numerator = (byte)numerator;
			this.denominator = (byte)denominator;
			this.ticksInMetronomeClick = (byte)ticksInMetronomeClick;
			this.no32ndNotesInQuarterNote = (byte)no32ndNotesInQuarterNote;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00011957 File Offset: 0x0000FB57
		[Obsolete("Use the constructor that has absolute time first")]
		public TimeSignatureEvent(int numerator, int denominator, int ticksInMetronomeClick, int no32ndNotesInQuarterNote, long absoluteTime)
			: base(MetaEventType.TimeSignature, 4, absoluteTime)
		{
			this.numerator = (byte)numerator;
			this.denominator = (byte)denominator;
			this.ticksInMetronomeClick = (byte)ticksInMetronomeClick;
			this.no32ndNotesInQuarterNote = (byte)no32ndNotesInQuarterNote;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00011985 File Offset: 0x0000FB85
		public int Numerator
		{
			get
			{
				return (int)this.numerator;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001198D File Offset: 0x0000FB8D
		public int Denominator
		{
			get
			{
				return (int)this.denominator;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00011995 File Offset: 0x0000FB95
		public int TicksInMetronomeClick
		{
			get
			{
				return (int)this.ticksInMetronomeClick;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001199D File Offset: 0x0000FB9D
		public int No32ndNotesInQuarterNote
		{
			get
			{
				return (int)this.no32ndNotesInQuarterNote;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public string TimeSignature
		{
			get
			{
				string text = string.Format("Unknown ({0})", this.denominator);
				switch (this.denominator)
				{
				case 1:
					text = "2";
					break;
				case 2:
					text = "4";
					break;
				case 3:
					text = "8";
					break;
				case 4:
					text = "16";
					break;
				case 5:
					text = "32";
					break;
				}
				return string.Format("{0}/{1}", this.numerator, text);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public override string ToString()
		{
			return string.Format("{0} {1} TicksInClick:{2} 32ndsInQuarterNote:{3}", new object[]
			{
				base.ToString(),
				this.TimeSignature,
				this.ticksInMetronomeClick,
				this.no32ndNotesInQuarterNote
			});
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00011A79 File Offset: 0x0000FC79
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.numerator);
			writer.Write(this.denominator);
			writer.Write(this.ticksInMetronomeClick);
			writer.Write(this.no32ndNotesInQuarterNote);
		}

		// Token: 0x040005D9 RID: 1497
		private byte numerator;

		// Token: 0x040005DA RID: 1498
		private byte denominator;

		// Token: 0x040005DB RID: 1499
		private byte ticksInMetronomeClick;

		// Token: 0x040005DC RID: 1500
		private byte no32ndNotesInQuarterNote;
	}
}
