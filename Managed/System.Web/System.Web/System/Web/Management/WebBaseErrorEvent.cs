using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Serves as the base class for all the health-monitoring error events.</summary>
	// Token: 0x020006DD RID: 1757
	public class WebBaseErrorEvent : WebManagementEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseErrorEvent" /> class.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="e">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004A7C RID: 19068 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, Exception e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseErrorEvent" /> class.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />.</param>
		/// <param name="eventDetailCode">The detailed identifier for the event.</param>
		/// <param name="e">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004A7D RID: 19069 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebBaseErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> associated with the error. </summary>
		/// <returns>The <see cref="T:System.Exception" /> associated with the error.</returns>
		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Exception ErrorException
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Increments the event-error performance-related counters.</summary>
		// Token: 0x06004A7F RID: 19071 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
