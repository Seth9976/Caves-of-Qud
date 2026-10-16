using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E8 RID: 232
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OutDeconstructVarPendingInference : BoundExpression
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0005F185 File Offset: 0x0005D385
		public override object Display
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0005F18C File Offset: 0x0005D38C
		public BoundDeconstructValuePlaceholder SetInferredTypeWithAnnotations(TypeWithAnnotations type, Binder binder, bool success)
		{
			this.Placeholder = new BoundDeconstructValuePlaceholder(this.Syntax, binder.LocalScopeDepth, type.Type, base.HasErrors || !success);
			return this.Placeholder;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
		public BoundDeconstructValuePlaceholder FailInference(Binder binder)
		{
			return this.SetInferredTypeWithAnnotations(TypeWithAnnotations.Create(binder.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), binder, false);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0005F1F3 File Offset: 0x0005D3F3
		public OutDeconstructVarPendingInference(SyntaxNode syntax, bool hasErrors)
			: base(BoundKind.OutDeconstructVarPendingInference, syntax, null, hasErrors)
		{
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0005F203 File Offset: 0x0005D403
		public OutDeconstructVarPendingInference(SyntaxNode syntax)
			: base(BoundKind.OutDeconstructVarPendingInference, syntax, null)
		{
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0005F212 File Offset: 0x0005D412
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0005F21A File Offset: 0x0005D41A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitOutDeconstructVarPendingInference(this);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0005F223 File Offset: 0x0005D423
		public OutDeconstructVarPendingInference Update()
		{
			return this;
		}

		// Token: 0x040005BB RID: 1467
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundDeconstructValuePlaceholder Placeholder;
	}
}
