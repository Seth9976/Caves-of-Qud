using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that is in a <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x0200042D RID: 1069
	public sealed class TreeNodeStyleCollection : StateManagedCollection
	{
		// Token: 0x060030CA RID: 12490 RVA: 0x00062525 File Offset: 0x00060725
		internal TreeNodeStyleCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <returns>The position into which the new <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> was inserted.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to append. </param>
		// Token: 0x060030CB RID: 12491 RVA: 0x0007EB63 File Offset: 0x0007CD63
		public int Add(TreeNodeStyle style)
		{
			style.Font.Underline = style.Font.Underline;
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is in the collection.</summary>
		/// <returns>true, if the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to find. </param>
		// Token: 0x060030CC RID: 12492 RVA: 0x00052B1E File Offset: 0x00050D1E
		public bool Contains(TreeNodeStyle style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x060030CD RID: 12493 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(TreeNodeStyle[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0007EB82 File Offset: 0x0007CD82
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeStyle();
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0007EB89 File Offset: 0x0007CD89
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeStyleCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="style" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />, if found; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to locate.</param>
		// Token: 0x060030D0 RID: 12496 RVA: 0x00052B31 File Offset: 0x00050D31
		public int IndexOf(TreeNodeStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" />. </param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to insert. </param>
		// Token: 0x060030D1 RID: 12497 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, TreeNodeStyle style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
		// Token: 0x060030D2 RID: 12498 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(TreeNodeStyle style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to remove. </param>
		// Token: 0x060030D3 RID: 12499 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeStyleCollection" />.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeStyle" /> to retrieve. </param>
		// Token: 0x17000F90 RID: 3984
		public TreeNodeStyle this[int i]
		{
			get
			{
				return (TreeNodeStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0007EB9E File Offset: 0x0007CD9E
		protected override void SetDirtyObject(object o)
		{
			((TreeNodeStyle)o).SetDirty();
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0006C63C File Offset: 0x0006A83C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
		}

		// Token: 0x04001C22 RID: 7202
		private static Type[] types = new Type[] { typeof(TreeNodeStyle) };
	}
}
