using System;
using XRL.Core;
using XRL.Rules;
using XRL.Wish;

namespace XRL.World.Effects
{
	// Token: 0x02000DBA RID: 3514
	[HasWishCommand]
	[Serializable]
	public class Hobbled : Effect, ITierInitialized
	{
		// Token: 0x06008C30 RID: 35888 RVA: 0x0031B9E7 File Offset: 0x00319BE7
		public Hobbled()
		{
			this.DisplayName = "{{C|hobbled}}";
		}

		// Token: 0x06008C31 RID: 35889 RVA: 0x0031B9FA File Offset: 0x00319BFA
		public Hobbled(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008C32 RID: 35890 RVA: 0x0031BA09 File Offset: 0x00319C09
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 100);
		}

		// Token: 0x06008C33 RID: 35891 RVA: 0x0031BA1A File Offset: 0x00319C1A
		public override int GetEffectType()
		{
			return 50332672;
		}

		// Token: 0x06008C34 RID: 35892 RVA: 0x0031BA21 File Offset: 0x00319C21
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008C35 RID: 35893 RVA: 0x0031BA24 File Offset: 0x00319C24
		public override string GetDetails()
		{
			return "-50% Move Speed";
		}

		// Token: 0x06008C36 RID: 35894 RVA: 0x0031BA2C File Offset: 0x00319C2C
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasStat("MoveSpeed") && Object.FireEvent("ApplyHobble"))
			{
				return false;
			}
			base.DidX("are", "hobbled", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			Object.ParticleText("*hobbled*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			Hobbled hobbled;
			if (Object.TryGetEffect<Hobbled>(out hobbled))
			{
				if (hobbled.Duration < this.Duration)
				{
					hobbled.Duration = this.Duration;
				}
				return false;
			}
			this.Penalty = 50;
			base.StatShifter.SetStatShift("MoveSpeed", this.Penalty, false);
			return true;
		}

		// Token: 0x06008C37 RID: 35895 RVA: 0x0031BADC File Offset: 0x00319CDC
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.Penalty = 0;
		}

		// Token: 0x06008C38 RID: 35896 RVA: 0x0031BAF0 File Offset: 0x00319CF0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008C39 RID: 35897 RVA: 0x0031BB05 File Offset: 0x00319D05
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.Duration--;
			}
			return true;
		}

		// Token: 0x06008C3A RID: 35898 RVA: 0x0031BB28 File Offset: 0x00319D28
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 50 && num < 60)
				{
					E.RenderString = "\u000f";
					E.ColorString = base.Object.GetTag("BleedParticleColor", "&r^r");
				}
			}
			return true;
		}

		// Token: 0x06008C3B RID: 35899 RVA: 0x0031BB78 File Offset: 0x00319D78
		[WishCommand("hobbled", null)]
		public static void Wish()
		{
			The.Player.ApplyEffect(new Hobbled(20), null);
		}

		// Token: 0x04003AF5 RID: 15093
		public const int BASE_MOVE_SPEED_PENALTY = 50;

		// Token: 0x04003AF6 RID: 15094
		public int Penalty;
	}
}
