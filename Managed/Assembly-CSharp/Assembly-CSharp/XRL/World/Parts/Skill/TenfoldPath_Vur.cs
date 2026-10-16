using System;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B6 RID: 5046
	[Serializable]
	public class TenfoldPath_Vur : BaseInitiatorySkill
	{
		// Token: 0x0600C2C6 RID: 49862 RVA: 0x00472181 File Offset: 0x00470381
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2C7 RID: 49863 RVA: 0x00472184 File Offset: 0x00470384
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectAttacking");
			Registrar.Register("TargetedForMissileWeapon");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C2C8 RID: 49864 RVA: 0x004721A4 File Offset: 0x004703A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectAttacking" || E.ID == "TargetedForMissileWeapon")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				if (gameObjectParameter != null && gameObjectParameter.FireEvent("CanApplyFear") && gameObjectParameter.FireEvent("ApplyFear") && !gameObjectParameter.MakeSave("Willpower", 15, null, null, "Vur Counteraggression Fear", false, false, false, false, null))
				{
					if (gameObjectParameter.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You cannot bring yourself to attack " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", 'r', true);
					}
					gameObjectParameter.UseEnergy(1000, "Attempted Attack", null, null, false);
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
