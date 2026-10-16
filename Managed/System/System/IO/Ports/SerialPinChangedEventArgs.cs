using System;
using Unity;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x02000534 RID: 1332
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06002AA9 RID: 10921 RVA: 0x00092E2C File Offset: 0x0009102C
		internal SerialPinChangedEventArgs(SerialPinChange eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x00092E3B File Offset: 0x0009103B
		public SerialPinChange EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal SerialPinChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001741 RID: 5953
		private SerialPinChange eventType;
	}
}
