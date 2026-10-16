using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000084 RID: 132
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagRelationalTest : BoundDagTest
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00051EC3 File Offset: 0x000500C3
		public BinaryOperatorKind Relation
		{
			get
			{
				return this.OperatorKind.Operator();
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00051ED0 File Offset: 0x000500D0
		public BoundDagRelationalTest(SyntaxNode syntax, BinaryOperatorKind operatorKind, ConstantValue value, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagRelationalTest, syntax, input, hasErrors || input.HasErrors())
		{
			this.OperatorKind = operatorKind;
			this.Value = value;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00051EF9 File Offset: 0x000500F9
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00051F01 File Offset: 0x00050101
		public ConstantValue Value { get; }

		// Token: 0x06000B2B RID: 2859 RVA: 0x00051F09 File Offset: 0x00050109
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagRelationalTest(this);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00051F12 File Offset: 0x00050112
		public BoundDagRelationalTest Update(BinaryOperatorKind operatorKind, ConstantValue value, BoundDagTemp input)
		{
			if (operatorKind != this.OperatorKind || value != this.Value || input != base.Input)
			{
				BoundDagRelationalTest boundDagRelationalTest = new BoundDagRelationalTest(this.Syntax, operatorKind, value, input, base.HasErrors);
				boundDagRelationalTest.CopyAttributes(this);
				return boundDagRelationalTest;
			}
			return this;
		}
	}
}
