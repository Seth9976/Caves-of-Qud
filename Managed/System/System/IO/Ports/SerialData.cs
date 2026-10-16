using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of character that was received on the serial port of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000530 RID: 1328
	public enum SerialData
	{
		/// <summary>A character was received and placed in the input buffer.</summary>
		// Token: 0x04001732 RID: 5938
		Chars = 1,
		/// <summary>The end of file character was received and placed in the input buffer.</summary>
		// Token: 0x04001733 RID: 5939
		Eof
	}
}
