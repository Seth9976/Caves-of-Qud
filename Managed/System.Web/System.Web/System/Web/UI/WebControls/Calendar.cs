using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200033F RID: 831
	[ControlValueProperty("SelectedDate", "1/1/0001 12:00:00 AM")]
	[DataBindingHandler("System.Web.UI.Design.WebControls.CalendarDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SelectionChanged")]
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.CalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("SelectedDate")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Calendar : WebControl, IPostBackEventHandler
	{
		/// <summary>Gets or sets a text value that is rendered as a caption for the calendar.</summary>
		/// <returns>The table caption.</returns>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00044F8F File Offset: 0x0004318F
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x00047FD1 File Offset: 0x000461D1
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				return this.ViewState.GetString("Caption", string.Empty);
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text that is rendered as a caption for the calendar.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> value that indicates the alignment of the caption. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values.</exception>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00044FCD File Offset: 0x000431CD
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x00047FE4 File Offset: 0x000461E4
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[DefaultValue(TableCaptionAlign.NotSet)]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				return (TableCaptionAlign)this.ViewState.GetInt("CaptionAlign", 0);
			}
			set
			{
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell padding is less than -1. </exception>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x00047FFC File Offset: 0x000461FC
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x0004800F File Offset: 0x0004620F
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(2)]
		public int CellPadding
		{
			get
			{
				return this.ViewState.GetInt("CellPadding", 2);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("The specified cell padding is less than -1.");
				}
				this.ViewState["CellPadding"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space (in pixels) between cells. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell spacing is less than -1. </exception>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00048036 File Offset: 0x00046236
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x00048049 File Offset: 0x00046249
		[WebSysDescription("")]
		[DefaultValue(0)]
		[WebCategory("Layout")]
		public int CellSpacing
		{
			get
			{
				return this.ViewState.GetInt("CellSpacing", 0);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("The specified cell spacing is less than -1");
				}
				this.ViewState["CellSpacing"] = value;
			}
		}

		/// <summary>Gets the style properties for the section that displays the day of the week.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the section that displays the day of the week. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00048070 File Offset: 0x00046270
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public TableItemStyle DayHeaderStyle
		{
			get
			{
				if (this.dayHeaderStyle == null)
				{
					this.dayHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dayHeaderStyle.TrackViewState();
					}
				}
				return this.dayHeaderStyle;
			}
		}

		/// <summary>Gets or sets the name format for days of the week.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. The default value is Short.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified day name format is not one of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. </exception>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0004809E File Offset: 0x0004629E
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x000480B1 File Offset: 0x000462B1
		[DefaultValue(DayNameFormat.Short)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public DayNameFormat DayNameFormat
		{
			get
			{
				return (DayNameFormat)this.ViewState.GetInt("DayNameFormat", 1);
			}
			set
			{
				if (value != DayNameFormat.FirstLetter && value != DayNameFormat.FirstTwoLetters && value != DayNameFormat.Full && value != DayNameFormat.Short && value != DayNameFormat.Shortest)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["DayNameFormat"] = value;
			}
		}

		/// <summary>Gets the style properties for the days in the displayed month.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000480E7 File Offset: 0x000462E7
		[WebCategory("Style")]
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TableItemStyle DayStyle
		{
			get
			{
				if (this.dayStyle == null)
				{
					this.dayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dayStyle.TrackViewState();
					}
				}
				return this.dayStyle;
			}
		}

		/// <summary>Gets or sets the day of the week to display in the first day column of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. The default is Default, which indicates that the day specified in the system setting is used.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date specified is not one of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. </exception>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00048115 File Offset: 0x00046315
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x00048128 File Offset: 0x00046328
		[DefaultValue(FirstDayOfWeek.Default)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public FirstDayOfWeek FirstDayOfWeek
		{
			get
			{
				return (FirstDayOfWeek)this.ViewState.GetInt("FirstDayOfWeek", 7);
			}
			set
			{
				if (value < FirstDayOfWeek.Sunday || value > FirstDayOfWeek.Default)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["FirstDayOfWeek"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the next month navigation control.</summary>
		/// <returns>The caption text for the next month navigation control. The default value is "&amp;gt;", which is rendered as the greater than sign (&gt;).</returns>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00048153 File Offset: 0x00046353
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0004816A File Offset: 0x0004636A
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		public string NextMonthText
		{
			get
			{
				return this.ViewState.GetString("NextMonthText", "&gt;");
			}
			set
			{
				this.ViewState["NextMonthText"] = value;
			}
		}

		/// <summary>Gets or sets the format of the next and previous month navigation elements in the title section of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. The default value is CustomText.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified format is not one of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. </exception>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0004817D File Offset: 0x0004637D
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x00048190 File Offset: 0x00046390
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(NextPrevFormat.CustomText)]
		public NextPrevFormat NextPrevFormat
		{
			get
			{
				return (NextPrevFormat)this.ViewState.GetInt("NextPrevFormat", 0);
			}
			set
			{
				if (value != NextPrevFormat.CustomText && value != NextPrevFormat.ShortMonth && value != NextPrevFormat.FullMonth)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["NextPrevFormat"] = value;
			}
		}

		/// <summary>Gets the style properties for the next and previous month navigation elements.</summary>
		/// <returns>The style properties for the next and previous month navigation elements. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000481BE File Offset: 0x000463BE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public TableItemStyle NextPrevStyle
		{
			get
			{
				if (this.nextPrevStyle == null)
				{
					this.nextPrevStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.nextPrevStyle.TrackViewState();
					}
				}
				return this.nextPrevStyle;
			}
		}

		/// <summary>Gets the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000481EC File Offset: 0x000463EC
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle OtherMonthDayStyle
		{
			get
			{
				if (this.otherMonthDayStyle == null)
				{
					this.otherMonthDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.otherMonthDayStyle.TrackViewState();
					}
				}
				return this.otherMonthDayStyle;
			}
		}

		/// <summary>Gets or sets the text displayed for the previous month navigation control.</summary>
		/// <returns>The caption text for the previous month navigation control. The default value is "&amp;lt;", which is rendered as the less than sign (&lt;).</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0004821A File Offset: 0x0004641A
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00048231 File Offset: 0x00046431
		[DefaultValue("&lt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string PrevMonthText
		{
			get
			{
				return this.ViewState.GetString("PrevMonthText", "&lt;");
			}
			set
			{
				this.ViewState["PrevMonthText"] = value;
			}
		}

		/// <summary>Gets or sets the selected date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the selected date. The default value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00048244 File Offset: 0x00046444
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x00048266 File Offset: 0x00046466
		[WebCategory("Appearance")]
		[Bindable(true, BindingDirection.TwoWay)]
		[DefaultValue("1/1/0001 12:00:00 AM")]
		[WebSysDescription("")]
		public DateTime SelectedDate
		{
			get
			{
				if (this.SelectedDates.Count > 0)
				{
					return this.SelectedDates[0];
				}
				return DateTime.MinValue;
			}
			set
			{
				this.SelectedDates.SelectRange(value, value);
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.DateTime" /> objects that represent the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> that contains a collection of <see cref="T:System.DateTime" /> objects representing the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x00048275 File Offset: 0x00046475
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SelectedDatesCollection SelectedDates
		{
			get
			{
				if (this.dateList == null)
				{
					this.dateList = new ArrayList();
				}
				if (this.selectedDatesCollection == null)
				{
					this.selectedDatesCollection = new SelectedDatesCollection(this.dateList);
				}
				return this.selectedDatesCollection;
			}
		}

		/// <summary>Gets the style properties for the selected dates.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the selected dates. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x000482A9 File Offset: 0x000464A9
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public TableItemStyle SelectedDayStyle
		{
			get
			{
				if (this.selectedDayStyle == null)
				{
					this.selectedDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedDayStyle.TrackViewState();
					}
				}
				return this.selectedDayStyle;
			}
		}

		/// <summary>Gets or sets the date selection mode on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that specifies whether the user can select a single day, a week, or an entire month.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. The default value is Day.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </exception>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000482D7 File Offset: 0x000464D7
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x000482EA File Offset: 0x000464EA
		[WebCategory("Behavior")]
		[DefaultValue(CalendarSelectionMode.Day)]
		[WebSysDescription("")]
		public CalendarSelectionMode SelectionMode
		{
			get
			{
				return (CalendarSelectionMode)this.ViewState.GetInt("SelectionMode", 1);
			}
			set
			{
				if (value != CalendarSelectionMode.Day && value != CalendarSelectionMode.DayWeek && value != CalendarSelectionMode.DayWeekMonth && value != CalendarSelectionMode.None)
				{
					throw new ArgumentOutOfRangeException("The specified selection mode is not one of the CalendarSelectionMode values.");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the month selection element in the selector column.</summary>
		/// <returns>The text displayed for the month selection element in the selector column. The default value is "&amp;gt;&amp;gt;", which is rendered as two greater than signs (&gt;&gt;).</returns>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0004831C File Offset: 0x0004651C
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x00048333 File Offset: 0x00046533
		[DefaultValue("&gt;&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string SelectMonthText
		{
			get
			{
				return this.ViewState.GetString("SelectMonthText", "&gt;&gt;");
			}
			set
			{
				this.ViewState["SelectMonthText"] = value;
			}
		}

		/// <summary>Gets the style properties for the week and month selector column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the week and month selector column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00048346 File Offset: 0x00046546
		[WebCategory("Style")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle SelectorStyle
		{
			get
			{
				if (this.selectorStyle == null)
				{
					this.selectorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectorStyle.TrackViewState();
					}
				}
				return this.selectorStyle;
			}
		}

		/// <summary>Gets or sets the text displayed for the week selection element in the selector column.</summary>
		/// <returns>The text displayed for the week selection element in the selector column. The default value is "&amp;gt;", which is rendered as a greater than sign (&gt;).</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00048374 File Offset: 0x00046574
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0004838B File Offset: 0x0004658B
		[DefaultValue("&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string SelectWeekText
		{
			get
			{
				return this.ViewState.GetString("SelectWeekText", "&gt;");
			}
			set
			{
				this.ViewState["SelectWeekText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the heading for the days of the week is displayed.</summary>
		/// <returns>true if the heading for the days of the week is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0004839E File Offset: 0x0004659E
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x000483B1 File Offset: 0x000465B1
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public bool ShowDayHeader
		{
			get
			{
				return this.ViewState.GetBool("ShowDayHeader", true);
			}
			set
			{
				this.ViewState["ShowDayHeader"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines.</summary>
		/// <returns>true if the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines; otherwise, false. The default value is false.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x000483C9 File Offset: 0x000465C9
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x000483DC File Offset: 0x000465DC
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		[WebSysDescription("")]
		public bool ShowGridLines
		{
			get
			{
				return this.ViewState.GetBool("ShowGridLines", false);
			}
			set
			{
				this.ViewState["ShowGridLines"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Calendar" /> control displays the next and previous month navigation elements in the title section.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the next and previous month navigation elements in the title section; otherwise, false. The default value is true.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000483F4 File Offset: 0x000465F4
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x00048407 File Offset: 0x00046607
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		public bool ShowNextPrevMonth
		{
			get
			{
				return this.ViewState.GetBool("ShowNextPrevMonth", true);
			}
			set
			{
				this.ViewState["ShowNextPrevMonth"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the title section is displayed.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the title section; otherwise, false. The default value is true.</returns>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0004841F File Offset: 0x0004661F
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x00048432 File Offset: 0x00046632
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public bool ShowTitle
		{
			get
			{
				return this.ViewState.GetBool("ShowTitle", true);
			}
			set
			{
				this.ViewState["ShowTitle"] = value;
			}
		}

		/// <summary>Gets or sets the format for the title section.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. The default value is MonthYear.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified title format is not one of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. </exception>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0004844A File Offset: 0x0004664A
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0004845D File Offset: 0x0004665D
		[DefaultValue(TitleFormat.MonthYear)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public TitleFormat TitleFormat
		{
			get
			{
				return (TitleFormat)this.ViewState.GetInt("TitleFormat", 1);
			}
			set
			{
				if (value != TitleFormat.Month && value != TitleFormat.MonthYear)
				{
					throw new ArgumentOutOfRangeException("The specified title format is not one of the TitleFormat values.");
				}
				this.ViewState["TitleFormat"] = value;
			}
		}

		/// <summary>Gets the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00048487 File Offset: 0x00046687
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public TableItemStyle TitleStyle
		{
			get
			{
				if (this.titleStyle == null)
				{
					this.titleStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.titleStyle.TrackViewState();
					}
				}
				return this.titleStyle;
			}
		}

		/// <summary>Gets the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000484B5 File Offset: 0x000466B5
		[WebCategory("Style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		public TableItemStyle TodayDayStyle
		{
			get
			{
				if (this.todayDayStyle == null)
				{
					this.todayDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.todayDayStyle.TrackViewState();
					}
				}
				return this.todayDayStyle;
			}
		}

		/// <summary>Gets or sets the value for today's date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the value that the <see cref="T:System.Web.UI.WebControls.Calendar" /> considers to be today's date. If this property is not explicitly set, this date will be the date on the server.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000484E4 File Offset: 0x000466E4
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x00048512 File Offset: 0x00046712
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public DateTime TodaysDate
		{
			get
			{
				object obj = this.ViewState["TodaysDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return this.today;
			}
			set
			{
				this.ViewState["TodaysDate"] = value.Date;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to render the table header &lt;th&gt; HTML element for the day headers instead of the table data &lt;td&gt; HTML element.</summary>
		/// <returns>true if the &lt;th&gt; element is used for day header cells; false if the &lt;td&gt; element is used for day headers.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00048530 File Offset: 0x00046730
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x00045204 File Offset: 0x00043404
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				return this.ViewState.GetBool("UseAccessibleHeader", true);
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.DateTime" /> value that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is <see cref="F:System.DateTime.MinValue" />, which displays the month that contains the date specified by <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" />.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00048544 File Offset: 0x00046744
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x00048571 File Offset: 0x00046771
		[Bindable(true)]
		[DefaultValue("1/1/0001 12:00:00 AM")]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public DateTime VisibleDate
		{
			get
			{
				object obj = this.ViewState["VisibleDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.MinValue;
			}
			set
			{
				this.ViewState["VisibleDate"] = value.Date;
			}
		}

		/// <summary>Gets the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0004858F File Offset: 0x0004678F
		[WebCategory("Style")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle WeekendDayStyle
		{
			get
			{
				if (this.weekendDayStyle == null)
				{
					this.weekendDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.weekendDayStyle.TrackViewState();
					}
				}
				return this.weekendDayStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000485BD File Offset: 0x000467BD
		private DateTimeFormatInfo DateInfo
		{
			get
			{
				if (this.dateInfo == null)
				{
					this.dateInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
				}
				return this.dateInfo;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000485E4 File Offset: 0x000467E4
		private DateTime DisplayDate
		{
			get
			{
				DateTime dateTime = this.VisibleDate;
				if (dateTime == DateTime.MinValue)
				{
					dateTime = this.TodaysDate;
				}
				return dateTime;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0004860D File Offset: 0x0004680D
		private DayOfWeek DisplayFirstDayOfWeek
		{
			get
			{
				if (this.FirstDayOfWeek != FirstDayOfWeek.Default)
				{
					return (DayOfWeek)this.FirstDayOfWeek;
				}
				return this.DateInfo.FirstDayOfWeek;
			}
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Always returns an InternalControlCollection object.</returns>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x0004862A File Offset: 0x0004682A
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Determines whether a <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> object contains week selectors.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> contains week selectors; otherwise, false.</returns>
		/// <param name="selectionMode">One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </param>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x00048632 File Offset: 0x00046832
		protected bool HasWeekSelectors(CalendarSelectionMode selectionMode)
		{
			return selectionMode == CalendarSelectionMode.DayWeek || selectionMode == CalendarSelectionMode.DayWeekMonth;
		}

		/// <summary>Raises events on postback for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <param name="eventArgument">The argument for the event. </param>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0004863F File Offset: 0x0004683F
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises an event for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler. </param>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x00048648 File Offset: 0x00046848
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (eventArgument.Length < 1)
			{
				return;
			}
			if (eventArgument[0] == 'V')
			{
				DateTime visibleDate = this.VisibleDate;
				int num = int.Parse(eventArgument.Substring(1));
				DateTime dateTime = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, num);
				this.VisibleDate = dateTime;
				this.OnVisibleMonthChanged(this.VisibleDate, visibleDate);
				return;
			}
			if (eventArgument[0] == 'R')
			{
				string text = eventArgument.Substring(1);
				string text2 = text.Substring(text.Length - 2, 2);
				string text3 = text.Substring(0, text.Length - 2);
				DateTime dateTime2 = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, int.Parse(text3));
				this.SelectedDates.SelectRange(dateTime2, dateTime2.AddDays((double)int.Parse(text2)));
				this.OnSelectionChanged();
				return;
			}
			int num2 = int.Parse(eventArgument);
			DateTime dateTime3 = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, num2);
			this.SelectedDates.SelectRange(dateTime3, dateTime3);
			this.OnSelectionChanged();
		}

		/// <summary>Loads a saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <param name="savedState">A <see cref="T:System.Object" /> that contains the saved condition of the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x00048758 File Offset: 0x00046958
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array[0] != null)
			{
				base.LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				this.DayHeaderStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.DayStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.NextPrevStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.OtherMonthDayStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.SelectedDayStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.TitleStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.TodayDayStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.SelectorStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				this.WeekendDayStyle.LoadViewState(array[9]);
			}
			ArrayList arrayList = (ArrayList)this.ViewState["SelectedDates"];
			if (arrayList != null)
			{
				this.dateList = arrayList;
				this.selectedDatesCollection = new SelectedDatesCollection(this.dateList);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to render. </param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that contains information about the day to render. </param>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x00048858 File Offset: 0x00046A58
		protected virtual void OnDayRender(TableCell cell, CalendarDay day)
		{
			DayRenderEventHandler dayRenderEventHandler = (DayRenderEventHandler)base.Events[Calendar.DayRenderEvent];
			if (dayRenderEventHandler != null)
			{
				Page page = this.Page;
				if (page != null)
				{
					dayRenderEventHandler(this, new DayRenderEventArgs(cell, day, page.ClientScript.GetPostBackClientHyperlink(this, this.GetDaysFromZenith(day.Date).ToString(), true)));
					return;
				}
				dayRenderEventHandler(this, new DayRenderEventArgs(cell, day));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event.</summary>
		// Token: 0x06001DEA RID: 7658 RVA: 0x000488C8 File Offset: 0x00046AC8
		protected virtual void OnSelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Calendar.SelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event.</summary>
		/// <param name="newDate">A <see cref="T:System.DateTime" /> that represents the month currently displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		/// <param name="previousDate">A <see cref="T:System.DateTime" /> that represents the previous month displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001DEB RID: 7659 RVA: 0x000488FC File Offset: 0x00046AFC
		protected virtual void OnVisibleMonthChanged(DateTime newDate, DateTime previousDate)
		{
			MonthChangedEventHandler monthChangedEventHandler = (MonthChangedEventHandler)base.Events[Calendar.VisibleMonthChangedEvent];
			if (monthChangedEventHandler != null)
			{
				monthChangedEventHandler(this, new MonthChangedEventArgs(newDate, previousDate));
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.Calendar" /> control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001DEC RID: 7660 RVA: 0x00048930 File Offset: 0x00046B30
		protected internal override void Render(HtmlTextWriter writer)
		{
			TableStyle tableStyle = new TableStyle();
			tableStyle.CellSpacing = this.CellSpacing;
			tableStyle.CellPadding = this.CellPadding;
			tableStyle.BorderWidth = 1;
			if (base.ControlStyleCreated)
			{
				tableStyle.CopyFrom(base.ControlStyle);
			}
			if (this.ShowGridLines)
			{
				tableStyle.GridLines = GridLines.Both;
			}
			tableStyle.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (!string.IsNullOrEmpty(this.Caption))
			{
				this.WriteCaption(writer);
			}
			bool isEnabled = base.IsEnabled;
			if (this.ShowTitle)
			{
				this.WriteTitle(writer, isEnabled);
			}
			if (this.ShowDayHeader)
			{
				this.WriteDayHeader(writer, isEnabled);
			}
			this.WriteDays(writer, isEnabled);
			writer.RenderEndTag();
		}

		/// <summary>Stores the state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x06001DED RID: 7661 RVA: 0x000489F0 File Offset: 0x00046BF0
		protected override object SaveViewState()
		{
			object[] array = new object[10];
			if (this.dayHeaderStyle != null)
			{
				array[1] = this.dayHeaderStyle.SaveViewState();
			}
			if (this.dayStyle != null)
			{
				array[2] = this.dayStyle.SaveViewState();
			}
			if (this.nextPrevStyle != null)
			{
				array[3] = this.nextPrevStyle.SaveViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				array[4] = this.otherMonthDayStyle.SaveViewState();
			}
			if (this.selectedDayStyle != null)
			{
				array[5] = this.selectedDayStyle.SaveViewState();
			}
			if (this.titleStyle != null)
			{
				array[6] = this.titleStyle.SaveViewState();
			}
			if (this.todayDayStyle != null)
			{
				array[7] = this.todayDayStyle.SaveViewState();
			}
			if (this.selectorStyle != null)
			{
				array[8] = this.selectorStyle.SaveViewState();
			}
			if (this.weekendDayStyle != null)
			{
				array[9] = this.weekendDayStyle.SaveViewState();
			}
			if (this.SelectedDates.Count > 0)
			{
				this.ViewState["SelectedDates"] = this.dateList;
			}
			array[0] = base.SaveViewState();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Marks the starting point to begin tracking and saving changes to the control as part of the control view state.</summary>
		// Token: 0x06001DEE RID: 7662 RVA: 0x00048B10 File Offset: 0x00046D10
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.dayHeaderStyle != null)
			{
				this.dayHeaderStyle.TrackViewState();
			}
			if (this.dayStyle != null)
			{
				this.dayStyle.TrackViewState();
			}
			if (this.nextPrevStyle != null)
			{
				this.nextPrevStyle.TrackViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				this.otherMonthDayStyle.TrackViewState();
			}
			if (this.selectedDayStyle != null)
			{
				this.selectedDayStyle.TrackViewState();
			}
			if (this.titleStyle != null)
			{
				this.titleStyle.TrackViewState();
			}
			if (this.todayDayStyle != null)
			{
				this.todayDayStyle.TrackViewState();
			}
			if (this.selectorStyle != null)
			{
				this.selectorStyle.TrackViewState();
			}
			if (this.weekendDayStyle != null)
			{
				this.weekendDayStyle.TrackViewState();
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00048BD0 File Offset: 0x00046DD0
		private void WriteDayHeader(HtmlTextWriter writer, bool enabled)
		{
			int num2;
			int num = (num2 = (int)this.DisplayFirstDayOfWeek);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (this.SelectionMode == CalendarSelectionMode.DayWeek)
			{
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ApplyStyle(this.DayHeaderStyle);
				tableCell.RenderBeginTag(writer);
				tableCell.RenderEndTag(writer);
			}
			else if (this.SelectionMode == CalendarSelectionMode.DayWeekMonth)
			{
				TableCell tableCell2 = new TableCell();
				tableCell2.ApplyStyle(this.SelectorStyle);
				tableCell2.HorizontalAlign = HorizontalAlign.Center;
				DateTime dateTime = new DateTime(this.DisplayDate.Year, this.DisplayDate.Month, 1);
				int num3 = DateTime.DaysInMonth(this.DisplayDate.Year, this.DisplayDate.Month);
				tableCell2.RenderBeginTag(writer);
				writer.Write(this.BuildLink("R" + this.GetDaysFromZenith(dateTime).ToString() + num3.ToString(), this.SelectMonthText, this.DayHeaderStyle.ForeColor, enabled));
				tableCell2.RenderEndTag(writer);
			}
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			for (;;)
			{
				DayOfWeek dayOfWeek = (DayOfWeek)num2;
				string text = dateTimeFormatInfo.GetDayName(dayOfWeek);
				TableCell tableCell;
				if (this.UseAccessibleHeader)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Abbr, text);
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", false);
					tableCell = new TableHeaderCell();
				}
				else
				{
					tableCell = new TableCell();
				}
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ApplyStyle(this.DayHeaderStyle);
				tableCell.RenderBeginTag(writer);
				switch (this.DayNameFormat)
				{
				case DayNameFormat.Full:
					break;
				case DayNameFormat.Short:
					goto IL_01A5;
				case DayNameFormat.FirstLetter:
					text = text.Substring(0, 1);
					break;
				case DayNameFormat.FirstTwoLetters:
					text = text.Substring(0, 2);
					break;
				case DayNameFormat.Shortest:
					text = dateTimeFormatInfo.GetShortestDayName(dayOfWeek);
					break;
				default:
					goto IL_01A5;
				}
				IL_01AF:
				writer.Write(text);
				tableCell.RenderEndTag(writer);
				if (num2 >= 6)
				{
					num2 = 0;
				}
				else
				{
					num2++;
				}
				if (num2 == num)
				{
					break;
				}
				continue;
				IL_01A5:
				text = dateTimeFormatInfo.GetAbbreviatedDayName(dayOfWeek);
				goto IL_01AF;
			}
			writer.RenderEndTag();
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00048DB4 File Offset: 0x00046FB4
		private void WriteDay(DateTime date, HtmlTextWriter writer, bool enabled)
		{
			TableItemStyle tableItemStyle = new TableItemStyle();
			TableCell tableCell = new TableCell();
			CalendarDay calendarDay = new CalendarDay(date, this.IsWeekEnd(date.DayOfWeek), date == this.TodaysDate, this.SelectedDates.Contains(date), this.GetGlobalCalendar().GetMonth(this.DisplayDate) != this.GetGlobalCalendar().GetMonth(date), date.Day.ToString());
			calendarDay.IsSelectable = this.SelectionMode > CalendarSelectionMode.None;
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.Width = Unit.Percentage(this.GetCellWidth());
			LiteralControl literalControl = new LiteralControl(calendarDay.DayNumberText);
			tableCell.Controls.Add(literalControl);
			this.OnDayRender(tableCell, calendarDay);
			if (this.dayStyle != null && !this.dayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.dayStyle);
			}
			if (calendarDay.IsWeekend && this.weekendDayStyle != null && !this.weekendDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.weekendDayStyle);
			}
			if (calendarDay.IsToday && this.todayDayStyle != null && !this.todayDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.todayDayStyle);
			}
			if (calendarDay.IsOtherMonth && this.otherMonthDayStyle != null && !this.otherMonthDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.otherMonthDayStyle);
			}
			if (enabled && calendarDay.IsSelected)
			{
				tableItemStyle.BackColor = Color.Silver;
				tableItemStyle.ForeColor = Color.White;
				if (this.selectedDayStyle != null && !this.selectedDayStyle.IsEmpty)
				{
					tableItemStyle.CopyFrom(this.selectedDayStyle);
				}
			}
			tableCell.ApplyStyle(tableItemStyle);
			literalControl.Text = this.BuildLink(this.GetDaysFromZenith(date).ToString(), calendarDay.DayNumberText, tableCell.ForeColor, enabled && calendarDay.IsSelectable);
			tableCell.RenderControl(writer);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00048F94 File Offset: 0x00047194
		private void WriteDays(HtmlTextWriter writer, bool enabled)
		{
			DateTime dateTime = new DateTime(this.DisplayDate.Year, this.DisplayDate.Month, 1);
			TableCell tableCell = null;
			int num = 0;
			while (num < 7 && dateTime.DayOfWeek != this.DisplayFirstDayOfWeek)
			{
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -1);
				num++;
			}
			if (num == 0)
			{
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -7);
			}
			DateTime dateTime2 = this.GetGlobalCalendar().AddDays(dateTime, 42);
			do
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (this.HasWeekSelectors(this.SelectionMode))
				{
					if (tableCell == null)
					{
						tableCell = new TableCell();
						tableCell.ApplyStyle(this.SelectorStyle);
						tableCell.HorizontalAlign = HorizontalAlign.Center;
						tableCell.Width = Unit.Percentage(this.GetCellWidth());
					}
					tableCell.RenderBeginTag(writer);
					writer.Write(this.BuildLink("R" + this.GetDaysFromZenith(dateTime).ToString() + "07", this.SelectWeekText, tableCell.ForeColor, enabled));
					tableCell.RenderEndTag(writer);
				}
				for (int i = 0; i < 7; i++)
				{
					this.WriteDay(dateTime, writer, enabled);
					dateTime = this.GetGlobalCalendar().AddDays(dateTime, 1);
				}
				writer.RenderEndTag();
			}
			while (!(dateTime >= dateTime2));
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000490D8 File Offset: 0x000472D8
		private string BuildLink(string arg, string text, Color foreColor, bool hasLink)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Page page = this.Page;
			hasLink = page != null && hasLink;
			if (hasLink)
			{
				stringBuilder.Append("<a href=\"");
				stringBuilder.Append(page.ClientScript.GetPostBackClientHyperlink(this, arg, true));
				stringBuilder.Append('"');
				Color color;
				if (!foreColor.IsEmpty)
				{
					color = foreColor;
				}
				else if (this.ForeColor.IsEmpty)
				{
					color = Color.Black;
				}
				else
				{
					color = this.ForeColor;
				}
				stringBuilder.Append(" style=\"color:" + ColorTranslator.ToHtml(color));
				stringBuilder.Append("\">");
				stringBuilder.Append(text);
				stringBuilder.Append("</a>");
			}
			else
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000491A4 File Offset: 0x000473A4
		private int GetDaysFromZenith(DateTime date)
		{
			return date.Subtract(Calendar.dateZenith).Days;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000491C8 File Offset: 0x000473C8
		private void WriteCaption(HtmlTextWriter writer)
		{
			if (this.CaptionAlign != TableCaptionAlign.NotSet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, this.CaptionAlign.ToString(Helpers.InvariantCulture));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(this.Caption);
			writer.RenderEndTag();
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00049218 File Offset: 0x00047418
		private void WriteTitle(HtmlTextWriter writer, bool enabled)
		{
			TableCell tableCell = null;
			TableCell tableCell2 = new TableCell();
			Table table = new Table();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			tableCell2.ColumnSpan = (this.HasWeekSelectors(this.SelectionMode) ? 8 : 7);
			if (this.titleStyle != null && !this.titleStyle.IsEmpty && !this.titleStyle.BackColor.IsEmpty)
			{
				tableCell2.BackColor = this.titleStyle.BackColor;
			}
			else
			{
				tableCell2.BackColor = Color.Silver;
			}
			tableCell2.RenderBeginTag(writer);
			table.Width = Unit.Percentage(100.0);
			if (this.titleStyle != null && !this.titleStyle.IsEmpty)
			{
				table.ApplyStyle(this.titleStyle);
			}
			table.RenderBeginTag(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (this.ShowNextPrevMonth)
			{
				tableCell = new TableCell();
				tableCell.ApplyStyle(this.nextPrevStyle);
				tableCell.Width = Unit.Percentage(15.0);
				DateTime dateTime = this.GetGlobalCalendar().AddMonths(this.DisplayDate, -1);
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -dateTime.Day + 1);
				tableCell.RenderBeginTag(writer);
				writer.Write(this.BuildLink("V" + this.GetDaysFromZenith(dateTime).ToString(), this.GetNextPrevFormatText(dateTime, false), tableCell.ForeColor, enabled));
				tableCell.RenderEndTag(writer);
			}
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			TableCell tableCell3 = new TableCell();
			tableCell3.Width = Unit.Percentage(70.0);
			tableCell3.HorizontalAlign = HorizontalAlign.Center;
			tableCell3.RenderBeginTag(writer);
			string text;
			if (this.TitleFormat == TitleFormat.MonthYear)
			{
				text = this.DisplayDate.ToString(dateTimeFormatInfo.YearMonthPattern, dateTimeFormatInfo);
			}
			else
			{
				text = dateTimeFormatInfo.GetMonthName(this.GetGlobalCalendar().GetMonth(this.DisplayDate));
			}
			writer.Write(text);
			tableCell3.RenderEndTag(writer);
			if (this.ShowNextPrevMonth)
			{
				DateTime dateTime2 = this.GetGlobalCalendar().AddMonths(this.DisplayDate, 1);
				dateTime2 = this.GetGlobalCalendar().AddDays(dateTime2, -dateTime2.Day + 1);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.RenderBeginTag(writer);
				writer.Write(this.BuildLink("V" + this.GetDaysFromZenith(dateTime2).ToString(), this.GetNextPrevFormatText(dateTime2, true), tableCell.ForeColor, enabled));
				tableCell.RenderEndTag(writer);
			}
			writer.RenderEndTag();
			table.RenderEndTag(writer);
			tableCell2.RenderEndTag(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000494A0 File Offset: 0x000476A0
		private string GetNextPrevFormatText(DateTime date, bool next)
		{
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			switch (this.NextPrevFormat)
			{
			case NextPrevFormat.ShortMonth:
				return dateTimeFormatInfo.GetAbbreviatedMonthName(this.GetGlobalCalendar().GetMonth(date));
			case NextPrevFormat.FullMonth:
				return dateTimeFormatInfo.GetMonthName(this.GetGlobalCalendar().GetMonth(date));
			}
			return next ? this.NextMonthText : this.PrevMonthText;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0004950C File Offset: 0x0004770C
		private bool IsWeekEnd(DayOfWeek day)
		{
			return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00049518 File Offset: 0x00047718
		private double GetCellWidth()
		{
			return (double)(this.HasWeekSelectors(this.SelectionMode) ? 12 : 14);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0004952F File Offset: 0x0004772F
		private Calendar GetGlobalCalendar()
		{
			return DateTimeFormatInfo.CurrentInfo.Calendar;
		}

		/// <summary>Occurs when each day is created in the control hierarchy for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001DFA RID: 7674 RVA: 0x0004953B File Offset: 0x0004773B
		// (remove) Token: 0x06001DFB RID: 7675 RVA: 0x0004954E File Offset: 0x0004774E
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DayRenderEventHandler DayRender
		{
			add
			{
				base.Events.AddHandler(Calendar.DayRenderEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.DayRenderEvent, value);
			}
		}

		/// <summary>Occurs when the user selects a day, a week, or an entire month by clicking the date selector controls.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001DFC RID: 7676 RVA: 0x00049561 File Offset: 0x00047761
		// (remove) Token: 0x06001DFD RID: 7677 RVA: 0x00049574 File Offset: 0x00047774
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.SelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.SelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the next or previous month navigation controls on the title heading.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001DFE RID: 7678 RVA: 0x00049587 File Offset: 0x00047787
		// (remove) Token: 0x06001DFF RID: 7679 RVA: 0x0004959A File Offset: 0x0004779A
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event MonthChangedEventHandler VisibleMonthChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.VisibleMonthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.VisibleMonthChangedEvent, value);
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000495AD File Offset: 0x000477AD
		// Note: this type is marked as 'beforefieldinit'.
		static Calendar()
		{
			Calendar.DayRenderEvent = new object();
			Calendar.SelectionChangedEvent = new object();
			Calendar.VisibleMonthChangedEvent = new object();
		}

		// Token: 0x04001834 RID: 6196
		private TableItemStyle dayHeaderStyle;

		// Token: 0x04001835 RID: 6197
		private TableItemStyle dayStyle;

		// Token: 0x04001836 RID: 6198
		private TableItemStyle nextPrevStyle;

		// Token: 0x04001837 RID: 6199
		private TableItemStyle otherMonthDayStyle;

		// Token: 0x04001838 RID: 6200
		private TableItemStyle selectedDayStyle;

		// Token: 0x04001839 RID: 6201
		private TableItemStyle titleStyle;

		// Token: 0x0400183A RID: 6202
		private TableItemStyle todayDayStyle;

		// Token: 0x0400183B RID: 6203
		private TableItemStyle selectorStyle;

		// Token: 0x0400183C RID: 6204
		private TableItemStyle weekendDayStyle;

		// Token: 0x0400183D RID: 6205
		private DateTimeFormatInfo dateInfo;

		// Token: 0x0400183E RID: 6206
		private SelectedDatesCollection selectedDatesCollection;

		// Token: 0x0400183F RID: 6207
		private ArrayList dateList;

		// Token: 0x04001840 RID: 6208
		private DateTime today = DateTime.Today;

		// Token: 0x04001841 RID: 6209
		private static DateTime dateZenith = new DateTime(2000, 1, 1);

		// Token: 0x04001842 RID: 6210
		private const int daysInAWeek = 7;
	}
}
