using System;
using System.Xml;
using XRL.World.ZoneBuilders;

namespace XRL
{
	// Token: 0x02000544 RID: 1348
	public class ZTPopulatonNode : ZoneTemplateNode
	{
		// Token: 0x0600399B RID: 14747 RVA: 0x0011CD54 File Offset: 0x0011AF54
		public override void Load(XmlReader Reader)
		{
			this.Table = Reader.GetAttribute("Table");
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x0011CD67 File Offset: 0x0011AF67
		public override bool Execute(ZoneTemplateGenerationContext Context)
		{
			ZoneBuilderSandbox.PlacePopulationInRegion(Context.Z, Context.Regions.Regions[Context.CurrentRegion], base.VariableReplace(this.Table, Context), this.Hint, null);
			return base.Execute(Context);
		}

		// Token: 0x04001D05 RID: 7429
		public string Table;
	}
}
