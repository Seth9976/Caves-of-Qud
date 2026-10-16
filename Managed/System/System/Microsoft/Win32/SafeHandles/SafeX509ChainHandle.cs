using System;
using Unity;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000137 RID: 311
	public sealed class SafeX509ChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00013BA0 File Offset: 0x00011DA0
		internal SafeX509ChainHandle(IntPtr handle)
			: base(true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override bool ReleaseHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal SafeX509ChainHandle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
