using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C3D RID: 3133
	[Serializable]
	public class BlinkingTicSickness : Effect, ITierInitialized
	{
		// Token: 0x06007FD2 RID: 32722 RVA: 0x002F1C7E File Offset: 0x002EFE7E
		public BlinkingTicSickness()
		{
			this.DisplayName = "{{B|acute blinking tic}}";
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x002F1C91 File Offset: 0x002EFE91
		public BlinkingTicSickness(int duration)
			: this()
		{
			this.Duration = duration;
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x002F1CA0 File Offset: 0x002EFEA0
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Roll(960, 1440);
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x002F1CB7 File Offset: 0x002EFEB7
		public override string GetDescription()
		{
			return "{{B|blinking}}";
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x002F1CBE File Offset: 0x002EFEBE
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public override int GetEffectType()
		{
			return 117440768;
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x002F1CC8 File Offset: 0x002EFEC8
		public override string GetDetails()
		{
			return "There is a small chance each round you're in combat that you randomly teleport to a nearby location.";
		}

		// Token: 0x06007FD9 RID: 32729 RVA: 0x002F1CD0 File Offset: 0x002EFED0
		public override bool Apply(GameObject Object)
		{
			BlinkingTicSickness effect = Object.GetEffect<BlinkingTicSickness>();
			if (effect != null)
			{
				effect.Duration = Math.Max(this.Duration, effect.Duration);
				return false;
			}
			Object.RegisterEffectEvent(this, "EndTurn");
			return true;
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x002F1D0D File Offset: 0x002EFF0D
		public override void Remove(GameObject Object)
		{
			Object.UnregisterEffectEvent(this, "EndTurn");
			base.Remove(Object);
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x002F1D24 File Offset: 0x002EFF24
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "EndTurn"))
			{
				return base.FireEvent(E);
			}
			this.Duration--;
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return true;
			}
			if (base.Object.IsPlayer() && !base.Object.AreHostilesNearby())
			{
				return true;
			}
			if (!base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0))
			{
				return true;
			}
			if (2.in1000())
			{
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You lurch suddenly!", 'r', true);
				}
				base.Object.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
			}
			return true;
		}
	}
}
