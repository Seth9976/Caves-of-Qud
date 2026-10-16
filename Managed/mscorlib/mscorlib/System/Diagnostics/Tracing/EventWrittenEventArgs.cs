using System;
using System.Collections.ObjectModel;
using Unity;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides data for the <see cref="M:System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)" /> callback.</summary>
	// Token: 0x020009FC RID: 2556
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x06005B22 RID: 23330 RVA: 0x00134886 File Offset: 0x00132A86
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.EventSource = eventSource;
		}

		/// <summary>Gets the activity ID on the thread that the event was written to. </summary>
		/// <returns>The activity ID on the thread that the event was written to. </returns>
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x00134895 File Offset: 0x00132A95
		public Guid ActivityId
		{
			get
			{
				return EventSource.CurrentThreadActivityId;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public EventChannel Channel
		{
			get
			{
				return EventChannel.None;
			}
		}

		/// <summary>Gets the event identifier.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005B25 RID: 23333 RVA: 0x0013489C File Offset: 0x00132A9C
		// (set) Token: 0x06005B26 RID: 23334 RVA: 0x001348A4 File Offset: 0x00132AA4
		public int EventId { get; internal set; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x001348AD File Offset: 0x00132AAD
		// (set) Token: 0x06005B28 RID: 23336 RVA: 0x001348B5 File Offset: 0x00132AB5
		public long OSThreadId { get; internal set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x001348BE File Offset: 0x00132ABE
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x001348C6 File Offset: 0x00132AC6
		public DateTime TimeStamp { get; internal set; }

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x001348CF File Offset: 0x00132ACF
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x001348D7 File Offset: 0x00132AD7
		public string EventName { get; internal set; }

		/// <summary>Gets the event source object.</summary>
		/// <returns>The event source object.</returns>
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x001348E0 File Offset: 0x00132AE0
		// (set) Token: 0x06005B2E RID: 23342 RVA: 0x001348E8 File Offset: 0x00132AE8
		public EventSource EventSource { get; private set; }

		/// <summary>Gets the keywords for the event.</summary>
		/// <returns>The keywords for the event.</returns>
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x0005CD4E File Offset: 0x0005AF4E
		public EventKeywords Keywords
		{
			get
			{
				return EventKeywords.None;
			}
		}

		/// <summary>Gets the level of the event.</summary>
		/// <returns>The level of the event.</returns>
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public EventLevel Level
		{
			get
			{
				return EventLevel.LogAlways;
			}
		}

		/// <summary>Gets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x001348F1 File Offset: 0x00132AF1
		// (set) Token: 0x06005B32 RID: 23346 RVA: 0x001348F9 File Offset: 0x00132AF9
		public string Message { get; internal set; }

		/// <summary>Gets the operation code for the event.</summary>
		/// <returns>The operation code for the event.</returns>
		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005B33 RID: 23347 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public EventOpcode Opcode
		{
			get
			{
				return EventOpcode.Info;
			}
		}

		/// <summary>Gets the payload for the event.</summary>
		/// <returns>The payload for the event.</returns>
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x00134902 File Offset: 0x00132B02
		// (set) Token: 0x06005B35 RID: 23349 RVA: 0x0013490A File Offset: 0x00132B0A
		public ReadOnlyCollection<object> Payload { get; internal set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x00134913 File Offset: 0x00132B13
		// (set) Token: 0x06005B37 RID: 23351 RVA: 0x0013491B File Offset: 0x00132B1B
		public ReadOnlyCollection<string> PayloadNames { get; internal set; }

		/// <summary>Gets the identifier of an activity that is related to the activity represented by the current instance. </summary>
		/// <returns>The identifier of the related activity, or <see cref="F:System.Guid.Empty" /> if there is no related activity.</returns>
		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005B38 RID: 23352 RVA: 0x00134924 File Offset: 0x00132B24
		// (set) Token: 0x06005B39 RID: 23353 RVA: 0x0013492C File Offset: 0x00132B2C
		public Guid RelatedActivityId { get; internal set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public EventTags Tags
		{
			get
			{
				return EventTags.None;
			}
		}

		/// <summary>Gets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public EventTask Task
		{
			get
			{
				return EventTask.None;
			}
		}

		/// <summary>Gets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public byte Version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x000173AD File Offset: 0x000155AD
		internal EventWrittenEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
