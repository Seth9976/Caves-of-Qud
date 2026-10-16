using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Diagnostics
{
	// Token: 0x020000BB RID: 187
	internal static class Tracing
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016A1C File Offset: 0x00014C1C
		private static object InternalSyncObject
		{
			get
			{
				if (Tracing.internalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Tracing.internalSyncObject, obj, null);
				}
				return Tracing.internalSyncObject;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00016A48 File Offset: 0x00014C48
		internal static bool On
		{
			get
			{
				if (!Tracing.tracingInitialized)
				{
					Tracing.InitializeLogging();
				}
				return Tracing.tracingEnabled;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016A5B File Offset: 0x00014C5B
		internal static bool IsVerbose
		{
			get
			{
				return Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Verbose);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016A69 File Offset: 0x00014C69
		internal static TraceSource Asmx
		{
			get
			{
				if (!Tracing.tracingInitialized)
				{
					Tracing.InitializeLogging();
				}
				if (!Tracing.tracingEnabled)
				{
					return null;
				}
				return Tracing.asmxTraceSource;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016A88 File Offset: 0x00014C88
		private static void InitializeLogging()
		{
			object obj = Tracing.InternalSyncObject;
			lock (obj)
			{
				if (!Tracing.tracingInitialized)
				{
					bool flag2 = false;
					Tracing.asmxTraceSource = new TraceSource("System.Web.Services.Asmx");
					if (Tracing.asmxTraceSource.Switch.ShouldTrace(TraceEventType.Critical))
					{
						flag2 = true;
						AppDomain currentDomain = AppDomain.CurrentDomain;
						currentDomain.UnhandledException += Tracing.UnhandledExceptionHandler;
						currentDomain.DomainUnload += Tracing.AppDomainUnloadEvent;
						currentDomain.ProcessExit += Tracing.ProcessExitEvent;
					}
					Tracing.tracingEnabled = flag2;
					Tracing.tracingInitialized = true;
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016B34 File Offset: 0x00014D34
		private static void Close()
		{
			if (Tracing.asmxTraceSource != null)
			{
				Tracing.asmxTraceSource.Close();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016B48 File Offset: 0x00014D48
		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			Tracing.ExceptionCatch(TraceEventType.Error, sender, "UnhandledExceptionHandler", ex);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016B6F File Offset: 0x00014D6F
		private static void ProcessExitEvent(object sender, EventArgs e)
		{
			Tracing.Close();
			Tracing.appDomainShutdown = true;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016B6F File Offset: 0x00014D6F
		private static void AppDomainUnloadEvent(object sender, EventArgs e)
		{
			Tracing.Close();
			Tracing.appDomainShutdown = true;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016B7C File Offset: 0x00014D7C
		private static bool ValidateSettings(TraceSource traceSource, TraceEventType traceLevel)
		{
			if (!Tracing.tracingEnabled)
			{
				return false;
			}
			if (!Tracing.tracingInitialized)
			{
				Tracing.InitializeLogging();
			}
			return traceSource != null && traceSource.Switch.ShouldTrace(traceLevel) && !Tracing.appDomainShutdown;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00016BB0 File Offset: 0x00014DB0
		internal static void Information(string format, params object[] args)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			Tracing.TraceEvent(TraceEventType.Information, Res.GetString(format, args));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00002B13 File Offset: 0x00000D13
		private static void TraceEvent(TraceEventType eventType, string format)
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016BCD File Offset: 0x00014DCD
		internal static Exception ExceptionThrow(TraceMethod method, Exception e)
		{
			return Tracing.ExceptionThrow(TraceEventType.Error, method, e);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016BD8 File Offset: 0x00014DD8
		internal static Exception ExceptionThrow(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("Exception thrown in {0}.\\r\\n    {1}: {2}", new object[]
			{
				method.ToString(),
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016C2D File Offset: 0x00014E2D
		internal static Exception ExceptionCatch(TraceMethod method, Exception e)
		{
			return Tracing.ExceptionCatch(TraceEventType.Error, method, e);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016C38 File Offset: 0x00014E38
		internal static Exception ExceptionCatch(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("Exception caught in {0}.\\r\\n    {1}: {2}", new object[]
			{
				method,
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016C88 File Offset: 0x00014E88
		internal static Exception ExceptionCatch(TraceEventType eventType, object target, string method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("Exception caught in {0}.\\r\\n    {1}: {2}", new object[]
			{
				TraceMethod.MethodId(target, method),
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016CE0 File Offset: 0x00014EE0
		internal static Exception ExceptionIgnore(TraceEventType eventType, TraceMethod method, Exception e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, eventType))
			{
				return e;
			}
			Tracing.TraceEvent(eventType, Res.GetString("Exception ignored in {0}.\\r\\n    {1}: {2}", new object[]
			{
				method,
				e.GetType(),
				e.Message
			}));
			Tracing.StackTrace(eventType, e);
			return e;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016D30 File Offset: 0x00014F30
		private static void StackTrace(TraceEventType eventType, Exception e)
		{
			if (Tracing.IsVerbose && !string.IsNullOrEmpty(e.StackTrace))
			{
				Tracing.TraceEvent(eventType, Res.GetString("Exception Details:\\r\\n{0}", new object[] { e.ToString() }));
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016D65 File Offset: 0x00014F65
		internal static string TraceId(string id)
		{
			return Res.GetString(id);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016D70 File Offset: 0x00014F70
		private static string GetHostByAddress(string ipAddress)
		{
			string text;
			try
			{
				text = Dns.GetHostByAddress(ipAddress).HostName;
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016DA4 File Offset: 0x00014FA4
		internal static List<string> Details(HttpRequest request)
		{
			if (request == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			list.Add(Res.GetString("Request Host Address: {0}", new object[] { request.UserHostAddress }));
			string text = ((request.UserHostAddress == request.UserHostName) ? Tracing.GetHostByAddress(request.UserHostAddress) : request.UserHostName);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Res.GetString("Request Host Name: {0}", new object[] { text }));
			}
			list.Add(Res.GetString("Request Url: [{0}] {1}", new object[] { request.HttpMethod, request.Url }));
			if (request.UrlReferrer != null)
			{
				list.Add(Res.GetString("Http referrer: {0}", new object[] { request.UrlReferrer }));
			}
			return list;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016E7C File Offset: 0x0001507C
		internal static void Enter(string callId, TraceMethod caller)
		{
			Tracing.Enter(callId, caller, null, null);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016E87 File Offset: 0x00015087
		internal static void Enter(string callId, TraceMethod caller, List<string> details)
		{
			Tracing.Enter(callId, caller, null, details);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016E92 File Offset: 0x00015092
		internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails)
		{
			Tracing.Enter(callId, caller, callDetails, null);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016EA0 File Offset: 0x000150A0
		internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails, List<string> details)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			string text = ((callDetails == null) ? Res.GetString("Calling {0}\\r\\n    Caller: {1}", new object[] { callId, caller }) : Res.GetString("Calling {0}\\r\\n    Method: {2}\\r\\n    Caller: {1}", new object[] { callId, caller, callDetails }));
			if (details != null && details.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				foreach (string text2 in details)
				{
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append("    ");
					stringBuilder.Append(text2);
				}
				text = stringBuilder.ToString();
			}
			Tracing.TraceEvent(TraceEventType.Information, text);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016F70 File Offset: 0x00015170
		internal static XmlDeserializationEvents GetDeserializationEvents()
		{
			return new XmlDeserializationEvents
			{
				OnUnknownElement = new XmlElementEventHandler(Tracing.OnUnknownElement),
				OnUnknownAttribute = new XmlAttributeEventHandler(Tracing.OnUnknownAttribute)
			};
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016FAC File Offset: 0x000151AC
		internal static void Exit(string callId, TraceMethod caller)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Information))
			{
				return;
			}
			Tracing.TraceEvent(TraceEventType.Information, Res.GetString("Return from {0}\\r\\n    Caller: {1}", new object[] { callId, caller }));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00016FDC File Offset: 0x000151DC
		internal static void OnUnknownElement(object sender, XmlElementEventArgs e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Warning))
			{
				return;
			}
			if (e.Element == null)
			{
				return;
			}
			string text = RuntimeUtils.ElementString(e.Element);
			string text2 = ((e.ExpectedElements == null) ? "The element was not expected in this context: {0}." : ((e.ExpectedElements.Length == 0) ? "An unexpected element was encountered: {0}. No elements were expected in this scope." : "The element was not expected in this context: {0}. Expected elements: {1}."));
			Tracing.TraceEvent(TraceEventType.Warning, Res.GetString(text2, new object[] { text, e.ExpectedElements }));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017054 File Offset: 0x00015254
		internal static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
		{
			if (!Tracing.ValidateSettings(Tracing.Asmx, TraceEventType.Warning))
			{
				return;
			}
			if (e.Attr == null)
			{
				return;
			}
			if (RuntimeUtils.IsKnownNamespace(e.Attr.NamespaceURI))
			{
				return;
			}
			string text = ((e.ExpectedAttributes == null) ? "An unexpected attribute was encountered: {0}='{1}'." : ((e.ExpectedAttributes.Length == 0) ? "An unexpected attribute was encountered: {0}='{1}'. No attributes were expected in this scope." : "The attribute was not expected in this context: {0}='{1}'. Expected attributes: {2}."));
			Tracing.TraceEvent(TraceEventType.Warning, Res.GetString(text, new object[]
			{
				e.Attr.Name,
				e.Attr.Value,
				e.ExpectedAttributes
			}));
		}

		// Token: 0x04000369 RID: 873
		private static bool tracingEnabled = true;

		// Token: 0x0400036A RID: 874
		private static bool tracingInitialized;

		// Token: 0x0400036B RID: 875
		private static bool appDomainShutdown;

		// Token: 0x0400036C RID: 876
		private const string TraceSourceAsmx = "System.Web.Services.Asmx";

		// Token: 0x0400036D RID: 877
		private static TraceSource asmxTraceSource;

		// Token: 0x0400036E RID: 878
		private static object internalSyncObject;
	}
}
