using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;
using XRL;
using XRL.Rules;
using XRL.UI;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000485 RID: 1157
	[UIView("WorldGenerationScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "WorldGeneration", UICanvasHost = 1)]
	public class WorldGenerationScreen : SingletonWindowBase<WorldGenerationScreen>, ControlManager.IControllerChangedEvent
	{
		// Token: 0x06002C65 RID: 11365 RVA: 0x000EF2D8 File Offset: 0x000ED4D8
		public static async Task<bool> ShowWorldGenerationScreen(int totalSteps)
		{
			await The.UiContext;
			WorldGenerationScreen instance = SingletonWindowBase<WorldGenerationScreen>.instance;
			await ((instance != null) ? instance._ShowWorldGenerationScreen(totalSteps) : null);
			return true;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000EF31B File Offset: 0x000ED51B
		public static void HideWorldGenerationScreen()
		{
			WorldGenerationScreen instance = SingletonWindowBase<WorldGenerationScreen>.instance;
			if (instance == null)
			{
				return;
			}
			instance._HideWorldGenerationScreen();
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000EF32C File Offset: 0x000ED52C
		public static void AddMessage(string message)
		{
			SingletonWindowBase<WorldGenerationScreen>.instance._AddMessage(message);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000EF339 File Offset: 0x000ED539
		public static void IncrementProgress()
		{
			SingletonWindowBase<WorldGenerationScreen>.instance._IncrementProgress();
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000EF348 File Offset: 0x000ED548
		public async void _IncrementProgress()
		{
			await The.UiContext;
			this.myStep++;
			int num = Math.Min(WorldGenerationScreen.ProgressBasis.Length - 18, (int)((float)this.myStep / (float)this.totalSteps * (float)(WorldGenerationScreen.ProgressBasis.Length - 18)));
			this.totalProgress = (float)num / (float)(WorldGenerationScreen.ProgressBasis.Length - 18);
			string text = WorldGenerationScreen.ProgressBasis.Remove(num + 10, 1).Insert(num + 10, "}}>{{K|");
			this.progressText.SetText(text);
			this.UpdateIcons();
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000EF380 File Offset: 0x000ED580
		public async void _AddMessage(string message)
		{
			await The.UiContext;
			if (!(this.progressLines.Last<string>() == message))
			{
				this.progressLines.Add(message);
				int num = 0;
				foreach (string text in this.progressLines.TakeLast<string>(5))
				{
					this.progressTexts[num].SetText(text);
					num++;
				}
				this.UpdateIcons();
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000EF3C0 File Offset: 0x000ED5C0
		public void InitIcons()
		{
			for (int i = 0; i < 4; i++)
			{
				UIThreeColorProperties uithreeColorProperties = this.eonIcons[i];
				if (i == 0)
				{
					int num = Stat.Random(1, 3);
					if (num == 1)
					{
						uithreeColorProperties.FromRenderable(new Renderable("Mutations/spacetime_vortex.bmp", " ", "", "&B", 'm'), true);
					}
					else if (num == 2)
					{
						uithreeColorProperties.FromRenderable(new Renderable("Creatures/sw_monad.bmp", " ", "", "&Y", 'm'), true);
					}
					else
					{
						uithreeColorProperties.FromRenderable(new Renderable("Mutations/light_manipulation.bmp", " ", "", "&Y", 'C'), true);
					}
				}
				else
				{
					string text;
					if (i == 1)
					{
						text = "Geological";
					}
					else if (i == 2)
					{
						text = "Historical";
					}
					else
					{
						text = "Contemporary";
					}
					string text2 = "unknown";
					Renderable renderable = new Renderable(GameObjectFactory.Factory.Blueprints["Snapjaw"]);
					for (int j = 0; j < 20; j++)
					{
						try
						{
							text2 = PopulationManager.RollOneFrom("DynamicSemanticTable:" + text, null, null).Blueprint;
							GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(text2);
							string paintedTile = blueprint.GetPaintedTile(0);
							if (!paintedTile.IsNullOrEmpty())
							{
								if (SpriteManager.HasTextureInfo(paintedTile))
								{
									renderable.Set(blueprint);
									renderable.Tile = paintedTile;
									break;
								}
								MetricsManager.LogError("Invalid tile [" + text2 + "]: " + paintedTile);
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("invalid worldgen blueprint " + text2, ex, "game_exception");
						}
					}
					try
					{
						uithreeColorProperties.FromRenderable(renderable, true);
						uithreeColorProperties.name = text2;
						if (i == 1)
						{
							Debug.LogWarning("walltype: " + text2);
						}
					}
					catch (Exception ex2)
					{
						MetricsManager.LogException("invalid worldgen blueprint " + text2, ex2, "game_exception");
					}
				}
				uithreeColorProperties.image.color = uithreeColorProperties.image.color.WithAlpha(0f);
			}
			this.iconsUpdated.Clear();
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000EF5E8 File Offset: 0x000ED7E8
		public void UpdateIcons()
		{
			for (int i = 0; i < 4; i++)
			{
				Stat.RandomCosmetic(0, this.iconOptions[i].Count - 1);
				float num = Math.Min(1f, this.totalProgress - (float)i * 0.1f);
				this.eonIcons[i].image.color = new Color(this.eonIcons[i].image.color.r, this.eonIcons[i].image.color.g, this.eonIcons[i].image.color.b, num);
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000EF694 File Offset: 0x000ED894
		public async Task _ShowWorldGenerationScreen(int totalSteps)
		{
			await The.UiContext;
			this.totalSteps = totalSteps;
			for (int i = 0; i < this.progressTexts.Length; i++)
			{
				this.progressTexts[i].SetText(" ");
			}
			for (int j = 0; j < 4; j++)
			{
				this.eonIcons[j].image.color = new Color(this.eonIcons[j].image.color.r, this.eonIcons[j].image.color.g, this.eonIcons[j].image.color.b, 0f);
			}
			this.attributionText.SetText("");
			this.quoteText.SetText("");
			this.progressLines.Clear();
			for (int k = 0; k < 5; k++)
			{
				this.progressLines.Add("   ");
			}
			this.InitIcons();
			ControlManager.ResetInput(false, true);
			this.Show();
			this._IncrementProgress();
			StringBuilder stringBuilder = new StringBuilder();
			List<string> list = new List<string>(BookUI.Books["Quotes"][Stat.RandomCosmetic(0, BookUI.Books["Quotes"].Count - 1)].Lines);
			list.RemoveAll((string l) => l.Trim().Length == 0);
			if (list.Count == 0)
			{
				this.quoteText.SetText("");
				this.attributionText.SetText("");
			}
			else if (list.Count == 1)
			{
				this.quoteText.SetText(list[0]);
				this.attributionText.SetText("");
			}
			else
			{
				foreach (string text in list.Take<string>(list.Count - 1))
				{
					stringBuilder.Append(text);
				}
				this.quoteText.SetText(stringBuilder.ToString());
				this.attributionText.SetText(list.Last<string>());
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000EF6E0 File Offset: 0x000ED8E0
		public async void _HideWorldGenerationScreen()
		{
			await The.UiContext;
			this.Hide();
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000EF717 File Offset: 0x000ED917
		public void Exit()
		{
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000EF719 File Offset: 0x000ED919
		public void Update()
		{
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000EF71B File Offset: 0x000ED91B
		public void ControllerChanged()
		{
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000EF71D File Offset: 0x000ED91D
		public void SetupContext()
		{
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000EF71F File Offset: 0x000ED91F
		public override void Show()
		{
			base.Show();
			this.SetupContext();
			this.myStep = 0;
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000EF734 File Offset: 0x000ED934
		public override void Hide()
		{
			base.Hide();
			ControlManager.ResetInput(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x0400189C RID: 6300
		public UITextSkin[] progressTexts;

		// Token: 0x0400189D RID: 6301
		public UITextSkin quoteText;

		// Token: 0x0400189E RID: 6302
		public UITextSkin attributionText;

		// Token: 0x0400189F RID: 6303
		public UITextSkin progressText;

		// Token: 0x040018A0 RID: 6304
		private List<string> progressLines = new List<string>();

		// Token: 0x040018A1 RID: 6305
		private static string ProgressBasis = "{{Y|}}{{Y| . . . . . . . . ■ .  . . . . . . . ■  . . . . . . . . ■  . . . . . . . . ■  . . . . . . . . {{Y|}}";

		// Token: 0x040018A2 RID: 6306
		public int myStep;

		// Token: 0x040018A3 RID: 6307
		public float totalProgress;

		// Token: 0x040018A4 RID: 6308
		[TupleElementNames(new string[] { "icon", "color", "detail" })]
		public List<ValueTuple<string, string, string>>[] iconOptions = new List<ValueTuple<string, string, string>>[]
		{
			new List<ValueTuple<string, string, string>>
			{
				new ValueTuple<string, string, string>("Mutations/spacetime_vortex.bmp", "B", "m")
			},
			new List<ValueTuple<string, string, string>>
			{
				new ValueTuple<string, string, string>("Terrain/sw_mountains.bmp", "Y", "Y"),
				new ValueTuple<string, string, string>("Terrain/sw_mountains_2.bmp", "Y", "Y"),
				new ValueTuple<string, string, string>("Terrain/sw_hills.bmp", "y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_hills_2.bmp", "y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_canyon.bmp", "w", "w"),
				new ValueTuple<string, string, string>("Terrain/sw_hills2.bmp", "w", "w"),
				new ValueTuple<string, string, string>("Tiles/wall_rock-00000000.bmp", "r", "w"),
				new ValueTuple<string, string, string>("Tiles/wall_rock-00000000.bmp", "y", "K"),
				new ValueTuple<string, string, string>("Tiles/wall_rock-00000000.bmp", "r", "W")
			},
			new List<ValueTuple<string, string, string>>
			{
				new ValueTuple<string, string, string>("Terrain/sw_ruins_9.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_ruins_11.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_ruins_20.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_ruins_deep_1.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_ruins_deep_10.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_ruins_deep_12.bmp", "Y", "y"),
				new ValueTuple<string, string, string>("Terrain/sw_rusted_archway.bmp", "r", "y"),
				new ValueTuple<string, string, string>("Terrain/tile_location8.bmp", "W", "C")
			},
			new List<ValueTuple<string, string, string>>
			{
				new ValueTuple<string, string, string>("Terrain/sw_joppa.bmp", "w", "y"),
				new ValueTuple<string, string, string>("Terrain/tile_kya.bmp", "Y", "R"),
				new ValueTuple<string, string, string>("Terrain/terrainpalladiumreef_3l.bmp", "c", "W"),
				new ValueTuple<string, string, string>("Terrain/sw_joppa.bmp", "b", "G"),
				new ValueTuple<string, string, string>("Terrain/sw_joppa.bmp", "r", "W"),
				new ValueTuple<string, string, string>("Terrain/sw_joppa.bmp", "m", "B")
			}
		};

		// Token: 0x040018A5 RID: 6309
		public UIThreeColorProperties[] eonIcons;

		// Token: 0x040018A6 RID: 6310
		public int[] iconSelections = new int[4];

		// Token: 0x040018A7 RID: 6311
		private HashSet<int> iconsUpdated = new HashSet<int>();

		// Token: 0x040018A8 RID: 6312
		public int totalSteps;

		// Token: 0x040018A9 RID: 6313
		public bool wasInScroller;
	}
}
