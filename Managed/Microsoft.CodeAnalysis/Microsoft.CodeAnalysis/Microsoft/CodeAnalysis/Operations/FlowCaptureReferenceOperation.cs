using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000441 RID: 1089
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class FlowCaptureReferenceOperation : Operation, IFlowCaptureReferenceOperation, IOperation
	{
		// Token: 0x060029B1 RID: 10673 RVA: 0x00078BB2 File Offset: 0x00076DB2
		internal FlowCaptureReferenceOperation(CaptureId id, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Id = id;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x00078BD5 File Offset: 0x00076DD5
		public CaptureId Id { get; }

		// Token: 0x060029B3 RID: 10675 RVA: 0x00078BDD File Offset: 0x00076DDD
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00078BF0 File Offset: 0x00076DF0
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x00078C02 File Offset: 0x00076E02
		public override ITypeSymbol Type { get; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x00078C0A File Offset: 0x00076E0A
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x00078C12 File Offset: 0x00076E12
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.FlowCaptureReference;
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00078C16 File Offset: 0x00076E16
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFlowCaptureReference(this);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00078C1F File Offset: 0x00076E1F
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFlowCaptureReference(this, argument);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00078C29 File Offset: 0x00076E29
		public FlowCaptureReferenceOperation(int id, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue)
			: this(new CaptureId(id), null, syntax, type, constantValue, true)
		{
		}
	}
}
