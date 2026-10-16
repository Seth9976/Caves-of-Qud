using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality common to all hot spot shapes.</summary>
	// Token: 0x020003AA RID: 938
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HotSpot : IStateManager
	{
		/// <summary>Gets or sets the access key that allows you to quickly navigate to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>The access key for quick navigation to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither null, an empty string (""), nor a single character string. </exception>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0006222C File Offset: 0x0006042C
		// (set) Token: 0x06002640 RID: 9792 RVA: 0x00062259 File Offset: 0x00060459
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string AccessKey
		{
			get
			{
				object obj = this.viewState["AccessKey"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (value == null || value.Length < 2)
				{
					this.viewState["AccessKey"] = value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", "AccessKey can only be null, empty or a single character");
			}
		}

		/// <summary>Gets or sets the alternate text to display for a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the image is unavailable or renders to a browser that does not support images.</summary>
		/// <returns>The text displayed in place of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> when the <see cref="T:System.Web.UI.WebControls.ImageMap" /> control's image is unavailable. The default value is an empty string ("").</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00062288 File Offset: 0x00060488
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x000622B5 File Offset: 0x000604B5
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Behavior")]
		public virtual string AlternateText
		{
			get
			{
				object obj = this.viewState["AlternateText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["AlternateText"] = value;
			}
		}

		/// <summary>Gets or sets the behavior of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. The default is Default.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.HotSpotMode" /> enumeration values. </exception>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000622C8 File Offset: 0x000604C8
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x000622F1 File Offset: 0x000604F1
		[NotifyParentProperty(true)]
		[DefaultValue(HotSpotMode.NotSet)]
		[WebCategory("Behavior")]
		public virtual HotSpotMode HotSpotMode
		{
			get
			{
				object obj = this.viewState["HotSpotMode"];
				if (obj == null)
				{
					return HotSpotMode.NotSet;
				}
				return (HotSpotMode)obj;
			}
			set
			{
				if (value < HotSpotMode.NotSet || value > HotSpotMode.Inactive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.viewState["HotSpotMode"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked.</summary>
		/// <returns>The URL to navigate to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x0006231C File Offset: 0x0006051C
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x00062349 File Offset: 0x00060549
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				object obj = this.viewState["NavigateUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked.</summary>
		/// <returns>The name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to pass in the event data when the <see cref="T:System.Web.UI.WebControls.HotSpot" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x0006235C File Offset: 0x0006055C
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x00062389 File Offset: 0x00060589
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string PostBackValue
		{
			get
			{
				object obj = this.viewState["PostBackValue"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["PostBackValue"] = value;
			}
		}

		/// <summary>Gets or sets the tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>The tab index of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region. The default is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x0006239C File Offset: 0x0006059C
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x000623C5 File Offset: 0x000605C5
		[WebCategory("Accessibility")]
		[DefaultValue(0)]
		public virtual short TabIndex
		{
			get
			{
				object obj = this.viewState["TabIndex"];
				if (obj == null)
				{
					return 0;
				}
				return (short)obj;
			}
			set
			{
				this.viewState["TabIndex"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object that navigates to a URL is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000623E0 File Offset: 0x000605E0
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x0006240D File Offset: 0x0006060D
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(TargetConverter))]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string Target
		{
			get
			{
				object obj = this.viewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["Target"] = value;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object across multiple requests for the same page.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region's view-state information.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00062420 File Offset: 0x00060620
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				return this.viewState;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
		// Token: 0x0600264E RID: 9806 RVA: 0x00062428 File Offset: 0x00060628
		protected virtual void LoadViewState(object savedState)
		{
			this.viewState.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state. If no view state is associated with the object, this method returns null.</returns>
		// Token: 0x0600264F RID: 9807 RVA: 0x00062436 File Offset: 0x00060636
		protected virtual object SaveViewState()
		{
			return this.viewState.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to track changes to its view state so they can be stored in the object's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x06002650 RID: 9808 RVA: 0x00062443 File Offset: 0x00060643
		protected virtual void TrackViewState()
		{
			this.viewState.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00062450 File Offset: 0x00060650
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this.viewState.IsTrackingViewState;
			}
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's previously saved view state to the object.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object to restore. </param>
		// Token: 0x06002652 RID: 9810 RVA: 0x0006245D File Offset: 0x0006065D
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state since the last time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's view state.</returns>
		// Token: 0x06002653 RID: 9811 RVA: 0x00062466 File Offset: 0x00060666
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region to track changes to its view state.</summary>
		// Token: 0x06002654 RID: 9812 RVA: 0x0006246E File Offset: 0x0006066E
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes.  </summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00062476 File Offset: 0x00060676
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Returns the <see cref="T:System.String" /> representation of this instance of a <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.HotSpot" /> object.</returns>
		// Token: 0x06002656 RID: 9814 RVA: 0x0006247E File Offset: 0x0006067E
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0006248B File Offset: 0x0006068B
		internal void SetDirty()
		{
			this.viewState.SetDirty(true);
		}

		/// <summary>When overridden in a derived class, returns a string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</summary>
		/// <returns>A string that represents the coordinates of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> region.</returns>
		// Token: 0x06002658 RID: 9816
		public abstract string GetCoordinates();

		/// <summary>When overridden in a derived class, gets the string representation for the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</summary>
		/// <returns>A string that represents the name of the <see cref="T:System.Web.UI.WebControls.HotSpot" /> object's shape.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002659 RID: 9817
		protected internal abstract string MarkupName { get; }

		// Token: 0x04001A47 RID: 6727
		private StateBag viewState = new StateBag();
	}
}
