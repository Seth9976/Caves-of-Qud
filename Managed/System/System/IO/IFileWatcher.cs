using System;

namespace System.IO
{
	// Token: 0x02000519 RID: 1305
	internal interface IFileWatcher
	{
		// Token: 0x06002A4F RID: 10831
		void StartDispatching(object fsw);

		// Token: 0x06002A50 RID: 10832
		void StopDispatching(object fsw);

		// Token: 0x06002A51 RID: 10833
		void Dispose(object fsw);
	}
}
