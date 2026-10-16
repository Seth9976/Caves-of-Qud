using System;

namespace NAudio.Utils
{
	// Token: 0x02000111 RID: 273
	public class Decibels
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x000131B0 File Offset: 0x000113B0
		public static double LinearToDecibels(double lin)
		{
			return Math.Log(lin) * 8.685889638065037;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000131C2 File Offset: 0x000113C2
		public static double DecibelsToLinear(double dB)
		{
			return Math.Exp(dB * 0.11512925464970228);
		}

		// Token: 0x040006A3 RID: 1699
		private const double LOG_2_DB = 8.685889638065037;

		// Token: 0x040006A4 RID: 1700
		private const double DB_2_LOG = 0.11512925464970228;
	}
}
