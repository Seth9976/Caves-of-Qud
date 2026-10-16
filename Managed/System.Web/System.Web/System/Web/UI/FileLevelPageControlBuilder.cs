using System;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	/// <summary>Parses page files and is the default <see cref="T:System.Web.UI.ControlBuilder" /> class for parsing page files.</summary>
	// Token: 0x020001CB RID: 459
	public class FileLevelPageControlBuilder : RootBuilder
	{
		/// <summary>Adds the specified literal content to a control. </summary>
		/// <param name="text">The content to add to the control.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.FileLevelPageControlBuilder.AppendLiteralString(System.String)" /> method cannot append the literal string to a content page.</exception>
		// Token: 0x060012A6 RID: 4774 RVA: 0x00030A2C File Offset: 0x0002EC2C
		public override void AppendLiteralString(string text)
		{
			bool flag = text == null || text.Trim().Length == 0;
			if (this.hasContentControls && !flag)
			{
				throw new HttpException("Literal strings cannot be appended to Content pages.");
			}
			if (!flag)
			{
				this.hasLiteralControls = true;
			}
			base.AppendLiteralString(text);
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.ControlBuilder" /> object to the <see cref="T:System.Web.UI.FileLevelPageControlBuilder" /> object for any child controls that belong to the container control.</summary>
		/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> assigned to the child control. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlBuilder" /> that was added is associated with a <see cref="T:System.Web.UI.WebControls.Content" /> control and is only allowed on pages that contain <see cref="T:System.Web.UI.WebControls.Content" /> controls.</exception>
		/// <exception cref="T:System.Web.HttpParseException">The content page contained a literal other than a <see cref="T:System.Web.UI.WebControls.Content" /> control.</exception>
		// Token: 0x060012A7 RID: 4775 RVA: 0x00030A78 File Offset: 0x0002EC78
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			if (subBuilder == null)
			{
				base.AppendSubBuilder(subBuilder);
				return;
			}
			if (typeof(ContentBuilderInternal).IsAssignableFrom(subBuilder.GetType()))
			{
				if (this.hasOtherControls)
				{
					throw new HttpException("Only Content controls are supported on content pages.");
				}
				this.hasContentControls = true;
				if (this.hasLiteralControls)
				{
					throw new HttpParseException("Only Content controls are supported on content pages.");
				}
			}
			else
			{
				this.hasOtherControls = true;
			}
			base.AppendSubBuilder(subBuilder);
		}

		// Token: 0x04001431 RID: 5169
		private bool hasContentControls;

		// Token: 0x04001432 RID: 5170
		private bool hasLiteralControls;

		// Token: 0x04001433 RID: 5171
		private bool hasOtherControls;
	}
}
