using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E84 RID: 3716
	[Serializable]
	public class MoltingBasilisk : IPart
	{
		// Token: 0x060095CD RID: 38349 RVA: 0x00362E80 File Offset: 0x00361080
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == SingletonEvent<BeginTakeActionEvent>.ID && !this.Created) || ID == EnteredCellEvent.ID || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x060095CE RID: 38350 RVA: 0x00362EAE File Offset: 0x003610AE
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.SyncState();
			return base.HandleEvent(E);
		}

		// Token: 0x060095CF RID: 38351 RVA: 0x00362EBD File Offset: 0x003610BD
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.SyncState();
			return base.HandleEvent(E);
		}

		// Token: 0x060095D0 RID: 38352 RVA: 0x00362ECC File Offset: 0x003610CC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Created)
			{
				this.Created = true;
				Zone currentZone = this.ParentObject.CurrentZone;
				if (currentZone != null && !currentZone.Built)
				{
					int num = Stat.Random(7, 11);
					for (int i = 0; i < num; i++)
					{
						int num2 = 20;
						while (num2-- > 0)
						{
							int num3 = Stat.Random(0, currentZone.Width - 1);
							int num4 = Stat.Random(0, currentZone.Height - 1);
							Cell cell = currentZone.GetCell(num3, num4);
							if (cell.IsEmpty())
							{
								cell.AddObject("Molting Basilisk Husk", null, null, null, null);
								break;
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095D1 RID: 38353 RVA: 0x00362F73 File Offset: 0x00361173
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AICombatStart");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095D2 RID: 38354 RVA: 0x00362F88 File Offset: 0x00361188
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AICombatStart")
			{
				this.SyncState();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060095D3 RID: 38355 RVA: 0x00362FAC File Offset: 0x003611AC
		public void SyncState()
		{
			Description part = this.ParentObject.GetPart<Description>();
			if (this.ParentObject.Target == null)
			{
				part.Short = "The sloughed off skin is dull quartz and statuesque.";
				this.ParentObject.DisplayName = "molting basilisk husk";
				this.ParentObject.SetIntProperty("HideCon", 1, false);
				return;
			}
			part.Short = "A lizard of quartz scales reposes in the stillness of an artist's mould. When prey gets too comfortable with =pronouns.possessive= lifelessness and traipeses by, =pronouns.subjective= =verb:quicken:afterpronoun= and snaps like a thunder clap.";
			this.ParentObject.DisplayName = "molting basilisk";
			this.ParentObject.RemoveIntProperty("HideCon");
		}

		// Token: 0x04003DEF RID: 15855
		public bool Created;

		// Token: 0x04003DF0 RID: 15856
		public int Puffed;
	}
}
