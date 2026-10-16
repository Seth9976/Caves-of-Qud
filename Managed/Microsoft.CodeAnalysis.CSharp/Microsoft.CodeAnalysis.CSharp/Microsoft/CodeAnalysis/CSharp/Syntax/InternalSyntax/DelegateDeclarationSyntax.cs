using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042A RID: 1066
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DelegateDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005B41 RID: 23361 RVA: 0x00165050 File Offset: 0x00163250
		internal DelegateDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraintClauses, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005B42 RID: 23362 RVA: 0x0016510C File Offset: 0x0016330C
		internal DelegateDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraintClauses, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x001651CC File Offset: 0x001633CC
		internal DelegateDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraintClauses, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x00165282 File Offset: 0x00163482
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06005B45 RID: 23365 RVA: 0x0016528F File Offset: 0x0016348F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0016529C File Offset: 0x0016349C
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return this.delegateKeyword;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x001652A4 File Offset: 0x001634A4
		public TypeSyntax ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x001652AC File Offset: 0x001634AC
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x001652B4 File Offset: 0x001634B4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x001652BC File Offset: 0x001634BC
		public ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x001652C4 File Offset: 0x001634C4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06005B4C RID: 23372 RVA: 0x001652D1 File Offset: 0x001634D1
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x001652DC File Offset: 0x001634DC
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
				greenNode = this.delegateKeyword;
				break;
			case 3:
				greenNode = this.returnType;
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
				greenNode = this.constraintClauses;
				break;
			case 8:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x00165369 File Offset: 0x00163569
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DelegateDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x00165373 File Offset: 0x00163573
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDelegateDeclaration(this);
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0016537C File Offset: 0x0016357C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDelegateDeclaration(this);
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x00165388 File Offset: 0x00163588
		public DelegateDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || delegateKeyword != this.DelegateKeyword || returnType != this.ReturnType || identifier != this.Identifier || typeParameterList != this.TypeParameterList || parameterList != this.ParameterList || constraintClauses != this.ConstraintClauses || semicolonToken != this.SemicolonToken)
			{
				DelegateDeclarationSyntax delegateDeclarationSyntax = SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					delegateDeclarationSyntax = delegateDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					delegateDeclarationSyntax = delegateDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return delegateDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00165440 File Offset: 0x00163640
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DelegateDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.delegateKeyword, this.returnType, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x00165498 File Offset: 0x00163698
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DelegateDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.delegateKeyword, this.returnType, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x001654F0 File Offset: 0x001636F0
		internal DelegateDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 9;
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
			this.delegateKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.returnType = typeSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)reader.ReadValue();
			if (typeParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeParameterListSyntax);
				this.typeParameterList = typeParameterListSyntax;
			}
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterListSyntax);
			this.parameterList = parameterListSyntax;
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.constraintClauses = greenNode3;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.semicolonToken = syntaxToken3;
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x00165614 File Offset: 0x00163814
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.delegateKeyword);
			writer.WriteValue(this.returnType);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00165694 File Offset: 0x00163894
		static DelegateDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DelegateDeclarationSyntax), (ObjectReader r) => new DelegateDeclarationSyntax(r));
		}

		// Token: 0x04001818 RID: 6168
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001819 RID: 6169
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400181A RID: 6170
		internal readonly SyntaxToken delegateKeyword;

		// Token: 0x0400181B RID: 6171
		internal readonly TypeSyntax returnType;

		// Token: 0x0400181C RID: 6172
		internal readonly SyntaxToken identifier;

		// Token: 0x0400181D RID: 6173
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x0400181E RID: 6174
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x0400181F RID: 6175
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode constraintClauses;

		// Token: 0x04001820 RID: 6176
		internal readonly SyntaxToken semicolonToken;
	}
}
