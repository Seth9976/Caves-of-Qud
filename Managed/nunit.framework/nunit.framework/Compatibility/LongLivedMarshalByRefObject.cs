using System;
using System.Security;

namespace NUnit.Compatibility
{
	// Token: 0x02000005 RID: 5
	public class LongLivedMarshalByRefObject : MarshalByRefObject
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020A6 File Offset: 0x000002A6
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
