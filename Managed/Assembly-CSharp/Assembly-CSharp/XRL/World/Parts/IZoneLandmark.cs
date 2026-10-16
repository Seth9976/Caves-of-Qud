using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x02000DF4 RID: 3572
	[HasWishCommand]
	public abstract class IZoneLandmark : IPart
	{
		// Token: 0x060090B4 RID: 37044
		public abstract string GetDisplayName();

		// Token: 0x060090B5 RID: 37045
		public abstract bool IsApplicable(int X, int Y);

		// Token: 0x060090B6 RID: 37046 RVA: 0x00342ACC File Offset: 0x00340CCC
		public static IZoneLandmark GetBestFor(Cell Cell)
		{
			return IZoneLandmark.GetBestFor(Cell.ParentZone, Cell.X, Cell.Y);
		}

		// Token: 0x060090B7 RID: 37047 RVA: 0x00342AE8 File Offset: 0x00340CE8
		public static IZoneLandmark GetBestFor(Zone Zone, int X, int Y)
		{
			foreach (GameObject gameObject in Zone.IterateObjects())
			{
				IZoneLandmark partDescendedFrom = gameObject.GetPartDescendedFrom<IZoneLandmark>();
				if (partDescendedFrom != null && partDescendedFrom.IsApplicable(X, Y))
				{
					return partDescendedFrom;
				}
			}
			return null;
		}

		// Token: 0x060090B8 RID: 37048 RVA: 0x00342B2C File Offset: 0x00340D2C
		public virtual void Append(StringBuilder Builder, GameObject Object = null)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			string displayName = this.GetDisplayName();
			if (!this.Preposition.IsNullOrEmpty())
			{
				Builder.Compound(this.Preposition);
			}
			if (Object != null && (this.OwnerBlueprint == Object.Blueprint || (Object.HasID && this.OwnerID == Object.BaseID)))
			{
				Builder.Compound(Object.its);
			}
			else
			{
				GameObject gameObject = null;
				if (this.OwnerID != 0)
				{
					gameObject = currentZone.FindObjectByID(this.OwnerID);
				}
				if (gameObject == null && !this.OwnerBlueprint.IsNullOrEmpty())
				{
					gameObject = currentZone.FindObject(this.OwnerBlueprint);
				}
				if (gameObject != null)
				{
					Builder.Compound(Grammar.MakePossessive(gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, true, true, false, false, false, null, false)));
				}
				else if (this.Indefinite)
				{
					Builder.Compound(Grammar.IndefiniteArticleShouldBeAn(displayName) ? "an" : "a");
				}
				else
				{
					Builder.Compound("the");
				}
			}
			Builder.Compound(displayName);
		}

		// Token: 0x060090B9 RID: 37049 RVA: 0x00342C3C File Offset: 0x00340E3C
		[WishCommand("showlandmarks", null)]
		public static void WishRender()
		{
			Zone activeZone = The.ActiveZone;
			List<IZoneLandmark> list = new List<IZoneLandmark>();
			foreach (GameObject gameObject in activeZone.IterateObjects())
			{
				IZoneLandmark partDescendedFrom = gameObject.GetPartDescendedFrom<IZoneLandmark>();
				if (partDescendedFrom != null)
				{
					list.Add(partDescendedFrom);
				}
			}
			if (list.IsNullOrEmpty<IZoneLandmark>())
			{
				return;
			}
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			scrapBuffer.RenderBase();
			int i = 0;
			int count = list.Count;
			while (i < activeZone.Height)
			{
				for (int j = 0; j < activeZone.Width; j++)
				{
					for (int k = 0; k < count; k++)
					{
						if (list[k].IsApplicable(j, i))
						{
							ConsoleChar consoleChar = scrapBuffer[j, i];
							consoleChar.Char = (char)(k + 48);
							consoleChar.Foreground = The.Color.Red;
							consoleChar.Background = The.Color.Green;
							consoleChar.Detail = The.Color.Blue;
						}
					}
				}
				i++;
			}
			scrapBuffer.Draw();
			Keyboard.getch();
		}

		// Token: 0x060090BA RID: 37050 RVA: 0x00342D50 File Offset: 0x00340F50
		[WishCommand("landmark", null)]
		public static void WishCurrent()
		{
			IZoneLandmark bestFor = IZoneLandmark.GetBestFor(The.PlayerCell);
			if (bestFor == null)
			{
				Popup.ShowFail("You are nut currently in a landmark location.", true, true, true);
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("You are");
			bestFor.Append(stringBuilder, The.Player);
			stringBuilder.Append('.');
			Popup.Show(Event.FinalizeString(stringBuilder), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x04003C43 RID: 15427
		public int OwnerID;

		// Token: 0x04003C44 RID: 15428
		public string OwnerBlueprint;

		// Token: 0x04003C45 RID: 15429
		public string Preposition;

		// Token: 0x04003C46 RID: 15430
		public bool Indefinite;
	}
}
