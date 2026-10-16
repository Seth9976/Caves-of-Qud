using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents a collection of custom Web Parts verbs. This class cannot be inherited. </summary>
	// Token: 0x0200048A RID: 1162
	public sealed class WebPartVerbCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class.</summary>
		// Token: 0x060034BE RID: 13502 RVA: 0x00087FBC File Offset: 0x000861BC
		public WebPartVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class using the specified collection.</summary>
		/// <param name="verbs">An object derived from <see cref="T:System.Collections.ICollection" /> that contains a set of Web Parts verbs.</param>
		// Token: 0x060034BF RID: 13503 RVA: 0x00087FC4 File Offset: 0x000861C4
		public WebPartVerbCollection(ICollection verbs)
		{
			base.InnerList.AddRange(verbs);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> class using the specified collections.</summary>
		/// <param name="existingVerbs">An existing <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" />.</param>
		/// <param name="verbs">An object derived from <see cref="T:System.Collections.ICollection" /> that contains a set of Web Parts verbs.</param>
		// Token: 0x060034C0 RID: 13504 RVA: 0x000884F7 File Offset: 0x000866F7
		public WebPartVerbCollection(WebPartVerbCollection existingVerbs, ICollection verbs)
		{
			base.InnerList.AddRange(existingVerbs.InnerList);
			base.InnerList.AddRange(verbs);
		}

		/// <summary>Searches the Web Parts verb collection for the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object.</summary>
		/// <returns>true if the collection contains the Web Parts verb; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> to be found.</param>
		// Token: 0x060034C1 RID: 13505 RVA: 0x00087FF8 File Offset: 0x000861F8
		public bool Contains(WebPartVerb value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies elements of the collection to the specified array, starting at the specified index.</summary>
		/// <param name="array">The array that elements are to be copied to.</param>
		/// <param name="index">The index where copying should begin.</param>
		// Token: 0x060034C2 RID: 13506 RVA: 0x0008851C File Offset: 0x0008671C
		public void CopyTo(WebPartVerb[] array, int index)
		{
			base.InnerList.CopyTo(0, array, index, this.Count);
		}

		/// <summary>Searches for the specified Web Parts verb and returns the zero-based index of the first occurrence within the entire collection.</summary>
		/// <returns>The index of the Web Parts verb.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> to be located.</param>
		// Token: 0x060034C3 RID: 13507 RVA: 0x00088015 File Offset: 0x00086215
		public int IndexOf(WebPartVerb value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Gets a Web Parts verb from the collection at the specified index.</summary>
		/// <returns>A Web Parts verb from the collection.</returns>
		/// <param name="index">The index value of the Web Parts verb to be retrieved.</param>
		// Token: 0x170010B6 RID: 4278
		public WebPartVerb this[int index]
		{
			get
			{
				return (WebPartVerb)base.InnerList[index];
			}
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one. This static field is read-only.</summary>
		// Token: 0x04001D3C RID: 7484
		public static readonly WebPartVerbCollection Empty = new WebPartVerbCollection();
	}
}
