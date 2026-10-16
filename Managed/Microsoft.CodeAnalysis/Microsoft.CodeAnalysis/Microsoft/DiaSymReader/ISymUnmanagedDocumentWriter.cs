using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F9 RID: 249
	[InterfaceType(1)]
	[Guid("B01FAFEB-C450-3A4D-BEEC-B4CEEC01E006")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface ISymUnmanagedDocumentWriter
	{
		// Token: 0x06000885 RID: 2181
		unsafe void SetSource(uint sourceSize, byte* source);

		// Token: 0x06000886 RID: 2182
		unsafe void SetCheckSum(Guid algorithmId, uint checkSumSize, byte* checkSum);
	}
}
