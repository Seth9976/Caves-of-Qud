using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection container that enables a <see cref="T:System.Web.UI.WebControls.MultiView" /> control to maintain a list of its child controls.</summary>
	// Token: 0x02000438 RID: 1080
	public class ViewCollection : ControlCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.MultiView" /> control that owns this collection of child controls. </param>
		// Token: 0x060031C4 RID: 12740 RVA: 0x000287B2 File Offset: 0x000269B2
		public ViewCollection(Control owner)
			: base(owner)
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the collection.</summary>
		/// <param name="v">The <see cref="T:System.Web.UI.WebControls.View" /> control to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="v" /> parameter does not specify a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
		// Token: 0x060031C5 RID: 12741 RVA: 0x00083080 File Offset: 0x00081280
		public override void Add(Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException("The parameter is not a View control");
			}
			base.Add(v);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the collection at the specified index location.</summary>
		/// <param name="index">The array index at which to add the <see cref="T:System.Web.UI.WebControls.View" /> control. </param>
		/// <param name="v">The <see cref="T:System.Web.UI.WebControls.View" /> control to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="v" /> parameter does not specify a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
		// Token: 0x060031C6 RID: 12742 RVA: 0x0008309C File Offset: 0x0008129C
		public override void AddAt(int index, Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException("The parameter is not a View control");
			}
			base.AddAt(index, v);
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.View" /> control at the specified index location in the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> collection.</summary>
		/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.View" /> control in the <see cref="T:System.Web.UI.WebControls.ViewCollection" />.</returns>
		/// <param name="i">The ordinal index value that specifies the location of the <see cref="T:System.Web.UI.WebControls.View" /> control in the <see cref="T:System.Web.UI.WebControls.ViewCollection" />. </param>
		// Token: 0x17000FCA RID: 4042
		public View this[int i]
		{
			get
			{
				return (View)base[i];
			}
		}
	}
}
