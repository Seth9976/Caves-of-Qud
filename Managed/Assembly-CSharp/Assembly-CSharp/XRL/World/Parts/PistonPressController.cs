using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x0200126D RID: 4717
	[Serializable]
	public class PistonPressController : IPart
	{
		// Token: 0x0600BAEE RID: 47854 RVA: 0x00444257 File Offset: 0x00442457
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600BAEF RID: 47855 RVA: 0x00444260 File Offset: 0x00442460
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BAF0 RID: 47856 RVA: 0x00444278 File Offset: 0x00442478
		public override bool Render(RenderEvent E)
		{
			if (this.Counter == this.Frequency - 1)
			{
				int num = XRLCore.CurrentFrame % 30;
				if (num > 0 && num < 15)
				{
					E.Tile = null;
					E.RenderString = "X";
					E.ColorString = "&R";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BAF1 RID: 47857 RVA: 0x004442C8 File Offset: 0x004424C8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.elements == null && this.Directions != null)
				{
					this.elements = new Dictionary<string, PistonPressElement>();
					for (int i = 0; i < this.Directions.Length; i++)
					{
						Cell cellFromDirection = this.ParentObject.Physics.currentCell.GetCellFromDirection(this.Directions[i].ToString(), true);
						if (cellFromDirection != null)
						{
							while (cellFromDirection.GetObjectCountWithPart("PistonPressElement") > 1)
							{
								cellFromDirection.GetObjectsWithPart("PistonPressElement").FirstOrDefault<GameObject>().Obliterate(null, false, null);
							}
							GameObject gameObject = cellFromDirection.GetObjectsWithPart("PistonPressElement").FirstOrDefault<GameObject>();
							if (gameObject != null)
							{
								this.elements.Add(this.Directions[i].ToString(), gameObject.GetPart<PistonPressElement>());
								gameObject.GetPart<PistonPressElement>().Direction = XRL.Rules.Directions.GetOppositeDirection(this.Directions[i].ToString());
							}
						}
					}
				}
				if (this.elements != null)
				{
					this.elements.RemoveAll<string, PistonPressElement>((KeyValuePair<string, PistonPressElement> e) => e.Value == null || e.Value.ParentObject == null || !e.Value.ParentObject.IsValid());
					List<string> list = null;
					foreach (string text in this.elements.Keys)
					{
						PistonPressElement pistonPressElement = this.elements[text];
						if (pistonPressElement == null || pistonPressElement.ParentObject == null || pistonPressElement.ParentObject.Physics == null || pistonPressElement.ParentObject.Physics.CurrentCell == null || pistonPressElement.ParentObject.Physics.CurrentCell != this.ParentObject.Physics.currentCell.GetCellFromDirection(text, true))
						{
							if (list == null)
							{
								list = new List<string>();
							}
							list.Add(text);
						}
					}
					if (list != null)
					{
						list.ForEach(delegate(string d)
						{
							this.elements.Remove(d);
						});
					}
				}
				this.Counter++;
				if (this.elements != null && this.elements.Count != 0)
				{
					if (this.Counter < this.Frequency - 1)
					{
						if (this.centerElement != null)
						{
							this.centerElement.Destroy(null, false, false, null);
							this.centerElement = null;
						}
						foreach (KeyValuePair<string, PistonPressElement> keyValuePair in this.elements)
						{
							keyValuePair.Value.ParentObject.GetPart<Render>().RenderString = PistonPressController.pressGlyph[keyValuePair.Key];
							keyValuePair.Value.ParentObject.GetPart<Render>().Tile = PistonPressController.pressTile[keyValuePair.Key];
							keyValuePair.Value.Danger = false;
						}
						this.ParentObject.Render.Visible = false;
					}
					else if (this.Counter == this.Frequency - 1)
					{
						if (this.centerElement != null)
						{
							this.centerElement.Destroy(null, false, false, null);
							this.centerElement = null;
						}
						foreach (KeyValuePair<string, PistonPressElement> keyValuePair2 in this.elements)
						{
							keyValuePair2.Value.ParentObject.GetPart<Render>().RenderString = PistonPressController.pressGlyph[keyValuePair2.Key];
							keyValuePair2.Value.ParentObject.GetPart<Render>().Tile = PistonPressController.pressTile[keyValuePair2.Key];
							keyValuePair2.Value.Danger = true;
						}
						this.ParentObject.Render.Visible = false;
					}
					else if (this.Counter >= this.Frequency)
					{
						this.Counter = 0;
						if (this.elements.Any<KeyValuePair<string, PistonPressElement>>((KeyValuePair<string, PistonPressElement> v) => v.Value.ParentObject.IsVisible()))
						{
							this.ParentObject.PistonPuff("&y", 6);
						}
						foreach (KeyValuePair<string, PistonPressElement> keyValuePair3 in this.elements)
						{
							keyValuePair3.Value.ParentObject.GetPart<Render>().RenderString = PistonPressController.extendGlyph[keyValuePair3.Key];
							keyValuePair3.Value.ParentObject.GetPart<Render>().Tile = PistonPressController.extendTile[keyValuePair3.Key];
							keyValuePair3.Value.Danger = false;
						}
						this.ParentObject.Render.Visible = true;
						this.ParentObject.Render.RenderString = PistonPressController.centerGlyph[this.Directions];
						this.ParentObject.Render.Tile = PistonPressController.centerTile[this.Directions];
						if (this.centerElement == null)
						{
							this.centerElement = this.ParentObject.CurrentCell.AddObject("PistonPressElement", null, null, null, null);
						}
						if (this.centerElement != null)
						{
							this.centerElement.Render.RenderString = PistonPressController.centerGlyph[this.Directions];
							this.centerElement.Render.Tile = PistonPressController.centerTile[this.Directions];
						}
						int phase = this.ParentObject.GetPhase();
						List<GameObject> list2 = Event.NewGameObjectList();
						list2.AddRange(this.ParentObject.CurrentCell.Objects);
						list2.RemoveAll((GameObject o) => o.Render == null || o.Render.RenderLayer <= 1 || o.Physics == null || !o.Physics.IsReal);
						if (this.elements.Count == 1)
						{
							if (this.centerElement != null)
							{
								this.centerElement.Render.RenderString = PistonPressController.centerGlyphOne[this.elements.Keys.FirstOrDefault<string>()];
								this.centerElement.Render.Tile = PistonPressController.centerTileOne[this.elements.Keys.FirstOrDefault<string>()];
							}
							this.ParentObject.Render.RenderString = PistonPressController.centerGlyphOne[this.elements.Keys.FirstOrDefault<string>()];
							this.ParentObject.Render.Tile = PistonPressController.centerTileOne[this.elements.Keys.FirstOrDefault<string>()];
							string oppositeDirection = XRL.Rules.Directions.GetOppositeDirection(this.elements.Keys.FirstOrDefault<string>());
							using (List<GameObject>.Enumerator enumerator3 = list2.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									GameObject gameObject2 = enumerator3.Current;
									Cudgel_Slam.Cast(this.elements.Values.FirstOrDefault<PistonPressElement>().ParentObject, null, oppositeDirection, gameObject2, false, 100, "1d10");
								}
								goto IL_0790;
							}
						}
						foreach (GameObject gameObject3 in list2)
						{
							if (gameObject3 != this.ParentObject && gameObject3.Physics != null && gameObject3.Render != null && gameObject3.Render.RenderLayer > 5)
							{
								gameObject3.TakeDamage(Stat.Random(80, 120), "from being crushed by a machine press.", "Bludgeoning", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, phase, null);
							}
						}
					}
				}
			}
			IL_0790:
			return base.FireEvent(E);
		}

		// Token: 0x04004B03 RID: 19203
		public string Directions;

		// Token: 0x04004B04 RID: 19204
		public int Frequency = 4;

		// Token: 0x04004B05 RID: 19205
		public int Counter;

		// Token: 0x04004B06 RID: 19206
		[NonSerialized]
		private Dictionary<string, PistonPressElement> elements;

		// Token: 0x04004B07 RID: 19207
		[NonSerialized]
		private static Dictionary<string, string> extendTile = new Dictionary<string, string>
		{
			{ "S", "Items/sw_crusher_s_extend.bmp" },
			{ "N", "Items/sw_crusher_n_extend.bmp" },
			{ "W", "Items/sw_crusher_w_extend.bmp" },
			{ "E", "Items/sw_crusher_e_extend.bmp" }
		};

		// Token: 0x04004B08 RID: 19208
		[NonSerialized]
		private static Dictionary<string, string> pressGlyph = new Dictionary<string, string>
		{
			{
				"S",
				'Ò'.ToString()
			},
			{
				"N",
				'Ð'.ToString()
			},
			{
				"W",
				'µ'.ToString()
			},
			{
				"E",
				'Æ'.ToString()
			}
		};

		// Token: 0x04004B09 RID: 19209
		[NonSerialized]
		private static Dictionary<string, string> pressTile = new Dictionary<string, string>
		{
			{ "S", "Items/sw_crusher_s_press.bmp" },
			{ "N", "Items/sw_crusher_n_press.bmp" },
			{ "W", "Items/sw_crusher_w_press.bmp" },
			{ "E", "Items/sw_crusher_e_press.bmp" }
		};

		// Token: 0x04004B0A RID: 19210
		[NonSerialized]
		private static Dictionary<string, string> extendGlyph = new Dictionary<string, string>
		{
			{
				"S",
				'º'.ToString()
			},
			{
				"N",
				'º'.ToString()
			},
			{
				"W",
				'Í'.ToString()
			},
			{
				"E",
				'ú'.ToString()
			}
		};

		// Token: 0x04004B0B RID: 19211
		[NonSerialized]
		private static Dictionary<string, string> centerGlyph = new Dictionary<string, string>
		{
			{
				"NS",
				'×'.ToString()
			},
			{
				"EW",
				'Ø'.ToString()
			}
		};

		// Token: 0x04004B0C RID: 19212
		[NonSerialized]
		private static Dictionary<string, string> centerTile = new Dictionary<string, string>
		{
			{ "NS", "Items/sw_crusher_ns_closed.bmp" },
			{ "EW", "Items/sw_crusher_we_open.bmp" }
		};

		// Token: 0x04004B0D RID: 19213
		[NonSerialized]
		private static Dictionary<string, string> centerGlyphOne = new Dictionary<string, string>
		{
			{
				"N",
				'Ð'.ToString()
			},
			{
				"S",
				'Ò'.ToString()
			},
			{
				"E",
				'Æ'.ToString()
			},
			{
				"W",
				'µ'.ToString()
			}
		};

		// Token: 0x04004B0E RID: 19214
		[NonSerialized]
		private static Dictionary<string, string> centerTileOne = new Dictionary<string, string>
		{
			{ "N", "Items/sw_crusher_n_closed.png" },
			{ "S", "Items/sw_crusher_s_closed.png" },
			{ "E", "Items/sw_crusher_e_closed.png" },
			{ "W", "Items/sw_crusher_w_closed.png" }
		};

		// Token: 0x04004B0F RID: 19215
		public GameObject centerElement;
	}
}
