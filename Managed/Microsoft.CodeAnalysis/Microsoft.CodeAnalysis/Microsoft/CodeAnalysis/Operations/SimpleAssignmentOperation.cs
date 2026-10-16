using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040B RID: 1035
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SimpleAssignmentOperation : BaseAssignmentOperation, ISimpleAssignmentOperation, IAssignmentOperation, IOperation
	{
		// Token: 0x060027BB RID: 10171 RVA: 0x00075F88 File Offset: 0x00074188
		[NullableContext(1)]
		internal SimpleAssignmentOperation(bool isRef, IOperation target, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, [Nullable(2)] ConstantValue constantValue, bool isImplicit)
			: base(target, value, semanticModel, syntax, isImplicit)
		{
			this.IsRef = isRef;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x00075FAF File Offset: 0x000741AF
		public bool IsRef { get; }

		// Token: 0x060027BD RID: 10173 RVA: 0x00075FB8 File Offset: 0x000741B8
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

		// Token: 0x060027BE RID: 10174 RVA: 0x00076008 File Offset: 0x00074208
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

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x0007606B File Offset: 0x0007426B
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x00076073 File Offset: 0x00074273
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0007607B File Offset: 0x0007427B
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.SimpleAssignment;
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0007607F File Offset: 0x0007427F
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSimpleAssignment(this);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00076088 File Offset: 0x00074288
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSimpleAssignment(this, argument);
		}
	}
}
