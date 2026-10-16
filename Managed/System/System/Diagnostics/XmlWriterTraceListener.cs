using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output as XML-encoded data to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as a <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200023B RID: 571
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class XmlWriterTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified stream as the recipient of the debugging and tracing output. </summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001117 RID: 4375 RVA: 0x0004A8E0 File Offset: 0x00048AE0
		public XmlWriterTraceListener(Stream stream)
			: base(stream)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified stream as the recipient of the debugging and tracing output. </summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001118 RID: 4376 RVA: 0x0004A8F4 File Offset: 0x00048AF4
		public XmlWriterTraceListener(Stream stream, string name)
			: base(stream, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class using the specified writer as the recipient of the debugging and tracing output. </summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06001119 RID: 4377 RVA: 0x0004A909 File Offset: 0x00048B09
		public XmlWriterTraceListener(TextWriter writer)
			: base(writer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified writer as the recipient of the debugging and tracing output. </summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x0600111A RID: 4378 RVA: 0x0004A91D File Offset: 0x00048B1D
		public XmlWriterTraceListener(TextWriter writer, string name)
			: base(writer, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified file as the recipient of the debugging and tracing output. </summary>
		/// <param name="filename">The name of the file to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is null. </exception>
		// Token: 0x0600111B RID: 4379 RVA: 0x0004A932 File Offset: 0x00048B32
		public XmlWriterTraceListener(string filename)
			: base(filename)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified file as the recipient of the debugging and tracing output.  </summary>
		/// <param name="filename">The name of the file to write to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x0600111C RID: 4380 RVA: 0x0004A946 File Offset: 0x00048B46
		public XmlWriterTraceListener(string filename, string name)
			: base(filename, name)
		{
		}

		/// <summary>Writes a verbatim message without any additional context information to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x0600111D RID: 4381 RVA: 0x0004A95B File Offset: 0x00048B5B
		public override void Write(string message)
		{
			this.WriteLine(message);
		}

		/// <summary>Writes a verbatim message without any additional context information followed by the current line terminator to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x0600111E RID: 4382 RVA: 0x0004A964 File Offset: 0x00048B64
		public override void WriteLine(string message)
		{
			this.TraceEvent(null, SR.GetString("Trace"), TraceEventType.Information, 0, message);
		}

		/// <summary>Writes trace information including an error message and a detailed error message to the file or stream.</summary>
		/// <param name="message">The error message to write.</param>
		/// <param name="detailMessage">The detailed error message to append to the error message.</param>
		// Token: 0x0600111F RID: 4383 RVA: 0x0004A97C File Offset: 0x00048B7C
		public override void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.TraceEvent(null, SR.GetString("Trace"), TraceEventType.Error, 0, stringBuilder.ToString());
		}

		/// <summary>Writes trace information, a formatted message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001120 RID: 4384 RVA: 0x0004A9C0 File Offset: 0x00048BC0
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			string text;
			if (args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				text = format;
			}
			this.WriteEscaped(text);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The message to write.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001121 RID: 4385 RVA: 0x0004AA1D File Offset: 0x00048C1D
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a data object, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">A data object to emit.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001122 RID: 4386 RVA: 0x0004AA58 File Offset: 0x00048C58
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				this.InternalWrite("<DataItem>");
				this.WriteData(data);
				this.InternalWrite("</DataItem>");
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, data objects, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of data objects to emit.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001123 RID: 4387 RVA: 0x0004AAD0 File Offset: 0x00048CD0
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					this.InternalWrite("<DataItem>");
					if (data[i] != null)
					{
						this.WriteData(data[i]);
					}
					this.InternalWrite("</DataItem>");
				}
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004AB60 File Offset: 0x00048D60
		private void WriteData(object data)
		{
			XPathNavigator xpathNavigator = data as XPathNavigator;
			if (xpathNavigator == null)
			{
				this.WriteEscaped(data.ToString());
				return;
			}
			if (this.strBldr == null)
			{
				this.strBldr = new StringBuilder();
				this.xmlBlobWriter = new XmlTextWriter(new StringWriter(this.strBldr, CultureInfo.CurrentCulture));
			}
			else
			{
				this.strBldr.Length = 0;
			}
			try
			{
				xpathNavigator.MoveToRoot();
				this.xmlBlobWriter.WriteNode(xpathNavigator, false);
				this.InternalWrite(this.strBldr.ToString());
			}
			catch (Exception)
			{
				this.InternalWrite(data.ToString());
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> for this listener so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06001125 RID: 4389 RVA: 0x0004AC08 File Offset: 0x00048E08
		public override void Close()
		{
			base.Close();
			if (this.xmlBlobWriter != null)
			{
				this.xmlBlobWriter.Close();
			}
			this.xmlBlobWriter = null;
			this.strBldr = null;
		}

		/// <summary>Writes trace information including the identity of a related activity, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A trace message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> structure that identifies a related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001126 RID: 4390 RVA: 0x0004AC34 File Offset: 0x00048E34
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			if (this.shouldRespectFilterOnTraceTransfer && base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, TraceEventType.Transfer, id, message))
			{
				return;
			}
			this.WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004AC88 File Offset: 0x00048E88
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache, Guid relatedActivityId)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.InternalWrite("\" RelatedActivityID=\"");
			this.InternalWrite(relatedActivityId.ToString("B"));
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004ACBA File Offset: 0x00048EBA
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.InternalWrite("<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
			this.InternalWrite("<EventID>");
			uint num = (uint)id;
			this.InternalWrite(num.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</EventID>");
			this.InternalWrite("<Type>3</Type>");
			this.InternalWrite("<SubType Name=\"");
			this.InternalWrite(eventType.ToString());
			this.InternalWrite("\">0</SubType>");
			this.InternalWrite("<Level>");
			int num2 = (int)eventType;
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.InternalWrite(num2.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</Level>");
			this.InternalWrite("<TimeCreated SystemTime=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			else
			{
				this.InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Source Name=\"");
			this.WriteEscaped(source);
			this.InternalWrite("\" />");
			this.InternalWrite("<Correlation ActivityID=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.ActivityId.ToString("B"));
				return;
			}
			this.InternalWrite(Guid.Empty.ToString("B"));
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004AE3C File Offset: 0x0004903C
		private void WriteEndHeader(TraceEventCache eventCache)
		{
			this.InternalWrite("\" />");
			this.InternalWrite("<Execution ProcessName=\"");
			this.InternalWrite(TraceEventCache.GetProcessName());
			this.InternalWrite("\" ProcessID=\"");
			this.InternalWrite(((uint)TraceEventCache.GetProcessId()).ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("\" ThreadID=\"");
			if (eventCache != null)
			{
				this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.WriteEscaped(TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Channel/>");
			this.InternalWrite("<Computer>");
			this.InternalWrite(this.machineName);
			this.InternalWrite("</Computer>");
			this.InternalWrite("</System>");
			this.InternalWrite("<ApplicationData>");
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0004AF1C File Offset: 0x0004911C
		private void WriteFooter(TraceEventCache eventCache)
		{
			bool flag = base.IsEnabled(TraceOptions.LogicalOperationStack);
			bool flag2 = base.IsEnabled(TraceOptions.Callstack);
			if (eventCache != null && (flag || flag2))
			{
				this.InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
				if (flag)
				{
					this.InternalWrite("<LogicalOperationStack>");
					Stack logicalOperationStack = eventCache.LogicalOperationStack;
					if (logicalOperationStack != null)
					{
						foreach (object obj in logicalOperationStack)
						{
							this.InternalWrite("<LogicalOperation>");
							this.WriteEscaped(obj.ToString());
							this.InternalWrite("</LogicalOperation>");
						}
					}
					this.InternalWrite("</LogicalOperationStack>");
				}
				this.InternalWrite("<Timestamp>");
				this.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
				this.InternalWrite("</Timestamp>");
				if (flag2)
				{
					this.InternalWrite("<Callstack>");
					this.WriteEscaped(eventCache.Callstack);
					this.InternalWrite("</Callstack>");
				}
				this.InternalWrite("</System.Diagnostics>");
			}
			this.InternalWrite("</ApplicationData></E2ETraceEvent>");
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004B044 File Offset: 0x00049244
		private void WriteEscaped(string str)
		{
			if (str == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c == '"')
							{
								this.InternalWrite(str.Substring(num, i - num));
								this.InternalWrite("&quot;");
								num = i + 1;
							}
						}
						else
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&#xD;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&#xA;");
						num = i + 1;
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c == '\'')
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&apos;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&amp;");
						num = i + 1;
					}
				}
				else if (c != '<')
				{
					if (c == '>')
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&gt;");
						num = i + 1;
					}
				}
				else
				{
					this.InternalWrite(str.Substring(num, i - num));
					this.InternalWrite("&lt;");
					num = i + 1;
				}
			}
			this.InternalWrite(str.Substring(num, str.Length - num));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0004B1C1 File Offset: 0x000493C1
		private void InternalWrite(string message)
		{
			if (!base.EnsureWriter())
			{
				return;
			}
			this.writer.Write(message);
		}

		// Token: 0x04000A38 RID: 2616
		private const string fixedHeader = "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">";

		// Token: 0x04000A39 RID: 2617
		private readonly string machineName = Environment.MachineName;

		// Token: 0x04000A3A RID: 2618
		private StringBuilder strBldr;

		// Token: 0x04000A3B RID: 2619
		private XmlTextWriter xmlBlobWriter;

		// Token: 0x04000A3C RID: 2620
		internal bool shouldRespectFilterOnTraceTransfer;
	}
}
