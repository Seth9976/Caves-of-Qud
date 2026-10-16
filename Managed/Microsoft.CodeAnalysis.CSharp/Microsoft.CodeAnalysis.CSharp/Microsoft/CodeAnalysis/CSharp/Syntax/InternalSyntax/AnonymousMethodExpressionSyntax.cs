using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B1 RID: 945
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AnonymousMethodExpressionSyntax : AnonymousFunctionExpressionSyntax
	{
		// Token: 0x060053CC RID: 21452 RVA: 0x0014DF40 File Offset: 0x0014C140
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal AnonymousMethodExpressionSyntax(SyntaxKind kind, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken delegateKeyword, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(1)] BlockSyntax block, ExpressionSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0014DFB8 File Offset: 0x0014C1B8
		internal AnonymousMethodExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken delegateKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0014E034 File Offset: 0x0014C234
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal AnonymousMethodExpressionSyntax(SyntaxKind kind, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken delegateKeyword, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(1)] BlockSyntax block, ExpressionSyntax expressionBody)
			: base(kind)
		{
			base.SlotCount = 5;
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			if (parameterList != null)
			{
				base.AdjustFlagsAndWidth(parameterList);
				this.parameterList = parameterList;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x0014E0A6 File Offset: 0x0014C2A6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x0014E0B3 File Offset: 0x0014C2B3
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return this.delegateKeyword;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x0014E0BB File Offset: 0x0014C2BB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x0014E0C3 File Offset: 0x0014C2C3
		public override BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x0014E0CB File Offset: 0x0014C2CB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExpressionSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0014E0D4 File Offset: 0x0014C2D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.modifiers;
				break;
			case 1:
				greenNode = this.delegateKeyword;
				break;
			case 2:
				greenNode = this.parameterList;
				break;
			case 3:
				greenNode = this.block;
				break;
			case 4:
				greenNode = this.expressionBody;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0014E12D File Offset: 0x0014C32D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AnonymousMethodExpressionSyntax(this, parent, position);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x0014E137 File Offset: 0x0014C337
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAnonymousMethodExpression(this);
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0014E140 File Offset: 0x0014C340
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAnonymousMethodExpression(this);
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0014E14C File Offset: 0x0014C34C
		public AnonymousMethodExpressionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, ParameterListSyntax parameterList, BlockSyntax block, ExpressionSyntax expressionBody)
		{
			if (modifiers != this.Modifiers || delegateKeyword != this.DelegateKeyword || parameterList != this.ParameterList || block != this.Block || expressionBody != this.ExpressionBody)
			{
				AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax = SyntaxFactory.AnonymousMethodExpression(modifiers, delegateKeyword, parameterList, block, expressionBody);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					anonymousMethodExpressionSyntax = anonymousMethodExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					anonymousMethodExpressionSyntax = anonymousMethodExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return anonymousMethodExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0014E1C9 File Offset: 0x0014C3C9
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AnonymousMethodExpressionSyntax(base.Kind, this.modifiers, this.delegateKeyword, this.parameterList, this.block, this.expressionBody, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x0014E1FB File Offset: 0x0014C3FB
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AnonymousMethodExpressionSyntax(base.Kind, this.modifiers, this.delegateKeyword, this.parameterList, this.block, this.expressionBody, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0014E230 File Offset: 0x0014C430
		internal AnonymousMethodExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.modifiers = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.delegateKeyword = syntaxToken;
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			if (parameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(parameterListSyntax);
				this.parameterList = parameterListSyntax;
			}
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			if (expressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(expressionSyntax);
				this.expressionBody = expressionSyntax;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0014E2DC File Offset: 0x0014C4DC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.delegateKeyword);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.block);
			writer.WriteValue(this.expressionBody);
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0014E32C File Offset: 0x0014C52C
		static AnonymousMethodExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AnonymousMethodExpressionSyntax), (ObjectReader r) => new AnonymousMethodExpressionSyntax(r));
		}

		// Token: 0x04001660 RID: 5728
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001661 RID: 5729
		internal readonly SyntaxToken delegateKeyword;

		// Token: 0x04001662 RID: 5730
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001663 RID: 5731
		internal readonly BlockSyntax block;

		// Token: 0x04001664 RID: 5732
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExpressionSyntax expressionBody;
	}
}
