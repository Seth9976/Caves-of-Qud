using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000F2 RID: 242
	public class TrackSequenceNumberEvent : MetaEvent
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		public TrackSequenceNumberEvent(BinaryReader br, int length)
		{
			if (length != 2)
			{
				throw new FormatException("Invalid sequence number length");
			}
			this.sequenceNumber = (ushort)(((int)br.ReadByte() << 8) + (int)br.ReadByte());
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), this.sequenceNumber);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011AFD File Offset: 0x0000FCFD
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)((this.sequenceNumber >> 8) & 255));
			writer.Write((byte)(this.sequenceNumber & 255));
		}

		// Token: 0x040005DD RID: 1501
		private ushort sequenceNumber;
	}
}
