using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C8 RID: 968
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class JoinIntoClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005521 RID: 21793 RVA: 0x00151FF5 File Offset: 0x001501F5
		internal JoinIntoClauseSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x00152025 File Offset: 0x00150225
		internal JoinIntoClauseSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00152059 File Offset: 0x00150259
		internal JoinIntoClauseSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06005524 RID: 21796 RVA: 0x00152085 File Offset: 0x00150285
		public SyntaxToken IntoKeyword
		{
			get
			{
				return this.intoKeyword;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x0015208D File Offset: 0x0015028D
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x00152098 File Offset: 0x00150298
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
					syntaxToken = this.identifier;
				}
			}
			else
			{
				syntaxToken = this.intoKeyword;
			}
			return syntaxToken;
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x001520C3 File Offset: 0x001502C3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new JoinIntoClauseSyntax(this, parent, position);
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x001520CD File Offset: 0x001502CD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitJoinIntoClause(this);
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x001520D6 File Offset: 0x001502D6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitJoinIntoClause(this);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x001520E0 File Offset: 0x001502E0
		public JoinIntoClauseSyntax Update(SyntaxToken intoKeyword, SyntaxToken identifier)
		{
			if (intoKeyword != this.IntoKeyword || identifier != this.Identifier)
			{
				JoinIntoClauseSyntax joinIntoClauseSyntax = SyntaxFactory.JoinIntoClause(intoKeyword, identifier);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					joinIntoClauseSyntax = joinIntoClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					joinIntoClauseSyntax = joinIntoClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return joinIntoClauseSyntax;
			}
			return this;
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x00152136 File Offset: 0x00150336
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new JoinIntoClauseSyntax(base.Kind, this.intoKeyword, this.identifier, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00152156 File Offset: 0x00150356
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new JoinIntoClauseSyntax(base.Kind, this.intoKeyword, this.identifier, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x00152178 File Offset: 0x00150378
		internal JoinIntoClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.intoKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x001521C7 File Offset: 0x001503C7
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.intoKeyword);
			writer.WriteValue(this.identifier);
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x001521E8 File Offset: 0x001503E8
		static JoinIntoClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(JoinIntoClauseSyntax), (ObjectReader r) => new JoinIntoClauseSyntax(r));
		}

		// Token: 0x040016AD RID: 5805
		internal readonly SyntaxToken intoKeyword;

		// Token: 0x040016AE RID: 5806
		internal readonly SyntaxToken identifier;
	}
}
