using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041C RID: 1052
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeTargetSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A4D RID: 23117 RVA: 0x001617D3 File Offset: 0x0015F9D3
		internal AttributeTargetSpecifierSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x00161803 File Offset: 0x0015FA03
		internal AttributeTargetSpecifierSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x00161837 File Offset: 0x0015FA37
		internal AttributeTargetSpecifierSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x00161863 File Offset: 0x0015FA63
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x0016186B File Offset: 0x0015FA6B
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x00161874 File Offset: 0x0015FA74
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxToken = null;
				}
				else
				{
					syntaxToken = this.colonToken;
				}
			}
			else
			{
				syntaxToken = this.identifier;
			}
			return syntaxToken;
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0016189F File Offset: 0x0015FA9F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AttributeTargetSpecifierSyntax(this, parent, position);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x001618A9 File Offset: 0x0015FAA9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeTargetSpecifier(this);
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x001618B2 File Offset: 0x0015FAB2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeTargetSpecifier(this);
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x001618BC File Offset: 0x0015FABC
		public AttributeTargetSpecifierSyntax Update(SyntaxToken identifier, SyntaxToken colonToken)
		{
			if (identifier != this.Identifier || colonToken != this.ColonToken)
			{
				AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = SyntaxFactory.AttributeTargetSpecifier(identifier, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					attributeTargetSpecifierSyntax = attributeTargetSpecifierSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					attributeTargetSpecifierSyntax = attributeTargetSpecifierSyntax.WithAnnotationsGreen(annotations);
				}
				return attributeTargetSpecifierSyntax;
			}
			return this;
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x00161912 File Offset: 0x0015FB12
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AttributeTargetSpecifierSyntax(base.Kind, this.identifier, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x00161932 File Offset: 0x0015FB32
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AttributeTargetSpecifierSyntax(base.Kind, this.identifier, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x00161954 File Offset: 0x0015FB54
		internal AttributeTargetSpecifierSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x001619A3 File Offset: 0x0015FBA3
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x001619C4 File Offset: 0x0015FBC4
		static AttributeTargetSpecifierSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AttributeTargetSpecifierSyntax), (ObjectReader r) => new AttributeTargetSpecifierSyntax(r));
		}

		// Token: 0x040017CF RID: 6095
		internal readonly SyntaxToken identifier;

		// Token: 0x040017D0 RID: 6096
		internal readonly SyntaxToken colonToken;
	}
}
