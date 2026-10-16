using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001007 RID: 4103
	[Serializable]
	public class CauseBleedingWhenDestroyed : IPart
	{
		// Token: 0x0600A381 RID: 41857 RVA: 0x003BC0FC File Offset: 0x003BA2FC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDestroyObjectEvent.ID;
		}

		// Token: 0x0600A382 RID: 41858 RVA: 0x003BC114 File Offset: 0x003BA314
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			GameObject gameObject = E.Object.Equipped ?? E.Object.Implantee;
			if (gameObject != null)
			{
				gameObject.ApplyEffect(new Bleeding(this.Damage, this.SaveTarget, null, this.Stack, false, false, false), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040043BF RID: 17343
		public string Damage = "1";

		// Token: 0x040043C0 RID: 17344
		public int SaveTarget = 20;

		// Token: 0x040043C1 RID: 17345
		public bool Stack = true;
	}
}
