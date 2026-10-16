using System;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000DB2 RID: 3506
	[Serializable]
	public class IrisdualMolting : IEffectWithPrefabImposter
	{
		// Token: 0x06008C03 RID: 35843 RVA: 0x0031B3DF File Offset: 0x003195DF
		public IrisdualMolting()
		{
			this.DisplayName = "{{rainbow|irisdual molting}}";
			this.PrefabID = "Prefabs/Particles/IrisdualMolting";
		}

		// Token: 0x06008C04 RID: 35844 RVA: 0x0031B3FD File Offset: 0x003195FD
		public IrisdualMolting(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008C05 RID: 35845 RVA: 0x0031B40C File Offset: 0x0031960C
		public override string GetDetails()
		{
			return "Molting beams of prismatic light.";
		}

		// Token: 0x06008C06 RID: 35846 RVA: 0x0031B413 File Offset: 0x00319613
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008C07 RID: 35847 RVA: 0x0031B416 File Offset: 0x00319616
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008C08 RID: 35848 RVA: 0x0031B420 File Offset: 0x00319620
		public override bool Apply(GameObject Object)
		{
			Object.ApplyEffect(new IrisdualMoltingSound(), null);
			ReflectProjectiles reflectProjectiles;
			if (Object.TryGetPart<ReflectProjectiles>(out reflectProjectiles))
			{
				reflectProjectiles.Deactivated = true;
				reflectProjectiles.Check();
			}
			return true;
		}

		// Token: 0x06008C09 RID: 35849 RVA: 0x0031B454 File Offset: 0x00319654
		public override void Remove(GameObject Object)
		{
			Object.RemoveEffect<IrisdualMoltingSound>();
			ReflectProjectiles reflectProjectiles;
			if (Object.TryGetPart<ReflectProjectiles>(out reflectProjectiles))
			{
				reflectProjectiles.Deactivated = false;
			}
		}

		// Token: 0x06008C0A RID: 35850 RVA: 0x0031B479 File Offset: 0x00319679
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06008C0B RID: 35851 RVA: 0x0031B490 File Offset: 0x00319690
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			Cell currentCell = base.Object.CurrentCell;
			currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, 1);
			currentCell.ParentZone.AddVisibility(currentCell.X, currentCell.Y, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x06008C0C RID: 35852 RVA: 0x0031B4E0 File Offset: 0x003196E0
		public override bool Render(RenderEvent E)
		{
			if (this.ImposterActive)
			{
				if (XRLCore.CurrentFrame >= this.NextFrame)
				{
					this.NextFrame = XRLCore.CurrentFrame + 4;
					if (this.NextFrame >= 62)
					{
						this.NextFrame -= 62;
					}
					this.LastColorIndex++;
					if (this.LastColorIndex >= Crayons.BrightColorStrings.Length)
					{
						this.LastColorIndex = 0;
					}
				}
				E.ColorString = Crayons.BrightColorStrings[this.LastColorIndex];
			}
			return base.Render(E);
		}

		// Token: 0x04003AED RID: 15085
		[NonSerialized]
		private int LastColorIndex;

		// Token: 0x04003AEE RID: 15086
		[NonSerialized]
		private int NextFrame;
	}
}
