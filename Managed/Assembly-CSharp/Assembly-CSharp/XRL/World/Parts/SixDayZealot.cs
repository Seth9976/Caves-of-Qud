using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E8C RID: 3724
	[Serializable]
	public class SixDayZealot : IPart
	{
		// Token: 0x060095F4 RID: 38388 RVA: 0x003648F7 File Offset: 0x00362AF7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060095F5 RID: 38389 RVA: 0x003648FA File Offset: 0x00362AFA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060095F6 RID: 38390 RVA: 0x003648FD File Offset: 0x00362AFD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUseEarly");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095F7 RID: 38391 RVA: 0x00364928 File Offset: 0x00362B28
		public void ZealotDeclaim(GameObject who, bool Dialog)
		{
			if (!Dialog && !this.ParentObject.IsAudible(IComponent<GameObject>.ThePlayer, 20))
			{
				return;
			}
			if (this.ParentObject.IsFrozen())
			{
				base.EmitMessage("The zealot mumbles inaudibly, encased in ice.", ' ', Dialog, false, false, null, null);
				return;
			}
			string text = null;
			int num = Stat.Random(1, 4);
			if (num == 1)
			{
				text = "Make an offering at the Argent Well! Pay homage to your Fathers!";
			}
			else if (num == 2)
			{
				text = "Cast down your artifacts! You are not worthy of their make!";
			}
			else if (num == 3)
			{
				text = "Piety compels you to deliver your sacred relics to the priests in the cathedral! Cleanse them of your filth!";
			}
			else if (num == 4)
			{
				text = "The Machine commands that you exorcise robots and bring their sacred husks here!";
			}
			IComponent<GameObject>.EmitMessage(who ?? this.ParentObject, "The zealot yells {{W|'" + text + "'}}", ' ', Dialog, false, false, null, null);
			if (!Dialog)
			{
				this.ParentObject.ParticleText("{{W|" + text + "}}", true);
			}
		}

		// Token: 0x060095F8 RID: 38392 RVA: 0x003649EB File Offset: 0x00362BEB
		public void ZealotDeclaim(bool Dialog)
		{
			this.ZealotDeclaim(null, Dialog);
		}

		// Token: 0x060095F9 RID: 38393 RVA: 0x003649F8 File Offset: 0x00362BF8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				if (ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
				{
					if (E.GetGameObjectParameter("User").IsPlayer())
					{
						if (!this.ParentObject.IsPlayerLed())
						{
							return false;
						}
					}
					else if (this.ParentObject.InActiveZone())
					{
						return false;
					}
				}
			}
			else if (E.ID == "CommandSmartUseEarly")
			{
				if (ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("User");
					if (gameObjectParameter.IsPlayer())
					{
						if (!this.ParentObject.IsPlayerLed())
						{
							this.ZealotDeclaim(gameObjectParameter, true);
						}
					}
					else if (this.ParentObject.InActiveZone())
					{
						this.ZealotDeclaim(false);
					}
				}
			}
			else if (E.ID == "BeginTakeAction" && this.ParentObject.InActiveZone() && IComponent<GameObject>.Visible(this.ParentObject))
			{
				this.LastTalk--;
				if (this.LastTalk < 0 && Stat.Random(0, this.QuietTimer) == 0)
				{
					this.LastTalk = this.OriginalLastTalk;
					this.ZealotDeclaim(false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E03 RID: 15875
		public int OriginalLastTalk;

		// Token: 0x04003E04 RID: 15876
		public int LastTalk;

		// Token: 0x04003E05 RID: 15877
		public int QuietTimer = 55;
	}
}
