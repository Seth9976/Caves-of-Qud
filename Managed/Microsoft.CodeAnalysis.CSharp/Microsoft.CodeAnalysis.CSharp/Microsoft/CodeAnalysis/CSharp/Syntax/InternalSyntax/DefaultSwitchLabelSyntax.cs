using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040E RID: 1038
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DefaultSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06005964 RID: 22884 RVA: 0x0015E79B File Offset: 0x0015C99B
		internal DefaultSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0015E7CB File Offset: 0x0015C9CB
		internal DefaultSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0015E7FF File Offset: 0x0015C9FF
		internal DefaultSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06005967 RID: 22887 RVA: 0x0015E82B File Offset: 0x0015CA2B
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06005968 RID: 22888 RVA: 0x0015E833 File Offset: 0x0015CA33
		public override SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0015E83C File Offset: 0x0015CA3C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxToken = null;
				}
				else
				{
					syntaxToken = this.colonToken;
				}
			}
			else
			{
				syntaxToken = this.keyword;
			}
			return syntaxToken;
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0015E867 File Offset: 0x0015CA67
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DefaultSwitchLabelSyntax(this, parent, position);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0015E871 File Offset: 0x0015CA71
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultSwitchLabel(this);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0015E87A File Offset: 0x0015CA7A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultSwitchLabel(this);
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0015E884 File Offset: 0x0015CA84
		public DefaultSwitchLabelSyntax Update(SyntaxToken keyword, SyntaxToken colonToken)
		{
			if (keyword != this.Keyword || colonToken != this.ColonToken)
			{
				DefaultSwitchLabelSyntax defaultSwitchLabelSyntax = SyntaxFactory.DefaultSwitchLabel(keyword, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					defaultSwitchLabelSyntax = defaultSwitchLabelSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					defaultSwitchLabelSyntax = defaultSwitchLabelSyntax.WithAnnotationsGreen(annotations);
				}
				return defaultSwitchLabelSyntax;
			}
			return this;
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x0015E8DA File Offset: 0x0015CADA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DefaultSwitchLabelSyntax(base.Kind, this.keyword, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x0015E8FA File Offset: 0x0015CAFA
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DefaultSwitchLabelSyntax(base.Kind, this.keyword, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x0015E91C File Offset: 0x0015CB1C
		internal DefaultSwitchLabelSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x0015E96B File Offset: 0x0015CB6B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x0015E98C File Offset: 0x0015CB8C
		static DefaultSwitchLabelSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DefaultSwitchLabelSyntax), (ObjectReader r) => new DefaultSwitchLabelSyntax(r));
		}

		// Token: 0x04001794 RID: 6036
		internal readonly SyntaxToken keyword;

		// Token: 0x04001795 RID: 6037
		internal readonly SyntaxToken colonToken;
	}
}
