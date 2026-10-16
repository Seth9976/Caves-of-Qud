using System;
using XRL.Language;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F8 RID: 2552
	public static class StyledStatus
	{
		// Token: 0x06006CDA RID: 27866 RVA: 0x00236B44 File Offset: 0x00234D44
		public static string Format(string Name, string Value, string Style = "plain")
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Style);
			if (num <= 1867851206U)
			{
				if (num != 609338507U)
				{
					if (num != 1540020817U)
					{
						if (num == 1867851206U)
						{
							if (Style == "structure")
							{
								return Event.NewStringBuilder(null).Append("{{Y|[").Append(Name)
									.Append(": ")
									.Append(Value)
									.Append("]}}")
									.ToString();
							}
						}
					}
					else if (Style == "bio")
					{
						if (Value == "Operational")
						{
							Value = "Healthy";
						}
						return Event.NewStringBuilder(null).Append("{{g|[").Append(Name)
							.Append(": ")
							.Append(Value)
							.Append("]}}")
							.ToString();
					}
				}
				else if (Style == "leet")
				{
					return Event.NewStringBuilder(null).Append("{{C|[{{c|\\\\\\}}").Append(TextFilters.Leet(Name.ToUpper().Replace(" ", "/"), true))
						.Append("{{c|::}}")
						.Append(TextFilters.Leet(Value.ToUpper().Replace(" ", "%"), true))
						.Append("{{c|\\\\\\}}]}}")
						.ToString();
				}
			}
			else if (num <= 2751005041U)
			{
				if (num != 2552832768U)
				{
					if (num == 2751005041U)
					{
						if (Style == "tech")
						{
							return Event.NewStringBuilder(null).Append("{{C|[").Append(Name)
								.Append(": ")
								.Append(Value)
								.Append("]}}")
								.ToString();
						}
					}
				}
				else if (Style == "ooc")
				{
					return Event.NewStringBuilder(null).Append("{{rules|").Append(Name)
						.Append(": ")
						.Append(Value)
						.Append("}}")
						.ToString();
				}
			}
			else if (num != 3037376254U)
			{
				if (num == 3552460647U)
				{
					if (Style == "plain")
					{
						return Event.NewStringBuilder(null).Append(Name).Append(": ")
							.Append(Value)
							.ToString();
					}
				}
			}
			else if (Style == "angry")
			{
				return Event.NewStringBuilder(null).Append(TextFilters.Angry(Name)).Append(": ")
					.Append(TextFilters.Angry(Value))
					.ToString();
			}
			throw new Exception("invalid status style " + Style);
		}
	}
}
