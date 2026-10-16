using System;

namespace System.Web.UI
{
	// Token: 0x0200022A RID: 554
	internal class TemplateBinding
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x0003AFD5 File Offset: 0x000391D5
		public TemplateBinding(Type controlType, string controlProperty, string controlId, string fieldName)
		{
			this.ControlType = controlType;
			this.ControlProperty = controlProperty;
			this.ControlId = controlId;
			this.FieldName = fieldName;
		}

		// Token: 0x04001583 RID: 5507
		public Type ControlType;

		// Token: 0x04001584 RID: 5508
		public string ControlProperty;

		// Token: 0x04001585 RID: 5509
		public string ControlId;

		// Token: 0x04001586 RID: 5510
		public string FieldName;
	}
}
