using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000404 RID: 1028
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ObjectCreationOperation : Operation, IObjectCreationOperation, IOperation
	{
		// Token: 0x0600277D RID: 10109 RVA: 0x000759EC File Offset: 0x00073BEC
		internal ObjectCreationOperation(IMethodSymbol constructor, IObjectOrCollectionInitializerOperation initializer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Constructor = constructor;
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
			this.Arguments = Operation.SetParentOperation<IArgumentOperation>(arguments, this);
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x00075A2B File Offset: 0x00073C2B
		public IMethodSymbol Constructor { get; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00075A33 File Offset: 0x00073C33
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x00075A3B File Offset: 0x00073C3B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00075A44 File Offset: 0x00073C44
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
			else if (index < this.Arguments.Length)
			{
				return this.Arguments[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00075AA4 File Offset: 0x00073CA4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Arguments.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Arguments.Length)
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

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00075B2F File Offset: 0x00073D2F
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x00075B37 File Offset: 0x00073D37
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x00075B3F File Offset: 0x00073D3F
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ObjectCreation;
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00075B43 File Offset: 0x00073D43
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitObjectCreation(this);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00075B4C File Offset: 0x00073D4C
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitObjectCreation(this, argument);
		}
	}
}
