using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200055D RID: 1373
	internal class SecurityPackageInfoClass
	{
		// Token: 0x06002C88 RID: 11400 RVA: 0x0009789C File Offset: 0x00095A9C
		internal unsafe SecurityPackageInfoClass(SafeHandle safeHandle, int index)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle: {0}", new object[] { safeHandle }), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle() + sizeof(SecurityPackageInfo) * index;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("unmanagedAddress: {0}", new object[] { intPtr }), ".ctor");
			}
			SecurityPackageInfo* ptr = (SecurityPackageInfo*)(void*)intPtr;
			this.Capabilities = ptr->Capabilities;
			this.Version = ptr->Version;
			this.RPCID = ptr->RPCID;
			this.MaxToken = ptr->MaxToken;
			IntPtr intPtr2 = ptr->Name;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Name = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Name: {0}", new object[] { this.Name }), ".ctor");
				}
			}
			intPtr2 = ptr->Comment;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Comment = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Comment: {0}", new object[] { this.Comment }), ".ctor");
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, this.ToString(), ".ctor");
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x00097A04 File Offset: 0x00095C04
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Capabilities:",
				string.Format(CultureInfo.InvariantCulture, "0x{0:x}", this.Capabilities),
				" Version:",
				this.Version.ToString(NumberFormatInfo.InvariantInfo),
				" RPCID:",
				this.RPCID.ToString(NumberFormatInfo.InvariantInfo),
				" MaxToken:",
				this.MaxToken.ToString(NumberFormatInfo.InvariantInfo),
				" Name:",
				(this.Name == null) ? "(null)" : this.Name,
				" Comment:",
				(this.Comment == null) ? "(null)" : this.Comment
			});
		}

		// Token: 0x04001806 RID: 6150
		internal int Capabilities;

		// Token: 0x04001807 RID: 6151
		internal short Version;

		// Token: 0x04001808 RID: 6152
		internal short RPCID;

		// Token: 0x04001809 RID: 6153
		internal int MaxToken;

		// Token: 0x0400180A RID: 6154
		internal string Name;

		// Token: 0x0400180B RID: 6155
		internal string Comment;
	}
}
