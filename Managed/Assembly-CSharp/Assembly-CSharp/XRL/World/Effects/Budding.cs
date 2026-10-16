using System;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C42 RID: 3138
	[Serializable]
	public class Budding : Effect, ITierInitialized
	{
		// Token: 0x06008008 RID: 32776 RVA: 0x002F2A3F File Offset: 0x002F0C3F
		public Budding()
		{
			this.Duration = 20;
			this.DisplayName = "{{r|budding}}";
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x002F2A74 File Offset: 0x002F0C74
		public Budding(GameObject Actor = null, int numClones = 1, string ReplicationContext = "Budding")
			: this()
		{
			this.ActorID = ((Actor != null) ? Actor.ID : null);
			this.numClones = numClones;
			this.ReplicationContext = ReplicationContext;
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x002F2A9C File Offset: 0x002F0C9C
		public Budding(string ActorID = null, int numClones = 1, string ReplicationContext = "Budding")
			: this()
		{
			this.ActorID = ActorID;
			this.numClones = numClones;
			this.ReplicationContext = ReplicationContext;
		}

		// Token: 0x0600800B RID: 32779 RVA: 0x002F2AB9 File Offset: 0x002F0CB9
		public void Initialize(int Tier)
		{
			this.numClones = Stat.Random(1, 3);
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x002F2AC8 File Offset: 0x002F0CC8
		public override int GetEffectType()
		{
			return 67108880;
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x002F2ACF File Offset: 0x002F0CCF
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x002F2AD2 File Offset: 0x002F0CD2
		public override string GetDescription()
		{
			return "{{r|budding}}";
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x002F2AD9 File Offset: 0x002F0CD9
		public override string GetStateDescription()
		{
			return "{{r|about to bud}}";
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x002F2AE0 File Offset: 0x002F0CE0
		public override string GetDetails()
		{
			return "Will spawn a clone soon.";
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x002F2AE8 File Offset: 0x002F0CE8
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Budding>())
			{
				return false;
			}
			if (!Cloning.CanBeCloned(Object, GameObject.FindByID(this.ActorID), this.ReplicationContext, false))
			{
				return false;
			}
			Body body = Object.Body;
			BodyPart bodyPart = ((body != null) ? body.GetFirstPart("Back") : null);
			if (base.Visible())
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_budding", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (bodyPart != null)
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"A grotesque protuberance swells from ",
						Object.poss(bodyPart.GetOrdinalName(), true),
						" as ",
						Object.does("begin", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
						" to bud!"
					}), null, true);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"A grotesque protuberance swells from ",
						Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" as ",
						Object.does("begin", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
						" to bud!"
					}), null, true);
				}
			}
			return true;
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x002F2C5C File Offset: 0x002F0E5C
		public override void Remove(GameObject Object)
		{
			if (this.numClones > 0)
			{
				Body body = Object.Body;
				BodyPart bodyPart = ((body != null) ? body.GetFirstPart("Back") : null);
				if (base.Visible())
				{
					if (bodyPart != null)
					{
						IComponent<GameObject>.AddPlayerMessage("The grotesque protuberance on " + Object.poss(bodyPart.GetOrdinalName(), true) + " subsides.", null, true);
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage(Object.Poss("grotesque protuberance", true) + " subsides.", null, true);
					}
				}
			}
			base.Remove(Object);
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x002F2CE0 File Offset: 0x002F0EE0
		public override bool WantEvent(int ID, int cascade)
		{
			if (!base.WantEvent(ID, cascade))
			{
				if (ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID)
				{
					GameObject @object = base.Object;
					if (((@object != null) ? @object.Brain : null) != null)
					{
						return true;
					}
				}
				if (ID == SingletonEvent<EndTurnEvent>.ID)
				{
					GameObject object2 = base.Object;
					return ((object2 != null) ? object2.Brain : null) == null;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x002F2D34 File Offset: 0x002F0F34
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			GameObject @object = base.Object;
			if (((@object != null) ? @object.Brain : null) != null)
			{
				this.ProcessTurn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008015 RID: 32789 RVA: 0x002F2D57 File Offset: 0x002F0F57
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject @object = base.Object;
			if (((@object != null) ? @object.Brain : null) == null)
			{
				this.ProcessTurn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x002F2D7A File Offset: 0x002F0F7A
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x002F2DA4 File Offset: 0x002F0FA4
		public void ProcessTurn()
		{
			GameObject @object = base.Object;
			Zone zone = ((@object != null) ? @object.CurrentZone : null);
			if (zone != null && zone.IsActive() && !zone.IsWorldMap() && this.Duration > 0 && this.Duration != 9999)
			{
				this.Duration--;
				if (this.Duration <= 0 && Cloning.CanBeCloned(base.Object, null, this.ReplicationContext, false))
				{
					base.Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_budding_clone_complete", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					if (Cloning.GenerateBuddedClone(base.Object, GameObject.FindByID(this.ActorID), false, true, 1, this.ReplicationContext) != null)
					{
						this.numClones--;
						if (this.numClones > 0)
						{
							this.Duration = this.baseDuration;
							return;
						}
					}
					else
					{
						this.Duration = 1;
					}
				}
			}
		}

		// Token: 0x040038E7 RID: 14567
		public const string DEFAULT_REPLICATION_CONTEXT = "Budding";

		// Token: 0x040038E8 RID: 14568
		public int numClones = 1;

		// Token: 0x040038E9 RID: 14569
		public int baseDuration = 20;

		// Token: 0x040038EA RID: 14570
		public string ActorID;

		// Token: 0x040038EB RID: 14571
		public string ReplicationContext = "Budding";
	}
}
