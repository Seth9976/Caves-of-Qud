using System;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B0 RID: 944
	internal interface IMenuRenderer
	{
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060026A6 RID: 9894
		HtmlTextWriterTag Tag { get; }

		// Token: 0x060026A7 RID: 9895
		void AddAttributesToRender(HtmlTextWriter writer);

		// Token: 0x060026A8 RID: 9896
		void PreRender(Page page, HtmlHead head, ClientScriptManager csm, string cmenu, StringBuilder script);

		// Token: 0x060026A9 RID: 9897
		void RenderBeginTag(HtmlTextWriter writer, string skipLinkText);

		// Token: 0x060026AA RID: 9898
		void RenderEndTag(HtmlTextWriter writer);

		// Token: 0x060026AB RID: 9899
		void RenderContents(HtmlTextWriter writer);

		// Token: 0x060026AC RID: 9900
		void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem);

		// Token: 0x060026AD RID: 9901
		void RenderMenuBeginTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x060026AE RID: 9902
		void RenderMenuBody(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, bool notLast);

		// Token: 0x060026AF RID: 9903
		void RenderMenuEndTag(HtmlTextWriter writer, bool dynamic, int menuLevel);

		// Token: 0x060026B0 RID: 9904
		void RenderMenuItem(HtmlTextWriter writer, MenuItem item, bool notLast, bool isFirst);

		// Token: 0x060026B1 RID: 9905
		bool IsDynamicItem(MenuItem item);

		// Token: 0x060026B2 RID: 9906
		bool IsDynamicItem(Menu owner, MenuItem item);
	}
}
