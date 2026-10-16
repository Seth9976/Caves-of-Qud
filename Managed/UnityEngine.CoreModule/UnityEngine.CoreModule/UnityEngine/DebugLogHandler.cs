using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000137 RID: 311
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		[ThreadAndSerializationSafe]
		internal unsafe static void Internal_Log(LogType level, LogOption options, string msg, Object obj)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(msg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = msg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				DebugLogHandler.Internal_Log_Injected(level, options, ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(obj));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000F520 File Offset: 0x0000D720
		[ThreadAndSerializationSafe]
		internal static void Internal_LogException(Exception ex, Object obj)
		{
			DebugLogHandler.Internal_LogException_Injected(ex, Object.MarshalledUnityObject.Marshal<Object>(obj));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000F539 File Offset: 0x0000D739
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, LogOption.None, string.Format(format, args), context);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000F54D File Offset: 0x0000D74D
		public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, logOptions, string.Format(format, args), context);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000F564 File Offset: 0x0000D764
		public void LogException(Exception exception, Object context)
		{
			bool flag = exception == null;
			if (flag)
			{
				throw new ArgumentNullException("exception");
			}
			DebugLogHandler.Internal_LogException(exception, context);
		}

		// Token: 0x06000900 RID: 2304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Log_Injected(LogType level, LogOption options, ref ManagedSpanWrapper msg, IntPtr obj);

		// Token: 0x06000901 RID: 2305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_LogException_Injected(Exception ex, IntPtr obj);
	}
}
