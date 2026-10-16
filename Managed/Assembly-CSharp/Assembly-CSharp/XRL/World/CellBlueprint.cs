using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000984 RID: 2436
	public class CellBlueprint
	{
		// Token: 0x060066F5 RID: 26357 RVA: 0x002001EC File Offset: 0x001FE3EC
		public void CopyFrom(CellBlueprint ParentBlueprint)
		{
			this.AmbientBed = ParentBlueprint.AmbientBed;
			for (int i = 0; i < Definitions.Height; i++)
			{
				for (int j = 0; j < Definitions.Width; j++)
				{
					for (int k = 0; k < Definitions.Layers; k++)
					{
						this.LevelBlueprint[j, i, k] = ParentBlueprint.LevelBlueprint[j, i, k];
					}
				}
			}
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x00200251 File Offset: 0x001FE451
		public void SetProperty(string Key, object Value)
		{
			if (this.Properties == null)
			{
				this.Properties = new Dictionary<string, object>();
			}
			this.Properties[Key] = Value;
		}

		// Token: 0x04002F91 RID: 12177
		public string Name;

		// Token: 0x04002F92 RID: 12178
		public string Inherits;

		// Token: 0x04002F93 RID: 12179
		public string ApplyTo;

		// Token: 0x04002F94 RID: 12180
		public string LandingZone;

		// Token: 0x04002F95 RID: 12181
		public string AmbientBed;

		// Token: 0x04002F96 RID: 12182
		public bool Mutable = true;

		// Token: 0x04002F97 RID: 12183
		public ZoneBlueprint[,,] LevelBlueprint = new ZoneBlueprint[Definitions.Width, Definitions.Height, Definitions.Layers];

		// Token: 0x04002F98 RID: 12184
		public Dictionary<string, object> Properties;
	}
}
