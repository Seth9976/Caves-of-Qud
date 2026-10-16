using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000447 RID: 1095
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ReDimOperation : Operation, IReDimOperation, IOperation
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x00079011 File Offset: 0x00077211
		[NullableContext(1)]
		internal ReDimOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IReDimClauseOperation> clauses, bool preserve, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Clauses = Operation.SetParentOperation<IReDimClauseOperation>(clauses, this);
			this.Preserve = preserve;
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x00079032 File Offset: 0x00077232
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IReDimClauseOperation> Clauses
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x0007903A File Offset: 0x0007723A
		public bool Preserve { get; }

		// Token: 0x060029EF RID: 10735 RVA: 0x00079044 File Offset: 0x00077244
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Clauses.Length)
			{
				return this.Clauses[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0007908C File Offset: 0x0007728C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Clauses.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Clauses.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00079102 File Offset: 0x00077302
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x00079105 File Offset: 0x00077305
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x00079108 File Offset: 0x00077308
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ReDim;
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0007910C File Offset: 0x0007730C
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitReDim(this);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00079115 File Offset: 0x00077315
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitReDim(this, argument);
		}
	}
}
