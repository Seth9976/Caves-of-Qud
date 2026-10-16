using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the properties and methods that must be implemented by any list control that repeats a list of items.</summary>
	// Token: 0x020002D3 RID: 723
	public interface IRepeatInfoUser
	{
		/// <summary>Gets a value indicating whether the list control contains a heading section.</summary>
		/// <returns>true if the list control contains a heading section; otherwise, false.</returns>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001B3B RID: 6971
		bool HasHeader { get; }

		/// <summary>Gets a value indicating whether the list control contains a footer section.</summary>
		/// <returns>true if the list control contains a footer section; otherwise, false.</returns>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001B3C RID: 6972
		bool HasFooter { get; }

		/// <summary>Gets a value indicating whether the list control contains a separator between items in the list.</summary>
		/// <returns>true if the list control contains a separator; otherwise, false.</returns>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001B3D RID: 6973
		bool HasSeparators { get; }

		/// <summary>Gets the number of items in the list control.</summary>
		/// <returns>The number of items in the list control.</returns>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001B3E RID: 6974
		int RepeatedItemCount { get; }

		/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06001B3F RID: 6975
		Style GetItemStyle(ListItemType itemType, int repeatIndex);

		/// <summary>Renders an item in the list with the specified information.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001B40 RID: 6976
		void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer);
	}
}
