using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event.</summary>
	// Token: 0x02000278 RID: 632
	public class AuthenticateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.AuthenticateEventArgs.Authenticated" /> property set to false.</summary>
		// Token: 0x06001A21 RID: 6689 RVA: 0x000432BB File Offset: 0x000414BB
		public AuthenticateEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class.</summary>
		/// <param name="authenticated">true if the user is authenticated; otherwise, false. </param>
		// Token: 0x06001A22 RID: 6690 RVA: 0x000432C4 File Offset: 0x000414C4
		public AuthenticateEventArgs(bool authenticated)
		{
			this._authenticated = authenticated;
		}

		/// <summary>Gets or sets a value indicating whether a user's authentication attempt succeeded.</summary>
		/// <returns>true if the authentication attempt succeeded; otherwise, false.</returns>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000432D3 File Offset: 0x000414D3
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x000432DB File Offset: 0x000414DB
		public bool Authenticated
		{
			get
			{
				return this._authenticated;
			}
			set
			{
				this._authenticated = value;
			}
		}

		// Token: 0x04001646 RID: 5702
		private bool _authenticated;
	}
}
