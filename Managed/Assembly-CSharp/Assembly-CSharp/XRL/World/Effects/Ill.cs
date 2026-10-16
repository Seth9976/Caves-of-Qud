using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D52 RID: 3410
	[Serializable]
	public class Ill : Effect, ITierInitialized
	{
		// Token: 0x060086B6 RID: 34486 RVA: 0x0030687B File Offset: 0x00304A7B
		public Ill()
		{
			this.DisplayName = "{{g|illness}}";
		}

		// Token: 0x060086B7 RID: 34487 RVA: 0x003068A0 File Offset: 0x00304AA0
		public Ill(int Duration, int Level = 1, string Message = null, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false)
			: this()
		{
			this.Duration = Duration;
			this.Message = Message;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
		}

		// Token: 0x060086B8 RID: 34488 RVA: 0x003068C6 File Offset: 0x00304AC6
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(30, 200);
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x003068DA File Offset: 0x00304ADA
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x003068DD File Offset: 0x00304ADD
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x060086BB RID: 34491 RVA: 0x003068E4 File Offset: 0x00304AE4
		public override string GetDescription()
		{
			return "{{g|ill}}";
		}

		// Token: 0x060086BC RID: 34492 RVA: 0x003068EB File Offset: 0x00304AEB
		public override string GetDetails()
		{
			return "Doesn't heal hit points naturally.\nExternal healing is only half as effective.";
		}

		// Token: 0x060086BD RID: 34493 RVA: 0x003068F4 File Offset: 0x00304AF4
		public override bool Apply(GameObject Object)
		{
			Ill effect = Object.GetEffect<Ill>();
			if (effect != null)
			{
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
				}
				return false;
			}
			if (Object.FireEvent("ApplyIll"))
			{
				if (!this.Message.IsNullOrEmpty() && Object.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Object, this.Message, ' ', false, this.StartMessageUsePopup, false, null, Object);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060086BE RID: 34494 RVA: 0x00306968 File Offset: 0x00304B68
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(Object, "You no longer feel ill.", ' ', false, this.StopMessageUsePopup, false, Object, null);
			}
			base.Remove(Object);
		}

		// Token: 0x060086BF RID: 34495 RVA: 0x0030699D File Offset: 0x00304B9D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Healing");
			Registrar.Register("Recuperating");
			Registrar.Register("Regenerating");
			base.Register(Object, Registrar);
		}

		// Token: 0x060086C0 RID: 34496 RVA: 0x003069C8 File Offset: 0x00304BC8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Healing")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) / 2);
			}
			else
			{
				if (E.ID == "Regenerating")
				{
					E.SetParameter("Amount", 0);
					return false;
				}
				if (E.ID == "Recuperating")
				{
					this.Duration = 0;
					base.DidX("are", "no longer ill", null, null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040039F1 RID: 14833
		public int Level = 1;

		// Token: 0x040039F2 RID: 14834
		public string Message = "The poison begins to abate, but you still feel nauseous.";

		// Token: 0x040039F3 RID: 14835
		public bool StartMessageUsePopup;

		// Token: 0x040039F4 RID: 14836
		public bool StopMessageUsePopup;
	}
}
