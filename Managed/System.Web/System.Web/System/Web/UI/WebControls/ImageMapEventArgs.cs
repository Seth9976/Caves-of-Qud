using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ImageMap.Click" /> event of an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control.</summary>
	// Token: 0x020002D6 RID: 726
	public class ImageMapEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageMapEventArgs" /> class.</summary>
		/// <param name="value">The <see cref="T:System.String" /> object assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked. </param>
		// Token: 0x06001B4D RID: 6989 RVA: 0x00043637 File Offset: 0x00041837
		public ImageMapEventArgs(string value)
		{
			this._postBackValue = value;
		}

		/// <summary>Gets the <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</summary>
		/// <returns>The <see cref="T:System.String" /> assigned to the <see cref="P:System.Web.UI.WebControls.HotSpot.PostBackValue" /> property of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that was clicked.</returns>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00043646 File Offset: 0x00041846
		public string PostBackValue
		{
			get
			{
				return this._postBackValue;
			}
		}

		// Token: 0x040016F9 RID: 5881
		private string _postBackValue;
	}
}
