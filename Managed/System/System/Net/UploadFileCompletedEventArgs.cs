using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
	// Token: 0x020005C0 RID: 1472
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000A5129 File Offset: 0x000A3329
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000A513C File Offset: 0x000A333C
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal UploadFileCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A6D RID: 6765
		private readonly byte[] _result;
	}
}
