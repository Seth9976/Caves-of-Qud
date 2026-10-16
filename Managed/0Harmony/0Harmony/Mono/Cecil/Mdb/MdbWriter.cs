using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.Cecil.Mdb
{
	// Token: 0x0200031F RID: 799
	internal sealed class MdbWriter : ISymbolWriter, IDisposable
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x00041575 File Offset: 0x0003F775
		public MdbWriter(ModuleDefinition module, string assembly)
		{
			this.module = module;
			this.writer = new MonoSymbolWriter(assembly);
			this.source_files = new Dictionary<string, MdbWriter.SourceFile>();
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0004159B File Offset: 0x0003F79B
		public ISymbolReaderProvider GetReaderProvider()
		{
			return new MdbReaderProvider();
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000415A4 File Offset: 0x0003F7A4
		private MdbWriter.SourceFile GetSourceFile(Document document)
		{
			string url = document.Url;
			MdbWriter.SourceFile sourceFile;
			if (this.source_files.TryGetValue(url, out sourceFile))
			{
				return sourceFile;
			}
			SourceFileEntry sourceFileEntry = this.writer.DefineDocument(url, null, (document.Hash != null && document.Hash.Length == 16) ? document.Hash : null);
			sourceFile = new MdbWriter.SourceFile(this.writer.DefineCompilationUnit(sourceFileEntry), sourceFileEntry);
			this.source_files.Add(url, sourceFile);
			return sourceFile;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00041618 File Offset: 0x0003F818
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

		// Token: 0x06001418 RID: 5144 RVA: 0x00041684 File Offset: 0x0003F884
		public void Write(MethodDebugInformation info)
		{
			MdbWriter.SourceMethod sourceMethod = new MdbWriter.SourceMethod(info.method);
			Collection<SequencePoint> sequencePoints = info.SequencePoints;
			int count = sequencePoints.Count;
			if (count == 0)
			{
				return;
			}
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

		// Token: 0x06001419 RID: 5145 RVA: 0x0004175A File Offset: 0x0003F95A
		private void WriteRootScope(ScopeDebugInformation scope, MethodDebugInformation info)
		{
			this.WriteScopeVariables(scope);
			if (scope.HasScopes)
			{
				this.WriteScopes(scope.Scopes, info);
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00041778 File Offset: 0x0003F978
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

		// Token: 0x0600141B RID: 5147 RVA: 0x000417EC File Offset: 0x0003F9EC
		private void WriteScopes(Collection<ScopeDebugInformation> scopes, MethodDebugInformation info)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				this.WriteScope(scopes[i], info);
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00041818 File Offset: 0x0003FA18
		private void WriteScopeVariables(ScopeDebugInformation scope)
		{
			if (!scope.HasVariables)
			{
				return;
			}
			foreach (VariableDebugInformation variableDebugInformation in scope.variables)
			{
				if (!string.IsNullOrEmpty(variableDebugInformation.Name))
				{
					this.writer.DefineLocalVariable(variableDebugInformation.Index, variableDebugInformation.Name);
				}
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00041894 File Offset: 0x0003FA94
		public ImageDebugHeader GetDebugHeader()
		{
			return new ImageDebugHeader();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004189B File Offset: 0x0003FA9B
		public void Dispose()
		{
			this.writer.WriteSymbolFile(this.module.Mvid);
		}

		// Token: 0x04000A6C RID: 2668
		private readonly ModuleDefinition module;

		// Token: 0x04000A6D RID: 2669
		private readonly MonoSymbolWriter writer;

		// Token: 0x04000A6E RID: 2670
		private readonly Dictionary<string, MdbWriter.SourceFile> source_files;

		// Token: 0x02000320 RID: 800
		private class SourceFile : ISourceFile
		{
			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x0600141F RID: 5151 RVA: 0x000418B3 File Offset: 0x0003FAB3
			public SourceFileEntry Entry
			{
				get
				{
					return this.entry;
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x000418BB File Offset: 0x0003FABB
			public CompileUnitEntry CompilationUnit
			{
				get
				{
					return this.compilation_unit;
				}
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x000418C3 File Offset: 0x0003FAC3
			public SourceFile(CompileUnitEntry comp_unit, SourceFileEntry entry)
			{
				this.compilation_unit = comp_unit;
				this.entry = entry;
			}

			// Token: 0x04000A6F RID: 2671
			private readonly CompileUnitEntry compilation_unit;

			// Token: 0x04000A70 RID: 2672
			private readonly SourceFileEntry entry;
		}

		// Token: 0x02000321 RID: 801
		private class SourceMethod : IMethodDef
		{
			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06001422 RID: 5154 RVA: 0x000418D9 File Offset: 0x0003FAD9
			public string Name
			{
				get
				{
					return this.method.Name;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06001423 RID: 5155 RVA: 0x000418E8 File Offset: 0x0003FAE8
			public int Token
			{
				get
				{
					return this.method.MetadataToken.ToInt32();
				}
			}

			// Token: 0x06001424 RID: 5156 RVA: 0x00041908 File Offset: 0x0003FB08
			public SourceMethod(MethodDefinition method)
			{
				this.method = method;
			}

			// Token: 0x04000A71 RID: 2673
			private readonly MethodDefinition method;
		}
	}
}
