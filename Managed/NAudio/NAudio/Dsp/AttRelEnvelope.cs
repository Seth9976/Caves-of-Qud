using System;

namespace NAudio.Dsp
{
	// Token: 0x0200009C RID: 156
	internal class AttRelEnvelope
	{
		// Token: 0x0600035A RID: 858 RVA: 0x0000B6FD File Offset: 0x000098FD
		public AttRelEnvelope(double attackMilliseconds, double releaseMilliseconds, double sampleRate)
		{
			this.attack = new EnvelopeDetector(attackMilliseconds, sampleRate);
			this.release = new EnvelopeDetector(releaseMilliseconds, sampleRate);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000B71F File Offset: 0x0000991F
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000B72C File Offset: 0x0000992C
		public double Attack
		{
			get
			{
				return this.attack.TimeConstant;
			}
			set
			{
				this.attack.TimeConstant = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000B73A File Offset: 0x0000993A
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000B747 File Offset: 0x00009947
		public double Release
		{
			get
			{
				return this.release.TimeConstant;
			}
			set
			{
				this.release.TimeConstant = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000B755 File Offset: 0x00009955
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000B764 File Offset: 0x00009964
		public double SampleRate
		{
			get
			{
				return this.attack.SampleRate;
			}
			set
			{
				EnvelopeDetector envelopeDetector = this.attack;
				this.release.SampleRate = value;
				envelopeDetector.SampleRate = value;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B78B File Offset: 0x0000998B
		public void Run(double inValue, ref double state)
		{
			if (inValue > state)
			{
				this.attack.Run(inValue, ref state);
				return;
			}
			this.release.Run(inValue, ref state);
		}

		// Token: 0x04000436 RID: 1078
		protected const double DC_OFFSET = 1E-25;

		// Token: 0x04000437 RID: 1079
		private readonly EnvelopeDetector attack;

		// Token: 0x04000438 RID: 1080
		private readonly EnvelopeDetector release;
	}
}
