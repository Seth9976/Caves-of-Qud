using System;
using System.Xml;

namespace XRL
{
	// Token: 0x02000546 RID: 1350
	public class ZTBuilderNode : ZoneTemplateNode
	{
		// Token: 0x060039A1 RID: 14753 RVA: 0x0011CDEE File Offset: 0x0011AFEE
		public override void Load(XmlReader Reader)
		{
			this.Class = Reader.GetAttribute("Class");
		}

		// Token: 0x04001D07 RID: 7431
		public string Class;
	}
}
