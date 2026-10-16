using System;

namespace System.Web.Compilation
{
	// Token: 0x02000667 RID: 1639
	internal enum TagType
	{
		// Token: 0x0400252D RID: 9517
		Text,
		// Token: 0x0400252E RID: 9518
		Tag,
		// Token: 0x0400252F RID: 9519
		Close,
		// Token: 0x04002530 RID: 9520
		SelfClosing,
		// Token: 0x04002531 RID: 9521
		Directive,
		// Token: 0x04002532 RID: 9522
		ServerComment,
		// Token: 0x04002533 RID: 9523
		DataBinding,
		// Token: 0x04002534 RID: 9524
		CodeRender,
		// Token: 0x04002535 RID: 9525
		CodeRenderExpression,
		// Token: 0x04002536 RID: 9526
		Include,
		// Token: 0x04002537 RID: 9527
		CodeRenderEncode
	}
}
