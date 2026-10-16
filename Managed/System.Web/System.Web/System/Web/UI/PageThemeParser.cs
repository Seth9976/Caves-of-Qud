using System;

namespace System.Web.UI
{
	// Token: 0x02000212 RID: 530
	internal sealed class PageThemeParser : UserControlParser
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00038B06 File Offset: 0x00036D06
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00038B0E File Offset: 0x00036D0E
		public string[] LinkedStyleSheets
		{
			get
			{
				return this.linkedStyleSheets;
			}
			set
			{
				this.linkedStyleSheets = value;
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00038B17 File Offset: 0x00036D17
		internal PageThemeParser(VirtualPath virtualPath, HttpContext context)
			: base(virtualPath, virtualPath.PhysicalPath, context, "System.Web.UI.PageTheme")
		{
			this.AddDependency(virtualPath.Original);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00003946 File Offset: 0x00001B46
		internal override void HandleOptions(object obj)
		{
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00038AFF File Offset: 0x00036CFF
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.PageTheme";
			}
		}

		// Token: 0x04001536 RID: 5430
		private string[] linkedStyleSheets;
	}
}
