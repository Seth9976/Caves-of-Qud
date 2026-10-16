using System;
using System.Reflection;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012E3 RID: 4835
	[Serializable]
	public class RandomColors : IPart
	{
		// Token: 0x0600BE2C RID: 48684 RVA: 0x0045AE98 File Offset: 0x00459098
		public override bool SameAs(IPart p)
		{
			RandomColors randomColors = p as RandomColors;
			return !(randomColors.MainColor != this.MainColor) && !(randomColors.TileColor != this.TileColor) && !(randomColors.DetailColor != this.DetailColor) && !(randomColors.BackgroundColor != this.BackgroundColor) && !(randomColors.ReferencePart != this.ReferencePart) && !(randomColors.MainColorField != this.MainColorField) && !(randomColors.TileColorField != this.TileColorField) && !(randomColors.DetailColorField != this.DetailColorField) && randomColors.MainColorFieldIsForegroundBackground == this.MainColorFieldIsForegroundBackground && randomColors.TileColorFieldIsForegroundBackground == this.TileColorFieldIsForegroundBackground && randomColors.DetailColorFieldIsForegroundBackground == this.DetailColorFieldIsForegroundBackground && randomColors.SetColorStringToDetailColor == this.SetColorStringToDetailColor && base.SameAs(p);
		}

		// Token: 0x0600BE2D RID: 48685 RVA: 0x0045AF9B File Offset: 0x0045919B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600BE2E RID: 48686 RVA: 0x0045AFB1 File Offset: 0x004591B1
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.RandomizeColors();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE2F RID: 48687 RVA: 0x0045AFC0 File Offset: 0x004591C0
		public virtual void RandomizeColors()
		{
			if (this.MainColor == null && this.TileColor == null && this.BackgroundColor == null && this.DetailColor == null)
			{
				MetricsManager.LogError("no colors specified for " + this.ParentObject.DebugName);
				return;
			}
			string text = this.MainColor;
			string text2 = this.TileColor;
			string text3 = this.DetailColor;
			string text4 = this.BackgroundColor;
			int num = -1;
			if (text != null)
			{
				if (text.Contains(","))
				{
					string[] array = text.Split(',', StringSplitOptions.None);
					num = Stat.Rand.Next(0, array.Length);
					text = array[num];
				}
				if (text == "bright")
				{
					text = Crayons.GetRandomColor(null);
				}
				if (text == "all")
				{
					text = Crayons.GetRandomColorAll(null);
				}
				if (this.MainColorFieldIsForegroundBackground)
				{
					text = "&" + text;
				}
			}
			if (text2 != null)
			{
				if (text2.Contains(","))
				{
					string[] array2 = text2.Split(',', StringSplitOptions.None);
					num = Stat.Rand.Next(0, array2.Length);
					text2 = array2[num];
				}
				if (text2 == "bright")
				{
					text2 = Crayons.GetRandomColor(null);
				}
				if (text2 == "all")
				{
					text2 = Crayons.GetRandomColorAll(null);
				}
				if (this.TileColorFieldIsForegroundBackground)
				{
					text2 = "&" + text2;
				}
			}
			if (text4 != null)
			{
				if (text4.Contains(","))
				{
					text4 = text4.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
				}
				if (text4 == "bright")
				{
					text4 = Crayons.GetRandomColor(null);
				}
				if (text4 == "all")
				{
					text4 = Crayons.GetRandomColorAll(null);
				}
			}
			if (text3 != null)
			{
				if (text3.Contains(","))
				{
					string[] array3 = text3.Split(',', StringSplitOptions.None);
					if (!this.PairDetailWithForeground || num < 0 || num >= array3.Length)
					{
						num = Stat.Rand.Next(0, array3.Length);
					}
					text3 = array3[num];
				}
				if (text3 == "bright")
				{
					text3 = Crayons.GetRandomColor(null);
				}
				if (text3 == "all")
				{
					text3 = Crayons.GetRandomColorAll(null);
				}
				if (this.DetailColorFieldIsForegroundBackground)
				{
					text3 = "&" + text3;
				}
			}
			if (this.ReferencePart == "Render" && this.MainColorField == "ColorString" && this.DetailColorField == "DetailColor" && this.MainColorFieldIsForegroundBackground && !this.DetailColorFieldIsForegroundBackground)
			{
				Render render = this.ParentObject.Render;
				if (render == null)
				{
					Debug.LogError("no Render part in " + this.ParentObject.DisplayNameOnly);
					return;
				}
				if (text != null)
				{
					render.ColorString = text;
				}
				if (text4 != null)
				{
					render.ColorString = render.ColorString.Split('^', StringSplitOptions.None)[0] + "^" + text4;
				}
				if (text2 != null)
				{
					render.TileColor = text2;
				}
				if (text3 != null)
				{
					render.DetailColor = text3;
				}
				if (this.SetColorStringToDetailColor)
				{
					render.ColorString = "&" + render.DetailColor;
				}
			}
			else
			{
				IPart part = this.ParentObject.GetPart(this.ReferencePart);
				if (part == null)
				{
					Debug.LogError("no " + this.ReferencePart + " part in " + this.ParentObject.DisplayNameOnly);
					return;
				}
				Type type = part.GetType();
				if (text != null)
				{
					FieldInfo field = type.GetField(this.MainColorField);
					if (field != null)
					{
						field.SetValue(part, text);
					}
					else
					{
						PropertyInfo property = type.GetProperty(this.MainColorField);
						if (property != null)
						{
							property.SetValue(part, text, null);
						}
						else
						{
							Debug.LogError(string.Concat(new string[]
							{
								"no ",
								this.MainColorField,
								" field or property in ",
								this.ReferencePart,
								" part in ",
								this.ParentObject.DisplayNameOnly
							}));
						}
					}
				}
				if (text2 != null)
				{
					FieldInfo field2 = type.GetField(this.TileColorField);
					if (field2 != null)
					{
						field2.SetValue(part, text2);
					}
					else
					{
						PropertyInfo property2 = type.GetProperty(this.TileColorField);
						if (property2 != null)
						{
							property2.SetValue(part, text2, null);
						}
						else
						{
							Debug.LogError(string.Concat(new string[]
							{
								"no ",
								this.TileColorField,
								" field or property in ",
								this.ReferencePart,
								" part in ",
								this.ParentObject.DisplayNameOnly
							}));
						}
					}
				}
				if (text3 != null)
				{
					FieldInfo field3 = type.GetField(this.DetailColorField);
					if (field3 != null)
					{
						field3.SetValue(part, text3);
					}
					else
					{
						PropertyInfo property3 = type.GetProperty(this.DetailColorField);
						if (property3 != null)
						{
							property3.SetValue(part, text3, null);
						}
						else
						{
							Debug.LogError(string.Concat(new string[]
							{
								"no ",
								this.DetailColorField,
								" field or property in ",
								this.ReferencePart,
								" part in ",
								this.ParentObject.DisplayNameOnly
							}));
						}
					}
				}
			}
			if (!this.KeepPartAround())
			{
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x0600BE30 RID: 48688 RVA: 0x0045B4E6 File Offset: 0x004596E6
		public virtual bool KeepPartAround()
		{
			return false;
		}

		// Token: 0x04004C94 RID: 19604
		public string MainColor;

		// Token: 0x04004C95 RID: 19605
		public string TileColor;

		// Token: 0x04004C96 RID: 19606
		public string DetailColor;

		// Token: 0x04004C97 RID: 19607
		public string BackgroundColor;

		// Token: 0x04004C98 RID: 19608
		public string ReferencePart = "Render";

		// Token: 0x04004C99 RID: 19609
		public string MainColorField = "ColorString";

		// Token: 0x04004C9A RID: 19610
		public string TileColorField = "TileColor";

		// Token: 0x04004C9B RID: 19611
		public string DetailColorField = "DetailColor";

		// Token: 0x04004C9C RID: 19612
		public bool MainColorFieldIsForegroundBackground = true;

		// Token: 0x04004C9D RID: 19613
		public bool TileColorFieldIsForegroundBackground = true;

		// Token: 0x04004C9E RID: 19614
		public bool DetailColorFieldIsForegroundBackground;

		// Token: 0x04004C9F RID: 19615
		public bool SetColorStringToDetailColor;

		// Token: 0x04004CA0 RID: 19616
		public bool PairDetailWithForeground;
	}
}
