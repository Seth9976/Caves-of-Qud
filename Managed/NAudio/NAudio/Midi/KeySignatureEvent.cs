using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000D1 RID: 209
	public class KeySignatureEvent : MetaEvent
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000F660 File Offset: 0x0000D860
		public KeySignatureEvent(BinaryReader br, int length)
		{
			if (length != 2)
			{
				throw new FormatException("Invalid key signature length");
			}
			this.sharpsFlats = br.ReadByte();
			this.majorMinor = br.ReadByte();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000F68F File Offset: 0x0000D88F
		public KeySignatureEvent(int sharpsFlats, int majorMinor, long absoluteTime)
			: base(MetaEventType.KeySignature, 2, absoluteTime)
		{
			this.sharpsFlats = (byte)sharpsFlats;
			this.majorMinor = (byte)majorMinor;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000F6AB File Offset: 0x0000D8AB
		public int SharpsFlats
		{
			get
			{
				return (int)this.sharpsFlats;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
		public int MajorMinor
		{
			get
			{
				return (int)this.majorMinor;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000F6BB File Offset: 0x0000D8BB
		public override string ToString()
		{
			return string.Format("{0} {1} {2}", base.ToString(), this.sharpsFlats, this.majorMinor);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000F6E3 File Offset: 0x0000D8E3
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.sharpsFlats);
			writer.Write(this.majorMinor);
		}

		// Token: 0x04000547 RID: 1351
		private byte sharpsFlats;

		// Token: 0x04000548 RID: 1352
		private byte majorMinor;
	}
}
