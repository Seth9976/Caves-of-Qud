using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;
using Trivial.Mono.CompilerServices.SymbolWriter;

namespace Trivial.Mono.Cecil.Mdb
{
	// Token: 0x0200001C RID: 28
	public sealed class MdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000059E2 File Offset: 0x00003BE2
		public MdbReader(ModuleDefinition module, MonoSymbolFile symFile)
		{
			this.module = module;
			this.symbol_file = symFile;
			this.documents = new Dictionary<string, Document>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005A03 File Offset: 0x00003C03
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new MdbWriterProvider();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005A0A File Offset: 0x00003C0A
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			return this.symbol_file.Guid == this.module.Mvid;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005A28 File Offset: 0x00003C28
		public MethodDebugInformation Read(MethodDefinition method)
		{
			MetadataToken metadataToken = method.MetadataToken;
			MethodEntry methodByToken = this.symbol_file.GetMethodByToken(metadataToken.ToInt32());
			if (methodByToken == null)
			{
				return null;
			}
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			methodDebugInformation.code_size = MdbReader.ReadCodeSize(method);
			ScopeDebugInformation[] array = MdbReader.ReadScopes(methodByToken, methodDebugInformation);
			this.ReadLineNumbers(methodByToken, methodDebugInformation);
			MdbReader.ReadLocalVariables(methodByToken, array);
			return methodDebugInformation;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005A7F File Offset: 0x00003C7F
		private static int ReadCodeSize(MethodDefinition method)
		{
			return method.Module.Read<MethodDefinition, int>(method, (MethodDefinition m, MetadataReader reader) => reader.ReadCodeSize(m));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005AAC File Offset: 0x00003CAC
		private static void ReadLocalVariables(MethodEntry entry, ScopeDebugInformation[] scopes)
		{
			LocalVariableEntry[] locals = entry.GetLocals();
			foreach (LocalVariableEntry localVariableEntry in locals)
			{
				VariableDebugInformation variableDebugInformation = new VariableDebugInformation(localVariableEntry.Index, localVariableEntry.Name);
				int blockIndex = localVariableEntry.BlockIndex;
				if (blockIndex >= 0 && blockIndex < scopes.Length)
				{
					ScopeDebugInformation scopeDebugInformation = scopes[blockIndex];
					if (scopeDebugInformation != null)
					{
						scopeDebugInformation.Variables.Add(variableDebugInformation);
					}
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005B18 File Offset: 0x00003D18
		private void ReadLineNumbers(MethodEntry entry, MethodDebugInformation info)
		{
			LineNumberTable lineNumberTable = entry.GetLineNumberTable();
			info.sequence_points = new Collection<SequencePoint>(lineNumberTable.LineNumbers.Length);
			for (int i = 0; i < lineNumberTable.LineNumbers.Length; i++)
			{
				LineNumberEntry lineNumberEntry = lineNumberTable.LineNumbers[i];
				if (i <= 0 || lineNumberTable.LineNumbers[i - 1].Offset != lineNumberEntry.Offset)
				{
					info.sequence_points.Add(this.LineToSequencePoint(lineNumberEntry));
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005B88 File Offset: 0x00003D88
		private Document GetDocument(SourceFileEntry file)
		{
			string fileName = file.FileName;
			Document document;
			if (this.documents.TryGetValue(fileName, ref document))
			{
				return document;
			}
			document = new Document(fileName)
			{
				Hash = file.Checksum
			};
			this.documents.Add(fileName, document);
			return document;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005BD0 File Offset: 0x00003DD0
		private static ScopeDebugInformation[] ReadScopes(MethodEntry entry, MethodDebugInformation info)
		{
			CodeBlockEntry[] codeBlocks = entry.GetCodeBlocks();
			ScopeDebugInformation[] array = new ScopeDebugInformation[codeBlocks.Length + 1];
			ScopeDebugInformation[] array2 = array;
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation
			{
				Start = new InstructionOffset(0),
				End = new InstructionOffset(info.code_size)
			};
			ScopeDebugInformation scopeDebugInformation2 = scopeDebugInformation;
			array2[0] = scopeDebugInformation;
			info.scope = scopeDebugInformation2;
			CodeBlockEntry[] array3 = codeBlocks;
			foreach (CodeBlockEntry codeBlockEntry in array3)
			{
				if (codeBlockEntry.BlockType == CodeBlockEntry.Type.Lexical || codeBlockEntry.BlockType == CodeBlockEntry.Type.CompilerGenerated)
				{
					ScopeDebugInformation scopeDebugInformation3 = new ScopeDebugInformation();
					scopeDebugInformation3.Start = new InstructionOffset(codeBlockEntry.StartOffset);
					scopeDebugInformation3.End = new InstructionOffset(codeBlockEntry.EndOffset);
					array[codeBlockEntry.Index + 1] = scopeDebugInformation3;
					if (!MdbReader.AddScope(info.scope.Scopes, scopeDebugInformation3))
					{
						info.scope.Scopes.Add(scopeDebugInformation3);
					}
				}
			}
			return array;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005CC0 File Offset: 0x00003EC0
		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			foreach (ScopeDebugInformation scopeDebugInformation in scopes)
			{
				if (scopeDebugInformation.HasScopes && MdbReader.AddScope(scopeDebugInformation.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scopeDebugInformation.Start.Offset && scope.End.Offset <= scopeDebugInformation.End.Offset)
				{
					scopeDebugInformation.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005D70 File Offset: 0x00003F70
		private SequencePoint LineToSequencePoint(LineNumberEntry line)
		{
			SourceFileEntry sourceFile = this.symbol_file.GetSourceFile(line.File);
			return new SequencePoint(line.Offset, this.GetDocument(sourceFile))
			{
				StartLine = line.Row,
				EndLine = ((line.EndRow != -1) ? line.EndRow : line.Row),
				StartColumn = line.Column,
				EndColumn = line.EndColumn
			};
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005DE2 File Offset: 0x00003FE2
		public void Dispose()
		{
			this.symbol_file.Dispose();
		}

		// Token: 0x0400009E RID: 158
		private readonly ModuleDefinition module;

		// Token: 0x0400009F RID: 159
		private readonly MonoSymbolFile symbol_file;

		// Token: 0x040000A0 RID: 160
		private readonly Dictionary<string, Document> documents;
	}
}
