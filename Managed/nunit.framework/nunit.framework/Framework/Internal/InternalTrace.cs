using System;
using System.IO;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200006A RID: 106
	public static class InternalTrace
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000800F File Offset: 0x0000620F
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00008016 File Offset: 0x00006216
		public static bool Initialized { get; private set; }

		// Token: 0x06000397 RID: 919 RVA: 0x00008020 File Offset: 0x00006220
		public static void Initialize(string logName, InternalTraceLevel level)
		{
			if (!InternalTrace.Initialized)
			{
				InternalTrace.traceLevel = level;
				if (InternalTrace.traceWriter == null && InternalTrace.traceLevel > InternalTraceLevel.Off)
				{
					InternalTrace.traceWriter = new InternalTraceWriter(logName);
					InternalTrace.traceWriter.WriteLine("InternalTrace: Initializing at level {0}", InternalTrace.traceLevel);
				}
				InternalTrace.Initialized = true;
				return;
			}
			InternalTrace.traceWriter.WriteLine("InternalTrace: Ignoring attempted re-initialization at level {0}", level);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000808C File Offset: 0x0000628C
		public static void Initialize(TextWriter writer, InternalTraceLevel level)
		{
			if (!InternalTrace.Initialized)
			{
				InternalTrace.traceLevel = level;
				if (InternalTrace.traceWriter == null && InternalTrace.traceLevel > InternalTraceLevel.Off)
				{
					InternalTrace.traceWriter = new InternalTraceWriter(writer);
					InternalTrace.traceWriter.WriteLine("InternalTrace: Initializing at level " + InternalTrace.traceLevel.ToString());
				}
				InternalTrace.Initialized = true;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000080EA File Offset: 0x000062EA
		public static Logger GetLogger(string name)
		{
			return new Logger(name, InternalTrace.traceLevel, InternalTrace.traceWriter);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000080FC File Offset: 0x000062FC
		public static Logger GetLogger(Type type)
		{
			return InternalTrace.GetLogger(type.FullName);
		}

		// Token: 0x04000076 RID: 118
		private static InternalTraceLevel traceLevel;

		// Token: 0x04000077 RID: 119
		private static InternalTraceWriter traceWriter;
	}
}
