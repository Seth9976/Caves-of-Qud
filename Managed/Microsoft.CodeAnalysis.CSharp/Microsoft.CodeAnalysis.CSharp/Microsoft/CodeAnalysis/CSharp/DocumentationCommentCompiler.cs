using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000116 RID: 278
	internal class DocumentationCommentCompiler : CSharpSymbolVisitor
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x0007CC14 File Offset: 0x0007AE14
		private DocumentationCommentCompiler(string assemblyName, CSharpCompilation compilation, TextWriter writer, SyntaxTree filterTree, TextSpan? filterSpanWithinTree, bool processIncludes, bool isForSingleSymbol, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			this._assemblyName = assemblyName;
			this._compilation = compilation;
			this._writer = writer;
			this._filterTree = filterTree;
			this._filterSpanWithinTree = filterSpanWithinTree;
			this._processIncludes = processIncludes;
			this._isForSingleSymbol = isForSingleSymbol;
			this._diagnostics = diagnostics;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0007CC6C File Offset: 0x0007AE6C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static void WriteDocumentationCommentXml([global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, string assemblyName, Stream xmlDocStream, [global::System.Runtime.CompilerServices.Nullable(1)] BindingDiagnosticBag diagnostics, CancellationToken cancellationToken, SyntaxTree filterTree = null, TextSpan? filterSpanWithinTree = null)
		{
			StreamWriter streamWriter = null;
			if (xmlDocStream != null && xmlDocStream.CanWrite)
			{
				streamWriter = new StreamWriter(xmlDocStream, new UTF8Encoding(false, false), 1024, true);
			}
			try
			{
				using (streamWriter)
				{
					new DocumentationCommentCompiler(assemblyName ?? compilation.SourceAssembly.Name, compilation, streamWriter, filterTree, filterSpanWithinTree, true, false, diagnostics, cancellationToken).Visit(compilation.SourceAssembly.GlobalNamespace);
					if (streamWriter != null)
					{
						streamWriter.Flush();
					}
				}
			}
			catch (Exception ex)
			{
				diagnostics.Add(ErrorCode.ERR_DocFileGen, Location.None, new object[] { ex.Message });
			}
			DiagnosticBag diagnosticBag = diagnostics.DiagnosticBag;
			if (diagnosticBag != null)
			{
				if (filterTree != null)
				{
					UnprocessedDocumentationCommentFinder.ReportUnprocessed(filterTree, filterSpanWithinTree, diagnosticBag, cancellationToken);
					return;
				}
				foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
				{
					UnprocessedDocumentationCommentFinder.ReportUnprocessed(syntaxTree, default(TextSpan?), diagnosticBag, cancellationToken);
				}
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0007CD74 File Offset: 0x0007AF74
		internal static string GetDocumentationCommentXml(Symbol symbol, bool processIncludes, CancellationToken cancellationToken)
		{
			CSharpCompilation declaringCompilation = symbol.DeclaringCompilation;
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringWriter stringWriter = new StringWriter(instance.Builder);
			new DocumentationCommentCompiler(null, declaringCompilation, stringWriter, null, default(TextSpan?), processIncludes, true, BindingDiagnosticBag.Discarded, cancellationToken).Visit(symbol);
			stringWriter.Dispose();
			return instance.ToStringAndFree();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0007CDC4 File Offset: 0x0007AFC4
		public override void VisitNamespace(NamespaceSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (symbol.IsGlobalNamespace)
			{
				this.WriteLine("<?xml version=\"1.0\"?>");
				this.WriteLine("<doc>");
				this.Indent();
				if (!this._compilation.Options.OutputKind.IsNetModule())
				{
					this.WriteLine("<assembly>");
					this.Indent();
					this.WriteLine("<name>{0}</name>", new object[] { this._assemblyName });
					this.Unindent();
					this.WriteLine("</assembly>");
				}
				this.WriteLine("<members>");
				this.Indent();
			}
			foreach (Symbol symbol2 in symbol.GetMembers())
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				symbol2.Accept(this);
			}
			if (symbol.IsGlobalNamespace)
			{
				this.Unindent();
				this.WriteLine("</members>");
				this.Unindent();
				this.WriteLine("</doc>");
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0007CECC File Offset: 0x0007B0CC
		public override void VisitNamedType(NamedTypeSymbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this._filterTree != null && !symbol.IsDefinedInSourceTree(this._filterTree, this._filterSpanWithinTree, default(CancellationToken)))
			{
				return;
			}
			this.DefaultVisit(symbol);
			if (!this._isForSingleSymbol)
			{
				foreach (Symbol symbol2 in symbol.GetMembers())
				{
					this._cancellationToken.ThrowIfCancellationRequested();
					symbol2.Accept(this);
				}
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0007CF54 File Offset: 0x0007B154
		public override void DefaultVisit(Symbol symbol)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (DocumentationCommentCompiler.ShouldSkip(symbol))
			{
				return;
			}
			if (this._filterTree != null && !symbol.IsDefinedInSourceTree(this._filterTree, this._filterSpanWithinTree, default(CancellationToken)))
			{
				return;
			}
			bool flag = symbol.IsPartialDefinition();
			if (flag)
			{
				MethodSymbol partialImplementationPart = ((MethodSymbol)symbol).PartialImplementationPart;
				if (partialImplementationPart != null)
				{
					this.Visit(partialImplementationPart);
				}
			}
			DocumentationMode documentationMode;
			ImmutableArray<DocumentationCommentTriviaSyntax> immutableArray;
			if (!this.TryGetDocumentationCommentNodes(symbol, out documentationMode, out immutableArray))
			{
				string message = ErrorFacts.GetMessage(MessageID.IDS_XMLIGNORED, CultureInfo.CurrentUICulture);
				this.WriteLine(string.Format(CultureInfo.CurrentUICulture, message, symbol.GetDocumentationCommentId()));
				return;
			}
			if (immutableArray.IsEmpty)
			{
				if (documentationMode >= DocumentationMode.Diagnose && DocumentationCommentCompiler.RequiresDocumentationComment(symbol))
				{
					Location locationInTreeReportingDocumentationCommentDiagnostics = DocumentationCommentCompiler.GetLocationInTreeReportingDocumentationCommentDiagnostics(symbol);
					if (locationInTreeReportingDocumentationCommentDiagnostics != null)
					{
						this._diagnostics.Add(ErrorCode.WRN_MissingXMLComment, locationInTreeReportingDocumentationCommentDiagnostics, new object[] { symbol });
					}
				}
				return;
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			bool flag2 = DocumentationCommentCompiler.GetLocationInTreeReportingDocumentationCommentDiagnostics(symbol) != null;
			string text;
			bool flag3;
			HashSet<TypeParameterSymbol> hashSet;
			HashSet<ParameterSymbol> hashSet2;
			ImmutableArray<CSharpSyntaxNode> immutableArray2;
			if (!this.TryProcessDocumentationCommentTriviaNodes(symbol, flag, immutableArray, flag2, out text, out flag3, out hashSet, out hashSet2, out immutableArray2))
			{
				return;
			}
			if (flag3)
			{
				string message2 = ErrorFacts.GetMessage(MessageID.IDS_XMLIGNORED, CultureInfo.CurrentUICulture);
				this.WriteLine(string.Format(CultureInfo.CurrentUICulture, message2, symbol.GetDocumentationCommentId()));
				return;
			}
			if (!immutableArray2.IsDefaultOrEmpty)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				TextWriter textWriter = (flag ? null : this._writer);
				DocumentationCommentCompiler.IncludeElementExpander.ProcessIncludes(text, symbol, immutableArray2, this._compilation, ref hashSet2, ref hashSet, ref this._includedFileCache, textWriter, this._diagnostics, this._cancellationToken);
			}
			else if (this._writer != null && !flag)
			{
				this.Write(text);
			}
			if (flag2)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				if (hashSet2 != null)
				{
					foreach (ParameterSymbol parameterSymbol in DocumentationCommentCompiler.GetParameters(symbol))
					{
						if (!hashSet2.Contains(parameterSymbol))
						{
							Location location = parameterSymbol.Locations[0];
							this._diagnostics.Add(ErrorCode.WRN_MissingParamTag, location, new object[] { parameterSymbol.Name, symbol });
						}
					}
				}
				if (hashSet != null)
				{
					foreach (TypeParameterSymbol typeParameterSymbol in DocumentationCommentCompiler.GetTypeParameters(symbol))
					{
						if (!hashSet.Contains(typeParameterSymbol))
						{
							Location location2 = typeParameterSymbol.Locations[0];
							this._diagnostics.Add(ErrorCode.WRN_MissingTypeParamTag, location2, new object[] { typeParameterSymbol, symbol });
						}
					}
				}
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0007D1EF File Offset: 0x0007B3EF
		private static bool ShouldSkip(Symbol symbol)
		{
			return symbol.IsImplicitlyDeclared || symbol.IsAccessor() || symbol is SynthesizedSimpleProgramEntryPointSymbol || symbol is SimpleProgramNamedTypeSymbol || symbol is SynthesizedRecordPropertySymbol;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0007D21C File Offset: 0x0007B41C
		private bool TryProcessDocumentationCommentTriviaNodes(Symbol symbol, bool isPartialMethodDefinitionPart, ImmutableArray<DocumentationCommentTriviaSyntax> docCommentNodes, bool reportParameterOrTypeParameterDiagnostics, out string withUnprocessedIncludes, out bool haveParseError, out HashSet<TypeParameterSymbol> documentedTypeParameters, out HashSet<ParameterSymbol> documentedParameters, out ImmutableArray<CSharpSyntaxNode> includeElementNodes)
		{
			bool flag = false;
			ArrayBuilder<CSharpSyntaxNode> arrayBuilder = null;
			documentedParameters = null;
			documentedTypeParameters = null;
			haveParseError = false;
			foreach (DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax in docCommentNodes)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				bool flag2 = documentationCommentTriviaSyntax.SyntaxTree.ReportDocumentationCommentDiagnostics();
				if (!flag)
				{
					this.BeginTemporaryString();
					if (this._processIncludes)
					{
						arrayBuilder = ArrayBuilder<CSharpSyntaxNode>.GetInstance();
					}
					if (!isPartialMethodDefinitionPart || this._processIncludes)
					{
						this.WriteLine("<member name=\"{0}\">", new object[] { symbol.GetDocumentationCommentId() });
						this.Indent();
					}
					flag = true;
				}
				string substitutedText = DocumentationCommentCompiler.DocumentationCommentWalker.GetSubstitutedText(this._compilation, this._diagnostics, symbol, documentationCommentTriviaSyntax, arrayBuilder, ref documentedParameters, ref documentedTypeParameters);
				string text = this.FormatComment(substitutedText);
				XmlException ex = XmlDocumentationCommentTextReader.ParseAndGetException(text);
				if (ex != null)
				{
					haveParseError = true;
					if (flag2)
					{
						Location location = new SourceLocation(documentationCommentTriviaSyntax.SyntaxTree, new TextSpan(documentationCommentTriviaSyntax.SpanStart, 0));
						this._diagnostics.Add(ErrorCode.WRN_XMLParseError, location, new object[] { DocumentationCommentCompiler.GetDescription(ex) });
					}
				}
				if (!isPartialMethodDefinitionPart || this._processIncludes)
				{
					this.Write(text);
				}
			}
			if (!flag)
			{
				withUnprocessedIncludes = null;
				includeElementNodes = default(ImmutableArray<CSharpSyntaxNode>);
				return false;
			}
			if (!isPartialMethodDefinitionPart || this._processIncludes)
			{
				this.Unindent();
				this.WriteLine("</member>");
			}
			withUnprocessedIncludes = this.GetAndEndTemporaryString();
			includeElementNodes = (this._processIncludes ? arrayBuilder.ToImmutableAndFree() : default(ImmutableArray<CSharpSyntaxNode>));
			return true;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0007D39C File Offset: 0x0007B59C
		private static Location GetLocationInTreeReportingDocumentationCommentDiagnostics(Symbol symbol)
		{
			foreach (Location location in symbol.Locations)
			{
				if (location.SourceTree.ReportDocumentationCommentDiagnostics())
				{
					return location;
				}
			}
			return null;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0007D3DC File Offset: 0x0007B5DC
		private static ImmutableArray<ParameterSymbol> GetParameters(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind <= SymbolKind.Method)
			{
				if (kind != SymbolKind.Event && kind != SymbolKind.Method)
				{
					goto IL_0040;
				}
			}
			else if (kind != SymbolKind.NamedType)
			{
				if (kind != SymbolKind.Property)
				{
					goto IL_0040;
				}
			}
			else
			{
				MethodSymbol delegateInvokeMethod = ((NamedTypeSymbol)symbol).DelegateInvokeMethod;
				if (delegateInvokeMethod != null)
				{
					return delegateInvokeMethod.Parameters;
				}
				goto IL_0040;
			}
			return symbol.GetParameters();
			IL_0040:
			return ImmutableArray<ParameterSymbol>.Empty;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0007D430 File Offset: 0x0007B630
		private static ImmutableArray<TypeParameterSymbol> GetTypeParameters(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind == SymbolKind.ErrorType || kind == SymbolKind.Method || kind == SymbolKind.NamedType)
			{
				return symbol.GetMemberTypeParameters();
			}
			return ImmutableArray<TypeParameterSymbol>.Empty;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0007D460 File Offset: 0x0007B660
		private static bool RequiresDocumentationComment(Symbol symbol)
		{
			if (DocumentationCommentCompiler.ShouldSkip(symbol))
			{
				return false;
			}
			while (symbol != null)
			{
				Accessibility declaredAccessibility = symbol.DeclaredAccessibility;
				if (declaredAccessibility != Accessibility.Protected && declaredAccessibility - Accessibility.ProtectedOrInternal > 1)
				{
					return false;
				}
				symbol = symbol.ContainingType;
			}
			return true;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0007D498 File Offset: 0x0007B698
		private bool TryGetDocumentationCommentNodes(Symbol symbol, out DocumentationMode maxDocumentationMode, out ImmutableArray<DocumentationCommentTriviaSyntax> nodes)
		{
			maxDocumentationMode = DocumentationMode.None;
			nodes = default(ImmutableArray<DocumentationCommentTriviaSyntax>);
			ArrayBuilder<DocumentationCommentTriviaSyntax> arrayBuilder = null;
			DiagnosticBag diagnosticBag = this._diagnostics.DiagnosticBag ?? DiagnosticBag.GetInstance();
			foreach (SyntaxReference syntaxReference in symbol.DeclaringSyntaxReferences)
			{
				DocumentationMode documentationMode = syntaxReference.SyntaxTree.Options.DocumentationMode;
				maxDocumentationMode = ((documentationMode > maxDocumentationMode) ? documentationMode : maxDocumentationMode);
				foreach (DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax in SourceDocumentationCommentUtils.GetDocumentationCommentTriviaFromSyntaxNode((CSharpSyntaxNode)syntaxReference.GetSyntax(default(CancellationToken)), diagnosticBag))
				{
					if (DocumentationCommentCompiler.ContainsXmlParseDiagnostic(documentationCommentTriviaSyntax))
					{
						if (arrayBuilder != null)
						{
							arrayBuilder.Free();
						}
						return false;
					}
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<DocumentationCommentTriviaSyntax>.GetInstance();
					}
					arrayBuilder.Add(documentationCommentTriviaSyntax);
				}
			}
			if (diagnosticBag != this._diagnostics.DiagnosticBag)
			{
				diagnosticBag.Free();
			}
			if (arrayBuilder == null)
			{
				nodes = ImmutableArray<DocumentationCommentTriviaSyntax>.Empty;
			}
			else
			{
				arrayBuilder.Sort(this.Comparer);
				nodes = arrayBuilder.ToImmutableAndFree();
			}
			return true;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		private static bool ContainsXmlParseDiagnostic(DocumentationCommentTriviaSyntax node)
		{
			if (!node.ContainsDiagnostics)
			{
				return false;
			}
			using (IEnumerator<Diagnostic> enumerator = node.GetDiagnostics().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Code == 1570)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0007D60C File Offset: 0x0007B80C
		private string FormatComment(string substitutedText)
		{
			this.BeginTemporaryString();
			if (DocumentationCommentCompiler.TrimmedStringStartsWith(substitutedText, "///"))
			{
				this.WriteFormattedSingleLineComment(substitutedText);
			}
			else
			{
				string[] array = substitutedText.Split(DocumentationCommentCompiler.s_newLineSequences, 0);
				int num = array.Length;
				if (string.IsNullOrEmpty(array[num - 1]))
				{
					num--;
				}
				this.WriteFormattedMultiLineComment(array, num);
			}
			return this.GetAndEndTemporaryString();
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0007D664 File Offset: 0x0007B864
		private static int GetIndexOfFirstNonWhitespaceChar(string str)
		{
			return DocumentationCommentCompiler.GetIndexOfFirstNonWhitespaceChar(str, 0, str.Length);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0007D673 File Offset: 0x0007B873
		private static int GetIndexOfFirstNonWhitespaceChar(string str, int start, int end)
		{
			while (start < end && SyntaxFacts.IsWhitespace(str.get_Chars(start)))
			{
				start++;
			}
			return start;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0007D690 File Offset: 0x0007B890
		private static bool TrimmedStringStartsWith(string str, string prefix)
		{
			int indexOfFirstNonWhitespaceChar = DocumentationCommentCompiler.GetIndexOfFirstNonWhitespaceChar(str);
			if (str.Length - indexOfFirstNonWhitespaceChar < prefix.Length)
			{
				return false;
			}
			for (int i = 0; i < prefix.Length; i++)
			{
				if (prefix.get_Chars(i) != str.get_Chars(i + indexOfFirstNonWhitespaceChar))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0007D6DC File Offset: 0x0007B8DC
		private static int IndexOfNewLine(string str, int start, out int newLineLength)
		{
			while (start < str.Length)
			{
				char c = str.get_Chars(start);
				if (c == '\n')
				{
					newLineLength = 1;
					return start;
				}
				if (c == '\r')
				{
					if (start + 1 < str.Length && str.get_Chars(start + 1) == '\n')
					{
						newLineLength = 2;
					}
					else
					{
						newLineLength = 1;
					}
					return start;
				}
				start++;
			}
			newLineLength = 0;
			return start;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0007D738 File Offset: 0x0007B938
		private void WriteFormattedSingleLineComment(string text)
		{
			bool flag = true;
			int num2;
			int num;
			for (int i = 0; i < text.Length; i = num + num2)
			{
				num = DocumentationCommentCompiler.IndexOfNewLine(text, i, out num2);
				int indexOfFirstNonWhitespaceChar = DocumentationCommentCompiler.GetIndexOfFirstNonWhitespaceChar(text, i, num);
				if (num - indexOfFirstNonWhitespaceChar < 4 || !SyntaxFacts.IsWhitespace(text.get_Chars(indexOfFirstNonWhitespaceChar + 3)))
				{
					flag = false;
					break;
				}
			}
			int num3 = (flag ? 4 : 3);
			int num5;
			int num4;
			for (int j = 0; j < text.Length; j = num4 + num5)
			{
				num4 = DocumentationCommentCompiler.IndexOfNewLine(text, j, out num5);
				int num6 = DocumentationCommentCompiler.GetIndexOfFirstNonWhitespaceChar(text, j, num4) + num3;
				this.WriteSubStringLine(text, num6, num4 - num6);
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0007D7D4 File Offset: 0x0007B9D4
		private void WriteFormattedMultiLineComment(string[] lines, int numLines)
		{
			bool flag = lines[0].Trim() == "/**";
			bool flag2 = lines[numLines - 1].Trim() == "*/";
			if (flag2)
			{
				numLines--;
			}
			int num = 0;
			if (numLines > 1)
			{
				string text = DocumentationCommentCompiler.FindMultiLineCommentPattern(lines[1]);
				if (text != null)
				{
					bool flag3 = true;
					for (int i = 2; i < numLines; i++)
					{
						string text2 = DocumentationCommentCompiler.LongestCommonPrefix(text, lines[i]);
						if (string.IsNullOrWhiteSpace(text2))
						{
							flag3 = false;
							break;
						}
						text = text2;
					}
					if (flag3)
					{
						num = text.Length;
					}
				}
			}
			if (!flag)
			{
				string text3 = lines[0].TrimStart(null);
				if (!flag2 && numLines == 1)
				{
					text3 = DocumentationCommentCompiler.TrimEndOfMultiLineComment(text3);
				}
				this.WriteLine(text3.Substring(SyntaxFacts.IsWhitespace(text3.get_Chars(3)) ? 4 : 3));
			}
			for (int j = 1; j < numLines; j++)
			{
				string text4 = lines[j].Substring(num);
				if (!flag2 && j == numLines - 1)
				{
					text4 = DocumentationCommentCompiler.TrimEndOfMultiLineComment(text4);
				}
				this.WriteLine(text4);
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0007D8D4 File Offset: 0x0007BAD4
		private static string TrimEndOfMultiLineComment(string trimmed)
		{
			int num = trimmed.IndexOf("*/", 4);
			if (num >= 0)
			{
				trimmed = trimmed.Substring(0, num);
			}
			return trimmed;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0007D900 File Offset: 0x0007BB00
		private static string FindMultiLineCommentPattern(string line)
		{
			int num = 0;
			bool flag = false;
			for (int i = 0; i < line.Length; i++)
			{
				char c = line.get_Chars(i);
				if (SyntaxFacts.IsWhitespace(c))
				{
					num++;
				}
				else
				{
					if (flag || c != '*')
					{
						break;
					}
					num++;
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return line.Substring(0, num);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0007D958 File Offset: 0x0007BB58
		private static string LongestCommonPrefix(string str1, string str2)
		{
			int num = 0;
			int num2 = Math.Min(str1.Length, str2.Length);
			while (num < num2 && str1.get_Chars(num) == str2.get_Chars(num))
			{
				num++;
			}
			return str1.Substring(0, num);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0007D99C File Offset: 0x0007BB9C
		private static string GetDocumentationCommentId(CrefSyntax crefSyntax, Binder binder, BindingDiagnosticBag diagnostics)
		{
			if (crefSyntax.ContainsDiagnostics)
			{
				return DocumentationCommentCompiler.ToBadCrefString(crefSyntax);
			}
			Symbol symbol;
			ImmutableArray<Symbol> immutableArray = binder.BindCref(crefSyntax, out symbol, diagnostics);
			int length = immutableArray.Length;
			if (length != 0)
			{
				Symbol symbol2;
				if (length != 1)
				{
					symbol2 = symbol;
				}
				else
				{
					symbol2 = immutableArray[0];
				}
				if (symbol2.Kind == SymbolKind.Alias)
				{
					symbol2 = ((AliasSymbol)symbol2).GetAliasTarget(null);
				}
				NamespaceSymbol namespaceSymbol = symbol2 as NamespaceSymbol;
				if (namespaceSymbol != null)
				{
					diagnostics.AddAssembliesUsedByNamespaceReference(namespaceSymbol);
				}
				else
				{
					diagnostics.AddDependencies((symbol2 as TypeSymbol) ?? symbol2.ContainingType);
				}
				return symbol2.OriginalDefinition.GetDocumentationCommentId();
			}
			return DocumentationCommentCompiler.ToBadCrefString(crefSyntax);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007DA34 File Offset: 0x0007BC34
		private static string ToBadCrefString(CrefSyntax cref)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				cref.WriteTo(stringWriter);
				text = "!:" + stringWriter.ToString().Replace("{", "&lt;").Replace("}", "&gt;");
			}
			return text;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0007DAA0 File Offset: 0x0007BCA0
		private static void BindName(XmlNameAttributeSyntax syntax, Binder binder, Symbol memberSymbol, ref HashSet<ParameterSymbol> documentedParameters, ref HashSet<TypeParameterSymbol> documentedTypeParameters, BindingDiagnosticBag diagnostics)
		{
			XmlNameAttributeElementKind elementKind = syntax.GetElementKind();
			if (elementKind == XmlNameAttributeElementKind.Parameter)
			{
				if (documentedParameters == null)
				{
					documentedParameters = new HashSet<ParameterSymbol>();
				}
			}
			else if (elementKind == XmlNameAttributeElementKind.TypeParameter && documentedTypeParameters == null)
			{
				documentedTypeParameters = new HashSet<TypeParameterSymbol>();
			}
			IdentifierNameSyntax identifier = syntax.Identifier;
			if (identifier.ContainsDiagnostics)
			{
				return;
			}
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = binder.GetNewCompoundUseSiteInfo(diagnostics);
			ImmutableArray<Symbol> immutableArray = binder.BindXmlNameAttribute(syntax, ref newCompoundUseSiteInfo);
			diagnostics.Add(syntax, newCompoundUseSiteInfo);
			if (!immutableArray.IsEmpty)
			{
				foreach (Symbol symbol in immutableArray)
				{
					if (elementKind == XmlNameAttributeElementKind.Parameter)
					{
						ParameterSymbol parameterSymbol = (ParameterSymbol)symbol;
						if (!parameterSymbol.ContainingSymbol.IsAccessor() && !documentedParameters.Add(parameterSymbol))
						{
							diagnostics.Add(ErrorCode.WRN_DuplicateParamTag, syntax.Location, new object[] { identifier });
						}
					}
					else if (elementKind == XmlNameAttributeElementKind.TypeParameter && !documentedTypeParameters.Add((TypeParameterSymbol)symbol))
					{
						diagnostics.Add(ErrorCode.WRN_DuplicateTypeParamTag, syntax.Location, new object[] { identifier });
					}
				}
				return;
			}
			switch (elementKind)
			{
			case XmlNameAttributeElementKind.Parameter:
				diagnostics.Add(ErrorCode.WRN_UnmatchedParamTag, identifier.Location, new object[] { identifier });
				return;
			case XmlNameAttributeElementKind.ParameterReference:
				diagnostics.Add(ErrorCode.WRN_UnmatchedParamRefTag, identifier.Location, new object[] { identifier, memberSymbol });
				return;
			case XmlNameAttributeElementKind.TypeParameter:
				diagnostics.Add(ErrorCode.WRN_UnmatchedTypeParamTag, identifier.Location, new object[] { identifier });
				return;
			case XmlNameAttributeElementKind.TypeParameterReference:
				diagnostics.Add(ErrorCode.WRN_UnmatchedTypeParamRefTag, identifier.Location, new object[] { identifier, memberSymbol });
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(elementKind);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0007DC4B File Offset: 0x0007BE4B
		private IComparer<CSharpSyntaxNode> Comparer
		{
			get
			{
				if (this._lazyComparer == null)
				{
					this._lazyComparer = new SyntaxNodeLocationComparer(this._compilation);
				}
				return this._lazyComparer;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0007DC6C File Offset: 0x0007BE6C
		private void BeginTemporaryString()
		{
			if (this._temporaryStringBuilders == null)
			{
				this._temporaryStringBuilders = new Stack<DocumentationCommentCompiler.TemporaryStringBuilder>();
			}
			this._temporaryStringBuilders.Push(new DocumentationCommentCompiler.TemporaryStringBuilder(this._indentDepth));
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0007DC98 File Offset: 0x0007BE98
		private string GetAndEndTemporaryString()
		{
			DocumentationCommentCompiler.TemporaryStringBuilder temporaryStringBuilder = this._temporaryStringBuilders.Pop();
			this._indentDepth = temporaryStringBuilder.InitialIndentDepth;
			return temporaryStringBuilder.Pooled.ToStringAndFree();
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0007DCC8 File Offset: 0x0007BEC8
		private void Indent()
		{
			this._indentDepth++;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0007DCD8 File Offset: 0x0007BED8
		private void Unindent()
		{
			this._indentDepth--;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0007DCE8 File Offset: 0x0007BEE8
		private void Write(string indentedAndWrappedString)
		{
			if (this._temporaryStringBuilders != null && this._temporaryStringBuilders.Count > 0)
			{
				this._temporaryStringBuilders.Peek().Pooled.Builder.Append(indentedAndWrappedString);
				return;
			}
			if (this._writer != null)
			{
				this._writer.Write(indentedAndWrappedString);
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0007DD3C File Offset: 0x0007BF3C
		private void WriteLine(string message)
		{
			Stack<DocumentationCommentCompiler.TemporaryStringBuilder> temporaryStringBuilders = this._temporaryStringBuilders;
			if (temporaryStringBuilders != null && temporaryStringBuilders.Count > 0)
			{
				StringBuilder builder = this._temporaryStringBuilders.Peek().Pooled.Builder;
				builder.Append(DocumentationCommentCompiler.MakeIndent(this._indentDepth));
				builder.AppendLine(message);
				return;
			}
			if (this._writer != null)
			{
				this._writer.Write(DocumentationCommentCompiler.MakeIndent(this._indentDepth));
				this._writer.WriteLine(message);
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0007DDBC File Offset: 0x0007BFBC
		private void WriteSubStringLine(string message, int start, int length)
		{
			Stack<DocumentationCommentCompiler.TemporaryStringBuilder> temporaryStringBuilders = this._temporaryStringBuilders;
			if (temporaryStringBuilders != null && temporaryStringBuilders.Count > 0)
			{
				StringBuilder builder = this._temporaryStringBuilders.Peek().Pooled.Builder;
				builder.Append(DocumentationCommentCompiler.MakeIndent(this._indentDepth));
				builder.Append(message, start, length);
				builder.AppendLine();
				return;
			}
			if (this._writer != null)
			{
				this._writer.Write(DocumentationCommentCompiler.MakeIndent(this._indentDepth));
				for (int i = 0; i < length; i++)
				{
					this._writer.Write(message.get_Chars(start + i));
				}
				this._writer.WriteLine();
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0007DE61 File Offset: 0x0007C061
		private void WriteLine(string format, params object[] args)
		{
			this.WriteLine(string.Format(format, args));
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0007DE70 File Offset: 0x0007C070
		private static string MakeIndent(int depth)
		{
			switch (depth)
			{
			case 0:
				return "";
			case 1:
				return "    ";
			case 2:
				return "        ";
			case 3:
				return "            ";
			default:
				return new string(' ', depth * 4);
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0007DEAC File Offset: 0x0007C0AC
		private static string GetDescription(XmlException e)
		{
			string message = e.Message;
			string text2;
			try
			{
				string text = string.Format(new ResourceManager("System.Xml", IntrospectionExtensions.GetTypeInfo(typeof(XmlException)).Assembly).GetString("Xml_MessageWithErrorPosition"), "", e.LineNumber, e.LinePosition);
				int num = message.IndexOf(text, 4);
				text2 = ((num < 0) ? message : message.Remove(num, text.Length));
			}
			catch
			{
				text2 = message;
			}
			return text2;
		}

		// Token: 0x0400065E RID: 1630
		private readonly string _assemblyName;

		// Token: 0x0400065F RID: 1631
		private readonly CSharpCompilation _compilation;

		// Token: 0x04000660 RID: 1632
		private readonly TextWriter _writer;

		// Token: 0x04000661 RID: 1633
		private readonly SyntaxTree _filterTree;

		// Token: 0x04000662 RID: 1634
		private readonly TextSpan? _filterSpanWithinTree;

		// Token: 0x04000663 RID: 1635
		private readonly bool _processIncludes;

		// Token: 0x04000664 RID: 1636
		private readonly bool _isForSingleSymbol;

		// Token: 0x04000665 RID: 1637
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x04000666 RID: 1638
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000667 RID: 1639
		private SyntaxNodeLocationComparer _lazyComparer;

		// Token: 0x04000668 RID: 1640
		private DocumentationCommentIncludeCache _includedFileCache;

		// Token: 0x04000669 RID: 1641
		private int _indentDepth;

		// Token: 0x0400066A RID: 1642
		private Stack<DocumentationCommentCompiler.TemporaryStringBuilder> _temporaryStringBuilders;

		// Token: 0x0400066B RID: 1643
		private static readonly string[] s_newLineSequences = new string[] { "\r\n", "\r", "\n" };

		// Token: 0x020006C4 RID: 1732
		private struct TemporaryStringBuilder
		{
			// Token: 0x0600844E RID: 33870 RVA: 0x001DF608 File Offset: 0x001DD808
			public TemporaryStringBuilder(int indentDepth)
			{
				this.InitialIndentDepth = indentDepth;
				this.Pooled = PooledStringBuilder.GetInstance();
			}

			// Token: 0x04002182 RID: 8578
			public readonly PooledStringBuilder Pooled;

			// Token: 0x04002183 RID: 8579
			public readonly int InitialIndentDepth;
		}

		// Token: 0x020006C5 RID: 1733
		private class DocumentationCommentWalker : CSharpSyntaxWalker
		{
			// Token: 0x0600844F RID: 33871 RVA: 0x001DF61C File Offset: 0x001DD81C
			private DocumentationCommentWalker(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, Symbol memberSymbol, TextWriter writer, ArrayBuilder<CSharpSyntaxNode> includeElementNodes, HashSet<ParameterSymbol> documentedParameters, HashSet<TypeParameterSymbol> documentedTypeParameters)
				: base(SyntaxWalkerDepth.StructuredTrivia)
			{
				this._compilation = compilation;
				this._diagnostics = diagnostics;
				this._memberSymbol = memberSymbol;
				this._writer = writer;
				this._includeElementNodes = includeElementNodes;
				this._documentedParameters = documentedParameters;
				this._documentedTypeParameters = documentedTypeParameters;
			}

			// Token: 0x06008450 RID: 33872 RVA: 0x001DF65C File Offset: 0x001DD85C
			public static string GetSubstitutedText(CSharpCompilation compilation, BindingDiagnosticBag diagnostics, Symbol symbol, DocumentationCommentTriviaSyntax trivia, ArrayBuilder<CSharpSyntaxNode> includeElementNodes, ref HashSet<ParameterSymbol> documentedParameters, ref HashSet<TypeParameterSymbol> documentedTypeParameters)
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				using (StringWriter stringWriter = new StringWriter(instance.Builder, CultureInfo.InvariantCulture))
				{
					DocumentationCommentCompiler.DocumentationCommentWalker documentationCommentWalker = new DocumentationCommentCompiler.DocumentationCommentWalker(compilation, diagnostics, symbol, stringWriter, includeElementNodes, documentedParameters, documentedTypeParameters);
					documentationCommentWalker.Visit(trivia);
					documentedParameters = documentationCommentWalker._documentedParameters;
					documentedTypeParameters = documentationCommentWalker._documentedTypeParameters;
				}
				return instance.ToStringAndFree();
			}

			// Token: 0x06008451 RID: 33873 RVA: 0x001DF6D0 File Offset: 0x001DD8D0
			public override void DefaultVisit(SyntaxNode node)
			{
				SyntaxKind syntaxKind = node.Kind();
				bool flag = node.SyntaxTree.ReportDocumentationCommentDiagnostics();
				if (syntaxKind == SyntaxKind.XmlCrefAttribute)
				{
					XmlCrefAttributeSyntax xmlCrefAttributeSyntax = (XmlCrefAttributeSyntax)node;
					CrefSyntax cref = xmlCrefAttributeSyntax.Cref;
					Binder binder = this._compilation.GetBinderFactory(cref.SyntaxTree, false).GetBinder(cref, null, null);
					string documentationCommentId = DocumentationCommentCompiler.GetDocumentationCommentId(cref, binder, flag ? this._diagnostics : new BindingDiagnosticBag(null, this._diagnostics.DependenciesBag));
					if (this._writer != null)
					{
						this.Visit(xmlCrefAttributeSyntax.Name);
						this.VisitToken(xmlCrefAttributeSyntax.EqualsToken);
						xmlCrefAttributeSyntax.StartQuoteToken.WriteTo(this._writer, true, false);
						this._writer.Write(documentationCommentId);
						xmlCrefAttributeSyntax.EndQuoteToken.WriteTo(this._writer, false, true);
					}
					return;
				}
				if (flag && syntaxKind == SyntaxKind.XmlNameAttribute)
				{
					XmlNameAttributeSyntax xmlNameAttributeSyntax = (XmlNameAttributeSyntax)node;
					Binder binder2 = this._compilation.GetBinderFactory(xmlNameAttributeSyntax.SyntaxTree, false).GetBinder(xmlNameAttributeSyntax, xmlNameAttributeSyntax.Identifier.SpanStart, null, null);
					DocumentationCommentCompiler.BindName(xmlNameAttributeSyntax, binder2, this._memberSymbol, ref this._documentedParameters, ref this._documentedTypeParameters, this._diagnostics);
				}
				if (this._includeElementNodes != null)
				{
					XmlNameSyntax xmlNameSyntax = null;
					if (syntaxKind == SyntaxKind.XmlEmptyElement)
					{
						xmlNameSyntax = ((XmlEmptyElementSyntax)node).Name;
					}
					else if (syntaxKind == SyntaxKind.XmlElementStartTag)
					{
						xmlNameSyntax = ((XmlElementStartTagSyntax)node).Name;
					}
					if (xmlNameSyntax != null && xmlNameSyntax.Prefix == null && DocumentationCommentXmlNames.ElementEquals(xmlNameSyntax.LocalName.ValueText, "include", false))
					{
						this._includeElementNodes.Add((CSharpSyntaxNode)node);
					}
				}
				base.DefaultVisit(node);
			}

			// Token: 0x06008452 RID: 33874 RVA: 0x001DF87E File Offset: 0x001DDA7E
			public override void VisitToken(SyntaxToken token)
			{
				if (this._writer != null)
				{
					token.WriteTo(this._writer);
				}
				base.VisitToken(token);
			}

			// Token: 0x04002184 RID: 8580
			private readonly CSharpCompilation _compilation;

			// Token: 0x04002185 RID: 8581
			private readonly BindingDiagnosticBag _diagnostics;

			// Token: 0x04002186 RID: 8582
			private readonly Symbol _memberSymbol;

			// Token: 0x04002187 RID: 8583
			private readonly TextWriter _writer;

			// Token: 0x04002188 RID: 8584
			private readonly ArrayBuilder<CSharpSyntaxNode> _includeElementNodes;

			// Token: 0x04002189 RID: 8585
			private HashSet<ParameterSymbol> _documentedParameters;

			// Token: 0x0400218A RID: 8586
			private HashSet<TypeParameterSymbol> _documentedTypeParameters;
		}

		// Token: 0x020006C6 RID: 1734
		private class IncludeElementExpander
		{
			// Token: 0x06008453 RID: 33875 RVA: 0x001DF89C File Offset: 0x001DDA9C
			private IncludeElementExpander(Symbol memberSymbol, ImmutableArray<CSharpSyntaxNode> sourceIncludeElementNodes, CSharpCompilation compilation, HashSet<ParameterSymbol> documentedParameters, HashSet<TypeParameterSymbol> documentedTypeParameters, DocumentationCommentIncludeCache includedFileCache, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
			{
				this._memberSymbol = memberSymbol;
				this._sourceIncludeElementNodes = sourceIncludeElementNodes;
				this._compilation = compilation;
				this._diagnostics = diagnostics;
				this._cancellationToken = cancellationToken;
				this._documentedParameters = documentedParameters;
				this._documentedTypeParameters = documentedTypeParameters;
				this._includedFileCache = includedFileCache;
				this._nextSourceIncludeElementIndex = 0;
			}

			// Token: 0x06008454 RID: 33876 RVA: 0x001DF8F4 File Offset: 0x001DDAF4
			public static void ProcessIncludes(string unprocessed, Symbol memberSymbol, ImmutableArray<CSharpSyntaxNode> sourceIncludeElementNodes, CSharpCompilation compilation, ref HashSet<ParameterSymbol> documentedParameters, ref HashSet<TypeParameterSymbol> documentedTypeParameters, ref DocumentationCommentIncludeCache includedFileCache, TextWriter writer, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
			{
				if (sourceIncludeElementNodes.IsEmpty)
				{
					if (writer != null)
					{
						writer.Write(unprocessed);
					}
					return;
				}
				XDocument xdocument;
				try
				{
					xdocument = XDocument.Parse(unprocessed, 1);
				}
				catch (XmlException)
				{
					if (writer != null)
					{
						writer.Write(unprocessed);
					}
					return;
				}
				cancellationToken.ThrowIfCancellationRequested();
				DocumentationCommentCompiler.IncludeElementExpander includeElementExpander = new DocumentationCommentCompiler.IncludeElementExpander(memberSymbol, sourceIncludeElementNodes, compilation, documentedParameters, documentedTypeParameters, includedFileCache, diagnostics, cancellationToken);
				foreach (XNode xnode in includeElementExpander.Rewrite(xdocument, null, null))
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (writer != null)
					{
						writer.Write(xnode);
					}
				}
				documentedParameters = includeElementExpander._documentedParameters;
				documentedTypeParameters = includeElementExpander._documentedTypeParameters;
				includedFileCache = includeElementExpander._includedFileCache;
			}

			// Token: 0x06008455 RID: 33877 RVA: 0x001DF9AC File Offset: 0x001DDBAC
			private XNode[] RewriteMany(XNode[] nodes, string currentXmlFilePath, CSharpSyntaxNode originatingSyntax)
			{
				ArrayBuilder<XNode> arrayBuilder = null;
				foreach (XNode xnode in nodes)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<XNode>.GetInstance();
					}
					arrayBuilder.AddRange(this.Rewrite(xnode, currentXmlFilePath, originatingSyntax));
				}
				if (arrayBuilder != null)
				{
					return arrayBuilder.ToArrayAndFree();
				}
				return Array.Empty<XNode>();
			}

			// Token: 0x06008456 RID: 33878 RVA: 0x001DF9F8 File Offset: 0x001DDBF8
			private XNode[] Rewrite(XNode node, string currentXmlFilePath, CSharpSyntaxNode originatingSyntax)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				string text = null;
				if (node.NodeType == 1)
				{
					XElement xelement = (XElement)node;
					if (DocumentationCommentCompiler.IncludeElementExpander.ElementNameIs(xelement, "include"))
					{
						XNode[] array = this.RewriteIncludeElement(xelement, currentXmlFilePath, originatingSyntax, out text);
						if (array != null)
						{
							return array;
						}
					}
				}
				XContainer xcontainer = node as XContainer;
				if (xcontainer == null)
				{
					return new XNode[] { node.Copy(false) };
				}
				IEnumerable<XNode> enumerable = xcontainer.Nodes();
				xcontainer = xcontainer.Copy(false);
				if (enumerable != null)
				{
					XNode[] array2 = this.RewriteMany(Enumerable.ToArray<XNode>(enumerable), currentXmlFilePath, originatingSyntax);
					XContainer xcontainer2 = xcontainer;
					object[] array3 = array2;
					xcontainer2.ReplaceNodes(array3);
				}
				if (xcontainer.NodeType == 1 && originatingSyntax != null)
				{
					XElement xelement2 = (XElement)xcontainer;
					foreach (XAttribute xattribute in xelement2.Attributes())
					{
						if (DocumentationCommentCompiler.IncludeElementExpander.AttributeNameIs(xattribute, "cref"))
						{
							this.BindAndReplaceCref(xattribute, originatingSyntax);
						}
						else if (DocumentationCommentCompiler.IncludeElementExpander.AttributeNameIs(xattribute, "name"))
						{
							if (DocumentationCommentCompiler.IncludeElementExpander.ElementNameIs(xelement2, "param") || DocumentationCommentCompiler.IncludeElementExpander.ElementNameIs(xelement2, "paramref"))
							{
								this.BindName(xattribute, originatingSyntax, true, false);
							}
							else if (DocumentationCommentCompiler.IncludeElementExpander.ElementNameIs(xelement2, "typeparam"))
							{
								this.BindName(xattribute, originatingSyntax, false, false);
							}
							else if (DocumentationCommentCompiler.IncludeElementExpander.ElementNameIs(xelement2, "typeparamref"))
							{
								this.BindName(xattribute, originatingSyntax, false, true);
							}
						}
					}
				}
				if (text == null)
				{
					return new XNode[] { xcontainer };
				}
				XComment xcomment = new XComment(text);
				return new XNode[] { xcomment, xcontainer };
			}

			// Token: 0x06008457 RID: 33879 RVA: 0x001DFB9C File Offset: 0x001DDD9C
			private static bool ElementNameIs(XElement element, string name)
			{
				return string.IsNullOrEmpty(element.Name.NamespaceName) && DocumentationCommentXmlNames.ElementEquals(element.Name.LocalName, name, false);
			}

			// Token: 0x06008458 RID: 33880 RVA: 0x001DFBC4 File Offset: 0x001DDDC4
			private static bool AttributeNameIs(XAttribute attribute, string name)
			{
				return string.IsNullOrEmpty(attribute.Name.NamespaceName) && DocumentationCommentXmlNames.AttributeEquals(attribute.Name.LocalName, name);
			}

			// Token: 0x06008459 RID: 33881 RVA: 0x001DFBEC File Offset: 0x001DDDEC
			private XNode[] RewriteIncludeElement(XElement includeElement, string currentXmlFilePath, CSharpSyntaxNode originatingSyntax, out string commentMessage)
			{
				Location includeElementLocation = this.GetIncludeElementLocation(includeElement, ref currentXmlFilePath, ref originatingSyntax);
				bool flag = originatingSyntax.SyntaxTree.ReportDocumentationCommentDiagnostics();
				if (!this.EnterIncludeElement(includeElementLocation))
				{
					XAttribute xattribute = includeElement.Attribute(XName.Get("file"));
					XAttribute xattribute2 = includeElement.Attribute(XName.Get("path"));
					string value = xattribute.Value;
					string value2 = xattribute2.Value;
					if (flag)
					{
						this._diagnostics.Add(ErrorCode.WRN_FailedInclude, includeElementLocation, new object[]
						{
							value,
							value2,
							new LocalizableErrorArgument(MessageID.IDS_OperationCausedStackOverflow)
						});
					}
					commentMessage = ErrorFacts.GetMessage(MessageID.IDS_XMLNOINCLUDE, CultureInfo.CurrentUICulture);
					return new XNode[]
					{
						new XComment(commentMessage),
						includeElement.Copy(false)
					};
				}
				DiagnosticBag instance = DiagnosticBag.GetInstance();
				XNode[] array;
				try
				{
					XAttribute xattribute3 = includeElement.Attribute(XName.Get("file"));
					XAttribute xattribute4 = includeElement.Attribute(XName.Get("path"));
					bool flag2 = xattribute3 != null;
					bool flag3 = xattribute4 != null;
					if (!flag2 || !flag3)
					{
						LocalizableErrorArgument localizableErrorArgument = (flag2 ? MessageID.IDS_XMLMISSINGINCLUDEPATH.Localize() : MessageID.IDS_XMLMISSINGINCLUDEFILE.Localize());
						instance.Add(ErrorCode.WRN_InvalidInclude, includeElementLocation, new object[] { localizableErrorArgument });
						commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLBADINCLUDE);
						array = null;
					}
					else
					{
						string value3 = xattribute4.Value;
						string value4 = xattribute3.Value;
						XmlReferenceResolver xmlReferenceResolver = this._compilation.Options.XmlReferenceResolver;
						if (xmlReferenceResolver == null)
						{
							instance.Add(ErrorCode.WRN_FailedInclude, includeElementLocation, new object[]
							{
								value4,
								value3,
								new CodeAnalysisResourcesLocalizableErrorArgument("XmlReferencesNotSupported")
							});
							commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLFAILEDINCLUDE);
							array = null;
						}
						else
						{
							string text = xmlReferenceResolver.ResolveReference(value4, currentXmlFilePath);
							if (text == null)
							{
								instance.Add(ErrorCode.WRN_FailedInclude, includeElementLocation, new object[]
								{
									value4,
									value3,
									new CodeAnalysisResourcesLocalizableErrorArgument("FileNotFound")
								});
								commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLFAILEDINCLUDE);
								array = null;
							}
							else
							{
								if (this._includedFileCache == null)
								{
									this._includedFileCache = new DocumentationCommentIncludeCache(xmlReferenceResolver);
								}
								try
								{
									XDocument orMakeDocument;
									try
									{
										orMakeDocument = this._includedFileCache.GetOrMakeDocument(text);
									}
									catch (IOException ex)
									{
										instance.Add(ErrorCode.WRN_FailedInclude, includeElementLocation, new object[] { value4, value3, ex.Message });
										commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLFAILEDINCLUDE);
										return null;
									}
									string text2;
									bool flag4;
									XElement[] array2 = XmlUtilities.TrySelectElements(orMakeDocument, value3, out text2, out flag4);
									if (array2 == null)
									{
										instance.Add(ErrorCode.WRN_FailedInclude, includeElementLocation, new object[] { value4, value3, text2 });
										commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLFAILEDINCLUDE);
										if (flag4)
										{
											array = null;
										}
										else if (includeElementLocation.IsInSource)
										{
											array = new XNode[]
											{
												new XComment(commentMessage)
											};
										}
										else
										{
											commentMessage = null;
											array = Array.Empty<XNode>();
										}
									}
									else
									{
										if (array2 != null && array2.Length != 0)
										{
											XNode[] array3 = array2;
											XNode[] array4 = this.RewriteMany(array3, text, originatingSyntax);
											if (array4.Length != 0)
											{
												commentMessage = null;
												return array4;
											}
										}
										commentMessage = DocumentationCommentCompiler.IncludeElementExpander.MakeCommentMessage(includeElementLocation, MessageID.IDS_XMLNOINCLUDE);
										array = null;
									}
								}
								catch (XmlException ex2)
								{
									Location location = XmlLocation.Create(ex2, text);
									instance.Add(ErrorCode.WRN_XMLParseIncludeError, location, new object[] { DocumentationCommentCompiler.GetDescription(ex2) });
									if (includeElementLocation.IsInSource)
									{
										commentMessage = string.Format(ErrorFacts.GetMessage(MessageID.IDS_XMLIGNORED2, CultureInfo.CurrentUICulture), text);
										array = new XNode[]
										{
											new XComment(commentMessage)
										};
									}
									else
									{
										commentMessage = null;
										array = Array.Empty<XNode>();
									}
								}
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						this._diagnostics.AddRange(instance);
					}
					instance.Free();
					this.LeaveIncludeElement(includeElementLocation);
				}
				return array;
			}

			// Token: 0x0600845A RID: 33882 RVA: 0x001E0004 File Offset: 0x001DE204
			private static string MakeCommentMessage(Location location, MessageID messageId)
			{
				if (location.IsInSource)
				{
					return ErrorFacts.GetMessage(messageId, CultureInfo.CurrentUICulture);
				}
				return null;
			}

			// Token: 0x0600845B RID: 33883 RVA: 0x001E001B File Offset: 0x001DE21B
			private bool EnterIncludeElement(Location location)
			{
				if (this._inProgressIncludeElementNodes == null)
				{
					this._inProgressIncludeElementNodes = new HashSet<Location>();
				}
				return this._inProgressIncludeElementNodes.Add(location);
			}

			// Token: 0x0600845C RID: 33884 RVA: 0x001E003C File Offset: 0x001DE23C
			private bool LeaveIncludeElement(Location location)
			{
				return this._inProgressIncludeElementNodes.Remove(location);
			}

			// Token: 0x0600845D RID: 33885 RVA: 0x001E004C File Offset: 0x001DE24C
			private Location GetIncludeElementLocation(XElement includeElement, ref string currentXmlFilePath, ref CSharpSyntaxNode originatingSyntax)
			{
				Location location = includeElement.Annotation<Location>();
				if (location != null)
				{
					return location;
				}
				if (currentXmlFilePath == null)
				{
					originatingSyntax = this._sourceIncludeElementNodes[this._nextSourceIncludeElementIndex];
					location = originatingSyntax.Location;
					this._nextSourceIncludeElementIndex++;
					currentXmlFilePath = location.GetLineSpan().Path;
				}
				else
				{
					location = XmlLocation.Create(includeElement, currentXmlFilePath);
				}
				includeElement.AddAnnotation(location);
				return location;
			}

			// Token: 0x0600845E RID: 33886 RVA: 0x001E00C0 File Offset: 0x001DE2C0
			private void BindAndReplaceCref(XAttribute attribute, CSharpSyntaxNode originatingSyntax)
			{
				CrefSyntax crefSyntax = SyntaxFactory.ParseCref(attribute.Value);
				if (crefSyntax == null)
				{
					return;
				}
				Location location = originatingSyntax.Location;
				this.RecordSyntaxDiagnostics(crefSyntax, location);
				MemberDeclarationSyntax associatedMemberForXmlSyntax = BinderFactory.GetAssociatedMemberForXmlSyntax(originatingSyntax);
				Binder binder = BinderFactory.MakeCrefBinder(crefSyntax, associatedMemberForXmlSyntax, this._compilation.GetBinderFactory(associatedMemberForXmlSyntax.SyntaxTree, false), false);
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
				attribute.Value = DocumentationCommentCompiler.GetDocumentationCommentId(crefSyntax, binder, instance);
				this.RecordBindingDiagnostics(instance, location);
				instance.Free();
			}

			// Token: 0x0600845F RID: 33887 RVA: 0x001E013C File Offset: 0x001DE33C
			private void BindName(XAttribute attribute, CSharpSyntaxNode originatingSyntax, bool isParameter, bool isTypeParameterRef)
			{
				XmlNameAttributeSyntax xmlNameAttributeSyntax = DocumentationCommentCompiler.IncludeElementExpander.ParseNameAttribute(attribute.ToString(), attribute.Parent.Name.LocalName);
				Location location = originatingSyntax.Location;
				this.RecordSyntaxDiagnostics(xmlNameAttributeSyntax, location);
				BinderFactory.GetAssociatedMemberForXmlSyntax(originatingSyntax);
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._diagnostics);
				Binder binder = DocumentationCommentCompiler.IncludeElementExpander.MakeNameBinder(isParameter, isTypeParameterRef, this._memberSymbol, this._compilation);
				DocumentationCommentCompiler.BindName(xmlNameAttributeSyntax, binder, this._memberSymbol, ref this._documentedParameters, ref this._documentedTypeParameters, instance);
				this.RecordBindingDiagnostics(instance, location);
				instance.Free();
			}

			// Token: 0x06008460 RID: 33888 RVA: 0x001E01C4 File Offset: 0x001DE3C4
			private static Binder MakeNameBinder(bool isParameter, bool isTypeParameterRef, Symbol memberSymbol, CSharpCompilation compilation)
			{
				Binder binder = new BuckStopsHereBinder(compilation);
				Symbol containingSymbol = memberSymbol.ContainingSymbol;
				binder = binder.WithContainingMemberOrLambda(containingSymbol);
				if (isParameter)
				{
					ImmutableArray<ParameterSymbol> immutableArray = ImmutableArray<ParameterSymbol>.Empty;
					SymbolKind symbolKind = memberSymbol.Kind;
					if (symbolKind <= SymbolKind.Method)
					{
						if (symbolKind != SymbolKind.ErrorType)
						{
							if (symbolKind != SymbolKind.Method)
							{
								goto IL_007F;
							}
							immutableArray = ((MethodSymbol)memberSymbol).Parameters;
							goto IL_007F;
						}
					}
					else if (symbolKind != SymbolKind.NamedType)
					{
						if (symbolKind != SymbolKind.Property)
						{
							goto IL_007F;
						}
						immutableArray = ((PropertySymbol)memberSymbol).Parameters;
						goto IL_007F;
					}
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)memberSymbol;
					if (namedTypeSymbol.IsDelegateType())
					{
						immutableArray = namedTypeSymbol.DelegateInvokeMethod.Parameters;
					}
					IL_007F:
					if (immutableArray.Length > 0)
					{
						binder = new WithParametersBinder(immutableArray, binder);
					}
				}
				else
				{
					Symbol symbol = memberSymbol;
					for (;;)
					{
						SymbolKind symbolKind = symbol.Kind;
						if (symbolKind == SymbolKind.ErrorType)
						{
							goto IL_00B0;
						}
						if (symbolKind != SymbolKind.Method)
						{
							if (symbolKind == SymbolKind.NamedType)
							{
								goto IL_00B0;
							}
						}
						else
						{
							MethodSymbol methodSymbol = (MethodSymbol)symbol;
							if (methodSymbol.Arity > 0)
							{
								binder = new WithMethodTypeParametersBinder(methodSymbol, binder);
							}
						}
						IL_00EA:
						symbol = symbol.ContainingSymbol;
						if (!isTypeParameterRef || symbol == null)
						{
							break;
						}
						continue;
						IL_00B0:
						NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)symbol;
						if (namedTypeSymbol2.Arity > 0)
						{
							binder = new WithClassTypeParametersBinder(namedTypeSymbol2, binder);
							goto IL_00EA;
						}
						goto IL_00EA;
					}
				}
				return binder;
			}

			// Token: 0x06008461 RID: 33889 RVA: 0x001E02CC File Offset: 0x001DE4CC
			private static XmlNameAttributeSyntax ParseNameAttribute(string attributeText, string elementName)
			{
				return (XmlNameAttributeSyntax)((XmlEmptyElementSyntax)((DocumentationCommentTriviaSyntax)SyntaxFactory.ParseLeadingTrivia(string.Format("/// <{0} {1}/>", elementName, attributeText), CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Diagnose), 0).ElementAt(0).GetStructure()).Content[1]).Attributes[0];
			}

			// Token: 0x06008462 RID: 33890 RVA: 0x001E0334 File Offset: 0x001DE534
			private void RecordSyntaxDiagnostics(CSharpSyntaxNode treelessSyntax, Location sourceLocation)
			{
				if (treelessSyntax.ContainsDiagnostics && sourceLocation.SourceTree.ReportDocumentationCommentDiagnostics())
				{
					foreach (Diagnostic diagnostic in CSharpSyntaxTree.Dummy.GetDiagnostics(treelessSyntax))
					{
						this._diagnostics.Add(diagnostic.WithLocation(sourceLocation));
					}
				}
			}

			// Token: 0x06008463 RID: 33891 RVA: 0x001E03A8 File Offset: 0x001DE5A8
			private void RecordBindingDiagnostics(BindingDiagnosticBag bindingDiagnostics, Location sourceLocation)
			{
				if (sourceLocation.SourceTree.ReportDocumentationCommentDiagnostics())
				{
					DiagnosticBag diagnosticBag = bindingDiagnostics.DiagnosticBag;
					if (diagnosticBag != null && !diagnosticBag.IsEmptyWithoutResolution)
					{
						foreach (Diagnostic diagnostic in bindingDiagnostics.DiagnosticBag.AsEnumerable())
						{
							this._diagnostics.Add(diagnostic.WithLocation(sourceLocation));
						}
					}
				}
				this._diagnostics.AddDependencies(bindingDiagnostics, false);
			}

			// Token: 0x0400218B RID: 8587
			private readonly Symbol _memberSymbol;

			// Token: 0x0400218C RID: 8588
			private readonly ImmutableArray<CSharpSyntaxNode> _sourceIncludeElementNodes;

			// Token: 0x0400218D RID: 8589
			private readonly CSharpCompilation _compilation;

			// Token: 0x0400218E RID: 8590
			private readonly BindingDiagnosticBag _diagnostics;

			// Token: 0x0400218F RID: 8591
			private readonly CancellationToken _cancellationToken;

			// Token: 0x04002190 RID: 8592
			private int _nextSourceIncludeElementIndex;

			// Token: 0x04002191 RID: 8593
			private HashSet<Location> _inProgressIncludeElementNodes;

			// Token: 0x04002192 RID: 8594
			private HashSet<ParameterSymbol> _documentedParameters;

			// Token: 0x04002193 RID: 8595
			private HashSet<TypeParameterSymbol> _documentedTypeParameters;

			// Token: 0x04002194 RID: 8596
			private DocumentationCommentIncludeCache _includedFileCache;
		}
	}
}
