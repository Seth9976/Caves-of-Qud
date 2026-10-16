using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040D RID: 1037
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CaseSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06005954 RID: 22868 RVA: 0x0015E4F8 File Offset: 0x0015C6F8
		internal CaseSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x0015E538 File Offset: 0x0015C738
		internal CaseSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0015E587 File Offset: 0x0015C787
		internal CaseSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(value);
			this.value = value;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x0015E5C3 File Offset: 0x0015C7C3
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06005958 RID: 22872 RVA: 0x0015E5CB File Offset: 0x0015C7CB
		public ExpressionSyntax Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x0015E5D3 File Offset: 0x0015C7D3
		public override SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0015E5DC File Offset: 0x0015C7DC
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
				greenNode = this.value;
				break;
			case 2:
				greenNode = this.colonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x0015E61B File Offset: 0x0015C81B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CaseSwitchLabelSyntax(this, parent, position);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x0015E625 File Offset: 0x0015C825
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCaseSwitchLabel(this);
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x0015E62E File Offset: 0x0015C82E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCaseSwitchLabel(this);
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x0015E638 File Offset: 0x0015C838
		public CaseSwitchLabelSyntax Update(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
		{
			if (keyword != this.Keyword || value != this.Value || colonToken != this.ColonToken)
			{
				CaseSwitchLabelSyntax caseSwitchLabelSyntax = SyntaxFactory.CaseSwitchLabel(keyword, value, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					caseSwitchLabelSyntax = caseSwitchLabelSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					caseSwitchLabelSyntax = caseSwitchLabelSyntax.WithAnnotationsGreen(annotations);
				}
				return caseSwitchLabelSyntax;
			}
			return this;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0015E698 File Offset: 0x0015C898
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CaseSwitchLabelSyntax(base.Kind, this.keyword, this.value, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0015E6BE File Offset: 0x0015C8BE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CaseSwitchLabelSyntax(base.Kind, this.keyword, this.value, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x0015E6E4 File Offset: 0x0015C8E4
		internal CaseSwitchLabelSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.value = expressionSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0015E74D File Offset: 0x0015C94D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.value);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0015E77A File Offset: 0x0015C97A
		static CaseSwitchLabelSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CaseSwitchLabelSyntax), (ObjectReader r) => new CaseSwitchLabelSyntax(r));
		}

		// Token: 0x04001791 RID: 6033
		internal readonly SyntaxToken keyword;

		// Token: 0x04001792 RID: 6034
		internal readonly ExpressionSyntax value;

		// Token: 0x04001793 RID: 6035
		internal readonly SyntaxToken colonToken;
	}
}
