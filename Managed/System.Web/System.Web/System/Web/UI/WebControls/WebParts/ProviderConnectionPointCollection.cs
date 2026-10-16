using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of all <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects associated with a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control acting as a provider in a connection. This class cannot be inherited.</summary>
	// Token: 0x020006D5 RID: 1749
	public sealed class ProviderConnectionPointCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" /> class. </summary>
		// Token: 0x06004A1A RID: 18970 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProviderConnectionPointCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" /> class using the specified collection of provider connection points.</summary>
		/// <param name="connectionPoints">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects used to create the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="connectionPoints" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The collection contains a null connection point.- or -The collection contains an object that is not of type <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" />.- or -There are duplicate IDs in the collection of connection points. </exception>
		// Token: 0x06004A1B RID: 18971 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProviderConnectionPointCollection(ICollection connectionPoints)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default connection point from the collection of provider connection points associated with a particular control.</summary>
		/// <returns>The default <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> from a <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" />.</returns>
		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProviderConnectionPoint Default
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProviderConnectionPoint get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a member of the collection based on a unique string identifier. </summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">A <see cref="T:System.String" /> serving as the unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> in the collection.</param>
		// Token: 0x170016F4 RID: 5876
		public ProviderConnectionPoint this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> object exists in the collection. </summary>
		/// <returns>true if the provider connection point is contained in the collection; otherwise, false.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> to search for.</param>
		// Token: 0x06004A1F RID: 18975 RVA: 0x000C7D94 File Offset: 0x000C5F94
		public bool Contains(ProviderConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> objects that receives the copied items of the collection.</param>
		/// <param name="index">The starting point in the array at which to insert the collection contents.</param>
		// Token: 0x06004A20 RID: 18976 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(ProviderConnectionPoint[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> object within a collection.</summary>
		/// <returns>An integer that indicates the zero-based index position of the specified <paramref name="connectionPoint" /> within a <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPointCollection" />.</returns>
		/// <param name="connectionPoint">The <see cref="T:System.Web.UI.WebControls.WebParts.ProviderConnectionPoint" /> to locate.</param>
		// Token: 0x06004A21 RID: 18977 RVA: 0x000C7DB0 File Offset: 0x000C5FB0
		public int IndexOf(ProviderConnectionPoint connectionPoint)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
