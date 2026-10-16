using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B5 RID: 949
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParenthesizedLambdaExpressionSyntax : LambdaExpressionSyntax
	{
		// Token: 0x06005405 RID: 21509 RVA: 0x0014EA44 File Offset: 0x0014CC44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ParenthesizedLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			if (block != null)
			{
				base.AdjustFlagsAndWidth(block);
				this.block = block;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0014EAD0 File Offset: 0x0014CCD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ParenthesizedLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			if (block != null)
			{
				base.AdjustFlagsAndWidth(block);
				this.block = block;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0014EB60 File Offset: 0x0014CD60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ParenthesizedLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
			: base(kind)
		{
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			base.AdjustFlagsAndWidth(arrowToken);
			this.arrowToken = arrowToken;
			if (block != null)
			{
				base.AdjustFlagsAndWidth(block);
				this.block = block;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x0014EBE5 File Offset: 0x0014CDE5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x0014EBF2 File Offset: 0x0014CDF2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x0014EBFF File Offset: 0x0014CDFF
		public ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x0600540B RID: 21515 RVA: 0x0014EC07 File Offset: 0x0014CE07
		public override SyntaxToken ArrowToken
		{
			get
			{
				return this.arrowToken;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x0014EC0F File Offset: 0x0014CE0F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Block
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.block;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x0014EC17 File Offset: 0x0014CE17
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0014EC20 File Offset: 0x0014CE20
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
				greenNode = this.parameterList;
				break;
			case 3:
				greenNode = this.arrowToken;
				break;
			case 4:
				greenNode = this.block;
				break;
			case 5:
				greenNode = this.expressionBody;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0014EC86 File Offset: 0x0014CE86
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParenthesizedLambdaExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0014EC90 File Offset: 0x0014CE90
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedLambdaExpression(this);
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x0014EC99 File Offset: 0x0014CE99
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedLambdaExpression(this);
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x0014ECA4 File Offset: 0x0014CEA4
		public ParenthesizedLambdaExpressionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || parameterList != this.ParameterList || arrowToken != this.ArrowToken || block != this.Block || expressionBody != this.ExpressionBody)
			{
				ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax = SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, parameterList, arrowToken, block, expressionBody);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					parenthesizedLambdaExpressionSyntax = parenthesizedLambdaExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					parenthesizedLambdaExpressionSyntax = parenthesizedLambdaExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return parenthesizedLambdaExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x0014ED34 File Offset: 0x0014CF34
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParenthesizedLambdaExpressionSyntax(base.Kind, this.attributeLists, this.modifiers, this.parameterList, this.arrowToken, this.block, this.expressionBody, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0014ED78 File Offset: 0x0014CF78
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParenthesizedLambdaExpressionSyntax(base.Kind, this.attributeLists, this.modifiers, this.parameterList, this.arrowToken, this.block, this.expressionBody, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0014EDBC File Offset: 0x0014CFBC
		internal ParenthesizedLambdaExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
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
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterListSyntax);
			this.parameterList = parameterListSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.arrowToken = syntaxToken;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			if (blockSyntax != null)
			{
				base.AdjustFlagsAndWidth(blockSyntax);
				this.block = blockSyntax;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expressionBody = expressionSyntax;
			}
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0014EE88 File Offset: 0x0014D088
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.arrowToken);
			writer.WriteValue(this.block);
			writer.WriteValue(this.expressionBody);
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0014EEE4 File Offset: 0x0014D0E4
		static ParenthesizedLambdaExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParenthesizedLambdaExpressionSyntax), (ObjectReader r) => new ParenthesizedLambdaExpressionSyntax(r));
		}

		// Token: 0x0400166D RID: 5741
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400166E RID: 5742
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400166F RID: 5743
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001670 RID: 5744
		internal readonly SyntaxToken arrowToken;

		// Token: 0x04001671 RID: 5745
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BlockSyntax block;

		// Token: 0x04001672 RID: 5746
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expressionBody;
	}
}
