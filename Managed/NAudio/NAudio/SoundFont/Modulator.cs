using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000BA RID: 186
	public class Modulator
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public ModulatorType SourceModulationData
		{
			get
			{
				return this.sourceModulationData;
			}
			set
			{
				this.sourceModulationData = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000DCC5 File Offset: 0x0000BEC5
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000DCCD File Offset: 0x0000BECD
		public GeneratorEnum DestinationGenerator
		{
			get
			{
				return this.destinationGenerator;
			}
			set
			{
				this.destinationGenerator = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		public short Amount
		{
			get
			{
				return this.amount;
			}
			set
			{
				this.amount = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		public ModulatorType SourceModulationAmount
		{
			get
			{
				return this.sourceModulationAmount;
			}
			set
			{
				this.sourceModulationAmount = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public TransformEnum SourceTransform
		{
			get
			{
				return this.sourceTransform;
			}
			set
			{
				this.sourceTransform = value;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public override string ToString()
		{
			return string.Format("Modulator {0} {1} {2} {3} {4}", new object[] { this.sourceModulationData, this.destinationGenerator, this.amount, this.sourceModulationAmount, this.sourceTransform });
		}

		// Token: 0x040004E9 RID: 1257
		private ModulatorType sourceModulationData;

		// Token: 0x040004EA RID: 1258
		private GeneratorEnum destinationGenerator;

		// Token: 0x040004EB RID: 1259
		private short amount;

		// Token: 0x040004EC RID: 1260
		private ModulatorType sourceModulationAmount;

		// Token: 0x040004ED RID: 1261
		private TransformEnum sourceTransform;
	}
}
