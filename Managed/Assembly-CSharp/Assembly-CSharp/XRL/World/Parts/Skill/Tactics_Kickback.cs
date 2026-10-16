using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A8 RID: 5032
	[Serializable]
	public class Tactics_Kickback : BaseSkill
	{
		// Token: 0x0600C280 RID: 49792 RVA: 0x00470F92 File Offset: 0x0046F192
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeFireMissileWeaponsEvent>.ID;
		}

		// Token: 0x0600C281 RID: 49793 RVA: 0x00470FA8 File Offset: 0x0046F1A8
		public override bool HandleEvent(BeforeFireMissileWeaponsEvent E)
		{
			if (E.Actor == this.ParentObject && GameObject.Validate(E.ApparentTarget))
			{
				Cell currentCell = E.Actor.CurrentCell;
				if (currentCell != null && currentCell.IsAdjacentTo(E.ApparentTarget.CurrentCell, true) && (E.Actor.HasBodyPart("Feet") || E.Actor.GetBodyPartCount("Foot") >= 2) && E.ApparentTarget.IsCombatObject() && E.Actor.FlightCanReach(E.ApparentTarget))
				{
					if (!E.Actor.PhaseMatches(E.ApparentTarget) || E.ApparentTarget.GetMatterPhase() != 1)
					{
						if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You kick at ",
								E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", but the kick passes through ",
								E.ApparentTarget.them,
								"."
							}), null, true);
						}
						else if (E.ApparentTarget.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " at you, but the kick passes through you.", null, true);
						}
						else if (E.ApparentTarget.IsVisible() && E.Actor.IsVisible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" at ",
								E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", but the kick passes through ",
								E.ApparentTarget.them,
								"."
							}), null, true);
						}
					}
					else if (!E.ApparentTarget.CanBeInvoluntarilyMoved() || E.ApparentTarget.MakeSave("Strength", 15, E.Actor, null, "Kickback Knockback", false, false, false, false, null) || !E.ApparentTarget.Push(E.Actor.CurrentCell.GetDirectionFromCell(E.ApparentTarget.CurrentCell, false), E.Actor.Stat("Strength", 0) * 50, 4, false, true))
					{
						E.ApparentTarget.PlayWorldSound("sfx_characterTrigger_kickback", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"You kick at ",
								E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", but ",
								E.ApparentTarget.does("hold", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
								" ",
								E.ApparentTarget.its,
								" ground."
							}), null, true);
						}
						else if (E.ApparentTarget.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " at you, but you hold your ground.", null, true);
						}
						if (E.ApparentTarget.IsVisible() && E.Actor.IsVisible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" at ",
								E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								", but ",
								E.ApparentTarget.does("hold", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
								" ",
								E.ApparentTarget.its,
								" ground."
							}), null, true);
						}
					}
					else
					{
						E.ApparentTarget.PlayWorldSound("sfx_characterTrigger_kickback", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						E.ApparentTarget.DustPuff("&y", 15);
						if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You kick " + E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " backwards.", null, true);
						}
						else if (E.ApparentTarget.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you backwards.", null, true);
						}
						else if (E.ApparentTarget.IsVisible() && E.Actor.IsVisible())
						{
							IComponent<GameObject>.AddPlayerMessage(E.Actor.Does("kick", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + E.ApparentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " backwards.", null, true);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C282 RID: 49794 RVA: 0x00471612 File Offset: 0x0046F812
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C283 RID: 49795 RVA: 0x00471618 File Offset: 0x0046F818
		public override bool AddSkill(GameObject Object)
		{
			Brain brain = Object.Brain;
			bool? flag = ((brain != null) ? new bool?(brain.PointBlankRange) : null);
			if (flag != null)
			{
				if (flag.GetValueOrDefault())
				{
					Object.ModIntProperty("HadPointBlankRangeBeforeKickback", 1, false);
				}
				else
				{
					Object.Brain.PointBlankRange = true;
				}
			}
			return base.AddSkill(Object);
		}

		// Token: 0x0600C284 RID: 49796 RVA: 0x0047167A File Offset: 0x0046F87A
		public override bool RemoveSkill(GameObject Object)
		{
			if (!Object.HasProperty("HadPointBlankRangeBeforeKickback") && Object.Brain != null)
			{
				Object.Brain.PointBlankRange = false;
			}
			return base.RemoveSkill(Object);
		}
	}
}
