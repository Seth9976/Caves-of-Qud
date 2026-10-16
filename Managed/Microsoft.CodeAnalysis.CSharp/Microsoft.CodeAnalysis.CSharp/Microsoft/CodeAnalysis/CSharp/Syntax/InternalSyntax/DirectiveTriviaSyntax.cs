using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036F RID: 879
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class DirectiveTriviaSyntax : StructuredTriviaSyntax
	{
		// Token: 0x06004F1D RID: 20253 RVA: 0x0014123D File Offset: 0x0013F43D
		internal override DirectiveStack ApplyDirectives(DirectiveStack stack)
		{
			return stack.Add(new Directive(this));
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0014124C File Offset: 0x0013F44C
		internal DirectiveTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			this.flags |= GreenNode.NodeFlags.ContainsDirectives;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x00141265 File Offset: 0x0013F465
		internal DirectiveTriviaSyntax(SyntaxKind kind)
			: base(kind, null, null)
		{
			this.flags |= GreenNode.NodeFlags.ContainsDirectives;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x0014127E File Offset: 0x0013F47E
		protected DirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			this.flags |= GreenNode.NodeFlags.ContainsDirectives;
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004F21 RID: 20257
		public abstract SyntaxToken HashToken { get; }

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004F22 RID: 20258
		public abstract SyntaxToken EndOfDirectiveToken { get; }

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004F23 RID: 20259
		public abstract bool IsActive { get; }
	}
}
