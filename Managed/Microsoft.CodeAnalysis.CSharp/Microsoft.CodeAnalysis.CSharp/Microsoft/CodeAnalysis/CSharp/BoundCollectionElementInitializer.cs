using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AA RID: 170
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundCollectionElementInitializer : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000558DC File Offset: 0x00053ADC
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.AddMethod;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000558E4 File Offset: 0x00053AE4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return CSharpOperationFactory.CreateInvalidChildrenFromArgumentsExpression(this.ImplicitReceiverOpt, this.Arguments, null);
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000558F8 File Offset: 0x00053AF8
		public BoundCollectionElementInitializer(SyntaxNode syntax, MethodSymbol addMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression implicitReceiverOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool invokedAsExtensionMethod, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.CollectionElementInitializer, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>() || implicitReceiverOpt.HasErrors())
		{
			this.AddMethod = addMethod;
			this.Arguments = arguments;
			this.ImplicitReceiverOpt = implicitReceiverOpt;
			this.Expanded = expanded;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.DefaultArguments = defaultArguments;
			this.InvokedAsExtensionMethod = invokedAsExtensionMethod;
			this._ResultKind = resultKind;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00055967 File Offset: 0x00053B67
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0005596F File Offset: 0x00053B6F
		public MethodSymbol AddMethod { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00055977 File Offset: 0x00053B77
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0005597F File Offset: 0x00053B7F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ImplicitReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00055987 File Offset: 0x00053B87
		public bool Expanded { get; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0005598F File Offset: 0x00053B8F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<int> ArgsToParamsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00055997 File Offset: 0x00053B97
		public BitVector DefaultArguments { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0005599F File Offset: 0x00053B9F
		public bool InvokedAsExtensionMethod { get; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000559A7 File Offset: 0x00053BA7
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000559AF File Offset: 0x00053BAF
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitCollectionElementInitializer(this);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000559B8 File Offset: 0x00053BB8
		public BoundCollectionElementInitializer Update(MethodSymbol addMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression implicitReceiverOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool invokedAsExtensionMethod, LookupResultKind resultKind, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(addMethod, this.AddMethod) || arguments != this.Arguments || implicitReceiverOpt != this.ImplicitReceiverOpt || expanded != this.Expanded || argsToParamsOpt != this.ArgsToParamsOpt || defaultArguments != this.DefaultArguments || invokedAsExtensionMethod != this.InvokedAsExtensionMethod || resultKind != this.ResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundCollectionElementInitializer boundCollectionElementInitializer = new BoundCollectionElementInitializer(this.Syntax, addMethod, arguments, implicitReceiverOpt, expanded, argsToParamsOpt, defaultArguments, invokedAsExtensionMethod, resultKind, type, base.HasErrors);
				boundCollectionElementInitializer.CopyAttributes(this);
				return boundCollectionElementInitializer;
			}
			return this;
		}

		// Token: 0x0400052D RID: 1325
		private readonly LookupResultKind _ResultKind;
	}
}
