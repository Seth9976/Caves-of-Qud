using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Contains a collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls used for editing the properties, layout, appearance, and behavior of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. This class cannot be inherited. </summary>
	// Token: 0x0200047F RID: 1151
	public sealed class EditorPartCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class.</summary>
		// Token: 0x06003428 RID: 13352 RVA: 0x00087FBC File Offset: 0x000861BC
		public EditorPartCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls. </param>
		// Token: 0x06003429 RID: 13353 RVA: 0x00088144 File Offset: 0x00086344
		public EditorPartCollection(ICollection editorParts)
		{
			foreach (object obj in editorParts)
			{
				base.InnerList.Add(obj);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls, and an <see cref="T:System.Collections.ICollection" /> collection of additional <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="existingEditorParts">An <see cref="T:System.Collections.ICollection" /> of existing <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone. </param>
		/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls not in a zone, but created programmatically. </param>
		// Token: 0x0600342A RID: 13354 RVA: 0x000881A0 File Offset: 0x000863A0
		public EditorPartCollection(EditorPartCollection existingEditorParts, ICollection editorParts)
		{
			foreach (object obj in existingEditorParts)
			{
				base.InnerList.Add(obj);
			}
			foreach (object obj2 in editorParts)
			{
				base.InnerList.Add(obj2);
			}
		}

		/// <summary>Returns a value that indicates whether a particular control is in the collection.</summary>
		/// <returns>A Boolean value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> is in the collection.</returns>
		/// <param name="editorPart">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> being tested for its status as a member of the collection. </param>
		// Token: 0x0600342B RID: 13355 RVA: 0x00087FF8 File Offset: 0x000861F8
		public bool Contains(EditorPart editorPart)
		{
			return base.InnerList.Contains(editorPart);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <param name="array">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> to contain the copied collection of controls. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x0600342C RID: 13356 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(EditorPart[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Returns the position of a particular member of the collection.</summary>
		/// <returns>An integer that corresponds to the index of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control in the collection.</returns>
		/// <param name="editorPart">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> that is a member of the collection. </param>
		// Token: 0x0600342D RID: 13357 RVA: 0x00088015 File Offset: 0x00086215
		public int IndexOf(EditorPart editorPart)
		{
			return base.InnerList.IndexOf(editorPart);
		}

		/// <summary>Returns a specific member of the collection according to a unique identifier.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> at the specified index in the collection. </returns>
		/// <param name="index">The index of a particular <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> in a collection. </param>
		// Token: 0x17001076 RID: 4214
		public EditorPart this[int index]
		{
			get
			{
				return (EditorPart)base.InnerList[index];
			}
		}

		/// <summary>References a static, read-only, empty instance of the collection. </summary>
		// Token: 0x04001D05 RID: 7429
		public static readonly EditorPartCollection Empty = new EditorPartCollection();
	}
}
