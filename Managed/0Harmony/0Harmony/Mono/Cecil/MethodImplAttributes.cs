using System;

namespace Mono.Cecil
{
	// Token: 0x0200023A RID: 570
	[Flags]
	public enum MethodImplAttributes : ushort
	{
		// Token: 0x040003C2 RID: 962
		CodeTypeMask = 3,
		// Token: 0x040003C3 RID: 963
		IL = 0,
		// Token: 0x040003C4 RID: 964
		Native = 1,
		// Token: 0x040003C5 RID: 965
		OPTIL = 2,
		// Token: 0x040003C6 RID: 966
		Runtime = 3,
		// Token: 0x040003C7 RID: 967
		ManagedMask = 4,
		// Token: 0x040003C8 RID: 968
		Unmanaged = 4,
		// Token: 0x040003C9 RID: 969
		Managed = 0,
		// Token: 0x040003CA RID: 970
		ForwardRef = 16,
		// Token: 0x040003CB RID: 971
		PreserveSig = 128,
		// Token: 0x040003CC RID: 972
		InternalCall = 4096,
		// Token: 0x040003CD RID: 973
		Synchronized = 32,
		// Token: 0x040003CE RID: 974
		NoOptimization = 64,
		// Token: 0x040003CF RID: 975
		NoInlining = 8,
		// Token: 0x040003D0 RID: 976
		AggressiveInlining = 256
	}
}
