using System;
using XRL.Core;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E0 RID: 2528
	public static class ColorCoding
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06006BFC RID: 27644 RVA: 0x0022D8D4 File Offset: 0x0022BAD4
		private static GameObject ThePlayer
		{
			get
			{
				if (XRLCore.Core.Game == null)
				{
					return null;
				}
				return The.Player;
			}
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x0022D8EC File Offset: 0x0022BAEC
		public static char ConsequentialColorChar(GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			if (ColorAsGoodFor != null)
			{
				if (ColorAsGoodFor.IsPlayer())
				{
					return 'G';
				}
				if (ColorAsGoodFor.IsPlayerLed())
				{
					return 'g';
				}
				if (ColorAsGoodFor.IsAlliedTowards(ColorCoding.ThePlayer))
				{
					return 'g';
				}
				if (ColorAsGoodFor.Target == ColorCoding.ThePlayer)
				{
					return 'R';
				}
				if (ColorAsGoodFor.IsHostileTowards(ColorCoding.ThePlayer))
				{
					return 'r';
				}
			}
			if (ColorAsBadFor != null)
			{
				if (ColorAsBadFor.IsPlayer())
				{
					return 'R';
				}
				if (ColorAsBadFor.IsPlayerLed())
				{
					return 'r';
				}
				if (ColorAsBadFor.IsAlliedTowards(ColorCoding.ThePlayer))
				{
					return 'r';
				}
				if (ColorAsBadFor.Target == ColorCoding.ThePlayer)
				{
					return 'G';
				}
				if (ColorAsBadFor.IsHostileTowards(ColorCoding.ThePlayer))
				{
					return 'g';
				}
			}
			return 'y';
		}

		// Token: 0x06006BFE RID: 27646 RVA: 0x0022D990 File Offset: 0x0022BB90
		public static string ConsequentialColor(GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null)
		{
			char c = ColorCoding.ConsequentialColorChar(ColorAsGoodFor, ColorAsBadFor);
			if (c == 'y')
			{
				return null;
			}
			return "&" + c.ToString();
		}

		// Token: 0x06006BFF RID: 27647 RVA: 0x0022D9C0 File Offset: 0x0022BBC0
		public static string ConsequentialColorize(string Text, GameObject ColorAsGoodFor = null, GameObject ColorAsBadFor = null, char Default = ' ')
		{
			char c = ColorCoding.ConsequentialColorChar(ColorAsGoodFor, ColorAsBadFor);
			if (c != 'y')
			{
				return string.Concat(new string[]
				{
					"{{",
					c.ToString(),
					"|",
					Text,
					"}}"
				});
			}
			if (Default != ' ')
			{
				return string.Concat(new string[]
				{
					"{{",
					Default.ToString(),
					"|",
					Text,
					"}}"
				});
			}
			return Text;
		}
	}
}
