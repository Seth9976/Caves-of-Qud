using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000439 RID: 1081
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ExplicitInterfaceSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005C0F RID: 23567 RVA: 0x001672FD File Offset: 0x001654FD
		internal ExplicitInterfaceSpecifierSyntax(SyntaxKind kind, NameSyntax name, SyntaxToken dotToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x0016732D File Offset: 0x0016552D
		internal ExplicitInterfaceSpecifierSyntax(SyntaxKind kind, NameSyntax name, SyntaxToken dotToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00167361 File Offset: 0x00165561
		internal ExplicitInterfaceSpecifierSyntax(SyntaxKind kind, NameSyntax name, SyntaxToken dotToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(dotToken);
			this.dotToken = dotToken;
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06005C12 RID: 23570 RVA: 0x0016738D File Offset: 0x0016558D
		public NameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x00167395 File Offset: 0x00165595
		public SyntaxToken DotToken
		{
			get
			{
				return this.dotToken;
			}
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x001673A0 File Offset: 0x001655A0
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
					greenNode = this.dotToken;
				}
			}
			else
			{
				greenNode = this.name;
			}
			return greenNode;
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x001673CB File Offset: 0x001655CB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ExplicitInterfaceSpecifierSyntax(this, parent, position);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x001673D5 File Offset: 0x001655D5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExplicitInterfaceSpecifier(this);
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x001673DE File Offset: 0x001655DE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExplicitInterfaceSpecifier(this);
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x001673E8 File Offset: 0x001655E8
		public ExplicitInterfaceSpecifierSyntax Update(NameSyntax name, SyntaxToken dotToken)
		{
			if (name != this.Name || dotToken != this.DotToken)
			{
				ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = SyntaxFactory.ExplicitInterfaceSpecifier(name, dotToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					explicitInterfaceSpecifierSyntax = explicitInterfaceSpecifierSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					explicitInterfaceSpecifierSyntax = explicitInterfaceSpecifierSyntax.WithAnnotationsGreen(annotations);
				}
				return explicitInterfaceSpecifierSyntax;
			}
			return this;
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x0016743E File Offset: 0x0016563E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ExplicitInterfaceSpecifierSyntax(base.Kind, this.name, this.dotToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x0016745E File Offset: 0x0016565E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ExplicitInterfaceSpecifierSyntax(base.Kind, this.name, this.dotToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00167480 File Offset: 0x00165680
		internal ExplicitInterfaceSpecifierSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			NameSyntax nameSyntax = (NameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(nameSyntax);
			this.name = nameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.dotToken = syntaxToken;
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x001674CF File Offset: 0x001656CF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.dotToken);
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x001674F0 File Offset: 0x001656F0
		static ExplicitInterfaceSpecifierSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ExplicitInterfaceSpecifierSyntax), (ObjectReader r) => new ExplicitInterfaceSpecifierSyntax(r));
		}

		// Token: 0x0400183E RID: 6206
		internal readonly NameSyntax name;

		// Token: 0x0400183F RID: 6207
		internal readonly SyntaxToken dotToken;
	}
}
