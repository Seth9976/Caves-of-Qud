using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D2F RID: 3375
	[Serializable]
	public class ElectromagneticPulsed : Effect, ITierInitialized
	{
		// Token: 0x06008512 RID: 34066 RVA: 0x00300B21 File Offset: 0x002FED21
		public ElectromagneticPulsed()
		{
			this.DisplayName = "{{B|pulsed}}";
		}

		// Token: 0x06008513 RID: 34067 RVA: 0x00300B34 File Offset: 0x002FED34
		public ElectromagneticPulsed(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008514 RID: 34068 RVA: 0x00300B43 File Offset: 0x002FED43
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(50, 300);
		}

		// Token: 0x06008515 RID: 34069 RVA: 0x00300B57 File Offset: 0x002FED57
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008516 RID: 34070 RVA: 0x00300B5A File Offset: 0x002FED5A
		public override int GetEffectType()
		{
			return 117440576;
		}

		// Token: 0x06008517 RID: 34071 RVA: 0x00300B61 File Offset: 0x002FED61
		public override string GetDetails()
		{
			return "Deactivated (if robotic).\nCan't be activated (if artifact).";
		}

		// Token: 0x06008518 RID: 34072 RVA: 0x00300B68 File Offset: 0x002FED68
		private bool ShouldPulse(GameObject obj)
		{
			if (base.Object.Body != null)
			{
				return true;
			}
			Inventory inventory = base.Object.Inventory;
			return (inventory != null && inventory.GetObjectCountDirect() > 0) || base.Object.IsEMPSensitive() || base.Object.HasRegisteredEvent("ApplyEMP");
		}

		// Token: 0x06008519 RID: 34073 RVA: 0x00300BC8 File Offset: 0x002FEDC8
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<ElectromagneticPulsed>())
			{
				ElectromagneticPulsed effect = Object.GetEffect<ElectromagneticPulsed>();
				if (effect.Duration < this.Duration)
				{
					effect.Duration = this.Duration;
				}
				return false;
			}
			if (!Object.HasRegisteredEvent("ApplyEMP") || Object.FireEvent(Event.New("ApplyEMP", "Duration", this.Duration)))
			{
				Inventory inventory = Object.Inventory;
				if (inventory != null)
				{
					bool flag = false;
					foreach (GameObject gameObject in inventory.GetObjectsDirect())
					{
						if (this.ShouldPulse(gameObject))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						foreach (GameObject gameObject2 in new List<GameObject>(inventory.GetObjectsDirect()))
						{
							if (this.ShouldPulse(gameObject2))
							{
								gameObject2.ApplyEffect(new ElectromagneticPulsed(this.Duration), null);
							}
						}
					}
				}
				Body body = Object.Body;
				if (body != null)
				{
					foreach (BodyPart bodyPart in body.GetParts())
					{
						if (bodyPart.DefaultBehavior != null && this.ShouldPulse(bodyPart.DefaultBehavior))
						{
							bodyPart.DefaultBehavior.ApplyEffect(new ElectromagneticPulsed(this.Duration), null);
						}
						if (bodyPart.Equipped != null && this.ShouldPulse(bodyPart.Equipped))
						{
							bodyPart.Equipped.ApplyEffect(new ElectromagneticPulsed(this.Duration), null);
						}
						if (bodyPart.Cybernetics != null && this.ShouldPulse(bodyPart.Cybernetics))
						{
							bodyPart.Cybernetics.ApplyEffect(new ElectromagneticPulsed(this.Duration), null);
						}
					}
				}
				return Object.IsEMPSensitive();
			}
			return false;
		}

		// Token: 0x0600851A RID: 34074 RVA: 0x00300DE0 File Offset: 0x002FEFE0
		public override bool Render(RenderEvent E)
		{
			if (base.Object != null && Stat.RandomCosmetic(1, 120) <= 2)
			{
				for (int i = 0; i < 2; i++)
				{
					base.Object.ParticleText("&Y" + ((char)Stat.RandomCosmetic(191, 198)).ToString(), 0.2f, 20);
				}
				for (int j = 0; j < 2; j++)
				{
					base.Object.ParticleText("&W\u000f", 0.02f, 10);
				}
			}
			return true;
		}

		// Token: 0x0600851B RID: 34075 RVA: 0x00300E64 File Offset: 0x002FF064
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x0600851C RID: 34076 RVA: 0x00300E7A File Offset: 0x002FF07A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				E.AddTag("[{{W|EMP}}]", 40);
			}
			return base.HandleEvent(E);
		}
	}
}
