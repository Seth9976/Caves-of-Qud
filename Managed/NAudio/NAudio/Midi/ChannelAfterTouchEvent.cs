using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000CE RID: 206
	public class ChannelAfterTouchEvent : MidiEvent
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000F26D File Offset: 0x0000D46D
		public ChannelAfterTouchEvent(BinaryReader br)
		{
			this.afterTouchPressure = br.ReadByte();
			if ((this.afterTouchPressure & 128) != 0)
			{
				throw new FormatException("Invalid afterTouchPressure");
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000F29A File Offset: 0x0000D49A
		public ChannelAfterTouchEvent(long absoluteTime, int channel, int afterTouchPressure)
			: base(absoluteTime, channel, MidiCommandCode.ChannelAfterTouch)
		{
			this.AfterTouchPressure = afterTouchPressure;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.afterTouchPressure);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000F2C6 File Offset: 0x0000D4C6
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000F2CE File Offset: 0x0000D4CE
		public int AfterTouchPressure
		{
			get
			{
				return (int)this.afterTouchPressure;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "After touch pressure must be in the range 0-127");
				}
				this.afterTouchPressure = (byte)value;
			}
		}

		// Token: 0x04000541 RID: 1345
		private byte afterTouchPressure;
	}
}
