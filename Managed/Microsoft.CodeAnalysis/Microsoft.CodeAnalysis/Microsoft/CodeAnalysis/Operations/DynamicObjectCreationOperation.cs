using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046D RID: 1133
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DynamicObjectCreationOperation : HasDynamicArgumentsExpression, IDynamicObjectCreationOperation, IOperation
	{
		// Token: 0x06002C5C RID: 11356 RVA: 0x0007D517 File Offset: 0x0007B717
		public DynamicObjectCreationOperation(IObjectOrCollectionInitializerOperation initializer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> arguments, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(arguments, argumentNames, argumentRefKinds, semanticModel, syntax, type, isImplicit)
		{
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x0007D538 File Offset: 0x0007B738
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x0007D540 File Offset: 0x0007B740
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x0007D543 File Offset: 0x0007B743
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DynamicObjectCreation;
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0007D548 File Offset: 0x0007B748
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Initializer != null)
					{
						return this.Initializer;
					}
				}
			}
			else if (index < base.Arguments.Length)
			{
				return base.Arguments[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!base.Arguments.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < base.Arguments.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
			case 2:
				IL_0064:
				return new ValueTuple<bool, int, int>(false, 2, 0);
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			goto IL_0064;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0007D633 File Offset: 0x0007B833
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDynamicObjectCreation(this);
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0007D63C File Offset: 0x0007B83C
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDynamicObjectCreation(this, argument);
		}
	}
}
