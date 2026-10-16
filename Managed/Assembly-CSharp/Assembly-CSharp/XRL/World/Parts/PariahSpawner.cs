using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000EEC RID: 3820
	public class PariahSpawner : IPart
	{
		// Token: 0x060098A9 RID: 39081 RVA: 0x00376D2C File Offset: 0x00374F2C
		public override bool SameAs(IPart p)
		{
			return (p as PariahSpawner).DoesWander == this.DoesWander && base.SameAs(p);
		}

		// Token: 0x060098AA RID: 39082 RVA: 0x00376D4A File Offset: 0x00374F4A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060098AB RID: 39083 RVA: 0x00376D60 File Offset: 0x00374F60
		public override bool HandleEvent(EnteredCellEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			GameObject gameObject = PariahSpawner.GeneratePariah(-1, !this.InheritName, this.IsUnique);
			if (gameObject.Brain != null)
			{
				if (this.DoesWander)
				{
					gameObject.Brain.Wanders = true;
					gameObject.Brain.WandersRandomly = true;
					gameObject.RequirePart<AIShopper>(false);
				}
				else
				{
					gameObject.Brain.Wanders = false;
					gameObject.Brain.WandersRandomly = false;
					gameObject.RequirePart<AISitting>(false);
				}
				gameObject.MakeActive(false);
			}
			if (this.InheritName && gameObject.Render != null)
			{
				gameObject.Render.DisplayName = this.ParentObject.Render.DisplayName;
			}
			Description description;
			if (this.InheritDescription && gameObject.TryGetPart<Description>(out description))
			{
				Description description2 = description;
				GameObject parentObject = this.ParentObject;
				string text;
				if (parentObject == null)
				{
					text = null;
				}
				else
				{
					Description part = parentObject.GetPart<Description>();
					text = ((part != null) ? part._Short : null);
				}
				description2._Short = text ?? description._Short;
			}
			gameObject.SetStringProperty("SpawnedFrom", this.ParentObject.Blueprint, false);
			gameObject.FireEvent("VillageInit");
			gameObject.SetIntProperty("Social", 1, false);
			GameObjectFactory.ApplyBuilders(gameObject, this.ParentObject.GetBlueprint(true), null);
			currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.ParentObject.Destroy(null, false, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x060098AC RID: 39084 RVA: 0x00376EC3 File Offset: 0x003750C3
		public static GameObject GeneratePariah(int level = -1, bool AlterName = true, bool IsUnique = false)
		{
			return PariahSpawner.GeneratePariah((level == -1) ? EncountersAPI.GetACreature(null) : EncountersAPI.GetCreatureAroundLevel(level, null), AlterName, IsUnique);
		}

		// Token: 0x060098AD RID: 39085 RVA: 0x00376EDF File Offset: 0x003750DF
		public static GameObject GeneratePariah(GameObject pariah, bool AlterName = true, bool IsUnique = false)
		{
			Pariah.MakePariah(pariah, AlterName, IsUnique);
			return pariah;
		}

		// Token: 0x04003F91 RID: 16273
		public bool InheritName;

		// Token: 0x04003F92 RID: 16274
		public bool InheritDescription;

		// Token: 0x04003F93 RID: 16275
		public bool DoesWander = true;

		// Token: 0x04003F94 RID: 16276
		public bool IsUnique;
	}
}
