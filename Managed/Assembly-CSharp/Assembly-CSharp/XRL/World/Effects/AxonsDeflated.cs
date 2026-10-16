using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000C36 RID: 3126
	[Serializable]
	public class AxonsDeflated : Effect, ITierInitialized
	{
		// Token: 0x06007F61 RID: 32609 RVA: 0x002F016D File Offset: 0x002EE36D
		public AxonsDeflated()
		{
			this.DisplayName = "{{r|sluggish}}";
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x002F0180 File Offset: 0x002EE380
		public AxonsDeflated(int Duration, int Penalty, string Source)
			: this()
		{
			this.Duration = Duration;
			this.Penalty = Penalty;
			this.Source = Source;
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x002F019D File Offset: 0x002EE39D
		public void Initialize(int Tier)
		{
			this.Duration = 10;
			this.Penalty = 10;
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x002F01AF File Offset: 0x002EE3AF
		public override int GetEffectType()
		{
			return 117448704;
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x002F01B6 File Offset: 0x002EE3B6
		public override string GetDetails()
		{
			return "-" + this.Penalty.ToString() + " Quickness";
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x002F01D4 File Offset: 0x002EE3D4
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyAxonsDeflated", "Effect", this)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You start to feel sluggish.", 'r', true);
			}
			base.StatShifter.SetStatShift("Speed", -this.Penalty, false);
			return true;
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x002F022A File Offset: 0x002EE42A
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x002F0237 File Offset: 0x002EE437
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06007F69 RID: 32617 RVA: 0x002F0250 File Offset: 0x002EE450
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject @object = base.Object;
			Cell cell = ((@object != null) ? @object.CurrentCell : null);
			if (cell == null || cell.OnWorldMap())
			{
				this.Duration = 0;
			}
			else if (this.Duration > 0 && this.Duration != 9999)
			{
				this.Duration--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x002F02AF File Offset: 0x002EE4AF
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame % 20 > 10)
			{
				E.RenderString = "\u0003";
				E.ColorString = "&r";
			}
			return true;
		}

		// Token: 0x040038CC RID: 14540
		public int Penalty;

		// Token: 0x040038CD RID: 14541
		public string Source;
	}
}
