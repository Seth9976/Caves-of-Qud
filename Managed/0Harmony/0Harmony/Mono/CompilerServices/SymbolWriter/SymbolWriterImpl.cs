using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Text;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000316 RID: 790
	internal class SymbolWriterImpl : ISymbolWriter
	{
		// Token: 0x060013DD RID: 5085 RVA: 0x00040D34 File Offset: 0x0003EF34
		public SymbolWriterImpl(Guid guid)
		{
			this.guid = guid;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00040D59 File Offset: 0x0003EF59
		public void Close()
		{
			this.msw.WriteSymbolFile(this.guid);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00040D6C File Offset: 0x0003EF6C
		public void CloseMethod()
		{
			if (this.methodOpened)
			{
				this.methodOpened = false;
				this.nextLocalIndex = 0;
				this.msw.CloseMethod();
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00040D8F File Offset: 0x0003EF8F
		public void CloseNamespace()
		{
			this.namespaceStack.Pop();
			this.msw.CloseNamespace();
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00040DA8 File Offset: 0x0003EFA8
		public void CloseScope(int endOffset)
		{
			this.msw.CloseScope(endOffset);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00040DB8 File Offset: 0x0003EFB8
		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			SymbolDocumentWriterImpl symbolDocumentWriterImpl = (SymbolDocumentWriterImpl)this.documents[url];
			if (symbolDocumentWriterImpl == null)
			{
				SourceFileEntry sourceFileEntry = this.msw.DefineDocument(url);
				symbolDocumentWriterImpl = new SymbolDocumentWriterImpl(this.msw.DefineCompilationUnit(sourceFileEntry));
				this.documents[url] = symbolDocumentWriterImpl;
			}
			return symbolDocumentWriterImpl;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00018659 File Offset: 0x00016859
		public void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00018659 File Offset: 0x00016859
		public void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00040E08 File Offset: 0x0003F008
		public void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset)
		{
			MonoSymbolWriter monoSymbolWriter = this.msw;
			int num = this.nextLocalIndex;
			this.nextLocalIndex = num + 1;
			monoSymbolWriter.DefineLocalVariable(num, name);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00018659 File Offset: 0x00016859
		public void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00040E34 File Offset: 0x0003F034
		public void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
		{
			SymbolDocumentWriterImpl symbolDocumentWriterImpl = (SymbolDocumentWriterImpl)document;
			SourceFileEntry sourceFileEntry = ((symbolDocumentWriterImpl != null) ? symbolDocumentWriterImpl.Entry.SourceFile : null);
			for (int i = 0; i < offsets.Length; i++)
			{
				if (i <= 0 || offsets[i] != offsets[i - 1] || lines[i] != lines[i - 1] || columns[i] != columns[i - 1])
				{
					this.msw.MarkSequencePoint(offsets[i], sourceFileEntry, lines[i], columns[i], false);
				}
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00040EA3 File Offset: 0x0003F0A3
		public void Initialize(IntPtr emitter, string filename, bool fFullBuild)
		{
			this.msw = new MonoSymbolWriter(filename);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00040EB1 File Offset: 0x0003F0B1
		public void OpenMethod(SymbolToken method)
		{
			this.currentToken = method.GetToken();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00040EC0 File Offset: 0x0003F0C0
		public void OpenNamespace(string name)
		{
			NamespaceInfo namespaceInfo = new NamespaceInfo();
			namespaceInfo.NamespaceID = -1;
			namespaceInfo.Name = name;
			this.namespaceStack.Push(namespaceInfo);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00040EED File Offset: 0x0003F0ED
		public int OpenScope(int startOffset)
		{
			return this.msw.OpenScope(startOffset);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00040EFC File Offset: 0x0003F0FC
		public void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn)
		{
			int currentNamespace = this.GetCurrentNamespace(startDoc);
			SourceMethodImpl sourceMethodImpl = new SourceMethodImpl(this.methodName, this.currentToken, currentNamespace);
			this.msw.OpenMethod(((ICompileUnit)startDoc).Entry, currentNamespace, sourceMethodImpl);
			this.methodOpened = true;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00018659 File Offset: 0x00016859
		public void SetScopeRange(int scopeID, int startOffset, int endOffset)
		{
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00040F44 File Offset: 0x0003F144
		public void SetSymAttribute(SymbolToken parent, string name, byte[] data)
		{
			if (name == "__name")
			{
				this.methodName = Encoding.UTF8.GetString(data);
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00018659 File Offset: 0x00016859
		public void SetUnderlyingWriter(IntPtr underlyingWriter)
		{
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00018659 File Offset: 0x00016859
		public void SetUserEntryPoint(SymbolToken entryMethod)
		{
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00040F64 File Offset: 0x0003F164
		public void UsingNamespace(string fullName)
		{
			if (this.namespaceStack.Count == 0)
			{
				this.OpenNamespace("");
			}
			NamespaceInfo namespaceInfo = (NamespaceInfo)this.namespaceStack.Peek();
			if (namespaceInfo.NamespaceID != -1)
			{
				NamespaceInfo namespaceInfo2 = namespaceInfo;
				this.CloseNamespace();
				this.OpenNamespace(namespaceInfo2.Name);
				namespaceInfo = (NamespaceInfo)this.namespaceStack.Peek();
				namespaceInfo.UsingClauses = namespaceInfo2.UsingClauses;
			}
			namespaceInfo.UsingClauses.Add(fullName);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00040FE4 File Offset: 0x0003F1E4
		private int GetCurrentNamespace(ISymbolDocumentWriter doc)
		{
			if (this.namespaceStack.Count == 0)
			{
				this.OpenNamespace("");
			}
			NamespaceInfo namespaceInfo = (NamespaceInfo)this.namespaceStack.Peek();
			if (namespaceInfo.NamespaceID == -1)
			{
				string[] array = (string[])namespaceInfo.UsingClauses.ToArray(typeof(string));
				int num = 0;
				if (this.namespaceStack.Count > 1)
				{
					this.namespaceStack.Pop();
					num = ((NamespaceInfo)this.namespaceStack.Peek()).NamespaceID;
					this.namespaceStack.Push(namespaceInfo);
				}
				namespaceInfo.NamespaceID = this.msw.DefineNamespace(namespaceInfo.Name, ((ICompileUnit)doc).Entry, array, num);
			}
			return namespaceInfo.NamespaceID;
		}

		// Token: 0x04000A58 RID: 2648
		private MonoSymbolWriter msw;

		// Token: 0x04000A59 RID: 2649
		private int nextLocalIndex;

		// Token: 0x04000A5A RID: 2650
		private int currentToken;

		// Token: 0x04000A5B RID: 2651
		private string methodName;

		// Token: 0x04000A5C RID: 2652
		private Stack namespaceStack = new Stack();

		// Token: 0x04000A5D RID: 2653
		private bool methodOpened;

		// Token: 0x04000A5E RID: 2654
		private Hashtable documents = new Hashtable();

		// Token: 0x04000A5F RID: 2655
		private Guid guid;
	}
}
