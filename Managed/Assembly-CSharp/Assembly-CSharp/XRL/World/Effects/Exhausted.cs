using System;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D35 RID: 3381
	[Serializable]
	public class Exhausted : Effect, ITierInitialized
	{
		// Token: 0x06008569 RID: 34153 RVA: 0x00302A84 File Offset: 0x00300C84
		public Exhausted()
		{
			this.DisplayName = "{{K|exhaustion}}";
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x00302A97 File Offset: 0x00300C97
		public Exhausted(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600856B RID: 34155 RVA: 0x00302AA6 File Offset: 0x00300CA6
		public void Initialize(int Tier)
		{
			this.Duration = 3;
		}

		// Token: 0x0600856C RID: 34156 RVA: 0x00302AAF File Offset: 0x00300CAF
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x00302AB6 File Offset: 0x00300CB6
		public override string GetDetails()
		{
			return "Can't take actions.";
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x00302ABD File Offset: 0x00300CBD
		public override string GetStateDescription()
		{
			return "{{K|exhausted}}";
		}

		// Token: 0x0600856F RID: 34159 RVA: 0x00302AC4 File Offset: 0x00300CC4
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyExhausted", 0, 0, 0)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You are {{K|exhausted}}!", null, true);
			}
			Object.ParticleText("*exhausted*", 'C', false, 1.5f, -8f);
			Object.ForfeitTurn(false);
			return true;
		}

		// Token: 0x06008570 RID: 34160 RVA: 0x00302B1C File Offset: 0x00300D1C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x06008571 RID: 34161 RVA: 0x00302B3C File Offset: 0x00300D3C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (this.Duration != 9999)
				{
					this.Duration--;
				}
				if (base.Object.IsPlayer())
				{
					XRLCore.Core.RenderBase(true, false);
					Popup.Show("You are too exhausted to act!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				E.PreventAction = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x00302BAC File Offset: 0x00300DAC
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				if (E.Mental && !E.Physical)
				{
					E.Message = base.Object.Poss("mind", true) + " is present, but doesn't seem to be responding to you at all.";
				}
				else
				{
					E.Message = base.Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + base.Object.GetVerb("stare", true, false, false, true, false) + " at you dully.";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008573 RID: 34163 RVA: 0x00302C55 File Offset: 0x00300E55
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanMoveExtremities");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008574 RID: 34164 RVA: 0x00302C6C File Offset: 0x00300E6C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanMoveExtremities" && this.Duration > 0 && !E.HasFlag("Involuntary"))
			{
				if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
				{
					Popup.ShowFail("You are too exhausted to do that.", true, true, true);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008575 RID: 34165 RVA: 0x00302CD4 File Offset: 0x00300ED4
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 10 && num < 25)
			{
				E.Tile = null;
				E.RenderString = "_";
				E.ColorString = "&C^c";
			}
			return true;
		}
	}
}
