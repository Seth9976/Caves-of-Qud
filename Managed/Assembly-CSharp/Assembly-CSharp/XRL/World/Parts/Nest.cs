using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000EE6 RID: 3814
	[Serializable]
	public class Nest : IPart
	{
		// Token: 0x0600988C RID: 39052 RVA: 0x003766C7 File Offset: 0x003748C7
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600988D RID: 39053 RVA: 0x003766CA File Offset: 0x003748CA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600988E RID: 39054 RVA: 0x003766CD File Offset: 0x003748CD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600988F RID: 39055 RVA: 0x003766E2 File Offset: 0x003748E2
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.CollapseAfterSpawn = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009890 RID: 39056 RVA: 0x00376704 File Offset: 0x00374904
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.NumberToSpawn > 0)
			{
				if (this.SpawnCooldown == -2147483648)
				{
					this.SpawnCooldown = this.TurnsPerSpawn.RollCached();
				}
				if (this.ParentObject.CurrentCell != null)
				{
					this.SpawnCooldown--;
					if (this.SpawnCooldown <= 0)
					{
						int i = 0;
						int num = this.NumberSpawned.RollCached();
						while (i < num)
						{
							Cell randomLocalAdjacentCell = this.ParentObject.CurrentCell.GetRandomLocalAdjacentCell();
							if (randomLocalAdjacentCell != null && randomLocalAdjacentCell.IsEmpty() && this.ChancePerSpawn.in100())
							{
								if (this.NumberToSpawn > 0)
								{
									this.NumberToSpawn--;
								}
								GameObject gameObject = GameObject.Create(this.BlueprintSpawned);
								if (gameObject.HasStat("XPValue"))
								{
									gameObject.GetStat("XPValue").BaseValue = (int)Math.Round((double)((float)gameObject.GetStat("XPValue").BaseValue * this.XPFactor / 5f)) * 5;
								}
								gameObject.TakeAllegiance<AllyBirth>(this.ParentObject);
								gameObject.MakeActive(false);
								randomLocalAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								if (base.Visible())
								{
									IComponent<GameObject>.AddPlayerMessage(this.SpawnMessage, null, true);
								}
								this.ParentObject.Slimesplatter(false, this.SpawnParticle);
								this.SpawnCooldown = this.TurnsPerSpawn.RollCached();
							}
							i++;
						}
					}
				}
				return;
			}
			if (!this.CollapseAfterSpawn)
			{
				this.ParentObject.RemovePart(this);
				return;
			}
			if (base.Visible())
			{
				IComponent<GameObject>.AddPlayerMessage(this.CollapseMessage, null, true);
			}
			this.ParentObject.Destroy(null, false, false, null);
		}

		// Token: 0x04003F85 RID: 16261
		public int NumberToSpawn = 30;

		// Token: 0x04003F86 RID: 16262
		public int ChancePerSpawn = 100;

		// Token: 0x04003F87 RID: 16263
		public float XPFactor = 0.25f;

		// Token: 0x04003F88 RID: 16264
		public bool CollapseAfterSpawn = true;

		// Token: 0x04003F89 RID: 16265
		public string TurnsPerSpawn = "15-20";

		// Token: 0x04003F8A RID: 16266
		public string NumberSpawned = "1";

		// Token: 0x04003F8B RID: 16267
		public string SpawnMessage = "A giant centipede crawls out of the nest.";

		// Token: 0x04003F8C RID: 16268
		public string CollapseMessage = "The nest collapses.";

		// Token: 0x04003F8D RID: 16269
		public string SpawnParticle = "&w.";

		// Token: 0x04003F8E RID: 16270
		public string BlueprintSpawned = "Giant Centipede";

		// Token: 0x04003F8F RID: 16271
		public int SpawnCooldown = int.MinValue;
	}
}
