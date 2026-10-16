using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000311 RID: 785
	public class MethodEntry : IComparable
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0003FBFC File Offset: 0x0003DDFC
		public MethodEntry.Flags MethodFlags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0003FC04 File Offset: 0x0003DE04
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0003FC0C File Offset: 0x0003DE0C
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0003FC18 File Offset: 0x0003DE18
		internal MethodEntry(MonoSymbolFile file, MyBinaryReader reader, int index)
		{
			this.SymbolFile = file;
			this.index = index;
			this.Token = reader.ReadInt32();
			this.DataOffset = reader.ReadInt32();
			this.LineNumberTableOffset = reader.ReadInt32();
			long position = reader.BaseStream.Position;
			reader.BaseStream.Position = (long)this.DataOffset;
			this.CompileUnitIndex = reader.ReadLeb128();
			this.LocalVariableTableOffset = reader.ReadLeb128();
			this.NamespaceID = reader.ReadLeb128();
			this.CodeBlockTableOffset = reader.ReadLeb128();
			this.ScopeVariableTableOffset = reader.ReadLeb128();
			this.RealNameOffset = reader.ReadLeb128();
			this.flags = (MethodEntry.Flags)reader.ReadLeb128();
			reader.BaseStream.Position = position;
			this.CompileUnit = file.GetCompileUnit(this.CompileUnitIndex);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003FCF0 File Offset: 0x0003DEF0
		internal MethodEntry(MonoSymbolFile file, CompileUnitEntry comp_unit, int token, ScopeVariable[] scope_vars, LocalVariableEntry[] locals, LineNumberEntry[] lines, CodeBlockEntry[] code_blocks, string real_name, MethodEntry.Flags flags, int namespace_id)
		{
			this.SymbolFile = file;
			this.real_name = real_name;
			this.locals = locals;
			this.code_blocks = code_blocks;
			this.scope_vars = scope_vars;
			this.flags = flags;
			this.index = -1;
			this.Token = token;
			this.CompileUnitIndex = comp_unit.Index;
			this.CompileUnit = comp_unit;
			this.NamespaceID = namespace_id;
			MethodEntry.CheckLineNumberTable(lines);
			this.lnt = new LineNumberTable(file, lines);
			file.NumLineNumbers += lines.Length;
			int num = ((locals != null) ? locals.Length : 0);
			if (num <= 32)
			{
				for (int i = 0; i < num; i++)
				{
					string name = locals[i].Name;
					for (int j = i + 1; j < num; j++)
					{
						if (locals[j].Name == name)
						{
							flags |= MethodEntry.Flags.LocalNamesAmbiguous;
							return;
						}
					}
				}
				return;
			}
			Dictionary<string, LocalVariableEntry> dictionary = new Dictionary<string, LocalVariableEntry>();
			foreach (LocalVariableEntry localVariableEntry in locals)
			{
				if (dictionary.ContainsKey(localVariableEntry.Name))
				{
					flags |= MethodEntry.Flags.LocalNamesAmbiguous;
					return;
				}
				dictionary.Add(localVariableEntry.Name, localVariableEntry);
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003FE28 File Offset: 0x0003E028
		private static void CheckLineNumberTable(LineNumberEntry[] line_numbers)
		{
			int num = -1;
			int num2 = -1;
			if (line_numbers == null)
			{
				return;
			}
			foreach (LineNumberEntry lineNumberEntry in line_numbers)
			{
				if (lineNumberEntry.Equals(LineNumberEntry.Null))
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset < num)
				{
					throw new MonoSymbolFileException();
				}
				if (lineNumberEntry.Offset > num)
				{
					num2 = lineNumberEntry.Row;
					num = lineNumberEntry.Offset;
				}
				else if (lineNumberEntry.Row > num2)
				{
					num2 = lineNumberEntry.Row;
				}
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003FE9A File Offset: 0x0003E09A
		internal void Write(MyBinaryWriter bw)
		{
			if (this.index <= 0 || this.DataOffset == 0)
			{
				throw new InvalidOperationException();
			}
			bw.Write(this.Token);
			bw.Write(this.DataOffset);
			bw.Write(this.LineNumberTableOffset);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0003FED8 File Offset: 0x0003E0D8
		internal void WriteData(MonoSymbolFile file, MyBinaryWriter bw)
		{
			if (this.index <= 0)
			{
				throw new InvalidOperationException();
			}
			this.LocalVariableTableOffset = (int)bw.BaseStream.Position;
			int num = ((this.locals != null) ? this.locals.Length : 0);
			bw.WriteLeb128(num);
			for (int i = 0; i < num; i++)
			{
				this.locals[i].Write(file, bw);
			}
			file.LocalCount += num;
			this.CodeBlockTableOffset = (int)bw.BaseStream.Position;
			int num2 = ((this.code_blocks != null) ? this.code_blocks.Length : 0);
			bw.WriteLeb128(num2);
			for (int j = 0; j < num2; j++)
			{
				this.code_blocks[j].Write(bw);
			}
			this.ScopeVariableTableOffset = (int)bw.BaseStream.Position;
			int num3 = ((this.scope_vars != null) ? this.scope_vars.Length : 0);
			bw.WriteLeb128(num3);
			for (int k = 0; k < num3; k++)
			{
				this.scope_vars[k].Write(bw);
			}
			if (this.real_name != null)
			{
				this.RealNameOffset = (int)bw.BaseStream.Position;
				bw.Write(this.real_name);
			}
			foreach (LineNumberEntry lineNumberEntry in this.lnt.LineNumbers)
			{
				if (lineNumberEntry.EndRow != -1 || lineNumberEntry.EndColumn != -1)
				{
					this.flags |= MethodEntry.Flags.EndInfoIncluded;
				}
			}
			this.LineNumberTableOffset = (int)bw.BaseStream.Position;
			this.lnt.Write(file, bw, (this.flags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0, (this.flags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0);
			this.DataOffset = (int)bw.BaseStream.Position;
			bw.WriteLeb128(this.CompileUnitIndex);
			bw.WriteLeb128(this.LocalVariableTableOffset);
			bw.WriteLeb128(this.NamespaceID);
			bw.WriteLeb128(this.CodeBlockTableOffset);
			bw.WriteLeb128(this.ScopeVariableTableOffset);
			bw.WriteLeb128(this.RealNameOffset);
			bw.WriteLeb128((int)this.flags);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000400F4 File Offset: 0x0003E2F4
		public void ReadAll()
		{
			this.GetLineNumberTable();
			this.GetLocals();
			this.GetCodeBlocks();
			this.GetScopeVariables();
			this.GetRealName();
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004011C File Offset: 0x0003E31C
		public LineNumberTable GetLineNumberTable()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			LineNumberTable lineNumberTable;
			lock (symbolFile)
			{
				if (this.lnt != null)
				{
					lineNumberTable = this.lnt;
				}
				else if (this.LineNumberTableOffset == 0)
				{
					lineNumberTable = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.LineNumberTableOffset;
					this.lnt = LineNumberTable.Read(this.SymbolFile, binaryReader, (this.flags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0, (this.flags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0);
					binaryReader.BaseStream.Position = position;
					lineNumberTable = this.lnt;
				}
			}
			return lineNumberTable;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000401E0 File Offset: 0x0003E3E0
		public LocalVariableEntry[] GetLocals()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			LocalVariableEntry[] array;
			lock (symbolFile)
			{
				if (this.locals != null)
				{
					array = this.locals;
				}
				else if (this.LocalVariableTableOffset == 0)
				{
					array = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.LocalVariableTableOffset;
					int num = binaryReader.ReadLeb128();
					this.locals = new LocalVariableEntry[num];
					for (int i = 0; i < num; i++)
					{
						this.locals[i] = new LocalVariableEntry(this.SymbolFile, binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					array = this.locals;
				}
			}
			return array;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000402C0 File Offset: 0x0003E4C0
		public CodeBlockEntry[] GetCodeBlocks()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			CodeBlockEntry[] array;
			lock (symbolFile)
			{
				if (this.code_blocks != null)
				{
					array = this.code_blocks;
				}
				else if (this.CodeBlockTableOffset == 0)
				{
					array = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.CodeBlockTableOffset;
					int num = binaryReader.ReadLeb128();
					this.code_blocks = new CodeBlockEntry[num];
					for (int i = 0; i < num; i++)
					{
						this.code_blocks[i] = new CodeBlockEntry(i, binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					array = this.code_blocks;
				}
			}
			return array;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00040398 File Offset: 0x0003E598
		public ScopeVariable[] GetScopeVariables()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			ScopeVariable[] array;
			lock (symbolFile)
			{
				if (this.scope_vars != null)
				{
					array = this.scope_vars;
				}
				else if (this.ScopeVariableTableOffset == 0)
				{
					array = null;
				}
				else
				{
					MyBinaryReader binaryReader = this.SymbolFile.BinaryReader;
					long position = binaryReader.BaseStream.Position;
					binaryReader.BaseStream.Position = (long)this.ScopeVariableTableOffset;
					int num = binaryReader.ReadLeb128();
					this.scope_vars = new ScopeVariable[num];
					for (int i = 0; i < num; i++)
					{
						this.scope_vars[i] = new ScopeVariable(binaryReader);
					}
					binaryReader.BaseStream.Position = position;
					array = this.scope_vars;
				}
			}
			return array;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00040470 File Offset: 0x0003E670
		public string GetRealName()
		{
			MonoSymbolFile symbolFile = this.SymbolFile;
			string text;
			lock (symbolFile)
			{
				if (this.real_name != null)
				{
					text = this.real_name;
				}
				else if (this.RealNameOffset == 0)
				{
					text = null;
				}
				else
				{
					this.real_name = this.SymbolFile.BinaryReader.ReadString(this.RealNameOffset);
					text = this.real_name;
				}
			}
			return text;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000404EC File Offset: 0x0003E6EC
		public int CompareTo(object obj)
		{
			MethodEntry methodEntry = (MethodEntry)obj;
			if (methodEntry.Token < this.Token)
			{
				return 1;
			}
			if (methodEntry.Token > this.Token)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00040524 File Offset: 0x0003E724
		public override string ToString()
		{
			return string.Format("[Method {0}:{1:x}:{2}:{3}]", new object[] { this.index, this.Token, this.CompileUnitIndex, this.CompileUnit });
		}

		// Token: 0x04000A2E RID: 2606
		public readonly int CompileUnitIndex;

		// Token: 0x04000A2F RID: 2607
		public readonly int Token;

		// Token: 0x04000A30 RID: 2608
		public readonly int NamespaceID;

		// Token: 0x04000A31 RID: 2609
		private int DataOffset;

		// Token: 0x04000A32 RID: 2610
		private int LocalVariableTableOffset;

		// Token: 0x04000A33 RID: 2611
		private int LineNumberTableOffset;

		// Token: 0x04000A34 RID: 2612
		private int CodeBlockTableOffset;

		// Token: 0x04000A35 RID: 2613
		private int ScopeVariableTableOffset;

		// Token: 0x04000A36 RID: 2614
		private int RealNameOffset;

		// Token: 0x04000A37 RID: 2615
		private MethodEntry.Flags flags;

		// Token: 0x04000A38 RID: 2616
		private int index;

		// Token: 0x04000A39 RID: 2617
		public readonly CompileUnitEntry CompileUnit;

		// Token: 0x04000A3A RID: 2618
		private LocalVariableEntry[] locals;

		// Token: 0x04000A3B RID: 2619
		private CodeBlockEntry[] code_blocks;

		// Token: 0x04000A3C RID: 2620
		private ScopeVariable[] scope_vars;

		// Token: 0x04000A3D RID: 2621
		private LineNumberTable lnt;

		// Token: 0x04000A3E RID: 2622
		private string real_name;

		// Token: 0x04000A3F RID: 2623
		public readonly MonoSymbolFile SymbolFile;

		// Token: 0x04000A40 RID: 2624
		public const int Size = 12;

		// Token: 0x02000312 RID: 786
		[Flags]
		public enum Flags
		{
			// Token: 0x04000A42 RID: 2626
			LocalNamesAmbiguous = 1,
			// Token: 0x04000A43 RID: 2627
			ColumnsInfoIncluded = 2,
			// Token: 0x04000A44 RID: 2628
			EndInfoIncluded = 4
		}
	}
}
