using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	internal sealed class UnmanagedCallersOnlyAttribute : Attribute
	{
		// Token: 0x04000002 RID: 2
		[Nullable(new byte[] { 2, 1 })]
		public Type[] CallConvs;

		// Token: 0x04000003 RID: 3
		[Nullable(2)]
		public string EntryPoint;
	}
}
