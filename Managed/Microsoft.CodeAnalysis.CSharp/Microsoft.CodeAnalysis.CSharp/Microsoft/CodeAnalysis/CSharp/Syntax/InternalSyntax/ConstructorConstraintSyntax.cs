using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000432 RID: 1074
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConstructorConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06005BAC RID: 23468 RVA: 0x001663A1 File Offset: 0x001645A1
		internal ConstructorConstraintSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x001663E4 File Offset: 0x001645E4
		internal ConstructorConstraintSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x00166433 File Offset: 0x00164633
		internal ConstructorConstraintSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x0016646F File Offset: 0x0016466F
		public SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00166477 File Offset: 0x00164677
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x0016647F File Offset: 0x0016467F
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x00166488 File Offset: 0x00164688
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.newKeyword;
				break;
			case 1:
				syntaxToken = this.openParenToken;
				break;
			case 2:
				syntaxToken = this.closeParenToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001664C7 File Offset: 0x001646C7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConstructorConstraintSyntax(this, parent, position);
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001664D1 File Offset: 0x001646D1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorConstraint(this);
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001664DA File Offset: 0x001646DA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorConstraint(this);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x001664E4 File Offset: 0x001646E4
		public ConstructorConstraintSyntax Update(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
		{
			if (newKeyword != this.NewKeyword || openParenToken != this.OpenParenToken || closeParenToken != this.CloseParenToken)
			{
				ConstructorConstraintSyntax constructorConstraintSyntax = SyntaxFactory.ConstructorConstraint(newKeyword, openParenToken, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					constructorConstraintSyntax = constructorConstraintSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					constructorConstraintSyntax = constructorConstraintSyntax.WithAnnotationsGreen(annotations);
				}
				return constructorConstraintSyntax;
			}
			return this;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00166544 File Offset: 0x00164744
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConstructorConstraintSyntax(base.Kind, this.newKeyword, this.openParenToken, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x0016656A File Offset: 0x0016476A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConstructorConstraintSyntax(base.Kind, this.newKeyword, this.openParenToken, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x00166590 File Offset: 0x00164790
		internal ConstructorConstraintSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001665F9 File Offset: 0x001647F9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x00166626 File Offset: 0x00164826
		static ConstructorConstraintSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConstructorConstraintSyntax), (ObjectReader r) => new ConstructorConstraintSyntax(r));
		}

		// Token: 0x0400182E RID: 6190
		internal readonly SyntaxToken newKeyword;

		// Token: 0x0400182F RID: 6191
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001830 RID: 6192
		internal readonly SyntaxToken closeParenToken;
	}
}
