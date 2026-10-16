using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000197 RID: 407
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSwitchDispatch : BoundStatement
	{
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000A128B File Offset: 0x0009F48B
		public BoundSwitchDispatch(SyntaxNode syntax, BoundExpression expression, [TupleElementNames(new string[] { "value", "label" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> cases, LabelSymbol defaultLabel, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol equalityMethod, bool hasErrors = false)
			: base(BoundKind.SwitchDispatch, syntax, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
			this.Cases = cases;
			this.DefaultLabel = defaultLabel;
			this.EqualityMethod = equalityMethod;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000A12C1 File Offset: 0x0009F4C1
		public BoundExpression Expression { get; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000A12C9 File Offset: 0x0009F4C9
		[TupleElementNames(new string[] { "value", "label" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })]
		public ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> Cases
		{
			[return: TupleElementNames(new string[] { "value", "label" })]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })]
			get;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x000A12D1 File Offset: 0x0009F4D1
		public LabelSymbol DefaultLabel { get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000A12D9 File Offset: 0x0009F4D9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol EqualityMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000A12E1 File Offset: 0x0009F4E1
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSwitchDispatch(this);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000A12EC File Offset: 0x0009F4EC
		public BoundSwitchDispatch Update(BoundExpression expression, [TupleElementNames(new string[] { "value", "label" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> cases, LabelSymbol defaultLabel, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol equalityMethod)
		{
			if (expression != this.Expression || cases != this.Cases || !SymbolEqualityComparer.ConsiderEverything.Equals(defaultLabel, this.DefaultLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(equalityMethod, this.EqualityMethod))
			{
				BoundSwitchDispatch boundSwitchDispatch = new BoundSwitchDispatch(this.Syntax, expression, cases, defaultLabel, equalityMethod, base.HasErrors);
				boundSwitchDispatch.CopyAttributes(this);
				return boundSwitchDispatch;
			}
			return this;
		}
	}
}
