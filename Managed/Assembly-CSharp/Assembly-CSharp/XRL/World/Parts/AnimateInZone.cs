using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FF5 RID: 4085
	[Serializable]
	public class AnimateInZone : IPart
	{
		// Token: 0x0600A2D9 RID: 41689 RVA: 0x003B7E03 File Offset: 0x003B6003
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600A2DA RID: 41690 RVA: 0x003B7E19 File Offset: 0x003B6019
		private void Animate(GameObject Object)
		{
			AnimateObject.Animate(Object, null, null);
			GameObjectFactory.ApplyBuilders(Object, this.ParentObject.GetBlueprint(true), null);
		}

		// Token: 0x0600A2DB RID: 41691 RVA: 0x003B7E38 File Offset: 0x003B6038
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				foreach (Cell cell in E.Cell.IterateAdjacent(20, false, true, true))
				{
					foreach (GameObject gameObject in cell.Objects)
					{
						if (gameObject.Brain == null && AnimateObject.CanBeAnimated(gameObject))
						{
							this.Animate(gameObject);
							return base.HandleEvent(E);
						}
					}
				}
				GameObject gameObject2 = E.Cell.AddObject(PopulationManager.RollOneFrom("DynamicObjectsTable:AnimatableFurniture", null, null).Blueprint, null, null, null, null);
				this.Animate(gameObject2);
			}
			finally
			{
				this.ParentObject.Destroy(null, true, false, null);
			}
			return base.HandleEvent(E);
		}
	}
}
