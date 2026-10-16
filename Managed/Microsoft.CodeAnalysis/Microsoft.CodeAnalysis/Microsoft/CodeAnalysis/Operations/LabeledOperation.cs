using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E8 RID: 1000
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class LabeledOperation : Operation, ILabeledOperation, IOperation
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x00074111 File Offset: 0x00072311
		[NullableContext(1)]
		internal LabeledOperation(ILabelSymbol label, [Nullable(2)] IOperation operation, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Label = label;
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00074132 File Offset: 0x00072332
		[Nullable(1)]
		public ILabelSymbol Label
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x0007413A File Offset: 0x0007233A
		public IOperation Operation { get; }

		// Token: 0x0600265D RID: 9821 RVA: 0x00074144 File Offset: 0x00072344
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00074178 File Offset: 0x00072378
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.Operation != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000741AF File Offset: 0x000723AF
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000741B2 File Offset: 0x000723B2
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000741B5 File Offset: 0x000723B5
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Labeled;
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000741B8 File Offset: 0x000723B8
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitLabeled(this);
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000741C1 File Offset: 0x000723C1
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitLabeled(this, argument);
		}
	}
}
