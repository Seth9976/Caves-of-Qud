using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D86 RID: 3462
	[Serializable]
	public class ShatterArmor : IShatterEffect, ITierInitialized
	{
		// Token: 0x06008999 RID: 35225 RVA: 0x00313079 File Offset: 0x00311279
		public ShatterArmor()
		{
			this.DisplayName = "{{r|cleaved}}";
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x00313093 File Offset: 0x00311293
		public ShatterArmor(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600899B RID: 35227 RVA: 0x003130A2 File Offset: 0x003112A2
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
			this.Duration = 300;
			this.AVPenalty = Stat.Random(1, 10);
		}

		// Token: 0x0600899C RID: 35228 RVA: 0x003130D8 File Offset: 0x003112D8
		public override int GetEffectType()
		{
			return 117441536;
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x003130E0 File Offset: 0x003112E0
		public override bool SameAs(Effect e)
		{
			ShatterArmor shatterArmor = e as ShatterArmor;
			return shatterArmor.AVPenalty == this.AVPenalty && shatterArmor.Owner == this.Owner && base.SameAs(e);
		}

		// Token: 0x0600899E RID: 35230 RVA: 0x0031311B File Offset: 0x0031131B
		public override string GetDescription()
		{
			return "{{r|cleaved ({{C|-" + this.AVPenalty.ToString() + " AV}})}}";
		}

		// Token: 0x0600899F RID: 35231 RVA: 0x00313137 File Offset: 0x00311337
		public override string GetStateDescription()
		{
			return "{{r|cleaved}}";
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x0031313E File Offset: 0x0031133E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x00313141 File Offset: 0x00311341
		public override string GetDetails()
		{
			return "-" + this.AVPenalty.ToString() + " AV";
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x0031315D File Offset: 0x0031135D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x0031317B File Offset: 0x0031137B
		public override bool HandleEvent(IsRepairableEvent E)
		{
			return false;
		}

		// Token: 0x060089A4 RID: 35236 RVA: 0x0031317E File Offset: 0x0031137E
		public override bool HandleEvent(RepairedEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x060089A5 RID: 35237 RVA: 0x00313195 File Offset: 0x00311395
		public override int GetPenalty()
		{
			return this.AVPenalty;
		}

		// Token: 0x060089A6 RID: 35238 RVA: 0x0031319D File Offset: 0x0031139D
		public override void IncrementPenalty()
		{
			this.AVPenalty++;
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x003131AD File Offset: 0x003113AD
		public override GameObject GetOwner()
		{
			return this.Owner;
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x003131B5 File Offset: 0x003113B5
		public override void SetOwner(GameObject Owner)
		{
			this.Owner = Owner;
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x003131C0 File Offset: 0x003113C0
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				bool flag = Object.BaseStat("AV", 0) > 0;
				List<GameObject> list = Event.NewGameObjectList();
				Body body = Object.Body;
				if (body != null)
				{
					foreach (BodyPart bodyPart in body.GetEquippedParts())
					{
						if (bodyPart.Equipped != null)
						{
							Armor part = bodyPart.Equipped.GetPart<Armor>();
							if (part != null && part.AV > 0)
							{
								list.Add(bodyPart.Equipped);
							}
						}
						else if (flag && bodyPart.Contact && !bodyPart.Abstract && !bodyPart.Extrinsic)
						{
							list.Add(null);
						}
					}
				}
				GameObject randomElement = list.GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					randomElement.ApplyEffect(new ShatteredArmor(this.AVPenalty, this.Duration), null);
					return false;
				}
				if (!flag)
				{
					return false;
				}
			}
			else if (Object.Stat("AV", 0) <= 0)
			{
				return false;
			}
			if (Object.Energy == null)
			{
				return false;
			}
			ShatterArmor effect = Object.GetEffect<ShatterArmor>();
			if (effect != null)
			{
				Object.PlayWorldSound("breakage", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
				}
				effect.UnapplyStats();
				effect.AVPenalty += this.AVPenalty;
				effect.ApplyStats();
				Object.ParticleText("*cleave (-" + effect.AVPenalty.ToString() + " AV)*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
				return false;
			}
			if (!Object.FireEvent("ApplyShatterArmor"))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_physicalRupture", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.ApplyStats();
			Object.ParticleText("*cleave (-" + this.AVPenalty.ToString() + " AV)*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			return true;
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x003133F8 File Offset: 0x003115F8
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x00313400 File Offset: 0x00311600
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("AV", -this.AVPenalty, false);
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x0031341B File Offset: 0x0031161B
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x00313428 File Offset: 0x00311628
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x00313448 File Offset: 0x00311648
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x00313484 File Offset: 0x00311684
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 40)
				{
					E.Tile = null;
					E.RenderString = "X";
					E.ColorString = "&B^c";
				}
			}
			return true;
		}

		// Token: 0x04003A81 RID: 14977
		public int AVPenalty = 1;

		// Token: 0x04003A82 RID: 14978
		public GameObject Owner;
	}
}
