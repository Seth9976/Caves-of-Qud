using System;
using System.Collections.Generic;
using XRL.Messages;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132D RID: 4909
	[Serializable]
	public class Cudgel_Backswing : BaseSkill
	{
		// Token: 0x0600BFEA RID: 49130 RVA: 0x00464BD6 File Offset: 0x00462DD6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BFEB RID: 49131 RVA: 0x00464BD9 File Offset: 0x00462DD9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterAttack");
			Registrar.Register("AttackerMeleeMiss");
		}

		// Token: 0x0600BFEC RID: 49132 RVA: 0x00464BF4 File Offset: 0x00462DF4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterAttack" || E.ID == "AttackerMeleeMiss")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Defender");
				if (GameObject.Validate(ref gameObjectParameter2) && GameObject.Validate(ref gameObjectParameter3) && gameObjectParameter3.HasHitpoints())
				{
					long segments = The.Game.Segments;
					if (this.BackswingSegment != segments)
					{
						this.WeaponsUsed.Clear();
						this.BackswingSegment = segments;
					}
					if (!this.WeaponsUsed.Contains(gameObjectParameter2) && gameObjectParameter2.IsEquippedOrDefaultOfPrimary(gameObjectParameter))
					{
						this.WeaponsUsed.Add(gameObjectParameter2);
						MeleeWeapon part = gameObjectParameter2.GetPart<MeleeWeapon>();
						if (part != null && part.Skill.Contains("Cudgel") && GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, gameObjectParameter2, "Skill Backswing", 25, gameObjectParameter3, null, true, false).in100())
						{
							if (this.ParentObject.IsPlayer())
							{
								MessageQueue.AddPlayerMessage("You backswing with " + this.ParentObject.its_(gameObjectParameter2, false) + ".", 'g', true);
							}
							if (gameObjectParameter3.IsPlayer())
							{
								MessageQueue.AddPlayerMessage(string.Concat(new string[]
								{
									gameObjectParameter.The,
									gameObjectParameter.ShortDisplayName,
									gameObjectParameter.GetVerb("backswing", true, false, false, true, false),
									" with ",
									gameObjectParameter.its_(gameObjectParameter2, false),
									"."
								}), 'r', true);
							}
							Combat.MeleeAttackWithWeapon(gameObjectParameter, gameObjectParameter3, gameObjectParameter2, gameObjectParameter.Body.FindDefaultOrEquippedItem(gameObjectParameter2), null, 0, 0, 0, 0, 0, true, true);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004D3B RID: 19771
		[NonSerialized]
		private List<GameObject> WeaponsUsed = new List<GameObject>(1);

		// Token: 0x04004D3C RID: 19772
		[NonSerialized]
		private long BackswingSegment = -1L;
	}
}
