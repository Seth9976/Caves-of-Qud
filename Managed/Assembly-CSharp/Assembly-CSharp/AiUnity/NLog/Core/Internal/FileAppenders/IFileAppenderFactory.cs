using System;

namespace AiUnity.NLog.Core.Internal.FileAppenders
{
	// Token: 0x0200027F RID: 639
	internal interface IFileAppenderFactory
	{
		// Token: 0x060016E4 RID: 5860
		BaseFileAppender Open(string fileName, ICreateFileParameters parameters);
	}
}
