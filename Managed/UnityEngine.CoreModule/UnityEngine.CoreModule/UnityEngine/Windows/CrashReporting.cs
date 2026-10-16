using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x0200030A RID: 778
	public static class CrashReporting
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x0004A67C File Offset: 0x0004887C
		public static string crashReportFolder
		{
			[NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h")]
			[ThreadSafe]
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					CrashReporting.get_crashReportFolder_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x060026A9 RID: 9897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_crashReportFolder_Injected(out ManagedSpanWrapper ret);
	}
}
