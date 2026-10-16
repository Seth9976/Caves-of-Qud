using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200042A RID: 1066
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class VariableDeclaratorOperation : Operation, IVariableDeclaratorOperation, IOperation
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x00077873 File Offset: 0x00075A73
		[NullableContext(1)]
		internal VariableDeclaratorOperation(ILocalSymbol symbol, [Nullable(2)] IVariableInitializerOperation initializer, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> ignoredArguments, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Symbol = symbol;
			this.Initializer = Operation.SetParentOperation<IVariableInitializerOperation>(initializer, this);
			this.IgnoredArguments = Operation.SetParentOperation<IOperation>(ignoredArguments, this);
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000778A2 File Offset: 0x00075AA2
		[Nullable(1)]
		public ILocalSymbol Symbol
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x000778AA File Offset: 0x00075AAA
		public IVariableInitializerOperation Initializer { get; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000778B2 File Offset: 0x00075AB2
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> IgnoredArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000778BC File Offset: 0x00075ABC
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
			else if (index < this.IgnoredArguments.Length)
			{
				return this.IgnoredArguments[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0007791C File Offset: 0x00075B1C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.IgnoredArguments.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.IgnoredArguments.Length)
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

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000779A7 File Offset: 0x00075BA7
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000779AA File Offset: 0x00075BAA
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000779AD File Offset: 0x00075BAD
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.VariableDeclarator;
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000779B1 File Offset: 0x00075BB1
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitVariableDeclarator(this);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000779BA File Offset: 0x00075BBA
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitVariableDeclarator(this, argument);
		}
	}
}
