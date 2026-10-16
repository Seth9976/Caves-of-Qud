using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011F RID: 287
	public static class CSharpFileSystemExtensions
	{
		// Token: 0x0600183B RID: 6203 RVA: 0x00082CFA File Offset: 0x00080EFA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static EmitResult Emit(this CSharpCompilation compilation, string outputPath, [global::System.Runtime.CompilerServices.Nullable(2)] string pdbPath = null, [global::System.Runtime.CompilerServices.Nullable(2)] string xmlDocumentationPath = null, [global::System.Runtime.CompilerServices.Nullable(2)] string win32ResourcesPath = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return compilation.Emit(outputPath, pdbPath, xmlDocumentationPath, win32ResourcesPath, manifestResources, cancellationToken);
		}
	}
}
