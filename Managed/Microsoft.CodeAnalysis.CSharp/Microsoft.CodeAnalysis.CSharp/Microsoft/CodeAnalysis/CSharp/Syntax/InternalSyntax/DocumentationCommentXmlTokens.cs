using System;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200035F RID: 863
	internal static class DocumentationCommentXmlTokens
	{
		// Token: 0x0600489B RID: 18587 RVA: 0x0011EFC8 File Offset: 0x0011D1C8
		private static SyntaxToken Identifier(string text)
		{
			return SyntaxFactory.Identifier(SyntaxKind.None, null, text, text, null);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0011EFD4 File Offset: 0x0011D1D4
		private static SyntaxToken IdentifierWithLeadingSpace(string text)
		{
			return SyntaxFactory.Identifier(SyntaxKind.None, SyntaxFactory.Space, text, text, null);
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0011EFE4 File Offset: 0x0011D1E4
		private static bool IsSingleSpaceTrivia(SyntaxListBuilder syntax)
		{
			return syntax.Count == 1 && SyntaxFactory.Space.IsEquivalentTo(syntax[0]);
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0011F002 File Offset: 0x0011D202
		public static SyntaxToken LookupToken(string text, SyntaxListBuilder leading)
		{
			if (leading == null)
			{
				return DocumentationCommentXmlTokens.LookupXmlElementTag(text);
			}
			if (DocumentationCommentXmlTokens.IsSingleSpaceTrivia(leading))
			{
				return DocumentationCommentXmlTokens.LookupXmlAttribute(text);
			}
			return null;
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0011F020 File Offset: 0x0011D220
		private static SyntaxToken LookupXmlElementTag(string text)
		{
			switch (text.Length)
			{
			case 3:
				if (text == "see")
				{
					return DocumentationCommentXmlTokens.s_seeToken;
				}
				break;
			case 4:
				if (text == "code")
				{
					return DocumentationCommentXmlTokens.s_codeToken;
				}
				if (text == "list")
				{
					return DocumentationCommentXmlTokens.s_listToken;
				}
				break;
			case 5:
				if (text == "param")
				{
					return DocumentationCommentXmlTokens.s_paramToken;
				}
				if (text == "value")
				{
					return DocumentationCommentXmlTokens.s_valueToken;
				}
				break;
			case 7:
				if (text == "example")
				{
					return DocumentationCommentXmlTokens.s_exampleToken;
				}
				if (text == "include")
				{
					return DocumentationCommentXmlTokens.s_includeToken;
				}
				if (text == "remarks")
				{
					return DocumentationCommentXmlTokens.s_remarksToken;
				}
				if (text == "seealso")
				{
					return DocumentationCommentXmlTokens.s_seealsoToken;
				}
				if (text == "summary")
				{
					return DocumentationCommentXmlTokens.s_summaryToken;
				}
				break;
			case 9:
				if (text == "exception")
				{
					return DocumentationCommentXmlTokens.s_exceptionToken;
				}
				if (text == "typeparam")
				{
					return DocumentationCommentXmlTokens.s_typeparamToken;
				}
				break;
			case 10:
				if (text == "permission")
				{
					return DocumentationCommentXmlTokens.s_permissionToken;
				}
				break;
			case 12:
				if (text == "typeparam")
				{
					return DocumentationCommentXmlTokens.s_typeparamrefToken;
				}
				break;
			}
			return null;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0011F188 File Offset: 0x0011D388
		private static SyntaxToken LookupXmlAttribute(string text)
		{
			if (text.Length != 4)
			{
				return null;
			}
			if (text == "cref")
			{
				return DocumentationCommentXmlTokens.s_crefToken;
			}
			if (text == "file")
			{
				return DocumentationCommentXmlTokens.s_fileToken;
			}
			if (text == "name")
			{
				return DocumentationCommentXmlTokens.s_nameToken;
			}
			if (text == "path")
			{
				return DocumentationCommentXmlTokens.s_pathToken;
			}
			if (!(text == "type"))
			{
				return null;
			}
			return DocumentationCommentXmlTokens.s_typeToken;
		}

		// Token: 0x04001547 RID: 5447
		private static readonly SyntaxToken s_seeToken = DocumentationCommentXmlTokens.Identifier("see");

		// Token: 0x04001548 RID: 5448
		private static readonly SyntaxToken s_codeToken = DocumentationCommentXmlTokens.Identifier("code");

		// Token: 0x04001549 RID: 5449
		private static readonly SyntaxToken s_listToken = DocumentationCommentXmlTokens.Identifier("list");

		// Token: 0x0400154A RID: 5450
		private static readonly SyntaxToken s_paramToken = DocumentationCommentXmlTokens.Identifier("param");

		// Token: 0x0400154B RID: 5451
		private static readonly SyntaxToken s_valueToken = DocumentationCommentXmlTokens.Identifier("value");

		// Token: 0x0400154C RID: 5452
		private static readonly SyntaxToken s_exampleToken = DocumentationCommentXmlTokens.Identifier("example");

		// Token: 0x0400154D RID: 5453
		private static readonly SyntaxToken s_includeToken = DocumentationCommentXmlTokens.Identifier("include");

		// Token: 0x0400154E RID: 5454
		private static readonly SyntaxToken s_remarksToken = DocumentationCommentXmlTokens.Identifier("remarks");

		// Token: 0x0400154F RID: 5455
		private static readonly SyntaxToken s_seealsoToken = DocumentationCommentXmlTokens.Identifier("seealso");

		// Token: 0x04001550 RID: 5456
		private static readonly SyntaxToken s_summaryToken = DocumentationCommentXmlTokens.Identifier("summary");

		// Token: 0x04001551 RID: 5457
		private static readonly SyntaxToken s_exceptionToken = DocumentationCommentXmlTokens.Identifier("exception");

		// Token: 0x04001552 RID: 5458
		private static readonly SyntaxToken s_typeparamToken = DocumentationCommentXmlTokens.Identifier("typeparam");

		// Token: 0x04001553 RID: 5459
		private static readonly SyntaxToken s_permissionToken = DocumentationCommentXmlTokens.Identifier("permission");

		// Token: 0x04001554 RID: 5460
		private static readonly SyntaxToken s_typeparamrefToken = DocumentationCommentXmlTokens.Identifier("typeparamref");

		// Token: 0x04001555 RID: 5461
		private static readonly SyntaxToken s_crefToken = DocumentationCommentXmlTokens.IdentifierWithLeadingSpace("cref");

		// Token: 0x04001556 RID: 5462
		private static readonly SyntaxToken s_fileToken = DocumentationCommentXmlTokens.IdentifierWithLeadingSpace("file");

		// Token: 0x04001557 RID: 5463
		private static readonly SyntaxToken s_nameToken = DocumentationCommentXmlTokens.IdentifierWithLeadingSpace("name");

		// Token: 0x04001558 RID: 5464
		private static readonly SyntaxToken s_pathToken = DocumentationCommentXmlTokens.IdentifierWithLeadingSpace("path");

		// Token: 0x04001559 RID: 5465
		private static readonly SyntaxToken s_typeToken = DocumentationCommentXmlTokens.IdentifierWithLeadingSpace("type");
	}
}
