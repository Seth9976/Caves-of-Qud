using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Identifiers
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Identifiers/Identifiers.h")]
	public static class Identifiers
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string installationId
		{
			get
			{
				return Identifiers.GetInstallationId();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[FreeFunction("UnityEngine_Identifiers_GetInstallationId")]
		private static string GetInstallationId()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Identifiers.GetInstallationId_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInstallationId_Injected(out ManagedSpanWrapper ret);
	}
}
