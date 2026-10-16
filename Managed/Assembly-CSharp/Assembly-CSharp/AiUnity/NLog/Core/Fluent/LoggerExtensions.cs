using System;
using AiUnity.Common.Log;

namespace AiUnity.NLog.Core.Fluent
{
	// Token: 0x02000282 RID: 642
	public static class LoggerExtensions
	{
		// Token: 0x060016FF RID: 5887 RVA: 0x00079FC9 File Offset: 0x000781C9
		public static LogBuilder Log(this NLogger logger, LogLevels logLevel)
		{
			return new LogBuilder(logger, logLevel);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00079FD2 File Offset: 0x000781D2
		public static LogBuilder Trace(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Trace);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00079FDC File Offset: 0x000781DC
		public static LogBuilder Debug(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Debug);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00079FE6 File Offset: 0x000781E6
		public static LogBuilder Info(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Info);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00079FF0 File Offset: 0x000781F0
		public static LogBuilder Warn(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Warn);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00079FF9 File Offset: 0x000781F9
		public static LogBuilder Error(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Error);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0007A002 File Offset: 0x00078202
		public static LogBuilder Fatal(this NLogger logger)
		{
			return new LogBuilder(logger, LogLevels.Fatal);
		}
	}
}
