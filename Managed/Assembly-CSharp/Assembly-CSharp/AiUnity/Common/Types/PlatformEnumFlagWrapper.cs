using System;
using System.Collections.Generic;
using System.Linq;
using AiUnity.Common.Extensions;
using AiUnity.Common.Utilities;

namespace AiUnity.Common.Types
{
	// Token: 0x020002BD RID: 701
	public class PlatformEnumFlagWrapper<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0007F065 File Offset: 0x0007D265
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0007F06D File Offset: 0x0007D26D
		public int EnumFlags { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0007F076 File Offset: 0x0007D276
		public IEnumerable<string> EnumNames
		{
			get
			{
				return this.EnumValues.Select<TEnum, string>((TEnum b) => b.ToString());
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0007F0A2 File Offset: 0x0007D2A2
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0007F0AA File Offset: 0x0007D2AA
		public IEnumerable<TEnum> EnumValues { get; set; }

		// Token: 0x06001872 RID: 6258 RVA: 0x0007F0B4 File Offset: 0x0007D2B4
		public PlatformEnumFlagWrapper(TEnum tEnum = default(TEnum))
		{
			this.EnumValues = (from e in EnumUtility.GetValues<TEnum>(true, false)
				where !this.legacyPlatforms.Any<string>((string p) => e.ToString().Contains(p))
				select e).Distinct<TEnum>().ToList<TEnum>();
			foreach (var anon in this.EnumValues.Select((TEnum e, int i) => new { e, i }))
			{
				this.EnumValueToFlag[anon.e] = this.EnumValueToFlag.GetValueOrDefault(anon.e) | (1 << anon.i);
			}
			this.EnumFlags = this.EnumValueToFlag[tEnum];
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0007F200 File Offset: 0x0007D400
		public PlatformEnumFlagWrapper(string names)
			: this(default(TEnum))
		{
			if (names == "Everything")
			{
				this.EnumFlags = -1;
				return;
			}
			if (!string.IsNullOrEmpty(names))
			{
				foreach (string text in from n in names.Split(',', StringSplitOptions.None)
					select n.Trim())
				{
					this.Add(text.ToEnum(default(TEnum)));
				}
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0007F2B0 File Offset: 0x0007D4B0
		public override string ToString()
		{
			if (this.EnumFlags == -1)
			{
				return "Everything";
			}
			return string.Join(", ", (from e in this.GetFlags()
				select e.ToString()).ToArray<string>());
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0007F305 File Offset: 0x0007D505
		public bool Has(TEnum e)
		{
			return (this.EnumFlags & this.EnumValueToFlag[e]) != 0;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0007F31D File Offset: 0x0007D51D
		public void Add(TEnum e)
		{
			this.EnumFlags |= this.EnumValueToFlag[e];
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0007F338 File Offset: 0x0007D538
		public void Remove(TEnum e)
		{
			this.EnumFlags &= ~this.EnumValueToFlag[e];
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0007F354 File Offset: 0x0007D554
		public IEnumerable<TEnum> GetFlags()
		{
			PlatformEnumFlagWrapper<TEnum>.<>c__DisplayClass18_0 CS$<>8__locals1 = new PlatformEnumFlagWrapper<TEnum>.<>c__DisplayClass18_0();
			CS$<>8__locals1.currentPow = 1;
			while (CS$<>8__locals1.currentPow != 0)
			{
				if ((CS$<>8__locals1.currentPow & this.EnumFlags) != 0)
				{
					yield return this.EnumValueToFlag.FirstOrDefault<KeyValuePair<TEnum, int>>((KeyValuePair<TEnum, int> x) => x.Value == CS$<>8__locals1.currentPow).Key;
				}
				CS$<>8__locals1.currentPow <<= 1;
			}
			CS$<>8__locals1 = null;
			yield break;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0007F364 File Offset: 0x0007D564
		public static implicit operator PlatformEnumFlagWrapper<TEnum>(TEnum tEnum)
		{
			return new PlatformEnumFlagWrapper<TEnum>(tEnum);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0007F36C File Offset: 0x0007D56C
		public static implicit operator PlatformEnumFlagWrapper<TEnum>(string names)
		{
			return new PlatformEnumFlagWrapper<TEnum>(names);
		}

		// Token: 0x04000E83 RID: 3715
		public Dictionary<TEnum, int> EnumValueToFlag = new Dictionary<TEnum, int> { 
		{
			default(TEnum),
			0
		} };

		// Token: 0x04000E84 RID: 3716
		private IEnumerable<string> legacyPlatforms = new List<string> { "PS3", "XBOX360", "WP8", "BB10Player", "BlackBerry", "NaCl", "FlashPlayer" };
	}
}
