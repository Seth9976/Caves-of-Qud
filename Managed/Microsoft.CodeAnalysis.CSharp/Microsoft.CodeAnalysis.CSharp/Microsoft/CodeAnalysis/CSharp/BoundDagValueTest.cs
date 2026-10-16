using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B4 RID: 436
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagValueTest : BoundDagTest
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x000A2573 File Offset: 0x000A0773
		public BoundDagValueTest(SyntaxNode syntax, ConstantValue value, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagValueTest, syntax, input, hasErrors || input.HasErrors())
		{
			this.Value = value;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x000A2593 File Offset: 0x000A0793
		public ConstantValue Value { get; }

		// Token: 0x06001F64 RID: 8036 RVA: 0x000A259B File Offset: 0x000A079B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagValueTest(this);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000A25A4 File Offset: 0x000A07A4
		public BoundDagValueTest Update(ConstantValue value, BoundDagTemp input)
		{
			if (value != this.Value || input != base.Input)
			{
				BoundDagValueTest boundDagValueTest = new BoundDagValueTest(this.Syntax, value, input, base.HasErrors);
				boundDagValueTest.CopyAttributes(this);
				return boundDagValueTest;
			}
			return this;
		}
	}
}
