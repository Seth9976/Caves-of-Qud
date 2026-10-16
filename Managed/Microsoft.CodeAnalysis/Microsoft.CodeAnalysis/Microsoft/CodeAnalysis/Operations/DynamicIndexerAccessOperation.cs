using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046F RID: 1135
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DynamicIndexerAccessOperation : HasDynamicArgumentsExpression, IDynamicIndexerAccessOperation, IOperation
	{
		// Token: 0x06002C6C RID: 11372 RVA: 0x0007D775 File Offset: 0x0007B975
		public DynamicIndexerAccessOperation(IOperation operation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> arguments, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(arguments, argumentNames, argumentRefKinds, semanticModel, syntax, type, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x0007D796 File Offset: 0x0007B996
		public IOperation Operation { get; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x0007D79E File Offset: 0x0007B99E
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x0007D7A1 File Offset: 0x0007B9A1
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DynamicIndexerAccess;
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
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

		// Token: 0x06002C71 RID: 11377 RVA: 0x0007D808 File Offset: 0x0007BA08
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

		// Token: 0x06002C72 RID: 11378 RVA: 0x0007D893 File Offset: 0x0007BA93
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDynamicIndexerAccess(this);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0007D89C File Offset: 0x0007BA9C
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDynamicIndexerAccess(this, argument);
		}
	}
}
