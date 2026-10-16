using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000396 RID: 918
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ElementBindingExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005241 RID: 21057 RVA: 0x00149F41 File Offset: 0x00148141
		internal ElementBindingExpressionSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00149F62 File Offset: 0x00148162
		internal ElementBindingExpressionSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x00149F87 File Offset: 0x00148187
		internal ElementBindingExpressionSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x00149FA5 File Offset: 0x001481A5
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x00149FAD File Offset: 0x001481AD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.argumentList;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x00149FBA File Offset: 0x001481BA
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ElementBindingExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x00149FC4 File Offset: 0x001481C4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElementBindingExpression(this);
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x00149FCD File Offset: 0x001481CD
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElementBindingExpression(this);
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x00149FD8 File Offset: 0x001481D8
		public ElementBindingExpressionSyntax Update(BracketedArgumentListSyntax argumentList)
		{
			if (argumentList != this.ArgumentList)
			{
				ElementBindingExpressionSyntax elementBindingExpressionSyntax = SyntaxFactory.ElementBindingExpression(argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					elementBindingExpressionSyntax = elementBindingExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					elementBindingExpressionSyntax = elementBindingExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return elementBindingExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0014A024 File Offset: 0x00148224
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ElementBindingExpressionSyntax(base.Kind, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0014A03E File Offset: 0x0014823E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ElementBindingExpressionSyntax(base.Kind, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0014A058 File Offset: 0x00148258
		internal ElementBindingExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(bracketedArgumentListSyntax);
			this.argumentList = bracketedArgumentListSyntax;
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0014A08D File Offset: 0x0014828D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0014A0A2 File Offset: 0x001482A2
		static ElementBindingExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ElementBindingExpressionSyntax), (ObjectReader r) => new ElementBindingExpressionSyntax(r));
		}

		// Token: 0x0400161A RID: 5658
		internal readonly BracketedArgumentListSyntax argumentList;
	}
}
