using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E9F RID: 3743
	[Serializable]
	public class Displacement : IActivePart
	{
		// Token: 0x06009671 RID: 38513 RVA: 0x003682F8 File Offset: 0x003664F8
		public Displacement()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "SpatialTransposer";
		}

		// Token: 0x06009672 RID: 38514 RVA: 0x00368328 File Offset: 0x00366528
		public override bool SameAs(IPart p)
		{
			Displacement displacement = p as Displacement;
			return !(displacement.Distance != this.Distance) && displacement.Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x06009673 RID: 38515 RVA: 0x00368368 File Offset: 0x00366568
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009674 RID: 38516 RVA: 0x0036836B File Offset: 0x0036656B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009675 RID: 38517 RVA: 0x00368380 File Offset: 0x00366580
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && this.Chance.in100())
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject = enumerator.Current;
							int num = this.Distance.RollCached();
							if (num > 0 && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
							{
								GameObject gameObject2 = gameObject;
								int num2 = num;
								bool flag = !gameObject.IsPlayer();
								gameObject2.RandomTeleport(IComponent<GameObject>.Visible(gameObject), this, null, null, E, 0, num2, flag, null, false, true, true, false);
							}
						}
						goto IL_013E;
					}
				}
				int num3 = this.Distance.RollCached();
				if (num3 > 0)
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					if (activePartFirstSubject != null && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						GameObject gameObject3 = activePartFirstSubject;
						int num2 = num3;
						bool flag = !activePartFirstSubject.IsPlayer();
						gameObject3.RandomTeleport(IComponent<GameObject>.Visible(activePartFirstSubject), this, null, null, E, 0, num2, flag, null, false, true, true, false);
					}
				}
			}
			IL_013E:
			return base.FireEvent(E);
		}

		// Token: 0x04003E58 RID: 15960
		public string Distance = "0-2";

		// Token: 0x04003E59 RID: 15961
		public int Chance = 100;
	}
}
