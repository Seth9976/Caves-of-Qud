using System;
using System.Collections.Generic;
using XRL.Names;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A0F RID: 2575
	public class GenericHero : IPart
	{
		// Token: 0x06006D17 RID: 27927 RVA: 0x002484C2 File Offset: 0x002466C2
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x002484C5 File Offset: 0x002466C5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.Created);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x002484E8 File Offset: 0x002466E8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Created)
			{
				this.Created = true;
				try
				{
					string text = this.MakeTitle();
					this.ParentObject.GiveProperName(this.MakeName(), false, null, false, null, null, null, null);
					this.ParentObject.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
					if (!text.IsNullOrEmpty())
					{
						this.ParentObject.RequirePart<Titles>(false).AddTitle(text, -40);
					}
					this.AfterNameGeneration();
					this.CreateRetinue();
					this.GenerateEquipment();
					this.ModifyStats();
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("GenericHero setup", ex);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x002485AC File Offset: 0x002467AC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x002485AF File Offset: 0x002467AF
		public virtual void AfterNameGeneration()
		{
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x002485B1 File Offset: 0x002467B1
		public virtual List<ObjectRetinueEntry> GenerateRetinue()
		{
			return new List<ObjectRetinueEntry>();
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x002485B8 File Offset: 0x002467B8
		public virtual void EquipFollower(GameObject Follower)
		{
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x002485BC File Offset: 0x002467BC
		private void CreateRetinue()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			List<Cell> list = Event.NewCellList();
			foreach (Cell cell in currentCell.GetAdjacentCells(4, true, true))
			{
				if (cell.IsEmptyOfSolid(true))
				{
					list.Add(cell);
				}
			}
			foreach (ObjectRetinueEntry objectRetinueEntry in this.GenerateRetinue())
			{
				foreach (GameObject gameObject in objectRetinueEntry.Generate())
				{
					this.EquipFollower(gameObject);
					gameObject.SetAlliedLeader<AllyRetinue>(this.ParentObject, 0, false);
					Cell cell2;
					if ((cell2 = list.GetRandomElement<Cell>(null)) == null)
					{
						Zone parentZone = currentCell.ParentZone;
						cell2 = ((parentZone != null) ? parentZone.GetRandomCell(0) : null) ?? currentCell;
					}
					Cell cell3 = cell2;
					cell3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					list.Remove(cell3);
				}
			}
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x00248714 File Offset: 0x00246914
		public virtual string MakeName()
		{
			return NameMaker.MakeName(this.ParentObject, null, null, null, null, null, null, null, null, null, "Hero", null, null, false, true, null, null);
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x00248750 File Offset: 0x00246950
		public virtual string MakeTitle()
		{
			return NameMaker.MakeTitle(this.ParentObject, null, null, null, null, null, null, null, null, null, "Hero", null, true, null, null);
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x0024878A File Offset: 0x0024698A
		public virtual void GenerateEquipment()
		{
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x0024878C File Offset: 0x0024698C
		public virtual void ModifyStats()
		{
			this.ParentObject.GetStat("Hitpoints").BaseValue *= this.HitpointMultiplier;
		}

		// Token: 0x040031E0 RID: 12768
		public int HitpointMultiplier = 2;

		// Token: 0x040031E1 RID: 12769
		public string ExtraEquipment = "Junk 2R,Junk 3";

		// Token: 0x040031E2 RID: 12770
		public bool Created;
	}
}
