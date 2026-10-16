using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides information to the parser during design time.</summary>
	// Token: 0x020001C4 RID: 452
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DesignTimeParseData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class without a specified filter. </summary>
		/// <param name="designerHost">The object for managing designer transactions and components.</param>
		/// <param name="parseText">The text to parse during design time.</param>
		// Token: 0x06001268 RID: 4712 RVA: 0x00030533 File Offset: 0x0002E733
		public DesignTimeParseData(IDesignerHost designerHost, string parseText)
		{
			this.host = designerHost;
			this.text = parseText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class with the specified filter. </summary>
		/// <param name="designerHost">The object for managing designer transactions and components.</param>
		/// <param name="parseText">The text to parse during design time.</param>
		/// <param name="filter">The filter to apply during design time.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parseText" /> is null.</exception>
		// Token: 0x06001269 RID: 4713 RVA: 0x00030549 File Offset: 0x0002E749
		public DesignTimeParseData(IDesignerHost designerHost, string parseText, string filter)
			: this(designerHost, parseText)
		{
			this.filter = filter;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003055A File Offset: 0x0002E75A
		internal void SetCollection(ICollection collection)
		{
			this.collection = collection;
		}

		/// <summary>Gets or sets the delegate for data binding at design time.</summary>
		/// <returns>An <see cref="T:System.EventHandler" /> for data binding at design time.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00030563 File Offset: 0x0002E763
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0003056B File Offset: 0x0002E76B
		public EventHandler DataBindingHandler
		{
			get
			{
				return this.db_handler;
			}
			set
			{
				this.db_handler = value;
			}
		}

		/// <summary>Gets the object for managing designer transactions and components.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> object for managing designer transactions and components.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00030574 File Offset: 0x0002E774
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		/// <summary>Gets or sets the URL at which the document is located.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the URL.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0003057C File Offset: 0x0002E77C
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00030584 File Offset: 0x0002E784
		public string DocumentUrl
		{
			get
			{
				return this.durl;
			}
			set
			{
				this.durl = value;
			}
		}

		/// <summary>Gets the text to parse.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the text to parse.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0003058D File Offset: 0x0002E78D
		public string ParseText
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets the filter used at design time.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the filter.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00030595 File Offset: 0x0002E795
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a theme should be applied.</summary>
		/// <returns>true if a theme should be applied; otherwise, false.</returns>
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0003059D File Offset: 0x0002E79D
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x000305A5 File Offset: 0x0002E7A5
		public bool ShouldApplyTheme
		{
			get
			{
				return this.theme;
			}
			set
			{
				this.theme = value;
			}
		}

		/// <summary>Gets a collection of information about user control registrations.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the tag prefix, tag name, and location of the user control. The collection is populated automatically by the .NET Framework at parse time.</returns>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000305AE File Offset: 0x0002E7AE
		public ICollection UserControlRegisterEntries
		{
			get
			{
				return this.collection;
			}
		}

		// Token: 0x04001420 RID: 5152
		private EventHandler db_handler;

		// Token: 0x04001421 RID: 5153
		private string text;

		// Token: 0x04001422 RID: 5154
		private IDesignerHost host;

		// Token: 0x04001423 RID: 5155
		private string durl;

		// Token: 0x04001424 RID: 5156
		private string filter;

		// Token: 0x04001425 RID: 5157
		private bool theme;

		// Token: 0x04001426 RID: 5158
		private ICollection collection;
	}
}
