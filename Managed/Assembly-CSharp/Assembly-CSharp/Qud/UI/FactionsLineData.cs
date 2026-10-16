using System;
using System.Text;
using ConsoleLib.Console;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000462 RID: 1122
	public class FactionsLineData : PooledFrameworkDataElement<FactionsLineData>
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000DFDC4 File Offset: 0x000DDFC4
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x000DFEC9 File Offset: 0x000DE0C9
		public string searchText
		{
			get
			{
				if (this._searchText == null)
				{
					FactionsLineData.searchBulder.Length = 0;
					Faction faction = Factions.Get(this.id);
					FactionsLineData.searchBulder.Append(faction.Name);
					FactionsLineData.searchBulder.Append(" ");
					FactionsLineData.searchBulder.Append(faction.GetFeelingText());
					FactionsLineData.searchBulder.Append(" ");
					FactionsLineData.searchBulder.Append(faction.GetRankText());
					FactionsLineData.searchBulder.Append(" ");
					FactionsLineData.searchBulder.Append(faction.GetPetText());
					FactionsLineData.searchBulder.Append(" ");
					FactionsLineData.searchBulder.Append(faction.GetHolyPlaceText());
					FactionsLineData.searchBulder.Append(" ");
					FactionsLineData.searchBulder.Append(Faction.GetPreferredSecretDescription(this.id));
					this._searchText = FactionsLineData.searchBulder.ToString().ToLower();
				}
				return this._searchText;
			}
			set
			{
				this._searchText = ((value != null) ? value.ToLower() : null);
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000DFEE0 File Offset: 0x000DE0E0
		public FactionsLineData set(string id, string label, IRenderable icon, bool expanded)
		{
			this.id = id;
			this.label = label;
			this.icon = icon;
			this.expanded = expanded;
			this.searchText = null;
			Faction faction = Factions.Get(id);
			this.name = faction.DisplayName;
			this.rep = Faction.PlayerReputation.Get(id);
			return this;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000DFF36 File Offset: 0x000DE136
		public override void free()
		{
			this.id = null;
			this.label = null;
			this.icon = null;
			base.free();
		}

		// Token: 0x0400167B RID: 5755
		public string id;

		// Token: 0x0400167C RID: 5756
		public string label;

		// Token: 0x0400167D RID: 5757
		public IRenderable icon;

		// Token: 0x0400167E RID: 5758
		public bool expanded = true;

		// Token: 0x0400167F RID: 5759
		public string name;

		// Token: 0x04001680 RID: 5760
		public int rep;

		// Token: 0x04001681 RID: 5761
		private static StringBuilder searchBulder = new StringBuilder();

		// Token: 0x04001682 RID: 5762
		public string _searchText;
	}
}
