using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000268 RID: 616
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class without a trace listener.</summary>
		// Token: 0x06001332 RID: 4914 RVA: 0x00046F88 File Offset: 0x00045188
		public EventLogTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified event log.</summary>
		/// <param name="eventLog">The event log to write to. </param>
		// Token: 0x06001333 RID: 4915 RVA: 0x0005114B File Offset: 0x0004F34B
		public EventLogTraceListener(EventLog eventLog)
		{
			if (eventLog == null)
			{
				throw new ArgumentNullException("eventLog");
			}
			this.event_log = eventLog;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified source.</summary>
		/// <param name="source">The name of an existing event log source. </param>
		// Token: 0x06001334 RID: 4916 RVA: 0x00051168 File Offset: 0x0004F368
		public EventLogTraceListener(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.event_log = new EventLog();
			this.event_log.Source = source;
		}

		/// <summary>Gets or sets the event log to write to.</summary>
		/// <returns>The event log to write to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00051195 File Offset: 0x0004F395
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x0005119D File Offset: 0x0004F39D
		public EventLog EventLog
		{
			get
			{
				return this.event_log;
			}
			set
			{
				this.event_log = value;
			}
		}

		/// <summary>Gets or sets the name of this <see cref="T:System.Diagnostics.EventLogTraceListener" />.</summary>
		/// <returns>The name of this trace listener.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000511A6 File Offset: 0x0004F3A6
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x000511C2 File Offset: 0x0004F3C2
		public override string Name
		{
			get
			{
				if (this.name == null)
				{
					return this.event_log.Source;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Closes the event log so that it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001339 RID: 4921 RVA: 0x000511CB File Offset: 0x0004F3CB
		public override void Close()
		{
			this.event_log.Close();
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000511D8 File Offset: 0x0004F3D8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.event_log.Dispose();
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133B RID: 4923 RVA: 0x000511E8 File Offset: 0x0004F3E8
		public override void Write(string message)
		{
			this.TraceData(new TraceEventCache(), this.event_log.Source, TraceEventType.Information, 0, message);
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133C RID: 4924 RVA: 0x00051203 File Offset: 0x0004F403
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		/// <summary>Writes trace information, a data object, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x0600133D RID: 4925 RVA: 0x0005120C File Offset: 0x0004F40C
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, object data)
		{
			EventLogEntryType eventLogEntryType;
			if (severity - TraceEventType.Critical > 1)
			{
				if (severity != TraceEventType.Warning)
				{
					eventLogEntryType = EventLogEntryType.Information;
				}
				else
				{
					eventLogEntryType = EventLogEntryType.Warning;
				}
			}
			else
			{
				eventLogEntryType = EventLogEntryType.Error;
			}
			this.event_log.WriteEntry((data != null) ? data.ToString() : string.Empty, eventLogEntryType, id, 0);
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">An array of data objects.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x0600133E RID: 4926 RVA: 0x00051250 File Offset: 0x0004F450
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, params object[] data)
		{
			string text = string.Empty;
			if (data != null)
			{
				string[] array = new string[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					array[i] = ((data[i] != null) ? data[i].ToString() : string.Empty);
				}
				text = string.Join(", ", array);
			}
			this.TraceData(eventCache, source, severity, id, text);
		}

		/// <summary>Writes trace information, a message, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="message">The trace message.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133F RID: 4927 RVA: 0x000512B1 File Offset: 0x0004F4B1
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			this.TraceData(eventCache, source, severity, id, message);
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.-or-The log entry string exceeds 32,766 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001340 RID: 4928 RVA: 0x000512C0 File Offset: 0x0004F4C0
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, (format != null) ? string.Format(format, args) : null);
		}

		// Token: 0x04000AFE RID: 2814
		private EventLog event_log;

		// Token: 0x04000AFF RID: 2815
		private string name;
	}
}
