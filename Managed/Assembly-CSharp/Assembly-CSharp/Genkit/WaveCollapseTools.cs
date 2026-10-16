using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using XRL;

namespace Genkit
{
	// Token: 0x02000396 RID: 918
	public static class WaveCollapseTools
	{
		// Token: 0x06002299 RID: 8857 RVA: 0x000AEDDC File Offset: 0x000ACFDC
		public static void LoadTemplates(bool force = false)
		{
			if (!force && WaveCollapseTools.waveTemplates.Count > 0)
			{
				return;
			}
			WaveCollapseTools.waveTemplates = new Dictionary<string, WaveTemplateEntry>();
			ModManager.ForEachFileIn("wavetemplates", delegate(string path, ModInfo info)
			{
				try
				{
					if (!string.IsNullOrEmpty(path) && path.ToUpper().EndsWith(".PNG"))
					{
						Texture2D texture2D = WaveCollapseTools.LoadPNG(path);
						if (texture2D == null)
						{
							Debug.Log("skipping file: " + path + " because tex returned NULL");
						}
						else
						{
							WaveTemplateEntry waveTemplateEntry = new WaveTemplateEntry();
							if (waveTemplateEntry == null)
							{
								Debug.Log("newEntry is null!");
							}
							if (texture2D == null)
							{
								Debug.Log("tex is null!");
							}
							waveTemplateEntry.name = Path.GetFileNameWithoutExtension(path);
							waveTemplateEntry.width = texture2D.width;
							waveTemplateEntry.height = texture2D.height;
							waveTemplateEntry.pixels = texture2D.GetPixels32();
							if (WaveCollapseTools.waveTemplates == null)
							{
								Debug.Log("waveTemplates is null!");
							}
							WaveCollapseTools.waveTemplates.Add(waveTemplateEntry.name.ToLower(), waveTemplateEntry);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Log("skipping file: " + path + " because of " + ex.ToString());
				}
			}, true, false);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000AEE30 File Offset: 0x000AD030
		public static Texture2D LoadPNG(string filePath)
		{
			byte[] array = File.ReadAllBytes(filePath);
			if (array == null)
			{
				Debug.Log("failed to read file data from: " + filePath);
			}
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
			if (array == null)
			{
				Debug.Log("failed to read file data because LoadImage puked: " + filePath);
			}
			return texture2D;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000AEE79 File Offset: 0x000AD079
		public static Color32[] GetPixelsFromPNG(string path)
		{
			return WaveCollapseTools.LoadPNG(path).GetPixels32();
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000AEE86 File Offset: 0x000AD086
		public static bool equals(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000AEEC4 File Offset: 0x000AD0C4
		public static int Random(this List<double> a, double r)
		{
			double num = a.Sum();
			if (num == 0.0)
			{
				for (int i = 0; i < a.Count<double>(); i++)
				{
					a[i] = 1.0;
				}
				num = a.Sum();
			}
			for (int j = 0; j < a.Count<double>(); j++)
			{
				int num2 = j;
				a[num2] /= num;
			}
			int k = 0;
			double num3 = 0.0;
			while (k < a.Count<double>())
			{
				num3 += a[k];
				if (r <= num3)
				{
					return k;
				}
				k++;
			}
			return 0;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000AEF68 File Offset: 0x000AD168
		public static int Random(this double[] a, double r)
		{
			double num = a.Sum();
			if (num == 0.0)
			{
				for (int i = 0; i < a.Count<double>(); i++)
				{
					a[i] = 1.0;
				}
				num = a.Sum();
			}
			for (int j = 0; j < a.Count<double>(); j++)
			{
				a[j] /= num;
			}
			int k = 0;
			double num2 = 0.0;
			while (k < a.Count<double>())
			{
				num2 += a[k];
				if (r <= num2)
				{
					return k;
				}
				k++;
			}
			return 0;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000AEFF8 File Offset: 0x000AD1F8
		public static long Power(int a, int n)
		{
			long num = 1L;
			for (int i = 0; i < n; i++)
			{
				num *= (long)a;
			}
			return num;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000AF01C File Offset: 0x000AD21C
		public static T Get<T>(this XmlNode node, string attribute, T defaultT = default(T))
		{
			string attribute2 = ((XmlElement)node).GetAttribute(attribute);
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			if (!(attribute2 == ""))
			{
				return (T)((object)converter.ConvertFromInvariantString(attribute2));
			}
			return defaultT;
		}

		// Token: 0x04001160 RID: 4448
		public static Dictionary<string, WaveTemplateEntry> waveTemplates = new Dictionary<string, WaveTemplateEntry>();
	}
}
