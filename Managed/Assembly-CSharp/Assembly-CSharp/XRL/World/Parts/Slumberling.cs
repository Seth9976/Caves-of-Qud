using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E8D RID: 3725
	[Serializable]
	public class Slumberling : IPart
	{
		// Token: 0x060095FB RID: 38395 RVA: 0x00364B46 File Offset: 0x00362D46
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060095FC RID: 38396 RVA: 0x00364B49 File Offset: 0x00362D49
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckHibernate(Amount);
		}

		// Token: 0x060095FD RID: 38397 RVA: 0x00364B53 File Offset: 0x00362D53
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && this.Initial);
		}

		// Token: 0x060095FE RID: 38398 RVA: 0x00364B71 File Offset: 0x00362D71
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Initial)
			{
				this.Initial = false;
				this.ParentObject.ApplyEffect(new Asleep(9999, false, false, true, true), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095FF RID: 38399 RVA: 0x00364BA4 File Offset: 0x00362DA4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009600 RID: 38400 RVA: 0x00364BA8 File Offset: 0x00362DA8
		public bool CheckHibernate(int Chances = 1)
		{
			if (this.ParentObject.HasEffect<Asleep>())
			{
				return true;
			}
			int i = 0;
			while (i < Chances)
			{
				if (10.in100())
				{
					if (this.ParentObject.ApplyEffect(new Asleep(9999, false, false, true, true), null))
					{
						base.DidX("lapse", "back into hibernation", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x04003E06 RID: 15878
		public bool Initial = true;
	}
}
