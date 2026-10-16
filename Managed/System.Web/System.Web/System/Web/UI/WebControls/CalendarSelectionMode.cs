using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the date selection mode of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x02000280 RID: 640
	public enum CalendarSelectionMode
	{
		/// <summary>No dates can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x0400167D RID: 5757
		None,
		/// <summary>A single date can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x0400167E RID: 5758
		Day,
		/// <summary>A single date or entire week can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x0400167F RID: 5759
		DayWeek,
		/// <summary>A single date, week, or entire month can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x04001680 RID: 5760
		DayWeekMonth
	}
}
