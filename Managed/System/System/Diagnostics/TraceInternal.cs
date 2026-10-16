using System;
using System.Collections;
using System.IO;

namespace System.Diagnostics
{
	// Token: 0x02000232 RID: 562
	internal static class TraceInternal
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x000474A0 File Offset: 0x000456A0
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.listeners == null)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						if (TraceInternal.listeners == null)
						{
							SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.SystemDiagnosticsSection;
							if (systemDiagnosticsSection != null)
							{
								TraceInternal.listeners = systemDiagnosticsSection.Trace.Listeners.GetRuntimeObject();
							}
							else
							{
								TraceInternal.listeners = new TraceListenerCollection();
								TraceListener traceListener = new DefaultTraceListener();
								traceListener.IndentLevel = TraceInternal.indentLevel;
								traceListener.IndentSize = TraceInternal.indentSize;
								TraceInternal.listeners.Add(traceListener);
							}
						}
					}
				}
				return TraceInternal.listeners;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00047554 File Offset: 0x00045754
		internal static string AppName
		{
			get
			{
				if (TraceInternal.appName == null)
				{
					string[] commandLineArgs = Environment.GetCommandLineArgs();
					if (commandLineArgs.Length != 0)
					{
						TraceInternal.appName = Path.GetFileName(commandLineArgs[0]);
					}
				}
				return TraceInternal.appName;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004758A File Offset: 0x0004578A
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x00047598 File Offset: 0x00045798
		public static bool AutoFlush
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.autoFlush;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.autoFlush = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x000475A7 File Offset: 0x000457A7
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x000475B5 File Offset: 0x000457B5
		public static bool UseGlobalLock
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.useGlobalLock;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.useGlobalLock = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x000475C4 File Offset: 0x000457C4
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x000475CC File Offset: 0x000457CC
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.indentLevel;
			}
			set
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (value < 0)
					{
						value = 0;
					}
					TraceInternal.indentLevel = value;
					if (TraceInternal.listeners != null)
					{
						foreach (object obj2 in TraceInternal.Listeners)
						{
							((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
						}
					}
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00047664 File Offset: 0x00045864
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00047672 File Offset: 0x00045872
		public static int IndentSize
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.indentSize;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.SetIndentSize(value);
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00047680 File Offset: 0x00045880
		private static void SetIndentSize(int value)
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				if (value < 0)
				{
					value = 0;
				}
				TraceInternal.indentSize = value;
				if (TraceInternal.listeners != null)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						((TraceListener)obj2).IndentSize = TraceInternal.indentSize;
					}
				}
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004771C File Offset: 0x0004591C
		public static void Indent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel < 2147483647)
				{
					TraceInternal.indentLevel++;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000477BC File Offset: 0x000459BC
		public static void Unindent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel > 0)
				{
					TraceInternal.indentLevel--;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					((TraceListener)obj2).IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00047858 File Offset: 0x00045A58
		public static void Flush()
		{
			if (TraceInternal.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								((TraceListener)obj2).Flush();
							}
							return;
						}
					}
				}
				foreach (object obj3 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj3;
					if (!traceListener.IsThreadSafe)
					{
						TraceListener traceListener2 = traceListener;
						lock (traceListener2)
						{
							traceListener.Flush();
							continue;
						}
					}
					traceListener.Flush();
				}
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0004796C File Offset: 0x00045B6C
		public static void Close()
		{
			if (TraceInternal.listeners != null)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						((TraceListener)obj2).Close();
					}
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000479F0 File Offset: 0x00045BF0
		public static void Assert(bool condition)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(string.Empty);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00047A00 File Offset: 0x00045C00
		public static void Assert(bool condition, string message)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00047A0C File Offset: 0x00045C0C
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00047A1C File Offset: 0x00045C1C
		public static void Fail(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Fail(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00047B58 File Offset: 0x00045D58
		public static void Fail(string message, string detailMessage)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message, detailMessage);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Fail(message, detailMessage);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message, detailMessage);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00047C94 File Offset: 0x00045E94
		private static void InitializeSettings()
		{
			if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
					{
						TraceInternal.defaultInitialized = DiagnosticsConfiguration.IsInitializing();
						TraceInternal.SetIndentSize(DiagnosticsConfiguration.IndentSize);
						TraceInternal.autoFlush = DiagnosticsConfiguration.AutoFlush;
						TraceInternal.useGlobalLock = DiagnosticsConfiguration.UseGlobalLock;
						TraceInternal.settingsInitialized = true;
					}
				}
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00047D38 File Offset: 0x00045F38
		internal static void Refresh()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.settingsInitialized = false;
				TraceInternal.listeners = null;
			}
			TraceInternal.InitializeSettings();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00047D88 File Offset: 0x00045F88
		public static void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			TraceEventCache traceEventCache = new TraceEventCache();
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (args == null)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener.Flush();
								}
							}
							return;
						}
					}
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj3 = enumerator.Current;
							TraceListener traceListener2 = (TraceListener)obj3;
							traceListener2.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
							if (TraceInternal.AutoFlush)
							{
								traceListener2.Flush();
							}
						}
						return;
					}
				}
			}
			if (args == null)
			{
				using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj4 = enumerator.Current;
						TraceListener traceListener3 = (TraceListener)obj4;
						if (!traceListener3.IsThreadSafe)
						{
							TraceListener traceListener4 = traceListener3;
							lock (traceListener4)
							{
								traceListener3.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener3.Flush();
								}
								continue;
							}
						}
						traceListener3.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
						if (TraceInternal.AutoFlush)
						{
							traceListener3.Flush();
						}
					}
					return;
				}
			}
			foreach (object obj5 in TraceInternal.Listeners)
			{
				TraceListener traceListener5 = (TraceListener)obj5;
				if (!traceListener5.IsThreadSafe)
				{
					TraceListener traceListener4 = traceListener5;
					lock (traceListener4)
					{
						traceListener5.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
						if (TraceInternal.AutoFlush)
						{
							traceListener5.Flush();
						}
						continue;
					}
				}
				traceListener5.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
				if (TraceInternal.AutoFlush)
				{
					traceListener5.Flush();
				}
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004800C File Offset: 0x0004620C
		public static void Write(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00048148 File Offset: 0x00046348
		public static void Write(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00048284 File Offset: 0x00046484
		public static void Write(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000483C0 File Offset: 0x000465C0
		public static void Write(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000484FC File Offset: 0x000466FC
		public static void WriteLine(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00048638 File Offset: 0x00046838
		public static void WriteLine(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00048774 File Offset: 0x00046974
		public static void WriteLine(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000488B0 File Offset: 0x00046AB0
		public static void WriteLine(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000489EC File Offset: 0x00046BEC
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.Write(message);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000489F7 File Offset: 0x00046BF7
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.Write(value);
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00048A02 File Offset: 0x00046C02
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.Write(message, category);
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00048A0E File Offset: 0x00046C0E
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.Write(value, category);
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00048A1A File Offset: 0x00046C1A
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00048A25 File Offset: 0x00046C25
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value);
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00048A30 File Offset: 0x00046C30
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message, category);
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00048A3C File Offset: 0x00046C3C
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value, category);
			}
		}

		// Token: 0x04000A06 RID: 2566
		private static volatile string appName = null;

		// Token: 0x04000A07 RID: 2567
		private static volatile TraceListenerCollection listeners;

		// Token: 0x04000A08 RID: 2568
		private static volatile bool autoFlush;

		// Token: 0x04000A09 RID: 2569
		private static volatile bool useGlobalLock;

		// Token: 0x04000A0A RID: 2570
		[ThreadStatic]
		private static int indentLevel;

		// Token: 0x04000A0B RID: 2571
		private static volatile int indentSize;

		// Token: 0x04000A0C RID: 2572
		private static volatile bool settingsInitialized;

		// Token: 0x04000A0D RID: 2573
		private static volatile bool defaultInitialized;

		// Token: 0x04000A0E RID: 2574
		internal static readonly object critSec = new object();
	}
}
