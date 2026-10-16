using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001192 RID: 4498
	[Serializable]
	public class ModUrbanCamo : IModification
	{
		// Token: 0x0600B2DF RID: 45791 RVA: 0x00414F30 File Offset: 0x00413130
		public ModUrbanCamo()
		{
		}

		// Token: 0x0600B2E0 RID: 45792 RVA: 0x00414F38 File Offset: 0x00413138
		public ModUrbanCamo(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B2E1 RID: 45793 RVA: 0x00414F41 File Offset: 0x00413141
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B2E2 RID: 45794 RVA: 0x00414F4A File Offset: 0x0041314A
		public override bool ModificationApplicable(GameObject Object)
		{
			return (Object.HasPart<Armor>() || Object.HasPart<Shield>()) && !Object.HasPart<FoliageCamouflage>() && !Object.HasPart<UrbanCamouflage>();
		}

		// Token: 0x0600B2E3 RID: 45795 RVA: 0x00414F6F File Offset: 0x0041316F
		public override void ApplyModification(GameObject Object)
		{
			Object.RequirePart<UrbanCamouflage>(false).Level = Math.Max(this.Tier / 2, 1);
		}

		// Token: 0x0600B2E4 RID: 45796 RVA: 0x00414F8B File Offset: 0x0041318B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B2E5 RID: 45797 RVA: 0x00414FA9 File Offset: 0x004131A9
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{urban camouflage|urban camo}}", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2E6 RID: 45798 RVA: 0x00414FD4 File Offset: 0x004131D4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModUrbanCamo.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B2E7 RID: 45799 RVA: 0x00415008 File Offset: 0x00413208
		public string GetPhysicalDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("are", true, false, false, true, false)).Append(" covered in mottled gray, white, and black markings.");
			return stringBuilder.ToString();
		}

		// Token: 0x0600B2E8 RID: 45800 RVA: 0x00415056 File Offset: 0x00413256
		public static string GetDescription(int Tier)
		{
			return "Urban camo: This item provides camouflage in urban environments.";
		}

		// Token: 0x0600B2E9 RID: 45801 RVA: 0x0041505D File Offset: 0x0041325D
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&y", "K", ModUrbanCamo.ICON_COLOR_FOREGROUND_PRIORITY, ModUrbanCamo.ICON_COLOR_DETAIL_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x0400483E RID: 18494
		public static readonly int ICON_COLOR_FOREGROUND_PRIORITY = 50;

		// Token: 0x0400483F RID: 18495
		public static readonly int ICON_COLOR_DETAIL_PRIORITY = 70;
	}
}
