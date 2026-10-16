using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents the per-user personalization information for a page and a user. This class cannot be inherited.</summary>
	// Token: 0x020007BB RID: 1979
	[Serializable]
	public sealed class UserPersonalizationStateInfo : PersonalizationStateInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> class. </summary>
		/// <param name="path">The page that the information applies to. <paramref name="path" /> is an application-relative (using tilde syntax) virtual path.</param>
		/// <param name="lastUpdatedDate">The last date and time that the user information for a page was updated.</param>
		/// <param name="size">The size, in bytes, of the per-user state information for the page.</param>
		/// <param name="username">The user to whom the personalization information for the page applies.</param>
		/// <param name="lastActivityDate">The last time the user was active in the ASP.NET application.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" />, after trimming, is an empty string ("").- or - <paramref name="username" />, after trimming, is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.- or - <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		// Token: 0x06004FC0 RID: 20416 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public UserPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, string username, DateTime lastActivityDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the last time the user was active in the ASP.NET application associated with a page.</summary>
		/// <returns>The last time the user was active in the ASP.NET application associated with a page.</returns>
		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x000C9064 File Offset: 0x000C7264
		public DateTime LastActivityDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the user name to which the personalization information for a page applies.</summary>
		/// <returns>The user name to which the personalization information for a page applies.</returns>
		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Username
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
