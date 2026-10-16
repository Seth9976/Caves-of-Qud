using System;
using System.ComponentModel;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
	// Token: 0x020005BB RID: 1467
	public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002FB9 RID: 12217 RVA: 0x000A5084 File Offset: 0x000A3284
		internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this._result = result;
		}

		/// <summary>Gets a writable stream that is used to send data to a server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> where you can write data to be uploaded.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000A5097 File Offset: 0x000A3297
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._result;
			}
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal OpenWriteCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001A68 RID: 6760
		private readonly Stream _result;
	}
}
