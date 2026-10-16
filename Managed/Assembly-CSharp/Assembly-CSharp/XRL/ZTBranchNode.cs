using System;
using System.Xml;

namespace XRL
{
	// Token: 0x02000545 RID: 1349
	public class ZTBranchNode : ZoneTemplateNode
	{
		// Token: 0x0600399E RID: 14750 RVA: 0x0011CDAD File Offset: 0x0011AFAD
		public override void Load(XmlReader Reader)
		{
			this.Template = Reader.GetAttribute("Template");
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x0011CDC0 File Offset: 0x0011AFC0
		public override bool Execute(ZoneTemplateGenerationContext Context)
		{
			ZoneTemplateManager.Templates[this.Template].Execute(Context.Z, null);
			return base.Execute(Context);
		}

		// Token: 0x04001D06 RID: 7430
		public string Template;
	}
}
