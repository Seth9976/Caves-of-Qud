using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DB RID: 219
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicInvocation : BoundDynamicInvocableBase
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0005E538 File Offset: 0x0005C738
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(base.Arguments.Insert(0, base.Expression));
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0005E560 File Offset: 0x0005C760
		public BoundDynamicInvocation(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DynamicInvocation, syntax, expression, arguments, type, hasErrors || expression.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.ApplicableMethods = applicableMethods;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0005E5AC File Offset: 0x0005C7AC
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0005E5B4 File Offset: 0x0005C7B4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0005E5BC File Offset: 0x0005C7BC
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0005E5C4 File Offset: 0x0005C7C4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> ApplicableMethods
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0005E5CC File Offset: 0x0005C7CC
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicInvocation(this);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		public BoundDynamicInvocation Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, TypeSymbol type)
		{
			if (argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || applicableMethods != this.ApplicableMethods || expression != base.Expression || arguments != base.Arguments || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicInvocation boundDynamicInvocation = new BoundDynamicInvocation(this.Syntax, argumentNamesOpt, argumentRefKindsOpt, applicableMethods, expression, arguments, type, base.HasErrors);
				boundDynamicInvocation.CopyAttributes(this);
				return boundDynamicInvocation;
			}
			return this;
		}
	}
}
