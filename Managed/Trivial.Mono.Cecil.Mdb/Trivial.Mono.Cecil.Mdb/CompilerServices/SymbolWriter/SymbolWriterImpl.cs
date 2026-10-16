using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Reflection;
using System.Text;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200001B RID: 27
	public class SymbolWriterImpl : ISymbolWriter
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00005671 File Offset: 0x00003871
		public SymbolWriterImpl(Guid guid)
		{
			this.guid = guid;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005696 File Offset: 0x00003896
		public void Close()
		{
			this.msw.WriteSymbolFile(this.guid);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000056A9 File Offset: 0x000038A9
		public void CloseMethod()
		{
			if (this.methodOpened)
			{
				this.methodOpened = false;
				this.nextLocalIndex = 0;
				this.msw.CloseMethod();
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000056CC File Offset: 0x000038CC
		public void CloseNamespace()
		{
			this.namespaceStack.Pop();
			this.msw.CloseNamespace();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000056E5 File Offset: 0x000038E5
		public void CloseScope(int endOffset)
		{
			this.msw.CloseScope(endOffset);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000056F4 File Offset: 0x000038F4
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

		// Token: 0x060000CA RID: 202 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005744 File Offset: 0x00003944
		public void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset)
		{
			MonoSymbolWriter monoSymbolWriter = this.msw;
			int num = this.nextLocalIndex;
			this.nextLocalIndex = num + 1;
			monoSymbolWriter.DefineLocalVariable(num, name);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005770 File Offset: 0x00003970
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

		// Token: 0x060000CF RID: 207 RVA: 0x000057DE File Offset: 0x000039DE
		public void Initialize(IntPtr emitter, string filename, bool fFullBuild)
		{
			this.msw = new MonoSymbolWriter(filename);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000057EC File Offset: 0x000039EC
		public void OpenMethod(SymbolToken method)
		{
			this.currentToken = method.GetToken();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000057FC File Offset: 0x000039FC
		public void OpenNamespace(string name)
		{
			NamespaceInfo namespaceInfo = new NamespaceInfo();
			namespaceInfo.NamespaceID = -1;
			namespaceInfo.Name = name;
			this.namespaceStack.Push(namespaceInfo);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005829 File Offset: 0x00003A29
		public int OpenScope(int startOffset)
		{
			return this.msw.OpenScope(startOffset);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005838 File Offset: 0x00003A38
		public void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn)
		{
			int currentNamespace = this.GetCurrentNamespace(startDoc);
			SourceMethodImpl sourceMethodImpl = new SourceMethodImpl(this.methodName, this.currentToken, currentNamespace);
			this.msw.OpenMethod(((ICompileUnit)startDoc).Entry, currentNamespace, sourceMethodImpl);
			this.methodOpened = true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void SetScopeRange(int scopeID, int startOffset, int endOffset)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005880 File Offset: 0x00003A80
		public void SetSymAttribute(SymbolToken parent, string name, byte[] data)
		{
			if (name == "__name")
			{
				this.methodName = Encoding.UTF8.GetString(data);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void SetUnderlyingWriter(IntPtr underlyingWriter)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void SetUserEntryPoint(SymbolToken entryMethod)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000058A0 File Offset: 0x00003AA0
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

		// Token: 0x060000D9 RID: 217 RVA: 0x00005920 File Offset: 0x00003B20
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

		// Token: 0x04000096 RID: 150
		private MonoSymbolWriter msw;

		// Token: 0x04000097 RID: 151
		private int nextLocalIndex;

		// Token: 0x04000098 RID: 152
		private int currentToken;

		// Token: 0x04000099 RID: 153
		private string methodName;

		// Token: 0x0400009A RID: 154
		private Stack namespaceStack = new Stack();

		// Token: 0x0400009B RID: 155
		private bool methodOpened;

		// Token: 0x0400009C RID: 156
		private Hashtable documents = new Hashtable();

		// Token: 0x0400009D RID: 157
		private Guid guid;
	}
}
