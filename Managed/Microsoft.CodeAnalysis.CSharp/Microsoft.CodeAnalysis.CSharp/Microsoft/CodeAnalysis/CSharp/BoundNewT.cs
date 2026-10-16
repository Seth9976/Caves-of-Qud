using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CB RID: 459
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNewT : BoundExpression
	{
		// Token: 0x06001FD6 RID: 8150 RVA: 0x000A3239 File Offset: 0x000A1439
		public BoundNewT(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.NewT, syntax, type, hasErrors || initializerExpressionOpt.HasErrors())
		{
			this.InitializerExpressionOpt = initializerExpressionOpt;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000A325C File Offset: 0x000A145C
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x000A3264 File Offset: 0x000A1464
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundObjectInitializerExpressionBase InitializerExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000A326C File Offset: 0x000A146C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNewT(this);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000A3275 File Offset: 0x000A1475
		public BoundNewT Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type)
		{
			if (initializerExpressionOpt != this.InitializerExpressionOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundNewT boundNewT = new BoundNewT(this.Syntax, initializerExpressionOpt, type, base.HasErrors);
				boundNewT.CopyAttributes(this);
				return boundNewT;
			}
			return this;
		}
	}
}
