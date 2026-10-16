using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A5 RID: 933
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeOfExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005323 RID: 21283 RVA: 0x0014C46C File Offset: 0x0014A66C
		internal TypeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005324 RID: 21284 RVA: 0x0014C4C8 File Offset: 0x0014A6C8
		internal TypeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x06005325 RID: 21285 RVA: 0x0014C528 File Offset: 0x0014A728
		internal TypeOfExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
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

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x0014C57F File Offset: 0x0014A77F
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x0014C587 File Offset: 0x0014A787
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x0014C58F File Offset: 0x0014A78F
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06005329 RID: 21289 RVA: 0x0014C597 File Offset: 0x0014A797
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x0014C5A0 File Offset: 0x0014A7A0
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

		// Token: 0x0600532B RID: 21291 RVA: 0x0014C5EC File Offset: 0x0014A7EC
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeOfExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0014C5F6 File Offset: 0x0014A7F6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeOfExpression(this);
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0014C5FF File Offset: 0x0014A7FF
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeOfExpression(this);
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x0014C608 File Offset: 0x0014A808
		public TypeOfExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || type != this.Type || closeParenToken != this.CloseParenToken)
			{
				TypeOfExpressionSyntax typeOfExpressionSyntax = SyntaxFactory.TypeOfExpression(keyword, openParenToken, type, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					typeOfExpressionSyntax = typeOfExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					typeOfExpressionSyntax = typeOfExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return typeOfExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0014C674 File Offset: 0x0014A874
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeOfExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0014C6A0 File Offset: 0x0014A8A0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeOfExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0014C6CC File Offset: 0x0014A8CC
		internal TypeOfExpressionSyntax(ObjectReader reader)
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

		// Token: 0x06005332 RID: 21298 RVA: 0x0014C74F File Offset: 0x0014A94F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0014C788 File Offset: 0x0014A988
		static TypeOfExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeOfExpressionSyntax), (ObjectReader r) => new TypeOfExpressionSyntax(r));
		}

		// Token: 0x04001643 RID: 5699
		internal readonly SyntaxToken keyword;

		// Token: 0x04001644 RID: 5700
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001645 RID: 5701
		internal readonly TypeSyntax type;

		// Token: 0x04001646 RID: 5702
		internal readonly SyntaxToken closeParenToken;
	}
}
