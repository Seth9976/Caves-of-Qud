using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000205 RID: 517
	internal ref struct RegexFCD
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x0003DCE5 File Offset: 0x0003BEE5
		private RegexFCD(Span<int> intStack)
		{
			this._fcStack = new List<RegexFC>(32);
			this._intStack = new global::System.Collections.Generic.ValueListBuilder<int>(intStack);
			this._failed = false;
			this._skipchild = false;
			this._skipAllChildren = false;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003DD18 File Offset: 0x0003BF18
		public unsafe static RegexPrefix? FirstChars(RegexTree t)
		{
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)128], 32);
			RegexFCD regexFCD = new RegexFCD(span);
			RegexFC regexFC = regexFCD.RegexFCFromRegexTree(t);
			regexFCD.Dispose();
			if (regexFC == null || regexFC._nullable)
			{
				return null;
			}
			CultureInfo cultureInfo = (((t.Options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			return new RegexPrefix?(new RegexPrefix(regexFC.GetFirstChars(cultureInfo), regexFC.CaseInsensitive));
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003DD98 File Offset: 0x0003BF98
		public static RegexPrefix Prefix(RegexTree tree)
		{
			RegexNode regexNode = tree.Root;
			RegexNode regexNode2 = null;
			int num = 0;
			for (;;)
			{
				int ntype = regexNode.NType;
				switch (ntype)
				{
				case 3:
				case 6:
					goto IL_00C3;
				case 4:
				case 5:
				case 7:
				case 8:
				case 10:
				case 11:
				case 13:
				case 17:
				case 22:
				case 24:
				case 26:
				case 27:
				case 29:
					goto IL_0139;
				case 9:
					goto IL_0106;
				case 12:
					goto IL_0122;
				case 14:
				case 15:
				case 16:
				case 18:
				case 19:
				case 20:
				case 21:
				case 23:
				case 30:
				case 31:
					break;
				case 25:
					if (regexNode.ChildCount() > 0)
					{
						regexNode2 = regexNode;
						num = 0;
					}
					break;
				case 28:
				case 32:
					regexNode = regexNode.Child(0);
					regexNode2 = null;
					continue;
				default:
					if (ntype != 41)
					{
						goto Block_2;
					}
					break;
				}
				if (regexNode2 == null || num >= regexNode2.ChildCount())
				{
					goto IL_014B;
				}
				regexNode = regexNode2.Child(num++);
			}
			Block_2:
			goto IL_0139;
			IL_00C3:
			if (regexNode.M > 0 && regexNode.M < 1000000)
			{
				return new RegexPrefix(string.Empty.PadRight(regexNode.M, regexNode.Ch), (regexNode.Options & RegexOptions.IgnoreCase) > RegexOptions.None);
			}
			return RegexPrefix.Empty;
			IL_0106:
			return new RegexPrefix(regexNode.Ch.ToString(), (regexNode.Options & RegexOptions.IgnoreCase) > RegexOptions.None);
			IL_0122:
			return new RegexPrefix(regexNode.Str, (regexNode.Options & RegexOptions.IgnoreCase) > RegexOptions.None);
			IL_0139:
			return RegexPrefix.Empty;
			IL_014B:
			return RegexPrefix.Empty;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003DF08 File Offset: 0x0003C108
		public static int Anchors(RegexTree tree)
		{
			RegexNode regexNode = null;
			int num = 0;
			int num2 = 0;
			RegexNode regexNode2 = tree.Root;
			int ntype;
			for (;;)
			{
				ntype = regexNode2.NType;
				switch (ntype)
				{
				case 14:
				case 15:
				case 16:
				case 18:
				case 19:
				case 20:
				case 21:
					goto IL_0091;
				case 17:
				case 22:
				case 24:
				case 26:
				case 27:
				case 29:
					return num2;
				case 23:
				case 30:
				case 31:
					goto IL_00A1;
				case 25:
					if (regexNode2.ChildCount() > 0)
					{
						regexNode = regexNode2;
						num = 0;
						goto IL_00A1;
					}
					goto IL_00A1;
				case 28:
				case 32:
					regexNode2 = regexNode2.Child(0);
					regexNode = null;
					continue;
				}
				break;
				IL_00A1:
				if (regexNode == null || num >= regexNode.ChildCount())
				{
					return num2;
				}
				regexNode2 = regexNode.Child(num++);
			}
			if (ntype != 41)
			{
				return num2;
			}
			IL_0091:
			return num2 | RegexFCD.AnchorFromType(regexNode2.NType);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003DFD4 File Offset: 0x0003C1D4
		private static int AnchorFromType(int type)
		{
			switch (type)
			{
			case 14:
				return 2;
			case 15:
				return 8;
			case 16:
				return 64;
			case 17:
				break;
			case 18:
				return 1;
			case 19:
				return 4;
			case 20:
				return 16;
			case 21:
				return 32;
			default:
				if (type == 41)
				{
					return 128;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003E029 File Offset: 0x0003C229
		private void PushInt(int i)
		{
			this._intStack.Append(i);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003E037 File Offset: 0x0003C237
		private bool IntIsEmpty()
		{
			return this._intStack.Length == 0;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003E047 File Offset: 0x0003C247
		private int PopInt()
		{
			return this._intStack.Pop();
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003E054 File Offset: 0x0003C254
		private void PushFC(RegexFC fc)
		{
			this._fcStack.Add(fc);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003E062 File Offset: 0x0003C262
		private bool FCIsEmpty()
		{
			return this._fcStack.Count == 0;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003E072 File Offset: 0x0003C272
		private RegexFC PopFC()
		{
			RegexFC regexFC = this.TopFC();
			this._fcStack.RemoveAt(this._fcStack.Count - 1);
			return regexFC;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003E092 File Offset: 0x0003C292
		private RegexFC TopFC()
		{
			return this._fcStack[this._fcStack.Count - 1];
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		public void Dispose()
		{
			this._intStack.Dispose();
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003E0BC File Offset: 0x0003C2BC
		private RegexFC RegexFCFromRegexTree(RegexTree tree)
		{
			RegexNode regexNode = tree.Root;
			int num = 0;
			for (;;)
			{
				if (regexNode.Children == null)
				{
					this.CalculateFC(regexNode.NType, regexNode, 0);
				}
				else if (num < regexNode.Children.Count && !this._skipAllChildren)
				{
					this.CalculateFC(regexNode.NType | 64, regexNode, num);
					if (!this._skipchild)
					{
						regexNode = regexNode.Children[num];
						this.PushInt(num);
						num = 0;
						continue;
					}
					num++;
					this._skipchild = false;
					continue;
				}
				this._skipAllChildren = false;
				if (this.IntIsEmpty())
				{
					goto IL_00B9;
				}
				num = this.PopInt();
				regexNode = regexNode.Next;
				this.CalculateFC(regexNode.NType | 128, regexNode, num);
				if (this._failed)
				{
					break;
				}
				num++;
			}
			return null;
			IL_00B9:
			if (this.FCIsEmpty())
			{
				return null;
			}
			return this.PopFC();
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E192 File Offset: 0x0003C392
		private void SkipChild()
		{
			this._skipchild = true;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E19C File Offset: 0x0003C39C
		private void CalculateFC(int NodeType, RegexNode node, int CurIndex)
		{
			bool flag = false;
			bool flag2 = false;
			if (NodeType <= 13)
			{
				if ((node.Options & RegexOptions.IgnoreCase) != RegexOptions.None)
				{
					flag = true;
				}
				if ((node.Options & RegexOptions.RightToLeft) != RegexOptions.None)
				{
					flag2 = true;
				}
			}
			switch (NodeType)
			{
			case 3:
			case 6:
				this.PushFC(new RegexFC(node.Ch, false, node.M == 0, flag));
				return;
			case 4:
			case 7:
				this.PushFC(new RegexFC(node.Ch, true, node.M == 0, flag));
				return;
			case 5:
			case 8:
				this.PushFC(new RegexFC(node.Str, node.M == 0, flag));
				return;
			case 9:
			case 10:
				this.PushFC(new RegexFC(node.Ch, NodeType == 10, false, flag));
				return;
			case 11:
				this.PushFC(new RegexFC(node.Str, false, flag));
				return;
			case 12:
				if (node.Str.Length == 0)
				{
					this.PushFC(new RegexFC(true));
					return;
				}
				if (!flag2)
				{
					this.PushFC(new RegexFC(node.Str[0], false, false, flag));
					return;
				}
				this.PushFC(new RegexFC(node.Str[node.Str.Length - 1], false, false, flag));
				return;
			case 13:
				this.PushFC(new RegexFC("\0\u0001\0\0", true, false));
				return;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 41:
			case 42:
				this.PushFC(new RegexFC(true));
				return;
			case 23:
				this.PushFC(new RegexFC(true));
				return;
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
				break;
			default:
				switch (NodeType)
				{
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 96:
				case 97:
					break;
				case 94:
				case 95:
					this.SkipChild();
					this.PushFC(new RegexFC(true));
					return;
				case 98:
					if (CurIndex == 0)
					{
						this.SkipChild();
						return;
					}
					break;
				default:
					switch (NodeType)
					{
					case 152:
					case 161:
						if (CurIndex != 0)
						{
							RegexFC regexFC = this.PopFC();
							RegexFC regexFC2 = this.TopFC();
							this._failed = !regexFC2.AddFC(regexFC, false);
							return;
						}
						break;
					case 153:
						if (CurIndex != 0)
						{
							RegexFC regexFC3 = this.PopFC();
							RegexFC regexFC4 = this.TopFC();
							this._failed = !regexFC4.AddFC(regexFC3, true);
						}
						if (!this.TopFC()._nullable)
						{
							this._skipAllChildren = true;
							return;
						}
						break;
					case 154:
					case 155:
						if (node.M == 0)
						{
							this.TopFC()._nullable = true;
							return;
						}
						break;
					case 156:
					case 157:
					case 158:
					case 159:
					case 160:
						break;
					case 162:
						if (CurIndex > 1)
						{
							RegexFC regexFC5 = this.PopFC();
							RegexFC regexFC6 = this.TopFC();
							this._failed = !regexFC6.AddFC(regexFC5, false);
							return;
						}
						break;
					default:
						goto IL_0312;
					}
					break;
				}
				return;
			}
			IL_0312:
			throw new ArgumentException(SR.Format("Unexpected opcode in regular expression generation: {0}.", NodeType.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x04000911 RID: 2321
		private const int StackBufferSize = 32;

		// Token: 0x04000912 RID: 2322
		private const int BeforeChild = 64;

		// Token: 0x04000913 RID: 2323
		private const int AfterChild = 128;

		// Token: 0x04000914 RID: 2324
		public const int Beginning = 1;

		// Token: 0x04000915 RID: 2325
		public const int Bol = 2;

		// Token: 0x04000916 RID: 2326
		public const int Start = 4;

		// Token: 0x04000917 RID: 2327
		public const int Eol = 8;

		// Token: 0x04000918 RID: 2328
		public const int EndZ = 16;

		// Token: 0x04000919 RID: 2329
		public const int End = 32;

		// Token: 0x0400091A RID: 2330
		public const int Boundary = 64;

		// Token: 0x0400091B RID: 2331
		public const int ECMABoundary = 128;

		// Token: 0x0400091C RID: 2332
		private readonly List<RegexFC> _fcStack;

		// Token: 0x0400091D RID: 2333
		private global::System.Collections.Generic.ValueListBuilder<int> _intStack;

		// Token: 0x0400091E RID: 2334
		private bool _skipAllChildren;

		// Token: 0x0400091F RID: 2335
		private bool _skipchild;

		// Token: 0x04000920 RID: 2336
		private bool _failed;
	}
}
