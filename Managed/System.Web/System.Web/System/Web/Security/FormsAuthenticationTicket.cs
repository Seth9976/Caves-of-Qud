using System;
using System.IO;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides access to properties and values of the ticket used with forms authentication to identify users. This class cannot be inherited.</summary>
	// Token: 0x020004BB RID: 1211
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Serializable]
	public sealed class FormsAuthenticationTicket
	{
		// Token: 0x0600367C RID: 13948 RVA: 0x0008CDB8 File Offset: 0x0008AFB8
		internal byte[] ToByteArray()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.version);
			binaryWriter.Write(this.persistent);
			binaryWriter.Write(this.issue_date.Ticks);
			binaryWriter.Write(this.expiration.Ticks);
			binaryWriter.Write(this.name != null);
			if (this.name != null)
			{
				binaryWriter.Write(this.name);
			}
			binaryWriter.Write(this.cookie_path != null);
			if (this.cookie_path != null)
			{
				binaryWriter.Write(this.cookie_path);
			}
			binaryWriter.Write(this.user_data != null);
			if (this.user_data != null)
			{
				binaryWriter.Write(this.user_data);
			}
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x0008CE80 File Offset: 0x0008B080
		internal static FormsAuthenticationTicket FromByteArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
			FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket();
			formsAuthenticationTicket.version = binaryReader.ReadInt32();
			formsAuthenticationTicket.persistent = binaryReader.ReadBoolean();
			formsAuthenticationTicket.issue_date = new DateTime(binaryReader.ReadInt64());
			formsAuthenticationTicket.expiration = new DateTime(binaryReader.ReadInt64());
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.name = binaryReader.ReadString();
			}
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.cookie_path = binaryReader.ReadString();
			}
			if (binaryReader.ReadBoolean())
			{
				formsAuthenticationTicket.user_data = binaryReader.ReadString();
			}
			return formsAuthenticationTicket;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x00002050 File Offset: 0x00000250
		private FormsAuthenticationTicket()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, expiration date, issue date, persistence, and user-specific data. The cookie path is set to the default value established in the application's configuration file.</summary>
		/// <param name="version">The version number of the ticket.</param>
		/// <param name="name">The user name associated with the ticket.</param>
		/// <param name="issueDate">The local date and time at which the ticket was issued.</param>
		/// <param name="expiration">The local date and time at which the ticket expires.</param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="userData">The user-specific data to be stored with the ticket.</param>
		// Token: 0x0600367F RID: 13951 RVA: 0x0008CF24 File Offset: 0x0008B124
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData)
		{
			this.version = version;
			this.name = name;
			this.issue_date = issueDate;
			this.expiration = expiration;
			this.persistent = isPersistent;
			this.user_data = userData;
			this.cookie_path = "/";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class with cookie name, version, directory path, issue date, expiration date, persistence, and user-defined data.</summary>
		/// <param name="version">The version number of the ticket. </param>
		/// <param name="name">The user name associated with the ticket. </param>
		/// <param name="issueDate">The local date and time at which the ticket was issued. </param>
		/// <param name="expiration">The local date and time at which the ticket expires. </param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="userData">The user-specific data to be stored with the ticket. </param>
		/// <param name="cookiePath">The path for the ticket when stored in a cookie. </param>
		// Token: 0x06003680 RID: 13952 RVA: 0x0008CF64 File Offset: 0x0008B164
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData, string cookiePath)
		{
			this.version = version;
			this.name = name;
			this.issue_date = issueDate;
			this.expiration = expiration;
			this.persistent = isPersistent;
			this.user_data = userData;
			this.cookie_path = cookiePath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> class using a cookie name and expiration information.</summary>
		/// <param name="name">The user name associated with the ticket.</param>
		/// <param name="isPersistent">true if the ticket will be stored in a persistent cookie (saved across browser sessions); otherwise, false. If the ticket is stored in the URL, this value is ignored.</param>
		/// <param name="timeout">The time, in minutes, for which the authentication ticket is valid.</param>
		// Token: 0x06003681 RID: 13953 RVA: 0x0008CFA4 File Offset: 0x0008B1A4
		public FormsAuthenticationTicket(string name, bool isPersistent, int timeout)
		{
			this.version = 1;
			this.name = name;
			this.issue_date = DateTime.Now;
			this.persistent = isPersistent;
			if (this.persistent)
			{
				this.expiration = this.issue_date.AddYears(50);
			}
			else
			{
				this.expiration = this.issue_date.AddMinutes((double)timeout);
			}
			this.user_data = "";
			this.cookie_path = "/";
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x0008D01D File Offset: 0x0008B21D
		internal void SetDates(DateTime issue_date, DateTime expiration)
		{
			this.issue_date = issue_date;
			this.expiration = expiration;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x0008D02D File Offset: 0x0008B22D
		internal FormsAuthenticationTicket Clone()
		{
			return new FormsAuthenticationTicket(this.version, this.name, this.issue_date, this.expiration, this.persistent, this.user_data, this.cookie_path);
		}

		/// <summary>Gets the cookie path for the forms-authentication ticket.</summary>
		/// <returns>The cookie path for the forms-authentication ticket.</returns>
		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x0008D05E File Offset: 0x0008B25E
		public string CookiePath
		{
			get
			{
				return this.cookie_path;
			}
		}

		/// <summary>Gets the local date and time at which the forms-authentication ticket expires.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> at which the forms-authentication ticket expires.</returns>
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x0008D066 File Offset: 0x0008B266
		public DateTime Expiration
		{
			get
			{
				return this.expiration;
			}
		}

		/// <summary>Gets a value indicating whether the forms-authentication ticket has expired.</summary>
		/// <returns>true if the forms-authentication ticket has expired; otherwise, false.</returns>
		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06003686 RID: 13958 RVA: 0x0008D06E File Offset: 0x0008B26E
		public bool Expired
		{
			get
			{
				return DateTime.UtcNow > this.expiration.ToUniversalTime();
			}
		}

		/// <summary>Gets a value indicating whether the cookie that contains the forms-authentication ticket information is persistent.</summary>
		/// <returns>true if a durable cookie (a cookie that is saved across browser sessions) was issued; otherwise, false.</returns>
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x0008D085 File Offset: 0x0008B285
		public bool IsPersistent
		{
			get
			{
				return this.persistent;
			}
		}

		/// <summary>Gets the local date and time at which the forms-authentication ticket was originally issued.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> when the forms-authentication ticket was originally issued.</returns>
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x0008D08D File Offset: 0x0008B28D
		public DateTime IssueDate
		{
			get
			{
				return this.issue_date;
			}
		}

		/// <summary>Gets the user name associated with the forms-authentication ticket.</summary>
		/// <returns>The user name associated with the forms-authentication ticket.</returns>
		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x0008D095 File Offset: 0x0008B295
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a user-specific string stored with the ticket.</summary>
		/// <returns>A user-specific string stored with the ticket. The default is an empty string ("").</returns>
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x0008D09D File Offset: 0x0008B29D
		public string UserData
		{
			get
			{
				return this.user_data;
			}
		}

		/// <summary>Gets the version number of the ticket.</summary>
		/// <returns>The version number of the ticket. The default is 2.</returns>
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x0008D0A5 File Offset: 0x0008B2A5
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04001DCB RID: 7627
		private int version;

		// Token: 0x04001DCC RID: 7628
		private bool persistent;

		// Token: 0x04001DCD RID: 7629
		private DateTime issue_date;

		// Token: 0x04001DCE RID: 7630
		private DateTime expiration;

		// Token: 0x04001DCF RID: 7631
		private string name;

		// Token: 0x04001DD0 RID: 7632
		private string cookie_path;

		// Token: 0x04001DD1 RID: 7633
		private string user_data;
	}
}
