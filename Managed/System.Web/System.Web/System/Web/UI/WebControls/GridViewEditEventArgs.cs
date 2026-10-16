using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowEditing" /> event.</summary>
	// Token: 0x020002BC RID: 700
	public class GridViewEditEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewEditEventArgs" /> class.</summary>
		/// <param name="newEditIndex">The index of the row to edit. </param>
		// Token: 0x06001AE5 RID: 6885 RVA: 0x00043579 File Offset: 0x00041779
		public GridViewEditEventArgs(int newEditIndex)
		{
			this._newEditIndex = newEditIndex;
		}

		/// <summary>Gets or sets the index of the row being edited.</summary>
		/// <returns>The index of the row being edited.</returns>
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00043588 File Offset: 0x00041788
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00043590 File Offset: 0x00041790
		public int NewEditIndex
		{
			get
			{
				return this._newEditIndex;
			}
			set
			{
				this._newEditIndex = value;
			}
		}

		// Token: 0x040016DD RID: 5853
		private int _newEditIndex;
	}
}
