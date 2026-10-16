using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects.</summary>
	// Token: 0x020003CE RID: 974
	public sealed class MenuItemBindingCollection : StateManagedCollection
	{
		// Token: 0x060029DA RID: 10714 RVA: 0x00062525 File Offset: 0x00060725
		internal MenuItemBindingCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the end of the collection.</summary>
		/// <returns>The index at which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> was inserted in the collection.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to append to the end of the collection.</param>
		// Token: 0x060029DB RID: 10715 RVA: 0x000624BA File Offset: 0x000606BA
		public int Add(MenuItemBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to find.</param>
		// Token: 0x060029DC RID: 10716 RVA: 0x00052B1E File Offset: 0x00050D1E
		public bool Contains(MenuItemBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects that receives the copied items from the collection.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x060029DD RID: 10717 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(MenuItemBinding[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0006BDC7 File Offset: 0x00069FC7
		protected override object CreateKnownType(int index)
		{
			return new MenuItemBinding();
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0006BDCE File Offset: 0x00069FCE
		protected override Type[] GetKnownTypes()
		{
			return MenuItemBindingCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to determine the index of.</param>
		// Token: 0x060029E0 RID: 10720 RVA: 0x00052B31 File Offset: 0x00050D31
		public int IndexOf(MenuItemBinding value)
		{
			return ((IList)this).IndexOf(value);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</param>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to insert.</param>
		// Token: 0x060029E1 RID: 10721 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, MenuItemBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to remove from the collection.</param>
		// Token: 0x060029E2 RID: 10722 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(MenuItemBinding binding)
		{
			((IList)this).Remove(binding);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The zero-based index location of the menu item binding to remove.</param>
		// Token: 0x060029E3 RID: 10723 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object at the specified index from the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> at the specified index in the collection.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> to retrieve.</param>
		// Token: 0x17000D7F RID: 3455
		public MenuItemBinding this[int i]
		{
			get
			{
				return (MenuItemBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0006BDED File Offset: 0x00069FED
		protected override void SetDirtyObject(object o)
		{
			((MenuItemBinding)o).SetDirty();
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0006BDFA File Offset: 0x00069FFA
		protected override void OnClear()
		{
			base.OnClear();
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0006BE02 File Offset: 0x0006A002
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00052BDE File Offset: 0x00050DDE
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		// Token: 0x04001AC7 RID: 6855
		private static Type[] types = new Type[] { typeof(MenuItemBinding) };
	}
}
