using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x020000A9 RID: 169
	[NativeClass(null)]
	internal struct NativeArrayDisposeJob : IJob
	{
		// Token: 0x0600038F RID: 911 RVA: 0x000074E1 File Offset: 0x000056E1
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000074F0 File Offset: 0x000056F0
		[RequiredByNativeCode]
		internal static void RegisterNativeArrayDisposeJobReflectionData()
		{
			IJobExtensions.EarlyJobInit<NativeArrayDisposeJob>();
		}

		// Token: 0x04000281 RID: 641
		internal NativeArrayDispose Data;
	}
}
