using System;
using System.Collections.Generic;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000314 RID: 788
	internal class MonoSymbolWriter
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x00040688 File Offset: 0x0003E888
		public MonoSymbolWriter(string filename)
		{
			this.methods = new List<SourceMethodBuilder>();
			this.sources = new List<SourceFileEntry>();
			this.comp_units = new List<CompileUnitEntry>();
			this.file = new MonoSymbolFile();
			this.filename = filename + ".mdb";
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000406E3 File Offset: 0x0003E8E3
		public MonoSymbolFile SymbolFile
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00018659 File Offset: 0x00016859
		public void CloseNamespace()
		{
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000406EB File Offset: 0x0003E8EB
		public void DefineLocalVariable(int index, string name)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.AddLocal(index, name);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00040703 File Offset: 0x0003E903
		public void DefineCapturedLocal(int scope_id, string name, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Local);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00040714 File Offset: 0x0003E914
		public void DefineCapturedParameter(int scope_id, string name, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, name, captured_name, CapturedVariable.CapturedKind.Parameter);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00040725 File Offset: 0x0003E925
		public void DefineCapturedThis(int scope_id, string captured_name)
		{
			this.file.DefineCapturedVariable(scope_id, "this", captured_name, CapturedVariable.CapturedKind.This);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004073A File Offset: 0x0003E93A
		public void DefineCapturedScope(int scope_id, int id, string captured_name)
		{
			this.file.DefineCapturedScope(scope_id, id, captured_name);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004074A File Offset: 0x0003E94A
		public void DefineScopeVariable(int scope, int index)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.AddScopeVariable(scope, index);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00040762 File Offset: 0x0003E962
		public void MarkSequencePoint(int offset, SourceFileEntry file, int line, int column, bool is_hidden)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.MarkSequencePoint(offset, file, line, column, is_hidden);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00040780 File Offset: 0x0003E980
		public SourceMethodBuilder OpenMethod(ICompileUnit file, int ns_id, IMethodDef method)
		{
			SourceMethodBuilder sourceMethodBuilder = new SourceMethodBuilder(file, ns_id, method);
			this.current_method_stack.Push(this.current_method);
			this.current_method = sourceMethodBuilder;
			this.methods.Add(this.current_method);
			return sourceMethodBuilder;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000407C0 File Offset: 0x0003E9C0
		public void CloseMethod()
		{
			this.current_method = this.current_method_stack.Pop();
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public SourceFileEntry DefineDocument(string url)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(this.file, url);
			this.sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000407FC File Offset: 0x0003E9FC
		public SourceFileEntry DefineDocument(string url, byte[] guid, byte[] checksum)
		{
			SourceFileEntry sourceFileEntry = new SourceFileEntry(this.file, url, guid, checksum);
			this.sources.Add(sourceFileEntry);
			return sourceFileEntry;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00040828 File Offset: 0x0003EA28
		public CompileUnitEntry DefineCompilationUnit(SourceFileEntry source)
		{
			CompileUnitEntry compileUnitEntry = new CompileUnitEntry(this.file, source);
			this.comp_units.Add(compileUnitEntry);
			return compileUnitEntry;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004084F File Offset: 0x0003EA4F
		public int DefineNamespace(string name, CompileUnitEntry unit, string[] using_clauses, int parent)
		{
			if (unit == null || using_clauses == null)
			{
				throw new NullReferenceException();
			}
			return unit.DefineNamespace(name, using_clauses, parent);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00040867 File Offset: 0x0003EA67
		public int OpenScope(int start_offset)
		{
			if (this.current_method == null)
			{
				return 0;
			}
			this.current_method.StartBlock(CodeBlockEntry.Type.Lexical, start_offset);
			return 0;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00040881 File Offset: 0x0003EA81
		public void CloseScope(int end_offset)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00040898 File Offset: 0x0003EA98
		public void OpenCompilerGeneratedBlock(int start_offset)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.StartBlock(CodeBlockEntry.Type.CompilerGenerated, start_offset);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00040881 File Offset: 0x0003EA81
		public void CloseCompilerGeneratedBlock(int end_offset)
		{
			if (this.current_method == null)
			{
				return;
			}
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000408B0 File Offset: 0x0003EAB0
		public void StartIteratorBody(int start_offset)
		{
			this.current_method.StartBlock(CodeBlockEntry.Type.IteratorBody, start_offset);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000408BF File Offset: 0x0003EABF
		public void EndIteratorBody(int end_offset)
		{
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000408CD File Offset: 0x0003EACD
		public void StartIteratorDispatcher(int start_offset)
		{
			this.current_method.StartBlock(CodeBlockEntry.Type.IteratorDispatcher, start_offset);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000408BF File Offset: 0x0003EABF
		public void EndIteratorDispatcher(int end_offset)
		{
			this.current_method.EndBlock(end_offset);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000408DC File Offset: 0x0003EADC
		public void DefineAnonymousScope(int id)
		{
			this.file.DefineAnonymousScope(id);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000408EC File Offset: 0x0003EAEC
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
			using (FileStream fileStream = new FileStream(this.filename, FileMode.Create, FileAccess.Write))
			{
				this.file.CreateSymbolFile(guid, fileStream);
			}
		}

		// Token: 0x04000A49 RID: 2633
		private List<SourceMethodBuilder> methods;

		// Token: 0x04000A4A RID: 2634
		private List<SourceFileEntry> sources;

		// Token: 0x04000A4B RID: 2635
		private List<CompileUnitEntry> comp_units;

		// Token: 0x04000A4C RID: 2636
		protected readonly MonoSymbolFile file;

		// Token: 0x04000A4D RID: 2637
		private string filename;

		// Token: 0x04000A4E RID: 2638
		private SourceMethodBuilder current_method;

		// Token: 0x04000A4F RID: 2639
		private Stack<SourceMethodBuilder> current_method_stack = new Stack<SourceMethodBuilder>();
	}
}
