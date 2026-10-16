using System;
using System.Security.Principal;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information about the current Web request.</summary>
	// Token: 0x020006E0 RID: 1760
	public sealed class WebRequestInformation
	{
		// Token: 0x06004A88 RID: 19080 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WebRequestInformation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the instance of the managed-code principal associated with the Web request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> instance that is associated with the request event.</returns>
		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IPrincipal Principal
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the physical path of the Web request.</summary>
		/// <returns>The physical path of the request.</returns>
		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string RequestPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the logical path of the request.</summary>
		/// <returns>The logical path of the request.</returns>
		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string RequestUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a string that represents the Windows logon name of the user on whose behalf the code is being run.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ThreadAccountName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user host address. </summary>
		/// <returns>The user host address.</returns>
		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string UserHostAddress
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Formats the Web-request information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004A8E RID: 19086 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
