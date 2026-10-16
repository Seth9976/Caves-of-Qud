using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents the base class for Web Parts controls that under certain conditions must replace other Web Parts controls on a page.</summary>
	// Token: 0x020006D2 RID: 1746
	[ToolboxItem(false)]
	public abstract class ProxyWebPart : WebPart
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPart" /> class when a dynamic Web Parts control must be replaced.</summary>
		/// <param name="originalID">A string that is the control ID (not the unique ID) of the control to replace. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control is replaced, the ID is the ID of its child server control.</param>
		/// <param name="originalTypeName">A string that is the name of the <see cref="T:System.Type" /> of the control to replace. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control is replaced, the type name is the type of its child server control.</param>
		/// <param name="originalPath">A string that contains the path to the user control to replace.</param>
		/// <param name="genericWebPartID">A string that returns the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control, if that type of control is being replaced. This is needed for controls that do not inherit from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> base class. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="originalID" /> is null or an empty string.- or -<paramref name="originalTypeName" /> is null or an empty string.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="originalPath" /> is specified but <paramref name="genericWebPartID" /> is null or an empty string.</exception>
		// Token: 0x06004A08 RID: 18952 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ProxyWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProxyWebPart" /> class when a static Web Parts control (or server or user control) must be replaced.</summary>
		/// <param name="webPart">The Web Parts control to be replaced.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webPart" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webPart" /> has an empty <see cref="P:System.Web.UI.Control.ID" /> property- or -<paramref name="webPart" /> is an empty <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" />- or -<paramref name="webPart" /> is of type <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" />  and its child control has an empty <see cref="P:System.Web.UI.Control.ID" /> property</exception>
		// Token: 0x06004A09 RID: 18953 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ProxyWebPart(WebPart webPart)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the value of the <see cref="P:System.Web.UI.WebControls.WebParts.GenericWebPart.ID" /> property from the generic Web Parts control replaced by a proxy Web Parts control.</summary>
		/// <returns>A string containing the <see cref="P:System.Web.UI.WebControls.WebParts.GenericWebPart.ID" /> value.</returns>
		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GenericWebPartID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.UI.Control.ID" /> of the Web Parts control replaced by the proxy Web Parts control.</summary>
		/// <returns>A string containing the <see cref="P:System.Web.UI.Control.ID" /> value of the Web Parts control replaced by the proxy Web Parts control.</returns>
		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string OriginalID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the path to the user control being replaced.</summary>
		/// <returns>A string that contains the path to a user control being replaced.</returns>
		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string OriginalPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the Web Parts control replaced by the proxy Web Parts control.</summary>
		/// <returns>A string containing the <see cref="T:System.Type" /> of the control replaced by the proxy Web Parts control.</returns>
		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string OriginalTypeName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
