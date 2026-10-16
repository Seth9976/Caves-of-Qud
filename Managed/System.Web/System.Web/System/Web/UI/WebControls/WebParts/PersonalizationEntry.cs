using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents core pieces of custom personalization state information contained in a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object. This class cannot be inherited. </summary>
	// Token: 0x020006D7 RID: 1751
	public sealed class PersonalizationEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> class using the specified value and scope.</summary>
		/// <param name="value">An object of personalization data associated with the personalization scope in the <paramref name="scope" /> parameter.</param>
		/// <param name="scope">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> that applies to the custom personalization information.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is set to a value that is not a valid member of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" />.</exception>
		// Token: 0x06004A3A RID: 19002 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationEntry(object value, PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> class using the provided parameters.</summary>
		/// <param name="value">An object of personalization data associated with the personalization scope in the <paramref name="scope" /> parameter.</param>
		/// <param name="scope">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> that applies to the custom personalization information.</param>
		/// <param name="isSensitive">A Boolean value indicating if the custom state information is sensitive and should not be exported.</param>
		// Token: 0x06004A3B RID: 19003 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationEntry(object value, PersonalizationScope scope, bool isSensitive)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Boolean value that indicates if the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> contains sensitive information.</summary>
		/// <returns>true if the sensitive setting for the attribute is set; otherwise false.</returns>
		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x000C7E74 File Offset: 0x000C6074
		// (set) Token: 0x06004A3D RID: 19005 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool IsSensitive
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

		/// <summary>Gets or sets the personalization scope associated with this personalization data entry.</summary>
		/// <returns>The personalization scope associated with this personalization data entry.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is set to a value that is not a valid member of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" />.</exception>
		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x000C7E90 File Offset: 0x000C6090
		// (set) Token: 0x06004A3F RID: 19007 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationScope Scope
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

		/// <summary>Gets or sets the personalization state information for this entry.</summary>
		/// <returns>An object representing personalization state information.</returns>
		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A41 RID: 19009 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public object Value
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
