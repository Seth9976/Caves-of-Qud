using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000426 RID: 1062
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class StructDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x06005AE1 RID: 23265 RVA: 0x00163170 File Offset: 0x00161370
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal StructDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x00163258 File Offset: 0x00161458
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal StructDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x00163344 File Offset: 0x00161544
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal StructDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x00163426 File Offset: 0x00161626
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x00163433 File Offset: 0x00161633
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x00163440 File Offset: 0x00161640
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x00163448 File Offset: 0x00161648
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x00163450 File Offset: 0x00161650
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x00163458 File Offset: 0x00161658
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.baseList;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06005AEA RID: 23274 RVA: 0x00163460 File Offset: 0x00161660
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06005AEB RID: 23275 RVA: 0x0016346D File Offset: 0x0016166D
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x00163475 File Offset: 0x00161675
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x00163482 File Offset: 0x00161682
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0016348A File Offset: 0x0016168A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x00163494 File Offset: 0x00161694
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
				greenNode = this.keyword;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.typeParameterList;
				break;
			case 5:
				greenNode = this.baseList;
				break;
			case 6:
				greenNode = this.constraintClauses;
				break;
			case 7:
				greenNode = this.openBraceToken;
				break;
			case 8:
				greenNode = this.members;
				break;
			case 9:
				greenNode = this.closeBraceToken;
				break;
			case 10:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0016353B File Offset: 0x0016173B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new StructDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x00163545 File Offset: 0x00161745
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitStructDeclaration(this);
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x0016354E File Offset: 0x0016174E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitStructDeclaration(this);
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x00163558 File Offset: 0x00161758
		public StructDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || keyword != this.Keyword || identifier != this.Identifier || typeParameterList != this.TypeParameterList || baseList != this.BaseList || constraintClauses != this.ConstraintClauses || openBraceToken != this.OpenBraceToken || members != this.Members || closeBraceToken != this.CloseBraceToken || semicolonToken != this.SemicolonToken)
			{
				StructDeclarationSyntax structDeclarationSyntax = SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					structDeclarationSyntax = structDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					structDeclarationSyntax = structDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return structDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0016362C File Offset: 0x0016182C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new StructDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x00163690 File Offset: 0x00161890
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new StructDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x001636F4 File Offset: 0x001618F4
		internal StructDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 11;
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
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)reader.ReadValue();
			if (typeParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeParameterListSyntax);
				this.typeParameterList = typeParameterListSyntax;
			}
			BaseListSyntax baseListSyntax = (BaseListSyntax)reader.ReadValue();
			if (baseListSyntax != null)
			{
				base.AdjustFlagsAndWidth(baseListSyntax);
				this.baseList = baseListSyntax;
			}
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.constraintClauses = greenNode3;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openBraceToken = syntaxToken3;
			GreenNode greenNode4 = (GreenNode)reader.ReadValue();
			if (greenNode4 != null)
			{
				base.AdjustFlagsAndWidth(greenNode4);
				this.members = greenNode4;
			}
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.closeBraceToken = syntaxToken4;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken5 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken5);
				this.semicolonToken = syntaxToken5;
			}
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x00163860 File Offset: 0x00161A60
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.baseList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.members);
			writer.WriteValue(this.closeBraceToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x001638F8 File Offset: 0x00161AF8
		static StructDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(StructDeclarationSyntax), (ObjectReader r) => new StructDeclarationSyntax(r));
		}

		// Token: 0x040017EC RID: 6124
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017ED RID: 6125
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040017EE RID: 6126
		internal readonly SyntaxToken keyword;

		// Token: 0x040017EF RID: 6127
		internal readonly SyntaxToken identifier;

		// Token: 0x040017F0 RID: 6128
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x040017F1 RID: 6129
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BaseListSyntax baseList;

		// Token: 0x040017F2 RID: 6130
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode constraintClauses;

		// Token: 0x040017F3 RID: 6131
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040017F4 RID: 6132
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x040017F5 RID: 6133
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x040017F6 RID: 6134
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
