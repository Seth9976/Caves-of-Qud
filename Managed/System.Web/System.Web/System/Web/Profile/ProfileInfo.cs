using System;

namespace System.Web.Profile
{
	/// <summary>Provides information about a user profile.</summary>
	// Token: 0x020004FF RID: 1279
	[Serializable]
	public class ProfileInfo
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> class with the specified property values.</summary>
		/// <param name="username">The user name for the profile.</param>
		/// <param name="isAnonymous">true to indicate the profile is for an anonymous user; false to indicate the profile is for an authenticated user.</param>
		/// <param name="lastActivityDate">The last date and time when the profile was read or updated.</param>
		/// <param name="lastUpdatedDate">The last date and time when the profile was updated.</param>
		/// <param name="size">The size of the profile information and values stored in the data source.</param>
		// Token: 0x060038EE RID: 14574 RVA: 0x0009736C File Offset: 0x0009556C
		public ProfileInfo(string username, bool isAnonymous, DateTime lastActivityDate, DateTime lastUpdatedDate, int size)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			this._UserName = username;
			if (lastActivityDate.Kind == DateTimeKind.Local)
			{
				lastActivityDate = lastActivityDate.ToUniversalTime();
			}
			this._LastActivityDate = lastActivityDate;
			if (lastUpdatedDate.Kind == DateTimeKind.Local)
			{
				lastUpdatedDate = lastUpdatedDate.ToUniversalTime();
			}
			this._LastUpdatedDate = lastUpdatedDate;
			this._IsAnonymous = isAnonymous;
			this._Size = size;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> object for a class that inherits the <see cref="T:System.Web.Profile.ProfileInfo" /> class.</summary>
		// Token: 0x060038EF RID: 14575 RVA: 0x00002050 File Offset: 0x00000250
		protected ProfileInfo()
		{
		}

		/// <summary>Gets the user name for the profile.</summary>
		/// <returns>The user name for the profile.</returns>
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000973D5 File Offset: 0x000955D5
		public virtual string UserName
		{
			get
			{
				return this._UserName;
			}
		}

		/// <summary>Gets the last date and time when the profile was read or updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was read or updated.</returns>
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000973DD File Offset: 0x000955DD
		public virtual DateTime LastActivityDate
		{
			get
			{
				return this._LastActivityDate.ToLocalTime();
			}
		}

		/// <summary>Gets the last date and time when the profile was updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was updated.</returns>
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000973EA File Offset: 0x000955EA
		public virtual DateTime LastUpdatedDate
		{
			get
			{
				return this._LastUpdatedDate.ToLocalTime();
			}
		}

		/// <summary>Gets whether the user name for the profile is anonymous.</summary>
		/// <returns>true if the user name for the profile is anonymous; otherwise, false.</returns>
		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000973F7 File Offset: 0x000955F7
		public virtual bool IsAnonymous
		{
			get
			{
				return this._IsAnonymous;
			}
		}

		/// <summary>Gets the size of the profile property names and values stored in the data source.</summary>
		/// <returns>The size of the profile property names and values stored in the data source.</returns>
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000973FF File Offset: 0x000955FF
		public virtual int Size
		{
			get
			{
				return this._Size;
			}
		}

		// Token: 0x04001F0D RID: 7949
		private string _UserName;

		// Token: 0x04001F0E RID: 7950
		private DateTime _LastActivityDate;

		// Token: 0x04001F0F RID: 7951
		private DateTime _LastUpdatedDate;

		// Token: 0x04001F10 RID: 7952
		private bool _IsAnonymous;

		// Token: 0x04001F11 RID: 7953
		private int _Size;
	}
}
