using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
	// Token: 0x020005BC RID: 1468
	public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FBC RID: 12220 RVA: 0x000A50A5 File Offset: 0x000A32A5
		internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000A50B8 File Offset: 0x000A32B8
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal DownloadStringCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A69 RID: 6761
		private readonly string _result;
	}
}
