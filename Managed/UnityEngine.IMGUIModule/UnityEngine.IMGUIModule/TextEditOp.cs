using System;

namespace UnityEngine
{
	// Token: 0x02000044 RID: 68
	internal enum TextEditOp
	{
		// Token: 0x04000156 RID: 342
		MoveLeft,
		// Token: 0x04000157 RID: 343
		MoveRight,
		// Token: 0x04000158 RID: 344
		MoveUp,
		// Token: 0x04000159 RID: 345
		MoveDown,
		// Token: 0x0400015A RID: 346
		MoveLineStart,
		// Token: 0x0400015B RID: 347
		MoveLineEnd,
		// Token: 0x0400015C RID: 348
		MoveTextStart,
		// Token: 0x0400015D RID: 349
		MoveTextEnd,
		// Token: 0x0400015E RID: 350
		MovePageUp,
		// Token: 0x0400015F RID: 351
		MovePageDown,
		// Token: 0x04000160 RID: 352
		MoveGraphicalLineStart,
		// Token: 0x04000161 RID: 353
		MoveGraphicalLineEnd,
		// Token: 0x04000162 RID: 354
		MoveWordLeft,
		// Token: 0x04000163 RID: 355
		MoveWordRight,
		// Token: 0x04000164 RID: 356
		MoveParagraphForward,
		// Token: 0x04000165 RID: 357
		MoveParagraphBackward,
		// Token: 0x04000166 RID: 358
		MoveToStartOfNextWord,
		// Token: 0x04000167 RID: 359
		MoveToEndOfPreviousWord,
		// Token: 0x04000168 RID: 360
		Delete,
		// Token: 0x04000169 RID: 361
		Backspace,
		// Token: 0x0400016A RID: 362
		DeleteWordBack,
		// Token: 0x0400016B RID: 363
		DeleteWordForward,
		// Token: 0x0400016C RID: 364
		DeleteLineBack,
		// Token: 0x0400016D RID: 365
		Cut,
		// Token: 0x0400016E RID: 366
		Paste,
		// Token: 0x0400016F RID: 367
		ScrollStart,
		// Token: 0x04000170 RID: 368
		ScrollEnd,
		// Token: 0x04000171 RID: 369
		ScrollPageUp,
		// Token: 0x04000172 RID: 370
		ScrollPageDown
	}
}
