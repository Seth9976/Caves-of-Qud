using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A6 RID: 934
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SizeOfExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005334 RID: 21300 RVA: 0x0014C7AC File Offset: 0x0014A9AC
		internal SizeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0014C808 File Offset: 0x0014AA08
		internal SizeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0014C868 File Offset: 0x0014AA68
		internal SizeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x0014C8BF File Offset: 0x0014AABF
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x0014C8C7 File Offset: 0x0014AAC7
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x0014C8CF File Offset: 0x0014AACF
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x0014C8D7 File Offset: 0x0014AAD7
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0014C8E0 File Offset: 0x0014AAE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.keyword;
				break;
			case 1:
				greenNode = this.openParenToken;
				break;
			case 2:
				greenNode = this.type;
				break;
			case 3:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0014C92C File Offset: 0x0014AB2C
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SizeOfExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0014C936 File Offset: 0x0014AB36
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSizeOfExpression(this);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0014C93F File Offset: 0x0014AB3F
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSizeOfExpression(this);
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0014C948 File Offset: 0x0014AB48
		public SizeOfExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || type != this.Type || closeParenToken != this.CloseParenToken)
			{
				SizeOfExpressionSyntax sizeOfExpressionSyntax = SyntaxFactory.SizeOfExpression(keyword, openParenToken, type, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					sizeOfExpressionSyntax = sizeOfExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					sizeOfExpressionSyntax = sizeOfExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return sizeOfExpressionSyntax;
			}
			return this;
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0014C9B4 File Offset: 0x0014ABB4
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SizeOfExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0014C9E0 File Offset: 0x0014ABE0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SizeOfExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0014CA0C File Offset: 0x0014AC0C
		internal SizeOfExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0014CA8F File Offset: 0x0014AC8F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0014CAC8 File Offset: 0x0014ACC8
		static SizeOfExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SizeOfExpressionSyntax), (ObjectReader r) => new SizeOfExpressionSyntax(r));
		}

		// Token: 0x04001647 RID: 5703
		internal readonly SyntaxToken keyword;

		// Token: 0x04001648 RID: 5704
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001649 RID: 5705
		internal readonly TypeSyntax type;

		// Token: 0x0400164A RID: 5706
		internal readonly SyntaxToken closeParenToken;
	}
}
