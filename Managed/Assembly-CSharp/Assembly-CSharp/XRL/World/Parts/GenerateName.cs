using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F5A RID: 3930
	[Serializable]
	public class GenerateName : IPart
	{
		// Token: 0x06009CB5 RID: 40117 RVA: 0x0038CBF0 File Offset: 0x0038ADF0
		public override bool SameAs(IPart p)
		{
			GenerateName generateName = p as GenerateName;
			return !(generateName.SpecialType != this.SpecialType) && !(generateName.NamingContext != this.NamingContext) && generateName.SpecialFaildown == this.SpecialFaildown && base.SameAs(p);
		}

		// Token: 0x06009CB6 RID: 40118 RVA: 0x0038CC45 File Offset: 0x0038AE45
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009CB7 RID: 40119 RVA: 0x0038CC5B File Offset: 0x0038AE5B
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ApplyName(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x06009CB8 RID: 40120 RVA: 0x0038CC70 File Offset: 0x0038AE70
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x0038CC73 File Offset: 0x0038AE73
		public override void AddedAfterCreation()
		{
			this.ApplyName(this.ParentObject);
		}

		// Token: 0x06009CBA RID: 40122 RVA: 0x0038CC84 File Offset: 0x0038AE84
		public void ApplyName(GameObject Object)
		{
			string text = null;
			bool flag = false;
			string specialType = this.SpecialType;
			bool flag2 = false;
			Dictionary<string, string> dictionary = (this.NamingContext.IsNullOrEmpty() ? null : this.NamingContext.CachedDictionaryExpansion());
			Object.GiveProperName(text, flag, specialType, flag2, null, null, dictionary, null);
		}

		// Token: 0x040040CD RID: 16589
		public string SpecialType;

		// Token: 0x040040CE RID: 16590
		public string NamingContext;

		// Token: 0x040040CF RID: 16591
		public bool SpecialFaildown = true;
	}
}
