using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000435 RID: 1077
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DefaultConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06005BD9 RID: 23513 RVA: 0x001669EB File Offset: 0x00164BEB
		internal DefaultConstraintSyntax(SyntaxKind kind, SyntaxToken defaultKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(defaultKeyword);
			this.defaultKeyword = defaultKeyword;
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x00166A0C File Offset: 0x00164C0C
		internal DefaultConstraintSyntax(SyntaxKind kind, SyntaxToken defaultKeyword, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(defaultKeyword);
			this.defaultKeyword = defaultKeyword;
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x00166A31 File Offset: 0x00164C31
		internal DefaultConstraintSyntax(SyntaxKind kind, SyntaxToken defaultKeyword)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(defaultKeyword);
			this.defaultKeyword = defaultKeyword;
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06005BDC RID: 23516 RVA: 0x00166A4F File Offset: 0x00164C4F
		public SyntaxToken DefaultKeyword
		{
			get
			{
				return this.defaultKeyword;
			}
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x00166A57 File Offset: 0x00164C57
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.defaultKeyword;
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x00166A64 File Offset: 0x00164C64
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DefaultConstraintSyntax(this, parent, position);
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x00166A6E File Offset: 0x00164C6E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultConstraint(this);
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x00166A77 File Offset: 0x00164C77
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultConstraint(this);
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x00166A80 File Offset: 0x00164C80
		public DefaultConstraintSyntax Update(SyntaxToken defaultKeyword)
		{
			if (defaultKeyword != this.DefaultKeyword)
			{
				DefaultConstraintSyntax defaultConstraintSyntax = SyntaxFactory.DefaultConstraint(defaultKeyword);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					defaultConstraintSyntax = defaultConstraintSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					defaultConstraintSyntax = defaultConstraintSyntax.WithAnnotationsGreen(annotations);
				}
				return defaultConstraintSyntax;
			}
			return this;
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x00166ACC File Offset: 0x00164CCC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DefaultConstraintSyntax(base.Kind, this.defaultKeyword, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x00166AE6 File Offset: 0x00164CE6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DefaultConstraintSyntax(base.Kind, this.defaultKeyword, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x00166B00 File Offset: 0x00164D00
		internal DefaultConstraintSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.defaultKeyword = syntaxToken;
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00166B35 File Offset: 0x00164D35
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.defaultKeyword);
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00166B4A File Offset: 0x00164D4A
		static DefaultConstraintSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DefaultConstraintSyntax), (ObjectReader r) => new DefaultConstraintSyntax(r));
		}

		// Token: 0x04001834 RID: 6196
		internal readonly SyntaxToken defaultKeyword;
	}
}
