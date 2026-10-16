using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003D2 RID: 978
	public sealed class MenuItemStyleCollection : StateManagedCollection
	{
		// Token: 0x06002A0F RID: 10767 RVA: 0x00062525 File Offset: 0x00060725
		internal MenuItemStyleCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object to the end of the current collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to append to the end of the current collection.</param>
		// Token: 0x06002A10 RID: 10768 RVA: 0x000624BA File Offset: 0x000606BA
		public int Add(MenuItemStyle style)
		{
			return ((IList)this).Add(style);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to find.</param>
		// Token: 0x06002A11 RID: 10769 RVA: 0x00052B1E File Offset: 0x00050D1E
		public bool Contains(MenuItemStyle style)
		{
			return ((IList)this).Contains(style);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="styleArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> objects that received the copied items from the <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" />.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x06002A12 RID: 10770 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(MenuItemStyle[] styleArray, int index)
		{
			((ICollection)this).CopyTo(styleArray, index);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x0006C613 File Offset: 0x0006A813
		protected override object CreateKnownType(int index)
		{
			return new MenuItemStyle();
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x0006C61A File Offset: 0x0006A81A
		protected override Type[] GetKnownTypes()
		{
			return MenuItemStyleCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />, if found; otherwise, -1.</returns>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to locate.</param>
		// Token: 0x06002A15 RID: 10773 RVA: 0x00052B31 File Offset: 0x00050D31
		public int IndexOf(MenuItemStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object into the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />.</param>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to insert.</param>
		// Token: 0x06002A16 RID: 10774 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, MenuItemStyle style)
		{
			((IList)this).Insert(index, style);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object from the collection.</summary>
		/// <param name="style">The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
		// Token: 0x06002A17 RID: 10775 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(MenuItemStyle style)
		{
			((IList)this).Remove(style);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> to remove.</param>
		// Token: 0x06002A18 RID: 10776 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object at the specified index from the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> at the specified index in the collection.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" />  to retrieve.</param>
		// Token: 0x17000D88 RID: 3464
		public MenuItemStyle this[int i]
		{
			get
			{
				return (MenuItemStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0006C62F File Offset: 0x0006A82F
		protected override void SetDirtyObject(object o)
		{
			((MenuItemStyle)o).SetDirty();
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0006C63C File Offset: 0x0006A83C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
		}

		// Token: 0x04001AD1 RID: 6865
		private static Type[] types = new Type[] { typeof(MenuItemStyle) };
	}
}
