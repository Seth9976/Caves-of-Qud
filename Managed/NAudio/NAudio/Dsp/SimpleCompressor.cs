using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
	// Token: 0x0200009F RID: 159
	internal class SimpleCompressor : AttRelEnvelope
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public SimpleCompressor(double attackTime, double releaseTime, double sampleRate)
			: base(attackTime, releaseTime, sampleRate)
		{
			this.Threshold = 0.0;
			this.Ratio = 1.0;
			this.MakeUpGain = 0.0;
			this.envdB = 1E-25;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000BC04 File Offset: 0x00009E04
		public SimpleCompressor()
			: base(10.0, 10.0, 44100.0)
		{
			this.Threshold = 0.0;
			this.Ratio = 1.0;
			this.MakeUpGain = 0.0;
			this.envdB = 1E-25;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000BC6E File Offset: 0x00009E6E
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000BC76 File Offset: 0x00009E76
		public double MakeUpGain { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000BC7F File Offset: 0x00009E7F
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000BC87 File Offset: 0x00009E87
		public double Threshold { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000BC90 File Offset: 0x00009E90
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000BC98 File Offset: 0x00009E98
		public double Ratio { get; set; }

		// Token: 0x06000371 RID: 881 RVA: 0x0000BCA1 File Offset: 0x00009EA1
		public void InitRuntime()
		{
			this.envdB = 1E-25;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public void Process(ref double in1, ref double in2)
		{
			double num = Math.Abs(in1);
			double num2 = Math.Abs(in2);
			double num3 = Math.Max(num, num2);
			num3 += 1E-25;
			double num4 = Decibels.LinearToDecibels(num3);
			double num5 = num4 - this.Threshold;
			if (num5 < 0.0)
			{
				num5 = 0.0;
			}
			num5 += 1E-25;
			base.Run(num5, ref this.envdB);
			num5 = this.envdB - 1E-25;
			double num6 = num5 * (this.Ratio - 1.0);
			num6 = Decibels.DecibelsToLinear(num6) * Decibels.DecibelsToLinear(this.MakeUpGain);
			in1 *= num6;
			in2 *= num6;
		}

		// Token: 0x04000439 RID: 1081
		private double envdB;
	}
}
