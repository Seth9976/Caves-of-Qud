using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CD5 RID: 3285
	[Serializable]
	public class CookingDomainPhase_UnitPhaseOnDamage : ProceduralCookingEffectUnit
	{
		// Token: 0x0600829E RID: 33438 RVA: 0x002F7F6B File Offset: 0x002F616B
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(15, 20);
		}

		// Token: 0x0600829F RID: 33439 RVA: 0x002F7F7C File Offset: 0x002F617C
		public override string GetDescription()
		{
			return "whenever @thisCreature take@s damage, there's a " + this.Tier.ToString() + "% chance @they start phasing for 8-10 turns.";
		}

		// Token: 0x060082A0 RID: 33440 RVA: 0x002F7F98 File Offset: 0x002F6198
		public override string GetTemplatedDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 15-20% chance @they start phasing for 8-10 turns.";
		}

		// Token: 0x060082A1 RID: 33441 RVA: 0x002F7F9F File Offset: 0x002F619F
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x002F7FAD File Offset: 0x002F61AD
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060082A3 RID: 33443 RVA: 0x002F7FBC File Offset: 0x002F61BC
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				GameObject @object = this.parent.Object;
				if (@object == null)
				{
					return;
				}
				if (Stat.Random(1, 100) <= this.Tier && @object.CurrentCell != null && !@object.OnWorldMap() && !@object.HasEffect<Phased>())
				{
					if (IComponent<GameObject>.CheckRealityDistortionAccessibility(@object))
					{
						@object.ApplyEffect(new Phased(Stat.Random(8, 10)), null);
						for (int i = 0; i < 5; i++)
						{
							XRLCore.ParticleManager.AddRadial("&bù", (float)@object.CurrentCell.X, (float)@object.CurrentCell.Y, (float)Stat.Random(0, 7), (float)Stat.Random(5, 10), 0.01f * (float)Stat.Random(4, 6), -0.05f * (float)Stat.Random(3, 7));
						}
					}
					return;
				}
			}
		}

		// Token: 0x0400393C RID: 14652
		public int Tier = 1;
	}
}
