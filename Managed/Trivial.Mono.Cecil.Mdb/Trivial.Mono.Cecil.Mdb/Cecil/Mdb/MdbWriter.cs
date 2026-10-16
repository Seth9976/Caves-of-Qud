using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;
using Trivial.Mono.CompilerServices.SymbolWriter;

namespace Trivial.Mono.Cecil.Mdb
{
	// Token: 0x0200001E RID: 30
	public sealed class MdbWriter : ISymbolWriter, IDisposable
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00005E8B File Offset: 0x0000408B
		public MdbWriter(Guid mvid, string assembly)
		{
			this.mvid = mvid;
			this.writer = new MonoSymbolWriter(assembly);
			this.source_files = new Dictionary<string, MdbWriter.SourceFile>();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005EB1 File Offset: 0x000040B1
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new MdbReaderProvider();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005EB8 File Offset: 0x000040B8
		private MdbWriter.SourceFile GetSourceFile(Document document)
		{
			string url = document.Url;
			MdbWriter.SourceFile sourceFile;
			if (this.source_files.TryGetValue(url, ref sourceFile))
			{
				return sourceFile;
			}
			SourceFileEntry sourceFileEntry = this.writer.DefineDocument(url, null, (document.Hash != null && document.Hash.Length == 16) ? document.Hash : null);
			sourceFile = new MdbWriter.SourceFile(this.writer.DefineCompilationUnit(sourceFileEntry), sourceFileEntry);
			this.source_files.Add(url, sourceFile);
			return sourceFile;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005F2C File Offset: 0x0000412C
		private void Populate(Collection<SequencePoint> sequencePoints, int[] offsets, int[] startRows, int[] endRows, int[] startCols, int[] endCols, out MdbWriter.SourceFile file)
		{
			MdbWriter.SourceFile sourceFile = null;
			for (int i = 0; i < sequencePoints.Count; i++)
			{
				SequencePoint sequencePoint = sequencePoints[i];
				offsets[i] = sequencePoint.Offset;
				if (sourceFile == null)
				{
					sourceFile = this.GetSourceFile(sequencePoint.Document);
				}
				startRows[i] = sequencePoint.StartLine;
				endRows[i] = sequencePoint.EndLine;
				startCols[i] = sequencePoint.StartColumn;
				endCols[i] = sequencePoint.EndColumn;
			}
			file = sourceFile;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005F98 File Offset: 0x00004198
		public void Write(MethodDebugInformation info)
		{
			MdbWriter.SourceMethod sourceMethod = new MdbWriter.SourceMethod(info.method);
			Collection<SequencePoint> sequencePoints = info.SequencePoints;
			int count = sequencePoints.Count;
			if (count != 0)
			{
				int[] array = new int[count];
				int[] array2 = new int[count];
				int[] array3 = new int[count];
				int[] array4 = new int[count];
				int[] array5 = new int[count];
				MdbWriter.SourceFile sourceFile;
				this.Populate(sequencePoints, array, array2, array3, array4, array5, out sourceFile);
				SourceMethodBuilder sourceMethodBuilder = this.writer.OpenMethod(sourceFile.CompilationUnit, 0, sourceMethod);
				for (int i = 0; i < count; i++)
				{
					sourceMethodBuilder.MarkSequencePoint(array[i], sourceFile.CompilationUnit.SourceFile, array2[i], array4[i], array3[i], array5[i], false);
				}
				if (info.scope != null)
				{
					this.WriteRootScope(info.scope, info);
				}
				this.writer.CloseMethod();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006070 File Offset: 0x00004270
		private void WriteRootScope(ScopeDebugInformation scope, MethodDebugInformation info)
		{
			this.WriteScopeVariables(scope);
			if (scope.HasScopes)
			{
				this.WriteScopes(scope.Scopes, info);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006090 File Offset: 0x00004290
		private void WriteScope(ScopeDebugInformation scope, MethodDebugInformation info)
		{
			this.writer.OpenScope(scope.Start.Offset);
			this.WriteScopeVariables(scope);
			if (scope.HasScopes)
			{
				this.WriteScopes(scope.Scopes, info);
			}
			this.writer.CloseScope(scope.End.IsEndOfMethod ? info.code_size : scope.End.Offset);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006104 File Offset: 0x00004304
		private void WriteScopes(Collection<ScopeDebugInformation> scopes, MethodDebugInformation info)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				this.WriteScope(scopes[i], info);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006130 File Offset: 0x00004330
		private void WriteScopeVariables(ScopeDebugInformation scope)
		{
			if (scope.HasVariables)
			{
				foreach (VariableDebugInformation variableDebugInformation in scope.variables)
				{
					if (!string.IsNullOrEmpty(variableDebugInformation.Name))
					{
						this.writer.DefineLocalVariable(variableDebugInformation.Index, variableDebugInformation.Name);
					}
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000061A8 File Offset: 0x000043A8
		public ImageDebugHeader GetDebugHeader()
		{
			return new ImageDebugHeader();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000061AF File Offset: 0x000043AF
		public void Dispose()
		{
			this.writer.WriteSymbolFile(this.mvid);
		}

		// Token: 0x040000A1 RID: 161
		private readonly Guid mvid;

		// Token: 0x040000A2 RID: 162
		private readonly MonoSymbolWriter writer;

		// Token: 0x040000A3 RID: 163
		private readonly Dictionary<string, MdbWriter.SourceFile> source_files;

		// Token: 0x02000027 RID: 39
		private class SourceFile : ISourceFile
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00006262 File Offset: 0x00004462
			public SourceFileEntry Entry
			{
				get
				{
					return this.entry;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000100 RID: 256 RVA: 0x0000626A File Offset: 0x0000446A
			public CompileUnitEntry CompilationUnit
			{
				get
				{
					return this.compilation_unit;
				}
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00006272 File Offset: 0x00004472
			public SourceFile(CompileUnitEntry comp_unit, SourceFileEntry entry)
			{
				this.compilation_unit = comp_unit;
				this.entry = entry;
			}

			// Token: 0x040000B7 RID: 183
			private readonly CompileUnitEntry compilation_unit;

			// Token: 0x040000B8 RID: 184
			private readonly SourceFileEntry entry;
		}

		// Token: 0x02000028 RID: 40
		private class SourceMethod : IMethodDef
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00006288 File Offset: 0x00004488
			public string Name
			{
				get
				{
					return this.method.Name;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00006298 File Offset: 0x00004498
			public int Token
			{
				get
				{
					return this.method.MetadataToken.ToInt32();
				}
			}

			// Token: 0x06000104 RID: 260 RVA: 0x000062B8 File Offset: 0x000044B8
			public SourceMethod(MethodDefinition method)
			{
				this.method = method;
			}

			// Token: 0x040000B9 RID: 185
			private readonly MethodDefinition method;
		}
	}
}
