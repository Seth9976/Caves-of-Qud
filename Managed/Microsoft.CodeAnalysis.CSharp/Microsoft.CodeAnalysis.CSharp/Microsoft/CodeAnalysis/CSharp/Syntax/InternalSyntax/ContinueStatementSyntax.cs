using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F8 RID: 1016
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ContinueStatementSyntax : StatementSyntax
	{
		// Token: 0x060057E1 RID: 22497 RVA: 0x00159160 File Offset: 0x00157360
		internal ContinueStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(continueKeyword);
			this.continueKeyword = continueKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057E2 RID: 22498 RVA: 0x001591B0 File Offset: 0x001573B0
		internal ContinueStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(continueKeyword);
			this.continueKeyword = continueKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x00159202 File Offset: 0x00157402
		internal ContinueStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(continueKeyword);
			this.continueKeyword = continueKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060057E4 RID: 22500 RVA: 0x00159241 File Offset: 0x00157441
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0015924E File Offset: 0x0015744E
		public SyntaxToken ContinueKeyword
		{
			get
			{
				return this.continueKeyword;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x060057E6 RID: 22502 RVA: 0x00159256 File Offset: 0x00157456
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x00159260 File Offset: 0x00157460
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.continueKeyword;
				break;
			case 2:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060057E8 RID: 22504 RVA: 0x0015929F File Offset: 0x0015749F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ContinueStatementSyntax(this, parent, position);
		}

		// Token: 0x060057E9 RID: 22505 RVA: 0x001592A9 File Offset: 0x001574A9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitContinueStatement(this);
		}

		// Token: 0x060057EA RID: 22506 RVA: 0x001592B2 File Offset: 0x001574B2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitContinueStatement(this);
		}

		// Token: 0x060057EB RID: 22507 RVA: 0x001592BC File Offset: 0x001574BC
		public ContinueStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || continueKeyword != this.ContinueKeyword || semicolonToken != this.SemicolonToken)
			{
				ContinueStatementSyntax continueStatementSyntax = SyntaxFactory.ContinueStatement(attributeLists, continueKeyword, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					continueStatementSyntax = continueStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					continueStatementSyntax = continueStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return continueStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00159321 File Offset: 0x00157521
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ContinueStatementSyntax(base.Kind, this.attributeLists, this.continueKeyword, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x00159347 File Offset: 0x00157547
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ContinueStatementSyntax(base.Kind, this.attributeLists, this.continueKeyword, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x00159370 File Offset: 0x00157570
		internal ContinueStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.continueKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.semicolonToken = syntaxToken2;
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001593DC File Offset: 0x001575DC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.continueKeyword);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x00159409 File Offset: 0x00157609
		static ContinueStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ContinueStatementSyntax), (ObjectReader r) => new ContinueStatementSyntax(r));
		}

		// Token: 0x04001724 RID: 5924
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001725 RID: 5925
		internal readonly SyntaxToken continueKeyword;

		// Token: 0x04001726 RID: 5926
		internal readonly SyntaxToken semicolonToken;
	}
}
