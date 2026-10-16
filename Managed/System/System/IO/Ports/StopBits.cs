using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200053C RID: 1340
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported by the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property. </summary>
		// Token: 0x0400176A RID: 5994
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x0400176B RID: 5995
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x0400176C RID: 5996
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x0400176D RID: 5997
		OnePointFive
	}
}
