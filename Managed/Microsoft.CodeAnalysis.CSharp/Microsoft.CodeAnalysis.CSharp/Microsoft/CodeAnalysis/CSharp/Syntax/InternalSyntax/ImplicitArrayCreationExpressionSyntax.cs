using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BE RID: 958
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ImplicitArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600548F RID: 21647 RVA: 0x00150304 File Offset: 0x0014E504
		internal ImplicitArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (commas != null)
			{
				base.AdjustFlagsAndWidth(commas);
				this.commas = commas;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x00150374 File Offset: 0x0014E574
		internal ImplicitArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (commas != null)
			{
				base.AdjustFlagsAndWidth(commas);
				this.commas = commas;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x001503E8 File Offset: 0x0014E5E8
		internal ImplicitArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (commas != null)
			{
				base.AdjustFlagsAndWidth(commas);
				this.commas = commas;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
			base.AdjustFlagsAndWidth(initializer);
			this.initializer = initializer;
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x00150453 File Offset: 0x0014E653
		public SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x0015045B File Offset: 0x0014E65B
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06005494 RID: 21652 RVA: 0x00150463 File Offset: 0x0014E663
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> Commas
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.commas);
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00150470 File Offset: 0x0014E670
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x00150478 File Offset: 0x0014E678
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x00150480 File Offset: 0x0014E680
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.newKeyword;
				break;
			case 1:
				greenNode = this.openBracketToken;
				break;
			case 2:
				greenNode = this.commas;
				break;
			case 3:
				greenNode = this.closeBracketToken;
				break;
			case 4:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x001504D9 File Offset: 0x0014E6D9
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ImplicitArrayCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x001504E3 File Offset: 0x0014E6E3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitArrayCreationExpression(this);
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x001504EC File Offset: 0x0014E6EC
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitArrayCreationExpression(this);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x001504F8 File Offset: 0x0014E6F8
		public ImplicitArrayCreationExpressionSyntax Update(SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			if (newKeyword != this.NewKeyword || openBracketToken != this.OpenBracketToken || commas != this.Commas || closeBracketToken != this.CloseBracketToken || initializer != this.Initializer)
			{
				ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpressionSyntax = SyntaxFactory.ImplicitArrayCreationExpression(newKeyword, openBracketToken, commas, closeBracketToken, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					implicitArrayCreationExpressionSyntax = implicitArrayCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					implicitArrayCreationExpressionSyntax = implicitArrayCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return implicitArrayCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00150575 File Offset: 0x0014E775
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ImplicitArrayCreationExpressionSyntax(base.Kind, this.newKeyword, this.openBracketToken, this.commas, this.closeBracketToken, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x001505A7 File Offset: 0x0014E7A7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ImplicitArrayCreationExpressionSyntax(base.Kind, this.newKeyword, this.openBracketToken, this.commas, this.closeBracketToken, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x001505DC File Offset: 0x0014E7DC
		internal ImplicitArrayCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openBracketToken = syntaxToken2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.commas = greenNode;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeBracketToken = syntaxToken3;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(initializerExpressionSyntax);
			this.initializer = initializerExpressionSyntax;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x00150680 File Offset: 0x0014E880
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.commas);
			writer.WriteValue(this.closeBracketToken);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x001506D0 File Offset: 0x0014E8D0
		static ImplicitArrayCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ImplicitArrayCreationExpressionSyntax), (ObjectReader r) => new ImplicitArrayCreationExpressionSyntax(r));
		}

		// Token: 0x04001689 RID: 5769
		internal readonly SyntaxToken newKeyword;

		// Token: 0x0400168A RID: 5770
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x0400168B RID: 5771
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode commas;

		// Token: 0x0400168C RID: 5772
		internal readonly SyntaxToken closeBracketToken;

		// Token: 0x0400168D RID: 5773
		internal readonly InitializerExpressionSyntax initializer;
	}
}
