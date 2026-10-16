using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000138 RID: 312
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	[NativeHeader("Runtime/Diagnostics/Validation.h")]
	[NativeHeader("Runtime/Diagnostics/IntegrityCheck.h")]
	public class Debug
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0000F58D File Offset: 0x0000D78D
		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000F594 File Offset: 0x0000D794
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawLine(start, end, color, duration, flag);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawLine(start, end, color, num, flag);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawLine(start, end, white, num, flag);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000F618 File Offset: 0x0000D818
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
		{
			bool flag = true;
			Debug.DrawRay(start, dir, color, duration, flag);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000F634 File Offset: 0x0000D834
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color)
		{
			bool flag = true;
			float num = 0f;
			Debug.DrawRay(start, dir, color, num, flag);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0000F658 File Offset: 0x0000D858
		[ExcludeFromDocs]
		public static void DrawRay(Vector3 start, Vector3 dir)
		{
			bool flag = true;
			float num = 0f;
			Color white = Color.white;
			Debug.DrawRay(start, dir, white, num, flag);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000F67F File Offset: 0x0000D87F
		public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine(start, start + dir, color, duration, depthTest);
		}

		// Token: 0x0600090B RID: 2315
		[FreeFunction("PauseEditor")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Break();

		// Token: 0x0600090C RID: 2316
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DebugBreak();

		// Token: 0x0600090D RID: 2317 RVA: 0x0000F694 File Offset: 0x0000D894
		[ThreadSafe]
		public unsafe static int ExtractStackTraceNoAlloc(byte* buffer, int bufferMax, string projectFolder)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(projectFolder, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = projectFolder.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Debug.ExtractStackTraceNoAlloc_Injected(buffer, bufferMax, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000F70D File Offset: 0x0000D90D
		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000F71E File Offset: 0x0000D91E
		public static void LogFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, context, format, args);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000F730 File Offset: 0x0000D930
		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler debugLogHandler = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = debugLogHandler == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				bool flag2 = Debug.unityLogger.IsLogTypeAllowed(logType);
				if (flag2)
				{
					debugLogHandler.LogFormat(logType, logOptions, context, format, args);
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000F784 File Offset: 0x0000D984
		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000F794 File Offset: 0x0000D994
		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000F7A5 File Offset: 0x0000D9A5
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000F7B6 File Offset: 0x0000D9B6
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x06000917 RID: 2327
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearDeveloperConsole();

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000918 RID: 2328
		// (set) Token: 0x06000919 RID: 2329
		public static extern bool developerConsoleEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600091A RID: 2330
		// (set) Token: 0x0600091B RID: 2331
		public static extern bool developerConsoleVisible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000F809 File Offset: 0x0000DA09
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000F81A File Offset: 0x0000DA1A
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000F82C File Offset: 0x0000DA2C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000F854 File Offset: 0x0000DA54
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed", context);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000F87C File Offset: 0x0000DA7C
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, object message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message, Object context)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message, context);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000F90C File Offset: 0x0000DB0C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0000F930 File Offset: 0x0000DB30
		[Conditional("UNITY_ASSERTIONS")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000F955 File Offset: 0x0000DB55
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000F965 File Offset: 0x0000DB65
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Assert, message, context);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000F976 File Offset: 0x0000DB76
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000F987 File Offset: 0x0000DB87
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, context, format, args);
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600092E RID: 2350
		public static extern bool isDebugBuild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600092F RID: 2351
		[NativeThrows]
		internal static extern DiagnosticSwitch[] diagnosticSwitches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000F99C File Offset: 0x0000DB9C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static DiagnosticSwitch GetDiagnosticSwitch(string name)
		{
			foreach (DiagnosticSwitch diagnosticSwitch in Debug.diagnosticSwitches)
			{
				bool flag = diagnosticSwitch.name == name;
				if (flag)
				{
					return diagnosticSwitch;
				}
			}
			throw new ArgumentException("Could not find DiagnosticSwitch named " + name);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				try
				{
					Debug.unityLogger.LogException(exception, obj);
				}
				catch (Exception ex)
				{
					Debug.s_DefaultLogger.LogError(string.Format("Invalid exception thrown from custom {0}.LogException(). Message: {1}", Debug.unityLogger.logHandler.GetType(), ex), obj);
					return false;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		[RequiredByNativeCode]
		internal static bool IsLoggingEnabled()
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = Debug.unityLogger.logEnabled;
			}
			else
			{
				flag2 = Debug.s_DefaultLogger.logEnabled;
			}
			return flag2;
		}

		// Token: 0x06000933 RID: 2355
		[FreeFunction("RetrieveStartupLogs")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Debug.StartupLog[] RetrieveStartupLogs();

		// Token: 0x06000934 RID: 2356 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		[FreeFunction("CheckApplicationIntegrity")]
		public static string CheckIntegrity(IntegrityCheckLevel level)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Debug.CheckIntegrity_Injected(level, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000935 RID: 2357
		[FreeFunction("IsValidationLevelEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValidationLevelEnabled(ValidationLevel level);

		// Token: 0x06000936 RID: 2358 RVA: 0x0000FADC File Offset: 0x0000DCDC
		[Conditional("UNITY_ASSERTIONS")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert(bool, string, params object[]) is obsolete. Use AssertFormat(bool, string, params object[]) (UnityUpgradable) -> AssertFormat(*)", true)]
		public static void Assert(bool condition, string format, params object[] args)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.LogFormat(LogType.Assert, format, args);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0000FB00 File Offset: 0x0000DD00
		[Obsolete("Debug.logger is obsolete. Please use Debug.unityLogger instead (UnityUpgradable) -> unityLogger")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static ILogger logger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x0600093A RID: 2362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected([In] ref Vector3 start, [In] ref Vector3 end, [DefaultValue("Color.white")] [In] ref Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest);

		// Token: 0x0600093B RID: 2363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int ExtractStackTraceNoAlloc_Injected(byte* buffer, int bufferMax, ref ManagedSpanWrapper projectFolder);

		// Token: 0x0600093C RID: 2364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckIntegrity_Injected(IntegrityCheckLevel level, out ManagedSpanWrapper ret);

		// Token: 0x04000421 RID: 1057
		internal static readonly ILogger s_DefaultLogger = new Logger(new DebugLogHandler());

		// Token: 0x04000422 RID: 1058
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());

		// Token: 0x02000139 RID: 313
		[NativeHeader("Runtime/Export/Debug/LogCapture.bindings.h")]
		public struct StartupLog
		{
			// Token: 0x04000423 RID: 1059
			public long timestamp;

			// Token: 0x04000424 RID: 1060
			public LogType logType;

			// Token: 0x04000425 RID: 1061
			public string message;
		}
	}
}
