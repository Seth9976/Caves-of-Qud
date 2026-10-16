using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x020001A2 RID: 418
	[StructLayout(0, Pack = 2)]
	public class WaveFormatExtraData : WaveFormat
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00017EB4 File Offset: 0x000160B4
		public byte[] ExtraData
		{
			get
			{
				return this.extraData;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00017EBC File Offset: 0x000160BC
		internal WaveFormatExtraData()
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00017ED1 File Offset: 0x000160D1
		public WaveFormatExtraData(BinaryReader reader)
			: base(reader)
		{
			this.ReadExtraData(reader);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00017EEE File Offset: 0x000160EE
		internal void ReadExtraData(BinaryReader reader)
		{
			if (this.extraSize > 0)
			{
				reader.Read(this.extraData, 0, (int)this.extraSize);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00017F0D File Offset: 0x0001610D
		public override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			if (this.extraSize > 0)
			{
				writer.Write(this.extraData, 0, (int)this.extraSize);
			}
		}

		// Token: 0x04000A4B RID: 2635
		[MarshalAs(30, SizeConst = 100)]
		private byte[] extraData = new byte[100];
	}
}
