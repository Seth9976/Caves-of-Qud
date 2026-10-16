using System;
using XRL.World.Parts;

namespace XRL.World.Quests
{
	// Token: 0x02000B21 RID: 2849
	[Serializable]
	public class ArgyveAnotherKnicknackSystem : ArgyveKnicknackSystem
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x002B6380 File Offset: 0x002B4580
		public override string StepID
		{
			get
			{
				return "Find Another Knickknack";
			}
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x002B6388 File Offset: 0x002B4588
		public override void Finish()
		{
			base.Finish();
			Zone zone = The.ZoneManager.GetZone("JoppaWorld.11.22.1.1.10");
			for (int i = 18; i <= 20; i++)
			{
				for (int j = 4; j <= 11; j++)
				{
					Cell.ObjectRack objects = zone.Map[j][i].Objects;
					for (int k = objects.Count - 1; k >= 0; k--)
					{
						GameObject gameObject = objects[k];
						Physics physics = gameObject.Physics;
						if (((physics != null) ? physics.Owner : null) == "Joppa")
						{
							gameObject.Physics.Owner = null;
						}
					}
				}
			}
		}
	}
}
