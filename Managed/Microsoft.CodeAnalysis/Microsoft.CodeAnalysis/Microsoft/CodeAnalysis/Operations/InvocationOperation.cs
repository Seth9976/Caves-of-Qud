using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F6 RID: 1014
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class InvocationOperation : Operation, IInvocationOperation, IOperation
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x00074C53 File Offset: 0x00072E53
		internal InvocationOperation([Nullable(1)] IMethodSymbol targetMethod, IOperation instance, bool isVirtual, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.TargetMethod = targetMethod;
			this.Instance = Operation.SetParentOperation<IOperation>(instance, this);
			this.IsVirtual = isVirtual;
			this.Arguments = Operation.SetParentOperation<IArgumentOperation>(arguments, this);
			this.Type = type;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00074C92 File Offset: 0x00072E92
		[Nullable(1)]
		public IMethodSymbol TargetMethod
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x00074C9A File Offset: 0x00072E9A
		public IOperation Instance { get; }

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x00074CA2 File Offset: 0x00072EA2
		public bool IsVirtual { get; }

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x00074CAA File Offset: 0x00072EAA
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00074CB4 File Offset: 0x00072EB4
		[NullableContext(1)]
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
			else if (this.Instance != null)
			{
				return this.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00074D14 File Offset: 0x00072F14
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Instance != null)
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

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x00074D9F File Offset: 0x00072F9F
		public override ITypeSymbol Type { get; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x00074DA7 File Offset: 0x00072FA7
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x00074DAA File Offset: 0x00072FAA
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Invocation;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00074DAE File Offset: 0x00072FAE
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInvocation(this);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00074DB7 File Offset: 0x00072FB7
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInvocation(this, argument);
		}
	}
}
