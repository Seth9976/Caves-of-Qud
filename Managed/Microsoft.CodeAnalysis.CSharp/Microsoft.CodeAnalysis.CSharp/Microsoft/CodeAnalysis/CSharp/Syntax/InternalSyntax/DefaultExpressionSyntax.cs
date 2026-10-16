using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003A4 RID: 932
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DefaultExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005312 RID: 21266 RVA: 0x0014C12C File Offset: 0x0014A32C
		internal DefaultExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005313 RID: 21267 RVA: 0x0014C188 File Offset: 0x0014A388
		internal DefaultExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x06005314 RID: 21268 RVA: 0x0014C1E8 File Offset: 0x0014A3E8
		internal DefaultExpressionSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
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

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06005315 RID: 21269 RVA: 0x0014C23F File Offset: 0x0014A43F
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x0014C247 File Offset: 0x0014A447
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06005317 RID: 21271 RVA: 0x0014C24F File Offset: 0x0014A44F
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x0014C257 File Offset: 0x0014A457
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0014C260 File Offset: 0x0014A460
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

		// Token: 0x0600531A RID: 21274 RVA: 0x0014C2AC File Offset: 0x0014A4AC
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DefaultExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0014C2B6 File Offset: 0x0014A4B6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultExpression(this);
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x0014C2BF File Offset: 0x0014A4BF
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultExpression(this);
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x0014C2C8 File Offset: 0x0014A4C8
		public DefaultExpressionSyntax Update(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword != this.Keyword || openParenToken != this.OpenParenToken || type != this.Type || closeParenToken != this.CloseParenToken)
			{
				DefaultExpressionSyntax defaultExpressionSyntax = SyntaxFactory.DefaultExpression(keyword, openParenToken, type, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					defaultExpressionSyntax = defaultExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					defaultExpressionSyntax = defaultExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return defaultExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x0014C334 File Offset: 0x0014A534
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DefaultExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0014C360 File Offset: 0x0014A560
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DefaultExpressionSyntax(base.Kind, this.keyword, this.openParenToken, this.type, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0014C38C File Offset: 0x0014A58C
		internal DefaultExpressionSyntax(ObjectReader reader)
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

		// Token: 0x06005321 RID: 21281 RVA: 0x0014C40F File Offset: 0x0014A60F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0014C448 File Offset: 0x0014A648
		static DefaultExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DefaultExpressionSyntax), (ObjectReader r) => new DefaultExpressionSyntax(r));
		}

		// Token: 0x0400163F RID: 5695
		internal readonly SyntaxToken keyword;

		// Token: 0x04001640 RID: 5696
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001641 RID: 5697
		internal readonly TypeSyntax type;

		// Token: 0x04001642 RID: 5698
		internal readonly SyntaxToken closeParenToken;
	}
}
