using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020E RID: 526
	internal sealed class RegexReplacement
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x00043354 File Offset: 0x00041554
		public RegexReplacement(string rep, RegexNode concat, Hashtable _caps)
		{
			if (concat.Type() != 25)
			{
				throw new ArgumentException("Replacement pattern error.");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			int i = 0;
			while (i < concat.ChildCount())
			{
				RegexNode regexNode = concat.Child(i);
				switch (regexNode.Type())
				{
				case 9:
					stringBuilder.Append(regexNode.Ch);
					break;
				case 10:
				case 11:
					goto IL_00E2;
				case 12:
					stringBuilder.Append(regexNode.Str);
					break;
				case 13:
				{
					if (stringBuilder.Length > 0)
					{
						list2.Add(list.Count);
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					int num = regexNode.M;
					if (_caps != null && num >= 0)
					{
						num = (int)_caps[num];
					}
					list2.Add(-5 - num);
					break;
				}
				default:
					goto IL_00E2;
				}
				i++;
				continue;
				IL_00E2:
				throw new ArgumentException("Replacement pattern error.");
			}
			if (stringBuilder.Length > 0)
			{
				list2.Add(list.Count);
				list.Add(stringBuilder.ToString());
			}
			StringBuilderCache.Release(stringBuilder);
			this.Pattern = rep;
			this._strings = list;
			this._rules = list2;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0004349C File Offset: 0x0004169C
		public static RegexReplacement GetOrCreate(WeakReference<RegexReplacement> replRef, string replacement, Hashtable caps, int capsize, Hashtable capnames, RegexOptions roptions)
		{
			RegexReplacement regexReplacement;
			if (!replRef.TryGetTarget(out regexReplacement) || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, caps, capsize, capnames, roptions);
				replRef.SetTarget(regexReplacement);
			}
			return regexReplacement;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x000434D6 File Offset: 0x000416D6
		public string Pattern { get; }

		// Token: 0x06000F16 RID: 3862 RVA: 0x000434E0 File Offset: 0x000416E0
		private void ReplacementImpl(StringBuilder sb, Match match)
		{
			for (int i = 0; i < this._rules.Count; i++)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					sb.Append(this._strings[num]);
				}
				else if (num < -4)
				{
					sb.Append(match.GroupToStringImpl(-5 - num));
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						sb.Append(match.Text);
						break;
					case -3:
						sb.Append(match.LastGroupToStringImpl());
						break;
					case -2:
						sb.Append(match.GetRightSubstring());
						break;
					case -1:
						sb.Append(match.GetLeftSubstring());
						break;
					}
				}
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000435A4 File Offset: 0x000417A4
		private void ReplacementImplRTL(List<string> al, Match match)
		{
			for (int i = this._rules.Count - 1; i >= 0; i--)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					al.Add(this._strings[num]);
				}
				else if (num < -4)
				{
					al.Add(match.GroupToStringImpl(-5 - num).ToString());
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						al.Add(match.Text);
						break;
					case -3:
						al.Add(match.LastGroupToStringImpl().ToString());
						break;
					case -2:
						al.Add(match.GetRightSubstring().ToString());
						break;
					case -1:
						al.Add(match.GetLeftSubstring().ToString());
						break;
					}
				}
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000436A0 File Offset: 0x000418A0
		public string Replacement(Match match)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			this.ReplacementImpl(stringBuilder, match);
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000436C4 File Offset: 0x000418C4
		public string Replace(Regex regex, string input, int count, int startat)
		{
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					this.ReplacementImpl(stringBuilder, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					this.ReplacementImplRTL(list, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x04000982 RID: 2434
		private const int Specials = 4;

		// Token: 0x04000983 RID: 2435
		public const int LeftPortion = -1;

		// Token: 0x04000984 RID: 2436
		public const int RightPortion = -2;

		// Token: 0x04000985 RID: 2437
		public const int LastGroup = -3;

		// Token: 0x04000986 RID: 2438
		public const int WholeString = -4;

		// Token: 0x04000987 RID: 2439
		private readonly List<string> _strings;

		// Token: 0x04000988 RID: 2440
		private readonly List<int> _rules;
	}
}
