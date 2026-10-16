using System;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001211 RID: 4625
	[Serializable]
	public class RefractLight : IPart
	{
		// Token: 0x0600B7DB RID: 47067 RVA: 0x0042E538 File Offset: 0x0042C738
		public override bool SameAs(IPart Part)
		{
			RefractLight refractLight = Part as RefractLight;
			return refractLight.Chance == this.Chance && !(refractLight.RetroVariance != this.RetroVariance) && !(refractLight.Verb != this.Verb) && refractLight.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(Part);
		}

		// Token: 0x0600B7DC RID: 47068 RVA: 0x0042E59D File Offset: 0x0042C79D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B7DD RID: 47069 RVA: 0x0042E5CB File Offset: 0x0042C7CB
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7DE RID: 47070 RVA: 0x0042E5E5 File Offset: 0x0042C7E5
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "RefractLight");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7DF RID: 47071 RVA: 0x0042E600 File Offset: 0x0042C800
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

		// Token: 0x0600B7E0 RID: 47072 RVA: 0x0042E75F File Offset: 0x0042C95F
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B7E1 RID: 47073 RVA: 0x0042E762 File Offset: 0x0042C962
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("RefractLight");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7E2 RID: 47074 RVA: 0x0042E778 File Offset: 0x0042C978
		public override bool FireEvent(Event E)
		{
			if (E.ID == "RefractLight" && this.Chance.in100())
			{
				base.PlayWorldSound("sfx_light_refract", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
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

		// Token: 0x040049FE RID: 18942
		public int Chance = 100;

		// Token: 0x040049FF RID: 18943
		public string RetroVariance;

		// Token: 0x04004A00 RID: 18944
		public string Verb;

		// Token: 0x04004A01 RID: 18945
		public bool ShowInShortDescription;
	}
}
