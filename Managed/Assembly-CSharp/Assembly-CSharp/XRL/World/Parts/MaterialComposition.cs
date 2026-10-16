using System;

namespace XRL.World.Parts
{
	// Token: 0x02001063 RID: 4195
	[Serializable]
	public class MaterialComposition : IPart
	{
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600A6F1 RID: 42737 RVA: 0x003D0A8F File Offset: 0x003CEC8F
		// (set) Token: 0x0600A6F2 RID: 42738 RVA: 0x003D0A97 File Offset: 0x003CEC97
		public string Material
		{
			get
			{
				return this._Material;
			}
			set
			{
				this._Material = value;
			}
		}

		// Token: 0x040044F8 RID: 17656
		public string _Material;
	}
}
