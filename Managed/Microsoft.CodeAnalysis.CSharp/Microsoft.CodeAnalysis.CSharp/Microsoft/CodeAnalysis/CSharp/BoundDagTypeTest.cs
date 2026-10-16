using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B1 RID: 433
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagTypeTest : BoundDagTest
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x000A2464 File Offset: 0x000A0664
		public BoundDagTypeTest(SyntaxNode syntax, TypeSymbol type, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagTypeTest, syntax, input, hasErrors || input.HasErrors())
		{
			this.Type = type;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000A2484 File Offset: 0x000A0684
		public TypeSymbol Type { get; }

		// Token: 0x06001F59 RID: 8025 RVA: 0x000A248C File Offset: 0x000A068C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagTypeTest(this);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000A2495 File Offset: 0x000A0695
		public BoundDagTypeTest Update(TypeSymbol type, BoundDagTemp input)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything) || input != base.Input)
			{
				BoundDagTypeTest boundDagTypeTest = new BoundDagTypeTest(this.Syntax, type, input, base.HasErrors);
				boundDagTypeTest.CopyAttributes(this);
				return boundDagTypeTest;
			}
			return this;
		}
	}
}
