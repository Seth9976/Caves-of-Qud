using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E41 RID: 3649
	[Serializable]
	public class CommerceRangeValue : IPart
	{
		// Token: 0x060093F1 RID: 37873 RVA: 0x0035404D File Offset: 0x0035224D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060093F2 RID: 37874 RVA: 0x00354050 File Offset: 0x00352250
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x060093F3 RID: 37875 RVA: 0x00354068 File Offset: 0x00352268
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
			{
				if (!this.ParentObject.HasPart<Commerce>())
				{
					this.ParentObject.AddPart<Commerce>(new Commerce(), true, false);
				}
				this.ParentObject.GetPart<Commerce>().Value = (double)Stat.Roll(this.Range, null);
				this.ParentObject.RemovePart(this);
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D33 RID: 15667
		public string Range = "1";
	}
}
