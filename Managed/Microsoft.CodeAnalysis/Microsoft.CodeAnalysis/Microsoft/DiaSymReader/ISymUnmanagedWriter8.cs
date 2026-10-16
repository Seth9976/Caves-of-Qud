using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000FC RID: 252
	[InterfaceType(1)]
	[Guid("5ba52f3b-6bf8-40fc-b476-d39c529b331e")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface ISymUnmanagedWriter8 : ISymUnmanagedWriter5
	{
		// Token: 0x060008AD RID: 2221
		void _VtblGap1_33();

		// Token: 0x060008AE RID: 2222
		void InitializeDeterministic([MarshalAs(25)] object emitter, [MarshalAs(25)] object stream);

		// Token: 0x060008AF RID: 2223
		unsafe void UpdateSignatureByHashingContent([In] byte* buffer, int size);

		// Token: 0x060008B0 RID: 2224
		void UpdateSignature(Guid pdbId, uint stamp, int age);

		// Token: 0x060008B1 RID: 2225
		unsafe void SetSourceServerData([In] byte* data, int size);

		// Token: 0x060008B2 RID: 2226
		unsafe void SetSourceLinkData([In] byte* data, int size);
	}
}
