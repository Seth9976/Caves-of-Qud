using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000B4 RID: 180
	internal class GeneratorBuilder : StructureBuilder<Generator>
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0000D720 File Offset: 0x0000B920
		public override Generator Read(BinaryReader br)
		{
			Generator generator = new Generator();
			generator.GeneratorType = (GeneratorEnum)br.ReadUInt16();
			generator.UInt16Amount = br.ReadUInt16();
			this.data.Add(generator);
			return generator;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D758 File Offset: 0x0000B958
		public override void Write(BinaryWriter bw, Generator o)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000D75A File Offset: 0x0000B95A
		public override int Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000D75D File Offset: 0x0000B95D
		public Generator[] Generators
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D76C File Offset: 0x0000B96C
		public void Load(Instrument[] instruments)
		{
			foreach (Generator generator in this.Generators)
			{
				if (generator.GeneratorType == GeneratorEnum.Instrument)
				{
					generator.Instrument = instruments[(int)generator.UInt16Amount];
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public void Load(SampleHeader[] sampleHeaders)
		{
			foreach (Generator generator in this.Generators)
			{
				if (generator.GeneratorType == GeneratorEnum.SampleID)
				{
					generator.SampleHeader = sampleHeaders[(int)generator.UInt16Amount];
				}
			}
		}
	}
}
