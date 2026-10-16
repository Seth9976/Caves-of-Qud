using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000FA RID: 250
	[InterfaceType(1)]
	[Guid("98ECEE1E-752D-11d3-8D56-00C04F680B2B")]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IPdbWriter
	{
		// Token: 0x06000887 RID: 2183
		int __SetPath();

		// Token: 0x06000888 RID: 2184
		int __OpenMod();

		// Token: 0x06000889 RID: 2185
		int __CloseMod();

		// Token: 0x0600088A RID: 2186
		int __GetPath();

		// Token: 0x0600088B RID: 2187
		void GetSignatureAge(out uint sig, out int age);
	}
}
