using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000460 RID: 1120
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class XmlElementEndTagSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E4F RID: 24143 RVA: 0x0016E598 File Offset: 0x0016C798
		internal XmlElementEndTagSyntax(SyntaxKind kind, SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanSlashToken);
			this.lessThanSlashToken = lessThanSlashToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x0016E5D8 File Offset: 0x0016C7D8
		internal XmlElementEndTagSyntax(SyntaxKind kind, SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanSlashToken);
			this.lessThanSlashToken = lessThanSlashToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x0016E627 File Offset: 0x0016C827
		internal XmlElementEndTagSyntax(SyntaxKind kind, SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanSlashToken);
			this.lessThanSlashToken = lessThanSlashToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06005E52 RID: 24146 RVA: 0x0016E663 File Offset: 0x0016C863
		public SyntaxToken LessThanSlashToken
		{
			get
			{
				return this.lessThanSlashToken;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x0016E66B File Offset: 0x0016C86B
		public XmlNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x0016E673 File Offset: 0x0016C873
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return this.greaterThanToken;
			}
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x0016E67C File Offset: 0x0016C87C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.lessThanSlashToken;
				break;
			case 1:
				greenNode = this.name;
				break;
			case 2:
				greenNode = this.greaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x0016E6BB File Offset: 0x0016C8BB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new XmlElementEndTagSyntax(this, parent, position);
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x0016E6C5 File Offset: 0x0016C8C5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitXmlElementEndTag(this);
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x0016E6CE File Offset: 0x0016C8CE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitXmlElementEndTag(this);
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x0016E6D8 File Offset: 0x0016C8D8
		public XmlElementEndTagSyntax Update(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
		{
			if (lessThanSlashToken != this.LessThanSlashToken || name != this.Name || greaterThanToken != this.GreaterThanToken)
			{
				XmlElementEndTagSyntax xmlElementEndTagSyntax = SyntaxFactory.XmlElementEndTag(lessThanSlashToken, name, greaterThanToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					xmlElementEndTagSyntax = xmlElementEndTagSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					xmlElementEndTagSyntax = xmlElementEndTagSyntax.WithAnnotationsGreen(annotations);
				}
				return xmlElementEndTagSyntax;
			}
			return this;
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x0016E738 File Offset: 0x0016C938
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new XmlElementEndTagSyntax(base.Kind, this.lessThanSlashToken, this.name, this.greaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x0016E75E File Offset: 0x0016C95E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new XmlElementEndTagSyntax(base.Kind, this.lessThanSlashToken, this.name, this.greaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x0016E784 File Offset: 0x0016C984
		internal XmlElementEndTagSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.lessThanSlashToken = syntaxToken;
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(xmlNameSyntax);
			this.name = xmlNameSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.greaterThanToken = syntaxToken2;
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0016E7ED File Offset: 0x0016C9ED
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanSlashToken);
			writer.WriteValue(this.name);
			writer.WriteValue(this.greaterThanToken);
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0016E81A File Offset: 0x0016CA1A
		static XmlElementEndTagSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlElementEndTagSyntax), (ObjectReader r) => new XmlElementEndTagSyntax(r));
		}

		// Token: 0x040018C7 RID: 6343
		internal readonly SyntaxToken lessThanSlashToken;

		// Token: 0x040018C8 RID: 6344
		internal readonly XmlNameSyntax name;

		// Token: 0x040018C9 RID: 6345
		internal readonly SyntaxToken greaterThanToken;
	}
}
