using System;

namespace Mono.Cecil
{
	// Token: 0x02000237 RID: 567
	public enum MethodCallingConvention : byte
	{
		// Token: 0x040003A8 RID: 936
		Default,
		// Token: 0x040003A9 RID: 937
		C,
		// Token: 0x040003AA RID: 938
		StdCall,
		// Token: 0x040003AB RID: 939
		ThisCall,
		// Token: 0x040003AC RID: 940
		FastCall,
		// Token: 0x040003AD RID: 941
		VarArg,
		// Token: 0x040003AE RID: 942
		Generic = 16
	}
}
