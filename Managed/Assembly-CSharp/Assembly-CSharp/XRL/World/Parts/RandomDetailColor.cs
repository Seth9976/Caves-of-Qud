using System;

namespace XRL.World.Parts
{
	// Token: 0x020012E5 RID: 4837
	[Serializable]
	public class RandomDetailColor : IPart
	{
		// Token: 0x0600BE38 RID: 48696 RVA: 0x0045B588 File Offset: 0x00459788
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BE39 RID: 48697 RVA: 0x0045B58B File Offset: 0x0045978B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BE3A RID: 48698 RVA: 0x0045B5A0 File Offset: 0x004597A0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectCreated")
			{
				if (!this.ParentObject.HasPart<Render>())
				{
					this.ParentObject.AddPart<Render>(new Render(), true, false);
				}
				char[] array = new char[]
				{
					'y', 'Y', 'b', 'B', 'c', 'C', 'r', 'R', 'g', 'G',
					'm', 'M', 'k', 'w', 'W'
				};
				this.ParentObject.GetPart<Render>().DetailColor = array.GetRandomElement<char>(null).ToString();
				this.ParentObject.RemovePart(this);
				return true;
			}
			return base.FireEvent(E);
		}
	}
}
