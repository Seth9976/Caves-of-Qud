using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC1 RID: 3009
	[Serializable]
	public class Anatomy
	{
		// Token: 0x060078F4 RID: 30964 RVA: 0x002DA8A0 File Offset: 0x002D8AA0
		public Anatomy(string Name)
		{
			this.Name = Name;
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x002DA8DC File Offset: 0x002D8ADC
		public void ApplyTo(Body body)
		{
			body.built = false;
			body._Anatomy = this.Name;
			body.DismemberedParts = null;
			BodyPart bodyPart = new BodyPart(this.BodyType, body, this.Parts.Count);
			body._Body = bodyPart;
			if (this.BodyCategory != null)
			{
				bodyPart.Category = this.BodyCategory.Value;
			}
			if (this.BodyMobility != null)
			{
				bodyPart.Mobility = this.BodyMobility.Value;
			}
			foreach (AnatomyPart anatomyPart in this.Parts)
			{
				anatomyPart.ApplyTo(bodyPart);
			}
			if (!this.ThrownWeapon.IsNullOrEmpty())
			{
				bodyPart.AddPart(this.ThrownWeapon, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, true);
			}
			if (!this.FloatingNearby.IsNullOrEmpty())
			{
				bodyPart.AddPart(this.FloatingNearby, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, true);
			}
			if (this.Category != null)
			{
				bodyPart.CategorizeAll(this.Category.Value);
			}
			body.MarkAllNative();
			body.built = true;
			body.UpdateBodyParts(0);
		}

		// Token: 0x060078F6 RID: 30966 RVA: 0x002DAB08 File Offset: 0x002D8D08
		public bool Contains(BodyPartType type)
		{
			using (List<AnatomyPart>.Enumerator enumerator = this.Parts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400370A RID: 14090
		public string Name;

		// Token: 0x0400370B RID: 14091
		public List<AnatomyPart> Parts = new List<AnatomyPart>();

		// Token: 0x0400370C RID: 14092
		public int? Category;

		// Token: 0x0400370D RID: 14093
		public string BodyType = "Body";

		// Token: 0x0400370E RID: 14094
		public int? BodyCategory;

		// Token: 0x0400370F RID: 14095
		public int? BodyMobility;

		// Token: 0x04003710 RID: 14096
		public string ThrownWeapon = "Thrown Weapon";

		// Token: 0x04003711 RID: 14097
		public string FloatingNearby = "Floating Nearby";
	}
}
