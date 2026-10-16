using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DB3 RID: 3507
	[Serializable]
	public class IrisdualMoltingSound : SoundRendererEffect
	{
		// Token: 0x06008C0D RID: 35853 RVA: 0x0031B565 File Offset: 0x00319765
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008C0E RID: 35854 RVA: 0x0031B568 File Offset: 0x00319768
		public override void Render(RenderSoundEvent E)
		{
			if (this.SoundID == null)
			{
				this.SoundID = "IrisdualMolting" + "." + Guid.NewGuid().ToString();
			}
			E.Char.soundExtra.Add(this.SoundID, "Sounds/Creatures/Ability/sfx_creature_girshNephilim_irisdualBeam_molting_lp", 1f, 1f, 0.5f, 20);
		}

		// Token: 0x04003AEF RID: 15087
		[NonSerialized]
		private string SoundID;
	}
}
