using System;

namespace NAudio.Dsp
{
	// Token: 0x02000099 RID: 153
	public class BiQuadFilter
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public float Transform(float inSample)
		{
			double num = this.a0 * (double)inSample + this.a1 * (double)this.x1 + this.a2 * (double)this.x2 - this.a3 * (double)this.y1 - this.a4 * (double)this.y2;
			this.x2 = this.x1;
			this.x1 = inSample;
			this.y2 = this.y1;
			this.y1 = (float)num;
			return this.y1;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000AE34 File Offset: 0x00009034
		private void SetCoefficients(double aa0, double aa1, double aa2, double b0, double b1, double b2)
		{
			this.a0 = b0 / aa0;
			this.a1 = b1 / aa0;
			this.a2 = b2 / aa0;
			this.a3 = aa1 / aa0;
			this.a4 = aa2 / aa0;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AE68 File Offset: 0x00009068
		public void SetLowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = 6.283185307179586 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num) / (double)(2f * q);
			double num4 = (1.0 - num2) / 2.0;
			double num5 = 1.0 - num2;
			double num6 = (1.0 - num2) / 2.0;
			double num7 = 1.0 + num3;
			double num8 = -2.0 * num2;
			double num9 = 1.0 - num3;
			this.SetCoefficients(num7, num8, num9, num4, num5, num6);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000AF10 File Offset: 0x00009110
		public void SetPeakingEq(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			double num = 6.283185307179586 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = Math.Pow(10.0, (double)(dbGain / 40f));
			double num6 = 1.0 + num4 * num5;
			double num7 = -2.0 * num2;
			double num8 = 1.0 - num4 * num5;
			double num9 = 1.0 + num4 / num5;
			double num10 = -2.0 * num2;
			double num11 = 1.0 - num4 / num5;
			this.SetCoefficients(num9, num10, num11, num6, num7, num8);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AFCC File Offset: 0x000091CC
		public void SetHighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double num = 6.283185307179586 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num) / (double)(2f * q);
			double num4 = (1.0 + num2) / 2.0;
			double num5 = -(1.0 + num2);
			double num6 = (1.0 + num2) / 2.0;
			double num7 = 1.0 + num3;
			double num8 = -2.0 * num2;
			double num9 = 1.0 - num3;
			this.SetCoefficients(num7, num8, num9, num4, num5, num6);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000B074 File Offset: 0x00009274
		public static BiQuadFilter LowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetLowPassFilter(sampleRate, cutoffFrequency, q);
			return biQuadFilter;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B094 File Offset: 0x00009294
		public static BiQuadFilter HighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetHighPassFilter(sampleRate, cutoffFrequency, q);
			return biQuadFilter;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public static BiQuadFilter BandPassFilterConstantSkirtGain(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.283185307179586 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = num3 / 2.0;
			int num6 = 0;
			double num7 = -num3 / 2.0;
			double num8 = 1.0 + num4;
			double num9 = -2.0 * num2;
			double num10 = 1.0 - num4;
			return new BiQuadFilter(num8, num9, num10, num5, (double)num6, num7);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B144 File Offset: 0x00009344
		public static BiQuadFilter BandPassFilterConstantPeakGain(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.283185307179586 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = num4;
			int num6 = 0;
			double num7 = -num4;
			double num8 = 1.0 + num4;
			double num9 = -2.0 * num2;
			double num10 = 1.0 - num4;
			return new BiQuadFilter(num8, num9, num10, num5, (double)num6, num7);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public static BiQuadFilter NotchFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.283185307179586 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			int num5 = 1;
			double num6 = -2.0 * num2;
			int num7 = 1;
			double num8 = 1.0 + num4;
			double num9 = -2.0 * num2;
			double num10 = 1.0 - num4;
			return new BiQuadFilter(num8, num9, num10, (double)num5, num6, (double)num7);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000B244 File Offset: 0x00009444
		public static BiQuadFilter AllPassFilter(float sampleRate, float centreFrequency, float q)
		{
			double num = 6.283185307179586 * (double)centreFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = num3 / (double)(2f * q);
			double num5 = 1.0 - num4;
			double num6 = -2.0 * num2;
			double num7 = 1.0 + num4;
			double num8 = 1.0 + num4;
			double num9 = -2.0 * num2;
			double num10 = 1.0 - num4;
			return new BiQuadFilter(num8, num9, num10, num5, num6, num7);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000B2DC File Offset: 0x000094DC
		public static BiQuadFilter PeakingEQ(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			BiQuadFilter biQuadFilter = new BiQuadFilter();
			biQuadFilter.SetPeakingEq(sampleRate, centreFrequency, q, dbGain);
			return biQuadFilter;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000B2FC File Offset: 0x000094FC
		public static BiQuadFilter LowShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = 6.283185307179586 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, (double)(dbGain / 40f));
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			double num7 = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 + num6);
			double num8 = 2.0 * num4 * (num4 - 1.0 - (num4 + 1.0) * num2);
			double num9 = num4 * (num4 + 1.0 - (num4 - 1.0) * num2 - num6);
			double num10 = num4 + 1.0 + (num4 - 1.0) * num2 + num6;
			double num11 = -2.0 * (num4 - 1.0 + (num4 + 1.0) * num2);
			double num12 = num4 + 1.0 + (num4 - 1.0) * num2 - num6;
			return new BiQuadFilter(num10, num11, num12, num7, num8, num9);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000B468 File Offset: 0x00009668
		public static BiQuadFilter HighShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double num = 6.283185307179586 * (double)cutoffFrequency / (double)sampleRate;
			double num2 = Math.Cos(num);
			double num3 = Math.Sin(num);
			double num4 = Math.Pow(10.0, (double)(dbGain / 40f));
			double num5 = num3 / 2.0 * Math.Sqrt((num4 + 1.0 / num4) * (double)(1f / shelfSlope - 1f) + 2.0);
			double num6 = 2.0 * Math.Sqrt(num4) * num5;
			double num7 = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 + num6);
			double num8 = -2.0 * num4 * (num4 - 1.0 + (num4 + 1.0) * num2);
			double num9 = num4 * (num4 + 1.0 + (num4 - 1.0) * num2 - num6);
			double num10 = num4 + 1.0 - (num4 - 1.0) * num2 + num6;
			double num11 = 2.0 * (num4 - 1.0 - (num4 + 1.0) * num2);
			double num12 = num4 + 1.0 - (num4 - 1.0) * num2 - num6;
			return new BiQuadFilter(num10, num11, num12, num7, num8, num9);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B5D4 File Offset: 0x000097D4
		private BiQuadFilter()
		{
			this.x1 = (this.x2 = 0f);
			this.y1 = (this.y2 = 0f);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000B610 File Offset: 0x00009810
		private BiQuadFilter(double a0, double a1, double a2, double b0, double b1, double b2)
		{
			this.SetCoefficients(a0, a1, a2, b0, b1, b2);
			this.x1 = (this.x2 = 0f);
			this.y1 = (this.y2 = 0f);
		}

		// Token: 0x04000428 RID: 1064
		private double a0;

		// Token: 0x04000429 RID: 1065
		private double a1;

		// Token: 0x0400042A RID: 1066
		private double a2;

		// Token: 0x0400042B RID: 1067
		private double a3;

		// Token: 0x0400042C RID: 1068
		private double a4;

		// Token: 0x0400042D RID: 1069
		private float x1;

		// Token: 0x0400042E RID: 1070
		private float x2;

		// Token: 0x0400042F RID: 1071
		private float y1;

		// Token: 0x04000430 RID: 1072
		private float y2;
	}
}
