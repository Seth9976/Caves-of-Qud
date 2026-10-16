using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x02000372 RID: 882
	public static class Calc
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x000A2B62 File Offset: 0x000A0D62
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x000A2B69 File Offset: 0x000A0D69
		public static Random R
		{
			get
			{
				return Calc._R;
			}
			set
			{
				Calc._R = value;
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000A2B71 File Offset: 0x000A0D71
		public static void Reseed(int s)
		{
			Calc.R = new Random(s);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000A2B7E File Offset: 0x000A0D7E
		public static int Clamp(int n, int Min, int Max)
		{
			if (n < Min)
			{
				return Min;
			}
			if (n > Max)
			{
				return Max;
			}
			return n;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000A2B90 File Offset: 0x000A0D90
		public static string GetOppositeDirection(string D)
		{
			if (D == "N")
			{
				return "S";
			}
			if (D == "S")
			{
				return "N";
			}
			if (D == "E")
			{
				return "W";
			}
			if (D == "W")
			{
				return "E";
			}
			if (D == "NW")
			{
				return "SE";
			}
			if (D == "NE")
			{
				return "SW";
			}
			if (D == "SW")
			{
				return "NE";
			}
			if (D == "SE")
			{
				return "NW";
			}
			return D;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000A2C36 File Offset: 0x000A0E36
		public static string GetOppositeDirection(char D)
		{
			if (D == 'N')
			{
				return "S";
			}
			if (D == 'S')
			{
				return "N";
			}
			if (D == 'E')
			{
				return "W";
			}
			if (D == 'W')
			{
				return "E";
			}
			return D.ToString();
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000A2C6B File Offset: 0x000A0E6B
		public static int Random(int Low, int High)
		{
			return Calc.R.Next(Low, High);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000A2C79 File Offset: 0x000A0E79
		public static T Random<T>(List<T> L)
		{
			return L[Calc.Random(0, L.Count - 1)];
		}

		// Token: 0x040010A5 RID: 4261
		public static Random _R = new Random();
	}
}
