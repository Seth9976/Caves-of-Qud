using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000444 RID: 1092
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StaticLocalInitializationSemaphoreOperation : Operation, IStaticLocalInitializationSemaphoreOperation, IOperation
	{
		// Token: 0x060029CE RID: 10702 RVA: 0x00078D77 File Offset: 0x00076F77
		internal StaticLocalInitializationSemaphoreOperation(ILocalSymbol local, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Local = local;
			this.Type = type;
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x00078D92 File Offset: 0x00076F92
		public ILocalSymbol Local { get; }

		// Token: 0x060029D0 RID: 10704 RVA: 0x00078D9A File Offset: 0x00076F9A
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x00078DAD File Offset: 0x00076FAD
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x00078DBF File Offset: 0x00076FBF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x00078DC7 File Offset: 0x00076FC7
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x00078DCA File Offset: 0x00076FCA
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.StaticLocalInitializationSemaphore;
			}
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00078DCE File Offset: 0x00076FCE
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitStaticLocalInitializationSemaphore(this);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00078DD7 File Offset: 0x00076FD7
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitStaticLocalInitializationSemaphore(this, argument);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00078DE1 File Offset: 0x00076FE1
		public StaticLocalInitializationSemaphoreOperation(ILocalSymbol local, SyntaxNode syntax, ITypeSymbol type)
			: this(local, null, syntax, type, true)
		{
		}
	}
}
