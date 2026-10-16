using System;
using System.Collections.Generic;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000018 RID: 24
	public class SourceMethodBuilder
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005344 File Offset: 0x00003544
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000537F File Offset: 0x0000357F
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000053A4 File Offset: 0x000035A4
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000053C0 File Offset: 0x000035C0
		public ICompileUnit SourceFile
		{
			get
			{
				return this._comp_unit;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000053C8 File Offset: 0x000035C8
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

		// Token: 0x060000B1 RID: 177 RVA: 0x000053E4 File Offset: 0x000035E4
		public SourceMethodBuilder(ICompileUnit comp_unit)
		{
			this._comp_unit = comp_unit;
			this.method_lines = new List<LineNumberEntry>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000053FE File Offset: 0x000035FE
		public SourceMethodBuilder(ICompileUnit comp_unit, int ns_id, IMethodDef method)
			: this(comp_unit)
		{
			this.ns_id = ns_id;
			this.method = method;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005415 File Offset: 0x00003615
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			this.MarkSequencePoint(offset, file, line, column, -1, -1, is_hidden);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005428 File Offset: 0x00003628
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

		// Token: 0x060000B5 RID: 181 RVA: 0x000054B8 File Offset: 0x000036B8
		public void StartBlock(CodeBlockEntry.Type type, int start_offset)
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
			CodeBlockEntry codeBlockEntry = new CodeBlockEntry(this._blocks.Count + 1, num, type, start_offset);
			this._block_stack.Push(codeBlockEntry);
			this._blocks.Add(codeBlockEntry);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005530 File Offset: 0x00003730
		public void EndBlock(int end_offset)
		{
			this._block_stack.Pop().Close(end_offset);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005544 File Offset: 0x00003744
		public void AddLocal(int index, string name)
		{
			if (this._locals == null)
			{
				this._locals = new List<LocalVariableEntry>();
			}
			int num = ((this.CurrentBlock != null) ? this.CurrentBlock.Index : 0);
			this._locals.Add(new LocalVariableEntry(index, name, num));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000558E File Offset: 0x0000378E
		public void AddScopeVariable(int scope, int index)
		{
			if (this._scope_vars == null)
			{
				this._scope_vars = new List<ScopeVariable>();
			}
			this._scope_vars.Add(new ScopeVariable(scope, index));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000055B5 File Offset: 0x000037B5
		public void DefineMethod(MonoSymbolFile file)
		{
			this.DefineMethod(file, this.method.Token);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000055CC File Offset: 0x000037CC
		public void DefineMethod(MonoSymbolFile file, int token)
		{
			MethodEntry methodEntry = new MethodEntry(file, this._comp_unit.Entry, token, this.ScopeVariables, this.Locals, this.method_lines.ToArray(), this.Blocks, null, MethodEntry.Flags.ColumnsInfoIncluded, this.ns_id);
			file.AddMethod(methodEntry);
		}

		// Token: 0x0400008A RID: 138
		private List<LocalVariableEntry> _locals;

		// Token: 0x0400008B RID: 139
		private List<CodeBlockEntry> _blocks;

		// Token: 0x0400008C RID: 140
		private List<ScopeVariable> _scope_vars;

		// Token: 0x0400008D RID: 141
		private Stack<CodeBlockEntry> _block_stack;

		// Token: 0x0400008E RID: 142
		private readonly List<LineNumberEntry> method_lines;

		// Token: 0x0400008F RID: 143
		private readonly ICompileUnit _comp_unit;

		// Token: 0x04000090 RID: 144
		private readonly int ns_id;

		// Token: 0x04000091 RID: 145
		private readonly IMethodDef method;
	}
}
