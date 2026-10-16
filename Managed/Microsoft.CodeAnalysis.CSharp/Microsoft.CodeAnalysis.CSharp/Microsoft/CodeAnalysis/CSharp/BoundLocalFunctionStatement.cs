using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B3 RID: 179
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLocalFunctionStatement : BoundStatement, IBoundLambdaOrFunction
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00056481 File Offset: 0x00054681
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundBlock Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.BlockBody ?? this.ExpressionBody;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00056493 File Offset: 0x00054693
		MethodSymbol IBoundLambdaOrFunction.Symbol
		{
			get
			{
				return this.Symbol;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0005649B File Offset: 0x0005469B
		SyntaxNode IBoundLambdaOrFunction.Syntax
		{
			get
			{
				return this.Syntax;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000564A3 File Offset: 0x000546A3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		BoundBlock IBoundLambdaOrFunction.Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Body;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000564AB File Offset: 0x000546AB
		public BoundLocalFunctionStatement(SyntaxNode syntax, LocalFunctionSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock blockBody, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock expressionBody, bool hasErrors = false)
			: base(BoundKind.LocalFunctionStatement, syntax, hasErrors || blockBody.HasErrors() || expressionBody.HasErrors())
		{
			this.Symbol = symbol;
			this.BlockBody = blockBody;
			this.ExpressionBody = expressionBody;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000564E2 File Offset: 0x000546E2
		public LocalFunctionSymbol Symbol { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x000564EA File Offset: 0x000546EA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundBlock BlockBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x000564F2 File Offset: 0x000546F2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundBlock ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000564FA File Offset: 0x000546FA
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLocalFunctionStatement(this);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00056504 File Offset: 0x00054704
		public BoundLocalFunctionStatement Update(LocalFunctionSymbol symbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock blockBody, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock expressionBody)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(symbol, this.Symbol) || blockBody != this.BlockBody || expressionBody != this.ExpressionBody)
			{
				BoundLocalFunctionStatement boundLocalFunctionStatement = new BoundLocalFunctionStatement(this.Syntax, symbol, blockBody, expressionBody, base.HasErrors);
				boundLocalFunctionStatement.CopyAttributes(this);
				return boundLocalFunctionStatement;
			}
			return this;
		}
	}
}
