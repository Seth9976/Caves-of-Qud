using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C4 RID: 452
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNoPiaObjectCreationExpression : BoundExpression
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x000A2EA2 File Offset: 0x000A10A2
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundNoPiaObjectCreationExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] string guidString, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.NoPiaObjectCreationExpression, syntax, type, hasErrors || initializerExpressionOpt.HasErrors())
		{
			this.GuidString = guidString;
			this.InitializerExpressionOpt = initializerExpressionOpt;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x000A2ECD File Offset: 0x000A10CD
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x000A2ED5 File Offset: 0x000A10D5
		public string GuidString { get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000A2EDD File Offset: 0x000A10DD
		public BoundObjectInitializerExpressionBase InitializerExpressionOpt { get; }

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000A2EE5 File Offset: 0x000A10E5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNoPiaObjectCreationExpression(this);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000A2EF0 File Offset: 0x000A10F0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundNoPiaObjectCreationExpression Update([global::System.Runtime.CompilerServices.Nullable(2)] string guidString, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type)
		{
			if (guidString != this.GuidString || initializerExpressionOpt != this.InitializerExpressionOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundNoPiaObjectCreationExpression boundNoPiaObjectCreationExpression = new BoundNoPiaObjectCreationExpression(this.Syntax, guidString, initializerExpressionOpt, type, base.HasErrors);
				boundNoPiaObjectCreationExpression.CopyAttributes(this);
				return boundNoPiaObjectCreationExpression;
			}
			return this;
		}
	}
}
