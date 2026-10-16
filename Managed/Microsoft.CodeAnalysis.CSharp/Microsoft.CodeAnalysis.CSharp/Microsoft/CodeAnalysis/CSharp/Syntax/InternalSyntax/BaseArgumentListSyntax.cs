using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A9 RID: 937
	internal abstract class BaseArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005363 RID: 21347 RVA: 0x0014CF11 File Offset: 0x0014B111
		internal BaseArgumentListSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0014CF1C File Offset: 0x0014B11C
		internal BaseArgumentListSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0014CF25 File Offset: 0x0014B125
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseArgumentListSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06005366 RID: 21350
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
