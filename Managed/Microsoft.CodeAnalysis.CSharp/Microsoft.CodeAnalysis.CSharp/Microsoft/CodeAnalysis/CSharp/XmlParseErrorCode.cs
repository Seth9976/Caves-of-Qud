using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000144 RID: 324
	internal enum XmlParseErrorCode
	{
		// Token: 0x04000DD3 RID: 3539
		XML_RefUndefinedEntity_1,
		// Token: 0x04000DD4 RID: 3540
		XML_InvalidCharEntity,
		// Token: 0x04000DD5 RID: 3541
		XML_InvalidUnicodeChar,
		// Token: 0x04000DD6 RID: 3542
		XML_InvalidWhitespace,
		// Token: 0x04000DD7 RID: 3543
		XML_MissingEqualsAttribute,
		// Token: 0x04000DD8 RID: 3544
		XML_StringLiteralNoStartQuote,
		// Token: 0x04000DD9 RID: 3545
		XML_StringLiteralNoEndQuote,
		// Token: 0x04000DDA RID: 3546
		XML_StringLiteralNonAsciiQuote,
		// Token: 0x04000DDB RID: 3547
		XML_LessThanInAttributeValue,
		// Token: 0x04000DDC RID: 3548
		XML_IncorrectComment,
		// Token: 0x04000DDD RID: 3549
		XML_ElementTypeMatch,
		// Token: 0x04000DDE RID: 3550
		XML_DuplicateAttribute,
		// Token: 0x04000DDF RID: 3551
		XML_WhitespaceMissing,
		// Token: 0x04000DE0 RID: 3552
		XML_EndTagNotExpected,
		// Token: 0x04000DE1 RID: 3553
		XML_CDataEndTagNotAllowed,
		// Token: 0x04000DE2 RID: 3554
		XML_EndTagExpected,
		// Token: 0x04000DE3 RID: 3555
		XML_ExpectedIdentifier,
		// Token: 0x04000DE4 RID: 3556
		XML_ExpectedEndOfTag,
		// Token: 0x04000DE5 RID: 3557
		XML_InvalidToken,
		// Token: 0x04000DE6 RID: 3558
		XML_ExpectedEndOfXml
	}
}
