using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F2D RID: 3885
	[Serializable]
	public class CyberneticsHasImplants : IPart
	{
		// Token: 0x06009ADE RID: 39646 RVA: 0x00382618 File Offset: 0x00380818
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009ADF RID: 39647 RVA: 0x0038261B File Offset: 0x0038081B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009AE0 RID: 39648 RVA: 0x00382634 File Offset: 0x00380834
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			Body body = this.ParentObject.Body;
			if (body != null)
			{
				string[] array = this.Implants.Split(',', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split('@', StringSplitOptions.None);
					GameObject gameObject = GameObject.Create(array2[0]);
					BodyPart partByNameWithoutCybernetics = body.GetPartByNameWithoutCybernetics(array2[1]);
					if (partByNameWithoutCybernetics != null)
					{
						partByNameWithoutCybernetics.Implant(gameObject, false, false);
					}
					else
					{
						gameObject.Obliterate(null, false, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004055 RID: 16469
		public string Implants = "";
	}
}
