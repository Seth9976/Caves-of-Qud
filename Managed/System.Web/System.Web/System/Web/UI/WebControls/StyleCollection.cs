using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.Style" /> objects.</summary>
	// Token: 0x0200040F RID: 1039
	public class StyleCollection : StateManagedCollection
	{
		// Token: 0x06002ECB RID: 11979 RVA: 0x00062525 File Offset: 0x00060725
		internal StyleCollection()
		{
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location in the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />.</returns>
		/// <param name="i">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object in the <see cref="T:System.Web.UI.WebControls.StyleCollection" />. </param>
		// Token: 0x17000EF6 RID: 3830
		public Style this[int i]
		{
			get
			{
				return (Style)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		/// <summary>Appends a specified <see cref="T:System.Web.UI.WebControls.Style" /> object to the end of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <returns>The index at which the style was added to the collection.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to add to the collection.</param>
		// Token: 0x06002ECE RID: 11982 RVA: 0x000624BA File Offset: 0x000606BA
		public int Add(Style style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified style is contained within the collection.</summary>
		/// <returns>true if the style is in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
		// Token: 0x06002ECF RID: 11983 RVA: 0x00052B1E File Offset: 0x00050D1E
		public bool Contains(Style style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> to a one-dimensional <see cref="T:System.Web.UI.WebControls.Style" /> array, starting at the specified index of the target array.</summary>
		/// <param name="styleArray">The <see cref="T:System.Array" /> that is the destination of the copied styles. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="styleArray" /> at which copying begins.</param>
		// Token: 0x06002ED0 RID: 11984 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(Style[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class, based on the single element collection returned by the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class.</returns>
		/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" />, of the type of the <see cref="T:System.Web.UI.IStateManager" /> object to create. Because the <see cref="M:System.Web.UI.WebControls.StyleCollection.GetKnownTypes" /> method of <see cref="T:System.Web.UI.WebControls.StyleCollection" /> returns a list with only one type, the input <paramref name="index" /> is ignored.</param>
		// Token: 0x06002ED1 RID: 11985 RVA: 0x0007A0E2 File Offset: 0x000782E2
		protected override object CreateKnownType(int index)
		{
			return new Style();
		}

		/// <summary>Gets an array of the <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain.</summary>
		/// <returns>An array containing one <see cref="T:System.Type" /> object for the <see cref="T:System.Web.UI.WebControls.Style" /> class, which indicates that the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> can contain <see cref="T:System.Web.UI.WebControls.Style" /> objects.</returns>
		// Token: 0x06002ED2 RID: 11986 RVA: 0x0007A0E9 File Offset: 0x000782E9
		protected override Type[] GetKnownTypes()
		{
			return new Type[] { typeof(Style) };
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.Style" /> object within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the collection; otherwise, -1 if the style is not in the collection.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> to locate within the collection.</param>
		// Token: 0x06002ED3 RID: 11987 RVA: 0x00052B31 File Offset: 0x00050D31
		public int IndexOf(Style style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts a specified <see cref="T:System.Web.UI.WebControls.Style" /> object into the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.Style" /> object. </param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to insert into the collection. </param>
		// Token: 0x06002ED4 RID: 11988 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, Style style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.Style" /> object from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.Style" /> object to remove from the collection. </param>
		// Token: 0x06002ED5 RID: 11989 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(Style style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.Style" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.Style" /> object to remove. </param>
		// Token: 0x06002ED6 RID: 11990 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Instructs the input <see cref="T:System.Web.UI.WebControls.Style" /> object contained in the collection to record its entire state to view state, rather than recording only change information.</summary>
		/// <param name="o">The <see cref="T:System.Web.UI.WebControls.Style" /> object that should serialize itself completely.</param>
		// Token: 0x06002ED7 RID: 11991 RVA: 0x0007A100 File Offset: 0x00078300
		protected override void SetDirtyObject(object o)
		{
			Style style = o as Style;
			if (style == null)
			{
				return;
			}
			style.SetDirty();
		}
	}
}
