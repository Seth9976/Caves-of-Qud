using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000405 RID: 1029
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class TypeParameterObjectCreationOperation : Operation, ITypeParameterObjectCreationOperation, IOperation
	{
		// Token: 0x06002788 RID: 10120 RVA: 0x00075B56 File Offset: 0x00073D56
		internal TypeParameterObjectCreationOperation(IObjectOrCollectionInitializerOperation initializer, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Initializer = Operation.SetParentOperation<IObjectOrCollectionInitializerOperation>(initializer, this);
			this.Type = type;
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x00075B77 File Offset: 0x00073D77
		public IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x0600278A RID: 10122 RVA: 0x00075B80 File Offset: 0x00073D80
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Initializer != null)
			{
				return this.Initializer;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00075BB4 File Offset: 0x00073DB4
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
			else if (this.Initializer != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x00075BEB File Offset: 0x00073DEB
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00075BF3 File Offset: 0x00073DF3
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x00075BF6 File Offset: 0x00073DF6
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.TypeParameterObjectCreation;
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00075BFA File Offset: 0x00073DFA
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTypeParameterObjectCreation(this);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00075C03 File Offset: 0x00073E03
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTypeParameterObjectCreation(this, argument);
		}
	}
}
