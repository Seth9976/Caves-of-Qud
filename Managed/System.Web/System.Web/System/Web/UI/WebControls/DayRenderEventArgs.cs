using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000381 RID: 897
	public sealed class DayRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell and calendar day.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		// Token: 0x0600228F RID: 8847 RVA: 0x00057397 File Offset: 0x00055597
		public DayRenderEventArgs(TableCell cell, CalendarDay day)
		{
			this.cell = cell;
			this.day = day;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell, calendar day, and selection URL.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="selectUrl">The script used to post the page back to the server when the user selects the date being rendered.</param>
		// Token: 0x06002290 RID: 8848 RVA: 0x000573AD File Offset: 0x000555AD
		public DayRenderEventArgs(TableCell cell, CalendarDay day, string selectUrl)
			: this(cell, day)
		{
			this._selectUrl = selectUrl;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000573BE File Offset: 0x000555BE
		public TableCell Cell
		{
			get
			{
				return this.cell;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> object that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000573C6 File Offset: 0x000555C6
		public CalendarDay Day
		{
			get
			{
				return this.day;
			}
		}

		/// <summary>Gets the script used to post the page back to the server when the date being rendered is selected in a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The script used to post the page back to the server when the date being rendered is selected.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000573CE File Offset: 0x000555CE
		public string SelectUrl
		{
			get
			{
				return this._selectUrl;
			}
		}

		// Token: 0x04001934 RID: 6452
		private TableCell cell;

		// Token: 0x04001935 RID: 6453
		private CalendarDay day;

		// Token: 0x04001936 RID: 6454
		private string _selectUrl;
	}
}
