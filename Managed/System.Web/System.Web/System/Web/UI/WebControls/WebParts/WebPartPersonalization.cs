using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements low-level personalization operations.</summary>
	// Token: 0x020006BB RID: 1723
	[TypeConverter("System.Web.UI.WebControls.EmptyStringExpandableObjectConverter")]
	public class WebPartPersonalization
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> used to manage the personalization information</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06004920 RID: 18720 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartPersonalization(WebPartManager owner)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value indicating whether the user is authorized to enter <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</summary>
		/// <returns>true if the user is authorized to enter <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope; otherwise, false.</returns>
		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06004921 RID: 18721 RVA: 0x000C7934 File Offset: 0x000C5B34
		public bool CanEnterSharedScope
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Returns a value indicating whether personalization is requested to be enabled for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <returns>true if personalization is enabled; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property value after the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's <see cref="M:System.Web.UI.WebControls.WebParts.WebPartManager.OnInit(System.EventArgs)" /> method had completed.</exception>
		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x000C7950 File Offset: 0x000C5B50
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool Enabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a value indicating whether the current page and personalization scope have associated personalization data.</summary>
		/// <returns>true if the page has personalization data associated with it; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no personalization provider associated with the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance. This can occur if the property is accessed prior to the completion of the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's <see cref="M:System.Web.UI.WebControls.WebParts.WebPartManager.OnInit(System.EventArgs)" /> method.- or -The value of the <see cref="P:System.Web.UI.Control.Page" /> property for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> is null.- or -The value of the <see cref="P:System.Web.UI.Page.Request" /> property on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's associated <see cref="T:System.Web.UI.Page" /> instance is null.</exception>
		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x000C796C File Offset: 0x000C5B6C
		public virtual bool HasPersonalizationState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the default personalization scope.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the default personalization scope of the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property to a value other than its current value after the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's <see cref="M:System.Web.UI.WebControls.WebParts.WebPartManager.OnInit(System.EventArgs)" /> method had completed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set a value of <see cref="P:System.Web.UI.WebControls.WebParts.WebPartPersonalization.InitialScope" /> that is not a member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x000C7988 File Offset: 0x000C5B88
		// (set) Token: 0x06004926 RID: 18726 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual PersonalizationScope InitialScope
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PersonalizationScope.User;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether personalization is enabled and has successfully loaded personalization data for this instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> class.</summary>
		/// <returns>true if personalization is enabled for the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance and personalization data has successfully loaded; otherwise, false.</returns>
		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x000C79A4 File Offset: 0x000C5BA4
		public bool IsEnabled
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether personalization is enabled and has successfully loaded personalization data for this instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> class.</summary>
		/// <returns>true if personalization is initialized for this instance; otherwise, false.</returns>
		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x000C79C0 File Offset: 0x000C5BC0
		protected bool IsInitialized
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value that indicates whether the current user is authorized to modify state information.</summary>
		/// <returns>true if the user is authorized to modify state information; otherwise, false.</returns>
		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x000C79DC File Offset: 0x000C5BDC
		public bool IsModifiable
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the provider name for personalization.</summary>
		/// <returns>The name of the personalization provider.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property to a value other than its current value after the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's <see cref="M:System.Web.UI.WebControls.WebParts.WebPartManager.OnInit(System.EventArgs)" /> method has completed.</exception>
		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x0600492A RID: 18730 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x0600492B RID: 18731 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ProviderName
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

		/// <summary>Gets the current personalization scope for the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> instance indicating the scope of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</returns>
		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x0600492C RID: 18732 RVA: 0x000C79F8 File Offset: 0x000C5BF8
		public PersonalizationScope Scope
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PersonalizationScope.User;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the personalization data of the current page has been reset (for example, a request was made to delete the personalization data from the underlying data store).</summary>
		/// <returns>true if the personalization state for the current page has been reset; otherwise, false.</returns>
		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x000C7A14 File Offset: 0x000C5C14
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected bool ShouldResetPersonalizationState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the set of user capabilities from <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> granted to the current user.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the set of user capabilities granted to the current user, or an empty <see cref="T:System.Collections.Specialized.HybridDictionary" /> if the user is anonymous.</returns>
		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual IDictionary UserCapabilities
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an instance of the current parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with this <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance.</summary>
		/// <returns>The current parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</returns>
		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Applies personalization data to the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to apply personalization state prior to the state being loaded from the underlying data store.- or -The personalization state returned from the data store was null.</exception>
		// Token: 0x06004931 RID: 18737 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void ApplyPersonalizationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Applies personalization data to the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control when requested to do so by the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <param name="webPart">The Web Parts control to which personalization data is to be applied.</param>
		/// <exception cref="T:System.ArgumentException">An attempt was made to apply personalization state to a Web Parts control that is not managed by the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to apply personalization state prior to the state being loaded from the underlying data store.- or -The personalization state returned from the data store was null.- or -An attempt was made to apply personalization data more than once to the same Web Parts control.</exception>
		// Token: 0x06004932 RID: 18738 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void ApplyPersonalizationState(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Changes the current page's <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> instance to the scope specified.</summary>
		/// <param name="scope">The new personalization scope for the current page.</param>
		/// <exception cref="T:System.InvalidOperationException">If attempting to switch from <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope, the current user does not have the user capability to enter <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004933 RID: 18739 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void ChangeScope(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Extracts the personalization state from one Web Parts control and applies it to a second Web Parts control.</summary>
		/// <param name="webPartA">The Web Parts control supplying the personalization data.</param>
		/// <param name="webPartB">The Web Parts control receiving the personalization data.</param>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="webPartA" /> or <paramref name="webPartB" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPartA" /> and <paramref name="webPartB" /> are not of the same <see cref="T:System.Type" />.- or -<paramref name="webPartA" /> and <paramref name="webPartB" /> are <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> controls but one or both lack a child control.- or -<paramref name="webPartA" /> and <paramref name="webPartB" /> are <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> controls, but their child controls are not of the same <see cref="T:System.Type" />.</exception>
		// Token: 0x06004934 RID: 18740 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void CopyPersonalizationState(WebPart webPartA, WebPart webPartB)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Ensures that the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance has completed initialization or that the current user has rights to modify personalization state.</summary>
		/// <param name="ensureModifiable">A Boolean value indicating which type of check should be made.</param>
		/// <exception cref="T:System.InvalidOperationException">The current user does not have rights to modify personalization information, or other checks failed.- or -<see cref="P:System.Web.UI.WebControls.WebParts.WebPartPersonalization.IsEnabled" /> returned false.</exception>
		// Token: 0x06004935 RID: 18741 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void EnsureEnabled(bool ensureModifiable)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Extracts personalization data from the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to extract personalization state prior to the state being loaded from the underlying data store.- or -Personalization state has not been applied yet.- or -The <see cref="P:System.Web.UI.Control.ID" /> of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> has changed since personalization data was applied.- or - The personalization state returned from the data store was null.</exception>
		// Token: 0x06004936 RID: 18742 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void ExtractPersonalizationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Extracts personalization data from a Web Parts control when requested to do so by the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control.</summary>
		/// <param name="webPart">The Web Parts control containing personalization data to be extracted.</param>
		/// <exception cref="T:System.ArgumentException">An attempt was made to extract personalization state from a Web Parts control that is not managed by the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to extract personalization state prior to the state being loaded from the underlying data store.- or -Personalization state has not been applied yet.- or -The <see cref="P:System.Web.UI.Control.ID" /> of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> has changed since personalization data was applied.- or - The personalization state returned from the data store was null.</exception>
		// Token: 0x06004937 RID: 18743 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void ExtractPersonalizationState(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the authorization filter, if any, associated with the specified Web Parts control.</summary>
		/// <returns>The authorization filter for the specified Web Parts control.</returns>
		/// <param name="webPartID">The ID of the Web Parts control associated with the filter to be retrieved.</param>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve an authorization filter when <see cref="P:System.Web.UI.WebControls.WebParts.WebPartPersonalization.IsEnabled" /> is false by calling this method too early in the page life cycle.- or -An attempt was made to retrieve an authorization filter and no personalization state has been loaded.- or The personalization state returned from the data store was null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPartID" /> is null or an empty string ("").</exception>
		// Token: 0x06004938 RID: 18744 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal virtual string GetAuthorizationFilter(string webPartID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Initializes personalization.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> instance for the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">This method is called when personalization is not enabled (<see cref="P:System.Web.UI.WebControls.WebParts.WebPartPersonalization.Enabled" /> equals false).- or -The value of the <see cref="P:System.Web.UI.Control.Page" /> property for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> is null.- or -The value of the <see cref="P:System.Web.UI.Page.Request" /> property on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's associated <see cref="T:System.Web.UI.Page" /> instance is null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">A provider was explicitly set in either the page markup or the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartPersonalization.ProviderName" />  property and the provider could not be found.- or - The object containing the personalization state data is null.</exception>
		/// <exception cref="T:System.ArgumentException">A problem occurred while loading and deserializing data.- or -An error occurred in the definition of a personalization provider in the configuration file.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The underlying personalization providers failed to initialize because a default provider could not be found or because a failure occurred while attempting to initialize a personalization provider.</exception>
		// Token: 0x06004939 RID: 18745 RVA: 0x000C7A30 File Offset: 0x000C5C30
		protected virtual PersonalizationScope Load()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return PersonalizationScope.User;
		}

		/// <summary>Resets personalization data for the current page, scope, and user in the underlying data store.</summary>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to reset personalization data when the current user is not authorized to modify personalization state.- or -The <see cref="T:System.Web.UI.Page" /> instance for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> is null.- or -The value of the <see cref="P:System.Web.UI.Page.Request" /> property on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's associated <see cref="T:System.Web.UI.Page" /> instance is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance cannot reference a valid personalization provider.</exception>
		// Token: 0x0600493A RID: 18746 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ResetPersonalizationState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves personalization data for the current page, scope, and user to the underlying data store.</summary>
		/// <exception cref="T:System.InvalidOperationException">The current user does not have the capability called <see cref="F:System.Web.UI.WebControls.WebParts.WebPartPersonalization.ModifyStateUserCapability" />.- or -The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance cannot reference a valid personalization provider.- or -No personalization provider is currently associated with the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance. This can occur if this method is called prior to calling <see cref="M:System.Web.UI.WebControls.WebParts.WebPartPersonalization.Load" /> (the personalization provider reference is obtained during the call to <see cref="M:System.Web.UI.WebControls.WebParts.WebPartPersonalization.Load" />.- or - No personalization state has been loaded.- or -The <see cref="T:System.Web.UI.Page" /> instance for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> is null.- or -The value of the <see cref="P:System.Web.UI.Page.Request" /> property on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's associated <see cref="T:System.Web.UI.Page" /> instance is null.</exception>
		// Token: 0x0600493B RID: 18747 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void Save()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Marks the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control as having personalization data that has changed (is "dirty").</summary>
		/// <exception cref="T:System.InvalidOperationException">No personalization data has been loaded.</exception>
		// Token: 0x0600493C RID: 18748 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void SetDirty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Marks the specified Web Parts control as having personalization data that has changed (is "dirty").</summary>
		/// <param name="webPart">The Web Parts control to be marked "dirty".</param>
		/// <exception cref="T:System.ArgumentException">Attempted to mark as "dirty" a Web Parts control that is not managed by the parent <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No personalization data has been loaded.</exception>
		// Token: 0x0600493D RID: 18749 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void SetDirty(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Switches the current page's personalization scope from <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> to <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> or from <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> to <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The current user does not have the user capability to enter <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope when attempting to switch from <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope to <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.- or -The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartPersonalization" /> instance has not completed initialization.- or -The <see cref="T:System.Web.UI.Page" /> instance for the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> is null.- or -The value of the <see cref="P:System.Web.UI.Page.Request" /> property on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control's associated <see cref="T:System.Web.UI.Page" /> instance is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to toggle to a scope that is not defined in the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration. Technically, this situation should never occur.</exception>
		// Token: 0x0600493E RID: 18750 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ToggleScope()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instance of a user's authorization to enter <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</summary>
		// Token: 0x040025D9 RID: 9689
		public static readonly WebPartUserCapability EnterSharedScopeUserCapability;

		/// <summary>Represents the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartUserCapability" /> instance of a user's authorization to modify personalization state.</summary>
		// Token: 0x040025DA RID: 9690
		public static readonly WebPartUserCapability ModifyStateUserCapability;
	}
}
