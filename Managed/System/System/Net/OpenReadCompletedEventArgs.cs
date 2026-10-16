using System;
using System.ComponentModel;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
	// Token: 0x020005BA RID: 1466
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FB6 RID: 12214 RVA: 0x000A5063 File Offset: 0x000A3263
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets a readable stream that contains data downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000A5076 File Offset: 0x000A3276
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal OpenReadCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A67 RID: 6759
		private readonly Stream _result;
	}
}
