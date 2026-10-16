using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x02000532 RID: 1330
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x06002AA6 RID: 10918 RVA: 0x00092E15 File Offset: 0x00091015
		internal SerialErrorReceivedEventArgs(SerialError eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x00092E24 File Offset: 0x00091024
		public SerialError EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal SerialErrorReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400173A RID: 5946
		private SerialError eventType;
	}
}
