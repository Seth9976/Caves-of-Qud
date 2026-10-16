using System;

namespace XRL.World.Parts
{
	// Token: 0x0200105D RID: 4189
	[Serializable]
	public class LightSource : IPart, ILightSource
	{
		// Token: 0x0600A6AF RID: 42671 RVA: 0x003CF7CC File Offset: 0x003CD9CC
		public override bool SameAs(IPart p)
		{
			LightSource lightSource = p as LightSource;
			return lightSource.Darkvision == this.Darkvision && lightSource.OnlyIfCharged == this.OnlyIfCharged && lightSource.Lit == this.Lit && lightSource.Radius == this.Radius && base.SameAs(p);
		}

		// Token: 0x0600A6B0 RID: 42672 RVA: 0x003CF827 File Offset: 0x003CDA27
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600A6B1 RID: 42673 RVA: 0x003CF83D File Offset: 0x003CDA3D
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			this.RadiateLight();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6B2 RID: 42674 RVA: 0x003CF850 File Offset: 0x003CDA50
		public bool RadiateLight()
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return false;
			}
			if (this.OnlyIfCharged && (base.IsBroken() || base.IsRusted() || !this.ParentObject.TestCharge(1, false, 0L, true, true)))
			{
				return false;
			}
			if (this.Darkvision)
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null && equipped.IsPlayer() && this.Lit)
				{
					if (this.Radius == 9999)
					{
						currentCell.ParentZone.LightAll(LightLevel.Darkvision);
					}
					else
					{
						currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, this.Radius, LightLevel.Darkvision, false);
					}
				}
			}
			else if (this.Lit)
			{
				if (this.Radius == 9999)
				{
					currentCell.ParentZone.LightAll(LightLevel.Light);
				}
				else
				{
					currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, this.Radius);
				}
			}
			return true;
		}

		// Token: 0x0600A6B3 RID: 42675 RVA: 0x003CF94D File Offset: 0x003CDB4D
		int ILightSource.GetRadius()
		{
			return this.Radius;
		}

		// Token: 0x0600A6B4 RID: 42676 RVA: 0x003CF955 File Offset: 0x003CDB55
		bool ILightSource.IsActive()
		{
			return this.Lit;
		}

		// Token: 0x0600A6B5 RID: 42677 RVA: 0x003CF95D File Offset: 0x003CDB5D
		bool ILightSource.IsDarkvision()
		{
			return this.Darkvision;
		}

		// Token: 0x040044E1 RID: 17633
		public bool Darkvision;

		// Token: 0x040044E2 RID: 17634
		public bool OnlyIfCharged;

		// Token: 0x040044E3 RID: 17635
		public bool Lit = true;

		// Token: 0x040044E4 RID: 17636
		public int Radius = 5;
	}
}
