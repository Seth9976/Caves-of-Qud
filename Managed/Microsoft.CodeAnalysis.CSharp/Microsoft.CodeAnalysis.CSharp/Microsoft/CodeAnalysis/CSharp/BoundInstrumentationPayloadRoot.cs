using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200017C RID: 380
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundInstrumentationPayloadRoot : BoundExpression
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x000A0648 File Offset: 0x0009E848
		public BoundInstrumentationPayloadRoot(SyntaxNode syntax, int analysisKind, TypeSymbol type, bool hasErrors)
			: base(BoundKind.InstrumentationPayloadRoot, syntax, type, hasErrors)
		{
			this.AnalysisKind = analysisKind;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000A065D File Offset: 0x0009E85D
		public BoundInstrumentationPayloadRoot(SyntaxNode syntax, int analysisKind, TypeSymbol type)
			: base(BoundKind.InstrumentationPayloadRoot, syntax, type)
		{
			this.AnalysisKind = analysisKind;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000A0670 File Offset: 0x0009E870
		public int AnalysisKind { get; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000A0678 File Offset: 0x0009E878
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000A0680 File Offset: 0x0009E880
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitInstrumentationPayloadRoot(this);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000A0689 File Offset: 0x0009E889
		public BoundInstrumentationPayloadRoot Update(int analysisKind, TypeSymbol type)
		{
			if (analysisKind != this.AnalysisKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundInstrumentationPayloadRoot boundInstrumentationPayloadRoot = new BoundInstrumentationPayloadRoot(this.Syntax, analysisKind, type, base.HasErrors);
				boundInstrumentationPayloadRoot.CopyAttributes(this);
				return boundInstrumentationPayloadRoot;
			}
			return this;
		}
	}
}
