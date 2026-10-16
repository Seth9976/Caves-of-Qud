using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for the ASP.NET health-monitoring events.</summary>
	// Token: 0x0200052B RID: 1323
	public class WebBaseEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The description of the event. </param>
		/// <param name="eventSource">The object that raised the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		// Token: 0x060039FE RID: 14846 RVA: 0x0009ACC0 File Offset: 0x00098EC0
		protected WebBaseEvent(string message, object eventSource, int eventCode)
		{
			this.message = message;
			this.event_source = eventSource;
			this.event_code = eventCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The description of the raised event. </param>
		/// <param name="eventSource">The object that raised the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x060039FF RID: 14847 RVA: 0x0009ACDD File Offset: 0x00098EDD
		protected WebBaseEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			this.message = message;
			this.event_source = eventSource;
			this.event_code = eventCode;
			this.event_detail_code = eventDetailCode;
		}

		/// <summary>Gets a <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the current application being monitored.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the application being monitored.</returns>
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x00003A2B File Offset: 0x00001C2B
		public static WebApplicationInformation ApplicationInformation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the code value associated with the event.</summary>
		/// <returns>One of the <see cref="T:System.Web.Management.WebEventCodes" /> values.</returns>
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x0009AD02 File Offset: 0x00098F02
		public int EventCode
		{
			get
			{
				return this.event_code;
			}
		}

		/// <summary>Gets the event detail code.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</returns>
		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x0009AD0A File Offset: 0x00098F0A
		public int EventDetailCode
		{
			get
			{
				return this.event_detail_code;
			}
		}

		/// <summary>Gets the identifier associated with the event.</summary>
		/// <returns>The identifier associated with the event.</returns>
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x00003A2B File Offset: 0x00001C2B
		public Guid EventID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of times the event has been raised by the application.</summary>
		/// <returns>The number of times the event has been raised.</returns>
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x00003A2B File Offset: 0x00001C2B
		public long EventSequence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the object that raises the event.</summary>
		/// <returns>The object that raised the event.</returns>
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x0009AD12 File Offset: 0x00098F12
		public object EventSource
		{
			get
			{
				return this.event_source;
			}
		}

		/// <summary>Gets the time when the event was raised.</summary>
		/// <returns>The time that the event was raised.</returns>
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x00003A2B File Offset: 0x00001C2B
		public DateTime EventTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time when the event was raised.</summary>
		/// <returns>The time of the event in Coordinated Universal Time (UTC) format.</returns>
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x00003A2B File Offset: 0x00001C2B
		public DateTime EventTimeUtc
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the message that describes the event.</summary>
		/// <returns>The description of the event.</returns>
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x0009AD1A File Offset: 0x00098F1A
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Provides standard formatting of the event information.</summary>
		/// <param name="formatter">A <see cref="T:System.Web.Management.WebEventFormatter" /> object that contains the formatted event information.</param>
		// Token: 0x06003A09 RID: 14857 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual void FormatCustomEventDetails(WebEventFormatter formatter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises an event by notifying any configured provider that the event has occurred.</summary>
		// Token: 0x06003A0A RID: 14858 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual void Raise()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the specified event by notifying any configured provider that the event has occurred.</summary>
		/// <param name="eventRaised">A <see cref="T:System.Web.Management.WebBaseEvent" /> object. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Management.WebBaseEvent.EventCode" /> property of <paramref name="eventRaised" /> has a value that is reserved for ASP.NET.</exception>
		// Token: 0x06003A0B RID: 14859 RVA: 0x00003A2B File Offset: 0x00001C2B
		public static void Raise(WebBaseEvent eventRaised)
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x06003A0C RID: 14860 RVA: 0x00003A2B File Offset: 0x00001C2B
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		/// <param name="includeAppInfo">true if standard application information must be displayed as part of the event information; otherwise, false. </param>
		/// <param name="includeCustomEventDetails">true if custom information must be displayed as part of the event information; otherwise, false.</param>
		// Token: 0x06003A0D RID: 14861 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual string ToString(bool includeAppInfo, bool includeCustomEventDetails)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a counter that represents the number of times the event has occurred.</summary>
		/// <returns>A counter that represents the number of times the event has occurred.</returns>
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x0009AD24 File Offset: 0x00098F24
		public long EventOccurrence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Used internally to increment performance counters.</summary>
		// Token: 0x06003A0F RID: 14863 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F6E RID: 8046
		private string message;

		// Token: 0x04001F6F RID: 8047
		private object event_source;

		// Token: 0x04001F70 RID: 8048
		private int event_code;

		// Token: 0x04001F71 RID: 8049
		private int event_detail_code;
	}
}
