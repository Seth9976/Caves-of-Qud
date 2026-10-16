using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the basic functionality that represents the personalization data for a page.</summary>
	// Token: 0x020007AD RID: 1965
	public abstract class PersonalizationState
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" /> class. </summary>
		/// <param name="webPartManager">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that manages Web Parts controls that have personalization data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="webPartManager" /> parameter is null.</exception>
		// Token: 0x06004F5E RID: 20318 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected PersonalizationState(WebPartManager webPartManager)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether any personalization information has changed.</summary>
		/// <returns>true if any personalization state instance has changed (is "dirty"); otherwise, false.</returns>
		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06004F5F RID: 20319 RVA: 0x000C8EA4 File Offset: 0x000C70A4
		public bool IsDirty
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When overridden, gets a value that indicates whether any personalization state was extracted by a state instance.</summary>
		/// <returns>true if any personalization state was extracted by a state instance; otherwise, false.</returns>
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x06004F60 RID: 20320
		public abstract bool IsEmpty { get; }

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control currently associated with the personalization state instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> associated with the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" />.</returns>
		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When overridden, applies personalization data to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the personalization state instance.</summary>
		// Token: 0x06004F62 RID: 20322
		public abstract void ApplyWebPartManagerPersonalization();

		/// <summary>When overridden, applies personalization data to the specified Web Parts control.</summary>
		/// <param name="webPart">The Web Parts control to which personalization data is applied.</param>
		// Token: 0x06004F63 RID: 20323
		public abstract void ApplyWebPartPersonalization(WebPart webPart);

		/// <summary>When overridden, extracts personalization information from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the current personalization state.</summary>
		// Token: 0x06004F64 RID: 20324
		public abstract void ExtractWebPartManagerPersonalization();

		/// <summary>When overridden, extracts personalization data from the specified Web Parts control.</summary>
		/// <param name="webPart">The Web Parts control from which personalization data is extracted.</param>
		// Token: 0x06004F65 RID: 20325
		public abstract void ExtractWebPartPersonalization(WebPart webPart);

		/// <summary>Retrieves the authorization filter for the specified Web Parts control.</summary>
		/// <returns>The authorization filter string for a Web Parts control.</returns>
		/// <param name="webPartID">The ID of the Web Parts control from which the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> is retrieved.</param>
		// Token: 0x06004F66 RID: 20326
		public abstract string GetAuthorizationFilter(string webPartID);

		/// <summary>Marks the current personalization state as having changed.</summary>
		// Token: 0x06004F67 RID: 20327 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected void SetDirty()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden, marks a Web Parts control as having changed.</summary>
		/// <param name="webPart">The Web Parts control to be marked as having changed.</param>
		// Token: 0x06004F68 RID: 20328
		public abstract void SetWebPartDirty(WebPart webPart);

		/// <summary>When overridden, marks the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control as having changed.</summary>
		// Token: 0x06004F69 RID: 20329
		public abstract void SetWebPartManagerDirty();

		/// <summary>Verifies that the specified Web Parts control is valid.</summary>
		/// <param name="webPart">The Web Parts control to be validated.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> is not currently managed by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> associated with the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationState" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart " />is null.</exception>
		// Token: 0x06004F6A RID: 20330 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected void ValidateWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
