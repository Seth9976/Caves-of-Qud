using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A2 RID: 930
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RefValueExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060052EE RID: 21230 RVA: 0x0014B970 File Offset: 0x00149B70
		internal RefValueExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(comma);
			this.comma = comma;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0014B9EC File Offset: 0x00149BEC
		internal RefValueExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(comma);
			this.comma = comma;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0014BA6C File Offset: 0x00149C6C
		internal RefValueExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 6;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(comma);
			this.comma = comma;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x060052F1 RID: 21233 RVA: 0x0014BAE3 File Offset: 0x00149CE3
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x0014BAEB File Offset: 0x00149CEB
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x0014BAF3 File Offset: 0x00149CF3
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x0014BAFB File Offset: 0x00149CFB
		public SyntaxToken Comma
		{
			get
			{
				return this.comma;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x060052F5 RID: 21237 RVA: 0x0014BB03 File Offset: 0x00149D03
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x0014BB0B File Offset: 0x00149D0B
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0014BB14 File Offset: 0x00149D14
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
				greenNode = this.expression;
				break;
			case 3:
				greenNode = this.comma;
				break;
			case 4:
				greenNode = this.type;
				break;
			case 5:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0014BB7A File Offset: 0x00149D7A
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RefValueExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0014BB84 File Offset: 0x00149D84
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefValueExpression(this);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0014BB8D File Offset: 0x00149D8D
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefValueExpression(this);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0014BB98 File Offset: 0x00149D98
		public RefValueExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || expression != this.Expression || comma != this.Comma || type != this.Type || closeParenToken != this.CloseParenToken)
			{
				RefValueExpressionSyntax refValueExpressionSyntax = SyntaxFactory.RefValueExpression(keyword, openParenToken, expression, comma, type, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					refValueExpressionSyntax = refValueExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					refValueExpressionSyntax = refValueExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return refValueExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0014BC1C File Offset: 0x00149E1C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RefValueExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.comma, this.type, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x0014BC60 File Offset: 0x00149E60
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RefValueExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.expression, this.comma, this.type, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0014BCA4 File Offset: 0x00149EA4
		internal RefValueExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openParenToken = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.comma = syntaxToken3;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.closeParenToken = syntaxToken4;
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0014BD64 File Offset: 0x00149F64
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.comma);
			writer.WriteValue(this.type);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0014BDC0 File Offset: 0x00149FC0
		static RefValueExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RefValueExpressionSyntax), (ObjectReader r) => new RefValueExpressionSyntax(r));
		}

		// Token: 0x04001635 RID: 5685
		internal readonly SyntaxToken keyword;

		// Token: 0x04001636 RID: 5686
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001637 RID: 5687
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001638 RID: 5688
		internal readonly SyntaxToken comma;

		// Token: 0x04001639 RID: 5689
		internal readonly TypeSyntax type;

		// Token: 0x0400163A RID: 5690
		internal readonly SyntaxToken closeParenToken;
	}
}
