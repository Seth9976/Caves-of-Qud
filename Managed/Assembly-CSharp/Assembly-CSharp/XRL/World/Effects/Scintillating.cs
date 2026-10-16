using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D80 RID: 3456
	[Serializable]
	public class Scintillating : Effect, ITierInitialized
	{
		// Token: 0x06008935 RID: 35125 RVA: 0x00311B4F File Offset: 0x0030FD4F
		public Scintillating()
		{
			this.DisplayName = "{{rainbow|scintillating}}";
		}

		// Token: 0x06008936 RID: 35126 RVA: 0x00311B62 File Offset: 0x0030FD62
		public Scintillating(int Duration, int Level = 1)
			: this()
		{
			this.Duration = Duration;
			this.Level = Level;
		}

		// Token: 0x06008937 RID: 35127 RVA: 0x00311B78 File Offset: 0x0030FD78
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 2, Tier + 2);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Duration = 5;
			this.Level = (int)((double)Tier * 1.5);
		}

		// Token: 0x06008938 RID: 35128 RVA: 0x00311BAF File Offset: 0x0030FDAF
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008939 RID: 35129 RVA: 0x00311BB6 File Offset: 0x0030FDB6
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x00311BB9 File Offset: 0x0030FDB9
		public override string GetDescription()
		{
			return "{{rainbow|scintillating}}";
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x00311BC0 File Offset: 0x0030FDC0
		public override string GetDetails()
		{
			return "Cannot take actions.\nConfuses nearby hostile creatures per Confusion 7.";
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x00311BC7 File Offset: 0x0030FDC7
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x00311BCC File Offset: 0x0030FDCC
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Scintillating>())
			{
				return false;
			}
			if (!Object.FireEvent("ApplyScintillating"))
			{
				return false;
			}
			base.DidX("start", "scintillating in {{rainbow|prismatic hues}}", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			Object.ParticleText("*scintillating*", IComponent<GameObject>.ConsequentialColorChar(Object, null), false, 1.5f, -8f);
			Object.ForfeitTurn(false);
			return true;
		}

		// Token: 0x0600893E RID: 35134 RVA: 0x00311C3C File Offset: 0x0030FE3C
		public override void Remove(GameObject Object)
		{
			base.DidX("stop", "scintillating", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.Remove(Object);
		}

		// Token: 0x0600893F RID: 35135 RVA: 0x00311C6F File Offset: 0x0030FE6F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x00311C8D File Offset: 0x0030FE8D
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			this.ConfuseHostiles();
			return base.HandleEvent(E);
		}

		// Token: 0x06008941 RID: 35137 RVA: 0x00311C9C File Offset: 0x0030FE9C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return false;
		}

		// Token: 0x06008942 RID: 35138 RVA: 0x00311C9F File Offset: 0x0030FE9F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008943 RID: 35139 RVA: 0x00311CB4 File Offset: 0x0030FEB4
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "IsMobile") || this.Duration <= 0) && base.FireEvent(E);
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x00311CDC File Offset: 0x0030FEDC
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int currentFrame = XRLCore.CurrentFrame;
				string text;
				string text2;
				if (currentFrame < 5)
				{
					text = "&r";
					text2 = "R";
				}
				else if (currentFrame <= 10)
				{
					text = "&W";
					text2 = "w";
				}
				else if (currentFrame <= 15)
				{
					text = "&G";
					text2 = "g";
				}
				else if (currentFrame <= 20)
				{
					text = "&C";
					text2 = "c";
				}
				else if (currentFrame <= 25)
				{
					text = "&B";
					text2 = "b";
				}
				else if (currentFrame <= 35)
				{
					text = "&M";
					text2 = "m";
				}
				else if (currentFrame <= 40)
				{
					text = "&B";
					text2 = "b";
				}
				else if (currentFrame <= 45)
				{
					text = "&C";
					text2 = "c";
				}
				else if (currentFrame <= 50)
				{
					text = "&G";
					text2 = "g";
				}
				else if (currentFrame < 55)
				{
					text = "&W";
					text2 = "w";
				}
				else
				{
					text = "&r";
					text2 = "R";
				}
				if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty())
				{
					E.ApplyColors(text, text2, Effect.ICON_COLOR_PRIORITY, Effect.ICON_COLOR_PRIORITY);
				}
			}
			return true;
		}

		// Token: 0x06008945 RID: 35141 RVA: 0x00311DF4 File Offset: 0x0030FFF4
		public void ConfuseHostiles()
		{
			Zone currentZone = base.Object.CurrentZone;
			List<GameObject> list = ((currentZone != null) ? currentZone.FindObjectsWithPart("Brain") : null);
			if (list == null || list.Count == 0)
			{
				return;
			}
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject = list[i];
				if ((gameObject.IsHostileTowards(base.Object) || gameObject.IsHostileTowards(base.Object.PartyLeader)) && !this.HasAffected(gameObject) && gameObject.Brain.CheckVisibilityOf(base.Object))
				{
					int level = Confusion.GetConfusionLevel(this.Level);
					int penalty = Confusion.GetMentalPenalty(this.Level);
					if (base.PerformMentalAttack((MentalAttackEvent E) => Confusion.Confuse(E, false, level, penalty, false), base.Object, gameObject, null, "Confuse", "1d8", 4, Confusion.GetDuration(this.Level).RollCached(), -2147483648, this.Level, 0))
					{
						this.MarkAffected(gameObject);
					}
				}
				i++;
			}
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x00311F04 File Offset: 0x00310104
		public bool HasAffected(GameObject Object)
		{
			return this.Affected != null && Object.HasID && this.Affected.CachedCommaExpansion().Contains(Object.ID);
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x00311F30 File Offset: 0x00310130
		public void MarkAffected(GameObject Object)
		{
			if (this.Affected == null)
			{
				this.Affected = Object.ID;
				return;
			}
			this.Affected = this.Affected + "," + Object.ID;
		}

		// Token: 0x04003A71 RID: 14961
		public int Level;

		// Token: 0x04003A72 RID: 14962
		public string Affected;
	}
}
