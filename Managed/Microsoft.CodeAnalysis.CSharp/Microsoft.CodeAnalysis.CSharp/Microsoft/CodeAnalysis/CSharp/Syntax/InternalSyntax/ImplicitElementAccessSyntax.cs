using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000398 RID: 920
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ImplicitElementAccessSyntax : ExpressionSyntax
	{
		// Token: 0x0600525F RID: 21087 RVA: 0x0014A39D File Offset: 0x0014859D
		internal ImplicitElementAccessSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0014A3BE File Offset: 0x001485BE
		internal ImplicitElementAccessSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0014A3E3 File Offset: 0x001485E3
		internal ImplicitElementAccessSyntax(SyntaxKind kind, BracketedArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06005262 RID: 21090 RVA: 0x0014A401 File Offset: 0x00148601
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0014A409 File Offset: 0x00148609
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.argumentList;
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0014A416 File Offset: 0x00148616
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ImplicitElementAccessSyntax(this, parent, position);
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0014A420 File Offset: 0x00148620
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitElementAccess(this);
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0014A429 File Offset: 0x00148629
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitElementAccess(this);
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0014A434 File Offset: 0x00148634
		public ImplicitElementAccessSyntax Update(BracketedArgumentListSyntax argumentList)
		{
			if (argumentList != this.ArgumentList)
			{
				ImplicitElementAccessSyntax implicitElementAccessSyntax = SyntaxFactory.ImplicitElementAccess(argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					implicitElementAccessSyntax = implicitElementAccessSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					implicitElementAccessSyntax = implicitElementAccessSyntax.WithAnnotationsGreen(annotations);
				}
				return implicitElementAccessSyntax;
			}
			return this;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0014A480 File Offset: 0x00148680
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ImplicitElementAccessSyntax(base.Kind, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0014A49A File Offset: 0x0014869A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ImplicitElementAccessSyntax(base.Kind, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0014A4B4 File Offset: 0x001486B4
		internal ImplicitElementAccessSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(bracketedArgumentListSyntax);
			this.argumentList = bracketedArgumentListSyntax;
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0014A4E9 File Offset: 0x001486E9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0014A4FE File Offset: 0x001486FE
		static ImplicitElementAccessSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ImplicitElementAccessSyntax), (ObjectReader r) => new ImplicitElementAccessSyntax(r));
		}

		// Token: 0x0400161E RID: 5662
		internal readonly BracketedArgumentListSyntax argumentList;
	}
}
