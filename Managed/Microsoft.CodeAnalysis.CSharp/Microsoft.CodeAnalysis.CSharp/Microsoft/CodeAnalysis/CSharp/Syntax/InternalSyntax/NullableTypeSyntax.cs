using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000387 RID: 903
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NullableTypeSyntax : TypeSyntax
	{
		// Token: 0x06005173 RID: 20851 RVA: 0x0014809B File Offset: 0x0014629B
		internal NullableTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken questionToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x001480CB File Offset: 0x001462CB
		internal NullableTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken questionToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x001480FF File Offset: 0x001462FF
		internal NullableTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken questionToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(questionToken);
			this.questionToken = questionToken;
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x0014812B File Offset: 0x0014632B
		public TypeSyntax ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x00148133 File Offset: 0x00146333
		public SyntaxToken QuestionToken
		{
			get
			{
				return this.questionToken;
			}
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0014813C File Offset: 0x0014633C
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
					greenNode = this.questionToken;
				}
			}
			else
			{
				greenNode = this.elementType;
			}
			return greenNode;
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x00148167 File Offset: 0x00146367
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NullableTypeSyntax(this, parent, position);
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x00148171 File Offset: 0x00146371
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNullableType(this);
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x0014817A File Offset: 0x0014637A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNullableType(this);
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00148184 File Offset: 0x00146384
		public NullableTypeSyntax Update(TypeSyntax elementType, SyntaxToken questionToken)
		{
			if (elementType != this.ElementType || questionToken != this.QuestionToken)
			{
				NullableTypeSyntax nullableTypeSyntax = SyntaxFactory.NullableType(elementType, questionToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					nullableTypeSyntax = nullableTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					nullableTypeSyntax = nullableTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return nullableTypeSyntax;
			}
			return this;
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x001481DA File Offset: 0x001463DA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NullableTypeSyntax(base.Kind, this.elementType, this.questionToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x001481FA File Offset: 0x001463FA
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NullableTypeSyntax(base.Kind, this.elementType, this.questionToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0014821C File Offset: 0x0014641C
		internal NullableTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.elementType = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.questionToken = syntaxToken;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0014826B File Offset: 0x0014646B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.elementType);
			writer.WriteValue(this.questionToken);
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0014828C File Offset: 0x0014648C
		static NullableTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NullableTypeSyntax), (ObjectReader r) => new NullableTypeSyntax(r));
		}

		// Token: 0x040015FB RID: 5627
		internal readonly TypeSyntax elementType;

		// Token: 0x040015FC RID: 5628
		internal readonly SyntaxToken questionToken;
	}
}
