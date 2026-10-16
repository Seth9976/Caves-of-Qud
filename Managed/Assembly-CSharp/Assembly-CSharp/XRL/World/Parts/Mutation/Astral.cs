using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001405 RID: 5125
	[Serializable]
	public class Astral : BaseMutation
	{
		// Token: 0x0600C708 RID: 50952 RVA: 0x00488EC0 File Offset: 0x004870C0
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C709 RID: 50953 RVA: 0x00488EC3 File Offset: 0x004870C3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C70A RID: 50954 RVA: 0x00488EE9 File Offset: 0x004870E9
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.EnsurePhased();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C70B RID: 50955 RVA: 0x00488EF8 File Offset: 0x004870F8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.EnsurePhased();
			return base.HandleEvent(E);
		}

		// Token: 0x0600C70C RID: 50956 RVA: 0x00488F07 File Offset: 0x00487107
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("stars", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C70D RID: 50957 RVA: 0x00488F2A File Offset: 0x0048712A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C70E RID: 50958 RVA: 0x00488F2D File Offset: 0x0048712D
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C70F RID: 50959 RVA: 0x00488F34 File Offset: 0x00487134
		public override string GetDescription()
		{
			return "You live in an alternate plane of reality.";
		}

		// Token: 0x0600C710 RID: 50960 RVA: 0x00488F3C File Offset: 0x0048713C
		private void EnsurePhased()
		{
			if (!this.ParentObject.HasEffect(typeof(Phased)) && base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0))
			{
				this.ParentObject.ApplyEffect(new Phased(9999), null);
			}
		}
	}
}
