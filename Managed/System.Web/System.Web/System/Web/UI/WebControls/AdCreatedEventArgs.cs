using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event of the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200032B RID: 811
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AdCreatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> class.</summary>
		/// <param name="adProperties">A <see cref="T:System.Collections.IDictionary" /> containing the advertisement properties from the XML file. </param>
		// Token: 0x06001BFF RID: 7167 RVA: 0x00043F88 File Offset: 0x00042188
		public AdCreatedEventArgs(IDictionary adProperties)
		{
			this.properties = adProperties;
			if (this.properties != null)
			{
				this.alt_text = (string)this.properties["AlternateText"];
				this.img_url = (string)this.properties["ImageUrl"];
				this.nav_url = (string)this.properties["NavigateUrl"];
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> object that contains all the advertisement properties for the currently displayed advertisement.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> that contains a list of advertisement properties for the currently displayed advertisement. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00043FFB File Offset: 0x000421FB
		public IDictionary AdProperties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control when the advertisement image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip for the advertisement.</summary>
		/// <returns>The text displayed in place of the advertisement image if the image is unavailable. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00044003 File Offset: 0x00042203
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0004400B File Offset: 0x0004220B
		public string AlternateText
		{
			get
			{
				return this.alt_text;
			}
			set
			{
				this.alt_text = value;
			}
		}

		/// <summary>Gets or sets the URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <returns>The URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00044014 File Offset: 0x00042214
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0004401C File Offset: 0x0004221C
		public string ImageUrl
		{
			get
			{
				return this.img_url;
			}
			set
			{
				this.img_url = value;
			}
		}

		/// <summary>Gets or sets the Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
		/// <returns>The Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00044025 File Offset: 0x00042225
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0004402D File Offset: 0x0004222D
		public string NavigateUrl
		{
			get
			{
				return this.nav_url;
			}
			set
			{
				this.nav_url = value;
			}
		}

		// Token: 0x040017E1 RID: 6113
		private IDictionary properties;

		// Token: 0x040017E2 RID: 6114
		private string alt_text;

		// Token: 0x040017E3 RID: 6115
		private string img_url;

		// Token: 0x040017E4 RID: 6116
		private string nav_url;
	}
}
