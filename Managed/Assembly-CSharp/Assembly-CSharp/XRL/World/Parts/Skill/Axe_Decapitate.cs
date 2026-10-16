using System;
using System.Collections.Generic;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131E RID: 4894
	[Serializable]
	public class Axe_Decapitate : BaseSkill
	{
		// Token: 0x0600BF5C RID: 48988 RVA: 0x0046173C File Offset: 0x0045F93C
		public override void Attach()
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				this.AddAbility();
			}
			base.Attach();
		}

		// Token: 0x0600BF5D RID: 48989 RVA: 0x0046175C File Offset: 0x0045F95C
		public static bool ShouldDecapitate(GameObject who)
		{
			Axe_Decapitate axe_Decapitate = ((who != null) ? who.GetPart<Axe_Decapitate>() : null);
			return axe_Decapitate != null && axe_Decapitate.ShouldDecapitate();
		}

		// Token: 0x0600BF5E RID: 48990 RVA: 0x00461781 File Offset: 0x0045F981
		public bool ShouldDecapitate()
		{
			return base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null);
		}

		// Token: 0x0600BF5F RID: 48991 RVA: 0x00461798 File Offset: 0x0045F998
		public static bool Decapitate(GameObject Attacker, GameObject Defender, Cell Where = null, BodyPart LostPart = null, GameObject Weapon = null, GameObject Projectile = null, bool weaponActing = false, bool UsePopups = false)
		{
			Body body = Defender.Body;
			if (body == null)
			{
				return false;
			}
			if (!Defender.CanBeDismembered(Weapon))
			{
				return false;
			}
			if (!Defender.FireEvent("BeforeDecapitate"))
			{
				return false;
			}
			if (LostPart == null)
			{
				List<BodyPart> list = new List<BodyPart>();
				foreach (BodyPart bodyPart in body.GetParts())
				{
					if (bodyPart.IsSeverable() && bodyPart.SeverRequiresDecapitate())
					{
						list.Add(bodyPart);
					}
				}
				LostPart = list.GetRandomElement<BodyPart>(null);
				if (LostPart == null)
				{
					return false;
				}
			}
			Defender.PlayWorldSound("sfx_characterTrigger_decapitate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (weaponActing && Weapon != null && Attacker != null)
			{
				IComponent<GameObject>.XDidYToZ(Weapon, "lop", "off", Defender, LostPart.GetOrdinalName(), "!", null, null, null, Defender, false, false, false, false, true, Attacker, null, null, false, false, false, false, UsePopups, null);
			}
			else
			{
				IComponent<GameObject>.XDidYToZ(Attacker, "lop", "off", Defender, LostPart.GetOrdinalName(), "!", null, null, null, Defender, false, false, false, false, true, null, null, null, false, false, false, false, UsePopups, null);
			}
			if (LostPart.Type == "Head")
			{
				Defender.ParticleText("*decapitated!*", IComponent<GameObject>.ConsequentialColorChar(null, Defender), false, 1.5f, -8f);
				if (Defender.IsPlayer())
				{
					Achievement.GET_DECAPITATED.Unlock();
				}
			}
			else
			{
				Defender.ParticleText("*dismembered!*", IComponent<GameObject>.ConsequentialColorChar(null, Defender), false, 1.5f, -8f);
			}
			body.Dismember(LostPart, Attacker, Where, false, UsePopups, null);
			Defender.ApplyEffect(new Bleeding("1d2+1", 35, Attacker, true, false, UsePopups, false), null);
			Defender.RemoveAllEffects<Hooked>(true);
			if (((Attacker != null && Attacker.IsPlayer()) || (Defender != null && Defender.IsPlayer())) && CombatJuice.enabled)
			{
				CombatJuice.cameraShake(0.5f, false);
			}
			if (body.AnyDismemberedMortalParts() && !body.AnyMortalParts())
			{
				Defender.Die(Attacker, null, string.Concat(new string[]
				{
					"You were ",
					(LostPart.Type == "Head") ? "decapitated" : "relieved of your vital anatomy",
					" by ",
					Attacker.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), string.Concat(new string[]
				{
					Defender.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
					" @@",
					(LostPart.Type == "Head") ? "decapitated" : ("relieved of " + Defender.its + " vital anatomy"),
					" by ",
					Attacker.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), false, Weapon, Projectile, false, false, null, null, null);
			}
			return true;
		}

		// Token: 0x0600BF60 RID: 48992 RVA: 0x00461AD0 File Offset: 0x0045FCD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600BF61 RID: 48993 RVA: 0x00461AE8 File Offset: 0x0045FCE8
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandToggleDecapitate")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF62 RID: 48994 RVA: 0x00461B26 File Offset: 0x0045FD26
		public override bool AddSkill(GameObject Object)
		{
			this.AddAbility();
			return base.AddSkill(Object);
		}

		// Token: 0x0600BF63 RID: 48995 RVA: 0x00461B35 File Offset: 0x0045FD35
		public override bool RemoveSkill(GameObject Object)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(Object);
		}

		// Token: 0x0600BF64 RID: 48996 RVA: 0x00461B4C File Offset: 0x0045FD4C
		public void AddAbility()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Decapitate", "CommandToggleDecapitate", "Skills", "Toggles whether you will attempt to decapitate opponents when dismembering them.", "ö", null, true, true, true, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x04004D17 RID: 19735
		public Guid ActivatedAbilityID;
	}
}
