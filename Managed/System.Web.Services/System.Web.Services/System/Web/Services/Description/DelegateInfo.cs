using System;

namespace System.Web.Services.Description
{
	// Token: 0x020000E2 RID: 226
	internal class DelegateInfo
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0001A99F File Offset: 0x00018B9F
		internal DelegateInfo(string handlerType, string handlerArgs)
		{
			this.handlerType = handlerType;
			this.handlerArgs = handlerArgs;
		}

		// Token: 0x040003B6 RID: 950
		internal string handlerType;

		// Token: 0x040003B7 RID: 951
		internal string handlerArgs;
	}
}
