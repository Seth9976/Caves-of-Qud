using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for a group of controls within a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	// Token: 0x02000437 RID: 1079
	[ParseChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.ViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxData("<{0}:View runat=\"server\"></{0}:View>")]
	public class View : Control
	{
		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x0006E38C File Offset: 0x0006C58C
		// (set) Token: 0x060031B6 RID: 12726 RVA: 0x0006E394 File Offset: 0x0006C594
		[Browsable(true)]
		public override bool EnableTheming
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.View" /> class. </summary>
		// Token: 0x060031B7 RID: 12727 RVA: 0x00082F79 File Offset: 0x00081179
		public View()
		{
			base.Visible = false;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00082F88 File Offset: 0x00081188
		internal void NotifyActivation(bool activated)
		{
			if (activated)
			{
				this.OnActivate(EventArgs.Empty);
				return;
			}
			this.OnDeactivate(EventArgs.Empty);
		}

		/// <summary>Occurs when the current <see cref="T:System.Web.UI.WebControls.View" /> control becomes the active view.</summary>
		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x060031B9 RID: 12729 RVA: 0x00082FA4 File Offset: 0x000811A4
		// (remove) Token: 0x060031BA RID: 12730 RVA: 0x00082FB7 File Offset: 0x000811B7
		public event EventHandler Activate
		{
			add
			{
				base.Events.AddHandler(View.ActivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(View.ActivateEvent, value);
			}
		}

		/// <summary>Occurs when the current active <see cref="T:System.Web.UI.WebControls.View" /> control becomes inactive.</summary>
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x060031BB RID: 12731 RVA: 0x00082FCA File Offset: 0x000811CA
		// (remove) Token: 0x060031BC RID: 12732 RVA: 0x00082FDD File Offset: 0x000811DD
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(View.DeactivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(View.DeactivateEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Activate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060031BD RID: 12733 RVA: 0x00082FF0 File Offset: 0x000811F0
		protected virtual void OnActivate(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[View.ActivateEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.View.Deactivate" /> event of the <see cref="T:System.Web.UI.WebControls.View" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060031BE RID: 12734 RVA: 0x00083028 File Offset: 0x00081228
		protected virtual void OnDeactivate(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[View.DeactivateEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x00034DBA File Offset: 0x00032FBA
		// (set) Token: 0x060031C0 RID: 12736 RVA: 0x00034DC2 File Offset: 0x00032FC2
		internal bool VisibleInternal
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.View" /> control is visible. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.View" /> control is visible; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set this property at run time.</exception>
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x00034DBA File Offset: 0x00032FBA
		// (set) Token: 0x060031C2 RID: 12738 RVA: 0x0008305E File Offset: 0x0008125E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new InvalidOperationException("The Visible property of a View control can only be set by setting the active View of a MultiView.");
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x0008306A File Offset: 0x0008126A
		// Note: this type is marked as 'beforefieldinit'.
		static View()
		{
			View.ActivateEvent = new object();
			View.DeactivateEvent = new object();
		}
	}
}
