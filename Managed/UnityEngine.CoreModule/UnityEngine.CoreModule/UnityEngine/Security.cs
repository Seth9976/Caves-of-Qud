using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020002C9 RID: 713
	public sealed class Security
	{
		// Token: 0x0600227E RID: 8830 RVA: 0x0004391C File Offset: 0x00041B1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00043930 File Offset: 0x00041B30
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00043944 File Offset: 0x00041B44
		[ExcludeFromDocs]
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int num = 3000;
			return Security.PrefetchSocketPolicy(ip, atPort, num);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00043964 File Offset: 0x00041B64
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort, [DefaultValue("3000")] int timeout)
		{
			return false;
		}
	}
}
