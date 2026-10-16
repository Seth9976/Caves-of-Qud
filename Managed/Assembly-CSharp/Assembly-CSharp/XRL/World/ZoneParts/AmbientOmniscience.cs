using System;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C1 RID: 2497
	[Serializable]
	public class AmbientOmniscience : IZonePart
	{
		// Token: 0x06006B44 RID: 27460 RVA: 0x00228B88 File Offset: 0x00226D88
		public override bool SameAs(IZonePart Part)
		{
			AmbientOmniscience ambientOmniscience = Part as AmbientOmniscience;
			return ambientOmniscience.IsRealityDistortionBased == this.IsRealityDistortionBased && ambientOmniscience.Duration == this.Duration && base.SameAs(Part);
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x00228BC3 File Offset: 0x00226DC3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || (ID == SingletonEvent<EndTurnEvent>.ID && this.Duration > 0);
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x00228BEC File Offset: 0x00226DEC
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.ParentZone.HasObject(The.Player))
			{
				if (this.IsRealityDistortionBased)
				{
					for (int i = 0; i < this.ParentZone.Height; i++)
					{
						for (int j = 0; j < this.ParentZone.Width; j++)
						{
							if (IComponent<Zone>.CheckRealityDistortionAccessibility(this.ParentZone.GetCell(j, i)))
							{
								this.ParentZone.AddLight(j, i, 0, LightLevel.Omniscient, false);
							}
						}
					}
				}
				else
				{
					this.ParentZone.AddLight(LightLevel.Omniscient, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x00228C84 File Offset: 0x00226E84
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0)
			{
				int num = this.Duration - 1;
				this.Duration = num;
				if (num <= 0)
				{
					this.ParentZone.RemovePart(this);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003119 RID: 12569
		public bool IsRealityDistortionBased;

		// Token: 0x0400311A RID: 12570
		public int Duration;
	}
}
