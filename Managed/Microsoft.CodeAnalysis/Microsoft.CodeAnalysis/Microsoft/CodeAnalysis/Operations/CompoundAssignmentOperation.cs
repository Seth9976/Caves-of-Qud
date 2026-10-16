using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040C RID: 1036
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompoundAssignmentOperation : BaseAssignmentOperation, ICompoundAssignmentOperation, IAssignmentOperation, IOperation
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x00076094 File Offset: 0x00074294
		internal CompoundAssignmentOperation(IConvertibleConversion inConversion, IConvertibleConversion outConversion, BinaryOperatorKind operatorKind, bool isLifted, bool isChecked, [Nullable(2)] IMethodSymbol operatorMethod, IOperation target, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(target, value, semanticModel, syntax, isImplicit)
		{
			this.InConversionConvertible = inConversion;
			this.OutConversionConvertible = outConversion;
			this.OperatorKind = operatorKind;
			this.IsLifted = isLifted;
			this.IsChecked = isChecked;
			this.OperatorMethod = operatorMethod;
			this.Type = type;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000760E6 File Offset: 0x000742E6
		internal IConvertibleConversion InConversionConvertible { get; }

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000760EE File Offset: 0x000742EE
		public CommonConversion InConversion
		{
			get
			{
				return this.InConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000760FB File Offset: 0x000742FB
		internal IConvertibleConversion OutConversionConvertible { get; }

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x00076103 File Offset: 0x00074303
		public CommonConversion OutConversion
		{
			get
			{
				return this.OutConversionConvertible.ToCommonConversion();
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x00076110 File Offset: 0x00074310
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x00076118 File Offset: 0x00074318
		public bool IsLifted { get; }

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00076120 File Offset: 0x00074320
		public bool IsChecked { get; }

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x00076128 File Offset: 0x00074328
		[Nullable(2)]
		public IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00076130 File Offset: 0x00074330
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

		// Token: 0x060027CE RID: 10190 RVA: 0x00076180 File Offset: 0x00074380
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

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x000761E3 File Offset: 0x000743E3
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x000761EB File Offset: 0x000743EB
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000761EE File Offset: 0x000743EE
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CompoundAssignment;
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000761F2 File Offset: 0x000743F2
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitCompoundAssignment(this);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000761FB File Offset: 0x000743FB
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitCompoundAssignment(this, argument);
		}
	}
}
