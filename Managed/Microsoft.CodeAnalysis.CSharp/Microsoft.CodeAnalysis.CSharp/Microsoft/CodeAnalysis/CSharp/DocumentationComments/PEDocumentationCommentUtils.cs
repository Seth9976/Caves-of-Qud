using System;
using System.Globalization;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;

namespace Microsoft.CodeAnalysis.CSharp.DocumentationComments
{
	// Token: 0x020004A3 RID: 1187
	internal static class PEDocumentationCommentUtils
	{
		// Token: 0x06006241 RID: 25153 RVA: 0x001798B8 File Offset: 0x00177AB8
		internal static string GetDocumentationComment(Symbol symbol, PEModuleSymbol containingPEModule, CultureInfo preferredCulture, CancellationToken cancellationToken, ref Tuple<CultureInfo, string> lazyDocComment)
		{
			if (lazyDocComment == null)
			{
				Interlocked.CompareExchange<Tuple<CultureInfo, string>>(ref lazyDocComment, Tuple.Create<CultureInfo, string>(preferredCulture, containingPEModule.DocumentationProvider.GetDocumentationForSymbol(symbol.GetDocumentationCommentId(), preferredCulture, cancellationToken)), null);
			}
			if (object.Equals(lazyDocComment.Item1, preferredCulture))
			{
				return lazyDocComment.Item2;
			}
			return containingPEModule.DocumentationProvider.GetDocumentationForSymbol(symbol.GetDocumentationCommentId(), preferredCulture, cancellationToken);
		}
	}
}
