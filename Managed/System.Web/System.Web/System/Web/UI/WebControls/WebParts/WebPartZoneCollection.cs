using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> controls.</summary>
	// Token: 0x020006BE RID: 1726
	public sealed class WebPartZoneCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneCollection" /> class.</summary>
		// Token: 0x06004948 RID: 18760 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartZoneCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneCollection" /> class by passing in a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> objects.</summary>
		/// <param name="webPartZones">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection of zones is null. </exception>
		/// <exception cref="T:System.ArgumentException">One of the objects in the collection is null or is not of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" />.</exception>
		// Token: 0x06004949 RID: 18761 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebPartZoneCollection(ICollection webPartZones)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartZoneBase get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a specific member of the collection by passing in a unique identifier.</summary>
		/// <returns>The first <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> in the collection whose ID equals the value of <paramref name="id" />.</returns>
		/// <param name="id">The unique identifier for a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> within the collection. </param>
		// Token: 0x170016A1 RID: 5793
		public WebPartZoneBase this[string id]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular zone exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular zone is in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> that is checked to determine whether it is in the collection. </param>
		// Token: 0x0600494C RID: 18764 RVA: 0x000C7A84 File Offset: 0x000C5C84
		public bool Contains(WebPartZoneBase value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> objects.</summary>
		/// <param name="array">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> array to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x0600494D RID: 18765 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(WebPartZoneBase[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that is a member of the collection. </param>
		// Token: 0x0600494E RID: 18766 RVA: 0x000C7AA0 File Offset: 0x000C5CA0
		public int IndexOf(WebPartZoneBase value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
