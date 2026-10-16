using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020007A8 RID: 1960
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x040024B4 RID: 9396
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x040024B5 RID: 9397
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x040024B6 RID: 9398
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x040024B7 RID: 9399
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x040024B8 RID: 9400
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x040024B9 RID: 9401
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x040024BA RID: 9402
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x040024BB RID: 9403
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x040024BC RID: 9404
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x040024BD RID: 9405
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x040024BE RID: 9406
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x040024BF RID: 9407
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x040024C0 RID: 9408
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x040024C1 RID: 9409
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x040024C2 RID: 9410
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x040024C3 RID: 9411
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x040024C4 RID: 9412
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x040024C5 RID: 9413
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x040024C6 RID: 9414
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x040024C7 RID: 9415
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x040024C8 RID: 9416
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x040024C9 RID: 9417
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x040024CA RID: 9418
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x040024CB RID: 9419
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x040024CC RID: 9420
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x040024CD RID: 9421
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x040024CE RID: 9422
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x040024CF RID: 9423
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x040024D0 RID: 9424
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x040024D1 RID: 9425
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x040024D2 RID: 9426
		Max
	}
}
