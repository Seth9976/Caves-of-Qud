using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000B2 RID: 178
	public class Generator
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public GeneratorEnum GeneratorType
		{
			get
			{
				return this.generatorType;
			}
			set
			{
				this.generatorType = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000D5DD File Offset: 0x0000B7DD
		public ushort UInt16Amount
		{
			get
			{
				return this.rawAmount;
			}
			set
			{
				this.rawAmount = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D5E6 File Offset: 0x0000B7E6
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000D5EF File Offset: 0x0000B7EF
		public short Int16Amount
		{
			get
			{
				return (short)this.rawAmount;
			}
			set
			{
				this.rawAmount = (ushort)value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000D608 File Offset: 0x0000B808
		public byte LowByteAmount
		{
			get
			{
				return (byte)(this.rawAmount & 255);
			}
			set
			{
				this.rawAmount &= 65280;
				this.rawAmount += (ushort)value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D62C File Offset: 0x0000B82C
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000D63D File Offset: 0x0000B83D
		public byte HighByteAmount
		{
			get
			{
				return (byte)((this.rawAmount & 65280) >> 8);
			}
			set
			{
				this.rawAmount &= 255;
				this.rawAmount += (ushort)(value << 8);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000D664 File Offset: 0x0000B864
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000D66C File Offset: 0x0000B86C
		public Instrument Instrument
		{
			get
			{
				return this.instrument;
			}
			set
			{
				this.instrument = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000D675 File Offset: 0x0000B875
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000D67D File Offset: 0x0000B87D
		public SampleHeader SampleHeader
		{
			get
			{
				return this.sampleHeader;
			}
			set
			{
				this.sampleHeader = value;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D688 File Offset: 0x0000B888
		public override string ToString()
		{
			if (this.generatorType == GeneratorEnum.Instrument)
			{
				return string.Format("Generator Instrument {0}", this.instrument.Name);
			}
			if (this.generatorType == GeneratorEnum.SampleID)
			{
				return string.Format("Generator SampleID {0}", this.sampleHeader);
			}
			return string.Format("Generator {0} {1}", this.generatorType, this.rawAmount);
		}

		// Token: 0x04000494 RID: 1172
		private GeneratorEnum generatorType;

		// Token: 0x04000495 RID: 1173
		private ushort rawAmount;

		// Token: 0x04000496 RID: 1174
		private Instrument instrument;

		// Token: 0x04000497 RID: 1175
		private SampleHeader sampleHeader;
	}
}
