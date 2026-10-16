using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FE RID: 1022
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EventReferenceOperation : BaseMemberReferenceOperation, IEventReferenceOperation, IMemberReferenceOperation, IOperation
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x00075311 File Offset: 0x00073511
		[NullableContext(2)]
		internal EventReferenceOperation([Nullable(1)] IEventSymbol @event, IOperation instance, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(instance, semanticModel, syntax, isImplicit)
		{
			this.Event = @event;
			this.Type = type;
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0007532E File Offset: 0x0007352E
		public IEventSymbol Event { get; }

		// Token: 0x06002736 RID: 10038 RVA: 0x00075338 File Offset: 0x00073538
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Instance != null)
			{
				return base.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0007536C File Offset: 0x0007356C
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
			else if (base.Instance != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000753A3 File Offset: 0x000735A3
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000753AB File Offset: 0x000735AB
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x000753AE File Offset: 0x000735AE
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.EventReference;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000753B2 File Offset: 0x000735B2
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitEventReference(this);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000753BB File Offset: 0x000735BB
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitEventReference(this, argument);
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000753C5 File Offset: 0x000735C5
		public override ISymbol Member
		{
			get
			{
				return this.Event;
			}
		}
	}
}
