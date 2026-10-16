using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200028F RID: 655
	[NullableContext(1)]
	[Nullable(0)]
	internal static class VersionHelper
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x0004FE7D File Offset: 0x0004E07D
		internal static bool TryParse(string s, out Version version)
		{
			return VersionHelper.TryParse(s, false, ushort.MaxValue, true, out version);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0004FE8D File Offset: 0x0004E08D
		internal static bool TryParseAssemblyVersion(string s, bool allowWildcard, out Version version)
		{
			return VersionHelper.TryParse(s, allowWildcard, 65534, false, out version);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0004FEA0 File Offset: 0x0004E0A0
		private static bool TryParse(string s, bool allowWildcard, ushort maxValue, bool allowPartialParse, out Version version)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				version = AssemblyIdentity.NullVersion;
				return false;
			}
			string[] array = s.Split(new char[] { '.' });
			bool flag = allowWildcard && array[array.Length - 1] == "*";
			if ((flag && array.Length < 3) || array.Length > 4)
			{
				version = AssemblyIdentity.NullVersion;
				return false;
			}
			ushort[] array2 = new ushort[4];
			int num = (flag ? (array.Length - 1) : array.Length);
			bool flag2 = false;
			for (int i = 0; i < num; i++)
			{
				if (!ushort.TryParse(array[i], 0, CultureInfo.InvariantCulture, ref array2[i]) || array2[i] > maxValue)
				{
					if (!allowPartialParse)
					{
						version = AssemblyIdentity.NullVersion;
						return false;
					}
					flag2 = true;
					if (string.IsNullOrWhiteSpace(array[i]))
					{
						array2[i] = 0;
						break;
					}
					if (array2[i] > maxValue)
					{
						array2[i] = 0;
					}
					else
					{
						bool flag3 = false;
						0;
						for (int j = 0; j < array[i].Length; j++)
						{
							if (!char.IsDigit(array[i].get_Chars(j)))
							{
								flag3 = true;
								VersionHelper.TryGetValue(array[i].Substring(0, j), out array2[i]);
								break;
							}
						}
						if (flag3 || !VersionHelper.TryGetValue(array[i], out array2[i]))
						{
							break;
						}
					}
				}
			}
			if (flag)
			{
				for (int k = num; k < array2.Length; k++)
				{
					array2[k] = ushort.MaxValue;
				}
			}
			version = new Version((int)array2[0], (int)array2[1], (int)array2[2], (int)array2[3]);
			return !flag2;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00050028 File Offset: 0x0004E228
		private static bool TryGetValue(string s, out ushort value)
		{
			BigInteger bigInteger;
			if (BigInteger.TryParse(s, 0, CultureInfo.InvariantCulture, ref bigInteger))
			{
				value = (ushort)(bigInteger % 65536);
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00050064 File Offset: 0x0004E264
		[return: Nullable(2)]
		public static Version GenerateVersionFromPatternAndCurrentTime(DateTime time, Version pattern)
		{
			if (pattern == null || pattern.Revision != 65535)
			{
				return pattern;
			}
			if (time == default(DateTime))
			{
				time = DateTime.Now;
			}
			int num = (int)time.TimeOfDay.TotalSeconds / 2;
			if (pattern.Build == 65535)
			{
				int num2 = Math.Min(65535, (int)(time.Date - new DateTime(2000, 1, 1)).TotalDays);
				return new Version(pattern.Major, pattern.Minor, (int)((ushort)num2), (int)((ushort)num));
			}
			return new Version(pattern.Major, pattern.Minor, pattern.Build, (int)((ushort)num));
		}
	}
}
