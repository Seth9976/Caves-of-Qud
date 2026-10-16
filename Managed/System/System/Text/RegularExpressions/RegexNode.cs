using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020A RID: 522
	internal sealed class RegexNode
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x000400DC File Offset: 0x0003E2DC
		public RegexNode(int type, RegexOptions options)
		{
			this.NType = type;
			this.Options = options;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000400F2 File Offset: 0x0003E2F2
		public RegexNode(int type, RegexOptions options, char ch)
		{
			this.NType = type;
			this.Options = options;
			this.Ch = ch;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0004010F File Offset: 0x0003E30F
		public RegexNode(int type, RegexOptions options, string str)
		{
			this.NType = type;
			this.Options = options;
			this.Str = str;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0004012C File Offset: 0x0003E32C
		public RegexNode(int type, RegexOptions options, int m)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00040149 File Offset: 0x0003E349
		public RegexNode(int type, RegexOptions options, int m, int n)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
			this.N = n;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004016E File Offset: 0x0003E36E
		public bool UseOptionR()
		{
			return (this.Options & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0004017C File Offset: 0x0003E37C
		public RegexNode ReverseLeft()
		{
			if (this.UseOptionR() && this.NType == 25 && this.Children != null)
			{
				this.Children.Reverse(0, this.Children.Count);
			}
			return this;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000401B0 File Offset: 0x0003E3B0
		private void MakeRep(int type, int min, int max)
		{
			this.NType += type - 9;
			this.M = min;
			this.N = max;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x000401D4 File Offset: 0x0003E3D4
		private RegexNode Reduce()
		{
			int num = this.Type();
			RegexNode regexNode;
			if (num != 5 && num != 11)
			{
				switch (num)
				{
				case 24:
					return this.ReduceAlternation();
				case 25:
					return this.ReduceConcatenation();
				case 26:
				case 27:
					return this.ReduceRep();
				case 29:
					return this.ReduceGroup();
				}
				regexNode = this;
			}
			else
			{
				regexNode = this.ReduceSet();
			}
			return regexNode;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00040244 File Offset: 0x0003E444
		private RegexNode StripEnation(int emptyType)
		{
			int num = this.ChildCount();
			if (num == 0)
			{
				return new RegexNode(emptyType, this.Options);
			}
			if (num != 1)
			{
				return this;
			}
			return this.Child(0);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00040278 File Offset: 0x0003E478
		private RegexNode ReduceGroup()
		{
			RegexNode regexNode = this;
			while (regexNode.Type() == 29)
			{
				regexNode = regexNode.Child(0);
			}
			return regexNode;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0004029C File Offset: 0x0003E49C
		private RegexNode ReduceRep()
		{
			RegexNode regexNode = this;
			int num = this.Type();
			int num2 = this.M;
			int num3 = this.N;
			while (regexNode.ChildCount() != 0)
			{
				RegexNode regexNode2 = regexNode.Child(0);
				if (regexNode2.Type() != num)
				{
					int num4 = regexNode2.Type();
					if ((num4 < 3 || num4 > 5 || num != 26) && (num4 < 6 || num4 > 8 || num != 27))
					{
						break;
					}
				}
				if ((regexNode.M == 0 && regexNode2.M > 1) || regexNode2.N < regexNode2.M * 2)
				{
					break;
				}
				regexNode = regexNode2;
				if (regexNode.M > 0)
				{
					num2 = (regexNode.M = ((2147483646 / regexNode.M < num2) ? int.MaxValue : (regexNode.M * num2)));
				}
				if (regexNode.N > 0)
				{
					num3 = (regexNode.N = ((2147483646 / regexNode.N < num3) ? int.MaxValue : (regexNode.N * num3)));
				}
			}
			if (num2 != 2147483647)
			{
				return regexNode;
			}
			return new RegexNode(22, this.Options);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000403B0 File Offset: 0x0003E5B0
		private RegexNode ReduceSet()
		{
			if (RegexCharClass.IsEmpty(this.Str))
			{
				this.NType = 22;
				this.Str = null;
			}
			else if (RegexCharClass.IsSingleton(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -2;
			}
			else if (RegexCharClass.IsSingletonInverse(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -1;
			}
			return this;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00040448 File Offset: 0x0003E648
		private RegexNode ReduceAlternation()
		{
			if (this.Children == null)
			{
				return new RegexNode(22, this.Options);
			}
			bool flag = false;
			bool flag2 = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 24)
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 11 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (regexNode.NType == 11)
					{
						if (!flag || regexOptions != regexOptions2 || flag2 || !RegexCharClass.IsMergeable(regexNode.Str))
						{
							flag = true;
							flag2 = !RegexCharClass.IsMergeable(regexNode.Str);
							regexOptions = regexOptions2;
							goto IL_01D0;
						}
					}
					else if (!flag || regexOptions != regexOptions2 || flag2)
					{
						flag = true;
						flag2 = false;
						regexOptions = regexOptions2;
						goto IL_01D0;
					}
					num--;
					RegexNode regexNode2 = this.Children[num];
					RegexCharClass regexCharClass;
					if (regexNode2.NType == 9)
					{
						regexCharClass = new RegexCharClass();
						regexCharClass.AddChar(regexNode2.Ch);
					}
					else
					{
						regexCharClass = RegexCharClass.Parse(regexNode2.Str);
					}
					if (regexNode.NType == 9)
					{
						regexCharClass.AddChar(regexNode.Ch);
					}
					else
					{
						RegexCharClass regexCharClass2 = RegexCharClass.Parse(regexNode.Str);
						regexCharClass.AddCharClass(regexCharClass2);
					}
					regexNode2.NType = 11;
					regexNode2.Str = regexCharClass.ToStringClass();
				}
				else if (regexNode.NType == 22)
				{
					num--;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
				IL_01D0:
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(22);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00040664 File Offset: 0x0003E864
		private RegexNode ReduceConcatenation()
		{
			if (this.Children == null)
			{
				return new RegexNode(23, this.Options);
			}
			bool flag = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 25 && (regexNode.Options & RegexOptions.RightToLeft) == (this.Options & RegexOptions.RightToLeft))
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 12 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (!flag || regexOptions != regexOptions2)
					{
						flag = true;
						regexOptions = regexOptions2;
					}
					else
					{
						RegexNode regexNode2 = this.Children[--num];
						if (regexNode2.NType == 9)
						{
							regexNode2.NType = 12;
							regexNode2.Str = Convert.ToString(regexNode2.Ch, CultureInfo.InvariantCulture);
						}
						if ((regexOptions2 & RegexOptions.RightToLeft) == RegexOptions.None)
						{
							if (regexNode.NType == 9)
							{
								RegexNode regexNode3 = regexNode2;
								regexNode3.Str += regexNode.Ch.ToString();
							}
							else
							{
								RegexNode regexNode4 = regexNode2;
								regexNode4.Str += regexNode.Str;
							}
						}
						else if (regexNode.NType == 9)
						{
							regexNode2.Str = regexNode.Ch.ToString() + regexNode2.Str;
						}
						else
						{
							regexNode2.Str = regexNode.Str + regexNode2.Str;
						}
					}
				}
				else if (regexNode.NType == 23)
				{
					num--;
				}
				else
				{
					flag = false;
				}
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(23);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004087C File Offset: 0x0003EA7C
		public RegexNode MakeQuantifier(bool lazy, int min, int max)
		{
			if (min == 0 && max == 0)
			{
				return new RegexNode(23, this.Options);
			}
			if (min == 1 && max == 1)
			{
				return this;
			}
			int ntype = this.NType;
			if (ntype - 9 <= 2)
			{
				this.MakeRep(lazy ? 6 : 3, min, max);
				return this;
			}
			RegexNode regexNode = new RegexNode(lazy ? 27 : 26, this.Options, min, max);
			regexNode.AddChild(this);
			return regexNode;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000408E4 File Offset: 0x0003EAE4
		public void AddChild(RegexNode newChild)
		{
			if (this.Children == null)
			{
				this.Children = new List<RegexNode>(4);
			}
			RegexNode regexNode = newChild.Reduce();
			this.Children.Add(regexNode);
			regexNode.Next = this;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004091F File Offset: 0x0003EB1F
		public RegexNode Child(int i)
		{
			return this.Children[i];
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0004092D File Offset: 0x0003EB2D
		public int ChildCount()
		{
			if (this.Children != null)
			{
				return this.Children.Count;
			}
			return 0;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00040944 File Offset: 0x0003EB44
		public int Type()
		{
			return this.NType;
		}

		// Token: 0x0400092F RID: 2351
		public const int Oneloop = 3;

		// Token: 0x04000930 RID: 2352
		public const int Notoneloop = 4;

		// Token: 0x04000931 RID: 2353
		public const int Setloop = 5;

		// Token: 0x04000932 RID: 2354
		public const int Onelazy = 6;

		// Token: 0x04000933 RID: 2355
		public const int Notonelazy = 7;

		// Token: 0x04000934 RID: 2356
		public const int Setlazy = 8;

		// Token: 0x04000935 RID: 2357
		public const int One = 9;

		// Token: 0x04000936 RID: 2358
		public const int Notone = 10;

		// Token: 0x04000937 RID: 2359
		public const int Set = 11;

		// Token: 0x04000938 RID: 2360
		public const int Multi = 12;

		// Token: 0x04000939 RID: 2361
		public const int Ref = 13;

		// Token: 0x0400093A RID: 2362
		public const int Bol = 14;

		// Token: 0x0400093B RID: 2363
		public const int Eol = 15;

		// Token: 0x0400093C RID: 2364
		public const int Boundary = 16;

		// Token: 0x0400093D RID: 2365
		public const int Nonboundary = 17;

		// Token: 0x0400093E RID: 2366
		public const int ECMABoundary = 41;

		// Token: 0x0400093F RID: 2367
		public const int NonECMABoundary = 42;

		// Token: 0x04000940 RID: 2368
		public const int Beginning = 18;

		// Token: 0x04000941 RID: 2369
		public const int Start = 19;

		// Token: 0x04000942 RID: 2370
		public const int EndZ = 20;

		// Token: 0x04000943 RID: 2371
		public const int End = 21;

		// Token: 0x04000944 RID: 2372
		public const int Nothing = 22;

		// Token: 0x04000945 RID: 2373
		public const int Empty = 23;

		// Token: 0x04000946 RID: 2374
		public const int Alternate = 24;

		// Token: 0x04000947 RID: 2375
		public const int Concatenate = 25;

		// Token: 0x04000948 RID: 2376
		public const int Loop = 26;

		// Token: 0x04000949 RID: 2377
		public const int Lazyloop = 27;

		// Token: 0x0400094A RID: 2378
		public const int Capture = 28;

		// Token: 0x0400094B RID: 2379
		public const int Group = 29;

		// Token: 0x0400094C RID: 2380
		public const int Require = 30;

		// Token: 0x0400094D RID: 2381
		public const int Prevent = 31;

		// Token: 0x0400094E RID: 2382
		public const int Greedy = 32;

		// Token: 0x0400094F RID: 2383
		public const int Testref = 33;

		// Token: 0x04000950 RID: 2384
		public const int Testgroup = 34;

		// Token: 0x04000951 RID: 2385
		public int NType;

		// Token: 0x04000952 RID: 2386
		public List<RegexNode> Children;

		// Token: 0x04000953 RID: 2387
		public string Str;

		// Token: 0x04000954 RID: 2388
		public char Ch;

		// Token: 0x04000955 RID: 2389
		public int M;

		// Token: 0x04000956 RID: 2390
		public int N;

		// Token: 0x04000957 RID: 2391
		public readonly RegexOptions Options;

		// Token: 0x04000958 RID: 2392
		public RegexNode Next;
	}
}
