using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001184 RID: 4484
	[Serializable]
	public class ModCamo : IModification
	{
		// Token: 0x0600B241 RID: 45633 RVA: 0x0041395D File Offset: 0x00411B5D
		public ModCamo()
		{
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x00413965 File Offset: 0x00411B65
		public ModCamo(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B243 RID: 45635 RVA: 0x0041396E File Offset: 0x00411B6E
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x00413977 File Offset: 0x00411B77
		public override bool ModificationApplicable(GameObject Object)
		{
			return (Object.HasPart<Armor>() || Object.HasPart<Shield>()) && !Object.HasPart<FoliageCamouflage>() && !Object.HasPart<UrbanCamouflage>();
		}

		// Token: 0x0600B245 RID: 45637 RVA: 0x0041399C File Offset: 0x00411B9C
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<FoliageCamouflage>(false).Level = Math.Max(this.Tier / 2, 1);
		}

		// Token: 0x0600B246 RID: 45638 RVA: 0x004139B8 File Offset: 0x00411BB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B247 RID: 45639 RVA: 0x004139D6 File Offset: 0x00411BD6
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{camouflage|camo}}", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B248 RID: 45640 RVA: 0x00413A01 File Offset: 0x00411C01
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModCamo.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B249 RID: 45641 RVA: 0x00413A34 File Offset: 0x00411C34
		public string GetPhysicalDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("are", true, false, false, true, false)).Append(" covered in mottled green, brown, gray, and black markings.");
			return stringBuilder.ToString();
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x00413A82 File Offset: 0x00411C82
		public static string GetDescription(int Tier)
		{
			return "Camo: This item provides camouflage in foliage.";
		}

		// Token: 0x0600B24B RID: 45643 RVA: 0x00413A89 File Offset: 0x00411C89
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&g", "K", ModCamo.ICON_COLOR_FOREGROUND_PRIORITY, ModCamo.ICON_COLOR_DETAIL_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x04004833 RID: 18483
		public static readonly int ICON_COLOR_FOREGROUND_PRIORITY = 40;

		// Token: 0x04004834 RID: 18484
		public static readonly int ICON_COLOR_DETAIL_PRIORITY = 80;
	}
}
