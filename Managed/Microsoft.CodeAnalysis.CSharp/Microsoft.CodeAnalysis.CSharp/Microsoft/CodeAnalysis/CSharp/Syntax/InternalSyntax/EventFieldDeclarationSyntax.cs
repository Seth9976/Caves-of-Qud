using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000438 RID: 1080
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EventFieldDeclarationSyntax : BaseFieldDeclarationSyntax
	{
		// Token: 0x06005BFD RID: 23549 RVA: 0x00166EF8 File Offset: 0x001650F8
		internal EventFieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00166F6C File Offset: 0x0016516C
		internal EventFieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x00166FE4 File Offset: 0x001651E4
		internal EventFieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00167051 File Offset: 0x00165251
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x0016705E File Offset: 0x0016525E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06005C02 RID: 23554 RVA: 0x0016706B File Offset: 0x0016526B
		public SyntaxToken EventKeyword
		{
			get
			{
				return this.eventKeyword;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x00167073 File Offset: 0x00165273
		public override VariableDeclarationSyntax Declaration
		{
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06005C04 RID: 23556 RVA: 0x0016707B File Offset: 0x0016527B
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00167084 File Offset: 0x00165284
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
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.eventKeyword;
				break;
			case 3:
				greenNode = this.declaration;
				break;
			case 4:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x001670DD File Offset: 0x001652DD
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EventFieldDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x001670E7 File Offset: 0x001652E7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEventFieldDeclaration(this);
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x001670F0 File Offset: 0x001652F0
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEventFieldDeclaration(this);
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x001670FC File Offset: 0x001652FC
		public EventFieldDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || eventKeyword != this.EventKeyword || declaration != this.Declaration || semicolonToken != this.SemicolonToken)
			{
				EventFieldDeclarationSyntax eventFieldDeclarationSyntax = SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, eventKeyword, declaration, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					eventFieldDeclarationSyntax = eventFieldDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					eventFieldDeclarationSyntax = eventFieldDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return eventFieldDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x0016717E File Offset: 0x0016537E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EventFieldDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.eventKeyword, this.declaration, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x001671B0 File Offset: 0x001653B0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EventFieldDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.eventKeyword, this.declaration, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x001671E4 File Offset: 0x001653E4
		internal EventFieldDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.eventKeyword = syntaxToken;
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDeclarationSyntax);
			this.declaration = variableDeclarationSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.semicolonToken = syntaxToken2;
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x0016728C File Offset: 0x0016548C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.eventKeyword);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x001672DC File Offset: 0x001654DC
		static EventFieldDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EventFieldDeclarationSyntax), (ObjectReader r) => new EventFieldDeclarationSyntax(r));
		}

		// Token: 0x04001839 RID: 6201
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400183A RID: 6202
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400183B RID: 6203
		internal readonly SyntaxToken eventKeyword;

		// Token: 0x0400183C RID: 6204
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x0400183D RID: 6205
		internal readonly SyntaxToken semicolonToken;
	}
}
