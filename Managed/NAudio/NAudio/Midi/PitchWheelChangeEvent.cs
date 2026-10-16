using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000EB RID: 235
	public class PitchWheelChangeEvent : MidiEvent
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000112FC File Offset: 0x0000F4FC
		public PitchWheelChangeEvent(BinaryReader br)
		{
			byte b = br.ReadByte();
			byte b2 = br.ReadByte();
			if ((b & 128) != 0)
			{
				throw new FormatException("Invalid pitchwheelchange byte 1");
			}
			if ((b2 & 128) != 0)
			{
				throw new FormatException("Invalid pitchwheelchange byte 2");
			}
			this.pitch = (int)b + ((int)b2 << 7);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00011350 File Offset: 0x0000F550
		public PitchWheelChangeEvent(long absoluteTime, int channel, int pitchWheel)
			: base(absoluteTime, channel, MidiCommandCode.PitchWheelChange)
		{
			this.Pitch = pitchWheel;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00011366 File Offset: 0x0000F566
		public override string ToString()
		{
			return string.Format("{0} Pitch {1} ({2})", base.ToString(), this.pitch, this.pitch - 8192);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00011394 File Offset: 0x0000F594
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001139C File Offset: 0x0000F59C
		public int Pitch
		{
			get
			{
				return this.pitch;
			}
			set
			{
				if (value < 0 || value > 16384)
				{
					throw new ArgumentOutOfRangeException("value", "Pitch value must be in the range 0 - 0x4000");
				}
				this.pitch = value;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000113C1 File Offset: 0x0000F5C1
		public override int GetAsShortMessage()
		{
			return base.GetAsShortMessage() + ((this.pitch & 127) << 8) + (((this.pitch >> 7) & 127) << 16);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)(this.pitch & 127));
			writer.Write((byte)((this.pitch >> 7) & 127));
		}

		// Token: 0x040005CF RID: 1487
		private int pitch;
	}
}
