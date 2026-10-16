using System;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x02000468 RID: 1128
	[NativeType(CodegenOptions.Custom, "ManagedCoveredSequencePoint", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredSequencePoint
	{
		// Token: 0x04000C4B RID: 3147
		public MethodBase method;

		// Token: 0x04000C4C RID: 3148
		public uint ilOffset;

		// Token: 0x04000C4D RID: 3149
		public uint hitCount;

		// Token: 0x04000C4E RID: 3150
		public string filename;

		// Token: 0x04000C4F RID: 3151
		public uint line;

		// Token: 0x04000C50 RID: 3152
		public uint column;
	}
}
