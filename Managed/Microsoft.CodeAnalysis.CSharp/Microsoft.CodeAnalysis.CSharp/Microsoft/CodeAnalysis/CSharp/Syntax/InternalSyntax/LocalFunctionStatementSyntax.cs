using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003EA RID: 1002
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LocalFunctionStatementSyntax : StatementSyntax
	{
		// Token: 0x06005708 RID: 22280 RVA: 0x00156944 File Offset: 0x00154B44
		internal LocalFunctionStatementSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 10;
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
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x00156A1C File Offset: 0x00154C1C
		internal LocalFunctionStatementSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 10;
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
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x00156AF8 File Offset: 0x00154CF8
		internal LocalFunctionStatementSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 10;
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
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x00156BCA File Offset: 0x00154DCA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600570C RID: 22284 RVA: 0x00156BD7 File Offset: 0x00154DD7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x0600570D RID: 22285 RVA: 0x00156BE4 File Offset: 0x00154DE4
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public TypeSyntax ReturnType
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600570E RID: 22286 RVA: 0x00156BEC File Offset: 0x00154DEC
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x0600570F RID: 22287 RVA: 0x00156BF4 File Offset: 0x00154DF4
		public TypeParameterListSyntax TypeParameterList
		{
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06005710 RID: 22288 RVA: 0x00156BFC File Offset: 0x00154DFC
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x00156C04 File Offset: 0x00154E04
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06005712 RID: 22290 RVA: 0x00156C11 File Offset: 0x00154E11
		public BlockSyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x00156C19 File Offset: 0x00154E19
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x00156C21 File Offset: 0x00154E21
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x00156C2C File Offset: 0x00154E2C
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
				greenNode = this.returnType;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.typeParameterList;
				break;
			case 5:
				greenNode = this.parameterList;
				break;
			case 6:
				greenNode = this.constraintClauses;
				break;
			case 7:
				greenNode = this.body;
				break;
			case 8:
				greenNode = this.expressionBody;
				break;
			case 9:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00156CC6 File Offset: 0x00154EC6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LocalFunctionStatementSyntax(this, parent, position);
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x00156CD0 File Offset: 0x00154ED0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLocalFunctionStatement(this);
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00156CD9 File Offset: 0x00154ED9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLocalFunctionStatement(this);
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x00156CE4 File Offset: 0x00154EE4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public LocalFunctionStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || returnType != this.ReturnType || identifier != this.Identifier || typeParameterList != this.TypeParameterList || parameterList != this.ParameterList || constraintClauses != this.ConstraintClauses || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				LocalFunctionStatementSyntax localFunctionStatementSyntax = SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					localFunctionStatementSyntax = localFunctionStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					localFunctionStatementSyntax = localFunctionStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return localFunctionStatementSyntax;
			}
			return this;
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x00156DA8 File Offset: 0x00154FA8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LocalFunctionStatementSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x00156E04 File Offset: 0x00155004
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LocalFunctionStatementSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x00156E60 File Offset: 0x00155060
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal LocalFunctionStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 10;
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
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.returnType = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
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
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			if (blockSyntax != null)
			{
				base.AdjustFlagsAndWidth(blockSyntax);
				this.body = blockSyntax;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = (ArrowExpressionClauseSyntax)reader.ReadValue();
			if (arrowExpressionClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(arrowExpressionClauseSyntax);
				this.expressionBody = arrowExpressionClauseSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.semicolonToken = syntaxToken2;
			}
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x00156FAC File Offset: 0x001551AC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.returnType);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x00157038 File Offset: 0x00155238
		static LocalFunctionStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LocalFunctionStatementSyntax), (ObjectReader r) => new LocalFunctionStatementSyntax(r));
		}

		// Token: 0x040016F7 RID: 5879
		internal readonly GreenNode attributeLists;

		// Token: 0x040016F8 RID: 5880
		internal readonly GreenNode modifiers;

		// Token: 0x040016F9 RID: 5881
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly TypeSyntax returnType;

		// Token: 0x040016FA RID: 5882
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x040016FB RID: 5883
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x040016FC RID: 5884
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x040016FD RID: 5885
		internal readonly GreenNode constraintClauses;

		// Token: 0x040016FE RID: 5886
		internal readonly BlockSyntax body;

		// Token: 0x040016FF RID: 5887
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x04001700 RID: 5888
		internal readonly SyntaxToken semicolonToken;
	}
}
