using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001014 RID: 4116
	[Serializable]
	public class DefensiveStasisGenerator : IPoweredPart
	{
		// Token: 0x0600A40B RID: 41995 RVA: 0x003BF94C File Offset: 0x003BDB4C
		public DefensiveStasisGenerator()
		{
			this.WorksOnEquipper = true;
			this.IsRealityDistortionBased = true;
		}

		// Token: 0x0600A40C RID: 41996 RVA: 0x003BF9A4 File Offset: 0x003BDBA4
		public override bool SameAs(IPart p)
		{
			DefensiveStasisGenerator defensiveStasisGenerator = p as DefensiveStasisGenerator;
			return defensiveStasisGenerator.Chance == this.Chance && !(defensiveStasisGenerator.Duration != this.Duration) && !(defensiveStasisGenerator.Blueprint != this.Blueprint) && !(defensiveStasisGenerator.AppearVerb != this.AppearVerb) && !(defensiveStasisGenerator.AppearExtra != this.AppearExtra) && !(defensiveStasisGenerator.AppearEndMark != this.AppearEndMark) && defensiveStasisGenerator.InterruptsTrigger == this.InterruptsTrigger && defensiveStasisGenerator.PhaseSync == this.PhaseSync && base.SameAs(p);
		}

		// Token: 0x0600A40D RID: 41997 RVA: 0x003BFA58 File Offset: 0x003BDC58
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600A40E RID: 41998 RVA: 0x003BFA78 File Offset: 0x003BDC78
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (!E.Damage.HasAttribute("Unstoppable") && this.IsObjectActivePartSubject(E.Object))
			{
				Cell currentCell = E.Object.CurrentCell;
				if (currentCell != null && !currentCell.HasObject(this.Blueprint))
				{
					int num = this.Duration.RollCached();
					if (num > 0 && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						GameObject gameObject;
						if ((gameObject = this.ParentObject.Equipped) == null)
						{
							gameObject = this.ParentObject.Implantee ?? this.ParentObject;
						}
						GameObject gameObject2 = gameObject;
						GameObject gameObject3 = GameObject.Create(this.Blueprint);
						Forcefield part = gameObject3.GetPart<Forcefield>();
						if (part != null && gameObject2 != null)
						{
							part.Creator = gameObject2;
						}
						if (num != 9999)
						{
							gameObject3.AddPart<Temporary>(new Temporary(num + 1), true, false);
						}
						if (this.PhaseSync && gameObject2 != null)
						{
							Phase.carryOver(gameObject2, gameObject3);
						}
						currentCell.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						IComponent<GameObject>.XDidY(gameObject3, this.AppearVerb, this.AppearExtra, this.AppearEndMark, null, null, null, null, false, true, null, null, false, false, false, false, false, null);
						if (this.InterruptsTrigger)
						{
							return false;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A40F RID: 41999 RVA: 0x003BFBD3 File Offset: 0x003BDDD3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("glass", 1);
				E.Add("ice", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040043E6 RID: 17382
		public int Chance = 100;

		// Token: 0x040043E7 RID: 17383
		public string Duration = "1";

		// Token: 0x040043E8 RID: 17384
		public string Blueprint = "Stasisfield";

		// Token: 0x040043E9 RID: 17385
		public string AppearVerb = "appear";

		// Token: 0x040043EA RID: 17386
		public string AppearExtra;

		// Token: 0x040043EB RID: 17387
		public string AppearEndMark;

		// Token: 0x040043EC RID: 17388
		public bool InterruptsTrigger = true;

		// Token: 0x040043ED RID: 17389
		public bool PhaseSync = true;
	}
}
