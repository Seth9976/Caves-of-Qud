using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D6B RID: 3435
	[Serializable]
	public class Overburdened : Effect
	{
		// Token: 0x060087F7 RID: 34807 RVA: 0x0030BBE2 File Offset: 0x00309DE2
		public Overburdened()
		{
			this.DisplayName = "{{K|overburdened}}";
			this.Duration = 1;
		}

		// Token: 0x060087F8 RID: 34808 RVA: 0x0030BBFC File Offset: 0x00309DFC
		public override string GetDetails()
		{
			return "Unable to move.";
		}

		// Token: 0x060087F9 RID: 34809 RVA: 0x0030BC03 File Offset: 0x00309E03
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Overburdened>())
			{
				return false;
			}
			if (Object.IsPlayer() || base.Visible())
			{
				Object.ParticleText("*overburdened*", 'K', false, 1.5f, -8f);
			}
			return true;
		}

		// Token: 0x060087FA RID: 34810 RVA: 0x0030BC38 File Offset: 0x00309E38
		public override int GetEffectType()
		{
			return 33554560;
		}

		// Token: 0x060087FB RID: 34811 RVA: 0x0030BC3F File Offset: 0x00309E3F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID;
		}

		// Token: 0x060087FC RID: 34812 RVA: 0x0030BC58 File Offset: 0x00309E58
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (E.Object == base.Object && E.To == "Flying")
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You can't fly while overburdened.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087FD RID: 34813 RVA: 0x0030BCA7 File Offset: 0x00309EA7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x060087FE RID: 34814 RVA: 0x0030BCBC File Offset: 0x00309EBC
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "IsMobile") || this.Duration <= 0 || base.Object.IsTryingToJoinPartyLeader()) && base.FireEvent(E);
		}
	}
}
