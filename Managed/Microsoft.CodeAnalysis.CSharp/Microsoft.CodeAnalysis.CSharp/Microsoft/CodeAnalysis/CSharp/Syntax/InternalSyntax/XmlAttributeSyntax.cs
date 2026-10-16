using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000464 RID: 1124
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class XmlAttributeSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E8E RID: 24206 RVA: 0x0016EFC5 File Offset: 0x0016D1C5
		internal XmlAttributeSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x0016EFD0 File Offset: 0x0016D1D0
		internal XmlAttributeSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x0016EFD9 File Offset: 0x0016D1D9
		protected XmlAttributeSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005E91 RID: 24209
		public abstract XmlNameSyntax Name { get; }

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005E92 RID: 24210
		public abstract SyntaxToken EqualsToken { get; }

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06005E93 RID: 24211
		public abstract SyntaxToken StartQuoteToken { get; }

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06005E94 RID: 24212
		public abstract SyntaxToken EndQuoteToken { get; }
	}
}
