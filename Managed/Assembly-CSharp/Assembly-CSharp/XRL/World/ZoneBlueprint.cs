using System;
using System.Collections.Generic;
using System.Linq;

namespace XRL.World
{
	// Token: 0x02000983 RID: 2435
	public class ZoneBlueprint
	{
		// Token: 0x060066F2 RID: 26354 RVA: 0x002000D8 File Offset: 0x001FE2D8
		public ZoneBlueprint(ZoneBlueprint Parent)
		{
			if (Parent != null)
			{
				this.Cell = Parent.Cell;
				this.Level = "-";
				this.Tier = Parent.Tier;
				this.AmbientBed = Parent.AmbientBed;
				this.AmbientSounds = Parent.AmbientSounds;
				this.AmbientVolume = Parent.AmbientVolume;
				this.Builders = new ZoneBuilderCollection(Parent.Builders);
				this.Parts = ((Parent.Parts == null) ? null : new ZonePartCollection(Parent.Parts));
				this.x = "-";
				this.y = "-";
				if (!Parent.Name.IsNullOrEmpty())
				{
					this.Name = Parent.Name;
					return;
				}
			}
			else
			{
				this.Builders = new ZoneBuilderCollection();
			}
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x002001BC File Offset: 0x001FE3BC
		public bool AnyBuilder(Func<ZoneBuilderBlueprint, bool> p)
		{
			return this.Builders.Any<ZoneBuilderBlueprint>(p);
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x002001CA File Offset: 0x001FE3CA
		public void SetProperty(string Key, object Value)
		{
			if (this.Properties == null)
			{
				this.Properties = new Dictionary<string, object>();
			}
			this.Properties[Key] = Value;
		}

		// Token: 0x04002F78 RID: 12152
		public string Name;

		// Token: 0x04002F79 RID: 12153
		public string Level;

		// Token: 0x04002F7A RID: 12154
		public int Tier;

		// Token: 0x04002F7B RID: 12155
		public string x;

		// Token: 0x04002F7C RID: 12156
		public string y;

		// Token: 0x04002F7D RID: 12157
		public string GroundLiquid;

		// Token: 0x04002F7E RID: 12158
		public bool disableForcedConnections;

		// Token: 0x04002F7F RID: 12159
		public bool ProperName;

		// Token: 0x04002F80 RID: 12160
		public string NameContext;

		// Token: 0x04002F81 RID: 12161
		public string IndefiniteArticle;

		// Token: 0x04002F82 RID: 12162
		public string DefiniteArticle;

		// Token: 0x04002F83 RID: 12163
		public string AmbientBed;

		// Token: 0x04002F84 RID: 12164
		public string AmbientSounds;

		// Token: 0x04002F85 RID: 12165
		public int AmbientVolume = -1;

		// Token: 0x04002F86 RID: 12166
		public bool IncludeContextInZoneDisplay = true;

		// Token: 0x04002F87 RID: 12167
		public bool IncludeStratumInZoneDisplay = true;

		// Token: 0x04002F88 RID: 12168
		public bool HasWeather;

		// Token: 0x04002F89 RID: 12169
		public bool HasBiomes = true;

		// Token: 0x04002F8A RID: 12170
		public string WindSpeed;

		// Token: 0x04002F8B RID: 12171
		public string WindDirections;

		// Token: 0x04002F8C RID: 12172
		public string WindDuration;

		// Token: 0x04002F8D RID: 12173
		public CellBlueprint Cell;

		// Token: 0x04002F8E RID: 12174
		public ZoneBuilderCollection Builders;

		// Token: 0x04002F8F RID: 12175
		public ZonePartCollection Parts;

		// Token: 0x04002F90 RID: 12176
		public Dictionary<string, object> Properties;
	}
}
