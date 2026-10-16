using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000453 RID: 1107
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class QualifiedCrefSyntax : CrefSyntax
	{
		// Token: 0x06005DA6 RID: 23974 RVA: 0x0016CA5B File Offset: 0x0016AC5B
		internal QualifiedCrefSyntax(SyntaxKind kind, TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(container);
			this.container = container;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(member);
			this.member = member;
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x0016CA9C File Offset: 0x0016AC9C
		internal QualifiedCrefSyntax(SyntaxKind kind, TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(container);
			this.container = container;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(member);
			this.member = member;
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x0016CAEB File Offset: 0x0016ACEB
		internal QualifiedCrefSyntax(SyntaxKind kind, TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(container);
			this.container = container;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
			base.AdjustFlagsAndWidth(member);
			this.member = member;
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x0016CB27 File Offset: 0x0016AD27
		public TypeSyntax Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x0016CB2F File Offset: 0x0016AD2F
		public SyntaxToken DotToken
		{
			get
			{
				return this.dotToken;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x0016CB37 File Offset: 0x0016AD37
		public MemberCrefSyntax Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x0016CB40 File Offset: 0x0016AD40
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.container;
				break;
			case 1:
				greenNode = this.dotToken;
				break;
			case 2:
				greenNode = this.member;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x0016CB7F File Offset: 0x0016AD7F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new QualifiedCrefSyntax(this, parent, position);
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x0016CB89 File Offset: 0x0016AD89
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQualifiedCref(this);
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x0016CB92 File Offset: 0x0016AD92
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQualifiedCref(this);
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x0016CB9C File Offset: 0x0016AD9C
		public QualifiedCrefSyntax Update(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
		{
			if (container != this.Container || dotToken != this.DotToken || member != this.Member)
			{
				QualifiedCrefSyntax qualifiedCrefSyntax = SyntaxFactory.QualifiedCref(container, dotToken, member);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					qualifiedCrefSyntax = qualifiedCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					qualifiedCrefSyntax = qualifiedCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return qualifiedCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x0016CBFC File Offset: 0x0016ADFC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new QualifiedCrefSyntax(base.Kind, this.container, this.dotToken, this.member, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x0016CC22 File Offset: 0x0016AE22
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new QualifiedCrefSyntax(base.Kind, this.container, this.dotToken, this.member, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0016CC48 File Offset: 0x0016AE48
		internal QualifiedCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.container = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.dotToken = syntaxToken;
			MemberCrefSyntax memberCrefSyntax = (MemberCrefSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(memberCrefSyntax);
			this.member = memberCrefSyntax;
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0016CCB1 File Offset: 0x0016AEB1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.container);
			writer.WriteValue(this.dotToken);
			writer.WriteValue(this.member);
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x0016CCDE File Offset: 0x0016AEDE
		static QualifiedCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(QualifiedCrefSyntax), (ObjectReader r) => new QualifiedCrefSyntax(r));
		}

		// Token: 0x040018AA RID: 6314
		internal readonly TypeSyntax container;

		// Token: 0x040018AB RID: 6315
		internal readonly SyntaxToken dotToken;

		// Token: 0x040018AC RID: 6316
		internal readonly MemberCrefSyntax member;
	}
}
