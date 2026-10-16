using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FCD RID: 4045
	public abstract class IObjectGasBehavior : IGasBehavior
	{
		// Token: 0x0600A0D5 RID: 41173 RVA: 0x003A85D1 File Offset: 0x003A67D1
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A0D6 RID: 41174 RVA: 0x003A85D4 File Offset: 0x003A67D4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A0D7 RID: 41175 RVA: 0x003A85EC File Offset: 0x003A67EC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A0D8 RID: 41176 RVA: 0x003A8640 File Offset: 0x003A6840
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A0D9 RID: 41177 RVA: 0x003A8656 File Offset: 0x003A6856
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Type != "Thrown")
			{
				this.ApplyGas(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0DA RID: 41178 RVA: 0x003A867E File Offset: 0x003A687E
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A0DB RID: 41179 RVA: 0x003A8681 File Offset: 0x003A6881
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ApplyGas(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600A0DC RID: 41180 RVA: 0x003A8698 File Offset: 0x003A6898
		public virtual bool ApplyGas(Cell Cell)
		{
			bool flag = false;
			if (Cell != null)
			{
				Cell.ObjectRack objects = Cell.Objects;
				int i = 0;
				int num = objects.Count;
				while (i < num)
				{
					GameObject gameObject = objects[i];
					if (this.ApplyGas(gameObject))
					{
						flag = true;
						if (num != objects.Count)
						{
							num = objects.Count;
							if (i < num && objects[i] != gameObject)
							{
								i--;
							}
						}
					}
					i++;
				}
			}
			return flag;
		}

		// Token: 0x0600A0DD RID: 41181 RVA: 0x003A86FD File Offset: 0x003A68FD
		public virtual bool ApplyGas(GameObject Object)
		{
			return false;
		}
	}
}
