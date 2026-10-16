using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001397 RID: 5015
	[Serializable]
	public class Survival_Camp : BaseSkill
	{
		// Token: 0x0600C226 RID: 49702 RVA: 0x0046EBA3 File Offset: 0x0046CDA3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C227 RID: 49703 RVA: 0x0046EBC1 File Offset: 0x0046CDC1
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C228 RID: 49704 RVA: 0x0046EBE4 File Offset: 0x0046CDE4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Survival_Camp.COMMAND_NAME)
			{
				this.AttemptCamp(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C229 RID: 49705 RVA: 0x0046EC0C File Offset: 0x0046CE0C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C22A RID: 49706 RVA: 0x0046EC10 File Offset: 0x0046CE10
		public bool AttemptCamp(GameObject Actor)
		{
			if (Actor.AreHostilesNearby())
			{
				return Actor.Fail("You can't cook with hostiles nearby.");
			}
			if (Actor.OnWorldMap())
			{
				return Actor.Fail("You can't cook on the world map.");
			}
			if (!Actor.CanChangeMovementMode("Camping", true, false, true, false))
			{
				return false;
			}
			PointOfInterest one = GetPointsOfInterestEvent.GetOne(Actor, "PlayerCampfire", null);
			if (one != null && one.GetDistanceTo(Actor) <= 24)
			{
				GameObject @object = one.Object;
				if (@object != null)
				{
					if (Actor.IsPlayer())
					{
						if (one.IsAt(Actor))
						{
							Actor.Fail(string.Concat(new string[]
							{
								"There ",
								@object.Is,
								" already ",
								@object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								" here."
							}));
						}
						else if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							"There ",
							@object.Is,
							" already ",
							@object.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" ",
							Actor.DescribeDirectionToward(@object, false, false),
							". Do you want to go to ",
							@object.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
						{
							one.NavigateTo(Actor);
						}
					}
					return false;
				}
			}
			string text = base.PickDirectionS("Make Camp", null);
			if (text == null)
			{
				return false;
			}
			Cell cellFromDirection = Actor.CurrentCell.GetCellFromDirection(text, true);
			if (cellFromDirection == null || cellFromDirection.ParentZone != Actor.CurrentZone)
			{
				return Actor.Fail("You can only build a campfire in the same zone you are in.");
			}
			if (cellFromDirection.HasObjectWithTag("ExcavatoryTerrainFeature"))
			{
				return Actor.Fail("There is nothing there you can build a campfire on.");
			}
			if (!cellFromDirection.IsEmpty())
			{
				return Actor.Fail("Something is in the way!");
			}
			GameObject gameObject = Campfire.FindExtinguishingPool(cellFromDirection);
			if (gameObject != null)
			{
				return Actor.Fail("You cannot start a campfire in " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
			}
			cellFromDirection.PlayWorldSound("Sounds/Abilities/sfx_ability_makeCamp", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.XDidY(Actor, "make", "camp", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			GameObject gameObject2;
			if (cellFromDirection.ParentZone.ZoneID.StartsWith("ThinWorld"))
			{
				gameObject2 = cellFromDirection.AddObject("BlueCampfire", null, null, null, null);
			}
			else
			{
				gameObject2 = cellFromDirection.AddObject("Campfire", null, null, null, null);
			}
			if (Actor.IsPlayer())
			{
				gameObject2.SetIntProperty("PlayerCampfire", 1, false);
				gameObject2.SetStringProperty("PointOfInterestKey", "PlayerCampfire", false);
			}
			if (!this.CampedZones.Contains(Actor.CurrentZone.ZoneID))
			{
				this.CampedZones.Add(Actor.CurrentZone.ZoneID);
			}
			return true;
		}

		// Token: 0x0600C22B RID: 49707 RVA: 0x0046EF20 File Offset: 0x0046D120
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Make Camp", Survival_Camp.COMMAND_NAME, "Maneuvers", "Start a campfire for cooking meals and preserving foods. You can't make camp in combat.", "\u0006", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C22C RID: 49708 RVA: 0x0046EF6C File Offset: 0x0046D16C
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.AddSkill(GO);
		}

		// Token: 0x04004D94 RID: 19860
		public static readonly string COMMAND_NAME = "CommandSurvivalCamp";

		// Token: 0x04004D95 RID: 19861
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D96 RID: 19862
		public Guid StopActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D97 RID: 19863
		public List<string> CampedZones = new List<string>();
	}
}
