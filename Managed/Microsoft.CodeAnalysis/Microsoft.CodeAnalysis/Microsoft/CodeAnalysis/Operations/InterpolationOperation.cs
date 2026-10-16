using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000437 RID: 1079
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class InterpolationOperation : BaseInterpolatedStringContentOperation, IInterpolationOperation, IInterpolatedStringContentOperation, IOperation
	{
		// Token: 0x0600295B RID: 10587 RVA: 0x0007845C File Offset: 0x0007665C
		internal InterpolationOperation([Nullable(1)] IOperation expression, IOperation alignment, IOperation formatString, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Expression = Operation.SetParentOperation<IOperation>(expression, this);
			this.Alignment = Operation.SetParentOperation<IOperation>(alignment, this);
			this.FormatString = Operation.SetParentOperation<IOperation>(formatString, this);
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x00078491 File Offset: 0x00076691
		[Nullable(1)]
		public IOperation Expression
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x00078499 File Offset: 0x00076699
		public IOperation Alignment { get; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000784A1 File Offset: 0x000766A1
		public IOperation FormatString { get; }

		// Token: 0x0600295F RID: 10591 RVA: 0x000784AC File Offset: 0x000766AC
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Expression != null)
				{
					return this.Expression;
				}
				break;
			case 1:
				if (this.Alignment != null)
				{
					return this.Alignment;
				}
				break;
			case 2:
				if (this.FormatString != null)
				{
					return this.FormatString;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00078518 File Offset: 0x00076718
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Expression != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				goto IL_0040;
			case 2:
			case 3:
				goto IL_0051;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Alignment != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0040:
			if (this.FormatString != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			IL_0051:
			return new ValueTuple<bool, int, int>(false, 3, 0);
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x00078590 File Offset: 0x00076790
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x00078593 File Offset: 0x00076793
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x00078596 File Offset: 0x00076796
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Interpolation;
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0007859A File Offset: 0x0007679A
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInterpolation(this);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000785A3 File Offset: 0x000767A3
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInterpolation(this, argument);
		}
	}
}
