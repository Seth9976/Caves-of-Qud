using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003FF RID: 1023
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class CommonForEachStatementSyntax : StatementSyntax
	{
		// Token: 0x06005865 RID: 22629 RVA: 0x0015B0C5 File Offset: 0x001592C5
		internal CommonForEachStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0015B0D0 File Offset: 0x001592D0
		internal CommonForEachStatementSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0015B0D9 File Offset: 0x001592D9
		protected CommonForEachStatementSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06005868 RID: 22632
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract SyntaxToken AwaitKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06005869 RID: 22633
		public abstract SyntaxToken ForEachKeyword { get; }

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600586A RID: 22634
		public abstract SyntaxToken OpenParenToken { get; }

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x0600586B RID: 22635
		public abstract SyntaxToken InKeyword { get; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x0600586C RID: 22636
		public abstract ExpressionSyntax Expression { get; }

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x0600586D RID: 22637
		public abstract SyntaxToken CloseParenToken { get; }

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x0600586E RID: 22638
		public abstract StatementSyntax Statement { get; }
	}
}
