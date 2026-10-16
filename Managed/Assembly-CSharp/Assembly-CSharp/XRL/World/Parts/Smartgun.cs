using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02001122 RID: 4386
	[Serializable]
	public class Smartgun : IPoweredPart
	{
		// Token: 0x0600AD87 RID: 44423 RVA: 0x00403DB4 File Offset: 0x00401FB4
		public Smartgun()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600AD88 RID: 44424 RVA: 0x00403DE0 File Offset: 0x00401FE0
		public override bool SameAs(IPart p)
		{
			Smartgun smartgun = p as Smartgun;
			return smartgun.Level == this.Level && !(smartgun.EquipperPropertyEnables != this.EquipperPropertyEnables) && !(smartgun.EquipperEventEnables != this.EquipperEventEnables) && base.SameAs(p);
		}

		// Token: 0x0600AD89 RID: 44425 RVA: 0x00403E35 File Offset: 0x00402035
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x0600AD8A RID: 44426 RVA: 0x00403E4C File Offset: 0x0040204C
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Checking == "Actor" && this.IsObjectActivePartSubject(E.Actor) && E.Target == E.AimedAt && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && !E.Prospective && this.UseTurn < XRLCore.Core.Game.Turns)
			{
				this.UseTurn = XRLCore.Core.Game.Turns;
				base.ConsumeCharge(null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD8B RID: 44427 RVA: 0x00403F09 File Offset: 0x00402109
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ModifyAimVariance");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD8C RID: 44428 RVA: 0x00403F20 File Offset: 0x00402120
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ModifyAimVariance" && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.UseTurn < XRLCore.Core.Game.Turns)
				{
					this.UseTurn = XRLCore.Core.Game.Turns;
					base.ConsumeCharge(null, null);
				}
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) - this.Level);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD8D RID: 44429 RVA: 0x00403FD5 File Offset: 0x004021D5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AD8E RID: 44430 RVA: 0x00403FD8 File Offset: 0x004021D8
		public bool IsDataReady()
		{
			GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
			if (gameObject == null)
			{
				return false;
			}
			if (!this.EquipperPropertyEnables.IsNullOrEmpty() && gameObject.GetIntProperty(this.EquipperPropertyEnables, 0) > 0)
			{
				return true;
			}
			if (!this.EquipperEventEnables.IsNullOrEmpty())
			{
				if (this.EquipperEventEnables == Smartgun.eHandleSmartData.ID)
				{
					if (!gameObject.FireEvent(Smartgun.eHandleSmartData))
					{
						return true;
					}
				}
				else if (!gameObject.FireEvent(this.EquipperEventEnables))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600AD8F RID: 44431 RVA: 0x00404068 File Offset: 0x00402268
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return !this.IsDataReady();
		}

		// Token: 0x0600AD90 RID: 44432 RVA: 0x00404073 File Offset: 0x00402273
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "DataProviderNotFound";
		}

		// Token: 0x040047CC RID: 18380
		public int Level = 1;

		// Token: 0x040047CD RID: 18381
		public string EquipperPropertyEnables = "TechScannerEquipped";

		// Token: 0x040047CE RID: 18382
		public string EquipperEventEnables = "HandleSmartData";

		// Token: 0x040047CF RID: 18383
		public long UseTurn;

		// Token: 0x040047D0 RID: 18384
		[NonSerialized]
		public static Event eHandleSmartData = new ImmutableEvent("HandleSmartData");
	}
}
