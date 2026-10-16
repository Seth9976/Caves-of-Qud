using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[NativeType(CodegenOptions.Custom, "ScriptingJNINativeMethod")]
	public struct JNINativeMethod
	{
		// Token: 0x0400001F RID: 31
		public string name;

		// Token: 0x04000020 RID: 32
		public string signature;

		// Token: 0x04000021 RID: 33
		public IntPtr fnPtr;
	}
}
