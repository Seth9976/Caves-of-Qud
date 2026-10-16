using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000130 RID: 304
	internal static class SourceDocumentationCommentUtils
	{
		// Token: 0x060018DA RID: 6362 RVA: 0x000850E4 File Offset: 0x000832E4
		internal static string GetAndCacheDocumentationComment(Symbol symbol, bool expandIncludes, ref string lazyXmlText)
		{
			if (lazyXmlText == null)
			{
				string documentationCommentXml = DocumentationCommentCompiler.GetDocumentationCommentXml(symbol, expandIncludes, default(CancellationToken));
				Interlocked.CompareExchange<string>(ref lazyXmlText, documentationCommentXml, null);
			}
			return lazyXmlText;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00085114 File Offset: 0x00083314
		internal static ImmutableArray<DocumentationCommentTriviaSyntax> GetDocumentationCommentTriviaFromSyntaxNode(CSharpSyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			if (syntaxNode.SyntaxTree.Options.DocumentationMode < DocumentationMode.Parse)
			{
				return ImmutableArray<DocumentationCommentTriviaSyntax>.Empty;
			}
			if (syntaxNode.Kind() == SyntaxKind.VariableDeclarator)
			{
				CSharpSyntaxNode csharpSyntaxNode;
				for (csharpSyntaxNode = syntaxNode; csharpSyntaxNode != null; csharpSyntaxNode = csharpSyntaxNode.Parent)
				{
					SyntaxKind syntaxKind = csharpSyntaxNode.Kind();
					if (syntaxKind == SyntaxKind.FieldDeclaration || syntaxKind == SyntaxKind.EventFieldDeclaration)
					{
						break;
					}
				}
				if (csharpSyntaxNode != null)
				{
					syntaxNode = csharpSyntaxNode;
				}
			}
			ArrayBuilder<DocumentationCommentTriviaSyntax> arrayBuilder = null;
			bool flag = false;
			foreach (SyntaxTrivia syntaxTrivia in syntaxNode.GetLeadingTrivia().Reverse())
			{
				SyntaxKind syntaxKind2 = syntaxTrivia.Kind();
				if (syntaxKind2 - SyntaxKind.EndOfLineTrivia > 1)
				{
					if (syntaxKind2 - SyntaxKind.SingleLineDocumentationCommentTrivia <= 1)
					{
						if (flag)
						{
							SyntaxTree syntaxTree = syntaxTrivia.SyntaxTree;
							if (syntaxTree.ReportDocumentationCommentDiagnostics())
							{
								int position = syntaxTrivia.Position;
								diagnostics.Add(ErrorCode.WRN_UnprocessedXMLComment, new SourceLocation(syntaxTree, new TextSpan(position, 1)));
							}
						}
						else
						{
							if (arrayBuilder == null)
							{
								arrayBuilder = ArrayBuilder<DocumentationCommentTriviaSyntax>.GetInstance();
							}
							arrayBuilder.Add((DocumentationCommentTriviaSyntax)syntaxTrivia.GetStructure());
						}
					}
					else if (arrayBuilder != null)
					{
						flag = true;
					}
				}
			}
			if (arrayBuilder == null)
			{
				return ImmutableArray<DocumentationCommentTriviaSyntax>.Empty;
			}
			arrayBuilder.ReverseContents();
			return arrayBuilder.ToImmutableAndFree();
		}
	}
}
