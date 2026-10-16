using System;
using System.Collections.Generic;
using System.IO;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000010 RID: 16
	public class MonoSymbolWriter
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004861 File Offset: 0x00002A61
		public MonoSymbolFile SymbolFile
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000486C File Offset: 0x00002A6C
		public MonoSymbolWriter(string filename)
		{
			this.methods = new List<SourceMethodBuilder>();
			this.sources = new List<SourceFileEntry>();
			this.comp_units = new List<CompileUnitEntry>();
			this.file = new MonoSymbolFile();
			this.filename = filename + ".mdb";
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void CloseNamespace()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000048C9 File Offset: 0x00002AC9
		public void DefineLocalVariable(int index, string name)
		{
			if (this.current_method != null)
			{
				this.current_method.AddLocal(index, name);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000048E0 File Offset: 0x00002AE0
		public void DefineCapturedLocal(int scope_id, string name, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Local);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000048F1 File Offset: 0x00002AF1
		public void DefineCapturedParameter(int scope_id, string name, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Parameter);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004902 File Offset: 0x00002B02
		public void DefineCapturedThis(int scope_id, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, "this", captured_name, CapturedVariable.CapturedKind.This);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004917 File Offset: 0x00002B17
		public void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			this.file.DefineCapturedScope(scope_id, id, captured_name);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004927 File Offset: 0x00002B27
		public void DefineScopeVariable(int scope, int index)
		{
			if (this.current_method != null)
			{
				this.current_method.AddScopeVariable(scope, index);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000493E File Offset: 0x00002B3E
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			if (this.current_method != null)
			{
				this.current_method.MarkSequencePoint(offset, file, line, column, is_hidden);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000495C File Offset: 0x00002B5C
		public SourceMethodBuilder OpenMethod(ICompileUnit file, int ns_id, IMethodDef method)
		{
			SourceMethodBuilder sourceMethodBuilder = new SourceMethodBuilder(file, ns_id, method);
			this.current_method_stack.Push(this.current_method);
			this.current_method = sourceMethodBuilder;
			this.methods.Add(this.current_method);
			return sourceMethodBuilder;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000499C File Offset: 0x00002B9C
		public void CloseMethod()
		{
			this.current_method = this.current_method_stack.Pop();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000049B0 File Offset: 0x00002BB0
		public SourceFileEntry DefineDocument(string url)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(this.file, url);
			this.sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000049D8 File Offset: 0x00002BD8
		public SourceFileEntry DefineDocument(string url, byte[] guid, byte[] checksum)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(this.file, url, guid, checksum);
			this.sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004A04 File Offset: 0x00002C04
		public CompileUnitEntry DefineCompilationUnit(SourceFileEntry source)
		{
			CompileUnitEntry compileUnitEntry = new CompileUnitEntry(this.file, source);
			this.comp_units.Add(compileUnitEntry);
			return compileUnitEntry;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004A2B File Offset: 0x00002C2B
		public int DefineNamespace(string name, CompileUnitEntry unit, string[] using_clauses, int parent)
		{
			if (unit == null || using_clauses == null)
			{
				throw new NullReferenceException();
			}
			return unit.DefineNamespace(name, using_clauses, parent);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A43 File Offset: 0x00002C43
		public int OpenScope(int start_offset)
		{
			if (this.current_method == null)
			{
				return 0;
			}
			this.current_method.StartBlock(CodeBlockEntry.Type.Lexical, start_offset);
			return 0;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A5D File Offset: 0x00002C5D
		public void CloseScope(int end_offset)
		{
			if (this.current_method != null)
			{
				this.current_method.EndBlock(end_offset);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004A73 File Offset: 0x00002C73
		public void OpenCompilerGeneratedBlock(int start_offset)
		{
			if (this.current_method != null)
			{
				this.current_method.StartBlock(CodeBlockEntry.Type.CompilerGenerated, start_offset);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004A5D File Offset: 0x00002C5D
		public void CloseCompilerGeneratedBlock(int end_offset)
		{
			if (this.current_method != null)
			{
				this.current_method.EndBlock(end_offset);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004A8A File Offset: 0x00002C8A
		public void StartIteratorBody(int start_offset)
		{
			this.current_method.StartBlock(CodeBlockEntry.Type.IteratorBody, start_offset);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004A99 File Offset: 0x00002C99
		public void EndIteratorBody(int end_offset)
		{
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public void StartIteratorDispatcher(int start_offset)
		{
			this.current_method.StartBlock(CodeBlockEntry.Type.IteratorDispatcher, start_offset);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004A99 File Offset: 0x00002C99
		public void EndIteratorDispatcher(int end_offset)
		{
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004AB6 File Offset: 0x00002CB6
		public void DefineAnonymousScope(int id)
		{
			this.file.DefineAnonymousScope(id);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public void WriteSymbolFile(Guid guid)
		{
			foreach (SourceMethodBuilder sourceMethodBuilder in this.methods)
			{
				sourceMethodBuilder.DefineMethod(this.file);
			}
			try
			{
				File.Delete(this.filename);
			}
			catch
			{
			}
			using (FileStream fileStream = new FileStream(this.filename, 2, 2))
			{
				this.file.CreateSymbolFile(guid, fileStream);
			}
		}

		// Token: 0x0400005B RID: 91
		private List<SourceMethodBuilder> methods;

		// Token: 0x0400005C RID: 92
		private List<SourceFileEntry> sources;

		// Token: 0x0400005D RID: 93
		private List<CompileUnitEntry> comp_units;

		// Token: 0x0400005E RID: 94
		protected readonly MonoSymbolFile file;

		// Token: 0x0400005F RID: 95
		private string filename;

		// Token: 0x04000060 RID: 96
		private SourceMethodBuilder current_method;

		// Token: 0x04000061 RID: 97
		private Stack<SourceMethodBuilder> current_method_stack = new Stack<SourceMethodBuilder>();
	}
}
