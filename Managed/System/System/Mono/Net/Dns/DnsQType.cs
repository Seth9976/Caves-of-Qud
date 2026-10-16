using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BC RID: 188
	internal enum DnsQType : ushort
	{
		// Token: 0x040002DE RID: 734
		A = 1,
		// Token: 0x040002DF RID: 735
		NS,
		// Token: 0x040002E0 RID: 736
		[Obsolete]
		MD,
		// Token: 0x040002E1 RID: 737
		[Obsolete]
		MF,
		// Token: 0x040002E2 RID: 738
		CNAME,
		// Token: 0x040002E3 RID: 739
		SOA,
		// Token: 0x040002E4 RID: 740
		[Obsolete]
		MB,
		// Token: 0x040002E5 RID: 741
		[Obsolete]
		MG,
		// Token: 0x040002E6 RID: 742
		[Obsolete]
		MR,
		// Token: 0x040002E7 RID: 743
		[Obsolete]
		NULL,
		// Token: 0x040002E8 RID: 744
		[Obsolete]
		WKS,
		// Token: 0x040002E9 RID: 745
		PTR,
		// Token: 0x040002EA RID: 746
		[Obsolete]
		HINFO,
		// Token: 0x040002EB RID: 747
		[Obsolete]
		MINFO,
		// Token: 0x040002EC RID: 748
		MX,
		// Token: 0x040002ED RID: 749
		TXT,
		// Token: 0x040002EE RID: 750
		[Obsolete]
		RP,
		// Token: 0x040002EF RID: 751
		AFSDB,
		// Token: 0x040002F0 RID: 752
		[Obsolete]
		X25,
		// Token: 0x040002F1 RID: 753
		[Obsolete]
		ISDN,
		// Token: 0x040002F2 RID: 754
		[Obsolete]
		RT,
		// Token: 0x040002F3 RID: 755
		[Obsolete]
		NSAP,
		// Token: 0x040002F4 RID: 756
		[Obsolete]
		NSAPPTR,
		// Token: 0x040002F5 RID: 757
		SIG,
		// Token: 0x040002F6 RID: 758
		KEY,
		// Token: 0x040002F7 RID: 759
		[Obsolete]
		PX,
		// Token: 0x040002F8 RID: 760
		[Obsolete]
		GPOS,
		// Token: 0x040002F9 RID: 761
		AAAA,
		// Token: 0x040002FA RID: 762
		LOC,
		// Token: 0x040002FB RID: 763
		[Obsolete]
		NXT,
		// Token: 0x040002FC RID: 764
		[Obsolete]
		EID,
		// Token: 0x040002FD RID: 765
		[Obsolete]
		NIMLOC,
		// Token: 0x040002FE RID: 766
		SRV,
		// Token: 0x040002FF RID: 767
		[Obsolete]
		ATMA,
		// Token: 0x04000300 RID: 768
		NAPTR,
		// Token: 0x04000301 RID: 769
		KX,
		// Token: 0x04000302 RID: 770
		CERT,
		// Token: 0x04000303 RID: 771
		[Obsolete]
		A6,
		// Token: 0x04000304 RID: 772
		DNAME,
		// Token: 0x04000305 RID: 773
		[Obsolete]
		SINK,
		// Token: 0x04000306 RID: 774
		OPT,
		// Token: 0x04000307 RID: 775
		[Obsolete]
		APL,
		// Token: 0x04000308 RID: 776
		DS,
		// Token: 0x04000309 RID: 777
		SSHFP,
		// Token: 0x0400030A RID: 778
		IPSECKEY,
		// Token: 0x0400030B RID: 779
		RRSIG,
		// Token: 0x0400030C RID: 780
		NSEC,
		// Token: 0x0400030D RID: 781
		DNSKEY,
		// Token: 0x0400030E RID: 782
		DHCID,
		// Token: 0x0400030F RID: 783
		NSEC3,
		// Token: 0x04000310 RID: 784
		NSEC3PARAM,
		// Token: 0x04000311 RID: 785
		HIP = 55,
		// Token: 0x04000312 RID: 786
		NINFO,
		// Token: 0x04000313 RID: 787
		RKEY,
		// Token: 0x04000314 RID: 788
		TALINK,
		// Token: 0x04000315 RID: 789
		SPF = 99,
		// Token: 0x04000316 RID: 790
		[Obsolete]
		UINFO,
		// Token: 0x04000317 RID: 791
		[Obsolete]
		UID,
		// Token: 0x04000318 RID: 792
		[Obsolete]
		GID,
		// Token: 0x04000319 RID: 793
		[Obsolete]
		UNSPEC,
		// Token: 0x0400031A RID: 794
		TKEY = 249,
		// Token: 0x0400031B RID: 795
		TSIG,
		// Token: 0x0400031C RID: 796
		IXFR,
		// Token: 0x0400031D RID: 797
		AXFR,
		// Token: 0x0400031E RID: 798
		[Obsolete]
		MAILB,
		// Token: 0x0400031F RID: 799
		[Obsolete]
		MAILA,
		// Token: 0x04000320 RID: 800
		ALL,
		// Token: 0x04000321 RID: 801
		URI,
		// Token: 0x04000322 RID: 802
		TA = 32768,
		// Token: 0x04000323 RID: 803
		DLV
	}
}
