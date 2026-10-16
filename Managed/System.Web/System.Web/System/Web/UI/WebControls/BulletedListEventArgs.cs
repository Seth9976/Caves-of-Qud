using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	// Token: 0x0200027C RID: 636
	public class BulletedListEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> class.</summary>
		/// <param name="index">The zero-based index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> that raised the event. </param>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000432E4 File Offset: 0x000414E4
		public BulletedListEventArgs(int index)
		{
			this._index = index;
		}

		/// <summary>Gets the index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</summary>
		/// <returns>The index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</returns>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000432F3 File Offset: 0x000414F3
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001674 RID: 5748
		private int _index;
	}
}
