using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D5 RID: 213
	internal sealed class BoundArgListOperator : BoundExpression
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0005E0E5 File Offset: 0x0005C2E5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.Arguments);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0005E0F2 File Offset: 0x0005C2F2
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return "__arglist";
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0005E0F9 File Offset: 0x0005C2F9
		public BoundArgListOperator([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ArgListOperator, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>())
		{
			this.Arguments = arguments;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0005E121 File Offset: 0x0005C321
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0005E129 File Offset: 0x0005C329
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0005E131 File Offset: 0x0005C331
		public ImmutableArray<RefKind> ArgumentRefKindsOpt { get; }

		// Token: 0x06001155 RID: 4437 RVA: 0x0005E139 File Offset: 0x0005C339
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArgListOperator(this);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0005E144 File Offset: 0x0005C344
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundArgListOperator Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (arguments != this.Arguments || argumentRefKindsOpt != this.ArgumentRefKindsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundArgListOperator boundArgListOperator = new BoundArgListOperator(this.Syntax, arguments, argumentRefKindsOpt, type, base.HasErrors);
				boundArgListOperator.CopyAttributes(this);
				return boundArgListOperator;
			}
			return this;
		}
	}
}
