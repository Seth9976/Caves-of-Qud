using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045C RID: 1116
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CrefParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E1C RID: 24092 RVA: 0x0016DD36 File Offset: 0x0016BF36
		internal CrefParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x0016DD69 File Offset: 0x0016BF69
		internal CrefParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0016DDA0 File Offset: 0x0016BFA0
		internal CrefParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 2;
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x0016DDCF File Offset: 0x0016BFCF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken RefKindKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.refKindKeyword;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x0016DDD7 File Offset: 0x0016BFD7
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x0016DDE0 File Offset: 0x0016BFE0
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
					greenNode = this.type;
				}
			}
			else
			{
				greenNode = this.refKindKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x0016DE0B File Offset: 0x0016C00B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CrefParameterSyntax(this, parent, position);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x0016DE15 File Offset: 0x0016C015
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefParameter(this);
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x0016DE1E File Offset: 0x0016C01E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefParameter(this);
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x0016DE28 File Offset: 0x0016C028
		public CrefParameterSyntax Update(SyntaxToken refKindKeyword, TypeSyntax type)
		{
			if (refKindKeyword != this.RefKindKeyword || type != this.Type)
			{
				CrefParameterSyntax crefParameterSyntax = SyntaxFactory.CrefParameter(refKindKeyword, type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					crefParameterSyntax = crefParameterSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					crefParameterSyntax = crefParameterSyntax.WithAnnotationsGreen(annotations);
				}
				return crefParameterSyntax;
			}
			return this;
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x0016DE7E File Offset: 0x0016C07E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CrefParameterSyntax(base.Kind, this.refKindKeyword, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x0016DE9E File Offset: 0x0016C09E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CrefParameterSyntax(base.Kind, this.refKindKeyword, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x0016DEC0 File Offset: 0x0016C0C0
		internal CrefParameterSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.refKindKeyword = syntaxToken;
			}
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0016DF12 File Offset: 0x0016C112
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.refKindKeyword);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x0016DF33 File Offset: 0x0016C133
		static CrefParameterSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CrefParameterSyntax), (ObjectReader r) => new CrefParameterSyntax(r));
		}

		// Token: 0x040018BE RID: 6334
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken refKindKeyword;

		// Token: 0x040018BF RID: 6335
		internal readonly TypeSyntax type;
	}
}
