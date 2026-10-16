using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F5 RID: 2549
	public static class Scanning
	{
		// Token: 0x06006CC5 RID: 27845 RVA: 0x002365D7 File Offset: 0x002347D7
		public static string GetScanDisplayName(Scanning.Scan scan)
		{
			return Scanning.scanNames[scan];
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x002365E4 File Offset: 0x002347E4
		public static string GetScanSubjectName(Scanning.Scan scan)
		{
			return Scanning.scanSubjectNames[scan];
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x002365F1 File Offset: 0x002347F1
		public static string GetScanPropertyName(Scanning.Scan scan)
		{
			return Scanning.scanProperties[scan];
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x002365FE File Offset: 0x002347FE
		public static Scanning.Scan GetScanTypeFor(GameObject obj)
		{
			return GetScanTypeEvent.GetFor(obj);
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x00236606 File Offset: 0x00234806
		public static bool HasScanningFor(GameObject who, Scanning.Scan scan)
		{
			return who.GetIntProperty(Scanning.GetScanPropertyName(scan), 0) > 0;
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x00236618 File Offset: 0x00234818
		public static bool HasScanningFor(GameObject who, GameObject obj)
		{
			return Scanning.HasScanningFor(who, Scanning.GetScanTypeFor(obj));
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x00236628 File Offset: 0x00234828
		public static int GetScanEpistemicStatus(GameObject who, GameObject obj)
		{
			if (!GameObject.Validate(ref who) || !GameObject.Validate(ref obj))
			{
				return 0;
			}
			if (Options.SifrahExamine)
			{
				return 0;
			}
			Scanning.Scan scan = Scanning.GetScanTypeFor(obj);
			if (scan == Scanning.Scan.Structure)
			{
				scan = Scanning.Scan.Tech;
			}
			if (Scanning.HasScanningFor(who, scan))
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x040031D3 RID: 12755
		[NonSerialized]
		private static Dictionary<Scanning.Scan, string> scanNames = new Dictionary<Scanning.Scan, string>
		{
			{
				Scanning.Scan.Bio,
				"bioscanning"
			},
			{
				Scanning.Scan.Tech,
				"techscanning"
			},
			{
				Scanning.Scan.Structure,
				"structural scanning"
			},
			{
				Scanning.Scan.Unscannable,
				"unscannable"
			}
		};

		// Token: 0x040031D4 RID: 12756
		[NonSerialized]
		private static Dictionary<Scanning.Scan, string> scanSubjectNames = new Dictionary<Scanning.Scan, string>
		{
			{
				Scanning.Scan.Bio,
				"biometrics"
			},
			{
				Scanning.Scan.Tech,
				"telemetry"
			},
			{
				Scanning.Scan.Structure,
				"structural scan"
			},
			{
				Scanning.Scan.Unscannable,
				"unscannable"
			}
		};

		// Token: 0x040031D5 RID: 12757
		[NonSerialized]
		private static Dictionary<Scanning.Scan, string> scanProperties = new Dictionary<Scanning.Scan, string>
		{
			{
				Scanning.Scan.Bio,
				"BioScannerEquipped"
			},
			{
				Scanning.Scan.Tech,
				"TechScannerEquipped"
			},
			{
				Scanning.Scan.Structure,
				"StructureScannerEquipped"
			},
			{
				Scanning.Scan.Unscannable,
				null
			}
		};

		// Token: 0x02001AD4 RID: 6868
		public enum Scan
		{
			// Token: 0x04006AB3 RID: 27315
			Bio,
			// Token: 0x04006AB4 RID: 27316
			Tech,
			// Token: 0x04006AB5 RID: 27317
			Structure,
			// Token: 0x04006AB6 RID: 27318
			Unscannable
		}
	}
}
