using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging" /> event.</summary>
	// Token: 0x020002BE RID: 702
	public class GridViewPageEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs" /> class.</summary>
		/// <param name="newPageIndex">The index of the new page to display. </param>
		// Token: 0x06001AEC RID: 6892 RVA: 0x00043599 File Offset: 0x00041799
		public GridViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridViewPageEventArgs.NewPageIndex" /> property is less than zero.</exception>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000435A8 File Offset: 0x000417A8
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x000435B0 File Offset: 0x000417B0
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._newPageIndex = value;
			}
		}

		// Token: 0x040016DE RID: 5854
		private int _newPageIndex;
	}
}
