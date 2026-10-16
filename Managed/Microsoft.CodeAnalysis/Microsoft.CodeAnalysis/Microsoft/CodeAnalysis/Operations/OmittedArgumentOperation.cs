using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000423 RID: 1059
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class OmittedArgumentOperation : Operation, IOmittedArgumentOperation, IOperation
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x00077459 File Offset: 0x00075659
		internal OmittedArgumentOperation(SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Type = type;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0007746C File Offset: 0x0007566C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0007747F File Offset: 0x0007567F
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x00077491 File Offset: 0x00075691
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x00077499 File Offset: 0x00075699
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0007749C File Offset: 0x0007569C
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.OmittedArgument;
			}
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000774A0 File Offset: 0x000756A0
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitOmittedArgument(this);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000774A9 File Offset: 0x000756A9
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitOmittedArgument(this, argument);
		}
	}
}
