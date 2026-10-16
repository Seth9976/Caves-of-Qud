using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x020002F7 RID: 759
[CompilerGenerated]
internal sealed class <494e73b6-7487-4f93-b283-2ffe144be45f><PrivateImplementationDetails>
{
	// Token: 0x0600131F RID: 4895 RVA: 0x0003D7EC File Offset: 0x0003B9EC
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619U;
			}
		}
		return num;
	}

	// Token: 0x040009B3 RID: 2483 RVA: 0x00058B88 File Offset: 0x00056D88
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '-8511746012302509385'.
	internal static readonly long 1B960802B155541DF3837ADE50790DA7E91762D14B8E011FA8223424FF75ACDB;

	// Token: 0x040009B4 RID: 2484 RVA: 0x00058B90 File Offset: 0x00056D90
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <494e73b6-7487-4f93-b283-2ffe144be45f><PrivateImplementationDetails>.__StaticArrayInitTypeSize=1790 2EF0065A03764C27AE8D5DC3002E10F0426E43BDFA7D8ECFFF633E45DD32376B;

	// Token: 0x040009B5 RID: 2485 RVA: 0x0005928E File Offset: 0x0005748E
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <494e73b6-7487-4f93-b283-2ffe144be45f><PrivateImplementationDetails>.__StaticArrayInitTypeSize=160 933598639CBAA1DE502F80D2FD1DB78F13C8D7BB64A5FDC1BC73AC0B5CE4F5CA;

	// Token: 0x040009B6 RID: 2486 RVA: 0x0005932E File Offset: 0x0005752E
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4182389475095035824'.
	internal static readonly long 971150DD73DC318E68A98CCE9B91AC7DEA2D43C562B4F5A9A2F4272C7E29477E;

	// Token: 0x040009B7 RID: 2487 RVA: 0x00059336 File Offset: 0x00057536
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <494e73b6-7487-4f93-b283-2ffe144be45f><PrivateImplementationDetails>.__StaticArrayInitTypeSize=128 BFDF5E72651B4EC588BD5FC6A9F17E9E0972248146BBACC10478F48D72F29B81;

	// Token: 0x020002F8 RID: 760
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 128)]
	private struct __StaticArrayInitTypeSize=128
	{
	}

	// Token: 0x020002F9 RID: 761
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 160)]
	private struct __StaticArrayInitTypeSize=160
	{
	}

	// Token: 0x020002FA RID: 762
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1790)]
	private struct __StaticArrayInitTypeSize=1790
	{
	}
}
