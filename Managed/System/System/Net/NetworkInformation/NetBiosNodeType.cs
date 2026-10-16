using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x0200070F RID: 1807
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x0400221D RID: 8733
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x0400221E RID: 8734
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x0400221F RID: 8735
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x04002220 RID: 8736
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x04002221 RID: 8737
		Hybrid = 8
	}
}
