using System;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000F43 RID: 3907
	[Serializable]
	public class CyberneticsRefractLight : IPart
	{
		// Token: 0x06009BD8 RID: 39896 RVA: 0x00388548 File Offset: 0x00386748
		public override bool SameAs(IPart p)
		{
			RefractLight refractLight = p as RefractLight;
			return refractLight.Chance == this.Chance && !(refractLight.RetroVariance != this.RetroVariance) && !(refractLight.Verb != this.Verb) && refractLight.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x06009BD9 RID: 39897 RVA: 0x003885AD File Offset: 0x003867AD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BDA RID: 39898 RVA: 0x003885DB File Offset: 0x003867DB
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x06009BDB RID: 39899 RVA: 0x003885F5 File Offset: 0x003867F5
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x06009BDC RID: 39900 RVA: 0x00388610 File Offset: 0x00386810
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				if (this.Chance >= 100)
				{
					if (this.Verb.IsNullOrEmpty())
					{
						stringBuilder.Append("Refracts");
					}
					else
					{
						stringBuilder.Append(ColorUtility.CapitalizeExceptFormatting(Grammar.ThirdPerson(this.Verb, false)));
					}
				}
				else
				{
					stringBuilder.Append("Has ").Append(Grammar.A(this.Chance, false)).Append("% chance to ")
						.Append(this.Verb ?? "refract");
				}
				stringBuilder.Append(" light-based attacks, sending them");
				if (this.RetroVariance.IsNullOrEmpty())
				{
					stringBuilder.Append(" in a random direction");
				}
				else
				{
					int num = this.RetroVariance.RollMinCached();
					int num2 = this.RetroVariance.RollMaxCached();
					int num3 = Math.Abs(num);
					int num4 = Math.Abs(num2);
					if (num == num2)
					{
						if (num == 0)
						{
							stringBuilder.Append(" back the way they came");
						}
						else
						{
							stringBuilder.Append(" away at an angle ").Append(num3).Append(" off from the way they came");
						}
					}
					else
					{
						stringBuilder.Append(" back the way they came, plus or minus up to ").Append(Math.Max(num3, num4)).Append(" degrees");
					}
				}
				stringBuilder.Append('.');
				E.Postfix.AppendRules(stringBuilder.ToString());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BDD RID: 39901 RVA: 0x0038876F File Offset: 0x0038696F
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009BDE RID: 39902 RVA: 0x00388772 File Offset: 0x00386972
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("RefractLight");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009BDF RID: 39903 RVA: 0x00388788 File Offset: 0x00386988
		public override bool FireEvent(Event E)
		{
			if (E.ID == "RefractLight" && this.Chance.in100())
			{
				E.SetParameter("By", this.ParentObject);
				if (!this.RetroVariance.IsNullOrEmpty())
				{
					float num = (float)E.GetParameter("Angle");
					E.SetParameter("Direction", (int)num + 180 + this.RetroVariance.RollCached());
				}
				if (!this.Verb.IsNullOrEmpty())
				{
					E.SetParameter("Verb", this.Verb);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400408C RID: 16524
		public int Chance = 100;

		// Token: 0x0400408D RID: 16525
		public string RetroVariance;

		// Token: 0x0400408E RID: 16526
		public string Verb;

		// Token: 0x0400408F RID: 16527
		public bool ShowInShortDescription;
	}
}
