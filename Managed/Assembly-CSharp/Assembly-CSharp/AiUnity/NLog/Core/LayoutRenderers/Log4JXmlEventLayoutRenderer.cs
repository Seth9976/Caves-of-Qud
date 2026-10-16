using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Targets;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000250 RID: 592
	[Preserve]
	public class Log4JXmlEventLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x00076ED7 File Offset: 0x000750D7
		public Log4JXmlEventLayoutRenderer()
		{
			this.IncludeNLogData = true;
			this.NdcItemSeparator = " ";
			this.AppInfo = "Unity3D Application";
			this.Parameters = new List<NLogViewerParameterInfo>();
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00076F07 File Offset: 0x00075107
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x00076F0F File Offset: 0x0007510F
		[DefaultValue(true)]
		public bool IncludeNLogData { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00076F18 File Offset: 0x00075118
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00076F20 File Offset: 0x00075120
		public bool IndentXml { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00076F29 File Offset: 0x00075129
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00076F31 File Offset: 0x00075131
		public string AppInfo { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00076F3A File Offset: 0x0007513A
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x00076F42 File Offset: 0x00075142
		public bool IncludeCallSite { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00076F4B File Offset: 0x0007514B
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x00076F53 File Offset: 0x00075153
		public bool IncludeSourceInfo { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00076F5C File Offset: 0x0007515C
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x00076F64 File Offset: 0x00075164
		public bool IncludeMdc { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00076F6D File Offset: 0x0007516D
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00076F75 File Offset: 0x00075175
		public bool IncludeNdc { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00076F7E File Offset: 0x0007517E
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00076F86 File Offset: 0x00075186
		[DefaultValue(" ")]
		public string NdcItemSeparator { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00076F8F File Offset: 0x0007518F
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.IncludeSourceInfo)
				{
					return StackTraceUsage.WithSource;
				}
				if (this.IncludeCallSite)
				{
					return StackTraceUsage.WithoutSource;
				}
				return StackTraceUsage.None;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00076FA6 File Offset: 0x000751A6
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x00076FAE File Offset: 0x000751AE
		internal IList<NLogViewerParameterInfo> Parameters { get; set; }

		// Token: 0x060015C7 RID: 5575 RVA: 0x00076FB7 File Offset: 0x000751B7
		internal void AppendToStringBuilder(StringBuilder sb, LogEventInfo logEvent)
		{
			this.Append(sb, logEvent);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00076FC4 File Offset: 0x000751C4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				Indent = this.IndentXml,
				ConformanceLevel = ConformanceLevel.Fragment,
				IndentChars = "  "
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
			{
				xmlWriter.WriteStartElement("log4j", "event", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("xmlns", "nlog", "http://www.w3.org/2000/xmlns/", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
				xmlWriter.WriteAttributeSafeString("logger", logEvent.LoggerName);
				xmlWriter.WriteAttributeSafeString("level", logEvent.Level.ToString().ToUpper(CultureInfo.InvariantCulture));
				xmlWriter.WriteAttributeSafeString("timestamp", Convert.ToString((long)(logEvent.TimeStamp.ToUniversalTime() - Log4JXmlEventLayoutRenderer.log4jDateBase).TotalMilliseconds));
				xmlWriter.WriteAttributeSafeString("thread", Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture));
				xmlWriter.WriteElementSafeString("log4j", "message", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.FormattedMessage);
				if (logEvent.Exception != null)
				{
					xmlWriter.WriteElementSafeString("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.Exception.ToString());
				}
				if (logEvent.Exception != null)
				{
					xmlWriter.WriteStartElement("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace);
					xmlWriter.WriteSafeCData(logEvent.Exception.ToString());
					xmlWriter.WriteEndElement();
				}
				if (this.IncludeCallSite || this.IncludeSourceInfo)
				{
					StackFrame userStackFrame = logEvent.UserStackFrame;
					if (userStackFrame != null)
					{
						MethodBase method = userStackFrame.GetMethod();
						Type declaringType = method.DeclaringType;
						xmlWriter.WriteStartElement("log4j", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNamespace);
						if (declaringType != null)
						{
							xmlWriter.WriteAttributeSafeString("class", declaringType.FullName);
						}
						xmlWriter.WriteAttributeSafeString("method", method.ToString());
						if (this.IncludeSourceInfo)
						{
							xmlWriter.WriteAttributeSafeString("file", userStackFrame.GetFileName());
							xmlWriter.WriteAttributeSafeString("line", userStackFrame.GetFileLineNumber().ToString(CultureInfo.InvariantCulture));
						}
						xmlWriter.WriteEndElement();
						if (this.IncludeNLogData)
						{
							xmlWriter.WriteElementSafeString("nlog", "eventSequenceNumber", Log4JXmlEventLayoutRenderer.dummyNLogNamespace, logEvent.SequenceID.ToString(CultureInfo.InvariantCulture));
							xmlWriter.WriteStartElement("nlog", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
							if (declaringType != null)
							{
								xmlWriter.WriteAttributeSafeString("assembly", declaringType.Assembly.FullName);
							}
							xmlWriter.WriteEndElement();
							xmlWriter.WriteStartElement("nlog", "properties", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
							foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
							{
								xmlWriter.WriteStartElement("nlog", "data", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
								xmlWriter.WriteAttributeSafeString("name", Convert.ToString(keyValuePair.Key));
								xmlWriter.WriteAttributeSafeString("value", Convert.ToString(keyValuePair.Value));
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
					}
				}
				xmlWriter.WriteStartElement("log4j", "properties", Log4JXmlEventLayoutRenderer.dummyNamespace);
				foreach (NLogViewerParameterInfo nlogViewerParameterInfo in this.Parameters)
				{
					xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
					xmlWriter.WriteAttributeSafeString("name", nlogViewerParameterInfo.Name);
					xmlWriter.WriteAttributeSafeString("value", nlogViewerParameterInfo.Layout.Render(logEvent));
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4japp");
				xmlWriter.WriteAttributeSafeString("value", this.AppInfo);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4jmachinename");
				xmlWriter.WriteAttributeSafeString("value", Environment.MachineName);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				stringBuilder.Replace(" xmlns:log4j=\"" + Log4JXmlEventLayoutRenderer.dummyNamespace + "\"", string.Empty);
				stringBuilder.Replace(" xmlns:nlog=\"" + Log4JXmlEventLayoutRenderer.dummyNLogNamespace + "\"", string.Empty);
				builder.Append(stringBuilder.ToString());
			}
		}

		// Token: 0x04000DA7 RID: 3495
		private static readonly DateTime log4jDateBase = new DateTime(1970, 1, 1);

		// Token: 0x04000DA8 RID: 3496
		private static readonly string dummyNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid().ToString();

		// Token: 0x04000DA9 RID: 3497
		private static readonly string dummyNLogNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid().ToString();
	}
}
