using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000973 RID: 2419
	internal static class ParticleFX
	{
		// Token: 0x06006412 RID: 25618 RVA: 0x001EDD84 File Offset: 0x001EBF84
		public static void Smoke(int X, int Y, int StartAngle, int EndAngle)
		{
			if (Options.DisableSmoke)
			{
				return;
			}
			float num = (float)Stat.RandomCosmetic(StartAngle, EndAngle) / 58f;
			float num2 = (float)Math.Sin((double)num) / 6f;
			float num3 = (float)Math.Cos((double)num) / 6f;
			int num4 = Stat.RandomCosmetic(1, 3);
			string text = "";
			if (num4 == 1)
			{
				text = "°";
			}
			else if (num4 == 2)
			{
				text = "±";
			}
			else if (num4 == 3)
			{
				text = "²";
			}
			XRLCore.ParticleManager.Add(text, (float)X, (float)Y, num2, num3);
		}
	}
}
