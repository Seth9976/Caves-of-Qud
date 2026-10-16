using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x02000815 RID: 2069
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermissionAttribute" /> class. </summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values that specifies the permission behavior.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06004233 RID: 16947 RVA: 0x000A979A File Offset: 0x000A799A
		public SmtpPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the level of access to SMTP servers controlled by the attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> value. Valid values are "Connect" and "None".</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000E4BD8 File Offset: 0x000E2DD8
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x000E4BE0 File Offset: 0x000E2DE0
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000E4BEC File Offset: 0x000E2DEC
		private SmtpAccess GetSmtpAccess()
		{
			if (this.access == null)
			{
				return SmtpAccess.None;
			}
			string text = this.access.ToLowerInvariant();
			if (text == "connecttounrestrictedport")
			{
				return SmtpAccess.ConnectToUnrestrictedPort;
			}
			if (text == "connect")
			{
				return SmtpAccess.Connect;
			}
			if (!(text == "none"))
			{
				string text2 = global::Locale.GetText("Invalid Access='{0}' value.", new object[] { this.access });
				throw new ArgumentException("Access", text2);
			}
			return SmtpAccess.None;
		}

		/// <summary>Creates a permission object that can be stored with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> instance.</returns>
		// Token: 0x06004237 RID: 16951 RVA: 0x000E4C63 File Offset: 0x000E2E63
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.GetSmtpAccess());
		}

		// Token: 0x040027A6 RID: 10150
		private string access;
	}
}
