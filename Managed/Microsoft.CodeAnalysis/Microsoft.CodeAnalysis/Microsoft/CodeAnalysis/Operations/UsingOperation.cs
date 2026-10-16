using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003EE RID: 1006
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class UsingOperation : Operation, IUsingOperation, IOperation
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x000745E9 File Offset: 0x000727E9
		internal UsingOperation(IOperation resources, IOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, bool isAsynchronous, DisposeOperationInfo disposeInfo, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Resources = Operation.SetParentOperation<IOperation>(resources, this);
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
			this.Locals = locals;
			this.IsAsynchronous = isAsynchronous;
			this.DisposeInfo = disposeInfo;
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00074628 File Offset: 0x00072828
		public IOperation Resources { get; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x00074630 File Offset: 0x00072830
		public IOperation Body { get; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x00074638 File Offset: 0x00072838
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00074640 File Offset: 0x00072840
		public bool IsAsynchronous { get; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x00074648 File Offset: 0x00072848
		public DisposeOperationInfo DisposeInfo { get; }

		// Token: 0x0600269C RID: 9884 RVA: 0x00074650 File Offset: 0x00072850
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Body != null)
					{
						return this.Body;
					}
				}
			}
			else if (this.Resources != null)
			{
				return this.Resources;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000746A0 File Offset: 0x000728A0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Resources != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00074703 File Offset: 0x00072903
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x00074706 File Offset: 0x00072906
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x00074709 File Offset: 0x00072909
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Using;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0007470D File Offset: 0x0007290D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitUsing(this);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00074716 File Offset: 0x00072916
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitUsing(this, argument);
		}
	}
}
