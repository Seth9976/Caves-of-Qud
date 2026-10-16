using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B2 RID: 434
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagNonNullTest : BoundDagTest
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x000A24CB File Offset: 0x000A06CB
		public BoundDagNonNullTest(SyntaxNode syntax, bool isExplicitTest, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagNonNullTest, syntax, input, hasErrors || input.HasErrors())
		{
			this.IsExplicitTest = isExplicitTest;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x000A24EB File Offset: 0x000A06EB
		public bool IsExplicitTest { get; }

		// Token: 0x06001F5D RID: 8029 RVA: 0x000A24F3 File Offset: 0x000A06F3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagNonNullTest(this);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000A24FC File Offset: 0x000A06FC
		public BoundDagNonNullTest Update(bool isExplicitTest, BoundDagTemp input)
		{
			if (isExplicitTest != this.IsExplicitTest || input != base.Input)
			{
				BoundDagNonNullTest boundDagNonNullTest = new BoundDagNonNullTest(this.Syntax, isExplicitTest, input, base.HasErrors);
				boundDagNonNullTest.CopyAttributes(this);
				return boundDagNonNullTest;
			}
			return this;
		}
	}
}
