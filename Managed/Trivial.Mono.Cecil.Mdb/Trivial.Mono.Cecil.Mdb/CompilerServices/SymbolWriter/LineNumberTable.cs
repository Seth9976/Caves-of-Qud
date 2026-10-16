using System;
using System.Collections.Generic;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200000B RID: 11
	public class LineNumberTable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002909 File Offset: 0x00000B09
		public LineNumberEntry[] LineNumbers
		{
			get
			{
				return this._line_numbers;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002914 File Offset: 0x00000B14
		protected LineNumberTable(MonoSymbolFile file)
		{
			this.LineBase = file.OffsetTable.LineNumberTable_LineBase;
			this.LineRange = file.OffsetTable.LineNumberTable_LineRange;
			this.OpcodeBase = (byte)file.OffsetTable.LineNumberTable_OpcodeBase;
			this.MaxAddressIncrement = (int)(byte.MaxValue - this.OpcodeBase) / this.LineRange;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002974 File Offset: 0x00000B74
		internal LineNumberTable(MonoSymbolFile file, LineNumberEntry[] lines)
			: this(file)
		{
			this._line_numbers = lines;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002984 File Offset: 0x00000B84
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw, bool hasColumnsInfo, bool hasEndInfo)
		{
			int num = (int)bw.BaseStream.Position;
			bool flag = false;
			int num2 = 1;
			int num3 = 0;
			int num4 = 1;
			for (int i = 0; i < this.LineNumbers.Length; i++)
			{
				int num5 = this.LineNumbers[i].Row - num2;
				int num6 = this.LineNumbers[i].Offset - num3;
				if (this.LineNumbers[i].File != num4)
				{
					bw.Write(4);
					bw.WriteLeb128(this.LineNumbers[i].File);
					num4 = this.LineNumbers[i].File;
				}
				if (this.LineNumbers[i].IsHidden != flag)
				{
					bw.Write(0);
					bw.Write(1);
					bw.Write(64);
					flag = this.LineNumbers[i].IsHidden;
				}
				if (num6 >= this.MaxAddressIncrement)
				{
					if (num6 < 2 * this.MaxAddressIncrement)
					{
						bw.Write(8);
						num6 -= this.MaxAddressIncrement;
					}
					else
					{
						bw.Write(2);
						bw.WriteLeb128(num6);
						num6 = 0;
					}
				}
				if (num5 < this.LineBase || num5 >= this.LineBase + this.LineRange)
				{
					bw.Write(3);
					bw.WriteLeb128(num5);
					if (num6 != 0)
					{
						bw.Write(2);
						bw.WriteLeb128(num6);
					}
					bw.Write(1);
				}
				else
				{
					byte b = (byte)(num5 - this.LineBase + this.LineRange * num6 + (int)this.OpcodeBase);
					bw.Write(b);
				}
				num2 = this.LineNumbers[i].Row;
				num3 = this.LineNumbers[i].Offset;
			}
			bw.Write(0);
			bw.Write(1);
			bw.Write(1);
			if (hasColumnsInfo)
			{
				for (int j = 0; j < this.LineNumbers.Length; j++)
				{
					LineNumberEntry lineNumberEntry = this.LineNumbers[j];
					if (lineNumberEntry.Row >= 0)
					{
						bw.WriteLeb128(lineNumberEntry.Column);
					}
				}
			}
			if (hasEndInfo)
			{
				for (int k = 0; k < this.LineNumbers.Length; k++)
				{
					LineNumberEntry lineNumberEntry2 = this.LineNumbers[k];
					if (lineNumberEntry2.EndRow == -1 || lineNumberEntry2.EndColumn == -1 || lineNumberEntry2.Row > lineNumberEntry2.EndRow)
					{
						bw.WriteLeb128(16777215);
					}
					else
					{
						bw.WriteLeb128(lineNumberEntry2.EndRow - lineNumberEntry2.Row);
						bw.WriteLeb128(lineNumberEntry2.EndColumn);
					}
				}
			}
			file.ExtendedLineNumberSize += (int)bw.BaseStream.Position - num;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C08 File Offset: 0x00000E08
		internal static LineNumberTable Read(MonoSymbolFile file, MyBinaryReader br, bool readColumnsInfo, bool readEndInfo)
		{
			LineNumberTable lineNumberTable = new LineNumberTable(file);
			lineNumberTable.DoRead(file, br, readColumnsInfo, readEndInfo);
			return lineNumberTable;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C28 File Offset: 0x00000E28
		private void DoRead(MonoSymbolFile file, MyBinaryReader br, bool includesColumns, bool includesEnds)
		{
			List<LineNumberEntry> list = new List<LineNumberEntry>();
			bool flag = false;
			bool flag2 = false;
			int num = 1;
			int num2 = 0;
			int num3 = 1;
			byte b;
			for (;;)
			{
				b = br.ReadByte();
				if (b == 0)
				{
					byte b2 = br.ReadByte();
					long num4 = br.BaseStream.Position + (long)((ulong)b2);
					b = br.ReadByte();
					switch (b)
					{
					case 1:
						goto IL_024C;
					case 64:
						flag = !flag;
						flag2 = true;
						goto IL_0336;
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
					case 70:
					case 71:
					case 72:
					case 73:
					case 74:
					case 75:
					case 76:
					case 77:
					case 78:
					case 79:
					case 80:
					case 81:
					case 82:
					case 83:
					case 84:
					case 85:
					case 86:
					case 87:
					case 88:
					case 89:
					case 90:
					case 91:
					case 92:
					case 93:
					case 94:
					case 95:
					case 96:
					case 97:
					case 98:
					case 99:
					case 100:
					case 101:
					case 102:
					case 103:
					case 104:
					case 105:
					case 106:
					case 107:
					case 108:
					case 109:
					case 110:
					case 111:
					case 112:
					case 113:
					case 114:
					case 115:
					case 116:
					case 117:
					case 118:
					case 119:
					case 120:
					case 121:
					case 122:
					case 123:
					case 124:
					case 125:
					case 126:
					case 127:
						goto IL_0336;
					}
					break;
					IL_0336:
					br.BaseStream.Position = num4;
				}
				else
				{
					if (b < this.OpcodeBase)
					{
						switch (b)
						{
						case 1:
							list.Add(new LineNumberEntry(num3, num, -1, num2, flag));
							flag2 = false;
							continue;
						case 2:
							num2 += br.ReadLeb128();
							flag2 = true;
							continue;
						case 3:
							num += br.ReadLeb128();
							flag2 = true;
							continue;
						case 4:
							num3 = br.ReadLeb128();
							flag2 = true;
							continue;
						case 8:
							num2 += this.MaxAddressIncrement;
							flag2 = true;
							continue;
						}
						goto Block_10;
					}
					b -= this.OpcodeBase;
					num2 += (int)b / this.LineRange;
					num += this.LineBase + (int)b % this.LineRange;
					list.Add(new LineNumberEntry(num3, num, -1, num2, flag));
					flag2 = false;
				}
			}
			goto IL_031B;
			IL_024C:
			if (flag2)
			{
				list.Add(new LineNumberEntry(num3, num, -1, num2, flag));
			}
			this._line_numbers = list.ToArray();
			if (includesColumns)
			{
				for (int i = 0; i < this._line_numbers.Length; i++)
				{
					LineNumberEntry lineNumberEntry = this._line_numbers[i];
					if (lineNumberEntry.Row >= 0)
					{
						lineNumberEntry.Column = br.ReadLeb128();
					}
				}
			}
			if (!includesEnds)
			{
				return;
			}
			for (int j = 0; j < this._line_numbers.Length; j++)
			{
				LineNumberEntry lineNumberEntry2 = this._line_numbers[j];
				int num5 = br.ReadLeb128();
				if (num5 == 16777215)
				{
					lineNumberEntry2.EndRow = -1;
					lineNumberEntry2.EndColumn = -1;
				}
				else
				{
					lineNumberEntry2.EndRow = lineNumberEntry2.Row + num5;
					lineNumberEntry2.EndColumn = br.ReadLeb128();
				}
			}
			return;
			IL_031B:
			throw new MonoSymbolFileException("Unknown extended opcode {0:x}", new object[] { b });
			Block_10:
			throw new MonoSymbolFileException("Unknown standard opcode {0:x} in LNT", new object[] { b });
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003071 File Offset: 0x00001271
		public bool GetMethodBounds(out LineNumberEntry start, out LineNumberEntry end)
		{
			if (this._line_numbers.Length > 1)
			{
				start = this._line_numbers[0];
				end = this._line_numbers[this._line_numbers.Length - 1];
				return true;
			}
			start = LineNumberEntry.Null;
			end = LineNumberEntry.Null;
			return false;
		}

		// Token: 0x0400001D RID: 29
		protected LineNumberEntry[] _line_numbers;

		// Token: 0x0400001E RID: 30
		public readonly int LineBase;

		// Token: 0x0400001F RID: 31
		public readonly int LineRange;

		// Token: 0x04000020 RID: 32
		public readonly byte OpcodeBase;

		// Token: 0x04000021 RID: 33
		public readonly int MaxAddressIncrement;

		// Token: 0x04000022 RID: 34
		public const int Default_LineBase = -1;

		// Token: 0x04000023 RID: 35
		public const int Default_LineRange = 8;

		// Token: 0x04000024 RID: 36
		public const byte Default_OpcodeBase = 9;

		// Token: 0x04000025 RID: 37
		public const byte DW_LNS_copy = 1;

		// Token: 0x04000026 RID: 38
		public const byte DW_LNS_advance_pc = 2;

		// Token: 0x04000027 RID: 39
		public const byte DW_LNS_advance_line = 3;

		// Token: 0x04000028 RID: 40
		public const byte DW_LNS_set_file = 4;

		// Token: 0x04000029 RID: 41
		public const byte DW_LNS_const_add_pc = 8;

		// Token: 0x0400002A RID: 42
		public const byte DW_LNE_end_sequence = 1;

		// Token: 0x0400002B RID: 43
		public const byte DW_LNE_MONO_negate_is_hidden = 64;

		// Token: 0x0400002C RID: 44
		internal const byte DW_LNE_MONO__extensions_start = 64;

		// Token: 0x0400002D RID: 45
		internal const byte DW_LNE_MONO__extensions_end = 127;
	}
}
