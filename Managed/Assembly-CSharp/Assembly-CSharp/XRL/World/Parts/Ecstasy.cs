using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE0 RID: 3552
	[Serializable]
	public class Ecstasy : IPart
	{
		// Token: 0x06009053 RID: 36947 RVA: 0x003411A0 File Offset: 0x0033F3A0
		public override bool SameAs(IPart p)
		{
			return !((p as Ecstasy).LeaderBlueprint != this.LeaderBlueprint) && base.SameAs(p);
		}

		// Token: 0x06009054 RID: 36948 RVA: 0x003411C3 File Offset: 0x0033F3C3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x003411C6 File Offset: 0x0033F3C6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x003411DC File Offset: 0x0033F3DC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && currentCell.ParentZone != null)
				{
					using (List<GameObject>.Enumerator enumerator = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 6, "Combat", this.ParentObject, false, false, null).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Blueprint == this.LeaderBlueprint)
							{
								if (!this.ParentObject.HasEffect<Ecstatic>())
								{
									this.ParentObject.ApplyEffect(new Ecstatic(), null);
								}
								goto IL_00BF;
							}
						}
					}
				}
				this.ParentObject.RemoveEffect<Ecstatic>();
			}
			IL_00BF:
			return base.FireEvent(E);
		}

		// Token: 0x04003C2D RID: 15405
		public string LeaderBlueprint = "Mechanimist Priest 1";
	}
}
