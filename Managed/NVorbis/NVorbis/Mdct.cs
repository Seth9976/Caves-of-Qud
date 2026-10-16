using System;
using System.Collections.Generic;
using System.Threading;

namespace NVorbis
{
	// Token: 0x02000008 RID: 8
	internal class Mdct
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000254E File Offset: 0x0000074E
		public static void Reverse(float[] samples, int sampleCount)
		{
			Mdct.GetSetup(sampleCount).CalcReverse(samples);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000255C File Offset: 0x0000075C
		private static Mdct GetSetup(int n)
		{
			Mdct mdct;
			lock (Mdct._setupCache)
			{
				if (!Mdct._setupCache.ContainsKey(n))
				{
					Mdct._setupCache[n] = new Mdct(n);
				}
				mdct = Mdct._setupCache[n];
			}
			return mdct;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025BC File Offset: 0x000007BC
		private Mdct(int n)
		{
			this._n = n;
			this._n2 = n >> 1;
			this._n4 = this._n2 >> 1;
			this._n8 = this._n4 >> 1;
			this._ld = Utils.ilog(n) - 1;
			this._A = new float[this._n2];
			this._B = new float[this._n2];
			this._C = new float[this._n4];
			int i;
			int num = (i = 0);
			while (i < this._n4)
			{
				this._A[num] = (float)Math.Cos((double)((float)(4 * i) * 3.1415927f / (float)n));
				this._A[num + 1] = (float)(-(float)Math.Sin((double)((float)(4 * i) * 3.1415927f / (float)n)));
				this._B[num] = (float)Math.Cos((double)((float)(num + 1) * 3.1415927f / (float)n / 2f)) * 0.5f;
				this._B[num + 1] = (float)Math.Sin((double)((float)(num + 1) * 3.1415927f / (float)n / 2f)) * 0.5f;
				i++;
				num += 2;
			}
			num = (i = 0);
			while (i < this._n8)
			{
				this._C[num] = (float)Math.Cos((double)((float)(2 * (num + 1)) * 3.1415927f / (float)n));
				this._C[num + 1] = (float)(-(float)Math.Sin((double)((float)(2 * (num + 1)) * 3.1415927f / (float)n)));
				i++;
				num += 2;
			}
			this._bitrev = new ushort[this._n8];
			for (int j = 0; j < this._n8; j++)
			{
				this._bitrev[j] = (ushort)(Utils.BitReverse((uint)j, this._ld - 3) << 2);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002784 File Offset: 0x00000984
		private float[] GetBuffer()
		{
			float[] array2;
			lock (this._threadLocalBuffers)
			{
				float[] array;
				if (!this._threadLocalBuffers.TryGetValue(Thread.CurrentThread.ManagedThreadId, ref array))
				{
					array = (this._threadLocalBuffers[Thread.CurrentThread.ManagedThreadId] = new float[this._n2]);
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027F8 File Offset: 0x000009F8
		private void CalcReverse(float[] buffer)
		{
			float[] buffer2 = this.GetBuffer();
			int i = this._n2 - 2;
			int num = 0;
			int num2 = 0;
			int n = this._n2;
			while (num2 != n)
			{
				buffer2[i + 1] = buffer[num2] * this._A[num] - buffer[num2 + 2] * this._A[num + 1];
				buffer2[i] = buffer[num2] * this._A[num + 1] + buffer[num2 + 2] * this._A[num];
				i -= 2;
				num += 2;
				num2 += 4;
			}
			num2 = this._n2 - 3;
			while (i >= 0)
			{
				buffer2[i + 1] = -buffer[num2 + 2] * this._A[num] - -buffer[num2] * this._A[num + 1];
				buffer2[i] = -buffer[num2 + 2] * this._A[num + 1] + -buffer[num2] * this._A[num];
				i -= 2;
				num += 2;
				num2 -= 4;
			}
			float[] array = buffer2;
			int j = this._n2 - 8;
			int num3 = this._n4;
			int num4 = 0;
			int num5 = this._n4;
			int num6 = 0;
			while (j >= 0)
			{
				float num7 = array[num3 + 1] - array[num4 + 1];
				float num8 = array[num3] - array[num4];
				buffer[num5 + 1] = array[num3 + 1] + array[num4 + 1];
				buffer[num5] = array[num3] + array[num4];
				buffer[num6 + 1] = num7 * this._A[j + 4] - num8 * this._A[j + 5];
				buffer[num6] = num8 * this._A[j + 4] + num7 * this._A[j + 5];
				num7 = array[num3 + 3] - array[num4 + 3];
				num8 = array[num3 + 2] - array[num4 + 2];
				buffer[num5 + 3] = array[num3 + 3] + array[num4 + 3];
				buffer[num5 + 2] = array[num3 + 2] + array[num4 + 2];
				buffer[num6 + 3] = num7 * this._A[j] - num8 * this._A[j + 1];
				buffer[num6 + 2] = num8 * this._A[j] + num7 * this._A[j + 1];
				j -= 8;
				num5 += 4;
				num6 += 4;
				num3 += 4;
				num4 += 4;
			}
			this.step3_iter0_loop(this._n >> 4, buffer, this._n2 - 1, -this._n8);
			this.step3_iter0_loop(this._n >> 4, buffer, this._n2 - 1 - this._n4, -this._n8);
			this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1, -(this._n >> 4), 16);
			this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8, -(this._n >> 4), 16);
			this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8 * 2, -(this._n >> 4), 16);
			this.step3_inner_r_loop(this._n >> 5, buffer, this._n2 - 1 - this._n8 * 3, -(this._n >> 4), 16);
			int k;
			for (k = 2; k < this._ld - 3 >> 1; k++)
			{
				int num9 = this._n >> k + 2;
				int num10 = num9 >> 1;
				int num11 = 1 << k + 1;
				for (int l = 0; l < num11; l++)
				{
					this.step3_inner_r_loop(this._n >> k + 4, buffer, this._n2 - 1 - num9 * l, -num10, 1 << k + 3);
				}
			}
			while (k < this._ld - 6)
			{
				int num12 = this._n >> k + 2;
				int num13 = 1 << k + 3;
				int num14 = num12 >> 1;
				int num15 = this._n >> k + 6;
				int num16 = 1 << k + 1;
				int num17 = this._n2 - 1;
				int num18 = 0;
				for (int m = num15; m > 0; m--)
				{
					this.step3_inner_s_loop(num16, buffer, num17, -num14, num18, num13, num12);
					num18 += num13 * 4;
					num17 -= 8;
				}
				k++;
			}
			this.step3_inner_s_loop_ld654(this._n >> 5, buffer, this._n2 - 1, this._n);
			int num19 = 0;
			int num20 = this._n4 - 4;
			int num21 = this._n2 - 4;
			while (num20 >= 0)
			{
				int num22 = (int)this._bitrev[num19];
				array[num21 + 3] = buffer[num22];
				array[num21 + 2] = buffer[num22 + 1];
				array[num20 + 3] = buffer[num22 + 2];
				array[num20 + 2] = buffer[num22 + 3];
				num22 = (int)this._bitrev[num19 + 1];
				array[num21 + 1] = buffer[num22];
				array[num21] = buffer[num22 + 1];
				array[num20 + 1] = buffer[num22 + 2];
				array[num20] = buffer[num22 + 3];
				num20 -= 4;
				num21 -= 4;
				num19 += 2;
			}
			int num23 = 0;
			int num24 = 0;
			int num25 = this._n2 - 4;
			while (num24 < num25)
			{
				float num26 = array[num24] - array[num25 + 2];
				float num27 = array[num24 + 1] + array[num25 + 3];
				float num28 = this._C[num23 + 1] * num26 + this._C[num23] * num27;
				float num29 = this._C[num23 + 1] * num27 - this._C[num23] * num26;
				float num30 = array[num24] + array[num25 + 2];
				float num31 = array[num24 + 1] - array[num25 + 3];
				array[num24] = num30 + num28;
				array[num24 + 1] = num31 + num29;
				array[num25 + 2] = num30 - num28;
				array[num25 + 3] = num29 - num31;
				num26 = array[num24 + 2] - array[num25];
				num27 = array[num24 + 3] + array[num25 + 1];
				num28 = this._C[num23 + 3] * num26 + this._C[num23 + 2] * num27;
				num29 = this._C[num23 + 3] * num27 - this._C[num23 + 2] * num26;
				num30 = array[num24 + 2] + array[num25];
				num31 = array[num24 + 3] - array[num25 + 1];
				array[num24 + 2] = num30 + num28;
				array[num24 + 3] = num31 + num29;
				array[num25] = num30 - num28;
				array[num25 + 1] = num29 - num31;
				num23 += 4;
				num24 += 4;
				num25 -= 4;
			}
			int num32 = this._n2 - 8;
			int num33 = this._n2 - 8;
			int num34 = 0;
			int num35 = this._n2 - 4;
			int num36 = this._n2;
			int num37 = this._n - 4;
			while (num33 >= 0)
			{
				float num38 = buffer2[num33 + 6] * this._B[num32 + 7] - buffer2[num33 + 7] * this._B[num32 + 6];
				float num39 = -buffer2[num33 + 6] * this._B[num32 + 6] - buffer2[num33 + 7] * this._B[num32 + 7];
				buffer[num34] = num38;
				buffer[num35 + 3] = -num38;
				buffer[num36] = num39;
				buffer[num37 + 3] = num39;
				float num40 = buffer2[num33 + 4] * this._B[num32 + 5] - buffer2[num33 + 5] * this._B[num32 + 4];
				float num41 = -buffer2[num33 + 4] * this._B[num32 + 4] - buffer2[num33 + 5] * this._B[num32 + 5];
				buffer[num34 + 1] = num40;
				buffer[num35 + 2] = -num40;
				buffer[num36 + 1] = num41;
				buffer[num37 + 2] = num41;
				num38 = buffer2[num33 + 2] * this._B[num32 + 3] - buffer2[num33 + 3] * this._B[num32 + 2];
				num39 = -buffer2[num33 + 2] * this._B[num32 + 2] - buffer2[num33 + 3] * this._B[num32 + 3];
				buffer[num34 + 2] = num38;
				buffer[num35 + 1] = -num38;
				buffer[num36 + 2] = num39;
				buffer[num37 + 1] = num39;
				num40 = buffer2[num33] * this._B[num32 + 1] - buffer2[num33 + 1] * this._B[num32];
				num41 = -buffer2[num33] * this._B[num32] - buffer2[num33 + 1] * this._B[num32 + 1];
				buffer[num34 + 3] = num40;
				buffer[num35] = -num40;
				buffer[num36 + 3] = num41;
				buffer[num37] = num41;
				num32 -= 8;
				num33 -= 8;
				num34 += 4;
				num36 += 4;
				num35 -= 4;
				num37 -= 4;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003080 File Offset: 0x00001280
		private void step3_iter0_loop(int n, float[] e, int i_off, int k_off)
		{
			int num = i_off;
			int num2 = num + k_off;
			int num3 = 0;
			for (int i = n >> 2; i > 0; i--)
			{
				float num4 = e[num] - e[num2];
				float num5 = e[num - 1] - e[num2 - 1];
				e[num] += e[num2];
				e[num - 1] += e[num2 - 1];
				e[num2] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 1] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += 8;
				num4 = e[num - 2] - e[num2 - 2];
				num5 = e[num - 3] - e[num2 - 3];
				e[num - 2] += e[num2 - 2];
				e[num - 3] += e[num2 - 3];
				e[num2 - 2] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 3] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += 8;
				num4 = e[num - 4] - e[num2 - 4];
				num5 = e[num - 5] - e[num2 - 5];
				e[num - 4] += e[num2 - 4];
				e[num - 5] += e[num2 - 5];
				e[num2 - 4] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 5] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += 8;
				num4 = e[num - 6] - e[num2 - 6];
				num5 = e[num - 7] - e[num2 - 7];
				e[num - 6] += e[num2 - 6];
				e[num - 7] += e[num2 - 7];
				e[num2 - 6] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 7] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += 8;
				num -= 8;
				num2 -= 8;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000032E0 File Offset: 0x000014E0
		private void step3_inner_r_loop(int lim, float[] e, int d0, int k_off, int k1)
		{
			int num = d0;
			int num2 = num + k_off;
			int num3 = 0;
			for (int i = lim >> 2; i > 0; i--)
			{
				float num4 = e[num] - e[num2];
				float num5 = e[num - 1] - e[num2 - 1];
				e[num] += e[num2];
				e[num - 1] += e[num2 - 1];
				e[num2] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 1] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += k1;
				num4 = e[num - 2] - e[num2 - 2];
				num5 = e[num - 3] - e[num2 - 3];
				e[num - 2] += e[num2 - 2];
				e[num - 3] += e[num2 - 3];
				e[num2 - 2] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 3] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += k1;
				num4 = e[num - 4] - e[num2 - 4];
				num5 = e[num - 5] - e[num2 - 5];
				e[num - 4] += e[num2 - 4];
				e[num - 5] += e[num2 - 5];
				e[num2 - 4] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 5] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += k1;
				num4 = e[num - 6] - e[num2 - 6];
				num5 = e[num - 7] - e[num2 - 7];
				e[num - 6] += e[num2 - 6];
				e[num - 7] += e[num2 - 7];
				e[num2 - 6] = num4 * this._A[num3] - num5 * this._A[num3 + 1];
				e[num2 - 7] = num5 * this._A[num3] + num4 * this._A[num3 + 1];
				num3 += k1;
				num -= 8;
				num2 -= 8;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000354C File Offset: 0x0000174C
		private void step3_inner_s_loop(int n, float[] e, int i_off, int k_off, int a, int a_off, int k0)
		{
			float num = this._A[a];
			float num2 = this._A[a + 1];
			float num3 = this._A[a + a_off];
			float num4 = this._A[a + a_off + 1];
			float num5 = this._A[a + a_off * 2];
			float num6 = this._A[a + a_off * 2 + 1];
			float num7 = this._A[a + a_off * 3];
			float num8 = this._A[a + a_off * 3 + 1];
			int num9 = i_off;
			int num10 = num9 + k_off;
			for (int i = n; i > 0; i--)
			{
				float num11 = e[num9] - e[num10];
				float num12 = e[num9 - 1] - e[num10 - 1];
				e[num9] += e[num10];
				e[num9 - 1] += e[num10 - 1];
				e[num10] = num11 * num - num12 * num2;
				e[num10 - 1] = num12 * num + num11 * num2;
				num11 = e[num9 - 2] - e[num10 - 2];
				num12 = e[num9 - 3] - e[num10 - 3];
				e[num9 - 2] += e[num10 - 2];
				e[num9 - 3] += e[num10 - 3];
				e[num10 - 2] = num11 * num3 - num12 * num4;
				e[num10 - 3] = num12 * num3 + num11 * num4;
				num11 = e[num9 - 4] - e[num10 - 4];
				num12 = e[num9 - 5] - e[num10 - 5];
				e[num9 - 4] += e[num10 - 4];
				e[num9 - 5] += e[num10 - 5];
				e[num10 - 4] = num11 * num5 - num12 * num6;
				e[num10 - 5] = num12 * num5 + num11 * num6;
				num11 = e[num9 - 6] - e[num10 - 6];
				num12 = e[num9 - 7] - e[num10 - 7];
				e[num9 - 6] += e[num10 - 6];
				e[num9 - 7] += e[num10 - 7];
				e[num10 - 6] = num11 * num7 - num12 * num8;
				e[num10 - 7] = num12 * num7 + num11 * num8;
				num9 -= k0;
				num10 -= k0;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000037CC File Offset: 0x000019CC
		private void step3_inner_s_loop_ld654(int n, float[] e, int i_off, int base_n)
		{
			int num = base_n >> 3;
			float num2 = this._A[num];
			int i = i_off;
			int num3 = i - 16 * n;
			while (i > num3)
			{
				float num4 = e[i] - e[i - 8];
				float num5 = e[i - 1] - e[i - 9];
				e[i] += e[i - 8];
				e[i - 1] += e[i - 9];
				e[i - 8] = num4;
				e[i - 9] = num5;
				num4 = e[i - 2] - e[i - 10];
				num5 = e[i - 3] - e[i - 11];
				e[i - 2] += e[i - 10];
				e[i - 3] += e[i - 11];
				e[i - 10] = (num4 + num5) * num2;
				e[i - 11] = (num5 - num4) * num2;
				num4 = e[i - 12] - e[i - 4];
				num5 = e[i - 5] - e[i - 13];
				e[i - 4] += e[i - 12];
				e[i - 5] += e[i - 13];
				e[i - 12] = num5;
				e[i - 13] = num4;
				num4 = e[i - 14] - e[i - 6];
				num5 = e[i - 7] - e[i - 15];
				e[i - 6] += e[i - 14];
				e[i - 7] += e[i - 15];
				e[i - 14] = (num4 + num5) * num2;
				e[i - 15] = (num4 - num5) * num2;
				this.iter_54(e, i);
				this.iter_54(e, i - 8);
				i -= 16;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000039A8 File Offset: 0x00001BA8
		private void iter_54(float[] e, int z)
		{
			float num = e[z] - e[z - 4];
			float num2 = e[z] + e[z - 4];
			float num3 = e[z - 2] + e[z - 6];
			float num4 = e[z - 2] - e[z - 6];
			e[z] = num2 + num3;
			e[z - 2] = num2 - num3;
			float num5 = e[z - 3] - e[z - 7];
			e[z - 4] = num + num5;
			e[z - 6] = num - num5;
			float num6 = e[z - 1] - e[z - 5];
			float num7 = e[z - 1] + e[z - 5];
			float num8 = e[z - 3] + e[z - 7];
			e[z - 1] = num7 + num8;
			e[z - 3] = num7 - num8;
			e[z - 5] = num6 - num4;
			e[z - 7] = num6 + num4;
		}

		// Token: 0x04000010 RID: 16
		private const float M_PI = 3.1415927f;

		// Token: 0x04000011 RID: 17
		private static Dictionary<int, Mdct> _setupCache = new Dictionary<int, Mdct>(2);

		// Token: 0x04000012 RID: 18
		private int _n;

		// Token: 0x04000013 RID: 19
		private int _n2;

		// Token: 0x04000014 RID: 20
		private int _n4;

		// Token: 0x04000015 RID: 21
		private int _n8;

		// Token: 0x04000016 RID: 22
		private int _ld;

		// Token: 0x04000017 RID: 23
		private float[] _A;

		// Token: 0x04000018 RID: 24
		private float[] _B;

		// Token: 0x04000019 RID: 25
		private float[] _C;

		// Token: 0x0400001A RID: 26
		private ushort[] _bitrev;

		// Token: 0x0400001B RID: 27
		private Dictionary<int, float[]> _threadLocalBuffers = new Dictionary<int, float[]>(1);
	}
}
