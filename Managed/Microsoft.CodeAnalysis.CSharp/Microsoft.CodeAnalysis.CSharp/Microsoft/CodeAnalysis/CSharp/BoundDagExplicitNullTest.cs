using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B3 RID: 435
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagExplicitNullTest : BoundDagTest
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x000A252C File Offset: 0x000A072C
		public BoundDagExplicitNullTest(SyntaxNode syntax, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagExplicitNullTest, syntax, input, hasErrors || input.HasErrors())
		{
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000A2544 File Offset: 0x000A0744
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagExplicitNullTest(this);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000A254D File Offset: 0x000A074D
		public BoundDagExplicitNullTest Update(BoundDagTemp input)
		{
			if (input != base.Input)
			{
				BoundDagExplicitNullTest boundDagExplicitNullTest = new BoundDagExplicitNullTest(this.Syntax, input, base.HasErrors);
				boundDagExplicitNullTest.CopyAttributes(this);
				return boundDagExplicitNullTest;
			}
			return this;
		}
	}
}
