using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046D RID: 1133
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class ConditionalDirectiveTriviaSyntax : BranchingDirectiveTriviaSyntax
	{
		// Token: 0x06005F0E RID: 24334 RVA: 0x0017061F File Offset: 0x0016E81F
		internal ConditionalDirectiveTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x0017062A File Offset: 0x0016E82A
		internal ConditionalDirectiveTriviaSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x00170633 File Offset: 0x0016E833
		protected ConditionalDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06005F11 RID: 24337
		public abstract ExpressionSyntax Condition { get; }

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06005F12 RID: 24338
		public abstract bool ConditionValue { get; }
	}
}
