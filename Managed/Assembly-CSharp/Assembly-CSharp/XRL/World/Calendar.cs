using System;
using XRL.Core;
using XRL.Wish;

namespace XRL.World
{
	// Token: 0x02000945 RID: 2373
	[HasWishCommand]
	[Serializable]
	public static class Calendar
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06005BBA RID: 23482 RVA: 0x001BC1E2 File Offset: 0x001BA3E2
		public static long TotalTimeTicks
		{
			get
			{
				return XRLCore.Core.Game.TimeTicks;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06005BBB RID: 23483 RVA: 0x001BC1F3 File Offset: 0x001BA3F3
		public static int CurrentDaySegment
		{
			get
			{
				return (int)(Calendar.TotalTimeTicks % 1200L) * 10;
			}
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001BC205 File Offset: 0x001BA405
		public static int GetYear()
		{
			return (int)(Calendar.TotalTimeTicks / 438000L) + 1001;
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001BC21A File Offset: 0x001BA41A
		public static int GetYear(long Time)
		{
			return (int)(Time / 438000L) + 1001;
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001BC22B File Offset: 0x001BA42B
		public static string GetMonth()
		{
			return Calendar.GetMonth((int)Calendar.TotalTimeTicks % 438000);
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x001BC23E File Offset: 0x001BA43E
		public static string GetMonth(long Time)
		{
			return Calendar.GetMonth((int)(Time % 438000L));
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x001BC250 File Offset: 0x001BA450
		public static string GetMonth(int TimeOfYear)
		{
			string text;
			if (TimeOfYear < 216001)
			{
				if (TimeOfYear < 144001)
				{
					if (TimeOfYear < 72001)
					{
						if (TimeOfYear >= 36001)
						{
							text = "Iyur Ut";
						}
						else
						{
							text = "Nivvun Ut";
						}
					}
					else if (TimeOfYear >= 108001)
					{
						text = "Tuum Ut";
					}
					else
					{
						text = "Simmun Ut";
					}
				}
				else if (TimeOfYear >= 180001)
				{
					text = "Uulu Ut";
				}
				else
				{
					text = "Ubu Ut";
				}
			}
			else if (TimeOfYear < 330001)
			{
				if (TimeOfYear < 258001)
				{
					if (TimeOfYear >= 222001)
					{
						text = "Tishru i Ux";
					}
					else
					{
						text = "Ut yara Ux";
					}
				}
				else if (TimeOfYear >= 294001)
				{
					text = "Kisu Ux";
				}
				else
				{
					text = "Tishru ii Ux";
				}
			}
			else if (TimeOfYear < 402001)
			{
				if (TimeOfYear >= 366001)
				{
					text = "Shwut Ux";
				}
				else
				{
					text = "Tebet Ux";
				}
			}
			else if (TimeOfYear >= 438001)
			{
				text = "Uru Ux";
			}
			else
			{
				text = "Uru Ux";
			}
			return text;
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x001BC342 File Offset: 0x001BA542
		public static string GetMarginaliaTime(long Time)
		{
			return "On the " + Calendar.GetDay(Time) + " of " + Calendar.GetMonth(Time);
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x001BC35F File Offset: 0x001BA55F
		public static int GetDayOfYear(long Time)
		{
			return (int)(Time % 438000L);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x001BC36A File Offset: 0x001BA56A
		public static string GetDay()
		{
			return Calendar.GetDay((int)Calendar.TotalTimeTicks % 438000);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001BC37D File Offset: 0x001BA57D
		public static string GetDay(long Time)
		{
			return Calendar.GetDay((int)(Time % 438000L));
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x001BC390 File Offset: 0x001BA590
		public static string GetDay(int TimeOfYear)
		{
			if (TimeOfYear > 216000 && TimeOfYear < 222001)
			{
				if (TimeOfYear < 217201)
				{
					return "1st";
				}
				if (TimeOfYear < 218401)
				{
					return "2nd";
				}
				if (TimeOfYear < 219601)
				{
					return "3rd";
				}
				if (TimeOfYear < 220801)
				{
					return "4th";
				}
				if (TimeOfYear < 222001)
				{
					return "5th";
				}
				return "0th";
			}
			else
			{
				if (TimeOfYear > 222000)
				{
					TimeOfYear -= 6000;
				}
				int num = TimeOfYear % 36000;
				if (num < 1200)
				{
					return "1st";
				}
				if (num < 2400)
				{
					return "2nd";
				}
				if (num < 3600)
				{
					return "3rd";
				}
				if (num < 4800)
				{
					return "4th";
				}
				if (num < 6000)
				{
					return "5th";
				}
				if (num < 7200)
				{
					return "6th";
				}
				if (num < 8400)
				{
					return "7th";
				}
				if (num < 9600)
				{
					return "8th";
				}
				if (num < 10800)
				{
					return "9th";
				}
				if (num < 12000)
				{
					return "10th";
				}
				if (num < 13200)
				{
					return "11th";
				}
				if (num < 14400)
				{
					return "12th";
				}
				if (num < 15600)
				{
					return "13th";
				}
				if (num < 16800)
				{
					return "14th";
				}
				if (num < 18000)
				{
					return "Ides";
				}
				if (num < 19200)
				{
					return "16th";
				}
				if (num < 20400)
				{
					return "17th";
				}
				if (num < 21600)
				{
					return "18th";
				}
				if (num < 22800)
				{
					return "19th";
				}
				if (num < 24000)
				{
					return "20th";
				}
				if (num < 25200)
				{
					return "21st";
				}
				if (num < 26400)
				{
					return "22nd";
				}
				if (num < 27600)
				{
					return "23rd";
				}
				if (num < 28800)
				{
					return "24th";
				}
				if (num < 30000)
				{
					return "25th";
				}
				if (num < 31200)
				{
					return "26th";
				}
				if (num < 32400)
				{
					return "27th";
				}
				if (num < 33600)
				{
					return "28th";
				}
				if (num < 34800)
				{
					return "29th";
				}
				if (num < 36000)
				{
					return "30th";
				}
				return "0th";
			}
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x001BC5BB File Offset: 0x001BA7BB
		public static string GetTime()
		{
			return Calendar.GetTime((int)Calendar.TotalTimeTicks % 1200);
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x001BC5CE File Offset: 0x001BA7CE
		public static string GetTime(long Time)
		{
			return Calendar.GetTime((int)(Time % 1200L));
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001BC5E0 File Offset: 0x001BA7E0
		public static string GetTime(int TimeOfDay)
		{
			string text;
			if (TimeOfDay < 451)
			{
				if (TimeOfDay < 151)
				{
					if (TimeOfDay >= 0)
					{
						if (TimeOfDay < 26)
						{
							return "Beetle Moon Zenith";
						}
					}
					text = "Waning Beetle Moon";
				}
				else if (TimeOfDay >= 301)
				{
					text = "Harvest Dawn";
				}
				else
				{
					text = "The Shallows";
				}
			}
			else if (TimeOfDay < 901)
			{
				if (TimeOfDay < 626)
				{
					if (TimeOfDay >= 576)
					{
						text = "High Salt Sun";
					}
					else
					{
						text = "Waxing Salt Sun";
					}
				}
				else if (TimeOfDay >= 751)
				{
					text = "Hindsun";
				}
				else
				{
					text = "Waning Salt Sun";
				}
			}
			else if (TimeOfDay < 1176)
			{
				if (TimeOfDay >= 1051)
				{
					text = "Waxing Beetle Moon";
				}
				else
				{
					text = "Jeweled Dusk";
				}
			}
			else if (TimeOfDay >= 1201)
			{
				text = "Zero Hour";
			}
			else
			{
				text = "Beetle Moon Zenith";
			}
			return text;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x001BC6B1 File Offset: 0x001BA8B1
		public static bool IsDay()
		{
			return Calendar.CurrentDaySegment >= 2500 && Calendar.CurrentDaySegment < 9124;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x001BC6D0 File Offset: 0x001BA8D0
		[WishCommand("advanceticks", null)]
		private static void WishAdvanceTime(string Ticks)
		{
			long num;
			if (!long.TryParse(Ticks, out num))
			{
				return;
			}
			The.Game.TimeTicks += num;
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x001BC6FA File Offset: 0x001BA8FA
		[Obsolete("Use GetYear()")]
		public static int getYear()
		{
			return (int)(Calendar.TotalTimeTicks / 438000L) + 1001;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x001BC70F File Offset: 0x001BA90F
		[Obsolete("Use GetMonth()")]
		public static string getMonth(int _dayOfYear = -1)
		{
			if (_dayOfYear > -1)
			{
				return Calendar.GetMonth(_dayOfYear);
			}
			return Calendar.GetMonth();
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001BC721 File Offset: 0x001BA921
		[Obsolete("Use GetDay()")]
		public static string getDay(int _dayOfYear = -1)
		{
			if (_dayOfYear > -1)
			{
				return Calendar.GetDay(_dayOfYear);
			}
			return Calendar.GetDay();
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x001BC733 File Offset: 0x001BA933
		[Obsolete("Use GetTime()")]
		public static string getTime(string zoneID = null)
		{
			return Calendar.GetTime();
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x001BC73A File Offset: 0x001BA93A
		[Obsolete("Use GetTime(int)")]
		public static string getTime(int minute)
		{
			return Calendar.GetTime(minute);
		}

		// Token: 0x04002D54 RID: 11604
		public const int TurnsPerYear = 438000;

		// Token: 0x04002D55 RID: 11605
		public const int TurnsPerDay = 1200;

		// Token: 0x04002D56 RID: 11606
		public const int TurnsPerHour = 50;

		// Token: 0x04002D57 RID: 11607
		public const int StartOfDay = 3250;

		// Token: 0x04002D58 RID: 11608
		public const int StartOfNight = 10000;

		// Token: 0x04002D59 RID: 11609
		[Obsolete("Use TurnsPerDay")]
		public const int turnsPerDay = 1200;

		// Token: 0x04002D5A RID: 11610
		[Obsolete("Use TurnsPerHour")]
		public const int turnsPerHour = 50;

		// Token: 0x04002D5B RID: 11611
		[Obsolete("Use StartOfDay")]
		public const int startOfDay = 3250;

		// Token: 0x04002D5C RID: 11612
		[Obsolete("Use StartOfNight")]
		public const int startOfNight = 10000;
	}
}
