using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020007B1 RID: 1969
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x0400253F RID: 9535
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x04002540 RID: 9536
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x04002541 RID: 9537
		SelectError
	}
}
