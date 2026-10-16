using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000089 RID: 137
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDiscardExpression : BoundExpression
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00052740 File Offset: 0x00050940
		public BoundExpression SetInferredTypeWithAnnotations(TypeWithAnnotations type)
		{
			return this.Update(type.Type);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0005274F File Offset: 0x0005094F
		public BoundDiscardExpression FailInference(Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnosticsOpt)
		{
			if (((diagnosticsOpt != null) ? diagnosticsOpt.DiagnosticBag : null) != null)
			{
				Binder.Error(diagnosticsOpt, ErrorCode.ERR_DiscardTypeInferenceFailed, this.Syntax);
			}
			return this.Update(binder.CreateErrorType("var"));
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00052788 File Offset: 0x00050988
		public override Symbol ExpressionSymbol
		{
			get
			{
				return new DiscardSymbol(TypeWithAnnotations.Create(this.Type, base.TopLevelNullability.Annotation.ToInternalAnnotation(), default(ImmutableArray<CustomModifier>)));
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000527C1 File Offset: 0x000509C1
		public override object Display
		{
			get
			{
				return this.Type ?? "_";
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000527D2 File Offset: 0x000509D2
		public BoundDiscardExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(BoundKind.DiscardExpression, syntax, type, hasErrors)
		{
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000527E2 File Offset: 0x000509E2
		public BoundDiscardExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(BoundKind.DiscardExpression, syntax, type)
		{
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x000527F1 File Offset: 0x000509F1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000527F9 File Offset: 0x000509F9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDiscardExpression(this);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00052802 File Offset: 0x00050A02
		public BoundDiscardExpression Update([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDiscardExpression boundDiscardExpression = new BoundDiscardExpression(this.Syntax, type, base.HasErrors);
				boundDiscardExpression.CopyAttributes(this);
				return boundDiscardExpression;
			}
			return this;
		}
	}
}
