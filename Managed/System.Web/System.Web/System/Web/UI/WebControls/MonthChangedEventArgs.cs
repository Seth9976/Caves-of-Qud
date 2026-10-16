using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x020002E5 RID: 741
	public class MonthChangedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MonthChangedEventArgs" /> class.</summary>
		/// <param name="newDate">The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		/// <param name="previousDate">The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001B69 RID: 7017 RVA: 0x000436CA File Offset: 0x000418CA
		public MonthChangedEventArgs(DateTime newDate, DateTime previousDate)
		{
			this.newDate = newDate;
			this.previousDate = previousDate;
		}

		/// <summary>Gets the date that determines the currently displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000436E0 File Offset: 0x000418E0
		public DateTime NewDate
		{
			get
			{
				return this.newDate;
			}
		}

		/// <summary>Gets the date that determines the previously displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000436E8 File Offset: 0x000418E8
		public DateTime PreviousDate
		{
			get
			{
				return this.previousDate;
			}
		}

		// Token: 0x0400171C RID: 5916
		private DateTime newDate;

		// Token: 0x0400171D RID: 5917
		private DateTime previousDate;
	}
}
