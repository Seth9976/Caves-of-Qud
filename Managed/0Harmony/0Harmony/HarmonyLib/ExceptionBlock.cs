using System;

namespace HarmonyLib
{
	// Token: 0x02000061 RID: 97
	public class ExceptionBlock
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000B6E9 File Offset: 0x000098E9
		public ExceptionBlock(ExceptionBlockType blockType, Type catchType = null)
		{
			this.blockType = blockType;
			this.catchType = catchType ?? typeof(object);
		}

		// Token: 0x04000123 RID: 291
		public ExceptionBlockType blockType;

		// Token: 0x04000124 RID: 292
		public Type catchType;
	}
}
