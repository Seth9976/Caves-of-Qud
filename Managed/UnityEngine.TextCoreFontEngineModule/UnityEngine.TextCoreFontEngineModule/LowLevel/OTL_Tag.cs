using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	internal struct OTL_Tag
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000058F8 File Offset: 0x00003AF8
		public unsafe override string ToString()
		{
			char* ptr = stackalloc char[(UIntPtr)8];
			*ptr = (char)this.c0;
			ptr[1] = (char)this.c1;
			ptr[2] = (char)this.c2;
			ptr[3] = (char)this.c3;
			return new string(ptr);
		}

		// Token: 0x0400008E RID: 142
		public byte c0;

		// Token: 0x0400008F RID: 143
		public byte c1;

		// Token: 0x04000090 RID: 144
		public byte c2;

		// Token: 0x04000091 RID: 145
		public byte c3;

		// Token: 0x04000092 RID: 146
		public byte c4;
	}
}
