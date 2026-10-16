using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the state of a thread in an ASP.NET process.</summary>
	// Token: 0x020006E1 RID: 1761
	public sealed class WebThreadInformation
	{
		// Token: 0x06004A8F RID: 19087 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WebThreadInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the current thread-impersonation mode.</summary>
		/// <returns>true if the thread is executing in impersonation mode; otherwise, false.</returns>
		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x000C7F00 File Offset: 0x000C6100
		public bool IsImpersonating
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the current thread-managed stack trace.</summary>
		/// <returns>The thread-managed stack trace.</returns>
		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string StackTrace
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the thread account name.</summary>
		/// <returns>The thread account name.</returns>
		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ThreadAccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the current thread identifier.</summary>
		/// <returns>The current thread identifier.</returns>
		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x000C7F1C File Offset: 0x000C611C
		public int ThreadID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Formats thread-related information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004A94 RID: 19092 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
