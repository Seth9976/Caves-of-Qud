using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003EB RID: 1003
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LocalDeclarationStatementSyntax : StatementSyntax
	{
		// Token: 0x0600571F RID: 22303 RVA: 0x0015705C File Offset: 0x0015525C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal LocalDeclarationStatementSyntax(SyntaxKind kind, GreenNode attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			if (usingKeyword != null)
			{
				base.AdjustFlagsAndWidth(usingKeyword);
				this.usingKeyword = usingKeyword;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x001570E8 File Offset: 0x001552E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal LocalDeclarationStatementSyntax(SyntaxKind kind, GreenNode attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			if (usingKeyword != null)
			{
				base.AdjustFlagsAndWidth(usingKeyword);
				this.usingKeyword = usingKeyword;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x00157178 File Offset: 0x00155378
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal LocalDeclarationStatementSyntax(SyntaxKind kind, GreenNode attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 6;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (awaitKeyword != null)
			{
				base.AdjustFlagsAndWidth(awaitKeyword);
				this.awaitKeyword = awaitKeyword;
			}
			if (usingKeyword != null)
			{
				base.AdjustFlagsAndWidth(usingKeyword);
				this.usingKeyword = usingKeyword;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06005722 RID: 22306 RVA: 0x001571FD File Offset: 0x001553FD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x0015720A File Offset: 0x0015540A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken AwaitKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.awaitKeyword;
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06005724 RID: 22308 RVA: 0x00157212 File Offset: 0x00155412
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken UsingKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.usingKeyword;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x0015721A File Offset: 0x0015541A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06005726 RID: 22310 RVA: 0x00157227 File Offset: 0x00155427
		public VariableDeclarationSyntax Declaration
		{
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06005727 RID: 22311 RVA: 0x0015722F File Offset: 0x0015542F
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x00157238 File Offset: 0x00155438
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
				greenNode = this.awaitKeyword;
				break;
			case 2:
				greenNode = this.usingKeyword;
				break;
			case 3:
				greenNode = this.modifiers;
				break;
			case 4:
				greenNode = this.declaration;
				break;
			case 5:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x0015729E File Offset: 0x0015549E
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LocalDeclarationStatementSyntax(this, parent, position);
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x001572A8 File Offset: 0x001554A8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLocalDeclarationStatement(this);
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001572B1 File Offset: 0x001554B1
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLocalDeclarationStatement(this);
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x001572BC File Offset: 0x001554BC
		public LocalDeclarationStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || awaitKeyword != this.AwaitKeyword || usingKeyword != this.UsingKeyword || modifiers != this.Modifiers || declaration != this.Declaration || semicolonToken != this.SemicolonToken)
			{
				LocalDeclarationStatementSyntax localDeclarationStatementSyntax = SyntaxFactory.LocalDeclarationStatement(attributeLists, awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					localDeclarationStatementSyntax = localDeclarationStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					localDeclarationStatementSyntax = localDeclarationStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return localDeclarationStatementSyntax;
			}
			return this;
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x0015734C File Offset: 0x0015554C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LocalDeclarationStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.usingKeyword, this.modifiers, this.declaration, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x00157390 File Offset: 0x00155590
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LocalDeclarationStatementSyntax(base.Kind, this.attributeLists, this.awaitKeyword, this.usingKeyword, this.modifiers, this.declaration, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x001573D4 File Offset: 0x001555D4
		internal LocalDeclarationStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.awaitKeyword = syntaxToken;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.usingKeyword = syntaxToken2;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDeclarationSyntax);
			this.declaration = variableDeclarationSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.semicolonToken = syntaxToken3;
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001574A0 File Offset: 0x001556A0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.awaitKeyword);
			writer.WriteValue(this.usingKeyword);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001574FC File Offset: 0x001556FC
		static LocalDeclarationStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LocalDeclarationStatementSyntax), (ObjectReader r) => new LocalDeclarationStatementSyntax(r));
		}

		// Token: 0x04001701 RID: 5889
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001702 RID: 5890
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken awaitKeyword;

		// Token: 0x04001703 RID: 5891
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken usingKeyword;

		// Token: 0x04001704 RID: 5892
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001705 RID: 5893
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x04001706 RID: 5894
		internal readonly SyntaxToken semicolonToken;
	}
}
