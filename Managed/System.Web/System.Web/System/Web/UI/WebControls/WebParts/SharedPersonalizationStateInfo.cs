using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a lightweight version of shared personalization information about a page. This class cannot be inherited.</summary>
	// Token: 0x020007B8 RID: 1976
	[Serializable]
	public sealed class SharedPersonalizationStateInfo : PersonalizationStateInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.SharedPersonalizationStateInfo" /> class. </summary>
		/// <param name="path">The page that the information applies to. <paramref name="path" /> is an application-relative (using tilde syntax) virtual path.</param>
		/// <param name="lastUpdatedDate">A <see cref="T:System.DateTime" /> indicating when the shared information for the page was last updated.</param>
		/// <param name="size">The size, in bytes, of the shared state information for the page.</param>
		/// <param name="sizeOfPersonalizations">The total size, in bytes, of all per-user personalization information that exists for the page.</param>
		/// <param name="countOfPersonalizations">The total number of users who have personalized the page.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" />, when trimmed, is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" />, <paramref name="sizeOfPersonalizations" /> or <paramref name="countOfPersonalizations" /> is negative.</exception>
		// Token: 0x06004FB1 RID: 20401 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SharedPersonalizationStateInfo(string path, DateTime lastUpdatedDate, int size, int sizeOfPersonalizations, int countOfPersonalizations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the total number of users that have personalized a page.</summary>
		/// <returns>The total number of per-user personalization settings that have been applied to a page.</returns>
		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x000C8FD8 File Offset: 0x000C71D8
		public int CountOfPersonalizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total size of all per-user personalization information for a page.</summary>
		/// <returns>The total size, in bytes, of all personalization information for a page.</returns>
		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x000C8FF4 File Offset: 0x000C71F4
		public int SizeOfPersonalizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
