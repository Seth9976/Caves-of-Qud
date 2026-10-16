using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000440 RID: 1088
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FlowCaptureOperation : Operation, IFlowCaptureOperation, IOperation
	{
		// Token: 0x060029A6 RID: 10662 RVA: 0x00078AE7 File Offset: 0x00076CE7
		internal FlowCaptureOperation(CaptureId id, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Id = id;
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x00078B08 File Offset: 0x00076D08
		public CaptureId Id { get; }

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x00078B10 File Offset: 0x00076D10
		public IOperation Value { get; }

		// Token: 0x060029A9 RID: 10665 RVA: 0x00078B18 File Offset: 0x00076D18
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00078B4C File Offset: 0x00076D4C
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
			else if (this.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x00078B83 File Offset: 0x00076D83
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00078B86 File Offset: 0x00076D86
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x00078B89 File Offset: 0x00076D89
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.FlowCapture;
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00078B8D File Offset: 0x00076D8D
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFlowCapture(this);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00078B96 File Offset: 0x00076D96
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFlowCapture(this, argument);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x00078BA0 File Offset: 0x00076DA0
		public FlowCaptureOperation(int id, SyntaxNode syntax, IOperation value)
			: this(new CaptureId(id), value, null, syntax, true)
		{
		}
	}
}
