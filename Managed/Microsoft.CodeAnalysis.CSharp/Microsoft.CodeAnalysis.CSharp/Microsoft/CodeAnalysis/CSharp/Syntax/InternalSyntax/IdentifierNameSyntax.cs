using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000370 RID: 880
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IdentifierNameSyntax : SimpleNameSyntax
	{
		// Token: 0x06004F24 RID: 20260 RVA: 0x00141295 File Offset: 0x0013F495
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override string ToString()
		{
			return this.Identifier.Text;
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x001412A2 File Offset: 0x0013F4A2
		internal IdentifierNameSyntax(SyntaxKind kind, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x001412C3 File Offset: 0x0013F4C3
		internal IdentifierNameSyntax(SyntaxKind kind, SyntaxToken identifier, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x001412E8 File Offset: 0x0013F4E8
		internal IdentifierNameSyntax(SyntaxKind kind, SyntaxToken identifier)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x00141306 File Offset: 0x0013F506
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0014130E File Offset: 0x0013F50E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.identifier;
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0014131B File Offset: 0x0013F51B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IdentifierNameSyntax(this, parent, position);
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x00141325 File Offset: 0x0013F525
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIdentifierName(this);
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0014132E File Offset: 0x0013F52E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIdentifierName(this);
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x00141338 File Offset: 0x0013F538
		public IdentifierNameSyntax Update(SyntaxToken identifier)
		{
			if (identifier != this.Identifier)
			{
				IdentifierNameSyntax identifierNameSyntax = SyntaxFactory.IdentifierName(identifier);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					identifierNameSyntax = identifierNameSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					identifierNameSyntax = identifierNameSyntax.WithAnnotationsGreen(annotations);
				}
				return identifierNameSyntax;
			}
			return this;
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00141384 File Offset: 0x0013F584
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IdentifierNameSyntax(base.Kind, this.identifier, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0014139E File Offset: 0x0013F59E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IdentifierNameSyntax(base.Kind, this.identifier, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001413B8 File Offset: 0x0013F5B8
		internal IdentifierNameSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001413ED File Offset: 0x0013F5ED
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.identifier);
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x00141402 File Offset: 0x0013F602
		static IdentifierNameSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IdentifierNameSyntax), (ObjectReader r) => new IdentifierNameSyntax(r));
		}

		// Token: 0x040015BE RID: 5566
		internal readonly SyntaxToken identifier;
	}
}
