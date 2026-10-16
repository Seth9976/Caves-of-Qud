using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UnityConsent
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityConsent/EndUserConsent.h")]
	public static class EndUserConsent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		[NativeMethod("GetConsentStateStatic")]
		public static ConsentState GetConsentState()
		{
			ConsentState consentState;
			EndUserConsent.GetConsentState_Injected(out consentState);
			return consentState;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		[NativeMethod("SetConsentStateStatic")]
		public static void SetConsentState(ConsentState consentState)
		{
			EndUserConsent.SetConsentState_Injected(ref consentState);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ConsentState> consentStateChanged;

		// Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
		[RequiredByNativeCode]
		private static void OnConsentStateChanged()
		{
			bool flag = EndUserConsent.consentStateChanged != null;
			if (flag)
			{
				EndUserConsent.consentStateChanged(EndUserConsent.GetConsentState());
			}
		}

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConsentState_Injected(out ConsentState ret);

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConsentState_Injected([In] ref ConsentState consentState);
	}
}
