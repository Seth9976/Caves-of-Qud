using System;

namespace System.Web.UI
{
	// Token: 0x020001DA RID: 474
	internal interface IScriptManager
	{
		// Token: 0x06001348 RID: 4936
		void RegisterOnSubmitStatementExternal(Control control, Type type, string key, string script);

		// Token: 0x06001349 RID: 4937
		void RegisterExpandoAttributeExternal(Control control, string controlId, string attributeName, string attributeValue, bool encode);

		// Token: 0x0600134A RID: 4938
		void RegisterHiddenFieldExternal(Control control, string hiddenFieldName, string hiddenFieldInitialValue);

		// Token: 0x0600134B RID: 4939
		void RegisterStartupScriptExternal(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x0600134C RID: 4940
		void RegisterArrayDeclarationExternal(Control control, string arrayName, string arrayValue);

		// Token: 0x0600134D RID: 4941
		void RegisterClientScriptBlockExternal(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x0600134E RID: 4942
		void RegisterClientScriptIncludeExternal(Control control, Type type, string key, string url);

		// Token: 0x0600134F RID: 4943
		void RegisterClientScriptResourceExternal(Control control, Type type, string resourceName);
	}
}
