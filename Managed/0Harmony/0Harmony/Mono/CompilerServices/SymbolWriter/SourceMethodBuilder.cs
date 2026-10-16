using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000315 RID: 789
	internal class SourceMethodBuilder
	{
		// Token: 0x060013CD RID: 5069 RVA: 0x00040994 File Offset: 0x0003EB94
		public SourceMethodBuilder(ICompileUnit comp_unit)
		{
			this._comp_unit = comp_unit;
			this.method_lines = new List<LineNumberEntry>();
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000409AE File Offset: 0x0003EBAE
		public SourceMethodBuilder(ICompileUnit comp_unit, int ns_id, IMethodDef method)
			: this(comp_unit)
		{
			this.ns_id = ns_id;
			this.method = method;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x000409C5 File Offset: 0x0003EBC5
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			this.MarkSequencePoint(offset, file, line, column, -1, -1, is_hidden);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000409D8 File Offset: 0x0003EBD8
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, int end_line, int end_column, bool is_hidden)
		{
			LineNumberEntry lineNumberEntry = new LineNumberEntry((file != null) ? file.Index : 0, line, column, end_line, end_column, offset, is_hidden);
			if (this.method_lines.Count > 0)
			{
				LineNumberEntry lineNumberEntry2 = this.method_lines[this.method_lines.Count - 1];
				if (lineNumberEntry2.Offset == offset)
				{
					if (LineNumberEntry.LocationComparer.Default.Compare(lineNumberEntry, lineNumberEntry2) > 0)
					{
						this.method_lines[this.method_lines.Count - 1] = lineNumberEntry;
					}
					return;
				}
			}
			this.method_lines.Add(lineNumberEntry);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00040A66 File Offset: 0x0003EC66
		public void StartBlock(CodeBlockEntry.Type type, int start_offset)
		{
			this.StartBlock(type, start_offset, (this._blocks == null) ? 1 : (this._blocks.Count + 1));
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00040A88 File Offset: 0x0003EC88
		public void StartBlock(CodeBlockEntry.Type type, int start_offset, int scopeIndex)
		{
			if (this._block_stack == null)
			{
				this._block_stack = new Stack<CodeBlockEntry>();
			}
			if (this._blocks == null)
			{
				this._blocks = new List<CodeBlockEntry>();
			}
			int num = ((this.CurrentBlock != null) ? this.CurrentBlock.Index : (-1));
			CodeBlockEntry codeBlockEntry = new CodeBlockEntry(scopeIndex, num, type, start_offset);
			this._block_stack.Push(codeBlockEntry);
			this._blocks.Add(codeBlockEntry);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00040AF4 File Offset: 0x0003ECF4
		public void EndBlock(int end_offset)
		{
			this._block_stack.Pop().Close(end_offset);
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00040B08 File Offset: 0x0003ED08
		public CodeBlockEntry[] Blocks
		{
			get
			{
				if (this._blocks == null)
				{
					return new CodeBlockEntry[0];
				}
				CodeBlockEntry[] array = new CodeBlockEntry[this._blocks.Count];
				this._blocks.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00040B43 File Offset: 0x0003ED43
		public CodeBlockEntry CurrentBlock
		{
			get
			{
				if (this._block_stack != null && this._block_stack.Count > 0)
				{
					return this._block_stack.Peek();
				}
				return null;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00040B68 File Offset: 0x0003ED68
		public LocalVariableEntry[] Locals
		{
			get
			{
				if (this._locals == null)
				{
					return new LocalVariableEntry[0];
				}
				return this._locals.ToArray();
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00040B84 File Offset: 0x0003ED84
		public ICompileUnit SourceFile
		{
			get
			{
				return this._comp_unit;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00040B8C File Offset: 0x0003ED8C
		public void AddLocal(int index, string name)
		{
			if (this._locals == null)
			{
				this._locals = new List<LocalVariableEntry>();
			}
			int num = ((this.CurrentBlock != null) ? this.CurrentBlock.Index : 0);
			this._locals.Add(new LocalVariableEntry(index, name, num));
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00040BD6 File Offset: 0x0003EDD6
		public ScopeVariable[] ScopeVariables
		{
			get
			{
				if (this._scope_vars == null)
				{
					return new ScopeVariable[0];
				}
				return this._scope_vars.ToArray();
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00040BF2 File Offset: 0x0003EDF2
		public void AddScopeVariable(int scope, int index)
		{
			if (this._scope_vars == null)
			{
				this._scope_vars = new List<ScopeVariable>();
			}
			this._scope_vars.Add(new ScopeVariable(scope, index));
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00040C19 File Offset: 0x0003EE19
		public void DefineMethod(MonoSymbolFile file)
		{
			this.DefineMethod(file, this.method.Token);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00040C30 File Offset: 0x0003EE30
		public void DefineMethod(MonoSymbolFile file, int token)
		{
			CodeBlockEntry[] array = this.Blocks;
			if (array.Length != 0)
			{
				List<CodeBlockEntry> list = new List<CodeBlockEntry>(array.Length);
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					num = Math.Max(num, array[i].Index);
				}
				for (int j = 0; j < num; j++)
				{
					int num2 = j + 1;
					if (j < array.Length && array[j].Index == num2)
					{
						list.Add(array[j]);
					}
					else
					{
						bool flag = false;
						for (int k = 0; k < array.Length; k++)
						{
							if (array[k].Index == num2)
							{
								list.Add(array[k]);
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(new CodeBlockEntry(num2, -1, CodeBlockEntry.Type.CompilerGenerated, 0));
						}
					}
				}
				array = list.ToArray();
			}
			MethodEntry methodEntry = new MethodEntry(file, this._comp_unit.Entry, token, this.ScopeVariables, this.Locals, this.method_lines.ToArray(), array, null, MethodEntry.Flags.ColumnsInfoIncluded, this.ns_id);
			file.AddMethod(methodEntry);
		}

		// Token: 0x04000A50 RID: 2640
		private List<LocalVariableEntry> _locals;

		// Token: 0x04000A51 RID: 2641
		private List<CodeBlockEntry> _blocks;

		// Token: 0x04000A52 RID: 2642
		private List<ScopeVariable> _scope_vars;

		// Token: 0x04000A53 RID: 2643
		private Stack<CodeBlockEntry> _block_stack;

		// Token: 0x04000A54 RID: 2644
		private readonly List<LineNumberEntry> method_lines;

		// Token: 0x04000A55 RID: 2645
		private readonly ICompileUnit _comp_unit;

		// Token: 0x04000A56 RID: 2646
		private readonly int ns_id;

		// Token: 0x04000A57 RID: 2647
		private readonly IMethodDef method;
	}
}
