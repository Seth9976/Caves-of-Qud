using System;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020010F4 RID: 4340
	[Serializable]
	public class SapOnPenetration : IPart
	{
		// Token: 0x0600ABE2 RID: 44002 RVA: 0x003F0036 File Offset: 0x003EE236
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == GetDifficultyEvaluationEvent.ID;
		}

		// Token: 0x0600ABE3 RID: 44003 RVA: 0x003F004C File Offset: 0x003EE24C
		public override bool HandleEvent(GetDifficultyEvaluationEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.Actor.HasStat(this.Stat))
			{
				E.MinimumRating(5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ABE4 RID: 44004 RVA: 0x003F007C File Offset: 0x003EE27C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABE5 RID: 44005 RVA: 0x003F007F File Offset: 0x003EE27F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("AttackerHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABE6 RID: 44006 RVA: 0x003F00A0 File Offset: 0x003EE2A0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit")
			{
				if (E.GetIntParameter("Penetrations", 0) > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
					GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
					GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
					if (GameObject.Validate(ref gameObjectParameter2) && gameObjectParameter2.HasStat(this.Stat))
					{
						GameObject gameObject = gameObjectParameter;
						GameObject gameObject2 = gameObjectParameter3;
						string text = "Part SapOnPenetration Activation";
						GameObject gameObject3 = gameObjectParameter2;
						GameObject gameObject4 = gameObjectParameter4;
						if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100() && (!this.NaturalOnly || gameObjectParameter3 == null || gameObjectParameter3.IsNatural()))
						{
							E.SetFlag("DidSpecialEffect", true);
							string text2 = E.GetStringParameter("Properties", null) ?? "";
							if (!text2.Contains("DrainedStat"))
							{
								E.SetParameter("Properties", (text2 == "") ? "DrainedStat" : (text2 + ",DrainedStat"));
							}
						}
					}
				}
			}
			else if (E.ID == "AttackerAfterDamage")
			{
				GameObject gameObjectParameter5 = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter6 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter7 = E.GetGameObjectParameter("Weapon");
				if ((E.GetStringParameter("Properties", null) ?? "").Contains("DrainedStat"))
				{
					int num = XRL.Rules.Stat.Roll(this.Amount, null);
					string text3 = ((num != 1) ? "points" : "point");
					bool flag = !this.ShownPopup && gameObjectParameter6.IsPlayerControlled();
					gameObjectParameter6.GetStat(this.Stat).BaseValue -= num;
					base.DidXToY("permanently drain", gameObjectParameter6, string.Concat(new string[]
					{
						Statistic.GetStatNarration(this.Stat),
						" by ",
						num.ToString(),
						" ",
						text3
					}), "!", null, null, null, gameObjectParameter6, false, false, false, false, true, null, null, null, false, false, false, false, flag, null);
					E.SetFlag("DidSpecialEffect", true);
					if (!gameObjectParameter6.IsHostileTowards(gameObjectParameter5))
					{
						gameObjectParameter6.AddOpinion<OpinionAttack>(gameObjectParameter5, gameObjectParameter7, 1f);
					}
					this.ShownPopup = this.ShownPopup || flag;
					if (gameObjectParameter6.IsPlayer())
					{
						gameObjectParameter6.PlayWorldSound("sfx_characterMod_generic_negative_gain", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						AutoAct.Interrupt(gameObjectParameter5, true, true);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004719 RID: 18201
		public int Chance = 100;

		// Token: 0x0400471A RID: 18202
		public string Stat = "Hitpoints";

		// Token: 0x0400471B RID: 18203
		public string Amount = "1-2";

		// Token: 0x0400471C RID: 18204
		public bool NaturalOnly = true;

		// Token: 0x0400471D RID: 18205
		[NonSerialized]
		private bool ShownPopup;
	}
}
