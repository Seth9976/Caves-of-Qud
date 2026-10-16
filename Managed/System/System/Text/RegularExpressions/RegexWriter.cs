using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000212 RID: 530
	internal ref struct RegexWriter
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x00043F63 File Offset: 0x00042163
		private RegexWriter(Span<int> emittedSpan, Span<int> intStackSpan)
		{
			this._emitted = new global::System.Collections.Generic.ValueListBuilder<int>(emittedSpan);
			this._intStack = new global::System.Collections.Generic.ValueListBuilder<int>(intStackSpan);
			this._stringHash = new Dictionary<string, int>();
			this._stringTable = new List<string>();
			this._caps = null;
			this._trackCount = 0;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00043FA4 File Offset: 0x000421A4
		public unsafe static RegexCode Write(RegexTree tree)
		{
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)224], 56);
			Span<int> span2 = new Span<int>(stackalloc byte[(UIntPtr)128], 32);
			RegexWriter regexWriter = new RegexWriter(span, span2);
			RegexCode regexCode = regexWriter.RegexCodeFromRegexTree(tree);
			regexWriter.Dispose();
			return regexCode;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00043FE9 File Offset: 0x000421E9
		public void Dispose()
		{
			this._emitted.Dispose();
			this._intStack.Dispose();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00044004 File Offset: 0x00042204
		public RegexCode RegexCodeFromRegexTree(RegexTree tree)
		{
			int num;
			if (tree.CapNumList == null || tree.CapTop == tree.CapNumList.Length)
			{
				num = tree.CapTop;
				this._caps = null;
			}
			else
			{
				num = tree.CapNumList.Length;
				this._caps = tree.Caps;
				for (int i = 0; i < tree.CapNumList.Length; i++)
				{
					this._caps[tree.CapNumList[i]] = i;
				}
			}
			RegexNode regexNode = tree.Root;
			int num2 = 0;
			this.Emit(23, 0);
			for (;;)
			{
				if (regexNode.Children == null)
				{
					this.EmitFragment(regexNode.NType, regexNode, 0);
				}
				else if (num2 < regexNode.Children.Count)
				{
					this.EmitFragment(regexNode.NType | 64, regexNode, num2);
					regexNode = regexNode.Children[num2];
					this._intStack.Append(num2);
					num2 = 0;
					continue;
				}
				if (this._intStack.Length == 0)
				{
					break;
				}
				num2 = this._intStack.Pop();
				regexNode = regexNode.Next;
				this.EmitFragment(regexNode.NType | 128, regexNode, num2);
				num2++;
			}
			this.PatchJump(0, this._emitted.Length);
			this.Emit(40);
			RegexPrefix? regexPrefix = RegexFCD.FirstChars(tree);
			RegexPrefix regexPrefix2 = RegexFCD.Prefix(tree);
			bool flag = (tree.Options & RegexOptions.RightToLeft) > RegexOptions.None;
			CultureInfo cultureInfo = (((tree.Options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			RegexBoyerMoore regexBoyerMoore;
			if (regexPrefix2.Prefix.Length > 0)
			{
				regexBoyerMoore = new RegexBoyerMoore(regexPrefix2.Prefix, regexPrefix2.CaseInsensitive, flag, cultureInfo);
			}
			else
			{
				regexBoyerMoore = null;
			}
			int num3 = RegexFCD.Anchors(tree);
			return new RegexCode(this._emitted.AsSpan().ToArray(), this._stringTable, this._trackCount, this._caps, num, regexBoyerMoore, regexPrefix, num3, flag);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000441E3 File Offset: 0x000423E3
		private unsafe void PatchJump(int offset, int jumpDest)
		{
			*this._emitted[offset + 1] = jumpDest;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000441F5 File Offset: 0x000423F5
		private void Emit(int op)
		{
			if (RegexCode.OpcodeBacktracks(op))
			{
				this._trackCount++;
			}
			this._emitted.Append(op);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00044219 File Offset: 0x00042419
		private void Emit(int op, int opd1)
		{
			if (RegexCode.OpcodeBacktracks(op))
			{
				this._trackCount++;
			}
			this._emitted.Append(op);
			this._emitted.Append(opd1);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00044249 File Offset: 0x00042449
		private void Emit(int op, int opd1, int opd2)
		{
			if (RegexCode.OpcodeBacktracks(op))
			{
				this._trackCount++;
			}
			this._emitted.Append(op);
			this._emitted.Append(opd1);
			this._emitted.Append(opd2);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00044288 File Offset: 0x00042488
		private int StringCode(string str)
		{
			if (str == null)
			{
				str = string.Empty;
			}
			int count;
			if (!this._stringHash.TryGetValue(str, out count))
			{
				count = this._stringTable.Count;
				this._stringHash[str] = count;
				this._stringTable.Add(str);
			}
			return count;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000442D5 File Offset: 0x000424D5
		private int MapCapnum(int capnum)
		{
			if (capnum == -1)
			{
				return -1;
			}
			if (this._caps != null)
			{
				return (int)this._caps[capnum];
			}
			return capnum;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00044300 File Offset: 0x00042500
		private void EmitFragment(int nodetype, RegexNode node, int curIndex)
		{
			int num = 0;
			if (nodetype <= 13)
			{
				if (node.UseOptionR())
				{
					num |= 64;
				}
				if ((node.Options & RegexOptions.IgnoreCase) != RegexOptions.None)
				{
					num |= 512;
				}
			}
			switch (nodetype)
			{
			case 3:
			case 4:
			case 6:
			case 7:
				if (node.M > 0)
				{
					this.Emit(((node.NType == 3 || node.NType == 6) ? 0 : 1) | num, (int)node.Ch, node.M);
				}
				if (node.N > node.M)
				{
					this.Emit(node.NType | num, (int)node.Ch, (node.N == int.MaxValue) ? int.MaxValue : (node.N - node.M));
					return;
				}
				return;
			case 5:
			case 8:
				if (node.M > 0)
				{
					this.Emit(2 | num, this.StringCode(node.Str), node.M);
				}
				if (node.N > node.M)
				{
					this.Emit(node.NType | num, this.StringCode(node.Str), (node.N == int.MaxValue) ? int.MaxValue : (node.N - node.M));
					return;
				}
				return;
			case 9:
			case 10:
				this.Emit(node.NType | num, (int)node.Ch);
				return;
			case 11:
				this.Emit(node.NType | num, this.StringCode(node.Str));
				return;
			case 12:
				this.Emit(node.NType | num, this.StringCode(node.Str));
				return;
			case 13:
				this.Emit(node.NType | num, this.MapCapnum(node.M));
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
				this.Emit(node.NType);
				return;
			case 23:
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
				switch (nodetype)
				{
				case 88:
					if (curIndex < node.Children.Count - 1)
					{
						this._intStack.Append(this._emitted.Length);
						this.Emit(23, 0);
						return;
					}
					return;
				case 89:
				case 93:
					return;
				case 90:
				case 91:
					if (node.N < 2147483647 || node.M > 1)
					{
						this.Emit((node.M == 0) ? 26 : 27, (node.M == 0) ? 0 : (1 - node.M));
					}
					else
					{
						this.Emit((node.M == 0) ? 30 : 31);
					}
					if (node.M == 0)
					{
						this._intStack.Append(this._emitted.Length);
						this.Emit(38, 0);
					}
					this._intStack.Append(this._emitted.Length);
					return;
				case 92:
					this.Emit(31);
					return;
				case 94:
					this.Emit(34);
					this.Emit(31);
					return;
				case 95:
					this.Emit(34);
					this._intStack.Append(this._emitted.Length);
					this.Emit(23, 0);
					return;
				case 96:
					this.Emit(34);
					return;
				case 97:
					if (curIndex == 0)
					{
						this.Emit(34);
						this._intStack.Append(this._emitted.Length);
						this.Emit(23, 0);
						this.Emit(37, this.MapCapnum(node.M));
						this.Emit(36);
						return;
					}
					return;
				case 98:
					if (curIndex == 0)
					{
						this.Emit(34);
						this.Emit(31);
						this._intStack.Append(this._emitted.Length);
						this.Emit(23, 0);
						return;
					}
					return;
				default:
					switch (nodetype)
					{
					case 152:
					{
						if (curIndex < node.Children.Count - 1)
						{
							int num2 = this._intStack.Pop();
							this._intStack.Append(this._emitted.Length);
							this.Emit(38, 0);
							this.PatchJump(num2, this._emitted.Length);
							return;
						}
						for (int i = 0; i < curIndex; i++)
						{
							this.PatchJump(this._intStack.Pop(), this._emitted.Length);
						}
						return;
					}
					case 153:
					case 157:
						return;
					case 154:
					case 155:
					{
						int length = this._emitted.Length;
						int num3 = nodetype - 154;
						if (node.N < 2147483647 || node.M > 1)
						{
							this.Emit(28 + num3, this._intStack.Pop(), (node.N == int.MaxValue) ? int.MaxValue : (node.N - node.M));
						}
						else
						{
							this.Emit(24 + num3, this._intStack.Pop());
						}
						if (node.M == 0)
						{
							this.PatchJump(this._intStack.Pop(), length);
							return;
						}
						return;
					}
					case 156:
						this.Emit(32, this.MapCapnum(node.M), this.MapCapnum(node.N));
						return;
					case 158:
						this.Emit(33);
						this.Emit(36);
						return;
					case 159:
						this.Emit(35);
						this.PatchJump(this._intStack.Pop(), this._emitted.Length);
						this.Emit(36);
						return;
					case 160:
						this.Emit(36);
						return;
					case 161:
						if (curIndex != 0)
						{
							if (curIndex != 1)
							{
								return;
							}
						}
						else
						{
							int num4 = this._intStack.Pop();
							this._intStack.Append(this._emitted.Length);
							this.Emit(38, 0);
							this.PatchJump(num4, this._emitted.Length);
							this.Emit(36);
							if (node.Children.Count > 1)
							{
								return;
							}
						}
						this.PatchJump(this._intStack.Pop(), this._emitted.Length);
						return;
					case 162:
						switch (curIndex)
						{
						case 0:
							this.Emit(33);
							this.Emit(36);
							return;
						case 1:
						{
							int num5 = this._intStack.Pop();
							this._intStack.Append(this._emitted.Length);
							this.Emit(38, 0);
							this.PatchJump(num5, this._emitted.Length);
							this.Emit(33);
							this.Emit(36);
							if (node.Children.Count > 2)
							{
								return;
							}
							break;
						}
						case 2:
							break;
						default:
							return;
						}
						this.PatchJump(this._intStack.Pop(), this._emitted.Length);
						return;
					}
					break;
				}
				break;
			}
			throw new ArgumentException(SR.Format("Unexpected opcode in regular expression generation: {0}.", nodetype.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x040009A4 RID: 2468
		private const int BeforeChild = 64;

		// Token: 0x040009A5 RID: 2469
		private const int AfterChild = 128;

		// Token: 0x040009A6 RID: 2470
		private const int EmittedSize = 56;

		// Token: 0x040009A7 RID: 2471
		private const int IntStackSize = 32;

		// Token: 0x040009A8 RID: 2472
		private global::System.Collections.Generic.ValueListBuilder<int> _emitted;

		// Token: 0x040009A9 RID: 2473
		private global::System.Collections.Generic.ValueListBuilder<int> _intStack;

		// Token: 0x040009AA RID: 2474
		private readonly Dictionary<string, int> _stringHash;

		// Token: 0x040009AB RID: 2475
		private readonly List<string> _stringTable;

		// Token: 0x040009AC RID: 2476
		private Hashtable _caps;

		// Token: 0x040009AD RID: 2477
		private int _trackCount;
	}
}
