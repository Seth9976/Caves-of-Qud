using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F9 RID: 1017
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ParameterReferenceOperation : Operation, IParameterReferenceOperation, IOperation
	{
		// Token: 0x06002707 RID: 9991 RVA: 0x00074F94 File Offset: 0x00073194
		internal ParameterReferenceOperation(IParameterSymbol parameter, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Parameter = parameter;
			this.Type = type;
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00074FAF File Offset: 0x000731AF
		public IParameterSymbol Parameter { get; }

		// Token: 0x06002709 RID: 9993 RVA: 0x00074FB7 File Offset: 0x000731B7
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00074FCA File Offset: 0x000731CA
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x00074FDC File Offset: 0x000731DC
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x00074FE4 File Offset: 0x000731E4
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x00074FE7 File Offset: 0x000731E7
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ParameterReference;
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x00074FEB File Offset: 0x000731EB
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitParameterReference(this);
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00074FF4 File Offset: 0x000731F4
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitParameterReference(this, argument);
		}
	}
}
