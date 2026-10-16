using System;

namespace NAudio.Dsp
{
	// Token: 0x0200009B RID: 155
	internal class EnvelopeDetector
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000B65A File Offset: 0x0000985A
		public EnvelopeDetector()
			: this(1.0, 44100.0)
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B674 File Offset: 0x00009874
		public EnvelopeDetector(double ms, double sampleRate)
		{
			this.sampleRate = sampleRate;
			this.ms = ms;
			this.SetCoef();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000B690 File Offset: 0x00009890
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000B698 File Offset: 0x00009898
		public double TimeConstant
		{
			get
			{
				return this.ms;
			}
			set
			{
				this.ms = value;
				this.SetCoef();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000B6A7 File Offset: 0x000098A7
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000B6AF File Offset: 0x000098AF
		public double SampleRate
		{
			get
			{
				return this.sampleRate;
			}
			set
			{
				this.sampleRate = value;
				this.SetCoef();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B6BE File Offset: 0x000098BE
		public void Run(double inValue, ref double state)
		{
			state = inValue + this.coeff * (state - inValue);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B6CF File Offset: 0x000098CF
		private void SetCoef()
		{
			this.coeff = Math.Exp(-1.0 / (0.001 * this.ms * this.sampleRate));
		}

		// Token: 0x04000433 RID: 1075
		private double sampleRate;

		// Token: 0x04000434 RID: 1076
		private double ms;

		// Token: 0x04000435 RID: 1077
		private double coeff;
	}
}
