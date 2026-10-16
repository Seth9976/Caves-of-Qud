using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000BB RID: 187
	internal class ModulatorBuilder : StructureBuilder<Modulator>
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public override Modulator Read(BinaryReader br)
		{
			Modulator modulator = new Modulator();
			modulator.SourceModulationData = new ModulatorType(br.ReadUInt16());
			modulator.DestinationGenerator = (GeneratorEnum)br.ReadUInt16();
			modulator.Amount = br.ReadInt16();
			modulator.SourceModulationAmount = new ModulatorType(br.ReadUInt16());
			modulator.SourceTransform = (TransformEnum)br.ReadUInt16();
			this.data.Add(modulator);
			return modulator;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		public override void Write(BinaryWriter bw, Modulator o)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public override int Length
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public Modulator[] Modulators
		{
			get
			{
				return this.data.ToArray();
			}
		}
	}
}
