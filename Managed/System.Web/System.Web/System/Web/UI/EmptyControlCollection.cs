using System;

namespace System.Web.UI
{
	/// <summary>Provides standard support for a <see cref="T:System.Web.UI.ControlCollection" /> collection that is always empty.</summary>
	// Token: 0x0200015A RID: 346
	public class EmptyControlCollection : ControlCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.EmptyControlCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this collection as its collection of child controls. </param>
		// Token: 0x06000EF8 RID: 3832 RVA: 0x000287B2 File Offset: 0x000269B2
		public EmptyControlCollection(Control owner)
			: base(owner)
		{
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000287BB File Offset: 0x000269BB
		private void ThrowNotSupportedException()
		{
			throw new HttpException(global::SR.GetString("'{0}' does not allow child controls.", new object[] { base.Owner.GetType().ToString() }));
		}

		/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
		/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
		// Token: 0x06000EFA RID: 3834 RVA: 0x000287E5 File Offset: 0x000269E5
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection, at the specified index position.</summary>
		/// <param name="index">The index at which to add the <see cref="T:System.Web.UI.Control" />. This parameter is always ignored. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
		/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
		// Token: 0x06000EFB RID: 3835 RVA: 0x000287E5 File Offset: 0x000269E5
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}
