using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Identifies the callback method in a server control acting as the consumer in a Web Parts connection, and enables developers to specify details about the consumer's connection point.</summary>
	// Token: 0x020007A3 RID: 1955
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionConsumerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> is null. </exception>
		// Token: 0x06004EBF RID: 20159 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionConsumerAttribute(string displayName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name and an ID for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.ID" />, a unique string value assigned to the consumer connection point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or <paramref name="id" /> is null. </exception>
		// Token: 0x06004EC0 RID: 20160 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionConsumerAttribute(string displayName, string id)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name, an ID, and a specific type of connection point object to use for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="id">The <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.ID" />, a unique string value assigned to the consumer connection point.</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" />, <paramref name="id" />, or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004EC1 RID: 20161 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionConsumerAttribute(string displayName, string id, Type connectionPointType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute" /> class, specifying a display name and a specific type of connection point object to use for the consumer connection point.</summary>
		/// <param name="displayName">A string that contains a friendly name for the consumer connection point to display in the user interface (UI).</param>
		/// <param name="connectionPointType">A <see cref="T:System.Type" /> that derives from <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />, and that you want to specify as the type of connection point object to use with a specific callback method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="displayName" /> or<paramref name=" connectionPointType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="connectionPointType" /> is not valid.</exception>
		// Token: 0x06004EC2 RID: 20162 RVA: 0x00003946 File Offset: 0x00001B46
		public ConnectionConsumerAttribute(string displayName, Type connectionPointType)
		{
		}

		/// <summary>Gets or sets a value that indicates whether the connection point allows multiple connections.</summary>
		/// <returns>true if the connection point accepts multiple connections; otherwise, false.</returns>
		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x000C8C58 File Offset: 0x000C6E58
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x00003946 File Offset: 0x00001B46
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

		/// <summary>Gets the connection point type of the consumer connection point.</summary>
		/// <returns>A <see cref="T:System.Type" /> that indicates the type of the connection point.</returns>
		/// <exception cref="T:System.InvalidOperationException">An invalid connection point type (one that does not derive from the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> class) was used.</exception>
		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ConnectionPointType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the friendly name of the consumer connection point.</summary>
		/// <returns>A string containing a friendly display name for the consumer connection point.</returns>
		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the string used as the value of the <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.DisplayName" /> property, for use in localization scenarios.</summary>
		/// <returns>A string that is used as the value of <see cref="P:System.Web.UI.WebControls.WebParts.ConnectionConsumerAttribute.DisplayName" />. </returns>
		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x00003946 File Offset: 0x00001B46
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

		/// <summary>Gets a string that represents the unique identity of the consumer connection point.</summary>
		/// <returns>The unique ID assigned to the consumer connection point. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
