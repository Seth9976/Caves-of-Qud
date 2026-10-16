using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000104 RID: 260
	internal static class CSharpCompilationExtensions
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x0006F9B8 File Offset: 0x0006DBB8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static bool IsFeatureEnabled(this CSharpCompilation compilation, MessageID feature)
		{
			SyntaxTree syntaxTree = compilation.SyntaxTrees.FirstOrDefault<SyntaxTree>();
			CSharpParseOptions csharpParseOptions = (CSharpParseOptions)((syntaxTree != null) ? syntaxTree.Options : null);
			return csharpParseOptions != null && csharpParseOptions.IsFeatureEnabled(feature);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0006F9E2 File Offset: 0x0006DBE2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsFeatureEnabled(this SyntaxNode syntax, MessageID feature)
		{
			CSharpParseOptions csharpParseOptions = (CSharpParseOptions)((syntax != null) ? syntax.SyntaxTree.Options : null);
			return csharpParseOptions != null && csharpParseOptions.IsFeatureEnabled(feature);
		}
	}
}
