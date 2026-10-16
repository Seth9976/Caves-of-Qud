using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C12 RID: 3090
	public class RequireReputation : IConversationPart
	{
		// Token: 0x170009DF RID: 2527
		// (set) Token: 0x06007BDA RID: 31706 RVA: 0x002E8E54 File Offset: 0x002E7054
		public string Level
		{
			set
			{
				string text = value.ToUpperInvariant();
				int num;
				if (!(text == "LOVED"))
				{
					if (!(text == "LIKED"))
					{
						if (!(text == "INDIFFERENT"))
						{
							if (!(text == "DISLIKED"))
							{
								if (!(text == "HATED"))
								{
									num = int.MaxValue;
								}
								else
								{
									num = -2;
								}
							}
							else
							{
								num = -1;
							}
						}
						else
						{
							num = 0;
						}
					}
					else
					{
						num = 1;
					}
				}
				else
				{
					num = 2;
				}
				this.Value = num;
			}
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x002E8ECD File Offset: 0x002E70CD
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID || ID == EnterElementEvent.ID || ID == GetChoiceTagEvent.ID || ID == ColorTextEvent.ID;
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x002E8EFB File Offset: 0x002E70FB
		public override bool HandleEvent(PrepareTextEvent E)
		{
			this.Fulfilled = The.Game.PlayerReputation.GetLevel(this.Faction ?? The.Speaker.GetPrimaryFaction(false)) >= this.Value;
			return base.HandleEvent(E);
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x002E8F39 File Offset: 0x002E7139
		public override bool HandleEvent(EnterElementEvent E)
		{
			return this.Fulfilled;
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x002E8F44 File Offset: 0x002E7144
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			Faction faction = Factions.Get(this.Faction ?? The.Speaker.GetPrimaryFaction(false));
			char c = (this.Fulfilled ? 'C' : 'r');
			string text = "Hated by ";
			if (this.Value >= 2)
			{
				text = "Loved by ";
			}
			else if (this.Value >= 1)
			{
				text = "Liked by ";
			}
			else if (this.Value >= 0)
			{
				text = "Indifferent to ";
			}
			else if (this.Value >= -1)
			{
				text = "Disliked by ";
			}
			E.Tag = string.Concat(new string[]
			{
				"{{",
				c.ToString(),
				"|[",
				text,
				faction.GetFormattedName(),
				"]}}"
			});
			return false;
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x002E9004 File Offset: 0x002E7204
		public override bool HandleEvent(ColorTextEvent E)
		{
			if (!this.Fulfilled)
			{
				E.Color = "K";
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400384C RID: 14412
		public string Faction;

		// Token: 0x0400384D RID: 14413
		public int Value = int.MaxValue;

		// Token: 0x0400384E RID: 14414
		public bool Fulfilled;
	}
}
