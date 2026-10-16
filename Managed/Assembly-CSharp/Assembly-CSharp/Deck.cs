using System;
using System.Collections.Generic;

// Token: 0x02000037 RID: 55
public static class Deck
{
	// Token: 0x060001F2 RID: 498 RVA: 0x0000A400 File Offset: 0x00008600
	public static IEnumerable<long> deal(long handSize, long deckSize, Random rng = null)
	{
		if (rng == null)
		{
			rng = new Random();
		}
		long i = -1L;
		long qu = -handSize + 1L + deckSize;
		long negalphainv = -13L;
		long threshold = -negalphainv * handSize;
		double nreal = (double)handSize;
		double Nreal = (double)deckSize;
		double num = 1.0 / (double)handSize;
		double nmin1inv = 1.0 / (double)(handSize - 1L);
		double Vprime = Math.Exp(Math.Log(rng.NextDouble()) * num);
		double qu1real = -nreal + 1.0 + Nreal;
		while (handSize > 1L && threshold < deckSize)
		{
			nmin1inv = 1.0 / (-1.0 + nreal);
			long S;
			double negSreal;
			for (;;)
			{
				double num2 = Nreal * (-Vprime + 1.0);
				S = (long)Math.Floor(num2);
				if (S >= qu)
				{
					Vprime = Math.Exp(Math.Log(rng.NextDouble()) * num);
				}
				else
				{
					double num3 = rng.NextDouble();
					negSreal = (double)(-(double)S);
					double num4 = Math.Exp(Math.Log(num3 * Nreal / qu1real) * nmin1inv);
					Vprime = num4 * (-num2 / Nreal + 1.0) * (qu1real / (negSreal + qu1real));
					if (Vprime <= 1.0)
					{
						break;
					}
					double num5 = 1.0;
					double num6 = -1.0 + Nreal;
					double num7;
					long num8;
					if (-1L + handSize > S)
					{
						num7 = -nreal + Nreal;
						num8 = -S + deckSize;
					}
					else
					{
						num7 = -1.0 + negSreal + Nreal;
						num8 = qu;
					}
					for (long num9 = deckSize - 1L; num9 >= num8; num9 -= 1L)
					{
						num5 = num5 * num6 / num7;
						num6 -= 1.0;
						num7 -= 1.0;
					}
					if (Nreal / (-num2 + Nreal) >= num4 * Math.Exp(Math.Log(num5) * nmin1inv))
					{
						goto Block_7;
					}
					Vprime = Math.Exp(Math.Log(rng.NextDouble()) * num);
				}
			}
			IL_0312:
			i += S + 1L;
			yield return i;
			deckSize = -S + (-1L + deckSize);
			Nreal = negSreal + (-1.0 + Nreal);
			long num10 = handSize;
			handSize = num10 - 1L;
			double num11 = nreal;
			nreal = num11 - 1.0;
			num = nmin1inv;
			qu = -S + qu;
			qu1real = negSreal + qu1real;
			threshold += negalphainv;
			continue;
			Block_7:
			Vprime = Math.Exp(Math.Log(rng.NextDouble()) * nmin1inv);
			goto IL_0312;
		}
		if (handSize > 1L)
		{
			foreach (long num12 in Deck.vitter_a(handSize, deckSize, i, rng))
			{
				yield return num12;
			}
			IEnumerator<long> enumerator = null;
		}
		else
		{
			long S = (long)Math.Floor((double)deckSize * Vprime);
			i += S + 1L;
			yield return i;
		}
		yield break;
		yield break;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000A41E File Offset: 0x0000861E
	public static IEnumerable<long> vitter_a(long n, long N, long j, Random rng)
	{
		double top = (double)(N - n);
		double Nreal = (double)N;
		long num2;
		while (n >= 2L)
		{
			double num = rng.NextDouble();
			num2 = 0L;
			double num4;
			for (double num3 = top / Nreal; num3 > num; num3 = num3 * top / Nreal)
			{
				num2 += 1L;
				num4 = top;
				top = num4 - 1.0;
				num4 = Nreal;
				Nreal = num4 - 1.0;
			}
			j += num2 + 1L;
			yield return j;
			num4 = Nreal;
			Nreal = num4 - 1.0;
			long num5 = n;
			n = num5 - 1L;
		}
		num2 = (long)Math.Floor(Math.Round(Nreal) * rng.NextDouble());
		j += num2 + 1L;
		yield return j;
		yield break;
	}
}
