using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000361 RID: 865
	[Flags]
	internal enum LexerMode
	{
		// Token: 0x04001563 RID: 5475
		Syntax = 1,
		// Token: 0x04001564 RID: 5476
		DebuggerSyntax = 2,
		// Token: 0x04001565 RID: 5477
		Directive = 4,
		// Token: 0x04001566 RID: 5478
		XmlDocComment = 8,
		// Token: 0x04001567 RID: 5479
		XmlElementTag = 16,
		// Token: 0x04001568 RID: 5480
		XmlAttributeTextQuote = 32,
		// Token: 0x04001569 RID: 5481
		XmlAttributeTextDoubleQuote = 64,
		// Token: 0x0400156A RID: 5482
		XmlCrefQuote = 128,
		// Token: 0x0400156B RID: 5483
		XmlCrefDoubleQuote = 256,
		// Token: 0x0400156C RID: 5484
		XmlNameQuote = 512,
		// Token: 0x0400156D RID: 5485
		XmlNameDoubleQuote = 1024,
		// Token: 0x0400156E RID: 5486
		XmlCDataSectionText = 2048,
		// Token: 0x0400156F RID: 5487
		XmlCommentText = 4096,
		// Token: 0x04001570 RID: 5488
		XmlProcessingInstructionText = 8192,
		// Token: 0x04001571 RID: 5489
		XmlCharacter = 16384,
		// Token: 0x04001572 RID: 5490
		MaskLexMode = 65535,
		// Token: 0x04001573 RID: 5491
		XmlDocCommentLocationStart = 0,
		// Token: 0x04001574 RID: 5492
		XmlDocCommentLocationInterior = 65536,
		// Token: 0x04001575 RID: 5493
		XmlDocCommentLocationExterior = 131072,
		// Token: 0x04001576 RID: 5494
		XmlDocCommentLocationEnd = 262144,
		// Token: 0x04001577 RID: 5495
		MaskXmlDocCommentLocation = 983040,
		// Token: 0x04001578 RID: 5496
		XmlDocCommentStyleSingleLine = 0,
		// Token: 0x04001579 RID: 5497
		XmlDocCommentStyleDelimited = 1048576,
		// Token: 0x0400157A RID: 5498
		MaskXmlDocCommentStyle = 3145728,
		// Token: 0x0400157B RID: 5499
		None = 0
	}
}
