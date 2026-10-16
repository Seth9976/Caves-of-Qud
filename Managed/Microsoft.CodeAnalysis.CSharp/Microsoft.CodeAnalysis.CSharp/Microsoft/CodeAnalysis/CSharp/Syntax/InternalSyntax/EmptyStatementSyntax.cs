using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F4 RID: 1012
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EmptyStatementSyntax : StatementSyntax
	{
		// Token: 0x0600579F RID: 22431 RVA: 0x00158502 File Offset: 0x00156702
		internal EmptyStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x00158535 File Offset: 0x00156735
		internal EmptyStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x0015856C File Offset: 0x0015676C
		internal EmptyStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 2;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0015859B File Offset: 0x0015679B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x001585A8 File Offset: 0x001567A8
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x001585B0 File Offset: 0x001567B0
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
					greenNode = this.semicolonToken;
				}
			}
			else
			{
				greenNode = this.attributeLists;
			}
			return greenNode;
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x001585DB File Offset: 0x001567DB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EmptyStatementSyntax(this, parent, position);
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x001585E5 File Offset: 0x001567E5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEmptyStatement(this);
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x001585EE File Offset: 0x001567EE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEmptyStatement(this);
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x001585F8 File Offset: 0x001567F8
		public EmptyStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || semicolonToken != this.SemicolonToken)
			{
				EmptyStatementSyntax emptyStatementSyntax = SyntaxFactory.EmptyStatement(attributeLists, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					emptyStatementSyntax = emptyStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					emptyStatementSyntax = emptyStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return emptyStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x00158653 File Offset: 0x00156853
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EmptyStatementSyntax(base.Kind, this.attributeLists, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x00158673 File Offset: 0x00156873
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EmptyStatementSyntax(base.Kind, this.attributeLists, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x00158694 File Offset: 0x00156894
		internal EmptyStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.semicolonToken = syntaxToken;
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x001586E6 File Offset: 0x001568E6
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x00158707 File Offset: 0x00156907
		static EmptyStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EmptyStatementSyntax), (ObjectReader r) => new EmptyStatementSyntax(r));
		}

		// Token: 0x04001716 RID: 5910
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001717 RID: 5911
		internal readonly SyntaxToken semicolonToken;
	}
}
