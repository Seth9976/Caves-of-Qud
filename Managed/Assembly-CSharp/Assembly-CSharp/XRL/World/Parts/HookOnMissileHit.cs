using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001118 RID: 4376
	[Serializable]
	public class HookOnMissileHit : IPart
	{
		// Token: 0x0600AD46 RID: 44358 RVA: 0x003F975F File Offset: 0x003F795F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileEntering");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD47 RID: 44359 RVA: 0x003F9774 File Offset: 0x003F7974
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileEntering")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				Cell cell = E.GetParameter("Cell") as Cell;
				MissilePath missilePath = E.GetParameter("Path") as MissilePath;
				if (missilePath == null)
				{
					return true;
				}
				foreach (GameObject gameObject in cell.GetObjectsWithPart("Physics"))
				{
					if (gameObject.Weight > 0 && gameObject.Physics.IsReal && gameObject.CanBeInvoluntarilyMoved() && !gameObject.HasTagOrProperty("ExcavatoryTerrainFeature"))
					{
						int i = 1;
						int count = missilePath.Path.Count;
						while (i < count)
						{
							if (missilePath.Path[i].IsEmpty() && gameObject.CurrentCell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null))
							{
								try
								{
									if (gameObject.IsCombat)
									{
										TextConsole textConsole = Look._TextConsole;
										ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
										TextConsole.ScrapBuffer2.Copy(TextConsole.CurrentBuffer);
										XRLCore.Core.RenderMapToBuffer(scrapBuffer);
										for (int j = missilePath.Path.Count - 1; j >= i; j--)
										{
											scrapBuffer.Copy(TextConsole.ScrapBuffer2);
											scrapBuffer.Goto(cell.X, cell.Y);
											scrapBuffer.Write("&b^b ", true, false, false, null, -1);
											for (int k = 1; k < missilePath.Path.Count - 1; k++)
											{
												scrapBuffer.Goto(missilePath.Path[k].X, missilePath.Path[k].Y);
												if (50.in100())
												{
													scrapBuffer.Write("&b^b ", true, false, false, null, -1);
												}
												else
												{
													scrapBuffer.Write("&b^r ", true, false, false, null, -1);
												}
											}
											if (!string.IsNullOrEmpty(gameObject.Render.TileColor))
											{
												if (string.IsNullOrEmpty(gameObject.Render.DetailColor))
												{
													scrapBuffer[missilePath.Path[j].X, missilePath.Path[j].Y].Detail = The.Color.DarkBlack;
												}
												else
												{
													scrapBuffer[missilePath.Path[j].X, missilePath.Path[j].Y].Detail = ConsoleLib.Console.ColorUtility.ColorMap[gameObject.Render.DetailColor[0]];
												}
											}
											else
											{
												scrapBuffer.Goto(missilePath.Path[j].X, missilePath.Path[j].Y);
												scrapBuffer.Write(gameObject.Render.ColorString + gameObject.Render.RenderString, true, false, false, null, -1);
											}
											if (!string.IsNullOrEmpty(gameObject.Render.Tile))
											{
												scrapBuffer[missilePath.Path[j].X, missilePath.Path[j].Y].Tile = gameObject.Render.Tile;
											}
											textConsole.DrawBuffer(scrapBuffer, null, false);
											textConsole.WaitFrame();
										}
									}
								}
								catch (Exception ex)
								{
									Debug.LogError("Exception during hookonhit: " + ex.ToString());
								}
								missilePath.Path[i].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								IComponent<GameObject>.XDidYToZ(gameObject, "are", "dragged toward", gameObjectParameter, null, null, null, null, null, gameObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
								break;
							}
							i++;
						}
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
