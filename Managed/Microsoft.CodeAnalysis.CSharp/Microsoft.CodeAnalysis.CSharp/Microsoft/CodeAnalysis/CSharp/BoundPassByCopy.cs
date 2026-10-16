using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008B RID: 139
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPassByCopy : BoundExpression
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00052945 File Offset: 0x00050B45
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00052948 File Offset: 0x00050B48
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Expression.ExpressionSymbol;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00052955 File Offset: 0x00050B55
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Expression);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00052962 File Offset: 0x00050B62
		public override object Display
		{
			get
			{
				return this.Expression.Display;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0005296F File Offset: 0x00050B6F
		public BoundPassByCopy(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.PassByCopy, syntax, type, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0005298F File Offset: 0x00050B8F
		public BoundExpression Expression { get; }

		// Token: 0x06000B6B RID: 2923 RVA: 0x00052997 File Offset: 0x00050B97
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPassByCopy(this);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000529A0 File Offset: 0x00050BA0
		public BoundPassByCopy Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != this.Expression || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPassByCopy boundPassByCopy = new BoundPassByCopy(this.Syntax, expression, type, base.HasErrors);
				boundPassByCopy.CopyAttributes(this);
				return boundPassByCopy;
			}
			return this;
		}
	}
}
