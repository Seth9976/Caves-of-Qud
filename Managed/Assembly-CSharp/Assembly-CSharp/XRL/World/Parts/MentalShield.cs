using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DC4 RID: 3524
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[Serializable]
	public class MentalShield : IPart
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06008E65 RID: 36453 RVA: 0x00330DAE File Offset: 0x0032EFAE
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return MentalShield.MentalShieldPool;
			}
		}

		// Token: 0x06008E66 RID: 36454 RVA: 0x00330DB5 File Offset: 0x0032EFB5
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
		}

		// Token: 0x06008E67 RID: 36455 RVA: 0x00330DB7 File Offset: 0x0032EFB7
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
		}

		// Token: 0x06008E68 RID: 36456 RVA: 0x00330DB9 File Offset: 0x0032EFB9
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06008E69 RID: 36457 RVA: 0x00330DBC File Offset: 0x0032EFBC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == CanApplyEffectEvent.ID || ID == PooledEvent<CanReceiveEmpathyEvent>.ID || ID == PooledEvent<CanReceiveTelepathyEvent>.ID || ID == BeginMentalDefendEvent.ID;
		}

		// Token: 0x06008E6A RID: 36458 RVA: 0x00330DFC File Offset: 0x0032EFFC
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (!MentalShield.Disabled && E.Object == this.ParentObject && E.Damage.HasAttribute("Mental"))
			{
				if (!E.Indirect && E.Actor != null && E.Actor.IsPlayer())
				{
					if (E.WillUseOutcomeMessageFragment)
					{
						E.OutcomeMessageFragment = ", but your mental attack has no effect";
					}
					else
					{
						IComponent<GameObject>.AddPlayerMessage("Your mental attack does not affect " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
					}
				}
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E6B RID: 36459 RVA: 0x00330EDE File Offset: 0x0032F0DE
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06008E6C RID: 36460 RVA: 0x00330EF2 File Offset: 0x0032F0F2
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06008E6D RID: 36461 RVA: 0x00330F06 File Offset: 0x0032F106
		public override bool HandleEvent(CanReceiveTelepathyEvent E)
		{
			return MentalShield.Disabled && base.HandleEvent(E);
		}

		// Token: 0x06008E6E RID: 36462 RVA: 0x00330F18 File Offset: 0x0032F118
		public override bool HandleEvent(CanReceiveEmpathyEvent E)
		{
			return MentalShield.Disabled && base.HandleEvent(E);
		}

		// Token: 0x06008E6F RID: 36463 RVA: 0x00330F2C File Offset: 0x0032F12C
		public override bool HandleEvent(BeginMentalDefendEvent E)
		{
			if (!MentalShield.Disabled && E.Psionic && E.Defender == this.ParentObject)
			{
				if (E.Attacker.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your mental attack does not affect " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E70 RID: 36464 RVA: 0x00330FAC File Offset: 0x0032F1AC
		private bool Check(IEffectCheckEvent E)
		{
			return (MentalShield.Disabled || (!(E.Name == "Beguile") && !(E.Name == "Confusion") && !(E.Name == "Domination") && !(E.Name == "Shaken") && !(E.Name == "Shamed") && !(E.Name == "ShatterMentalArmor") && !(E.Name == "Terrified"))) && base.HandleEvent(E);
		}

		// Token: 0x04003B72 RID: 15218
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool MentalShieldPool = new IPartPool();

		// Token: 0x04003B73 RID: 15219
		public static bool Disabled = false;
	}
}
