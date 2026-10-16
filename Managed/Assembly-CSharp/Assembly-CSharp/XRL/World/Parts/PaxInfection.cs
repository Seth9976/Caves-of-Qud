using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World.AI.GoalHandlers;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001036 RID: 4150
	[Serializable]
	public class PaxInfection : IPart
	{
		// Token: 0x0600A546 RID: 42310 RVA: 0x003C6CF5 File Offset: 0x003C4EF5
		public int GetCooldown()
		{
			return 50;
		}

		// Token: 0x0600A547 RID: 42311 RVA: 0x003C6CF9 File Offset: 0x003C4EF9
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, this.ParentObject.Equipped), this.GetCooldown(), 0);
		}

		// Token: 0x0600A548 RID: 42312 RVA: 0x003C6D1F File Offset: 0x003C4F1F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<AfterGameLoadedEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A549 RID: 42313 RVA: 0x003C6D4D File Offset: 0x003C4F4D
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A54A RID: 42314 RVA: 0x003C6D7A File Offset: 0x003C4F7A
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			this.UpdateAbility(this.ParentObject.Equipped, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A54B RID: 42315 RVA: 0x003C6D98 File Offset: 0x003C4F98
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "BeforeApplyDamage");
			E.Actor.RegisterPartEvent(this, "ActivatePaxInfection");
			this.UpdateAbility(E.Actor, false);
			if (!E.Actor.IsPlayer() && E.Actor.Brain != null)
			{
				E.Actor.Brain.PushGoal(new PaxKlanqMadness());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A54C RID: 42316 RVA: 0x003C6E0B File Offset: 0x003C500B
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "BeforeApplyDamage");
			E.Actor.UnregisterPartEvent(this, "ActivatePaxInfection");
			this.UpdateAbility(E.Actor, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A54D RID: 42317 RVA: 0x003C6E43 File Offset: 0x003C5043
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A54E RID: 42318 RVA: 0x003C6E48 File Offset: 0x003C5048
		public void UpdateAbility(GameObject GO, bool ForceRemove = false)
		{
			if (GO == null)
			{
				return;
			}
			bool flag = false;
			if (!ForceRemove)
			{
				Body body = GO.Body;
				if (body != null)
				{
					foreach (BodyPart bodyPart in body.GetParts())
					{
						if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == this.ParentObject.Blueprint)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				if (this.ActivatedAbilityID == Guid.Empty || GO.GetActivatedAbility(this.ActivatedAbilityID) == null)
				{
					this.ActivatedAbilityID = GO.AddActivatedAbility("Puff Spores", "ActivatePaxInfection", "Items", null, "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
					return;
				}
			}
			else
			{
				GO.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			}
		}

		// Token: 0x0600A54F RID: 42319 RVA: 0x003C6F38 File Offset: 0x003C5138
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ActivatePaxInfection")
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null && equipped.IsActivatedAbilityUsable(this.ActivatedAbilityID))
				{
					this.Puff(false);
					this.ParentObject.UseEnergy(1000);
					equipped.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, false);
					if (equipped.IsPlayer())
					{
						The.Game.Systems.ForEach(delegate(IGameSystem system)
						{
							system.FireEvent(E);
						});
					}
					IComponent<GameObject>.XDidY(equipped, "spew", "a cloud of spores", null, null, null, null, null, false, false, null, null, false, true, false, false, false, null);
				}
			}
			else if (E.ID == "BeforeApplyDamage" && 25.in100())
			{
				this.Puff(true);
				GameObject equipped2 = this.ParentObject.Equipped;
				if (equipped2 != null)
				{
					if (equipped2.IsPlayer())
					{
						BodyPart bodyPart = this.ParentObject.EquippedOn();
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"Your ",
							bodyPart.GetOrdinalName(),
							" ",
							bodyPart.Plural ? "spew" : "spews",
							" a cloud of spores."
						}), null, true);
					}
					else if (IComponent<GameObject>.Visible(equipped2))
					{
						BodyPart bodyPart2 = this.ParentObject.EquippedOn();
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							Grammar.MakePossessive(equipped2.The + equipped2.ShortDisplayName),
							" ",
							bodyPart2.GetOrdinalName(),
							" ",
							bodyPart2.Plural ? "spew" : "spews",
							" a cloud of spores."
						}), null, true);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A550 RID: 42320 RVA: 0x003C7128 File Offset: 0x003C5328
		public void Puff(bool OnlyIfCreatureNearby = true)
		{
			List<Cell> list = new List<Cell>();
			GameObject equipped = this.ParentObject.Equipped;
			if (this.ParentObject.CurrentCell == null)
			{
				if (equipped == null)
				{
					return;
				}
				list = equipped.CurrentCell.GetLocalAdjacentCells();
			}
			else
			{
				list = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
			}
			bool flag = !OnlyIfCreatureNearby;
			if (!flag && list != null)
			{
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasObjectWithPart("Brain"))
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag && !list.IsNullOrEmpty<Cell>())
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_gasMutation_activeRelease", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (this.ParentObject.CurrentCell != null)
				{
					this.ParentObject.ParticleBlip("&O*", 10, 0L, false);
				}
				if (equipped != null && equipped.IsPlayer() && list[0].ParentZone.ResolveZoneWorld() == "NorthSheva")
				{
					Achievement.KLANQ_SUN.Unlock();
				}
				for (int i = 0; i < list.Count; i++)
				{
					GameObject gameObject = list[i].AddObject("FungalSporeGasPuff", null, null, null, null);
					gameObject.GetPart<Gas>().ColorString = this.ColorString;
					gameObject.GetPart<GasFungalSpores>().Infection = "PaxInfection";
					if (this.ParentObject.Equipped != null)
					{
						gameObject.GetPart<Gas>().Creator = this.ParentObject.Equipped;
					}
					else
					{
						gameObject.GetPart<Gas>().Creator = this.ParentObject;
					}
				}
			}
		}

		// Token: 0x04004455 RID: 17493
		public string ColorString = "&O";

		// Token: 0x04004456 RID: 17494
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
