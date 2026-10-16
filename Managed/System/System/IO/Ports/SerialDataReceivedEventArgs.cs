using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x0200053A RID: 1338
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x00093EBE File Offset: 0x000920BE
		internal SerialDataReceivedEventArgs(SerialData eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00093ECD File Offset: 0x000920CD
		public SerialData EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal SerialDataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001761 RID: 5985
		private SerialData eventType;
	}
}
