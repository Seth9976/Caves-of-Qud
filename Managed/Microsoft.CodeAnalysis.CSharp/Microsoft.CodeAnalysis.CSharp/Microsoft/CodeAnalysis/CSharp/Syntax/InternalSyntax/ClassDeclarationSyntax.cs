using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000425 RID: 1061
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ClassDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x06005AC9 RID: 23241 RVA: 0x001629C4 File Offset: 0x00160BC4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ClassDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005ACA RID: 23242 RVA: 0x00162AAC File Offset: 0x00160CAC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ClassDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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

		// Token: 0x06005ACB RID: 23243 RVA: 0x00162B98 File Offset: 0x00160D98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ClassDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
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

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06005ACC RID: 23244 RVA: 0x00162C7A File Offset: 0x00160E7A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06005ACD RID: 23245 RVA: 0x00162C87 File Offset: 0x00160E87
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06005ACE RID: 23246 RVA: 0x00162C94 File Offset: 0x00160E94
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x00162C9C File Offset: 0x00160E9C
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x00162CA4 File Offset: 0x00160EA4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x00162CAC File Offset: 0x00160EAC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.baseList;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x00162CB4 File Offset: 0x00160EB4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x00162CC1 File Offset: 0x00160EC1
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x00162CC9 File Offset: 0x00160EC9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x00162CD6 File Offset: 0x00160ED6
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x00162CDE File Offset: 0x00160EDE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x00162CE8 File Offset: 0x00160EE8
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

		// Token: 0x06005AD8 RID: 23256 RVA: 0x00162D8F File Offset: 0x00160F8F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ClassDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x00162D99 File Offset: 0x00160F99
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitClassDeclaration(this);
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x00162DA2 File Offset: 0x00160FA2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitClassDeclaration(this);
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x00162DAC File Offset: 0x00160FAC
		public ClassDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || keyword != this.Keyword || identifier != this.Identifier || typeParameterList != this.TypeParameterList || baseList != this.BaseList || constraintClauses != this.ConstraintClauses || openBraceToken != this.OpenBraceToken || members != this.Members || closeBraceToken != this.CloseBraceToken || semicolonToken != this.SemicolonToken)
			{
				ClassDeclarationSyntax classDeclarationSyntax = SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					classDeclarationSyntax = classDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					classDeclarationSyntax = classDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return classDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x00162E80 File Offset: 0x00161080
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ClassDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x00162EE4 File Offset: 0x001610E4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ClassDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x00162F48 File Offset: 0x00161148
		internal ClassDeclarationSyntax(ObjectReader reader)
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

		// Token: 0x06005ADF RID: 23263 RVA: 0x001630B4 File Offset: 0x001612B4
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

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0016314C File Offset: 0x0016134C
		static ClassDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ClassDeclarationSyntax), (ObjectReader r) => new ClassDeclarationSyntax(r));
		}

		// Token: 0x040017E1 RID: 6113
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017E2 RID: 6114
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040017E3 RID: 6115
		internal readonly SyntaxToken keyword;

		// Token: 0x040017E4 RID: 6116
		internal readonly SyntaxToken identifier;

		// Token: 0x040017E5 RID: 6117
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x040017E6 RID: 6118
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BaseListSyntax baseList;

		// Token: 0x040017E7 RID: 6119
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode constraintClauses;

		// Token: 0x040017E8 RID: 6120
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040017E9 RID: 6121
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x040017EA RID: 6122
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x040017EB RID: 6123
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
