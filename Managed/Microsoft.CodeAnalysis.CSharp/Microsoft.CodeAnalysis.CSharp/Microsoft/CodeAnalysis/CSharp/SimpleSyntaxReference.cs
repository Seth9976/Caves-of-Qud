using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000232 RID: 562
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SimpleSyntaxReference : SyntaxReference
	{
		// Token: 0x06002ED1 RID: 11985 RVA: 0x000F0D18 File Offset: 0x000EEF18
		internal SimpleSyntaxReference(SyntaxNode node)
		{
			this._node = node;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000F0D27 File Offset: 0x000EEF27
		public override SyntaxTree SyntaxTree
		{
			get
			{
				return this._node.SyntaxTree;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000F0D34 File Offset: 0x000EEF34
		public override TextSpan Span
		{
			get
			{
				return this._node.Span;
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000F0D41 File Offset: 0x000EEF41
		public override SyntaxNode GetSyntax(CancellationToken cancellationToken)
		{
			return this._node;
		}

		// Token: 0x0400115B RID: 4443
		private readonly SyntaxNode _node;
	}
}
