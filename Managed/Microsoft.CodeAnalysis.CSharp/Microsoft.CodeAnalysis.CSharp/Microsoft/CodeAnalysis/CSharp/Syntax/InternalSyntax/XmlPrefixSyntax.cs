using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000463 RID: 1123
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlPrefixSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E7F RID: 24191 RVA: 0x0016EDB4 File Offset: 0x0016CFB4
		internal XmlPrefixSyntax(SyntaxKind kind, SyntaxToken prefix, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(prefix);
			this.prefix = prefix;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x0016EDE4 File Offset: 0x0016CFE4
		internal XmlPrefixSyntax(SyntaxKind kind, SyntaxToken prefix, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(prefix);
			this.prefix = prefix;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x0016EE18 File Offset: 0x0016D018
		internal XmlPrefixSyntax(SyntaxKind kind, SyntaxToken prefix, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(prefix);
			this.prefix = prefix;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005E82 RID: 24194 RVA: 0x0016EE44 File Offset: 0x0016D044
		public SyntaxToken Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x0016EE4C File Offset: 0x0016D04C
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0016EE54 File Offset: 0x0016D054
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
				syntaxToken = this.prefix;
			}
			return syntaxToken;
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0016EE7F File Offset: 0x0016D07F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlPrefixSyntax(this, parent, position);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0016EE89 File Offset: 0x0016D089
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlPrefix(this);
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0016EE92 File Offset: 0x0016D092
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlPrefix(this);
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x0016EE9C File Offset: 0x0016D09C
		public XmlPrefixSyntax Update(SyntaxToken prefix, SyntaxToken colonToken)
		{
			if (prefix != this.Prefix || colonToken != this.ColonToken)
			{
				XmlPrefixSyntax xmlPrefixSyntax = SyntaxFactory.XmlPrefix(prefix, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlPrefixSyntax = xmlPrefixSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlPrefixSyntax = xmlPrefixSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlPrefixSyntax;
			}
			return this;
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0016EEF2 File Offset: 0x0016D0F2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlPrefixSyntax(base.Kind, this.prefix, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0016EF12 File Offset: 0x0016D112
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlPrefixSyntax(base.Kind, this.prefix, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x0016EF34 File Offset: 0x0016D134
		internal XmlPrefixSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.prefix = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x0016EF83 File Offset: 0x0016D183
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.prefix);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x0016EFA4 File Offset: 0x0016D1A4
		static XmlPrefixSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlPrefixSyntax), (ObjectReader r) => new XmlPrefixSyntax(r));
		}

		// Token: 0x040018D0 RID: 6352
		internal readonly SyntaxToken prefix;

		// Token: 0x040018D1 RID: 6353
		internal readonly SyntaxToken colonToken;
	}
}
