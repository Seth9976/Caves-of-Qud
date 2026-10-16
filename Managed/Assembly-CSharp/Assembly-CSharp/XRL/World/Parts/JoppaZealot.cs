using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E7D RID: 3709
	[Serializable]
	public class JoppaZealot : IPart
	{
		// Token: 0x060095AC RID: 38316 RVA: 0x003624D1 File Offset: 0x003606D1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060095AD RID: 38317 RVA: 0x003624D4 File Offset: 0x003606D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == SingletonEvent<CommandTakeActionEvent>.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID;
		}

		// Token: 0x060095AE RID: 38318 RVA: 0x00362504 File Offset: 0x00360704
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.Actor.IsPlayer() && !The.Game.HasQuest("O Glorious Shekhinah!") && E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, -1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095AF RID: 38319 RVA: 0x00362578 File Offset: 0x00360778
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			if (ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
			{
				if (E.Actor.IsPlayer())
				{
					if (The.Game.HasQuest("O Glorious Shekhinah!") && !this.ParentObject.IsPlayerLed())
					{
						return false;
					}
				}
				else if (this.ParentObject.InActiveZone())
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095B0 RID: 38320 RVA: 0x003625DC File Offset: 0x003607DC
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
			{
				if (E.Actor.IsPlayer())
				{
					if (The.Game.HasQuest("O Glorious Shekhinah!") && !this.ParentObject.IsPlayerLed())
					{
					}
				}
				else
				{
					this.ParentObject.InActiveZone();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095B1 RID: 38321 RVA: 0x0036263C File Offset: 0x0036083C
		public override bool HandleEvent(CommandTakeActionEvent E)
		{
			if (this.ParentObject.InActiveZone() && !this.ParentObject.IsPlayerLed())
			{
				this.LastTalk--;
				if (this.LastTalk < 0 && 55.in100())
				{
					this.LastTalk = 350;
					this.ZealotDeclaim(false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095B2 RID: 38322 RVA: 0x0036269C File Offset: 0x0036089C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060095B3 RID: 38323 RVA: 0x003626A0 File Offset: 0x003608A0
		public void ZealotDeclaim(GameObject who, bool Dialog)
		{
			if (!Dialog && !this.ParentObject.IsAudible(IComponent<GameObject>.ThePlayer, 80))
			{
				return;
			}
			if (this.ParentObject.IsFrozen())
			{
				return;
			}
			string text = null;
			int num = Stat.Random(1, 4);
			if (num == 1)
			{
				text = "Who ventures into the Great Salt Desert, and nearer the Six Day Stilt?";
			}
			else if (num == 2)
			{
				text = "Hmm, what of your artifacts? Make an offering of them to Shekhinah at the Sacred Well.";
			}
			else if (num == 3)
			{
				text = "The beauty! My stomach is in stirs.";
			}
			else if (num == 4)
			{
				text = "Is it a dybbuk that possesses the robot? It should be sacred and still.";
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (who == null)
				{
					who = this.ParentObject;
				}
				IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
				{
					who.The,
					who.ShortDisplayName,
					who.GetVerb("yell", true, false, false, true, false),
					", {{W|'",
					text,
					"'}}"
				}), ' ', Dialog, false, false, null, null);
				if (!Dialog)
				{
					this.ParentObject.ParticleText("{{W|" + text + "}}", (this.ParentObject.CurrentCell.X < 40) ? 0.4f : (-0.4f), (this.ParentObject.CurrentCell.Y < 12) ? 0.2f : (-0.2f), ' ', true);
				}
			}
		}

		// Token: 0x060095B4 RID: 38324 RVA: 0x003627D3 File Offset: 0x003609D3
		public void ZealotDeclaim(bool Dialog)
		{
			this.ZealotDeclaim(null, Dialog);
		}

		// Token: 0x04003DEA RID: 15850
		public int LastTalk;
	}
}
