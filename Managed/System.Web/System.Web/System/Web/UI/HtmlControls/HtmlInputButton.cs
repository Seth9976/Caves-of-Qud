using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= button&gt;, &lt;input type= submit&gt;, and &lt;input type= reset&gt; elements on the server.</summary>
	// Token: 0x0200025C RID: 604
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputButton : HtmlInputControl, IPostBackEventHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using default values.</summary>
		// Token: 0x0600188B RID: 6283 RVA: 0x00040277 File Offset: 0x0003E477
		public HtmlInputButton()
			: this("button")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> class using the specified button type.</summary>
		/// <param name="type">The input button type. </param>
		// Token: 0x0600188C RID: 6284 RVA: 0x00040284 File Offset: 0x0003E484
		public HtmlInputButton(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00040290 File Offset: 0x0003E490
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x000402B9 File Offset: 0x0003E4B9
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool CausesValidation
		{
			get
			{
				string text = base.Attributes["CausesValidation"];
				return text == null || bool.Parse(text);
			}
			set
			{
				base.Attributes["CausesValidation"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x000402D4 File Offset: 0x0003E4D4
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x000402FC File Offset: 0x0003E4FC
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = base.Attributes["ValidationGroup"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("ValidationGroup");
					return;
				}
				base.Attributes["ValidationGroup"] = value;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00040324 File Offset: 0x0003E524
		private void RaisePostBackEventInternal(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			if (string.Compare(base.Type, "reset", true, Helpers.InvariantCulture) != 0)
			{
				this.OnServerClick(EventArgs.Empty);
				return;
			}
			this.ResetForm(this.FindForm());
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00040388 File Offset: 0x0003E588
		private HtmlForm FindForm()
		{
			Page page = this.Page;
			if (page != null)
			{
				return page.Form;
			}
			return null;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000403A7 File Offset: 0x0003E5A7
		private void ResetForm(HtmlForm form)
		{
			if (form == null || !form.HasControls())
			{
				return;
			}
			this.ResetChildrenValues(form.Controls);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000403C4 File Offset: 0x0003E5C4
		private void ResetChildrenValues(ControlCollection children)
		{
			foreach (object obj in children)
			{
				Control control = (Control)obj;
				if (control != null)
				{
					if (control.HasControls())
					{
						this.ResetChildrenValues(control.Controls);
					}
					this.ResetChildValue(control);
				}
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00040430 File Offset: 0x0003E630
		private void ResetChildValue(Control child)
		{
			Type type = child.GetType();
			object[] array = type.GetCustomAttributes(false);
			if (array == null || array.Length == 0)
			{
				return;
			}
			string text = null;
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DefaultPropertyAttribute defaultPropertyAttribute = array2[i] as DefaultPropertyAttribute;
				if (defaultPropertyAttribute != null)
				{
					text = defaultPropertyAttribute.Name;
					break;
				}
			}
			if (text == null || text.Length == 0)
			{
				return;
			}
			PropertyInfo propertyInfo = null;
			try
			{
				propertyInfo = type.GetProperty(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			}
			catch (Exception)
			{
			}
			if (propertyInfo == null || !propertyInfo.CanWrite)
			{
				return;
			}
			array = propertyInfo.GetCustomAttributes(false);
			if (array == null || array.Length == 0)
			{
				return;
			}
			object obj = null;
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DefaultValueAttribute defaultValueAttribute = array2[i] as DefaultValueAttribute;
				if (defaultValueAttribute != null)
				{
					obj = defaultValueAttribute.Value;
					break;
				}
			}
			if (obj == null || propertyInfo.PropertyType != obj.GetType())
			{
				return;
			}
			try
			{
				propertyInfo.SetValue(child, obj, null);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001896 RID: 6294 RVA: 0x00040548 File Offset: 0x0003E748
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEventInternal(eventArgument);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputButton.RaisePostBackEvent(System.String)" /> method.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents an optional event argument to be passed to the event handler.</param>
		// Token: 0x06001897 RID: 6295 RVA: 0x00040551 File Offset: 0x0003E751
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers client script for generating postback.</summary>
		/// <param name="e">An <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001898 RID: 6296 RVA: 0x0004055A File Offset: 0x0003E75A
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (base.Events[HtmlInputButton.ServerClickEvent] != null)
			{
				this.Page.RequiresPostBackScript();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputButton.ServerClick" /> event. This allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001899 RID: 6297 RVA: 0x00040580 File Offset: 0x0003E780
		protected virtual void OnServerClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputButton.ServerClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the attributes into the specified writer and then calls the <see cref="M:System.Web.UI.HtmlControls.HtmlControl.RenderAttributes(System.Web.UI.HtmlTextWriter)" /> method.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x0600189A RID: 6298 RVA: 0x000405B0 File Offset: 0x0003E7B0
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			CultureInfo invariantCulture = Helpers.InvariantCulture;
			string type = base.Type;
			if (string.Compare(type, "reset", true, invariantCulture) != 0 && (string.Compare(type, "submit", true, invariantCulture) == 0 || (string.Compare(type, "button", true, invariantCulture) == 0 && base.Events[HtmlInputButton.ServerClickEvent] != null)))
			{
				string text = string.Empty;
				if (base.Attributes["onclick"] != null)
				{
					text = ClientScriptManager.EnsureEndsWithSemicolon(base.Attributes["onclick"] + text);
					base.Attributes.Remove("onclick");
				}
				Page page = this.Page;
				if (page != null)
				{
					PostBackOptions postBackOptions = this.GetPostBackOptions();
					text += page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				}
				if (text.Length > 0)
				{
					bool flag = true;
					if (base.Events[HtmlInputButton.ServerClickEvent] != null)
					{
						flag = false;
					}
					writer.WriteAttribute("onclick", text, flag);
					writer.WriteAttribute("language", "javascript");
				}
			}
			base.Attributes.Remove("CausesValidation");
			base.RenderAttributes(writer);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000406D4 File Offset: 0x0003E8D4
		private PostBackOptions GetPostBackOptions()
		{
			Page page = this.Page;
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ValidationGroup = null;
			postBackOptions.ActionUrl = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = string.Compare(base.Type, "submit", true, Helpers.InvariantCulture) != 0;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.Validators.Count > 0;
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputButton" /> control is clicked on the Web page.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600189C RID: 6300 RVA: 0x00040765 File Offset: 0x0003E965
		// (remove) Token: 0x0600189D RID: 6301 RVA: 0x00040778 File Offset: 0x0003E978
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerClick
		{
			add
			{
				base.Events.AddHandler(HtmlInputButton.ServerClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputButton.ServerClickEvent, value);
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0004078B File Offset: 0x0003E98B
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputButton()
		{
			HtmlInputButton.ServerClickEvent = new object();
		}
	}
}
