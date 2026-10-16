using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C7 RID: 199
	internal enum DnsType : ushort
	{
		// Token: 0x0400034C RID: 844
		A = 1,
		// Token: 0x0400034D RID: 845
		NS,
		// Token: 0x0400034E RID: 846
		[Obsolete]
		MD,
		// Token: 0x0400034F RID: 847
		[Obsolete]
		MF,
		// Token: 0x04000350 RID: 848
		CNAME,
		// Token: 0x04000351 RID: 849
		SOA,
		// Token: 0x04000352 RID: 850
		[Obsolete]
		MB,
		// Token: 0x04000353 RID: 851
		[Obsolete]
		MG,
		// Token: 0x04000354 RID: 852
		[Obsolete]
		MR,
		// Token: 0x04000355 RID: 853
		[Obsolete]
		NULL,
		// Token: 0x04000356 RID: 854
		[Obsolete]
		WKS,
		// Token: 0x04000357 RID: 855
		PTR,
		// Token: 0x04000358 RID: 856
		[Obsolete]
		HINFO,
		// Token: 0x04000359 RID: 857
		[Obsolete]
		MINFO,
		// Token: 0x0400035A RID: 858
		MX,
		// Token: 0x0400035B RID: 859
		TXT,
		// Token: 0x0400035C RID: 860
		[Obsolete]
		RP,
		// Token: 0x0400035D RID: 861
		AFSDB,
		// Token: 0x0400035E RID: 862
		[Obsolete]
		X25,
		// Token: 0x0400035F RID: 863
		[Obsolete]
		ISDN,
		// Token: 0x04000360 RID: 864
		[Obsolete]
		RT,
		// Token: 0x04000361 RID: 865
		[Obsolete]
		NSAP,
		// Token: 0x04000362 RID: 866
		[Obsolete]
		NSAPPTR,
		// Token: 0x04000363 RID: 867
		SIG,
		// Token: 0x04000364 RID: 868
		KEY,
		// Token: 0x04000365 RID: 869
		[Obsolete]
		PX,
		// Token: 0x04000366 RID: 870
		[Obsolete]
		GPOS,
		// Token: 0x04000367 RID: 871
		AAAA,
		// Token: 0x04000368 RID: 872
		LOC,
		// Token: 0x04000369 RID: 873
		[Obsolete]
		NXT,
		// Token: 0x0400036A RID: 874
		[Obsolete]
		EID,
		// Token: 0x0400036B RID: 875
		[Obsolete]
		NIMLOC,
		// Token: 0x0400036C RID: 876
		SRV,
		// Token: 0x0400036D RID: 877
		[Obsolete]
		ATMA,
		// Token: 0x0400036E RID: 878
		NAPTR,
		// Token: 0x0400036F RID: 879
		KX,
		// Token: 0x04000370 RID: 880
		CERT,
		// Token: 0x04000371 RID: 881
		[Obsolete]
		A6,
		// Token: 0x04000372 RID: 882
		DNAME,
		// Token: 0x04000373 RID: 883
		[Obsolete]
		SINK,
		// Token: 0x04000374 RID: 884
		OPT,
		// Token: 0x04000375 RID: 885
		[Obsolete]
		APL,
		// Token: 0x04000376 RID: 886
		DS,
		// Token: 0x04000377 RID: 887
		SSHFP,
		// Token: 0x04000378 RID: 888
		IPSECKEY,
		// Token: 0x04000379 RID: 889
		RRSIG,
		// Token: 0x0400037A RID: 890
		NSEC,
		// Token: 0x0400037B RID: 891
		DNSKEY,
		// Token: 0x0400037C RID: 892
		DHCID,
		// Token: 0x0400037D RID: 893
		NSEC3,
		// Token: 0x0400037E RID: 894
		NSEC3PARAM,
		// Token: 0x0400037F RID: 895
		HIP = 55,
		// Token: 0x04000380 RID: 896
		NINFO,
		// Token: 0x04000381 RID: 897
		RKEY,
		// Token: 0x04000382 RID: 898
		TALINK,
		// Token: 0x04000383 RID: 899
		SPF = 99,
		// Token: 0x04000384 RID: 900
		[Obsolete]
		UINFO,
		// Token: 0x04000385 RID: 901
		[Obsolete]
		UID,
		// Token: 0x04000386 RID: 902
		[Obsolete]
		GID,
		// Token: 0x04000387 RID: 903
		[Obsolete]
		UNSPEC,
		// Token: 0x04000388 RID: 904
		TKEY = 249,
		// Token: 0x04000389 RID: 905
		TSIG,
		// Token: 0x0400038A RID: 906
		IXFR,
		// Token: 0x0400038B RID: 907
		AXFR,
		// Token: 0x0400038C RID: 908
		[Obsolete]
		MAILB,
		// Token: 0x0400038D RID: 909
		[Obsolete]
		MAILA,
		// Token: 0x0400038E RID: 910
		URI = 256,
		// Token: 0x0400038F RID: 911
		TA = 32768,
		// Token: 0x04000390 RID: 912
		DLV
	}
}
