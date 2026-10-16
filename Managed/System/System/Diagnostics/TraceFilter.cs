using System;

namespace System.Diagnostics
{
	/// <summary>Provides the base class for trace filter implementations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000231 RID: 561
	public abstract class TraceFilter
	{
		/// <summary>When overridden in a derived class, determines whether the trace listener should trace the event.</summary>
		/// <returns>true to trace the specified event; otherwise, false. </returns>
		/// <param name="cache">The <see cref="T:System.Diagnostics.TraceEventCache" /> that contains information for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">Either the format to use for writing an array of arguments specified by the <paramref name="args" /> parameter, or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001070 RID: 4208
		public abstract bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data);

		// Token: 0x06001071 RID: 4209 RVA: 0x00047440 File Offset: 0x00045640
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, null, null, null);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00047460 File Offset: 0x00045660
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, null, null);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00047480 File Offset: 0x00045680
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, null);
		}

		// Token: 0x04000A05 RID: 2565
		internal string initializeData;
	}
}
