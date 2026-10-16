using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001141 RID: 4417
	[Serializable]
	public class ModGlassArmor : IModification
	{
		// Token: 0x0600AF48 RID: 44872 RVA: 0x0040AC53 File Offset: 0x00408E53
		public ModGlassArmor()
		{
		}

		// Token: 0x0600AF49 RID: 44873 RVA: 0x0040AC66 File Offset: 0x00408E66
		public ModGlassArmor(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF4A RID: 44874 RVA: 0x0040AC7A File Offset: 0x00408E7A
		public override void Configure()
		{
			this.WorksOnWearer = true;
		}

		// Token: 0x0600AF4B RID: 44875 RVA: 0x0040AC83 File Offset: 0x00408E83
		public override int GetModificationSlotUsage()
		{
			return 0;
		}

		// Token: 0x0600AF4C RID: 44876 RVA: 0x0040AC86 File Offset: 0x00408E86
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Armor>();
		}

		// Token: 0x0600AF4D RID: 44877 RVA: 0x0040AC8E File Offset: 0x00408E8E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF4E RID: 44878 RVA: 0x0040ACB4 File Offset: 0x00408EB4
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Damage.Amount > 0 && !E.Damage.HasAttribute("reflected") && !E.Damage.HasAttribute("Unavoidable") && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject equipped = this.ParentObject.Equipped;
				int num = (int)Math.Ceiling((double)((float)E.Damage.Amount * (float)this.Tier / 100f));
				if (num > 0 && E.Actor != null && E.Actor != this.ParentObject && E.Actor != equipped)
				{
					List<string> list = new List<string>(E.Damage.Attributes);
					if (!list.Contains("reflected"))
					{
						list.Add("reflected");
					}
					GameObject gameObject;
					if (equipped != null && equipped.IsPlayer())
					{
						string[] array = new string[6];
						int num2 = 0;
						GameObject parentObject = this.ParentObject;
						string text = "reflect";
						int maxValue = int.MaxValue;
						string text2 = null;
						string text3 = null;
						string text4 = null;
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						bool flag6 = true;
						bool flag7 = true;
						bool flag8 = false;
						bool flag9 = false;
						string text5 = null;
						bool flag10 = false;
						bool flag11 = false;
						bool flag12 = true;
						gameObject = equipped;
						array[num2] = parentObject.Does(text, maxValue, text2, text3, text4, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, text5, flag10, flag11, flag12, null, true, gameObject, false);
						array[1] = " ";
						array[2] = num.ToString();
						array[3] = " damage back at ";
						array[4] = E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
						array[5] = ".";
						IComponent<GameObject>.AddPlayerMessage(string.Concat(array), null, true);
					}
					GameObject actor = E.Actor;
					int num3 = num;
					string text6 = string.Join(" ", list.ToArray());
					gameObject = equipped ?? this.ParentObject;
					GameObject parentObject2 = this.ParentObject;
					actor.TakeDamage(num3, "from %t " + this.Type + " armor!", text6, null, null, null, gameObject, parentObject2, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					GameObject equipped2 = this.ParentObject.Equipped;
					if (equipped2 != null)
					{
						equipped2.FireEvent("ReflectedDamage");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF4F RID: 44879 RVA: 0x0040AEDB File Offset: 0x004090DB
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Reflects " + this.Tier.ToString() + "% damage back at your attackers, rounded up.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF50 RID: 44880 RVA: 0x0040AF0A File Offset: 0x0040910A
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("glass", 10);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF51 RID: 44881 RVA: 0x0040AF2E File Offset: 0x0040912E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040047FF RID: 18431
		public string Type = "glass";
	}
}
