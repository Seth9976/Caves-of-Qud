using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200004E RID: 78
	internal class ExceptionHolder
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00006AE6 File Offset: 0x00004CE6
		public ExceptionHolder(ExceptionDispatchInfo exception)
		{
			this.exception = exception;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006AF5 File Offset: 0x00004CF5
		public ExceptionDispatchInfo GetException()
		{
			if (!this.calledGet)
			{
				this.calledGet = true;
				GC.SuppressFinalize(this);
			}
			return this.exception;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006B14 File Offset: 0x00004D14
		~ExceptionHolder()
		{
			if (!this.calledGet)
			{
				UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
			}
		}

		// Token: 0x0400009E RID: 158
		private ExceptionDispatchInfo exception;

		// Token: 0x0400009F RID: 159
		private bool calledGet;
	}
}
