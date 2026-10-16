using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001022 RID: 4130
	[Serializable]
	public class EmergencyTeleporter : IPoweredPart
	{
		// Token: 0x0600A479 RID: 42105 RVA: 0x003C2050 File Offset: 0x003C0250
		public EmergencyTeleporter()
		{
			this.WorksOnEquipper = true;
			this.ChargeUse = 100;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			base.IsTechScannable = true;
		}

		// Token: 0x0600A47A RID: 42106 RVA: 0x003C2084 File Offset: 0x003C0284
		public EmergencyTeleporter(int Chance)
			: this()
		{
			this.Chance = Chance;
		}

		// Token: 0x0600A47B RID: 42107 RVA: 0x003C2093 File Offset: 0x003C0293
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600A47C RID: 42108 RVA: 0x003C20A9 File Offset: 0x003C02A9
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A47D RID: 42109 RVA: 0x003C20CC File Offset: 0x003C02CC
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A47E RID: 42110 RVA: 0x003C20CF File Offset: 0x003C02CF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A47F RID: 42111 RVA: 0x003C20F0 File Offset: 0x003C02F0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					goto IL_012D;
				}
				using (List<GameObject>.Enumerator enumerator = this.GetActivePartSubjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						if (!gameObject.OnWorldMap() && IComponent<GameObject>.CheckRealityDistortionUsability(gameObject, null, null, this.ParentObject, null, null, 0) && this.Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
						{
							gameObject.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
						}
					}
					goto IL_012D;
				}
			}
			if (E.ID == "Equipped")
			{
				E.GetGameObjectParameter("EquippingObject").RegisterPartEvent(this, "BeforeApplyDamage");
			}
			else if (E.ID == "Unequipped")
			{
				E.GetGameObjectParameter("UnequippingObject").UnregisterPartEvent(this, "BeforeApplyDamage");
			}
			IL_012D:
			return base.FireEvent(E);
		}

		// Token: 0x0400442A RID: 17450
		public int Chance = 100;
	}
}
