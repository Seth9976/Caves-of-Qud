using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a read-only collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
	// Token: 0x020006B3 RID: 1715
	public sealed class TransformerTypeCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class with no members in the collection. </summary>
		// Token: 0x060048C2 RID: 18626 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TransformerTypeCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class containing the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="transformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="transformerTypes" /> contains objects that are not transformers.</exception>
		// Token: 0x060048C3 RID: 18627 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TransformerTypeCollection(ICollection transformerTypes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> class by combining an existing <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" /> collection with the specified <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="existingTransformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects already contained in a <see cref="T:System.Web.UI.WebControls.WebParts.TransformerTypeCollection" />. </param>
		/// <param name="transformerTypes">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects to combine with the collection in the <paramref name="existingTransformerTypes" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="transformerTypes" /> contains objects that are not transformers.</exception>
		// Token: 0x060048C4 RID: 18628 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TransformerTypeCollection(TransformerTypeCollection existingTransformerTypes, ICollection transformerTypes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a member of the collection based on its position in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> located at <paramref name="index" />.</returns>
		/// <param name="index">The index of a particular <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> in a collection. </param>
		// Token: 0x17001679 RID: 5753
		public Type this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a value indicating whether a particular transformer exists in the collection.</summary>
		/// <returns>A Boolean value that indicates whether a particular transformer is in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is checked to determine whether it is in the collection. </param>
		// Token: 0x060048C6 RID: 18630 RVA: 0x000C76B0 File Offset: 0x000C58B0
		public bool Contains(Type value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects.</summary>
		/// <param name="array">An array of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> objects to contain the copied collection. </param>
		/// <param name="index">The starting point in the array at which to place the collection contents. </param>
		// Token: 0x060048C7 RID: 18631 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(Type[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the position of a particular member in the collection.</summary>
		/// <returns>An integer that indicates the position of a particular object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> that is a member of the collection.</param>
		// Token: 0x060048C8 RID: 18632 RVA: 0x000C76CC File Offset: 0x000C58CC
		public int IndexOf(Type value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Creates a collection for storing transformers. </summary>
		// Token: 0x040025D8 RID: 9688
		public static readonly TransformerTypeCollection Empty;
	}
}
