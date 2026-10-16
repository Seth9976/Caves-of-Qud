using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls used to track and manage related groups of controls. This class cannot be inherited.</summary>
	// Token: 0x020006BD RID: 1725
	public sealed class WebPartCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> class.</summary>
		// Token: 0x06004941 RID: 18753 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> object by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <param name="webParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="webParts" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An object in the <paramref name="webParts" /> collection is null.- or -An object in the <paramref name="webParts" /> collection is not a of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.</exception>
		// Token: 0x06004942 RID: 18754 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartCollection(ICollection webParts)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPart get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a member of the collection based on a unique string identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> in the collection whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">The unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  control in a collection.</param>
		// Token: 0x170016A0 RID: 5792
		public WebPart this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular control exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular control is in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is checked to determine whether it is in the collection.</param>
		// Token: 0x06004945 RID: 18757 RVA: 0x000C7A4C File Offset: 0x000C5C4C
		public bool Contains(WebPart value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />  objects to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x06004946 RID: 18758 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(WebPart[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is a member of the collection. </param>
		// Token: 0x06004947 RID: 18759 RVA: 0x000C7A68 File Offset: 0x000C5C68
		public int IndexOf(WebPart value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
