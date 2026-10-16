using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001445 RID: 5189
	[Serializable]
	public class HeightenedHearing : BaseMutation
	{
		// Token: 0x0600CA99 RID: 51865 RVA: 0x0049B137 File Offset: 0x00499337
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ExtraHostilePerceptionEvent>.ID;
		}

		// Token: 0x0600CA9A RID: 51866 RVA: 0x0049B150 File Offset: 0x00499350
		public override bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			if (E.Actor == this.ParentObject && this.ParentObject.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					bool? flag;
					if (currentCell == null)
					{
						flag = null;
					}
					else
					{
						Zone parentZone = currentCell.ParentZone;
						flag = ((parentZone != null) ? new bool?(parentZone.IsWorldMap()) : null);
					}
					bool? flag2 = flag;
					if (!flag2.GetValueOrDefault())
					{
						List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.GetRadius(), "Combat");
						int i = 0;
						int count = list.Count;
						while (i < count)
						{
							GameObject gameObject = list[i];
							HeightenedHearingEffect heightenedHearingEffect = gameObject.GetEffect(typeof(HeightenedHearingEffect), new Predicate<Effect>(this.IsOurs)) as HeightenedHearingEffect;
							if (heightenedHearingEffect != null && heightenedHearingEffect.Listener == this.ParentObject && heightenedHearingEffect.Identified && this.ParentObject.IsRelevantHostile(gameObject))
							{
								E.Hostile = gameObject;
								E.PerceiveVerb = "hear";
							}
							i++;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA9B RID: 51867 RVA: 0x0049B274 File Offset: 0x00499474
		private bool IsOurs(Effect FX)
		{
			return (FX as HeightenedHearingEffect).Listener == this.ParentObject;
		}

		// Token: 0x0600CA9C RID: 51868 RVA: 0x0049B289 File Offset: 0x00499489
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA9D RID: 51869 RVA: 0x0049B29E File Offset: 0x0049949E
		public override string GetDescription()
		{
			return "You are possessed of unnaturally acute hearing.";
		}

		// Token: 0x0600CA9E RID: 51870 RVA: 0x0049B2A8 File Offset: 0x004994A8
		public override string GetLevelText(int Level)
		{
			string text = "You detect the presence of creatures within a radius of {{rules|" + HeightenedHearing.GetRadius(Level).ToString() + "}}.\n";
			if (Level == base.Level)
			{
				text += "Chance to identify nearby detected creatures";
			}
			else
			{
				text += "{{rules|Increased chance to identify nearby detected creatures}}";
			}
			return text;
		}

		// Token: 0x0600CA9F RID: 51871 RVA: 0x0049B2F7 File Offset: 0x004994F7
		public static int GetRadius(int Level)
		{
			if (Level < 10)
			{
				return 3 + Level * 2;
			}
			return 40;
		}

		// Token: 0x0600CAA0 RID: 51872 RVA: 0x0049B306 File Offset: 0x00499506
		public int GetRadius()
		{
			return HeightenedHearing.GetRadius(base.Level);
		}

		// Token: 0x0600CAA1 RID: 51873 RVA: 0x0049B314 File Offset: 0x00499514
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && this.ParentObject.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					int radius = this.GetRadius();
					List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, radius, "Combat");
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						GameObject gameObject = list[i];
						if (this.ParentObject.DistanceTo(gameObject) <= radius && !gameObject.HasEffect(typeof(HeightenedHearingEffect), new Predicate<Effect>(this.IsOurs)))
						{
							gameObject.ApplyEffect(new HeightenedHearingEffect(base.Level, this.ParentObject), null);
						}
						i++;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CAA2 RID: 51874 RVA: 0x0049B3EB File Offset: 0x004995EB
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CAA3 RID: 51875 RVA: 0x0049B3F4 File Offset: 0x004995F4
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CAA4 RID: 51876 RVA: 0x0049B3FE File Offset: 0x004995FE
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
