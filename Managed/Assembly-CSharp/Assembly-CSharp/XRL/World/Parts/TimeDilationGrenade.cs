using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000FD9 RID: 4057
	[Serializable]
	public class TimeDilationGrenade : IGrenade
	{
		// Token: 0x0600A12C RID: 41260 RVA: 0x003AA96A File Offset: 0x003A8B6A
		public override bool SameAs(IPart p)
		{
			return (p as TimeDilationGrenade).Level == this.Level && base.SameAs(p);
		}

		// Token: 0x0600A12D RID: 41261 RVA: 0x003AA988 File Offset: 0x003A8B88
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetComponentNavigationWeightEvent.ID || ID == GetComponentAdjacentNavigationWeightEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600A12E RID: 41262 RVA: 0x003AA9AE File Offset: 0x003A8BAE
		public override bool HandleEvent(GetComponentNavigationWeightEvent E)
		{
			E.MinWeight(3);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A12F RID: 41263 RVA: 0x003AA9BE File Offset: 0x003A8BBE
		public override bool HandleEvent(GetComponentAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A130 RID: 41264 RVA: 0x003AA9CE File Offset: 0x003A8BCE
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("time", 10);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A131 RID: 41265 RVA: 0x003AA9F4 File Offset: 0x003A8BF4
		protected override bool DoDetonate(Cell C, GameObject Actor = null, GameObject ApparentTarget = null, bool Indirect = false)
		{
			base.PlayWorldSound(base.GetPropertyOrTag("DetonatedSound", null), 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
			base.DidX("discorporate", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			for (int i = 0; i < Stat.RandomCosmetic(1, 3); i++)
			{
				float num = (float)Stat.RandomCosmetic(4, 14) / 3f;
				for (int j = 0; j < 360; j++)
				{
					XRLCore.ParticleManager.Add("@", (float)C.X, (float)C.Y, (float)Math.Sin((double)((float)j) * 0.017) / num, (float)Math.Cos((double)((float)j) * 0.017) / num);
				}
			}
			TimeDilation.ApplyField(this.ParentObject, this.Range, true, 15, this.Level, null);
			this.ParentObject.Destroy(null, true, false, null);
			return true;
		}

		// Token: 0x040042EF RID: 17135
		public int Range = 9;

		// Token: 0x040042F0 RID: 17136
		public int Level = 1;
	}
}
