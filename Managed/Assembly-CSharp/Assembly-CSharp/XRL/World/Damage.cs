using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XRL.World
{
	// Token: 0x02000946 RID: 2374
	[HasModSensitiveStaticCache]
	[Serializable]
	public class Damage
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x001BC742 File Offset: 0x001BA942
		// (set) Token: 0x06005BD1 RID: 23505 RVA: 0x001BC74A File Offset: 0x001BA94A
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = Math.Max(value, 0);
			}
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x001BC759 File Offset: 0x001BA959
		public static void SetAttributeSound(string attribute, string sounds)
		{
			if (Damage.AttributeSounds == null)
			{
				Damage.AttributeSounds = new Dictionary<string, string>();
			}
			Damage.AttributeSounds.Set(attribute, sounds);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x001BC778 File Offset: 0x001BA978
		[ModSensitiveCacheInit]
		private static void Init()
		{
			Damage.SetAttributeSound("Fire", "Sounds/Damage/sfx_damage_elemental_heat");
			Damage.SetAttributeSound("Heat", "Sounds/Damage/sfx_damage_elemental_heat");
			Damage.SetAttributeSound("Acid", "Sounds/Damage/sfx_damage_elemental_acid");
			Damage.SetAttributeSound("Cold", "Sounds/Damage/sfx_damage_elemental_cold");
			Damage.SetAttributeSound("Electric", "Sounds/Damage/sfx_damage_elemental_electrical");
			Damage.SetAttributeSound("Electricity", "Sounds/Damage/sfx_damage_elemental_electrical");
			Damage.SetAttributeSound("Electrical", "Sounds/Damage/sfx_damage_elemental_electrical");
			Damage.SetAttributeSound("Shock", "Sounds/Damage/sfx_damage_elemental_electrical");
			Damage.SetAttributeSound("Lightning", "Sounds/Damage/sfx_damage_elemental_electrical");
			Damage.SetAttributeSound("Light", "Sounds/Damage/sfx_damage_elemental_light");
			Damage.SetAttributeSound("Laser", "Sounds/Damage/sfx_damage_elemental_light");
			Damage.SetAttributeSound("Poison", "Sounds/Damage/sfx_damage_elemental_poison");
			Damage.SetAttributeSound("Asphyxiation", "Sounds/Damage/sfx_damage_elemental_poison");
			Damage.SetAttributeSound("Bleeding", "Sounds/Damage/sfx_damage_elemental_bleeding");
			Damage.SetAttributeSound("Mental", "Sounds/Damage/sfx_damage_elemental_mental");
			Damage.SetAttributeSound("Psionic", "Sounds/Damage/sfx_damage_elemental_mental");
			Damage.SetAttributeSound("Explosion", "Sounds/Damage/sfx_damage_explosive");
			Damage.SetAttributeSound("Explosive", "Sounds/Damage/sfx_damage_explosive");
			Damage.SetAttributeSound("Explosives", "Sounds/Damage/sfx_damage_explosive");
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x001BC8A4 File Offset: 0x001BAAA4
		public void PlaySound(GameObject target)
		{
			foreach (string text in (from _ in Damage.AttributeSounds
				where this.HasAttribute(_.Key)
				select _.Value).Distinct<string>())
			{
				target.PlayWorldSound(text, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x001BC94C File Offset: 0x001BAB4C
		public Damage(int Amount)
		{
			this.Amount = Amount;
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x001BC968 File Offset: 0x001BAB68
		public bool HasAnyAttribute(List<string> Names)
		{
			if (Names == null)
			{
				return false;
			}
			if (this.Attributes == null)
			{
				return false;
			}
			foreach (string text in this.Attributes)
			{
				if (Names.Contains(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001BC9D4 File Offset: 0x001BABD4
		public bool HasAttribute(string Name)
		{
			return this.Attributes != null && this.Attributes.Contains(Name);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x001BC9F1 File Offset: 0x001BABF1
		public void AddAttribute(string Name)
		{
			this.Attributes.Add(Name);
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x001BCA00 File Offset: 0x001BAC00
		public void AddAttributes(string List)
		{
			if (List.IsNullOrEmpty())
			{
				return;
			}
			if (List.Contains(" "))
			{
				foreach (string text in List.Split(' ', StringSplitOptions.None))
				{
					this.AddAttribute(text);
				}
				return;
			}
			this.AddAttribute(List);
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x001BCA4E File Offset: 0x001BAC4E
		public bool IsColdDamage()
		{
			return this.HasAttribute("Cold") || this.HasAttribute("Ice") || this.HasAttribute("Freeze");
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x001BCA77 File Offset: 0x001BAC77
		public bool IsHeatDamage()
		{
			return this.HasAttribute("Fire") || this.HasAttribute("Heat");
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x001BCA93 File Offset: 0x001BAC93
		public bool IsElectricDamage()
		{
			return this.HasAttribute("Electric") || this.HasAttribute("Shock") || this.HasAttribute("Lightning") || this.HasAttribute("Electricity");
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x001BCAC9 File Offset: 0x001BACC9
		public bool IsBludgeoningDamage()
		{
			return this.HasAttribute("Cudgel") || this.HasAttribute("Bludgeoning");
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x001BCAE5 File Offset: 0x001BACE5
		public bool IsAcidDamage()
		{
			return this.HasAttribute("Acid");
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x001BCAF2 File Offset: 0x001BACF2
		public bool IsLightDamage()
		{
			return this.HasAttribute("Light") || this.HasAttribute("Laser");
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x001BCB0E File Offset: 0x001BAD0E
		public bool IsDisintegrationDamage()
		{
			return this.HasAttribute("Disintegrate") || this.HasAttribute("Disintegration");
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x001BCB2A File Offset: 0x001BAD2A
		public static bool IsColdDamage(string Type)
		{
			return Type == "Cold" || Type == "Ice" || Type == "Freeze";
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x001BCB53 File Offset: 0x001BAD53
		public static bool IsHeatDamage(string Type)
		{
			return Type == "Fire" || Type == "Heat";
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x001BCB70 File Offset: 0x001BAD70
		public static bool IsElectricDamage(string Type)
		{
			return Type == "Electric" || Type == "Shock" || Type == "Lightning" || Type == "Electricity" || Type == "Electrical";
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x001BCBBE File Offset: 0x001BADBE
		public static bool IsAcidDamage(string Type)
		{
			return Type == "Acid";
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x001BCBCB File Offset: 0x001BADCB
		public static bool IsLightDamage(string Type)
		{
			return Type == "Light" || Type == "Laser";
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x001BCBE7 File Offset: 0x001BADE7
		public static bool IsDisintegrationDamage(string Type)
		{
			return Type == "Disintegrate" || Type == "Disintegration";
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x001BCC03 File Offset: 0x001BAE03
		public static bool ContainsColdDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Cold", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Ice", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Freeze", StringComparison.Ordinal);
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x001BCC35 File Offset: 0x001BAE35
		public static bool ContainsHeatDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Fire", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Heat", StringComparison.Ordinal);
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x001BCC58 File Offset: 0x001BAE58
		public static bool ContainsElectricDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Electric", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Shock", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Lightning", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Electricity", StringComparison.Ordinal);
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x001BCCA5 File Offset: 0x001BAEA5
		public static bool ContainsAcidDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Acid", StringComparison.Ordinal);
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x001BCCB5 File Offset: 0x001BAEB5
		public static bool ContainsLightDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Light", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Laser", StringComparison.Ordinal);
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x001BCCD7 File Offset: 0x001BAED7
		public static bool ContainsDisintegrationDamage(string Type)
		{
			return Type.HasDelimitedSubstring(' ', "Disintegrate", StringComparison.Ordinal) || Type.HasDelimitedSubstring(' ', "Disintegration", StringComparison.Ordinal);
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x001BCCF9 File Offset: 0x001BAEF9
		public static string GetDamageColor(string Attributes)
		{
			return "r";
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x001BCD00 File Offset: 0x001BAF00
		public string GetDebugInfo()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.Amount);
			if (this.Attributes.Count > 0)
			{
				bool flag = true;
				foreach (string text in this.Attributes)
				{
					stringBuilder.Append(flag ? ':' : '/').Append(text);
				}
			}
			if (this.SuppressionMessageDone)
			{
				stringBuilder.Append("(SMD)");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002D5D RID: 11613
		public int _Amount;

		// Token: 0x04002D5E RID: 11614
		public List<string> Attributes = new List<string>();

		// Token: 0x04002D5F RID: 11615
		public bool SuppressionMessageDone;

		// Token: 0x04002D60 RID: 11616
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, string> AttributeSounds;
	}
}
