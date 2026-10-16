using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B3 RID: 947
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SimpleLambdaExpressionSyntax : LambdaExpressionSyntax
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x0014E36C File Offset: 0x0014C56C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SimpleLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(parameter);
			this.parameter = parameter;
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

		// Token: 0x060053E4 RID: 21476 RVA: 0x0014E3F8 File Offset: 0x0014C5F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SimpleLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(parameter);
			this.parameter = parameter;
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

		// Token: 0x060053E5 RID: 21477 RVA: 0x0014E488 File Offset: 0x0014C688
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SimpleLambdaExpressionSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
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
			base.AdjustFlagsAndWidth(parameter);
			this.parameter = parameter;
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

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x0014E50D File Offset: 0x0014C70D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0014E51A File Offset: 0x0014C71A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x0014E527 File Offset: 0x0014C727
		public ParameterSyntax Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x0014E52F File Offset: 0x0014C72F
		public override SyntaxToken ArrowToken
		{
			get
			{
				return this.arrowToken;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x0014E537 File Offset: 0x0014C737
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Block
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.block;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x0014E53F File Offset: 0x0014C73F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0014E548 File Offset: 0x0014C748
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
				greenNode = this.parameter;
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

		// Token: 0x060053ED RID: 21485 RVA: 0x0014E5AE File Offset: 0x0014C7AE
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SimpleLambdaExpressionSyntax(this, parent, position);
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0014E5B8 File Offset: 0x0014C7B8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSimpleLambdaExpression(this);
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x0014E5C1 File Offset: 0x0014C7C1
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSimpleLambdaExpression(this);
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0014E5CC File Offset: 0x0014C7CC
		public SimpleLambdaExpressionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, BlockSyntax block, ExpressionSyntax expressionBody)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || parameter != this.Parameter || arrowToken != this.ArrowToken || block != this.Block || expressionBody != this.ExpressionBody)
			{
				SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax = SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, arrowToken, block, expressionBody);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					simpleLambdaExpressionSyntax = simpleLambdaExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					simpleLambdaExpressionSyntax = simpleLambdaExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return simpleLambdaExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0014E65C File Offset: 0x0014C85C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SimpleLambdaExpressionSyntax(base.Kind, this.attributeLists, this.modifiers, this.parameter, this.arrowToken, this.block, this.expressionBody, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x0014E6A0 File Offset: 0x0014C8A0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SimpleLambdaExpressionSyntax(base.Kind, this.attributeLists, this.modifiers, this.parameter, this.arrowToken, this.block, this.expressionBody, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x0014E6E4 File Offset: 0x0014C8E4
		internal SimpleLambdaExpressionSyntax(ObjectReader reader)
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
			ParameterSyntax parameterSyntax = (ParameterSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterSyntax);
			this.parameter = parameterSyntax;
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

		// Token: 0x060053F4 RID: 21492 RVA: 0x0014E7B0 File Offset: 0x0014C9B0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.parameter);
			writer.WriteValue(this.arrowToken);
			writer.WriteValue(this.block);
			writer.WriteValue(this.expressionBody);
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x0014E80C File Offset: 0x0014CA0C
		static SimpleLambdaExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SimpleLambdaExpressionSyntax), (ObjectReader r) => new SimpleLambdaExpressionSyntax(r));
		}

		// Token: 0x04001665 RID: 5733
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001666 RID: 5734
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001667 RID: 5735
		internal readonly ParameterSyntax parameter;

		// Token: 0x04001668 RID: 5736
		internal readonly SyntaxToken arrowToken;

		// Token: 0x04001669 RID: 5737
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BlockSyntax block;

		// Token: 0x0400166A RID: 5738
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expressionBody;
	}
}
