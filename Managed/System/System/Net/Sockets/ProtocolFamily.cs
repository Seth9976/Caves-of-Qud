using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020007AF RID: 1967
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04002505 RID: 9477
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04002506 RID: 9478
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x04002507 RID: 9479
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x04002508 RID: 9480
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x04002509 RID: 9481
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x0400250A RID: 9482
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x0400250B RID: 9483
		Chaos,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x0400250C RID: 9484
		NS,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x0400250D RID: 9485
		Ipx = 6,
		/// <summary>ISO protocol.</summary>
		// Token: 0x0400250E RID: 9486
		Iso,
		/// <summary>OSI protocol.</summary>
		// Token: 0x0400250F RID: 9487
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x04002510 RID: 9488
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x04002511 RID: 9489
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x04002512 RID: 9490
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x04002513 RID: 9491
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x04002514 RID: 9492
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x04002515 RID: 9493
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x04002516 RID: 9494
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x04002517 RID: 9495
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x04002518 RID: 9496
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x04002519 RID: 9497
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x0400251A RID: 9498
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x0400251B RID: 9499
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x0400251C RID: 9500
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x0400251D RID: 9501
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x0400251E RID: 9502
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x0400251F RID: 9503
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x04002520 RID: 9504
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x04002521 RID: 9505
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x04002522 RID: 9506
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x04002523 RID: 9507
		Max
	}
}
