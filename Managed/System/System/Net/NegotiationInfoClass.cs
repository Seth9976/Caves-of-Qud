using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000552 RID: 1362
	internal class NegotiationInfoClass
	{
		// Token: 0x06002C2A RID: 11306 RVA: 0x00096310 File Offset: 0x00094510
		internal unsafe NegotiationInfoClass(SafeHandle safeHandle, int negotiationState)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle:{0}", new object[] { safeHandle }), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x}", new object[] { intPtr, negotiationState }), ".ctor");
			}
			if (negotiationState == 0 || negotiationState == 1)
			{
				string text = null;
				IntPtr name = ((SecurityPackageInfo*)(void*)intPtr)->Name;
				if (name != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(name);
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x} name:{2}", new object[] { intPtr, negotiationState, text }), ".ctor");
				}
				if (string.Compare(text, "Kerberos", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "Kerberos";
					return;
				}
				if (string.Compare(text, "NTLM", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "NTLM";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x040017E5 RID: 6117
		internal string AuthenticationPackage;

		// Token: 0x040017E6 RID: 6118
		internal const string NTLM = "NTLM";

		// Token: 0x040017E7 RID: 6119
		internal const string Kerberos = "Kerberos";

		// Token: 0x040017E8 RID: 6120
		internal const string Negotiate = "Negotiate";

		// Token: 0x040017E9 RID: 6121
		internal const string Basic = "Basic";
	}
}
