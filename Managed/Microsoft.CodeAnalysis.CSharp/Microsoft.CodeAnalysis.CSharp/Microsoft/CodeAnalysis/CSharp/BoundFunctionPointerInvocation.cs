using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B1 RID: 177
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFunctionPointerInvocation : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00056370 File Offset: 0x00054570
		public FunctionPointerTypeSymbol FunctionPointer
		{
			get
			{
				return (FunctionPointerTypeSymbol)this.InvokedExpression.Type;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00056382 File Offset: 0x00054582
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return CSharpOperationFactory.CreateInvalidChildrenFromArgumentsExpression(this.InvokedExpression, this.Arguments, null);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00056396 File Offset: 0x00054596
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundNode>(((IBoundInvalidNode)this).InvalidNodeChildren);
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000563A3 File Offset: 0x000545A3
		public BoundFunctionPointerInvocation(SyntaxNode syntax, BoundExpression invokedExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.FunctionPointerInvocation, syntax, type, hasErrors || invokedExpression.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.InvokedExpression = invokedExpression;
			this.Arguments = arguments;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this._ResultKind = resultKind;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000563E3 File Offset: 0x000545E3
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x000563EB File Offset: 0x000545EB
		public BoundExpression InvokedExpression { get; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000563F3 File Offset: 0x000545F3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x000563FB File Offset: 0x000545FB
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00056403 File Offset: 0x00054603
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0005640B File Offset: 0x0005460B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFunctionPointerInvocation(this);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00056414 File Offset: 0x00054614
		public BoundFunctionPointerInvocation Update(BoundExpression invokedExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, LookupResultKind resultKind, TypeSymbol type)
		{
			if (invokedExpression != this.InvokedExpression || arguments != this.Arguments || argumentRefKindsOpt != this.ArgumentRefKindsOpt || resultKind != this.ResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFunctionPointerInvocation boundFunctionPointerInvocation = new BoundFunctionPointerInvocation(this.Syntax, invokedExpression, arguments, argumentRefKindsOpt, resultKind, type, base.HasErrors);
				boundFunctionPointerInvocation.CopyAttributes(this);
				return boundFunctionPointerInvocation;
			}
			return this;
		}

		// Token: 0x0400053D RID: 1341
		private readonly LookupResultKind _ResultKind;
	}
}
