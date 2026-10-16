using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F1 RID: 1009
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class StopOperation : Operation, IStopOperation, IOperation
	{
		// Token: 0x060026B7 RID: 9911 RVA: 0x000748E4 File Offset: 0x00072AE4
		[NullableContext(1)]
		internal StopOperation([Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000748EF File Offset: 0x00072AEF
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00074902 File Offset: 0x00072B02
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00074914 File Offset: 0x00072B14
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00074917 File Offset: 0x00072B17
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0007491A File Offset: 0x00072B1A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Stop;
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0007491E File Offset: 0x00072B1E
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitStop(this);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00074927 File Offset: 0x00072B27
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitStop(this, argument);
		}
	}
}
