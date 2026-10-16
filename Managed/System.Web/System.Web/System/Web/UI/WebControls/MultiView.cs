using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for a group of <see cref="T:System.Web.UI.WebControls.View" /> controls.</summary>
	// Token: 0x020003D6 RID: 982
	[ToolboxData("<{0}:MultiView runat=\"server\"></{0}:MultiView>")]
	[DefaultEvent("ActiveViewChanged")]
	[Designer("System.Web.UI.Design.WebControls.MultiViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(typeof(View))]
	[ControlBuilder(typeof(MultiViewControlBuilder))]
	public class MultiView : Control
	{
		/// <summary>Occurs when the active <see cref="T:System.Web.UI.WebControls.View" /> control of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control changes between posts to the server.</summary>
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06002A41 RID: 10817 RVA: 0x0006E21C File Offset: 0x0006C41C
		// (remove) Token: 0x06002A42 RID: 10818 RVA: 0x0006E22F File Offset: 0x0006C42F
		public event EventHandler ActiveViewChanged
		{
			add
			{
				base.Events.AddHandler(MultiView.ActiveViewChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MultiView.ActiveViewChangedEvent, value);
			}
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.MultiView" /> control that an XML or HTML element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> collection of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">The specified <see cref="T:System.Object" /> is not a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
		// Token: 0x06002A43 RID: 10819 RVA: 0x0006E242 File Offset: 0x0006C442
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is View)
			{
				this.Controls.Add(obj as View);
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.ControlCollection" /> to hold the child controls of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> to contain the <see cref="T:System.Web.UI.WebControls.View" /> controls of the current <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
		// Token: 0x06002A44 RID: 10820 RVA: 0x0006E25D File Offset: 0x0006C45D
		protected override ControlCollection CreateControlCollection()
		{
			return new ViewCollection(this);
		}

		/// <summary>Returns the current active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.View" /> control that represents the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</returns>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Web.UI.WebControls.MultiView.ActiveViewIndex" /> property is not set to a valid <see cref="T:System.Web.UI.WebControls.View" /> control within the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
		// Token: 0x06002A45 RID: 10821 RVA: 0x0006E265 File Offset: 0x0006C465
		public View GetActiveView()
		{
			if (this.viewIndex < 0 || this.viewIndex >= this.Controls.Count)
			{
				throw new HttpException("The ActiveViewIndex is not set to a valid View control");
			}
			return this.Controls[this.viewIndex] as View;
		}

		/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="view">A <see cref="T:System.Web.UI.WebControls.View" /> control to set as the active view within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
		/// <exception cref="T:System.Web.HttpException">The specified <paramref name="view" /> parameter value was not contained in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </exception>
		// Token: 0x06002A46 RID: 10822 RVA: 0x0006E2A4 File Offset: 0x0006C4A4
		public void SetActiveView(View view)
		{
			int num = this.Controls.IndexOf(view);
			if (num == -1)
			{
				throw new HttpException("The provided view is not contained in the MultiView control.");
			}
			this.ActiveViewIndex = num;
		}

		/// <summary>Gets or sets the index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>The zero-based index of the active <see cref="T:System.Web.UI.WebControls.View" /> control within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is -1, indicating that no view is set as active.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was set to less than -1, or greater than or equal to the number of items on the list. </exception>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x0006E2F0 File Offset: 0x0006C4F0
		[DefaultValue(-1)]
		public virtual int ActiveViewIndex
		{
			get
			{
				if (this.Controls.Count == 0)
				{
					return this.initialIndex;
				}
				return this.viewIndex;
			}
			set
			{
				if (this.Controls.Count == 0)
				{
					this.initialIndex = value;
					return;
				}
				if (value < -1 || value >= this.Controls.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.viewIndex != -1)
				{
					((View)this.Controls[this.viewIndex]).NotifyActivation(false);
				}
				this.viewIndex = value;
				if (this.viewIndex != -1)
				{
					((View)this.Controls[this.viewIndex]).NotifyActivation(true);
				}
				this.UpdateViewVisibility();
				this.OnActiveViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>true if themes are to be used; otherwise, false. The default is false.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x0006E38C File Offset: 0x0006C58C
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x0006E394 File Offset: 0x0006C594
		[Browsable(true)]
		public new virtual bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.UI.WebControls.View" /> controls in the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ViewCollection" /> that represents a collection of <see cref="T:System.Web.UI.WebControls.View" /> controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. The default is null.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0006E39D File Offset: 0x0006C59D
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public virtual ViewCollection Views
		{
			get
			{
				return this.Controls as ViewCollection;
			}
		}

		/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.MultiView" /> control is passed to the page's UI server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.MultiView" /> control cannot find the <see cref="T:System.Web.UI.WebControls.View" /> specified in the <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" />.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.CommandEventArgs.CommandArgument" /> property of <paramref name="e" /> cannot be parsed as an integer.</exception>
		// Token: 0x06002A4C RID: 10828 RVA: 0x0006E3AC File Offset: 0x0006C5AC
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 != null)
			{
				string commandName = e2.CommandName;
				if (!(commandName == "NextView"))
				{
					if (!(commandName == "PrevView"))
					{
						if (!(commandName == "SwitchViewByID"))
						{
							if (!(commandName == "SwitchViewByIndex"))
							{
								return false;
							}
						}
						else
						{
							using (IEnumerator enumerator = this.Controls.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									View view = (View)obj;
									if (view.ID == (string)e2.CommandArgument)
									{
										this.SetActiveView(view);
										break;
									}
								}
								return false;
							}
						}
						int num = (int)Convert.ChangeType(e2.CommandArgument, typeof(int));
						this.ActiveViewIndex = num;
					}
					else if (this.viewIndex > 0)
					{
						this.ActiveViewIndex = this.viewIndex - 1;
					}
				}
				else if (this.viewIndex < this.Controls.Count - 1 && this.Controls.Count > 0)
				{
					this.ActiveViewIndex = this.viewIndex + 1;
				}
			}
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002A4D RID: 10829 RVA: 0x0006E4F0 File Offset: 0x0006C6F0
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			if (this.initialIndex != -1)
			{
				this.ActiveViewIndex = this.initialIndex;
				this.initialIndex = -1;
			}
			base.OnInit(e);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x0006E524 File Offset: 0x0006C724
		private void UpdateViewVisibility()
		{
			for (int i = 0; i < this.Views.Count; i++)
			{
				this.Views[i].VisibleInternal = i == this.viewIndex;
			}
		}

		/// <summary>Called after a <see cref="T:System.Web.UI.WebControls.View" /> control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="ctl">The <see cref="T:System.Web.UI.WebControls.View" /> control that has been removed. </param>
		// Token: 0x06002A4F RID: 10831 RVA: 0x0006E561 File Offset: 0x0006C761
		protected internal override void RemovedControl(Control ctl)
		{
			if (this.viewIndex >= this.Controls.Count)
			{
				this.viewIndex = this.Controls.Count - 1;
				this.UpdateViewVisibility();
			}
			base.RemovedControl(ctl);
		}

		/// <summary>Loads the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. </param>
		// Token: 0x06002A50 RID: 10832 RVA: 0x0006E596 File Offset: 0x0006C796
		protected internal override void LoadControlState(object state)
		{
			if (state != null)
			{
				this.viewIndex = (int)state;
				this.UpdateViewVisibility();
				return;
			}
			this.viewIndex = -1;
		}

		/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.MultiView" /> control. If there is no state associated with the <see cref="T:System.Web.UI.WebControls.MultiView" /> control, this method returns null.</returns>
		// Token: 0x06002A51 RID: 10833 RVA: 0x0006E5B5 File Offset: 0x0006C7B5
		protected internal override object SaveControlState()
		{
			if (this.viewIndex != -1)
			{
				return this.viewIndex;
			}
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.MultiView.ActiveViewChanged" /> event of a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002A52 RID: 10834 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		protected virtual void OnActiveViewChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[MultiView.ActiveViewChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.MultiView" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002A53 RID: 10835 RVA: 0x0006E606 File Offset: 0x0006C806
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count == 0 && this.initialIndex != -1)
			{
				this.viewIndex = this.initialIndex;
			}
			if (this.viewIndex != -1)
			{
				this.GetActiveView().Render(writer);
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x0006E655 File Offset: 0x0006C855
		// Note: this type is marked as 'beforefieldinit'.
		static MultiView()
		{
			MultiView.ActiveViewChangedEvent = new object();
		}

		/// <summary>Represents the command name associated with the next <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
		// Token: 0x04001AD6 RID: 6870
		public static readonly string NextViewCommandName = "NextView";

		/// <summary>Represents the command name associated with the previous <see cref="T:System.Web.UI.WebControls.View" /> control to display in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control. This field is read-only.</summary>
		// Token: 0x04001AD7 RID: 6871
		public static readonly string PreviousViewCommandName = "PrevView";

		/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control, based on a specified <see cref="T:System.Web.UI.WebControls.View" /> id. This field is read-only.</summary>
		// Token: 0x04001AD8 RID: 6872
		public static readonly string SwitchViewByIDCommandName = "SwitchViewByID";

		/// <summary>Represents the command name associated with changing the active <see cref="T:System.Web.UI.WebControls.View" /> control in a <see cref="T:System.Web.UI.WebControls.MultiView" /> control based on a specified <see cref="T:System.Web.UI.WebControls.View" /> index. This field is read-only.</summary>
		// Token: 0x04001AD9 RID: 6873
		public static readonly string SwitchViewByIndexCommandName = "SwitchViewByIndex";

		// Token: 0x04001ADB RID: 6875
		private int viewIndex = -1;

		// Token: 0x04001ADC RID: 6876
		private int initialIndex = -1;
	}
}
