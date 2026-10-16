using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	// Token: 0x020000EC RID: 236
	public class SequencerSpecificEvent : MetaEvent
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00011410 File Offset: 0x0000F610
		public SequencerSpecificEvent(BinaryReader br, int length)
		{
			this.data = br.ReadBytes(length);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00011425 File Offset: 0x0000F625
		public SequencerSpecificEvent(byte[] data, long absoluteTime)
			: base(MetaEventType.SequencerSpecific, data.Length, absoluteTime)
		{
			this.data = data;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001143A File Offset: 0x0000F63A
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00011442 File Offset: 0x0000F642
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
				this.metaDataLength = this.data.Length;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001145C File Offset: 0x0000F65C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(" ");
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			stringBuilder.Length--;
			return stringBuilder.ToString();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000114C7 File Offset: 0x0000F6C7
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.data);
		}

		// Token: 0x040005D0 RID: 1488
		private byte[] data;
	}
}
