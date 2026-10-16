using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
	// Token: 0x020005BE RID: 1470
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FC2 RID: 12226 RVA: 0x000A50E7 File Offset: 0x000A32E7
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets the server reply to a string upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000A50FA File Offset: 0x000A32FA
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal UploadStringCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A6B RID: 6763
		private readonly string _result;
	}
}
