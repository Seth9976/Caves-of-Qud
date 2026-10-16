using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DF RID: 223
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConvertedStackAllocExpression : BoundStackAllocArrayCreationBase
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0005E8B4 File Offset: 0x0005CAB4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(BoundStackAllocArrayCreationBase.GetChildInitializers(base.InitializerOpt).Insert(0, base.Count));
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0005E8E0 File Offset: 0x0005CAE0
		public BoundConvertedStackAllocExpression(SyntaxNode syntax, TypeSymbol elementType, BoundExpression count, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ConvertedStackAllocExpression, syntax, elementType, count, initializerOpt, type, hasErrors || count.HasErrors() || initializerOpt.HasErrors())
		{
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0005E90A File Offset: 0x0005CB0A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0005E912 File Offset: 0x0005CB12
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConvertedStackAllocExpression(this);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0005E91C File Offset: 0x0005CB1C
		public BoundConvertedStackAllocExpression Update(TypeSymbol elementType, BoundExpression count, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, TypeSymbol type)
		{
			if (!TypeSymbol.Equals(elementType, base.ElementType, TypeCompareKind.ConsiderEverything) || count != base.Count || initializerOpt != base.InitializerOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConvertedStackAllocExpression boundConvertedStackAllocExpression = new BoundConvertedStackAllocExpression(this.Syntax, elementType, count, initializerOpt, type, base.HasErrors);
				boundConvertedStackAllocExpression.CopyAttributes(this);
				return boundConvertedStackAllocExpression;
			}
			return this;
		}
	}
}
