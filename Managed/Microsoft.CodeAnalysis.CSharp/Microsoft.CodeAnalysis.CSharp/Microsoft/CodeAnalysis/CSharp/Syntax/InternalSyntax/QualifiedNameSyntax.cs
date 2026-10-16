using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037A RID: 890
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class QualifiedNameSyntax : NameSyntax
	{
		// Token: 0x060050AA RID: 20650 RVA: 0x0014613C File Offset: 0x0014433C
		internal QualifiedNameSyntax(SyntaxKind kind, NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0014617C File Offset: 0x0014437C
		internal QualifiedNameSyntax(SyntaxKind kind, NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x001461CB File Offset: 0x001443CB
		internal QualifiedNameSyntax(SyntaxKind kind, NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(left);
			this.left = left;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(right);
			this.right = right;
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x00146207 File Offset: 0x00144407
		public NameSyntax Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x0014620F File Offset: 0x0014440F
		public SyntaxToken DotToken
		{
			get
			{
				return this.dotToken;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x00146217 File Offset: 0x00144417
		public SimpleNameSyntax Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00146220 File Offset: 0x00144420
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.left;
				break;
			case 1:
				greenNode = this.dotToken;
				break;
			case 2:
				greenNode = this.right;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0014625F File Offset: 0x0014445F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new QualifiedNameSyntax(this, parent, position);
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x00146269 File Offset: 0x00144469
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQualifiedName(this);
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x00146272 File Offset: 0x00144472
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQualifiedName(this);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0014627C File Offset: 0x0014447C
		public QualifiedNameSyntax Update(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
		{
			if (left != this.Left || dotToken != this.DotToken || right != this.Right)
			{
				QualifiedNameSyntax qualifiedNameSyntax = SyntaxFactory.QualifiedName(left, dotToken, right);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					qualifiedNameSyntax = qualifiedNameSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					qualifiedNameSyntax = qualifiedNameSyntax.WithAnnotationsGreen(annotations);
				}
				return qualifiedNameSyntax;
			}
			return this;
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x001462DC File Offset: 0x001444DC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new QualifiedNameSyntax(base.Kind, this.left, this.dotToken, this.right, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x00146302 File Offset: 0x00144502
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new QualifiedNameSyntax(base.Kind, this.left, this.dotToken, this.right, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00146328 File Offset: 0x00144528
		internal QualifiedNameSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			NameSyntax nameSyntax = (NameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(nameSyntax);
			this.left = nameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.dotToken = syntaxToken;
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(simpleNameSyntax);
			this.right = simpleNameSyntax;
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00146391 File Offset: 0x00144591
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.left);
			writer.WriteValue(this.dotToken);
			writer.WriteValue(this.right);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x001463BE File Offset: 0x001445BE
		static QualifiedNameSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(QualifiedNameSyntax), (ObjectReader r) => new QualifiedNameSyntax(r));
		}

		// Token: 0x040015DB RID: 5595
		internal readonly NameSyntax left;

		// Token: 0x040015DC RID: 5596
		internal readonly SyntaxToken dotToken;

		// Token: 0x040015DD RID: 5597
		internal readonly SimpleNameSyntax right;
	}
}
