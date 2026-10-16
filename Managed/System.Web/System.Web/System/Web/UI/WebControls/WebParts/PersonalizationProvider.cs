using System;
using System.Collections;
using System.Configuration.Provider;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements the basic functionality for a personalization provider.</summary>
	// Token: 0x020007AC RID: 1964
	public abstract class PersonalizationProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> class. </summary>
		// Token: 0x06004F4E RID: 20302 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected PersonalizationProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, gets or sets the name of the application configured for the provider.</summary>
		/// <returns>The application configured for the personalization provider.</returns>
		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06004F4F RID: 20303
		// (set) Token: 0x06004F50 RID: 20304
		public abstract string ApplicationName { get; set; }

		/// <summary>Returns a list of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> objects that represent the set of known capabilities used by the Web Parts control set.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents the set of known capabilities used by the Web Parts control set.</returns>
		// Token: 0x06004F51 RID: 20305 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual IList CreateSupportedUserCapabilities()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Determines whether the initial personalization scope should be <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating whether the current personalization scope is <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" />.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that manages the personalization information.</param>
		/// <param name="loadedState">The personalization state information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.</exception>
		// Token: 0x06004F52 RID: 20306 RVA: 0x000C8E88 File Offset: 0x000C7088
		public virtual PersonalizationScope DetermineInitialScope(WebPartManager webPartManager, PersonalizationState loadedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return PersonalizationScope.User;
		}

		/// <summary>Returns a dictionary containing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instances that represent the personalization-related capabilities of the currently executing user account.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instances if the user account is authenticated, or null if the executing user account is not authenticated.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization information.</param>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.- or -The request associated with the page is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		// Token: 0x06004F53 RID: 20307 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual IDictionary DetermineUserCapabilities(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, returns a collection containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects based on scope and specific query parameters. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing zero or more <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived objects.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> with the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		/// <param name="pageIndex">The location where the query starts.</param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		// Token: 0x06004F54 RID: 20308
		public abstract PersonalizationStateInfoCollection FindState(PersonalizationScope scope, PersonalizationStateQuery query, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>When overridden in a derived class, returns the number of rows in the underlying data store that exist within the specified scope.</summary>
		/// <returns>The number of rows in the underlying data store that exist for the specified <paramref name="scope" /> parameter.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> of the personalization information to be queried. This value cannot be null.</param>
		/// <param name="query">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateQuery" /> containing a query. This value can be null.</param>
		// Token: 0x06004F55 RID: 20309
		public abstract int GetCountOfState(PersonalizationScope scope, PersonalizationStateQuery query);

		/// <summary>When overridden in a derived class, loads raw personalization data from the underlying data store.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the retrieval key.</param>
		/// <param name="userName">The user name for personalization information to be used as the retrieval key.</param>
		/// <param name="sharedDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</param>
		/// <param name="userDataBlob">The returned data for the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</param>
		// Token: 0x06004F56 RID: 20310
		protected abstract void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob);

		/// <summary>Loads the raw data from the underlying data store and converts that data into a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> containing personalization data.</returns>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="ignoreCurrentUser">A <see cref="T:System.Boolean" /> indicating whether the user name should be passed to the personalization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The page associated with <paramref name="webPartManager" /> is null.- or -The request associated with the page is null.</exception>
		// Token: 0x06004F57 RID: 20311 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual PersonalizationState LoadPersonalizationState(WebPartManager webPartManager, bool ignoreCurrentUser)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, deletes raw personalization data from the underlying data store. </summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the data store key.</param>
		/// <param name="userName">The user name for personalization information to be used as the data store key.</param>
		// Token: 0x06004F58 RID: 20312
		protected abstract void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName);

		/// <summary>Resets personalization data to the underlying data store.</summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPartManager" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPartManager" /> is not associated with a page.- or -The page is not associated with an in-progress <see cref="T:System.Web.HttpRequest" />.</exception>
		// Token: 0x06004F59 RID: 20313 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ResetPersonalizationState(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, deletes personalization state from the underlying data store based on the specified parameters. </summary>
		/// <returns>The number of rows deleted.</returns>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> of the personalization information to be reset. This value cannot be null.</param>
		/// <param name="paths">The paths for personalization information to be deleted.</param>
		/// <param name="usernames">The user names for personalization information to be deleted.</param>
		// Token: 0x06004F5A RID: 20314
		public abstract int ResetState(PersonalizationScope scope, string[] paths, string[] usernames);

		/// <summary>When overridden in a derived class, deletes Web Parts personalization data from the underlying data store based on the specified parameters. </summary>
		/// <returns>The number of rows deleted from the underlying data store.</returns>
		/// <param name="path">The path of the personalization data to be deleted. This value can be null but cannot be an empty string ("").</param>
		/// <param name="userInactiveSinceDate">The date indicating the last time a Web site user changed personalization data.</param>
		// Token: 0x06004F5B RID: 20315
		public abstract int ResetUserState(string path, DateTime userInactiveSinceDate);

		/// <summary>When overridden in a derived class, saves raw personalization data to the underlying data store. </summary>
		/// <param name="webPartManager">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> managing the personalization data.</param>
		/// <param name="path">The path for personalization information to be used as the data store key.</param>
		/// <param name="userName">The user name for personalization information to be used as the key.</param>
		/// <param name="dataBlob">The byte array of data to be saved.</param>
		// Token: 0x06004F5C RID: 20316
		protected abstract void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob);

		/// <summary>Saves personalization data to a data store.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> containing personalization data to be saved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="state" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> object being saved has a different type from the <paramref name="state" /> object that was returned by the <see cref="M:System.Web.UI.WebControls.WebParts.PersonalizationProvider.LoadPersonalizationState(System.Web.UI.WebControls.WebParts.WebPartManager,System.Boolean)" /> method. </exception>
		// Token: 0x06004F5D RID: 20317 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void SavePersonalizationState(PersonalizationState state)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
