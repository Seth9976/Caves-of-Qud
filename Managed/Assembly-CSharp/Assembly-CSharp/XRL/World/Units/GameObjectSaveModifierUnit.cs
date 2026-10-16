using System;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Units
{
	// Token: 0x020009B3 RID: 2483
	[Serializable]
	public class GameObjectSaveModifierUnit : GameObjectUnit
	{
		// Token: 0x06006AFC RID: 27388 RVA: 0x002273F5 File Offset: 0x002255F5
		public override void Apply(GameObject Object)
		{
			SaveModifiers saveModifiers = Object.RequirePart<SaveModifiers>(false);
			saveModifiers.WorksOnSelf = true;
			saveModifiers.WorksOnEquipper = false;
			saveModifiers.AddModifier(this.Vs, this.Value);
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x00227420 File Offset: 0x00225620
		public override void Remove(GameObject Object)
		{
			SaveModifiers part = Object.GetPart<SaveModifiers>();
			if (part != null)
			{
				part.AddModifier(this.Vs, -this.Value);
				if (part.Tracking.Count == 0)
				{
					Object.RemovePart(part);
				}
			}
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x0022745E File Offset: 0x0022565E
		public override void Reset()
		{
			base.Reset();
			this.Vs = null;
			this.Value = 0;
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x00227474 File Offset: 0x00225674
		public override string GetDescription(bool Inscription = false)
		{
			return SavingThrows.GetSaveBonusDescription(this.Value, this.Vs, false);
		}

		// Token: 0x040030FF RID: 12543
		public string Vs;

		// Token: 0x04003100 RID: 12544
		public int Value;
	}
}
