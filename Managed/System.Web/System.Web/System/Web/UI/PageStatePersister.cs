using System;

namespace System.Web.UI
{
	/// <summary>Provides the base functionality for ASP.NET view state persistence mechanisms.</summary>
	// Token: 0x0200020E RID: 526
	public abstract class PageStatePersister
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageStatePersister" /> class.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="page" /> parameter is null.</exception>
		// Token: 0x0600159F RID: 5535 RVA: 0x00038977 File Offset: 0x00036B77
		protected PageStatePersister(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			this.page = page;
		}

		/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
		/// <returns>An object that contains view state data.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00038994 File Offset: 0x00036B94
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x0003899C File Offset: 0x00036B9C
		public object ControlState
		{
			get
			{
				return this.control_state;
			}
			set
			{
				this.control_state = value;
			}
		}

		/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
		/// <returns>An object that contains view state data.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000389A5 File Offset: 0x00036BA5
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x000389AD File Offset: 0x00036BAD
		public object ViewState
		{
			get
			{
				return this.view_state;
			}
			set
			{
				this.view_state = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.Page" /> object that the view state persistence mechanism is created for.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> that the <see cref="T:System.Web.UI.PageStatePersister" /> is associated with.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000389B6 File Offset: 0x00036BB6
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000389BE File Offset: 0x00036BBE
		protected Page Page
		{
			get
			{
				return this.page;
			}
			set
			{
				this.page = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IStateFormatter" /> object that is used to serialize and deserialize the state information contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> and <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> properties during calls to the <see cref="M:System.Web.UI.PageStatePersister.Save" /> and <see cref="M:System.Web.UI.PageStatePersister.Load" /> methods.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.IStateFormatter" /> that is used to serialize and deserialize object state.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000389C7 File Offset: 0x00036BC7
		protected IStateFormatter StateFormatter
		{
			get
			{
				if (this.state_formatter == null)
				{
					this.state_formatter = this.page.GetFormatter();
				}
				return this.state_formatter;
			}
		}

		/// <summary>Overridden by derived classes to deserialize and load persisted state information when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
		// Token: 0x060015A7 RID: 5543
		public abstract void Load();

		/// <summary>Overridden by derived classes to serialize persisted state information when a <see cref="T:System.Web.UI.Page" /> object is unloaded from memory.</summary>
		// Token: 0x060015A8 RID: 5544
		public abstract void Save();

		// Token: 0x04001531 RID: 5425
		private object control_state;

		// Token: 0x04001532 RID: 5426
		private object view_state;

		// Token: 0x04001533 RID: 5427
		private Page page;

		// Token: 0x04001534 RID: 5428
		private IStateFormatter state_formatter;
	}
}
