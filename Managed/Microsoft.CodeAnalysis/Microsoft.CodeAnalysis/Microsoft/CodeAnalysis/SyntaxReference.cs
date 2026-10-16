using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000280 RID: 640
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SyntaxReference
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060019D8 RID: 6616
		public abstract SyntaxTree SyntaxTree { get; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060019D9 RID: 6617
		public abstract TextSpan Span { get; }

		// Token: 0x060019DA RID: 6618
		public abstract SyntaxNode GetSyntax(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060019DB RID: 6619 RVA: 0x0004D62E File Offset: 0x0004B82E
		public virtual Task<SyntaxNode> GetSyntaxAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult<SyntaxNode>(this.GetSyntax(cancellationToken));
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0004D63C File Offset: 0x0004B83C
		internal Location GetLocation()
		{
			return this.SyntaxTree.GetLocation(this.Span);
		}
	}
}
