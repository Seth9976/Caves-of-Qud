using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001438 RID: 5176
	[Serializable]
	public class FrostWebs : BaseMutation
	{
		// Token: 0x0600CA39 RID: 51769 RVA: 0x0049A13E File Offset: 0x0049833E
		public FrostWebs()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600CA3A RID: 51770 RVA: 0x0049A151 File Offset: 0x00498351
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CA3B RID: 51771 RVA: 0x0049A154 File Offset: 0x00498354
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CA3C RID: 51772 RVA: 0x0049A172 File Offset: 0x00498372
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 12 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandFrostWebs", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA3D RID: 51773 RVA: 0x0049A1A5 File Offset: 0x004983A5
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA3E RID: 51774 RVA: 0x0049A1C8 File Offset: 0x004983C8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStuck");
			Registrar.Register("CommandFrostWebs");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA3F RID: 51775 RVA: 0x0049A1E8 File Offset: 0x004983E8
		public override string GetDescription()
		{
			return "You fill a nearby area with frosty webs.\n\n" + "Range: 12\n" + "Area: 3x3\n" + "Cooldown: 30 rounds\n";
		}

		// Token: 0x0600CA40 RID: 51776 RVA: 0x0049A20D File Offset: 0x0049840D
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600CA41 RID: 51777 RVA: 0x0049A214 File Offset: 0x00498414
		public int GetCooldown()
		{
			return 30;
		}

		// Token: 0x0600CA42 RID: 51778 RVA: 0x0049A218 File Offset: 0x00498418
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", 12, false, 0);
			stats.Set("Area", "3x3", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(), 0);
		}

		// Token: 0x0600CA43 RID: 51779 RVA: 0x0049A258 File Offset: 0x00498458
		public void FrostWeb(List<Cell> Cells)
		{
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			XRLCore.Core.RenderMapToBuffer(scrapBuffer);
			foreach (Cell cell in Cells)
			{
				if (80.in100())
				{
					GameObject gameObject = GameObject.Create("FrostWeb");
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.DotPuff("&C");
				}
			}
			IComponent<GameObject>.XDidY(this.ParentObject, "shoot", "a swatch of frost webs", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0600CA44 RID: 51780 RVA: 0x0049A314 File Offset: 0x00498514
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyStuck")
			{
				return false;
			}
			if (E.ID == "CommandFrostWebs")
			{
				List<Cell> list = base.PickBurst(1, 12, false, AllowVis.OnlyVisible, "Knit Frosty Webs");
				if (list == null)
				{
					return false;
				}
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.DistanceTo(this.ParentObject) > 13)
						{
							if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("That is out of range! (12 squares)", true, true, true);
							}
							return false;
						}
					}
				}
				if (list == null)
				{
					return false;
				}
				base.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_spinnerets_webDrop", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				base.UseEnergy(1000, "Physical Mutation Frost Webs");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, null, false);
				this.FrostWeb(list);
			}
			IL_00F2:
			return base.FireEvent(E);
		}

		// Token: 0x0600CA45 RID: 51781 RVA: 0x0049A42C File Offset: 0x0049862C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA46 RID: 51782 RVA: 0x0049A438 File Offset: 0x00498638
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Knit Frosty Webs", "CommandFrostWebs", "Mental Mutations", null, "#", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA47 RID: 51783 RVA: 0x0049A481 File Offset: 0x00498681
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
