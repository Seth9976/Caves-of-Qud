using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F3 RID: 1011
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RaiseEventOperation : Operation, IRaiseEventOperation, IOperation
	{
		// Token: 0x060026C7 RID: 9927 RVA: 0x0007497E File Offset: 0x00072B7E
		internal RaiseEventOperation(IEventReferenceOperation eventReference, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.EventReference = Operation.SetParentOperation<IEventReferenceOperation>(eventReference, this);
			this.Arguments = Operation.SetParentOperation<IArgumentOperation>(arguments, this);
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x000749A5 File Offset: 0x00072BA5
		public IEventReferenceOperation EventReference { get; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x000749AD File Offset: 0x00072BAD
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000749B8 File Offset: 0x00072BB8
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Arguments.Length)
					{
						return this.Arguments[index];
					}
				}
			}
			else if (this.EventReference != null)
			{
				return this.EventReference;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00074A18 File Offset: 0x00072C18
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.EventReference != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.Arguments.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Arguments.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x00074AA3 File Offset: 0x00072CA3
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x00074AA6 File Offset: 0x00072CA6
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x00074AA9 File Offset: 0x00072CA9
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.RaiseEvent;
			}
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00074AAD File Offset: 0x00072CAD
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitRaiseEvent(this);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x00074AB6 File Offset: 0x00072CB6
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitRaiseEvent(this, argument);
		}
	}
}
