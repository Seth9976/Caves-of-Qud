using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.World;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C7 RID: 1479
	public class MapFileCell
	{
		// Token: 0x06003F01 RID: 16129 RVA: 0x00136A10 File Offset: 0x00134C10
		public HashSet<string> UsedBlueprints(HashSet<string> result)
		{
			if (result == null)
			{
				result = new HashSet<string>();
			}
			foreach (MapFileObjectBlueprint mapFileObjectBlueprint in this.Objects)
			{
				if (!result.Contains(mapFileObjectBlueprint.Name))
				{
					result.Add(mapFileObjectBlueprint.Name);
				}
			}
			return result;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x00136A84 File Offset: 0x00134C84
		public void Render(MapFileCellRender RenderCell, MapFileCellReference cref)
		{
			if (this.Objects.Count == 0)
			{
				RenderCell.Char = '.';
				RenderCell.Foreground = The.Color.Black;
				RenderCell.Background = The.Color.DarkBlack;
				RenderCell.Detail = The.Color.DarkBlack;
				return;
			}
			int num = -1;
			Dictionary<string, GameObjectBlueprint> blueprints = GameObjectFactory.Factory.Blueprints;
			foreach (MapFileObjectBlueprint mapFileObjectBlueprint in this.Objects)
			{
				try
				{
					GameObjectBlueprint gameObjectBlueprint;
					if (!blueprints.TryGetValue(mapFileObjectBlueprint.Name, out gameObjectBlueprint))
					{
						RenderCell.Foreground = new Color(1f, 0f, 0f, 1f);
						RenderCell.Char = 'x';
						break;
					}
					int partParameter = gameObjectBlueprint.GetPartParameter<int>("Render", "RenderLayer", -1);
					if (partParameter > num)
					{
						num = partParameter;
						RenderCell.RenderBlueprint(gameObjectBlueprint, cref);
					}
				}
				catch (Exception ex)
				{
					Debug.Log(string.Concat(new string[] { "Exception rendering ", mapFileObjectBlueprint.Name, " : ", ex.Message, ex.StackTrace }));
				}
			}
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00136BDC File Offset: 0x00134DDC
		public void ApplyTo(Cell C, bool CheckEmpty = true, Action<Cell> PreAction = null, Action<Cell> PostAction = null, Func<string, Cell, bool> ShouldPlace = null, Func<string, Cell, string> Replace = null, Action<XRL.World.GameObject> BeforeObjectCreated = null, Action<string, Cell> BeforePlacement = null, Action<string, Cell> AfterPlacement = null)
		{
			if (this.Clear)
			{
				C.Clear(null, false, true, null);
			}
			if (CheckEmpty && this.Objects.Count == 0)
			{
				return;
			}
			if (PreAction != null)
			{
				PreAction(C);
			}
			GameObjectFactory factory = GameObjectFactory.Factory;
			foreach (MapFileObjectBlueprint mapFileObjectBlueprint in this.Objects)
			{
				string text = mapFileObjectBlueprint.Name;
				if (ShouldPlace == null || ShouldPlace(text, C))
				{
					if (Replace != null)
					{
						text = Replace(text, C);
					}
					if (text != null)
					{
						if (!factory.Blueprints.ContainsKey(text))
						{
							MetricsManager.LogError(string.Format("Unknown map object {0} at [{1}, {2}]", text, C.Y, C.Y));
						}
						else
						{
							try
							{
								XRL.World.GameObject gameObject = mapFileObjectBlueprint.Create(BeforeObjectCreated);
								if (BeforePlacement != null)
								{
									BeforePlacement(text, C);
								}
								C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								if (AfterPlacement != null)
								{
									AfterPlacement(text, C);
								}
							}
							catch (Exception ex)
							{
								MetricsManager.LogError(string.Format("Error adding {0} at [{1}, {2}]:", mapFileObjectBlueprint.Name, C.Y, C.Y), ex);
							}
						}
					}
				}
			}
			if (PostAction != null)
			{
				PostAction(C);
			}
		}

		// Token: 0x04001F20 RID: 7968
		public bool Clear;

		// Token: 0x04001F21 RID: 7969
		public List<MapFileObjectBlueprint> Objects = new List<MapFileObjectBlueprint>();
	}
}
