using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
	// Token: 0x020005BF RID: 1471
	public class UploadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000A5108 File Offset: 0x000A3308
		internal UploadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000A511B File Offset: 0x000A331B
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal UploadDataCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A6C RID: 6764
		private readonly byte[] _result;
	}
}
