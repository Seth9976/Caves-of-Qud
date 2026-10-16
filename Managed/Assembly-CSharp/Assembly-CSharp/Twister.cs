using System;

// Token: 0x020000A8 RID: 168
internal static class Twister
{
	// Token: 0x0600060D RID: 1549 RVA: 0x0002C49D File Offset: 0x0002A69D
	private static uint HighestBit(uint u)
	{
		return u & 2147483648U;
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0002C4A6 File Offset: 0x0002A6A6
	private static uint LowestBit(uint u)
	{
		return u & 1U;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0002C4AB File Offset: 0x0002A6AB
	private static uint LowestBits(uint u)
	{
		return u & 2147483647U;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0002C4B4 File Offset: 0x0002A6B4
	private static uint MixBits(uint u, uint v)
	{
		return Twister.HighestBit(u) | Twister.LowestBits(v);
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
	public static void Seed(uint seed)
	{
		uint num = (seed | 1U) & uint.MaxValue;
		uint[] array = Twister.state;
		int num2 = 0;
		Twister.left = 0;
		array[num2++] = num;
		int num3 = 624;
		while (Convert.ToBoolean(--num3))
		{
			array[num2++] = (num *= 69069U) & uint.MaxValue;
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0002C514 File Offset: 0x0002A714
	private static uint Reload()
	{
		uint num = 0U;
		uint num2 = 2U;
		uint num3 = 397U;
		if (Twister.left < -1)
		{
			Twister.Seed(4357U);
		}
		Twister.left = 623;
		Twister.nextRand = Twister.state[1];
		uint num4 = Twister.state[0];
		uint num5 = Twister.state[1];
		int num6 = 228;
		while (Convert.ToBoolean(--num6))
		{
			Twister.state[(int)num++] = Twister.state[(int)num3++] ^ (Twister.MixBits(num4, num5) >> 1) ^ (Convert.ToBoolean(Twister.LowestBit(num5)) ? 2567483615U : 0U);
			num4 = num5;
			num5 = Twister.state[(int)num2++];
		}
		num3 = 0U;
		num6 = 397;
		while (Convert.ToBoolean(--num6))
		{
			Twister.state[(int)num++] = Twister.state[(int)num3++] ^ (Twister.MixBits(num4, num5) >> 1) ^ (Convert.ToBoolean(Twister.LowestBit(num5)) ? 2567483615U : 0U);
			num4 = num5;
			num5 = Twister.state[(int)num2++];
		}
		num5 = Twister.state[0];
		Twister.state[(int)num] = Twister.state[(int)num3] ^ (Twister.MixBits(num4, num5) >> 1) ^ (Convert.ToBoolean(Twister.LowestBit(num5)) ? 2567483615U : 0U);
		num5 ^= num5 >> 11;
		num5 ^= (num5 << 7) & 2636928640U;
		num5 ^= (num5 << 15) & 4022730752U;
		return num5 ^ (num5 >> 18);
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002C68E File Offset: 0x0002A88E
	public static float NextDouble()
	{
		return Twister.Random() / 4.2949673E+09f;
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0002C6A0 File Offset: 0x0002A8A0
	public static uint Random()
	{
		if (--Twister.left < 0)
		{
			return Twister.Reload();
		}
		uint num = Twister.nextRand++;
		uint num2 = num ^ (num >> 11);
		uint num3 = num2 ^ ((num2 << 7) & 2636928640U);
		uint num4 = num3 ^ ((num3 << 15) & 4022730752U);
		return num4 ^ (num4 >> 18);
	}

	// Token: 0x04000462 RID: 1122
	private const uint N = 624U;

	// Token: 0x04000463 RID: 1123
	private const uint M = 397U;

	// Token: 0x04000464 RID: 1124
	private const uint K = 2567483615U;

	// Token: 0x04000465 RID: 1125
	private static uint[] state = new uint[625];

	// Token: 0x04000466 RID: 1126
	private static uint nextRand;

	// Token: 0x04000467 RID: 1127
	private static int left = -1;
}
