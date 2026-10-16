using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000531 RID: 1329
	public enum SerialError
	{
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x04001735 RID: 5941
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x04001736 RID: 5942
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x04001737 RID: 5943
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x04001738 RID: 5944
		Frame = 8,
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x04001739 RID: 5945
		TXFull = 256
	}
}
