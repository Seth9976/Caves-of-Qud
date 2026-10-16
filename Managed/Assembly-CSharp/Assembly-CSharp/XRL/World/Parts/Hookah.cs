using System;

namespace XRL.World.Parts
{
	// Token: 0x0200104D RID: 4173
	[Serializable]
	public class Hookah : IPart
	{
		// Token: 0x0600A623 RID: 42531 RVA: 0x003CCE4A File Offset: 0x003CB04A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A624 RID: 42532 RVA: 0x003CCE80 File Offset: 0x003CB080
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Smoke", "smoke", Hookah.COMMAND_NAME, null, 's', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A625 RID: 42533 RVA: 0x003CCEB8 File Offset: 0x003CB0B8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == Hookah.COMMAND_NAME && this.SmokeHookah(E.Actor, !E.Auto))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A626 RID: 42534 RVA: 0x003CCEF0 File Offset: 0x003CB0F0
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600A627 RID: 42535 RVA: 0x003CCEF3 File Offset: 0x003CB0F3
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			return !this.SmokeHookah(E.Actor, false);
		}

		// Token: 0x0600A628 RID: 42536 RVA: 0x003CCF08 File Offset: 0x003CB108
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return (!E.Actor.HasPart<Brain>() || E.Actor.HasPart<Robot>() || E.Actor.DistanceTo(this.ParentObject) > 1 || !10.in100() || !this.SmokeHookah(E.Actor, false)) && base.HandleEvent(E);
		}

		// Token: 0x0600A629 RID: 42537 RVA: 0x003CCF64 File Offset: 0x003CB164
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A62A RID: 42538 RVA: 0x003CCF67 File Offset: 0x003CB167
		public bool CanPuff()
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsPureLiquid("water", false);
		}

		// Token: 0x0600A62B RID: 42539 RVA: 0x003CCF88 File Offset: 0x003CB188
		public bool SmokeHookah(GameObject Actor, bool FromDialog)
		{
			if (!this.CanPuff())
			{
				return Actor.Fail(this.ParentObject.Does("need", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " water in " + this.ParentObject.them + ".");
			}
			if (!BeforeConsumeEvent.Check(Actor, Actor, this.ParentObject, false, false, false, true, false, true))
			{
				return false;
			}
			base.PlayWorldSound("sfx_interact_hookah_smoke", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.XDidYToZ(Actor, "take", "a puff on", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, FromDialog, false, null);
			Actor.UseEnergy(1000, "Item", null, null, false);
			for (int i = 2; i < 5; i++)
			{
				this.ParentObject.SmokePuff();
			}
			if (Actor.IsPlayer())
			{
				Achievement.SMOKE_HOOKAH.Unlock();
			}
			Event @event = Event.New("Smoked", "Actor", Actor, "Object", this.ParentObject);
			this.ParentObject.FireEvent(@event);
			Actor.FireEvent(@event);
			AfterConsumeEvent.Send(Actor, Actor, this.ParentObject, false, false, false, true, false, true);
			return true;
		}

		// Token: 0x0400449E RID: 17566
		public static readonly string COMMAND_NAME = "SmokeHookah";
	}
}
