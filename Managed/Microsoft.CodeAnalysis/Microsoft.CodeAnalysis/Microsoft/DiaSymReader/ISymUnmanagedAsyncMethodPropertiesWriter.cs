using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F8 RID: 248
	[InterfaceType(1)]
	[Guid("FC073774-1739-4232-BD56-A027294BEC15")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface ISymUnmanagedAsyncMethodPropertiesWriter
	{
		// Token: 0x06000882 RID: 2178
		void DefineKickoffMethod(int kickoffMethod);

		// Token: 0x06000883 RID: 2179
		void DefineCatchHandlerILOffset(int catchHandlerOffset);

		// Token: 0x06000884 RID: 2180
		unsafe void DefineAsyncStepInfo(int count, int* yieldOffsets, int* breakpointOffset, int* breakpointMethod);
	}
}
