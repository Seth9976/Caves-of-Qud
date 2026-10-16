using System;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreateUserError" /> event.</summary>
	// Token: 0x02000286 RID: 646
	public class CreateUserErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> class.</summary>
		/// <param name="s">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> object that describes the result of a <see cref="Overload:System.Web.Security.Membership.CreateUser" /> attempt.</param>
		// Token: 0x06001A40 RID: 6720 RVA: 0x0004335F File Offset: 0x0004155F
		public CreateUserErrorEventArgs(MembershipCreateStatus s)
		{
			this._error = s;
		}

		/// <summary>Gets or sets a value indicating the result of a <see cref="E:System.Web.UI.WebControls.CreateUserWizard.CreatingUser" /> event.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration values.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0004336E File Offset: 0x0004156E
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x00043376 File Offset: 0x00041576
		public MembershipCreateStatus CreateUserError
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}

		// Token: 0x04001689 RID: 5769
		private MembershipCreateStatus _error;
	}
}
