using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000419 RID: 1049
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DelegateCreationOperation : Operation, IDelegateCreationOperation, IOperation
	{
		// Token: 0x06002847 RID: 10311 RVA: 0x00076D21 File Offset: 0x00074F21
		internal DelegateCreationOperation(IOperation target, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Target = Operation.SetParentOperation<IOperation>(target, this);
			this.Type = type;
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x00076D42 File Offset: 0x00074F42
		public IOperation Target { get; }

		// Token: 0x06002849 RID: 10313 RVA: 0x00076D4C File Offset: 0x00074F4C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Target != null)
			{
				return this.Target;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00076D80 File Offset: 0x00074F80
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
			else if (this.Target != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00076DB7 File Offset: 0x00074FB7
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00076DBF File Offset: 0x00074FBF
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x00076DC2 File Offset: 0x00074FC2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DelegateCreation;
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x00076DC6 File Offset: 0x00074FC6
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDelegateCreation(this);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00076DCF File Offset: 0x00074FCF
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDelegateCreation(this, argument);
		}
	}
}
