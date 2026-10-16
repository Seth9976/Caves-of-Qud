using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BF RID: 959
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class StackAllocArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060054A1 RID: 21665 RVA: 0x001506F4 File Offset: 0x0014E8F4
		internal StackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00150744 File Offset: 0x0014E944
		internal StackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x00150797 File Offset: 0x0014E997
		internal StackAllocArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stackAllocKeyword);
			this.stackAllocKeyword = stackAllocKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x001507D7 File Offset: 0x0014E9D7
		public SyntaxToken StackAllocKeyword
		{
			get
			{
				return this.stackAllocKeyword;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x001507DF File Offset: 0x0014E9DF
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x001507E7 File Offset: 0x0014E9E7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x001507F0 File Offset: 0x0014E9F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.stackAllocKeyword;
				break;
			case 1:
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0015082F File Offset: 0x0014EA2F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new StackAllocArrayCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00150839 File Offset: 0x0014EA39
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x00150842 File Offset: 0x0014EA42
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitStackAllocArrayCreationExpression(this);
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0015084C File Offset: 0x0014EA4C
		public StackAllocArrayCreationExpressionSyntax Update(SyntaxToken stackAllocKeyword, TypeSyntax type, InitializerExpressionSyntax initializer)
		{
			if (stackAllocKeyword != this.StackAllocKeyword || type != this.Type || initializer != this.Initializer)
			{
				StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax = SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					stackAllocArrayCreationExpressionSyntax = stackAllocArrayCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					stackAllocArrayCreationExpressionSyntax = stackAllocArrayCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return stackAllocArrayCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x001508AC File Offset: 0x0014EAAC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new StackAllocArrayCreationExpressionSyntax(base.Kind, this.stackAllocKeyword, this.type, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x001508D2 File Offset: 0x0014EAD2
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new StackAllocArrayCreationExpressionSyntax(base.Kind, this.stackAllocKeyword, this.type, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x001508F8 File Offset: 0x0014EAF8
		internal StackAllocArrayCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.stackAllocKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			if (initializerExpressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(initializerExpressionSyntax);
				this.initializer = initializerExpressionSyntax;
			}
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00150964 File Offset: 0x0014EB64
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.stackAllocKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x00150991 File Offset: 0x0014EB91
		static StackAllocArrayCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(StackAllocArrayCreationExpressionSyntax), (ObjectReader r) => new StackAllocArrayCreationExpressionSyntax(r));
		}

		// Token: 0x0400168E RID: 5774
		internal readonly SyntaxToken stackAllocKeyword;

		// Token: 0x0400168F RID: 5775
		internal readonly TypeSyntax type;

		// Token: 0x04001690 RID: 5776
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InitializerExpressionSyntax initializer;
	}
}
