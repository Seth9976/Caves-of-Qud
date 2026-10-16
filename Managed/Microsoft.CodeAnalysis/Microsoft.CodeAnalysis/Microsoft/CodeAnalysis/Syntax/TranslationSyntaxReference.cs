using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002AA RID: 682
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class TranslationSyntaxReference : SyntaxReference
	{
		// Token: 0x06001CE8 RID: 7400 RVA: 0x00054E0F File Offset: 0x0005300F
		protected TranslationSyntaxReference(SyntaxReference reference)
		{
			this._reference = reference;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x00054E1E File Offset: 0x0005301E
		public sealed override TextSpan Span
		{
			get
			{
				return this._reference.Span;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00054E2B File Offset: 0x0005302B
		public sealed override SyntaxTree SyntaxTree
		{
			get
			{
				return this._reference.SyntaxTree;
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00054E38 File Offset: 0x00053038
		public sealed override SyntaxNode GetSyntax(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Translate(this._reference, cancellationToken);
		}

		// Token: 0x06001CEC RID: 7404
		protected abstract SyntaxNode Translate(SyntaxReference reference, CancellationToken cancellationToken);

		// Token: 0x04000D5F RID: 3423
		private readonly SyntaxReference _reference;
	}
}
