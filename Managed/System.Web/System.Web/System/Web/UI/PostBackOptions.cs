using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Specifies how client-side JavaScript is generated to initiate a postback event.</summary>
	// Token: 0x02000217 RID: 535
	public sealed class PostBackOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control data.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		// Token: 0x060015EF RID: 5615 RVA: 0x00038E4C File Offset: 0x0003704C
		public PostBackOptions(Control targetControl)
			: this(targetControl, null, null, false, false, false, true, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified target control and argument data.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		/// <param name="argument">The optional parameter passed during the postback event.</param>
		// Token: 0x060015F0 RID: 5616 RVA: 0x00038E68 File Offset: 0x00037068
		public PostBackOptions(Control targetControl, string argument)
			: this(targetControl, argument, null, false, false, false, true, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PostBackOptions" /> class with the specified values for the instance's properties.</summary>
		/// <param name="targetControl">The <see cref="T:System.Web.UI.Control" /> that receives the postback event.</param>
		/// <param name="argument">The optional parameter passed during the postback event.</param>
		/// <param name="actionUrl">The target of the postback.</param>
		/// <param name="autoPostBack">true to automatically post the form back to the server in response to a user action; otherwise, false.</param>
		/// <param name="requiresJavaScriptProtocol">true if the javascript: prefix is required; otherwise, false.</param>
		/// <param name="trackFocus">true if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, false.</param>
		/// <param name="clientSubmit">true if the postback event can be raised by client script; otherwise, false.</param>
		/// <param name="performValidation">true if client-side validation is required before the postback event occurs; otherwise, false.</param>
		/// <param name="validationGroup">The group of controls for which <see cref="T:System.Web.UI.PostBackOptions" /> causes validation when it posts back to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetControl" /> is null.</exception>
		// Token: 0x060015F1 RID: 5617 RVA: 0x00038E84 File Offset: 0x00037084
		public PostBackOptions(Control targetControl, string argument, string actionUrl, bool autoPostBack, bool requiresJavaScriptProtocol, bool trackFocus, bool clientSubmit, bool performValidation, string validationGroup)
		{
			if (targetControl == null)
			{
				throw new ArgumentNullException("targetControl");
			}
			this.control = targetControl;
			this.argument = argument;
			this.actionUrl = actionUrl;
			this.autoPostBack = autoPostBack;
			this.requiresJavaScriptProtocol = requiresJavaScriptProtocol;
			this.trackFocus = trackFocus;
			this.clientSubmit = clientSubmit;
			this.performValidation = performValidation;
			this.validationGroup = validationGroup;
		}

		/// <summary>Gets or sets the target URL for the postback of a Web Forms page.</summary>
		/// <returns>The URL for the postback of a Web Forms page. The default value is an empty string ("").</returns>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00038EEA File Offset: 0x000370EA
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x00038EF2 File Offset: 0x000370F2
		[DefaultValue("")]
		public string ActionUrl
		{
			get
			{
				return this.actionUrl;
			}
			set
			{
				this.actionUrl = value;
			}
		}

		/// <summary>Gets or sets an optional argument that is transferred in the postback event.</summary>
		/// <returns>The optional argument that is transferred in the postback event. The default value is an empty string ("").</returns>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00038EFB File Offset: 0x000370FB
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00038F03 File Offset: 0x00037103
		[DefaultValue("")]
		public string Argument
		{
			get
			{
				return this.argument;
			}
			set
			{
				this.argument = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the form will automatically post back to the server in response to a user action.</summary>
		/// <returns>true if the form will automatically post back in response to a user action; otherwise, false. The default value is false.</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00038F0C File Offset: 0x0003710C
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00038F14 File Offset: 0x00037114
		[global::System.MonoTODO("Implement support for this in Page")]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return this.autoPostBack;
			}
			set
			{
				this.autoPostBack = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the postback event should occur from client-side script.</summary>
		/// <returns>true if the postback event should occur from client-side script; otherwise, false. The default value is true.</returns>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00038F1D File Offset: 0x0003711D
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00038F25 File Offset: 0x00037125
		[DefaultValue(true)]
		public bool ClientSubmit
		{
			get
			{
				return this.clientSubmit;
			}
			set
			{
				this.clientSubmit = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side validation is required before the postback event occurs.</summary>
		/// <returns>true if client-side validation is required before the postback event occurs; otherwise, false. The default value is false.</returns>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00038F2E File Offset: 0x0003712E
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x00038F36 File Offset: 0x00037136
		[DefaultValue(false)]
		public bool PerformValidation
		{
			get
			{
				return this.performValidation;
			}
			set
			{
				this.performValidation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the javascript: prefix is generated for the client-side script. </summary>
		/// <returns>true if the javascript: prefix is generated for the client-side script; otherwise, false. The default value is true.</returns>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00038F3F File Offset: 0x0003713F
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x00038F47 File Offset: 0x00037147
		[DefaultValue(true)]
		public bool RequiresJavaScriptProtocol
		{
			get
			{
				return this.requiresJavaScriptProtocol;
			}
			set
			{
				this.requiresJavaScriptProtocol = value;
			}
		}

		/// <summary>Gets the control target that receives the postback event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that represents the control that receives the postback event.</returns>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00038F50 File Offset: 0x00037150
		[DefaultValue(null)]
		public Control TargetControl
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets or sets a value indicating whether the postback event should return the page to the current scroll position and return focus to the current control.</summary>
		/// <returns>true if the postback event should return the page to the current scroll position and return focus to the target control; otherwise, false. The default value is false.</returns>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00038F58 File Offset: 0x00037158
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x00038F60 File Offset: 0x00037160
		[global::System.MonoTODO("Implement support for this in Page")]
		[DefaultValue(false)]
		public bool TrackFocus
		{
			get
			{
				return this.trackFocus;
			}
			set
			{
				this.trackFocus = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.PostBackOptions" /> object causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00038F69 File Offset: 0x00037169
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x00038F71 File Offset: 0x00037171
		[DefaultValue("")]
		[global::System.MonoTODO("Implement support for this in Page")]
		public string ValidationGroup
		{
			get
			{
				return this.validationGroup;
			}
			set
			{
				this.validationGroup = value;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00038F7A File Offset: 0x0003717A
		internal bool RequiresSpecialPostBack
		{
			get
			{
				return this.actionUrl != null || this.validationGroup != null || this.trackFocus || this.autoPostBack || this.argument != null;
			}
		}

		// Token: 0x0400154D RID: 5453
		private Control control;

		// Token: 0x0400154E RID: 5454
		private string argument;

		// Token: 0x0400154F RID: 5455
		private string actionUrl;

		// Token: 0x04001550 RID: 5456
		private bool autoPostBack;

		// Token: 0x04001551 RID: 5457
		private bool requiresJavaScriptProtocol;

		// Token: 0x04001552 RID: 5458
		private bool trackFocus;

		// Token: 0x04001553 RID: 5459
		private bool clientSubmit;

		// Token: 0x04001554 RID: 5460
		private bool performValidation;

		// Token: 0x04001555 RID: 5461
		private string validationGroup;
	}
}
