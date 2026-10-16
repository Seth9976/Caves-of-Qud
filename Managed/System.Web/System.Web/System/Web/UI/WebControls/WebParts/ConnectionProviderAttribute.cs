using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Identifies the callback method in a server control acting as the provider in a Web Parts connection, and enables developers to specify details about the provider's connection point.</summary>
	// Token: 0x020007A4 RID: 1956
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute" /> class, specifying a display name for the provider connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the provider connection point to display in the user interface (UI).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> is null. </exception>
		// Token: 0x06004ECA RID: 20170 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionProviderAttribute(string displayName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute" /> class, specifying a display name and an ID for the provider connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the provider connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute.ID" />, a unique string value assigned to the provider connection point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or <paramref name="id" /> is null.</exception>
		// Token: 0x06004ECB RID: 20171 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionProviderAttribute(string displayName, string id)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute" /> class, specifying a display name, an ID, and a specific type of connection point object to use for the provider connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the provider connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute.ID" />, a unique string value assigned to the provider connection point.</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" />, <paramref name="id, " />or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004ECC RID: 20172 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionProviderAttribute(string displayName, string id, Type connectionPointType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute" /> class, specifying a display name and a specific type of connection point object to use for the provider connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the provider connection point to display in the user interface (UI).</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004ECD RID: 20173 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionProviderAttribute(string displayName, Type connectionPointType)
		{
		}

		/// <summary>Gets or sets a value that indicates whether the connection point allows multiple connections.</summary>
		/// <returns>true if the connection point accepts multiple connections; otherwise, false. </returns>
		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x000C8C74 File Offset: 0x000C6E74
		// (set) Token: 0x06004ECF RID: 20175 RVA: 0x00003946 File Offset: 0x00001B46
		public bool AllowsMultipleConnections
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Gets the type of the connection point associated with a provider control.</summary>
		/// <returns>A <see cref="T:System.Type" /> that indicates the type of the connection point.</returns>
		/// <exception cref="T:System.InvalidOperationException">An invalid connection point type (one that does not derive from the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> class) was used.</exception>
		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ConnectionPointType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the friendly name of the provider connection point.</summary>
		/// <returns>A string containing a friendly display name for the provider connection point. </returns>
		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the string used as the value of the <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute.DisplayName" /> property, for use in localization scenarios.</summary>
		/// <returns>A string that is used as the value of <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionProviderAttribute.DisplayName" />.</returns>
		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06004ED2 RID: 20178 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004ED3 RID: 20179 RVA: 0x00003946 File Offset: 0x00001B46
		protected string DisplayNameValue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets a string that represents the unique identity of the provider connection point object.</summary>
		/// <returns>The unique ID assigned to the provider connection point. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
