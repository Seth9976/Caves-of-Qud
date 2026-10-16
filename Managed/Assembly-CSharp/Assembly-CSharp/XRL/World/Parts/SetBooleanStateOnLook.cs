using System;

namespace XRL.World.Parts
{
	// Token: 0x02001227 RID: 4647
	[Serializable]
	public class SetBooleanStateOnLook : IPart
	{
		// Token: 0x0600B860 RID: 47200 RVA: 0x0043120C File Offset: 0x0042F40C
		public SetBooleanStateOnLook()
		{
		}

		// Token: 0x0600B861 RID: 47201 RVA: 0x00431214 File Offset: 0x0042F414
		public SetBooleanStateOnLook(string State)
			: this()
		{
			this.State = State;
		}

		// Token: 0x0600B862 RID: 47202 RVA: 0x00431224 File Offset: 0x0042F424
		public override bool SameAs(IPart Part)
		{
			SetBooleanStateOnLook setBooleanStateOnLook = Part as SetBooleanStateOnLook;
			return setBooleanStateOnLook != null && setBooleanStateOnLook.State == this.State;
		}

		// Token: 0x0600B863 RID: 47203 RVA: 0x0043124E File Offset: 0x0042F44E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID;
		}

		// Token: 0x0600B864 RID: 47204 RVA: 0x00431264 File Offset: 0x0042F464
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && !this.LookedAt && this.HasUnsetState())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B865 RID: 47205 RVA: 0x004312A2 File Offset: 0x0042F4A2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
		}

		// Token: 0x0600B866 RID: 47206 RVA: 0x004312AF File Offset: 0x0042F4AF
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt)
			{
				The.Game.SetBooleanGameState(this.State, true);
				this.LookedAt = true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B867 RID: 47207 RVA: 0x004312EA File Offset: 0x0042F4EA
		public bool HasUnsetState()
		{
			return this.State != null && !The.Game.GetBooleanGameState(this.State, false);
		}

		// Token: 0x04004A47 RID: 19015
		public string State;

		// Token: 0x04004A48 RID: 19016
		public bool LookedAt;
	}
}
