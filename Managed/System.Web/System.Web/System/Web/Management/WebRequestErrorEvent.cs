using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the event that carries information about Web-request errors.</summary>
	// Token: 0x020006DC RID: 1756
	public class WebRequestErrorEvent : WebBaseErrorEvent
	{
		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestErrorEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The identifier associated with the event. It must be greater than the <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" /> field constant.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error.</param>
		// Token: 0x06004A77 RID: 19063 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the <see cref="T:System.Web.Management.WebRequestErrorEvent" /> class with specified event parameters.</summary>
		/// <param name="message">The event description.</param>
		/// <param name="eventSource">The object that is the source of the event.</param>
		/// <param name="eventCode">The identifier associated with the event. It must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" /> field constant.</param>
		/// <param name="eventDetailCode">The event detail code identifier.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> associated with the error. </param>
		// Token: 0x06004A78 RID: 19064 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal WebRequestErrorEvent(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application request information.</summary>
		/// <returns>The application request information.</returns>
		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebRequestInformation RequestInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the application thread information.</summary>
		/// <returns>The application thread information.</returns>
		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebThreadInformation ThreadInformation
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally to increment the relevant performance counters.</summary>
		// Token: 0x06004A7B RID: 19067 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
