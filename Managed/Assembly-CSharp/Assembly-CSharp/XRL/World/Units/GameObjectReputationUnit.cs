using System;

namespace XRL.World.Units
{
	// Token: 0x020009B2 RID: 2482
	[Serializable]
	public class GameObjectReputationUnit : GameObjectUnit
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06006AF5 RID: 27381 RVA: 0x002272AC File Offset: 0x002254AC
		// (set) Token: 0x06006AF6 RID: 27382 RVA: 0x002272D8 File Offset: 0x002254D8
		public Faction Entry
		{
			get
			{
				Faction faction;
				if ((faction = this._Entry) == null)
				{
					faction = (this._Entry = Factions.GetIfExists(this.Faction));
				}
				return faction;
			}
			set
			{
				this._Entry = value;
				this.Faction = ((value != null) ? value.Name : null);
			}
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x00227304 File Offset: 0x00225504
		public override void Apply(GameObject Object)
		{
			The.Game.PlayerReputation.Modify(this.Entry, this.Value, null, null, this.Type, this.Silent, false, false, false);
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x00227340 File Offset: 0x00225540
		public override void Remove(GameObject Object)
		{
			The.Game.PlayerReputation.Modify(this.Entry, -this.Value, null, null, this.Type, this.Silent, false, false, false);
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x0022737B File Offset: 0x0022557B
		public override void Reset()
		{
			base.Reset();
			this.Faction = null;
			this.Value = 0;
			this._Entry = null;
			this.Silent = false;
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x002273A0 File Offset: 0x002255A0
		public override string GetDescription(bool Inscription = false)
		{
			if (this.Entry != null)
			{
				return Event.FinalizeString(Event.NewStringBuilder(null).AppendSigned(this.Value).Append(" reputation with ")
					.Append(this.Entry.GetFormattedName()));
			}
			return base.GetDescription(Inscription);
		}

		// Token: 0x040030FA RID: 12538
		public string Faction;

		// Token: 0x040030FB RID: 12539
		public string Type;

		// Token: 0x040030FC RID: 12540
		public int Value;

		// Token: 0x040030FD RID: 12541
		public bool Silent;

		// Token: 0x040030FE RID: 12542
		[NonSerialized]
		private Faction _Entry;
	}
}
