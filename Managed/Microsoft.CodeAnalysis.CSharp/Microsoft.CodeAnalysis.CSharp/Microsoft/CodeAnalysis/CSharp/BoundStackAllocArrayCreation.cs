using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DE RID: 222
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundStackAllocArrayCreation : BoundStackAllocArrayCreationBase
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0005E79C File Offset: 0x0005C99C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(BoundStackAllocArrayCreationBase.GetChildInitializers(base.InitializerOpt).Insert(0, base.Count));
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0005E7C8 File Offset: 0x0005C9C8
		public override object Display
		{
			get
			{
				if (base.Type != null)
				{
					return base.Display;
				}
				return FormattableStringFactory.Create("stackalloc {0}[{1}]", new object[]
				{
					base.ElementType,
					base.Count.WasCompilerGenerated ? null : base.Count.Syntax.ToString()
				});
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0005E820 File Offset: 0x0005CA20
		public BoundStackAllocArrayCreation(SyntaxNode syntax, TypeSymbol elementType, BoundExpression count, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.StackAllocArrayCreation, syntax, elementType, count, initializerOpt, type, hasErrors || count.HasErrors() || initializerOpt.HasErrors())
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0005E84A File Offset: 0x0005CA4A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitStackAllocArrayCreation(this);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0005E854 File Offset: 0x0005CA54
		public BoundStackAllocArrayCreation Update(TypeSymbol elementType, BoundExpression count, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (!TypeSymbol.Equals(elementType, base.ElementType, TypeCompareKind.ConsiderEverything) || count != base.Count || initializerOpt != base.InitializerOpt || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundStackAllocArrayCreation boundStackAllocArrayCreation = new BoundStackAllocArrayCreation(this.Syntax, elementType, count, initializerOpt, type, base.HasErrors);
				boundStackAllocArrayCreation.CopyAttributes(this);
				return boundStackAllocArrayCreation;
			}
			return this;
		}
	}
}
