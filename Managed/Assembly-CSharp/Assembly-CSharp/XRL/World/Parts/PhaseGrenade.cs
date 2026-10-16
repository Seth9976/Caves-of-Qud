using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FD6 RID: 4054
	[Serializable]
	public class PhaseGrenade : IGrenade
	{
		// Token: 0x0600A11A RID: 41242 RVA: 0x003A9DD8 File Offset: 0x003A7FD8
		public override bool SameAs(IPart p)
		{
			PhaseGrenade phaseGrenade = p as PhaseGrenade;
			return phaseGrenade.Radius == this.Radius && !(phaseGrenade.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x0600A11B RID: 41243 RVA: 0x003A9E18 File Offset: 0x003A8018
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID;
		}

		// Token: 0x0600A11C RID: 41244 RVA: 0x003A9E36 File Offset: 0x003A8036
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(3);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A11D RID: 41245 RVA: 0x003A9E46 File Offset: 0x003A8046
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A11E RID: 41246 RVA: 0x003A9E58 File Offset: 0x003A8058
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			if (!IComponent<GameObject>.CheckRealityDistortionUsability(this.ParentObject, null, null, this.ParentObject, null, null, 0))
			{
				return false;
			}
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			List<Cell> list = new List<Cell>((this.Radius + 2) * (this.Radius + 2));
			C.GetAdjacentCells(this.Radius, list, false, true);
			int phase = this.ParentObject.GetPhase();
			List<GameObject> list2 = Event.NewGameObjectList();
			foreach (Cell cell in list)
			{
				if (IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, null, this.ParentObject, null, 0))
				{
					list2.Clear();
					list2.AddRange(cell.Objects);
					foreach (GameObject gameObject in list2)
					{
						if (gameObject.HasEffect<Phased>())
						{
							gameObject.RemoveAllEffects<Phased>(true);
						}
						else
						{
							gameObject.ForceApplyEffect(new Phased(this.Duration.RollCached()), null);
						}
					}
					if (cell.ParentZone.IsActive())
					{
						if (phase == 1)
						{
							The.ParticleManager.Add("&M" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(4, 9), 0f, 0f, 0L);
							The.ParticleManager.Add("&m" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(2, 6), 0f, 0f, 0L);
							The.ParticleManager.Add("&K" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(0, 3), 0f, 0f, 0L);
						}
						else if (phase == 2)
						{
							The.ParticleManager.Add("&B" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(4, 9), 0f, 0f, 0L);
							The.ParticleManager.Add("&Y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(2, 6), 0f, 0f, 0L);
							The.ParticleManager.Add("&b" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(0, 3), 0f, 0f, 0L);
						}
						else if (phase == 3)
						{
							The.ParticleManager.Add("&G" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(4, 9), 0f, 0f, 0L);
							The.ParticleManager.Add("&c" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(2, 6), 0f, 0f, 0L);
							The.ParticleManager.Add("&M" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(0, 3), 0f, 0f, 0L);
						}
						else if (phase == 4)
						{
							The.ParticleManager.Add("&K" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(4, 9), 0f, 0f, 0L);
							The.ParticleManager.Add("&y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(2, 6), 0f, 0f, 0L);
							The.ParticleManager.Add("&b" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), (float)cell.X, (float)cell.Y, 0f, 0f, Stat.Random(0, 3), 0f, 0f, 0L);
						}
					}
				}
			}
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042E9 RID: 17129
		public int Radius;

		// Token: 0x040042EA RID: 17130
		public string Duration;
	}
}
