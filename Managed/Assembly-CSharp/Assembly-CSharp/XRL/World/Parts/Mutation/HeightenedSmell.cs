using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001446 RID: 5190
	[Serializable]
	public class HeightenedSmell : BaseMutation
	{
		// Token: 0x0600CAA6 RID: 51878 RVA: 0x0049B40F File Offset: 0x0049960F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ExtraHostilePerceptionEvent>.ID;
		}

		// Token: 0x0600CAA7 RID: 51879 RVA: 0x0049B428 File Offset: 0x00499628
		public override bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			if (E.Actor == this.ParentObject && this.ParentObject.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.GetRadius(), "Combat");
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						GameObject gameObject = list[i];
						HeightenedSmellEffect heightenedSmellEffect = gameObject.GetEffect(typeof(HeightenedSmellEffect), new Predicate<Effect>(this.IsOurs)) as HeightenedSmellEffect;
						if (heightenedSmellEffect != null && heightenedSmellEffect.Smeller == this.ParentObject && heightenedSmellEffect.Identified && this.ParentObject.IsRelevantHostile(gameObject))
						{
							E.Hostile = gameObject;
							E.PerceiveVerb = "smell";
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAA8 RID: 51880 RVA: 0x0049B510 File Offset: 0x00499710
		private bool IsOurs(Effect FX)
		{
			return (FX as HeightenedSmellEffect).Smeller == this.ParentObject;
		}

		// Token: 0x0600CAA9 RID: 51881 RVA: 0x0049B525 File Offset: 0x00499725
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CAAA RID: 51882 RVA: 0x0049B53A File Offset: 0x0049973A
		public override string GetDescription()
		{
			return "You are possessed of exceptionally acute smell.";
		}

		// Token: 0x0600CAAB RID: 51883 RVA: 0x0049B544 File Offset: 0x00499744
		public override string GetLevelText(int Level)
		{
			string text = "You detect the presence of creatures within a distance typically up to " + HeightenedSmell.GetRadius(Level).ToString() + " squares depending on terrain\n";
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

		// Token: 0x0600CAAC RID: 51884 RVA: 0x0049B593 File Offset: 0x00499793
		public static int GetRadius(int Level)
		{
			return 5 + Level * 4;
		}

		// Token: 0x0600CAAD RID: 51885 RVA: 0x0049B59A File Offset: 0x0049979A
		public int GetRadius()
		{
			return HeightenedSmell.GetRadius(base.Level);
		}

		// Token: 0x0600CAAE RID: 51886 RVA: 0x0049B5A8 File Offset: 0x004997A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && this.ParentObject.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					List<GameObject> list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.GetRadius() + 10, "Combat");
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						GameObject gameObject = list[i];
						if (gameObject.IsSmellable(this.ParentObject) && !gameObject.HasEffect(typeof(HeightenedSmellEffect), new Predicate<Effect>(this.IsOurs)))
						{
							gameObject.ApplyEffect(new HeightenedSmellEffect(base.Level, this.ParentObject), null);
						}
						i++;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CAAF RID: 51887 RVA: 0x0049B67D File Offset: 0x0049987D
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CAB0 RID: 51888 RVA: 0x0049B686 File Offset: 0x00499886
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CAB1 RID: 51889 RVA: 0x0049B690 File Offset: 0x00499890
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
