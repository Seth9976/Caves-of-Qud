using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020010BD RID: 4285
	[Serializable]
	public class WaterRitualDiscount : IActivePart
	{
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600AAAB RID: 43691 RVA: 0x003E6BEC File Offset: 0x003E4DEC
		public string[] SplitTypes
		{
			get
			{
				if (this._SplitTypes == null)
				{
					if (string.IsNullOrEmpty(this.Types))
					{
						this._SplitTypes = new string[0];
					}
					else
					{
						this._SplitTypes = this.Types.Split(',', StringSplitOptions.None);
					}
				}
				return this._SplitTypes;
			}
		}

		// Token: 0x0600AAAC RID: 43692 RVA: 0x003E6C2B File Offset: 0x003E4E2B
		public WaterRitualDiscount()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600AAAD RID: 43693 RVA: 0x003E6C41 File Offset: 0x003E4E41
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<GetWaterRitualCostEvent>.ID;
		}

		// Token: 0x0600AAAE RID: 43694 RVA: 0x003E6C60 File Offset: 0x003E4E60
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.SplitTypes.Length != 0)
			{
				E.Postfix.Compound("{{rules|", '\n').AppendSigned(-this.Percent).Append("% reputation cost for ");
				int i = 0;
				int num = this.SplitTypes.Length;
				while (i < num)
				{
					if (i != 0)
					{
						if (i == num - 1)
						{
							E.Postfix.Append(", and ");
						}
						else
						{
							E.Postfix.Append(", ");
						}
					}
					E.Postfix.Append(WaterRitualDiscount.GetTypeDesc(this.SplitTypes[i]));
					i++;
				}
				E.Postfix.Append(" via the water ritual");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAAF RID: 43695 RVA: 0x003E6D18 File Offset: 0x003E4F18
		public static string GetTypeDesc(string Type)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Type);
			if (num <= 1769848520U)
			{
				if (num != 526680070U)
				{
					if (num != 805458841U)
					{
						if (num == 1769848520U)
						{
							if (Type == "TinkerRecipe")
							{
								return "learning tinker blueprints";
							}
						}
					}
					else if (Type == "Join")
					{
						return "recruiting creatures";
					}
				}
				else if (Type == "Item")
				{
					return "buying items";
				}
			}
			else if (num <= 2895110961U)
			{
				if (num != 1875984854U)
				{
					if (num == 2895110961U)
					{
						if (Type == "Secret")
						{
							return "sharing secrets";
						}
					}
				}
				else if (Type == "Skill")
				{
					return "learning skills";
				}
			}
			else if (num != 2972600418U)
			{
				if (num == 3211256865U)
				{
					if (Type == "CookingRecipe")
					{
						return "learning cooking recipes";
					}
				}
			}
			else if (Type == "Mutation")
			{
				return "gaining mutations";
			}
			return "buying " + Grammar.Pluralize(Type.ToLower());
		}

		// Token: 0x0600AAB0 RID: 43696 RVA: 0x003E6E34 File Offset: 0x003E5034
		public override bool HandleEvent(GetWaterRitualCostEvent E)
		{
			if (this.SplitTypes.Contains(E.Type) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Cost = Math.Max(0, E.Cost * (100 - this.Percent) / 100);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004655 RID: 18005
		public string Types;

		// Token: 0x04004656 RID: 18006
		public int Percent;

		// Token: 0x04004657 RID: 18007
		private string[] _SplitTypes;
	}
}
