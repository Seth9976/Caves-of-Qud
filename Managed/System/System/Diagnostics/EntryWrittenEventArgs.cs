using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000259 RID: 601
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06001279 RID: 4729 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
		public EntryWrittenEventArgs()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written. </param>
		// Token: 0x0600127A RID: 4730 RVA: 0x0004FBD1 File Offset: 0x0004DDD1
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04000AC9 RID: 2761
		private EventLogEntry entry;
	}
}
