using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046E RID: 1134
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DynamicInvocationOperation : HasDynamicArgumentsExpression, IDynamicInvocationOperation, IOperation
	{
		// Token: 0x06002C64 RID: 11364 RVA: 0x0007D646 File Offset: 0x0007B846
		public DynamicInvocationOperation(IOperation operation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> arguments, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(arguments, argumentNames, argumentRefKinds, semanticModel, syntax, type, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0007D668 File Offset: 0x0007B868
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < base.Arguments.Length)
					{
						return base.Arguments[index];
					}
				}
			}
			else if (this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0007D6C8 File Offset: 0x0007B8C8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Operation != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < base.Arguments.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!base.Arguments.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x0007D753 File Offset: 0x0007B953
		public IOperation Operation { get; }

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x0007D75B File Offset: 0x0007B95B
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x0007D75E File Offset: 0x0007B95E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DynamicInvocation;
			}
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0007D762 File Offset: 0x0007B962
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDynamicInvocation(this);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0007D76B File Offset: 0x0007B96B
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDynamicInvocation(this, argument);
		}
	}
}
