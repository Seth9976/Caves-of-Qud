using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046B RID: 1131
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FlowAnonymousFunctionOperation : Operation, IFlowAnonymousFunctionOperation, IOperation
	{
		// Token: 0x06002C4E RID: 11342 RVA: 0x0007D454 File Offset: 0x0007B654
		public FlowAnonymousFunctionOperation(in ControlFlowGraphBuilder.Context context, IAnonymousFunctionOperation original, bool isImplicit)
			: base(null, original.Syntax, isImplicit)
		{
			this.Context = context;
			this.Original = original;
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x0007D477 File Offset: 0x0007B677
		public IMethodSymbol Symbol
		{
			get
			{
				return this.Original.Symbol;
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0007D484 File Offset: 0x0007B684
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0007D497 File Offset: 0x0007B697
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x0007D4A9 File Offset: 0x0007B6A9
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.FlowAnonymousFunction;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x0007D4AD File Offset: 0x0007B6AD
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x0007D4B0 File Offset: 0x0007B6B0
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0007D4B3 File Offset: 0x0007B6B3
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFlowAnonymousFunction(this);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0007D4BC File Offset: 0x0007B6BC
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFlowAnonymousFunction(this, argument);
		}

		// Token: 0x04001194 RID: 4500
		public readonly ControlFlowGraphBuilder.Context Context;

		// Token: 0x04001195 RID: 4501
		public readonly IAnonymousFunctionOperation Original;
	}
}
