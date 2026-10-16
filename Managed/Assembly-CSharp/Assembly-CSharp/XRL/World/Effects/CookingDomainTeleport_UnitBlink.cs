using System;
using XRL.Messages;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D14 RID: 3348
	[Serializable]
	public class CookingDomainTeleport_UnitBlink : ProceduralCookingEffectUnit
	{
		// Token: 0x060083EF RID: 33775 RVA: 0x002FBB64 File Offset: 0x002F9D64
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(20, 25);
		}

		// Token: 0x060083F0 RID: 33776 RVA: 0x002FBB75 File Offset: 0x002F9D75
		public override string GetDescription()
		{
			return "Whenever @thisCreature take@s avoidable damage, there's a " + this.Tier.ToString() + "% chance @they teleport to a random space on the map instead.";
		}

		// Token: 0x060083F1 RID: 33777 RVA: 0x002FBB91 File Offset: 0x002F9D91
		public override string GetTemplatedDescription()
		{
			return "Whenever @thisCreature take@s avoidable damage, there's a 20-25% chance @they teleport to a random space on the map instead.";
		}

		// Token: 0x060083F2 RID: 33778 RVA: 0x002FBB98 File Offset: 0x002F9D98
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083F3 RID: 33779 RVA: 0x002FBBA6 File Offset: 0x002F9DA6
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x060083F4 RID: 33780 RVA: 0x002FBBB4 File Offset: 0x002F9DB4
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				if (this.parent == null)
				{
					return;
				}
				GameObject @object = this.parent.Object;
				if (@object == null)
				{
					return;
				}
				Cell currentCell = @object.CurrentCell;
				if (currentCell == null || currentCell.ParentZone == null || currentCell.ParentZone.IsWorldMap())
				{
					return;
				}
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage.HasAttribute("Unavoidable"))
				{
					return;
				}
				if (!IComponent<GameObject>.CheckRealityDistortionUsability(@object, null, @object, null, null, null, 0))
				{
					return;
				}
				if (this.Tier.in100())
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Owner");
					if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
					{
						MessageQueue.AddPlayerMessage("Fate intervenes and you deal no damage to " + @object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", 'r', true);
					}
					damage.Amount = 0;
					@object.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
				}
			}
		}

		// Token: 0x0400396F RID: 14703
		public int Tier = 1;
	}
}
