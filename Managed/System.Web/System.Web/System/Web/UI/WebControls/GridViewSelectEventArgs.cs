using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanging" /> event.</summary>
	// Token: 0x020002C2 RID: 706
	public class GridViewSelectEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSelectEventArgs" /> class.</summary>
		/// <param name="newSelectedIndex">The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control. </param>
		// Token: 0x06001AF9 RID: 6905 RVA: 0x000435DF File Offset: 0x000417DF
		public GridViewSelectEventArgs(int newSelectedIndex)
		{
			this._newSelectedIndex = newSelectedIndex;
		}

		/// <summary>Gets or sets the index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000435EE File Offset: 0x000417EE
		// (set) Token: 0x06001AFB RID: 6907 RVA: 0x000435F6 File Offset: 0x000417F6
		public int NewSelectedIndex
		{
			get
			{
				return this._newSelectedIndex;
			}
			set
			{
				this._newSelectedIndex = value;
			}
		}

		// Token: 0x040016E0 RID: 5856
		private int _newSelectedIndex;
	}
}
