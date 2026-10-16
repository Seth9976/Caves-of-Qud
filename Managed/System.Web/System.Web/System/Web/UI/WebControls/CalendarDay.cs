using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a date in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x02000340 RID: 832
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CalendarDay
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class.</summary>
		/// <param name="date">A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. </param>
		/// <param name="isWeekend">true to indicate that the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, false. </param>
		/// <param name="isToday">true to indicate that the date represented by an instance of this class is the current date; otherwise, false. </param>
		/// <param name="isSelected">true to indicate that the date represented by an instance of this class is selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false. </param>
		/// <param name="isOtherMonth">true to indicate that the date represented by an instance of this class is in a month other than the displayed month on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false. </param>
		/// <param name="dayNumberText">The day number for the date represented by this class. </param>
		// Token: 0x06001E01 RID: 7681 RVA: 0x000495DE File Offset: 0x000477DE
		public CalendarDay(DateTime date, bool isWeekend, bool isToday, bool isSelected, bool isOtherMonth, string dayNumberText)
		{
			this.date = date;
			this.isWeekend = isWeekend;
			this.isToday = isToday;
			this.isSelected = isSelected;
			this.isOtherMonth = isOtherMonth;
			this.dayNumberText = dayNumberText;
			this.isSelectable = false;
		}

		/// <summary>Gets the date represented by an instance of this class. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. This allows you to programmatically control the appearance or behavior of the day, based on this value.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0004961A File Offset: 0x0004781A
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		/// <summary>Gets the string equivalent of the day number for the date represented by an instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class. This property is read-only.</summary>
		/// <returns>The string equivalent of the day number for the date represented by an instance of this class.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00049622 File Offset: 0x00047822
		public string DayNumberText
		{
			get
			{
				return this.dayNumberText;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0004962A File Offset: 0x0004782A
		public bool IsOtherMonth
		{
			get
			{
				return this.isOtherMonth;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the date represented by an instance of this class can be selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>true if the date can be selected; otherwise, false.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00049632 File Offset: 0x00047832
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0004963A File Offset: 0x0004783A
		public bool IsSelectable
		{
			get
			{
				return this.isSelectable;
			}
			set
			{
				this.isSelectable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00049643 File Offset: 0x00047843
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0004964B File Offset: 0x0004784B
		public bool IsToday
		{
			get
			{
				return this.isToday;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is a either Saturday or Sunday. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, false.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00049653 File Offset: 0x00047853
		public bool IsWeekend
		{
			get
			{
				return this.isWeekend;
			}
		}

		// Token: 0x04001846 RID: 6214
		private DateTime date;

		// Token: 0x04001847 RID: 6215
		private bool isWeekend;

		// Token: 0x04001848 RID: 6216
		private bool isToday;

		// Token: 0x04001849 RID: 6217
		private bool isSelected;

		// Token: 0x0400184A RID: 6218
		private bool isOtherMonth;

		// Token: 0x0400184B RID: 6219
		private string dayNumberText;

		// Token: 0x0400184C RID: 6220
		private bool isSelectable;
	}
}
