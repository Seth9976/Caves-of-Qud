using System;

namespace XRL.World.Parts
{
	// Token: 0x02001241 RID: 4673
	public abstract class SoundRendererPart : IPart, ISoundRenderer
	{
		// Token: 0x0600B9BA RID: 47546
		public abstract void Render(RenderSoundEvent E);

		// Token: 0x0600B9BB RID: 47547 RVA: 0x0043D0A8 File Offset: 0x0043B2A8
		void ISoundRenderer.Render(RenderSoundEvent E)
		{
			GameObject parentObject = this.ParentObject;
			Cell cell = ((parentObject != null) ? parentObject.CurrentCell : null);
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

		// Token: 0x0600B9BC RID: 47548 RVA: 0x0043D143 File Offset: 0x0043B343
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600B9BD RID: 47549 RVA: 0x0043D161 File Offset: 0x0043B361
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			E.Zone.RegisterSoundRenderer(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9BE RID: 47550 RVA: 0x0043D176 File Offset: 0x0043B376
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
