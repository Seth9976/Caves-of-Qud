using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a set of query parameters that can be passed to a personalization provider using the various get and find methods. This class cannot be inherited.</summary>
	// Token: 0x020007AE RID: 1966
	[Serializable]
	public sealed class PersonalizationStateQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> class. </summary>
		// Token: 0x06004F6B RID: 20331 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationStateQuery()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the value of the query parameter.</summary>
		/// <returns>The value of the query parameter indicated by the <paramref name="queryKey" /> parameter.</returns>
		/// <param name="queryKey">A case-insensitive query string. The value should be one of the following: "PathToMatch", "UserInactiveSinceDate", or "UsernameToMatch".</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queryKey" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="queryKey" /> is an empty string ("").- or -When trimmed, <paramref name="queryKey" /> results in an empty string. - or -A value provided for one of the three properties on <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> was of the wrong type.</exception>
		// Token: 0x17001836 RID: 6198
		public object this[string queryKey]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the path to be used for a query.</summary>
		/// <returns>The path to be used for a query.</returns>
		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F6F RID: 20335 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string PathToMatch
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the last active date used in a query.</summary>
		/// <returns>The last active date to be used in a query.</returns>
		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x000C8EC0 File Offset: 0x000C70C0
		// (set) Token: 0x06004F71 RID: 20337 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DateTime UserInactiveSinceDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the user name in a query.</summary>
		/// <returns>The user name to be used in a query.</returns>
		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F73 RID: 20339 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string UsernameToMatch
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
