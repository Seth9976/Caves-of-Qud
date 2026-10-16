using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.SiteMapPath.ItemDataBound" /> events.</summary>
	// Token: 0x02000303 RID: 771
	public class SiteMapNodeItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemEventArgs" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object as the source of the event.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event. </param>
		// Token: 0x06001BB6 RID: 7094 RVA: 0x00043852 File Offset: 0x00041A52
		public SiteMapNodeItemEventArgs(SiteMapNodeItem item)
		{
			this._item = item;
		}

		/// <summary>Gets the node item that is the source of the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> that is the source of the event.</returns>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00043861 File Offset: 0x00041A61
		public SiteMapNodeItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x04001752 RID: 5970
		private SiteMapNodeItem _item;
	}
}
