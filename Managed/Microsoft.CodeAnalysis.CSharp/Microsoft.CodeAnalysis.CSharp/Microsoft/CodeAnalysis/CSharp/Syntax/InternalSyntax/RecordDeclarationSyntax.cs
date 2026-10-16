using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000428 RID: 1064
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RecordDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x06005B11 RID: 23313 RVA: 0x001640C8 File Offset: 0x001622C8
		internal RecordDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, GreenNode constraintClauses, SyntaxToken openBraceToken, GreenNode members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 13;
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
			if (classOrStructKeyword != null)
			{
				base.AdjustFlagsAndWidth(classOrStructKeyword);
				this.classOrStructKeyword = classOrStructKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
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
			if (openBraceToken != null)
			{
				base.AdjustFlagsAndWidth(openBraceToken);
				this.openBraceToken = openBraceToken;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			if (closeBraceToken != null)
			{
				base.AdjustFlagsAndWidth(closeBraceToken);
				this.closeBraceToken = closeBraceToken;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x001641E0 File Offset: 0x001623E0
		internal RecordDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, GreenNode constraintClauses, SyntaxToken openBraceToken, GreenNode members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 13;
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
			if (classOrStructKeyword != null)
			{
				base.AdjustFlagsAndWidth(classOrStructKeyword);
				this.classOrStructKeyword = classOrStructKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
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
			if (openBraceToken != null)
			{
				base.AdjustFlagsAndWidth(openBraceToken);
				this.openBraceToken = openBraceToken;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			if (closeBraceToken != null)
			{
				base.AdjustFlagsAndWidth(closeBraceToken);
				this.closeBraceToken = closeBraceToken;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x001642FC File Offset: 0x001624FC
		internal RecordDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, GreenNode constraintClauses, SyntaxToken openBraceToken, GreenNode members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 13;
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
			if (classOrStructKeyword != null)
			{
				base.AdjustFlagsAndWidth(classOrStructKeyword);
				this.classOrStructKeyword = classOrStructKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
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
			if (openBraceToken != null)
			{
				base.AdjustFlagsAndWidth(openBraceToken);
				this.openBraceToken = openBraceToken;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			if (closeBraceToken != null)
			{
				base.AdjustFlagsAndWidth(closeBraceToken);
				this.closeBraceToken = closeBraceToken;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x0016440E File Offset: 0x0016260E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x0016441B File Offset: 0x0016261B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06005B16 RID: 23318 RVA: 0x00164428 File Offset: 0x00162628
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override SyntaxToken Keyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x00164430 File Offset: 0x00162630
		public SyntaxToken ClassOrStructKeyword
		{
			get
			{
				return this.classOrStructKeyword;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x00164438 File Offset: 0x00162638
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x00164440 File Offset: 0x00162640
		public override TypeParameterListSyntax TypeParameterList
		{
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x00164448 File Offset: 0x00162648
		public ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x00164450 File Offset: 0x00162650
		public override BaseListSyntax BaseList
		{
			get
			{
				return this.baseList;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x00164458 File Offset: 0x00162658
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x00164465 File Offset: 0x00162665
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0016446D File Offset: 0x0016266D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0016447A File Offset: 0x0016267A
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06005B20 RID: 23328 RVA: 0x00164482 File Offset: 0x00162682
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x0016448C File Offset: 0x0016268C
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
				greenNode = this.classOrStructKeyword;
				break;
			case 4:
				greenNode = this.identifier;
				break;
			case 5:
				greenNode = this.typeParameterList;
				break;
			case 6:
				greenNode = this.parameterList;
				break;
			case 7:
				greenNode = this.baseList;
				break;
			case 8:
				greenNode = this.constraintClauses;
				break;
			case 9:
				greenNode = this.openBraceToken;
				break;
			case 10:
				greenNode = this.members;
				break;
			case 11:
				greenNode = this.closeBraceToken;
				break;
			case 12:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x0016454D File Offset: 0x0016274D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RecordDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x00164557 File Offset: 0x00162757
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRecordDeclaration(this);
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x00164560 File Offset: 0x00162760
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRecordDeclaration(this);
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0016456C File Offset: 0x0016276C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecordDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken classOrStructKeyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || keyword != this.Keyword || classOrStructKeyword != this.ClassOrStructKeyword || identifier != this.Identifier || typeParameterList != this.TypeParameterList || parameterList != this.ParameterList || baseList != this.BaseList || constraintClauses != this.ConstraintClauses || openBraceToken != this.OpenBraceToken || members != this.Members || closeBraceToken != this.CloseBraceToken || semicolonToken != this.SemicolonToken)
			{
				RecordDeclarationSyntax recordDeclarationSyntax = SyntaxFactory.RecordDeclaration(base.Kind, attributeLists, modifiers, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					recordDeclarationSyntax = recordDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					recordDeclarationSyntax = recordDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return recordDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x00164664 File Offset: 0x00162864
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RecordDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.classOrStructKeyword, this.identifier, this.typeParameterList, this.parameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x001646D4 File Offset: 0x001628D4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RecordDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.classOrStructKeyword, this.identifier, this.typeParameterList, this.parameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00164744 File Offset: 0x00162944
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal RecordDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 13;
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
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.classOrStructKeyword = syntaxToken2;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.identifier = syntaxToken3;
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)reader.ReadValue();
			if (typeParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeParameterListSyntax);
				this.typeParameterList = typeParameterListSyntax;
			}
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			if (parameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(parameterListSyntax);
				this.parameterList = parameterListSyntax;
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
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken4 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken4);
				this.openBraceToken = syntaxToken4;
			}
			GreenNode greenNode4 = (GreenNode)reader.ReadValue();
			if (greenNode4 != null)
			{
				base.AdjustFlagsAndWidth(greenNode4);
				this.members = greenNode4;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken5 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken5);
				this.closeBraceToken = syntaxToken5;
			}
			SyntaxToken syntaxToken6 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken6 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken6);
				this.semicolonToken = syntaxToken6;
			}
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x001648F8 File Offset: 0x00162AF8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.classOrStructKeyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.baseList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.members);
			writer.WriteValue(this.closeBraceToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x001649A8 File Offset: 0x00162BA8
		static RecordDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RecordDeclarationSyntax), (ObjectReader r) => new RecordDeclarationSyntax(r));
		}

		// Token: 0x04001802 RID: 6146
		internal readonly GreenNode attributeLists;

		// Token: 0x04001803 RID: 6147
		internal readonly GreenNode modifiers;

		// Token: 0x04001804 RID: 6148
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken keyword;

		// Token: 0x04001805 RID: 6149
		internal readonly SyntaxToken classOrStructKeyword;

		// Token: 0x04001806 RID: 6150
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x04001807 RID: 6151
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x04001808 RID: 6152
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001809 RID: 6153
		internal readonly BaseListSyntax baseList;

		// Token: 0x0400180A RID: 6154
		internal readonly GreenNode constraintClauses;

		// Token: 0x0400180B RID: 6155
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x0400180C RID: 6156
		internal readonly GreenNode members;

		// Token: 0x0400180D RID: 6157
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x0400180E RID: 6158
		internal readonly SyntaxToken semicolonToken;
	}
}
