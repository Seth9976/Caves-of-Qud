using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D8D RID: 3469
	public abstract class SoundRendererEffect : Effect, ISoundRenderer
	{
		// Token: 0x06008A1F RID: 35359
		public abstract void Render(RenderSoundEvent E);

		// Token: 0x06008A20 RID: 35360 RVA: 0x00314C4C File Offset: 0x00312E4C
		void ISoundRenderer.Render(RenderSoundEvent E)
		{
			GameObject @object = base.Object;
			Cell cell = ((@object != null) ? @object.CurrentCell : null);
			if (cell == null || !cell.ParentZone.IsActive())
			{
				E.Unregister = true;
				return;
			}
			E.Char = E.Buffer[cell.X, cell.Y];
			this.Render(E);
			E.Char.soundExtra.SetDistance(Zone.SoundMap.GetCostAtPoint(cell.X, cell.Y));
			E.Char.soundExtra.SetOccluded(!cell.IsVisible());
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x00314CE7 File Offset: 0x00312EE7
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x00314D05 File Offset: 0x00312F05
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			E.Zone.RegisterSoundRenderer(this);
			return base.HandleEvent(E);
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x00314D1A File Offset: 0x00312F1A
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Cell.ParentZone.IsActive())
			{
				E.Cell.ParentZone.RegisterSoundRenderer(this);
			}
			return base.HandleEvent(E);
		}
	}
}
