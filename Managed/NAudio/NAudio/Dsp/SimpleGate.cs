using System;
using NAudio.Utils;

namespace NAudio.Dsp
{
	// Token: 0x020000A0 RID: 160
	internal class SimpleGate : AttRelEnvelope
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000BD74 File Offset: 0x00009F74
		public SimpleGate()
			: base(10.0, 10.0, 44100.0)
		{
			this.threshdB = 0.0;
			this.thresh = 1.0;
			this.env = 1E-25;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		public void Process(ref double in1, ref double in2)
		{
			double num = Math.Abs(in1);
			double num2 = Math.Abs(in2);
			double num3 = Math.Max(num, num2);
			double num4 = ((num3 > this.thresh) ? 1.0 : 0.0);
			num4 += 1E-25;
			base.Run(num4, ref this.env);
			num4 = this.env - 1E-25;
			in1 *= num4;
			in2 *= num4;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BE49 File Offset: 0x0000A049
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000BE51 File Offset: 0x0000A051
		public double Threshold
		{
			get
			{
				return this.threshdB;
			}
			set
			{
				this.threshdB = value;
				this.thresh = Decibels.DecibelsToLinear(value);
			}
		}

		// Token: 0x0400043D RID: 1085
		private double threshdB;

		// Token: 0x0400043E RID: 1086
		private double thresh;

		// Token: 0x0400043F RID: 1087
		private double env;
	}
}
