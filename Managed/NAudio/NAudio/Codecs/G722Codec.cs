using System;

namespace NAudio.Codecs
{
	// Token: 0x02000004 RID: 4
	public class G722Codec
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000238C File Offset: 0x0000058C
		private static short Saturate(int amp)
		{
			short num = (short)amp;
			if (amp == (int)num)
			{
				return num;
			}
			if (amp > 32767)
			{
				return short.MaxValue;
			}
			return short.MinValue;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023B8 File Offset: 0x000005B8
		private static void Block4(G722CodecState s, int band, int d)
		{
			s.Band[band].d[0] = d;
			s.Band[band].r[0] = (int)G722Codec.Saturate(s.Band[band].s + d);
			s.Band[band].p[0] = (int)G722Codec.Saturate(s.Band[band].sz + d);
			for (int i = 0; i < 3; i++)
			{
				s.Band[band].sg[i] = s.Band[band].p[i] >> 15;
			}
			int num = (int)G722Codec.Saturate(s.Band[band].a[1] << 2);
			int num2 = ((s.Band[band].sg[0] == s.Band[band].sg[1]) ? (-num) : num);
			if (num2 > 32767)
			{
				num2 = 32767;
			}
			int num3 = ((s.Band[band].sg[0] == s.Band[band].sg[2]) ? 128 : (-128));
			num3 += num2 >> 7;
			num3 += s.Band[band].a[2] * 32512 >> 15;
			if (num3 > 12288)
			{
				num3 = 12288;
			}
			else if (num3 < -12288)
			{
				num3 = -12288;
			}
			s.Band[band].ap[2] = num3;
			s.Band[band].sg[0] = s.Band[band].p[0] >> 15;
			s.Band[band].sg[1] = s.Band[band].p[1] >> 15;
			num = ((s.Band[band].sg[0] == s.Band[band].sg[1]) ? 192 : (-192));
			num2 = s.Band[band].a[1] * 32640 >> 15;
			s.Band[band].ap[1] = (int)G722Codec.Saturate(num + num2);
			num3 = (int)G722Codec.Saturate(15360 - s.Band[band].ap[2]);
			if (s.Band[band].ap[1] > num3)
			{
				s.Band[band].ap[1] = num3;
			}
			else if (s.Band[band].ap[1] < -num3)
			{
				s.Band[band].ap[1] = -num3;
			}
			num = ((d == 0) ? 0 : 128);
			s.Band[band].sg[0] = d >> 15;
			for (int i = 1; i < 7; i++)
			{
				s.Band[band].sg[i] = s.Band[band].d[i] >> 15;
				num2 = ((s.Band[band].sg[i] == s.Band[band].sg[0]) ? num : (-num));
				num3 = s.Band[band].b[i] * 32640 >> 15;
				s.Band[band].bp[i] = (int)G722Codec.Saturate(num2 + num3);
			}
			for (int i = 6; i > 0; i--)
			{
				s.Band[band].d[i] = s.Band[band].d[i - 1];
				s.Band[band].b[i] = s.Band[band].bp[i];
			}
			for (int i = 2; i > 0; i--)
			{
				s.Band[band].r[i] = s.Band[band].r[i - 1];
				s.Band[band].p[i] = s.Band[band].p[i - 1];
				s.Band[band].a[i] = s.Band[band].ap[i];
			}
			num = (int)G722Codec.Saturate(s.Band[band].r[1] + s.Band[band].r[1]);
			num = s.Band[band].a[1] * num >> 15;
			num2 = (int)G722Codec.Saturate(s.Band[band].r[2] + s.Band[band].r[2]);
			num2 = s.Band[band].a[2] * num2 >> 15;
			s.Band[band].sp = (int)G722Codec.Saturate(num + num2);
			s.Band[band].sz = 0;
			for (int i = 6; i > 0; i--)
			{
				num = (int)G722Codec.Saturate(s.Band[band].d[i] + s.Band[band].d[i]);
				s.Band[band].sz += s.Band[band].b[i] * num >> 15;
			}
			s.Band[band].sz = (int)G722Codec.Saturate(s.Band[band].sz);
			s.Band[band].s = (int)G722Codec.Saturate(s.Band[band].sp + s.Band[band].sz);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000028A0 File Offset: 0x00000AA0
		public int Decode(G722CodecState state, short[] outputBuffer, byte[] inputG722Data, int inputLength)
		{
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < inputLength)
			{
				int num3;
				if (state.Packed)
				{
					if (state.InBits < state.BitsPerSample)
					{
						state.InBuffer |= (uint)((uint)inputG722Data[i++] << state.InBits);
						state.InBits += 8;
					}
					num3 = (int)(state.InBuffer & ((1U << state.BitsPerSample) - 1U));
					state.InBuffer >>= state.BitsPerSample;
					state.InBits -= state.BitsPerSample;
				}
				else
				{
					num3 = (int)inputG722Data[i++];
				}
				int num4;
				int num5;
				int num6;
				switch (state.BitsPerSample)
				{
				case 6:
					num4 = num3 & 15;
					num5 = (num3 >> 4) & 3;
					num6 = G722Codec.qm4[num4];
					break;
				case 7:
					num4 = num3 & 31;
					num5 = (num3 >> 5) & 3;
					num6 = G722Codec.qm5[num4];
					num4 >>= 1;
					break;
				default:
					num4 = num3 & 63;
					num5 = (num3 >> 6) & 3;
					num6 = G722Codec.qm6[num4];
					num4 >>= 2;
					break;
				}
				num6 = state.Band[0].det * num6 >> 15;
				int num7 = state.Band[0].s + num6;
				if (num7 > 16383)
				{
					num7 = 16383;
				}
				else if (num7 < -16384)
				{
					num7 = -16384;
				}
				num6 = G722Codec.qm4[num4];
				int num8 = state.Band[0].det * num6 >> 15;
				num6 = G722Codec.rl42[num4];
				num4 = state.Band[0].nb * 127 >> 7;
				num4 += G722Codec.wl[num6];
				if (num4 < 0)
				{
					num4 = 0;
				}
				else if (num4 > 18432)
				{
					num4 = 18432;
				}
				state.Band[0].nb = num4;
				num4 = (state.Band[0].nb >> 6) & 31;
				num6 = 8 - (state.Band[0].nb >> 11);
				int num9 = ((num6 < 0) ? (G722Codec.ilb[num4] << -num6) : (G722Codec.ilb[num4] >> num6));
				state.Band[0].det = num9 << 2;
				G722Codec.Block4(state, 0, num8);
				if (!state.EncodeFrom8000Hz)
				{
					num6 = G722Codec.qm2[num5];
					int num10 = state.Band[1].det * num6 >> 15;
					num2 = num10 + state.Band[1].s;
					if (num2 > 16383)
					{
						num2 = 16383;
					}
					else if (num2 < -16384)
					{
						num2 = -16384;
					}
					num6 = G722Codec.rh2[num5];
					num4 = state.Band[1].nb * 127 >> 7;
					num4 += G722Codec.wh[num6];
					if (num4 < 0)
					{
						num4 = 0;
					}
					else if (num4 > 22528)
					{
						num4 = 22528;
					}
					state.Band[1].nb = num4;
					num4 = (state.Band[1].nb >> 6) & 31;
					num6 = 10 - (state.Band[1].nb >> 11);
					num9 = ((num6 < 0) ? (G722Codec.ilb[num4] << -num6) : (G722Codec.ilb[num4] >> num6));
					state.Band[1].det = num9 << 2;
					G722Codec.Block4(state, 1, num10);
				}
				if (state.ItuTestMode)
				{
					outputBuffer[num++] = (short)(num7 << 1);
					outputBuffer[num++] = (short)(num2 << 1);
				}
				else if (state.EncodeFrom8000Hz)
				{
					outputBuffer[num++] = (short)(num7 << 1);
				}
				else
				{
					for (int j = 0; j < 22; j++)
					{
						state.QmfSignalHistory[j] = state.QmfSignalHistory[j + 2];
					}
					state.QmfSignalHistory[22] = num7 + num2;
					state.QmfSignalHistory[23] = num7 - num2;
					int num11 = 0;
					int num12 = 0;
					for (int j = 0; j < 12; j++)
					{
						num12 += state.QmfSignalHistory[2 * j] * G722Codec.qmf_coeffs[j];
						num11 += state.QmfSignalHistory[2 * j + 1] * G722Codec.qmf_coeffs[11 - j];
					}
					outputBuffer[num++] = (short)(num11 >> 11);
					outputBuffer[num++] = (short)(num12 >> 11);
				}
			}
			return num;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002CEC File Offset: 0x00000EEC
		public int Encode(G722CodecState state, byte[] outputBuffer, short[] inputBuffer, int inputBufferCount)
		{
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < inputBufferCount)
			{
				int num3;
				int j;
				if (state.ItuTestMode)
				{
					num2 = (num3 = inputBuffer[i++] >> 1);
				}
				else if (state.EncodeFrom8000Hz)
				{
					num3 = inputBuffer[i++] >> 1;
				}
				else
				{
					for (j = 0; j < 22; j++)
					{
						state.QmfSignalHistory[j] = state.QmfSignalHistory[j + 2];
					}
					state.QmfSignalHistory[22] = (int)inputBuffer[i++];
					state.QmfSignalHistory[23] = (int)inputBuffer[i++];
					int num4 = 0;
					int num5 = 0;
					for (j = 0; j < 12; j++)
					{
						num5 += state.QmfSignalHistory[2 * j] * G722Codec.qmf_coeffs[j];
						num4 += state.QmfSignalHistory[2 * j + 1] * G722Codec.qmf_coeffs[11 - j];
					}
					num3 = num4 + num5 >> 14;
					num2 = num4 - num5 >> 14;
				}
				int num6 = (int)G722Codec.Saturate(num3 - state.Band[0].s);
				int num7 = ((num6 >= 0) ? num6 : (-(num6 + 1)));
				int num8;
				for (j = 1; j < 30; j++)
				{
					num8 = G722Codec.q6[j] * state.Band[0].det >> 12;
					if (num7 < num8)
					{
						break;
					}
				}
				int num9 = ((num6 < 0) ? G722Codec.iln[j] : G722Codec.ilp[j]);
				int num10 = num9 >> 2;
				int num11 = G722Codec.qm4[num10];
				int num12 = state.Band[0].det * num11 >> 15;
				int num13 = G722Codec.rl42[num10];
				num7 = state.Band[0].nb * 127 >> 7;
				state.Band[0].nb = num7 + G722Codec.wl[num13];
				if (state.Band[0].nb < 0)
				{
					state.Band[0].nb = 0;
				}
				else if (state.Band[0].nb > 18432)
				{
					state.Band[0].nb = 18432;
				}
				num8 = (state.Band[0].nb >> 6) & 31;
				num11 = 8 - (state.Band[0].nb >> 11);
				int num14 = ((num11 < 0) ? (G722Codec.ilb[num8] << -num11) : (G722Codec.ilb[num8] >> num11));
				state.Band[0].det = num14 << 2;
				G722Codec.Block4(state, 0, num12);
				int num15;
				if (state.EncodeFrom8000Hz)
				{
					num15 = (192 | num9) >> 8 - state.BitsPerSample;
				}
				else
				{
					int num16 = (int)G722Codec.Saturate(num2 - state.Band[1].s);
					num7 = ((num16 >= 0) ? num16 : (-(num16 + 1)));
					num8 = 564 * state.Band[1].det >> 12;
					int num17 = ((num7 >= num8) ? 2 : 1);
					int num18 = ((num16 < 0) ? G722Codec.ihn[num17] : G722Codec.ihp[num17]);
					num11 = G722Codec.qm2[num18];
					int num19 = state.Band[1].det * num11 >> 15;
					int num20 = G722Codec.rh2[num18];
					num7 = state.Band[1].nb * 127 >> 7;
					state.Band[1].nb = num7 + G722Codec.wh[num20];
					if (state.Band[1].nb < 0)
					{
						state.Band[1].nb = 0;
					}
					else if (state.Band[1].nb > 22528)
					{
						state.Band[1].nb = 22528;
					}
					num8 = (state.Band[1].nb >> 6) & 31;
					num11 = 10 - (state.Band[1].nb >> 11);
					num14 = ((num11 < 0) ? (G722Codec.ilb[num8] << -num11) : (G722Codec.ilb[num8] >> num11));
					state.Band[1].det = num14 << 2;
					G722Codec.Block4(state, 1, num19);
					num15 = ((num18 << 6) | num9) >> 8 - state.BitsPerSample;
				}
				if (state.Packed)
				{
					state.OutBuffer |= (uint)((uint)num15 << state.OutBits);
					state.OutBits += state.BitsPerSample;
					if (state.OutBits >= 8)
					{
						outputBuffer[num++] = (byte)(state.OutBuffer & 255U);
						state.OutBits -= 8;
						state.OutBuffer >>= 8;
					}
				}
				else
				{
					outputBuffer[num++] = (byte)num15;
				}
			}
			return num;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000035F0 File Offset: 0x000017F0
		// Note: this type is marked as 'beforefieldinit'.
		static G722Codec()
		{
			int[] array = new int[3];
			array[1] = 1;
			G722Codec.ihn = array;
			G722Codec.ihp = new int[]
			{
				default(int),
				3,
				2
			};
		}

		// Token: 0x04000005 RID: 5
		private static readonly int[] wl = new int[] { -60, -30, 58, 172, 334, 538, 1198, 3042 };

		// Token: 0x04000006 RID: 6
		private static readonly int[] rl42 = new int[]
		{
			0, 7, 6, 5, 4, 3, 2, 1, 7, 6,
			5, 4, 3, 2, 1, 0
		};

		// Token: 0x04000007 RID: 7
		private static readonly int[] ilb = new int[]
		{
			2048, 2093, 2139, 2186, 2233, 2282, 2332, 2383, 2435, 2489,
			2543, 2599, 2656, 2714, 2774, 2834, 2896, 2960, 3025, 3091,
			3158, 3228, 3298, 3371, 3444, 3520, 3597, 3676, 3756, 3838,
			3922, 4008
		};

		// Token: 0x04000008 RID: 8
		private static readonly int[] wh = new int[]
		{
			default(int),
			-214,
			798
		};

		// Token: 0x04000009 RID: 9
		private static readonly int[] rh2 = new int[] { 2, 1, 2, 1 };

		// Token: 0x0400000A RID: 10
		private static readonly int[] qm2 = new int[] { -7408, -1616, 7408, 1616 };

		// Token: 0x0400000B RID: 11
		private static readonly int[] qm4 = new int[]
		{
			0, -20456, -12896, -8968, -6288, -4240, -2584, -1200, 20456, 12896,
			8968, 6288, 4240, 2584, 1200, 0
		};

		// Token: 0x0400000C RID: 12
		private static readonly int[] qm5 = new int[]
		{
			-280, -280, -23352, -17560, -14120, -11664, -9752, -8184, -6864, -5712,
			-4696, -3784, -2960, -2208, -1520, -880, 23352, 17560, 14120, 11664,
			9752, 8184, 6864, 5712, 4696, 3784, 2960, 2208, 1520, 880,
			280, -280
		};

		// Token: 0x0400000D RID: 13
		private static readonly int[] qm6 = new int[]
		{
			-136, -136, -136, -136, -24808, -21904, -19008, -16704, -14984, -13512,
			-12280, -11192, -10232, -9360, -8576, -7856, -7192, -6576, -6000, -5456,
			-4944, -4464, -4008, -3576, -3168, -2776, -2400, -2032, -1688, -1360,
			-1040, -728, 24808, 21904, 19008, 16704, 14984, 13512, 12280, 11192,
			10232, 9360, 8576, 7856, 7192, 6576, 6000, 5456, 4944, 4464,
			4008, 3576, 3168, 2776, 2400, 2032, 1688, 1360, 1040, 728,
			432, 136, -432, -136
		};

		// Token: 0x0400000E RID: 14
		private static readonly int[] qmf_coeffs = new int[]
		{
			3, -11, 12, 32, -210, 951, 3876, -805, 362, -156,
			53, -11
		};

		// Token: 0x0400000F RID: 15
		private static readonly int[] q6 = new int[]
		{
			0, 35, 72, 110, 150, 190, 233, 276, 323, 370,
			422, 473, 530, 587, 650, 714, 786, 858, 940, 1023,
			1121, 1219, 1339, 1458, 1612, 1765, 1980, 2195, 2557, 2919,
			0, 0
		};

		// Token: 0x04000010 RID: 16
		private static readonly int[] iln = new int[]
		{
			0, 63, 62, 31, 30, 29, 28, 27, 26, 25,
			24, 23, 22, 21, 20, 19, 18, 17, 16, 15,
			14, 13, 12, 11, 10, 9, 8, 7, 6, 5,
			4, 0
		};

		// Token: 0x04000011 RID: 17
		private static readonly int[] ilp = new int[]
		{
			0, 61, 60, 59, 58, 57, 56, 55, 54, 53,
			52, 51, 50, 49, 48, 47, 46, 45, 44, 43,
			42, 41, 40, 39, 38, 37, 36, 35, 34, 33,
			32, 0
		};

		// Token: 0x04000012 RID: 18
		private static readonly int[] ihn;

		// Token: 0x04000013 RID: 19
		private static readonly int[] ihp;
	}
}
