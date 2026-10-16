using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the worker process that hosts ASP.NET.</summary>
	// Token: 0x020006DF RID: 1759
	public sealed class WebProcessInformation
	{
		// Token: 0x06004A83 RID: 19075 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WebProcessInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the account name for the worker process.</summary>
		/// <returns>The worker process account name.</returns>
		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string AccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the process identifier.</summary>
		/// <returns>The process identifier.</returns>
		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x000C7EE4 File Offset: 0x000C60E4
		public int ProcessID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name of the process.</returns>
		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ProcessName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Formats the application information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004A87 RID: 19079 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
