using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F7 RID: 1015
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BreakStatementSyntax : StatementSyntax
	{
		// Token: 0x060057D1 RID: 22481 RVA: 0x00158E94 File Offset: 0x00157094
		internal BreakStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(breakKeyword);
			this.breakKeyword = breakKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00158EE4 File Offset: 0x001570E4
		internal BreakStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(breakKeyword);
			this.breakKeyword = breakKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00158F36 File Offset: 0x00157136
		internal BreakStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(breakKeyword);
			this.breakKeyword = breakKeyword;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060057D4 RID: 22484 RVA: 0x00158F75 File Offset: 0x00157175
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060057D5 RID: 22485 RVA: 0x00158F82 File Offset: 0x00157182
		public SyntaxToken BreakKeyword
		{
			get
			{
				return this.breakKeyword;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060057D6 RID: 22486 RVA: 0x00158F8A File Offset: 0x0015718A
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00158F94 File Offset: 0x00157194
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
				greenNode = this.breakKeyword;
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

		// Token: 0x060057D8 RID: 22488 RVA: 0x00158FD3 File Offset: 0x001571D3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BreakStatementSyntax(this, parent, position);
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00158FDD File Offset: 0x001571DD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBreakStatement(this);
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x00158FE6 File Offset: 0x001571E6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBreakStatement(this);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00158FF0 File Offset: 0x001571F0
		public BreakStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || breakKeyword != this.BreakKeyword || semicolonToken != this.SemicolonToken)
			{
				BreakStatementSyntax breakStatementSyntax = SyntaxFactory.BreakStatement(attributeLists, breakKeyword, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					breakStatementSyntax = breakStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					breakStatementSyntax = breakStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return breakStatementSyntax;
			}
			return this;
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x00159055 File Offset: 0x00157255
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BreakStatementSyntax(base.Kind, this.attributeLists, this.breakKeyword, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x0015907B File Offset: 0x0015727B
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BreakStatementSyntax(base.Kind, this.attributeLists, this.breakKeyword, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060057DE RID: 22494 RVA: 0x001590A4 File Offset: 0x001572A4
		internal BreakStatementSyntax(ObjectReader reader)
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
			this.breakKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.semicolonToken = syntaxToken2;
		}

		// Token: 0x060057DF RID: 22495 RVA: 0x00159110 File Offset: 0x00157310
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.breakKeyword);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x0015913D File Offset: 0x0015733D
		static BreakStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BreakStatementSyntax), (ObjectReader r) => new BreakStatementSyntax(r));
		}

		// Token: 0x04001721 RID: 5921
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001722 RID: 5922
		internal readonly SyntaxToken breakKeyword;

		// Token: 0x04001723 RID: 5923
		internal readonly SyntaxToken semicolonToken;
	}
}
