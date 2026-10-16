using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BB RID: 955
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AnonymousObjectMemberDeclaratorSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600545F RID: 21599 RVA: 0x0014FABF File Offset: 0x0014DCBF
		internal AnonymousObjectMemberDeclaratorSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x0014FAF2 File Offset: 0x0014DCF2
		internal AnonymousObjectMemberDeclaratorSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0014FB29 File Offset: 0x0014DD29
		internal AnonymousObjectMemberDeclaratorSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 2;
			if (nameEquals != null)
			{
				base.AdjustFlagsAndWidth(nameEquals);
				this.nameEquals = nameEquals;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0014FB58 File Offset: 0x0014DD58
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameEqualsSyntax NameEquals
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.nameEquals;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x0014FB60 File Offset: 0x0014DD60
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0014FB68 File Offset: 0x0014DD68
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.expression;
				}
			}
			else
			{
				greenNode = this.nameEquals;
			}
			return greenNode;
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0014FB93 File Offset: 0x0014DD93
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AnonymousObjectMemberDeclaratorSyntax(this, parent, position);
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x0014FB9D File Offset: 0x0014DD9D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousObjectMemberDeclarator(this);
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x0014FBA6 File Offset: 0x0014DDA6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousObjectMemberDeclarator(this);
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x0014FBB0 File Offset: 0x0014DDB0
		public AnonymousObjectMemberDeclaratorSyntax Update(NameEqualsSyntax nameEquals, ExpressionSyntax expression)
		{
			if (nameEquals != this.NameEquals || expression != this.Expression)
			{
				AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax = SyntaxFactory.AnonymousObjectMemberDeclarator(nameEquals, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					anonymousObjectMemberDeclaratorSyntax = anonymousObjectMemberDeclaratorSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					anonymousObjectMemberDeclaratorSyntax = anonymousObjectMemberDeclaratorSyntax.WithAnnotationsGreen(annotations);
				}
				return anonymousObjectMemberDeclaratorSyntax;
			}
			return this;
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0014FC06 File Offset: 0x0014DE06
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AnonymousObjectMemberDeclaratorSyntax(base.Kind, this.nameEquals, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0014FC26 File Offset: 0x0014DE26
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AnonymousObjectMemberDeclaratorSyntax(base.Kind, this.nameEquals, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0014FC48 File Offset: 0x0014DE48
		internal AnonymousObjectMemberDeclaratorSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)reader.ReadValue();
			if (nameEqualsSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameEqualsSyntax);
				this.nameEquals = nameEqualsSyntax;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0014FC9A File Offset: 0x0014DE9A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.nameEquals);
			writer.WriteValue(this.expression);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0014FCBB File Offset: 0x0014DEBB
		static AnonymousObjectMemberDeclaratorSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AnonymousObjectMemberDeclaratorSyntax), (ObjectReader r) => new AnonymousObjectMemberDeclaratorSyntax(r));
		}

		// Token: 0x04001680 RID: 5760
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameEqualsSyntax nameEquals;

		// Token: 0x04001681 RID: 5761
		internal readonly ExpressionSyntax expression;
	}
}
