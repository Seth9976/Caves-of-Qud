using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040E RID: 1038
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class EventAssignmentOperation : Operation, IEventAssignmentOperation, IOperation
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x000762CA File Offset: 0x000744CA
		internal EventAssignmentOperation(IOperation eventReference, IOperation handlerValue, bool adds, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.EventReference = Operation.SetParentOperation<IOperation>(eventReference, this);
			this.HandlerValue = Operation.SetParentOperation<IOperation>(handlerValue, this);
			this.Adds = adds;
			this.Type = type;
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00076301 File Offset: 0x00074501
		public IOperation EventReference { get; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060027DF RID: 10207 RVA: 0x00076309 File Offset: 0x00074509
		public IOperation HandlerValue { get; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00076311 File Offset: 0x00074511
		public bool Adds { get; }

		// Token: 0x060027E1 RID: 10209 RVA: 0x0007631C File Offset: 0x0007451C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.HandlerValue != null)
					{
						return this.HandlerValue;
					}
				}
			}
			else if (this.EventReference != null)
			{
				return this.EventReference;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0007636C File Offset: 0x0007456C
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
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.HandlerValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000763CF File Offset: 0x000745CF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000763D7 File Offset: 0x000745D7
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000763DA File Offset: 0x000745DA
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.EventAssignment;
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000763DE File Offset: 0x000745DE
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitEventAssignment(this);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000763E7 File Offset: 0x000745E7
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitEventAssignment(this, argument);
		}
	}
}
