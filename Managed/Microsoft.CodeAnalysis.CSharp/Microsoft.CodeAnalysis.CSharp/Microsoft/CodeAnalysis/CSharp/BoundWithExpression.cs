using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E2 RID: 482
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundWithExpression : BoundExpression
	{
		// Token: 0x06002050 RID: 8272 RVA: 0x000A410E File Offset: 0x000A230E
		public BoundWithExpression(SyntaxNode syntax, BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol cloneMethod, BoundObjectInitializerExpressionBase initializerExpression, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.WithExpression, syntax, type, hasErrors || receiver.HasErrors() || initializerExpression.HasErrors())
		{
			this.Receiver = receiver;
			this.CloneMethod = cloneMethod;
			this.InitializerExpression = initializerExpression;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x000A414A File Offset: 0x000A234A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x000A4152 File Offset: 0x000A2352
		public BoundExpression Receiver { get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000A415A File Offset: 0x000A235A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol CloneMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x000A4162 File Offset: 0x000A2362
		public BoundObjectInitializerExpressionBase InitializerExpression { get; }

		// Token: 0x06002055 RID: 8277 RVA: 0x000A416A File Offset: 0x000A236A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitWithExpression(this);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000A4174 File Offset: 0x000A2374
		public BoundWithExpression Update(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol cloneMethod, BoundObjectInitializerExpressionBase initializerExpression, TypeSymbol type)
		{
			if (receiver != this.Receiver || !SymbolEqualityComparer.ConsiderEverything.Equals(cloneMethod, this.CloneMethod) || initializerExpression != this.InitializerExpression || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundWithExpression boundWithExpression = new BoundWithExpression(this.Syntax, receiver, cloneMethod, initializerExpression, type, base.HasErrors);
				boundWithExpression.CopyAttributes(this);
				return boundWithExpression;
			}
			return this;
		}
	}
}
