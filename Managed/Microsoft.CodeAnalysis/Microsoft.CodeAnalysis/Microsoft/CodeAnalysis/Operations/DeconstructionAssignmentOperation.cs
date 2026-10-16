using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000421 RID: 1057
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DeconstructionAssignmentOperation : BaseAssignmentOperation, IDeconstructionAssignmentOperation, IAssignmentOperation, IOperation
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x000772B5 File Offset: 0x000754B5
		[NullableContext(1)]
		internal DeconstructionAssignmentOperation(IOperation target, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(target, value, semanticModel, syntax, isImplicit)
		{
			this.Type = type;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000772CC File Offset: 0x000754CC
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (base.Value != null)
					{
						return base.Value;
					}
				}
			}
			else if (base.Target != null)
			{
				return base.Target;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0007731C File Offset: 0x0007551C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (base.Target != null)
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
			if (base.Value != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0007737F File Offset: 0x0007557F
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x00077387 File Offset: 0x00075587
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0007738A File Offset: 0x0007558A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DeconstructionAssignment;
			}
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0007738E File Offset: 0x0007558E
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDeconstructionAssignment(this);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x00077397 File Offset: 0x00075597
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDeconstructionAssignment(this, argument);
		}
	}
}
