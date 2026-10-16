using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.Cci
{
	// Token: 0x0200007C RID: 124
	internal static class Constants
	{
		// Token: 0x04000126 RID: 294
		public const CharSet CharSet_None = 1;

		// Token: 0x04000127 RID: 295
		public const CharSet CharSet_Auto = 4;

		// Token: 0x04000128 RID: 296
		public const CallingConvention CallingConvention_FastCall = 5;

		// Token: 0x04000129 RID: 297
		public const UnmanagedType UnmanagedType_CustomMarshaler = 44;

		// Token: 0x0400012A RID: 298
		public const UnmanagedType UnmanagedType_IDispatch = 26;

		// Token: 0x0400012B RID: 299
		public const UnmanagedType UnmanagedType_SafeArray = 29;

		// Token: 0x0400012C RID: 300
		public const UnmanagedType UnmanagedType_VBByRefStr = 34;

		// Token: 0x0400012D RID: 301
		public const UnmanagedType UnmanagedType_AnsiBStr = 35;

		// Token: 0x0400012E RID: 302
		public const UnmanagedType UnmanagedType_TBStr = 36;

		// Token: 0x0400012F RID: 303
		public const ComInterfaceType ComInterfaceType_InterfaceIsDual = 0;

		// Token: 0x04000130 RID: 304
		public const ComInterfaceType ComInterfaceType_InterfaceIsIDispatch = 2;

		// Token: 0x04000131 RID: 305
		public const ClassInterfaceType ClassInterfaceType_AutoDispatch = 1;

		// Token: 0x04000132 RID: 306
		public const ClassInterfaceType ClassInterfaceType_AutoDual = 2;

		// Token: 0x04000133 RID: 307
		public const int CompilationRelaxations_NoStringInterning = 8;

		// Token: 0x04000134 RID: 308
		public const TypeAttributes TypeAttributes_TypeForwarder = 2097152;
	}
}
