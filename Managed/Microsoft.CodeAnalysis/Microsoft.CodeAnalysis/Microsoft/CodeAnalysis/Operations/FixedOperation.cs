using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044F RID: 1103
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FixedOperation : Operation, IFixedOperation, IOperation
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x00079934 File Offset: 0x00077B34
		internal FixedOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IVariableDeclarationGroupOperation variables, IOperation body, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Locals = locals;
			this.Variables = Operation.SetParentOperation<IVariableDeclarationGroupOperation>(variables, this);
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x00079963 File Offset: 0x00077B63
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x0007996B File Offset: 0x00077B6B
		public IVariableDeclarationGroupOperation Variables { get; }

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x00079973 File Offset: 0x00077B73
		public IOperation Body { get; }

		// Token: 0x06002A43 RID: 10819 RVA: 0x0007997C File Offset: 0x00077B7C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Body != null)
					{
						return this.Body;
					}
				}
			}
			else if (this.Variables != null)
			{
				return this.Variables;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000799CC File Offset: 0x00077BCC
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Variables != null)
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
			if (this.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x00079A2F File Offset: 0x00077C2F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x00079A32 File Offset: 0x00077C32
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x00079A35 File Offset: 0x00077C35
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00079A38 File Offset: 0x00077C38
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFixed(this);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00079A41 File Offset: 0x00077C41
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFixed(this, argument);
		}
	}
}
