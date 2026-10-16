using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Kobold;
using Qud.UI;
using QupKit;
using RedShadow.CommonDialogs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.Collections;
using XRL.Core;
using XRL.EditorFormats.Map;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Parts.Mutation;
using XRL.World.Text;

namespace Overlay.MapEditor
{
	// Token: 0x020003E1 RID: 993
	public class MapEditorView : BaseView
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000B9E9C File Offset: 0x000B809C
		public override void OnCreate()
		{
			MapEditorView.instance = this;
			this.OnDrag = delegate(PointerEventData D)
			{
				this.OnDragMove(D.position, D.delta, D.button);
			};
			this.OnClicked = delegate(PointerEventData D)
			{
				this.OnClick(D.position, D.button);
			};
			MapEditorView.UpdateComponent updateComponent;
			if (!base.rootObject.TryGetComponent<MapEditorView.UpdateComponent>(out updateComponent))
			{
				base.rootObject.AddComponent<MapEditorView.UpdateComponent>();
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000B9EF0 File Offset: 0x000B80F0
		public bool LoadMap(string file)
		{
			if (!File.Exists(file))
			{
				DialogManager.notify("File does not exist.", null);
				return false;
			}
			this.UndoStack = new Stack<MapEditorView.MapEditorAction>();
			this.RedoStack = new Stack<MapEditorView.MapEditorAction>();
			this.HasSelectedRegion = false;
			this.Map = new MapFile(0, 0);
			this.Map.LoadFile(file);
			this.Map.Cells.FillEmptyCells();
			this.FileName = file;
			this.AddRecentFile(file);
			this.UpdateTitle();
			this.RenderMap();
			this.RenderSelectionFrame();
			if (this.FilterUsed)
			{
				this.UpdateFilter();
			}
			return true;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000B9F88 File Offset: 0x000B8188
		public void AddRecentFile(string File)
		{
			Rack<string> recentFiles = this.Settings.RecentFiles;
			int i = recentFiles.Count - 1;
			int num = i;
			while (i >= 0)
			{
				if (!(recentFiles[i] != File))
				{
					if (i == num)
					{
						return;
					}
					recentFiles.RemoveAt(i);
					break;
				}
				else
				{
					i--;
				}
			}
			if (recentFiles.Count >= 10)
			{
				recentFiles.RemoveAt(0);
			}
			recentFiles.Add(File);
			this.UpdateRecent();
			this.WriteModSettings();
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000B9FF7 File Offset: 0x000B81F7
		public bool IsSingleObjectBrush()
		{
			return this.Brush.width == 1 && this.Brush.height == 1 && this.Brush.GetOrCreateCellAt(0, 0).Objects.Count == 1;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000BA034 File Offset: 0x000B8234
		public string GetBrushText()
		{
			if (this.Brush.width != 1 || this.Brush.height != 1)
			{
				List<MapFileObjectBlueprint> list = new List<MapFileObjectBlueprint>();
				foreach (MapFileObjectReference mapFileObjectReference in this.Brush.AllObjects(null))
				{
					list.Add(mapFileObjectReference.blueprint);
				}
				return string.Format("{0}x{1} brush, {2} objects", this.Brush.width, this.Brush.height, list.Count);
			}
			MapFileCell orCreateCellAt = this.Brush.GetOrCreateCellAt(0, 0);
			if (orCreateCellAt.Objects.Count == 1)
			{
				return orCreateCellAt.Objects[0].Name;
			}
			List<MapFileObjectBlueprint> list2 = new List<MapFileObjectBlueprint>();
			foreach (MapFileObjectReference mapFileObjectReference2 in this.Brush.AllObjects(null))
			{
				list2.Add(mapFileObjectReference2.blueprint);
			}
			return list2.Count.ToString() + " objects";
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000BA184 File Offset: 0x000B8384
		public void SetSelectedBlueprint(string blueprintName)
		{
			this.Brush = new MapFileRegion(1, 1);
			this.Brush.GetOrCreateCellAt(0, 0).Objects.Add(new MapFileObjectBlueprint(blueprintName, null, null));
			this.SetBrush(this.Brush);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000BA1BE File Offset: 0x000B83BE
		public void SetSelectedBlueprint(MapFileObjectBlueprint blueprint)
		{
			this.Brush = new MapFileRegion(1, 1);
			this.Brush.GetOrCreateCellAt(0, 0).Objects.Add(new MapFileObjectBlueprint(blueprint));
			this.SetBrush(this.Brush);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000BA1F8 File Offset: 0x000B83F8
		public void SetBrush(MapFileRegion brush)
		{
			this.DataFilter = this.DataFilter;
			this.Brush = brush;
			this.GhostHighlight.SetActive(false);
			global::UnityEngine.GameObject gameObject = base.FindChild("SelectionHeader/SelectedItem");
			global::UnityEngine.UI.Text text = ((gameObject != null) ? gameObject.GetComponent<global::UnityEngine.UI.Text>() : null);
			if (text)
			{
				text.text = string.Format("Brush: {0}", this.GetBrushText());
			}
			this.UpdateSelectedContents();
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000BA260 File Offset: 0x000B8460
		public void FilterUsedUpdated()
		{
			this.FilterUsed = base.FindChild("FilterUsedButton").GetComponent<Toggle>().isOn;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000BA27D File Offset: 0x000B847D
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x000BA285 File Offset: 0x000B8485
		public bool FilterUsed
		{
			get
			{
				return this._FilterUsed;
			}
			set
			{
				this._FilterUsed = value;
				base.FindChild("FilterUsedButton").GetComponent<Toggle>().isOn = value;
				this.UpdateFilter();
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000BA2AC File Offset: 0x000B84AC
		public void UpdateHoverXY(int X, int Y)
		{
			if (!this.IsSingleObjectBrush() && (this.Brush.width != 1 || this.Brush.height != 1))
			{
				this.GhostHighlight.transform.parent = GameManager.Instance.ConsoleCharacter[X, Y].transform;
				this.GhostHighlight.transform.localPosition = Vector3.back * 2f;
				this.GhostHighlight.GetComponent<MapEditorSelectionWidget>().SetTileSize(this.Brush.width, this.Brush.height);
				this.GhostHighlight.SetActive(true);
			}
			base.FindChild("SelectionHeader/PositionText").GetComponent<global::UnityEngine.UI.Text>().text = string.Format("Mouse Position: {0}, {1}", X, Y);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000BA384 File Offset: 0x000B8584
		public void UpdateSelectedXY()
		{
			if (!this.HasSelectedRegion)
			{
				base.FindChild("SelectionHeader/SelectedText").GetComponent<global::UnityEngine.UI.Text>().text = string.Format("Selected Cell: none", Array.Empty<object>());
				return;
			}
			if (this.SelectedRegion.size == Vector2.zero)
			{
				base.FindChild("SelectionHeader/SelectedText").GetComponent<global::UnityEngine.UI.Text>().text = string.Format("Selected Cell: {0}, {1}", this.SelectedRegion.x, this.SelectedRegion.y);
				return;
			}
			base.FindChild("SelectionHeader/SelectedText").GetComponent<global::UnityEngine.UI.Text>().text = string.Format("Selected Cell: {0}, {1} - {2}, {3}", new object[]
			{
				this.SelectedRegion.xMin,
				this.SelectedRegion.yMin,
				this.SelectedRegion.xMax,
				this.SelectedRegion.yMax
			});
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000BA488 File Offset: 0x000B8688
		public string GetDefaultPath()
		{
			if (this.Settings != null && !this.Settings.DefaultPath.IsNullOrEmpty() && Directory.Exists(this.Settings.DefaultPath))
			{
				return this.Settings.DefaultPath;
			}
			return Path.Combine(Application.streamingAssetsPath, "Base").Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000BA4F4 File Offset: 0x000B86F4
		public void SetDefaultPath(string Path)
		{
			if (Path.IsNullOrEmpty())
			{
				return;
			}
			if (this.Settings == null)
			{
				return;
			}
			if (Path.Contains('\\'))
			{
				Path = Path.Replace('\\', '/');
			}
			if (this.Settings.DefaultPath == Path)
			{
				return;
			}
			this.Settings.DefaultPath = Path;
			this.WriteModSettings();
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000BA54F File Offset: 0x000B874F
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x000BA557 File Offset: 0x000B8757
		public string DataFilter
		{
			get
			{
				return this._DataFilter;
			}
			set
			{
				this._DataFilter = value;
				this.UpdateFilter();
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000BA568 File Offset: 0x000B8768
		private void UpdateFilter()
		{
			HashSet<string> hashSet = null;
			if (this.FilterUsed)
			{
				hashSet = this.Map.UsedBlueprints(null);
			}
			base.GetChild("BlueprintScrollerController").rootObject.GetComponent<BlueprintScrollerController>().UpdateDataFilter(this.DataFilter, hashSet);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000BA5B0 File Offset: 0x000B87B0
		public override void Update()
		{
			this.t += Time.deltaTime;
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
			{
				this.HasSelectedRegion = false;
				this.RenderSelectionFrame();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.Z) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				Debug.Log("undo");
				this.Undo();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.F1) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.ToggleOverlay("Backdrops/NorthShevaBackdrop");
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.Y) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				Debug.Log("redo");
				this.Redo();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.N) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.NewButton();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.O) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.Load();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.S) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.Save();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.T) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.Test();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.A) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.SelectAll();
			}
			if (Input.GetKeyDown(global::UnityEngine.KeyCode.C) && (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl)))
			{
				this.SetBrush(this.Map.Cells.GetRegion(this.SelectedRegion).Clone());
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000BA77E File Offset: 0x000B897E
		public void SelectAll()
		{
			this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
			this.HasSelectedRegion = true;
			this.RenderSelectionFrame();
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000BA7AC File Offset: 0x000B89AC
		public void RenderSelectionFrame()
		{
			this.SelectionFrame.SetActive(this.HasSelectedRegion);
			if (this.HasSelectedRegion)
			{
				this.SelectionFrame.transform.parent = GameManager.Instance.ConsoleCharacter[(int)this.SelectedRegion.xMin, (int)this.SelectedRegion.yMin].transform;
				this.SelectionFrame.transform.localPosition = Vector3.back;
				this.SelectionFrame.GetComponent<MapEditorSelectionWidget>().SetTileSize((int)this.SelectedRegion.size.x + 1, (int)this.SelectedRegion.size.y + 1);
			}
			this.UpdateSelectedXY();
			this.UpdateSelectedContents();
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000BA868 File Offset: 0x000B8A68
		public void OnClick(Vector2 FingerPos, PointerEventData.InputButton Button)
		{
			Vector3 vector = GameManager.MainCamera.GetComponent<Camera>().ScreenToWorldPoint(FingerPos);
			foreach (RaycastHit raycastHit in Physics.RaycastAll(new Vector3(vector.x, vector.y, -10000f), new Vector3(0f, 0f, 1f)))
			{
				TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
				if (component != null)
				{
					if (Button == PointerEventData.InputButton.Left)
					{
						this.OnCommand("Tile:" + component.x.ToString() + "," + component.y.ToString(), 0);
					}
					if (Button == PointerEventData.InputButton.Right)
					{
						this.OnCommand("RightTile:" + component.x.ToString() + "," + component.y.ToString(), 0);
					}
					if (Button == PointerEventData.InputButton.Middle)
					{
						this.OnCommand("MiddleTile:" + component.x.ToString() + "," + component.y.ToString(), 0);
					}
					return;
				}
			}
			this.HasSelectedRegion = false;
			this.RenderSelectionFrame();
			this.RenderMap();
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000BA9A4 File Offset: 0x000B8BA4
		private void UpdateSelectedContents()
		{
			if (!this.HasSelectedRegion)
			{
				base.GetChildComponent<SelectedCellContentsView>("SelectedCellContents").Set(new MapFileRegion(0, 0), false);
				return;
			}
			base.GetChildComponent<SelectedCellContentsView>("SelectedCellContents").Set(this.Map.Cells.GetRegion(this.SelectedRegion), this.IsSingleObjectBrush());
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000BA9FE File Offset: 0x000B8BFE
		public override void OnGUI()
		{
			if (!this._HasRendered)
			{
				this.RenderMap();
				this._HasRendered = true;
			}
			if (!this._HasSelectionRendered)
			{
				this.RenderSelectionFrame();
				this._HasSelectionRendered = true;
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000BAA2C File Offset: 0x000B8C2C
		public void OnBeginDrag(PointerEventData eventData, TileBehavior T)
		{
			if (T == null)
			{
				this.isDragging = false;
				this.HasSelectedRegion = false;
				return;
			}
			this._cameraStart = Camera.main.transform.localPosition;
			this._dragStart = new Vector2((float)T.x, (float)T.y);
			this._dragDelta = Vector2.zero;
			this.isDragging = true;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000BAA94 File Offset: 0x000B8C94
		public bool OnDragMove(Vector2 FingerPos, Vector2 Delta, PointerEventData.InputButton Button)
		{
			this._dragDelta += Delta;
			if (Input.GetKey(global::UnityEngine.KeyCode.LeftShift) || Input.GetKey(global::UnityEngine.KeyCode.RightShift))
			{
				Vector3 vector = GameManager.MainCamera.GetComponent<Camera>().ScreenToWorldPoint(FingerPos + Delta);
				RaycastHit[] array = Physics.RaycastAll(new Vector3(vector.x, vector.y, -10000f), new Vector3(0f, 0f, 1f));
				TileBehavior tileBehavior = null;
				foreach (RaycastHit raycastHit in array)
				{
					TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
					if (component != null)
					{
						tileBehavior = component;
					}
				}
				if (tileBehavior != null && this.isDragging)
				{
					this.SelectedRegion = default(Rect);
					Vector2 vector2 = new Vector2((float)tileBehavior.x, (float)tileBehavior.y);
					this.SelectedRegion.min = Vector2.Min(this._dragStart, vector2);
					this.SelectedRegion.max = Vector2.Max(this._dragStart, vector2);
					this.HasSelectedRegion = true;
					this.RenderSelectionFrame();
				}
				return true;
			}
			if (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl))
			{
				Vector3 vector3 = GameManager.MainCamera.GetComponent<Camera>().ScreenToWorldPoint(FingerPos + Delta);
				RaycastHit[] array3 = Physics.RaycastAll(new Vector3(vector3.x, vector3.y, -10000f), new Vector3(0f, 0f, 1f));
				bool flag = false;
				RaycastHit raycastHit2 = default(RaycastHit);
				foreach (RaycastHit raycastHit3 in array3)
				{
					if (raycastHit3.collider.gameObject.GetComponent<TileBehavior>() != null)
					{
						raycastHit2 = raycastHit3;
						flag = true;
					}
				}
				if (flag)
				{
					TileBehavior component2 = raycastHit2.collider.gameObject.GetComponent<TileBehavior>();
					if (this.Map != null && this.IsSingleObjectBrush())
					{
						int x = component2.x;
						int y = component2.y;
						string name = this.Brush.GetOrCreateCellAt(0, 0).Objects[0].Name;
						if (Button == PointerEventData.InputButton.Left)
						{
							for (int j = 0; j < this.Map.Cells[x, y].Objects.Count; j++)
							{
								if (this.Map.Cells[x, y].Objects[j].Name == name)
								{
									return true;
								}
							}
							MapFileObjectBlueprint mapFileObjectBlueprint = new MapFileObjectBlueprint(name, null, null);
							this.Map.Cells[x, y].Objects.Add(mapFileObjectBlueprint);
							this.UndoStack.Push(new MapEditorView.MapEditorAction("Add", this.Map.Cells[x, y], mapFileObjectBlueprint));
							this.RedoStack.Clear();
							this.RenderSelectionFrame();
							this.RenderMap();
							return true;
						}
						if (Button == PointerEventData.InputButton.Right && this.Map.Cells[x, y].Objects.Count > 0)
						{
							MapEditorView.Instance.RedoStack.Clear();
							List<string> list = new List<string>();
							foreach (MapFileObjectBlueprint mapFileObjectBlueprint2 in this.Map.Cells[x, y].Objects)
							{
								MapEditorView.Instance.UndoStack.Push(new MapEditorView.MapEditorAction("Remove", this.Map.Cells[x, y], mapFileObjectBlueprint2));
								list.Add(mapFileObjectBlueprint2.Name);
							}
							this.Map.Cells[x, y].Objects.Clear();
							this.RenderSelectionFrame();
							this.RenderMap();
							return true;
						}
					}
				}
			}
			else
			{
				LetterboxCamera component3 = GameManager.MainCamera.GetComponent<LetterboxCamera>();
				Vector3 vector4 = this._cameraStart - new Vector3(this._dragDelta.x * component3.UnitsPerPixel, this._dragDelta.y * component3.UnitsPerPixel, 0f);
				component3.SetPositionImmediately(vector4);
			}
			return true;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000BAEF4 File Offset: 0x000B90F4
		public MapEditorView.MapEditorAction ReverseAction(MapEditorView.MapEditorAction A)
		{
			if (A.Action == "Add")
			{
				for (int i = 0; i < A.Cell.Objects.Count; i++)
				{
					if (A.Cell.Objects[i] == A.Brush)
					{
						A.Cell.Objects.RemoveAt(i);
						return new MapEditorView.MapEditorAction("Remove", A.Cell, A.Brush);
					}
				}
			}
			if (A.Action == "AddMulti")
			{
				MapFileRegion mapFileRegion = (MapFileRegion)A.AdditionalData;
				int num = (int)A.Region.xMin;
				int num2 = (int)A.Region.yMin;
				using (IEnumerator<MapFileObjectReference> enumerator = mapFileRegion.AllObjects(null).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MapFileObjectReference oref = enumerator.Current;
						if (oref.x + num <= 79 && oref.y + num2 <= 24)
						{
							MapFileCell mapFileCell = this.Map.Cells[oref.x + num, oref.y + num2];
							int num3 = mapFileCell.Objects.FindLastIndex((MapFileObjectBlueprint o) => o.Name == oref.blueprint.Name);
							if (num3 != -1)
							{
								mapFileCell.Objects.RemoveAt(num3);
							}
						}
					}
				}
				return new MapEditorView.MapEditorAction("DeleteMulti", null, null, A.Region, A.AdditionalData);
			}
			if (A.Action == "DeleteMulti")
			{
				MapFileRegion mapFileRegion2 = (MapFileRegion)A.AdditionalData;
				int num4 = (int)A.Region.xMin;
				int num5 = (int)A.Region.yMin;
				foreach (MapFileObjectReference mapFileObjectReference in mapFileRegion2.AllObjects(null))
				{
					if (mapFileObjectReference.x + num4 <= 79 && mapFileObjectReference.y + num5 <= 24)
					{
						this.Map.Cells[mapFileObjectReference.x + num4, mapFileObjectReference.y + num5].Objects.Add(new MapFileObjectBlueprint(mapFileObjectReference.blueprint));
					}
				}
				return new MapEditorView.MapEditorAction("AddMulti", null, null, A.Region, A.AdditionalData);
			}
			if (A.Action == "Remove")
			{
				A.Cell.Objects.Add(new MapFileObjectBlueprint(A.Brush));
				return new MapEditorView.MapEditorAction("Add", A.Cell, A.Brush);
			}
			if (A.Action == "FlipH")
			{
				this.FlipHorizontal(A.Region);
			}
			if (A.Action == "FlipV")
			{
				this.FlipVertical(A.Region);
			}
			if (A.Action == "BulkDelete")
			{
				A.Action = "BulkAdd";
				MapFileRegion region = this.Map.Cells.GetRegion(A.Region);
				using (Dictionary<MapFileCellReference, int>.Enumerator enumerator2 = (A.AdditionalData as Dictionary<MapFileCellReference, int>).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<MapFileCellReference, int> keyValuePair = enumerator2.Current;
						for (int j = 0; j < keyValuePair.Value; j++)
						{
							region[keyValuePair.Key.x, keyValuePair.Key.y].Objects.Add(new MapFileObjectBlueprint(A.Brush));
						}
					}
					goto IL_06BA;
				}
			}
			if (A.Action == "BulkAdd")
			{
				A.Action = "BulkDelete";
				MapFileRegion region2 = this.Map.Cells.GetRegion(A.Region);
				using (Dictionary<MapFileCellReference, int>.Enumerator enumerator2 = (A.AdditionalData as Dictionary<MapFileCellReference, int>).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<MapFileCellReference, int> keyValuePair2 = enumerator2.Current;
						int count = keyValuePair2.Value;
						region2[keyValuePair2.Key.x, keyValuePair2.Key.y].Objects.RemoveAll(delegate(MapFileObjectBlueprint o)
						{
							if (o == A.Brush)
							{
								int count2 = count;
								count = count2 - 1;
								return count2 > 0;
							}
							return false;
						});
					}
					goto IL_06BA;
				}
			}
			if (A.Action == "BulkReplace")
			{
				MapFileRegion region3 = this.Map.Cells.GetRegion(A.Region);
				MapEditorView.ReplaceOperationNotes notes = (MapEditorView.ReplaceOperationNotes)A.AdditionalData;
				foreach (MapFileCellReference mapFileCellReference in notes.replaceCount.Keys)
				{
					int toReplace = notes.replaceCount[mapFileCellReference];
					for (int k = 0; k < toReplace; k++)
					{
						region3[mapFileCellReference.x, mapFileCellReference.y].Objects.Add(new MapFileObjectBlueprint(notes.Original));
					}
					region3[mapFileCellReference.x, mapFileCellReference.y].Objects.RemoveAll(delegate(MapFileObjectBlueprint o)
					{
						if (o == notes.Replacement)
						{
							int toReplace2 = toReplace;
							toReplace = toReplace2 - 1;
							return toReplace2 > 0;
						}
						return false;
					});
				}
				MapFileObjectBlueprint original = notes.Original;
				notes.Original = notes.Replacement;
				notes.Replacement = original;
				A.AdditionalData = notes;
			}
			IL_06BA:
			return A;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000BB604 File Offset: 0x000B9804
		public override void OnCommand(string Command)
		{
			this.OnCommand(Command, 0);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000BB610 File Offset: 0x000B9810
		public override void OnLeave()
		{
			Options.PlayScaleOverride = null;
			Camera.main.GetComponent<LetterboxCamera>().suspendUpdate = false;
			base.OnLeave();
			global::UnityEngine.GameObject selectionFrame = this.SelectionFrame;
			if (selectionFrame != null)
			{
				selectionFrame.SetActive(false);
			}
			global::UnityEngine.GameObject ghostHighlight = this.GhostHighlight;
			if (ghostHighlight == null)
			{
				return;
			}
			ghostHighlight.SetActive(false);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000BB664 File Offset: 0x000B9864
		public override void OnEnter()
		{
			Options.PlayScaleOverride = new Options.PlayAreaScaleTypes?(Options.PlayAreaScaleTypes.Fit);
			Camera.main.GetComponent<LetterboxCamera>().suspendUpdate = true;
			MapEditorView.Instance = this;
			base.OnEnter();
			if (this.SelectionFrame == null)
			{
				this.SelectionFrame = PrefabManager.Create("UI/SelectionFrame", "Prefabs/");
			}
			if (this.GhostHighlight == null)
			{
				this.GhostHighlight = PrefabManager.Create("UI/SelectionFrame", "Prefabs/");
				this.GhostHighlight.GetComponent<MapEditorSelectionWidget>().Color1 = new Color(1f, 1f, 0f, 1f);
				this.GhostHighlight.GetComponent<MapEditorSelectionWidget>().Color2 = new Color(1f, 1f, 0f, 0.75f);
				this.GhostHighlight.SetActive(false);
			}
			this.SelectionFrame.GetComponent<MapEditorSelectionWidget>().SetTileSize(1, 1);
			this.SelectionFrame.SetActive(false);
			base.GetChild("BlueprintScrollerController").rootObject.GetComponent<BlueprintScrollerController>().UpdateDataFilter("", null);
			this._HasRendered = false;
			if (this.Map == null)
			{
				this.NewMap();
			}
			if (!this.setup)
			{
				this.setup = true;
				this.MenuBar = base.GetChild("MenuBar").rootObject.GetComponent<MenuBar>();
				Menu menu = DialogManager.createMenu();
				menu.addItem(null, "New map").setHotKeyText("Ctrl+N").setMenuItemClicked(new Action(this.NewButton));
				menu.addItem(null, "Load map...").setHotKeyText("Ctrl+O").setMenuItemClicked(new Action(this.Load));
				menu.addItem(null, "Test").setHotKeyText("Ctrl+T").setMenuItemClicked(new Action(this.Test));
				menu.addItem(null, "Save").setHotKeyText("Ctrl+S").setMenuItemClicked(new Action(this.Save));
				menu.addItem(null, "Save As...").setMenuItemClicked(new Action(this.SaveAs));
				menu.addSeparator();
				menu.addItem(null, "Reload Blueprints").setMenuItemClicked(new Action(this.ReloadBlueprints));
				menu.addSeparator();
				menu.addItem(null, "Exit").setMenuItemClicked(new Action(this.Exit));
				this.MenuBar.addMenu("File", menu);
				Menu menu2 = DialogManager.createMenu();
				menu2.addItem(null, "Select All").setHotKeyText("Ctrl+A").setMenuItemClicked(new Action(this.SelectAll));
				menu2.addItem(null, "Undo").setHotKeyText("Ctrl+Z").setMenuItemClicked(new Action(this.Undo));
				menu2.addItem(null, "Redo").setHotKeyText("Ctrl+Y").setMenuItemClicked(new Action(this.Redo));
				this.MenuBar.addMenu("Edit", menu2);
				Menu menu3 = DialogManager.createMenu();
				menu3.addItem(null, "Set ID").setMenuItemClicked(new Action(this.SetMapID));
				menu3.addItem(null, "Flip Horizontal").setMenuItemClicked(new Action(this.FlipHorizontal));
				menu3.addItem(null, "Flip Vertical").setMenuItemClicked(new Action(this.FlipVertical));
				this.MenuBar.addMenu("Map", menu3);
				Menu menu4 = DialogManager.createMenu();
				menu4.addItem(null, "Toggle NorthSheva Overlay").setHotKeyText("Ctrl+F1").setMenuItemClicked(delegate
				{
					this.ToggleOverlay("Backdrops/NorthShevaBackdrop");
				});
				this.MenuBar.addMenu("View", menu4);
				this.RecentMenu = DialogManager.createMenu();
				this.MenuBar.addMenu("Recent", this.RecentMenu);
				this.ContextMenu = DialogManager.createMenu();
				this.ContextMenu.transform.localScale = new Vector3(0.5f, 0.5f);
				this.ContextMenu.addItem(null, "Set owner").setMenuItemClicked(new Action(this.SetOwnerForContext));
				this.ContextMenu.addItem(null, "Set part").setMenuItemClicked(new Action(this.SetPartForContext));
				this.ContextMenu.addItem(null, "Add string property").setMenuItemClicked(new Action(this.AddPropertyForContext));
				this.ContextMenu.addItem(null, "Add int property").setMenuItemClicked(new Action(this.AddIntPropertyForContext));
				this.ContextMenu.addItem(null, "Remove property").setMenuItemClicked(new Action(this.RemovePropertyForContext));
				this.ContextMenu.DestroyOnClose = false;
			}
			if (this.Settings == null)
			{
				this.ReadSettings();
			}
			this.UpdateTitle();
			this.RenderMap();
			this.RenderSelectionFrame();
			this.UpdateRecent();
			TextConsole.BufferUpdated = true;
			if (this.FilterUsed)
			{
				this.UpdateFilter();
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000BBB60 File Offset: 0x000B9D60
		public void ReadSettings()
		{
			try
			{
				string text = DataManager.LocalPath("MapEditorSettings.json");
				if (File.Exists(text))
				{
					this.Settings = ModManager.JsonSerializer.Deserialize<MapEditorSettings>(text);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error loading map editor settings", ex, "game_exception");
			}
			if (this.Settings == null)
			{
				this.Settings = new MapEditorSettings();
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000BBBCC File Offset: 0x000B9DCC
		public void WriteModSettings()
		{
			if (this.Settings == null)
			{
				return;
			}
			try
			{
				string text = DataManager.LocalPath("MapEditorSettings.json");
				ModManager.JsonSerializer.Serialize(text, this.Settings);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error saving map editor settings", ex, "game_exception");
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000BBC24 File Offset: 0x000B9E24
		public void UpdateTitle()
		{
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				TMP_Text component = base.GetChild("Title").rootObject.GetComponent<TextMeshProUGUI>();
				textBuilder.Append("Map Editor");
				if (!this.Map.ID.IsNullOrEmpty())
				{
					textBuilder.Compound(this.Map.ID, " - ");
				}
				if (!this.FileName.IsNullOrEmpty())
				{
					textBuilder.Compound(Path.GetFileName(this.FileName), " - ");
				}
				component.text = textBuilder.ToString();
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000BBCD0 File Offset: 0x000B9ED0
		public void UpdateRecent()
		{
			Menu recentMenu = this.RecentMenu;
			int count = recentMenu.Items.Count;
			Rack<string> recentFiles = this.Settings.RecentFiles;
			if (!recentFiles.IsNullOrEmpty<string>())
			{
				int i = 0;
				int num = recentFiles.Count - 1;
				while (i <= num)
				{
					string fileName = Path.GetFileName(recentFiles[num - i]);
					if (i < count)
					{
						recentMenu.Items[i].setText(fileName);
					}
					else
					{
						recentMenu.addItem(null, fileName).setMenuItemClicked(this.GetRecentClick(i));
					}
					i++;
				}
				return;
			}
			if (count == 0)
			{
				recentMenu.addItem(null, "No history").setMenuItemClicked(this.GetRecentClick(0));
				return;
			}
			recentMenu.Items[0].setText("No history");
			for (int j = count - 1; j > 0; j--)
			{
				recentMenu.RemoveAt(j);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000BBDB0 File Offset: 0x000B9FB0
		private Action GetRecentClick(int Index)
		{
			for (int i = this.RecentDelegates.Count; i <= Index; i++)
			{
				int clickIndex = i;
				this.RecentDelegates.Add(delegate
				{
					this.RecentClick(clickIndex);
				});
			}
			return this.RecentDelegates[Index];
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000BBE0C File Offset: 0x000BA00C
		public void RecentClick(int Index)
		{
			int count = this.Settings.RecentFiles.Count;
			if (Index < count)
			{
				this.LoadMap(this.Settings.RecentFiles[count - Index - 1]);
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000BBE4C File Offset: 0x000BA04C
		public void FlipHorizontal()
		{
			Rect selectedRegion = this.SelectedRegion;
			if (!this.HasSelectedRegion || (this.SelectedRegion.width == 0f && this.SelectedRegion.height == 0f))
			{
				selectedRegion = new Rect(0f, 0f, 79f, 24f);
			}
			this.FlipHorizontal(selectedRegion);
			this.UndoStack.Push(new MapEditorView.MapEditorAction("FlipH", null, null, selectedRegion, null));
			this.RedoStack.Clear();
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000BBED4 File Offset: 0x000BA0D4
		public void FlipHorizontal(Rect region)
		{
			this.Map.Cells.SetRegion(this.Map.Cells.GetRegion(region).FlippedHorizontal(), (int)region.xMin, (int)region.yMin);
			MapEditorView.Instance.RenderMap();
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000BBF24 File Offset: 0x000BA124
		public void FlipVertical()
		{
			Rect selectedRegion = this.SelectedRegion;
			if (!this.HasSelectedRegion || (this.SelectedRegion.width == 0f && this.SelectedRegion.height == 0f))
			{
				selectedRegion = new Rect(0f, 0f, 79f, 24f);
			}
			this.FlipVertical(selectedRegion);
			this.UndoStack.Push(new MapEditorView.MapEditorAction("FlipV", null, null, selectedRegion, null));
			this.RedoStack.Clear();
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000BBFAC File Offset: 0x000BA1AC
		public void FlipVertical(Rect region)
		{
			this.Map.Cells.SetRegion(this.Map.Cells.GetRegion(region).FlippedVertical(), (int)region.xMin, (int)region.yMin);
			MapEditorView.Instance.RenderMap();
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		public IEnumerator _ReloadBlueprints()
		{
			base.GetChild("Working")._rootObject.SetActive(true);
			base.GetChildComponent<global::UnityEngine.UI.Text>("Working/Text").text = "Reloading ObjectBlueprints.xml\nMay take a few seconds...";
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			try
			{
				GameObjectFactory.Factory.Hotload();
				base.GetChild("BlueprintScrollerController").rootObject.GetComponent<BlueprintScrollerController>().ResetCaches();
				this.UpdateFilter();
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error hotloading", ex);
				DialogManager.notify("There was an error while loading ObjectBlueprints.xml", null);
			}
			base.GetChild("Working")._rootObject.SetActive(false);
			base.GetChildComponent<global::UnityEngine.UI.Text>("Working/Text").text = "Working...";
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield break;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000BC00B File Offset: 0x000BA20B
		public void ReloadBlueprints()
		{
			GameManager.Instance.StartCoroutine(this._ReloadBlueprints());
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000BC01E File Offset: 0x000BA21E
		public override bool BeforeLeave()
		{
			base.BeforeLeave();
			GameManager.Instance.PopGameView(false);
			return true;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000BC034 File Offset: 0x000BA234
		public void Undo()
		{
			if (this.Map != null && this.UndoStack.Count > 0)
			{
				MapEditorView.MapEditorAction mapEditorAction = this.UndoStack.Pop();
				MapEditorView.MapEditorAction mapEditorAction2 = this.ReverseAction(mapEditorAction);
				this.RedoStack.Push(mapEditorAction2);
			}
			this.RenderSelectionFrame();
			this.RenderMap();
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000BC084 File Offset: 0x000BA284
		public void Redo()
		{
			if (this.Map != null && this.RedoStack.Count > 0)
			{
				MapEditorView.MapEditorAction mapEditorAction = this.RedoStack.Pop();
				MapEditorView.MapEditorAction mapEditorAction2 = this.ReverseAction(mapEditorAction);
				this.UndoStack.Push(mapEditorAction2);
			}
			this.RenderSelectionFrame();
			this.RenderMap();
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000BC0D4 File Offset: 0x000BA2D4
		public void NewMap()
		{
			this.Map = new MapFile(80, 25);
			this.Map.Cells.FillEmptyCells();
			this.UndoStack = new Stack<MapEditorView.MapEditorAction>();
			this.RedoStack = new Stack<MapEditorView.MapEditorAction>();
			this.FileName = null;
			this.UpdateTitle();
			this._HasRendered = false;
			if (this.FilterUsed)
			{
				this.UpdateFilter();
			}
			this.RenderMap();
			this.RenderSelectionFrame();
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000BC144 File Offset: 0x000BA344
		public void NewButton()
		{
			DialogManager.createMessageDialog().show("Create a new map?", delegate(Buttons b)
			{
				if (b == Buttons.Yes)
				{
					this.NewMap();
				}
			}, null, Buttons.Yes | Buttons.No);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000BC164 File Offset: 0x000BA364
		public void Exit()
		{
			DialogManager.createMessageDialog().show("Are you sure you want to go back and lose any unsaved changes?", delegate(Buttons b)
			{
				if (b == Buttons.Yes)
				{
					UIManager.showWindow("ModToolkit", false);
				}
			}, null, Buttons.Yes | Buttons.No);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000BC198 File Offset: 0x000BA398
		public void Save()
		{
			if (this.FileName != null)
			{
				this.Map.Save(this.FileName);
				DialogManager.notify("Map saved!", null);
				return;
			}
			DialogManager.createFileDialog().show(FileDialogMode.Save, "Save Map", "*.rpm", new Action<string>(this.SaveCallback), "newmap", this.GetDefaultPath());
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000BC1F6 File Offset: 0x000BA3F6
		public void Test()
		{
			if (this.Map == null)
			{
				DialogManager.notify("No map is loaded for testing.", null);
				return;
			}
			DialogManager.notify("Starting test...", null);
			this.RunTest();
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000BC220 File Offset: 0x000BA420
		private async void RunTest()
		{
			await The.GameContext;
			XRLCore core = The.Core;
			XRLGame xrlgame = new XRLGame();
			core.Game = xrlgame;
			xrlgame.CreateNewGame();
			string text = Guid.NewGuid().ToString();
			xrlgame.SetStringGameState("OriginalWorldSeed", text);
			xrlgame.IsSeeded = true;
			Stat.ReseedFrom(text, true);
			Zone zone = this.BuildTestZone(xrlgame);
			global::XRL.World.GameObject gameObject = global::XRL.World.GameObject.Create("Snapjaw Scavenger");
			gameObject.GiveProperName(null, false, null, false, null, null, null, null);
			gameObject.ReceiveObject("Torch", 5, false, 0, 0, null, null, null, null);
			gameObject.ReceiveObject("HalfFullWaterskin", 2, false, 0, 0, null, null, null, null);
			foreach (string text2 in Statistic.Attributes)
			{
				gameObject.GetStat(text2).BaseValue = 20;
			}
			gameObject.GetStat("Hitpoints").BaseValue = 100;
			gameObject.Brain.Factions = "Player-100";
			gameObject.RemovePart<HeightenedSmell>();
			Cell cell = null;
			if (this.HasSelectedRegion)
			{
				Vector2 center = this.SelectedRegion.center;
				cell = zone.GetCell(Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.y));
				if (cell != null && !cell.IsPassable(gameObject, true))
				{
					cell = cell.getClosestPassableCellFor(gameObject);
				}
			}
			if (cell == null)
			{
				cell = zone.GetPullDownLocation(gameObject);
			}
			cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			xrlgame.Running = true;
			xrlgame.Transient = true;
			xrlgame.DontSaveThisIsAReplay = true;
			xrlgame.ExitViewStack = GameManager.Instance.GetGameViewStackCopy();
			xrlgame.Player.Body = gameObject;
			xrlgame.RegisterEvent(new MapEditorView.CanTravelHandler(), PooledEvent<CanTravelEvent>.ID, 0, false);
			xrlgame.RegisterEvent(new MapEditorView.GetZoneHandler(), PooledEvent<GetZoneEvent>.ID, 0, false);
			GameManager.Instance.PushGameView(Options.StageViewID, true);
			Keyboard.PushMouseEvent("EscapeGetVK", null);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000BC258 File Offset: 0x000BA458
		private Zone BuildTestZone(XRLGame Game)
		{
			ZoneManager zoneManager = Game.ZoneManager;
			Zone zone = new Zone(80, 25);
			zone.ZoneID = "MapEditor";
			zone.Fill("TerrainBrightsheol");
			zone.Built = true;
			zoneManager.AddCachedZone(zone);
			Zone zone2 = new Zone(80, 25);
			zone2.ZoneID = "MapEditor.1.1.1.1.10";
			zoneManager.AddCachedZone(zone2);
			foreach (MapFileCellReference mapFileCellReference in this.Map.Cells.AllCells(null))
			{
				Cell cell = zone2.GetCell(mapFileCellReference.x, mapFileCellReference.y);
				if (cell != null)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
					{
						global::XRL.World.GameObject gameObject = global::XRL.World.GameObject.Create(mapFileObjectBlueprint.Name);
						if (!gameObject.IsInvalid())
						{
							if (!mapFileObjectBlueprint.Owner.IsNullOrEmpty())
							{
								gameObject.Physics.Owner = mapFileObjectBlueprint.Owner;
							}
							if (!mapFileObjectBlueprint.Part.IsNullOrEmpty())
							{
								Type type = ModManager.ResolveType("XRL.World.Parts", mapFileObjectBlueprint.Part, false, false, true);
								if (type != null)
								{
									GamePartBlueprint.PartReflectionCache partReflectionCache = GamePartBlueprint.PartReflectionCache.Get(type);
									gameObject.AddPart(partReflectionCache.GetNewInstance(), true, false);
								}
							}
							if (!mapFileObjectBlueprint.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
							{
								foreach (KeyValuePair<string, string> keyValuePair in mapFileObjectBlueprint.Properties)
								{
									string text;
									string text2;
									keyValuePair.Deconstruct(out text, out text2);
									string text3 = text;
									string text4 = text2;
									gameObject.Property[text3] = text4;
								}
							}
							if (!mapFileObjectBlueprint.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
							{
								foreach (KeyValuePair<string, int> keyValuePair2 in mapFileObjectBlueprint.IntProperties)
								{
									string text2;
									int num;
									keyValuePair2.Deconstruct(out text2, out num);
									string text5 = text2;
									int num2 = num;
									gameObject.IntProperty[text5] = num2;
								}
							}
							cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			zone2.Built = true;
			BeforeZoneBuiltEvent.Send(zone2);
			ZoneBuiltEvent.Send(zone2);
			ZoneManager.PaintWalls(zone2, 0, 0, -1, -1);
			ZoneManager.PaintWater(zone2, 0, 0, -1, -1);
			AfterZoneBuiltEvent.Send(zone2);
			return zone2;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000BC538 File Offset: 0x000BA738
		public void SaveAs()
		{
			FileDialog fileDialog = DialogManager.createFileDialog();
			string text = Path.GetFileName(this.FileName).Replace(".", " - copy.");
			fileDialog.show(FileDialogMode.Save, "Save Map As", "*.rpm", new Action<string>(this.SaveCallback), text, this.GetDefaultPath());
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000BC588 File Offset: 0x000BA788
		public void SaveCallback(string File)
		{
			if (File.IsNullOrEmpty())
			{
				return;
			}
			this.FileName = File;
			this.UpdateTitle();
			this.Map.Save(File);
			DialogManager.notify("Map saved!", null);
			this.SetDefaultPath(Path.GetDirectoryName(File));
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000BC5C3 File Offset: 0x000BA7C3
		public void Load()
		{
			DialogManager.createFileDialog().show(FileDialogMode.Load, "Load Map", "*.rpm", new Action<string>(this.LoadCallback), null, this.GetDefaultPath());
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000BC5ED File Offset: 0x000BA7ED
		private void LoadCallback(string File)
		{
			if (File.IsNullOrEmpty())
			{
				return;
			}
			this.LoadMap(File);
			this.SetDefaultPath(Path.GetDirectoryName(File));
			DialogManager.notify("Map loaded!", null);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000BC618 File Offset: 0x000BA818
		public bool OnCommand(string Command, int nFinger = 0)
		{
			if (Command == "Undo")
			{
				this.Undo();
			}
			if (Command == "Redo")
			{
				this.Redo();
			}
			if (Command == "New")
			{
				this.NewButton();
			}
			if (Command == "Back")
			{
				this.Exit();
			}
			if (Command == "FilterUpdated")
			{
				this.DataFilter = base.rootObject.transform.Find("InputField").Find("Field").GetComponent<TMP_InputField>()
					.text;
				base.rootObject.transform.Find("InputField").Find("Field").GetComponent<TMP_InputField>()
					.Select();
			}
			if (Command == "SaveAs")
			{
				this.SaveAs();
			}
			if (Command == "Save")
			{
				this.Save();
			}
			if (Command == "Load")
			{
				this.Load();
			}
			if (Command.StartsWith("RightTile:"))
			{
				string text = Command.Split(':', StringSplitOptions.None)[1];
				int num = Convert.ToInt32(text.Split(',', StringSplitOptions.None)[0]);
				int num2 = Convert.ToInt32(text.Split(',', StringSplitOptions.None)[1]);
				if (this.IsSingleObjectBrush())
				{
					if (this.Map.Cells[num, num2].Objects.Count > 0)
					{
						this.UndoStack.Push(new MapEditorView.MapEditorAction("Remove", this.Map.Cells[num, num2], this.Map.Cells[num, num2].Objects[0]));
						this.RedoStack.Clear();
						this.Map.Cells[num, num2].Objects.RemoveAt(0);
					}
				}
				else
				{
					Rect rect = new Rect((float)num, (float)num2, (float)(this.Brush.width - 1), (float)(this.Brush.height - 1));
					MapFileRegion region = this.Map.Cells.GetRegion(rect);
					MapFileRegion mapFileRegion = region.Clone();
					foreach (MapFileCellReference mapFileCellReference in new List<MapFileCellReference>(region.AllCells(null)))
					{
						mapFileCellReference.cell.Objects.RemoveAll((MapFileObjectBlueprint t) => true);
					}
					this.UndoStack.Push(new MapEditorView.MapEditorAction("DeleteMulti", null, null, rect, mapFileRegion));
					this.RedoStack.Clear();
				}
				this.RenderMap();
			}
			if (Command == "Fill" && this.IsSingleObjectBrush())
			{
				for (int i = 0; i < 80; i++)
				{
					for (int j = 0; j < 25; j++)
					{
						if (this.Map.Cells[i, j].Objects.Count == 0)
						{
							this.Map.Cells[i, j].Objects.Add(new MapFileObjectBlueprint(this.Brush.GetOrCreateCellAt(0, 0).Objects[0]));
						}
					}
				}
				this.RenderMap();
			}
			if (Command.StartsWith("Tile:"))
			{
				if (nFinger == 0)
				{
					if (Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || Input.GetKey(global::UnityEngine.KeyCode.RightAlt))
					{
						if (this.Map != null)
						{
							string text2 = Command.Split(':', StringSplitOptions.None)[1];
							int num3 = Convert.ToInt32(text2.Split(',', StringSplitOptions.None)[0]);
							int num4 = Convert.ToInt32(text2.Split(',', StringSplitOptions.None)[1]);
							if (this.Map.Cells[num3, num4].Objects.Count > 0)
							{
								this.SetSelectedBlueprint(this.Map.Cells[num3, num4].Objects[0]);
							}
						}
					}
					else if (Input.GetKey(global::UnityEngine.KeyCode.LeftControl) || Input.GetKey(global::UnityEngine.KeyCode.RightControl))
					{
						string text3 = Command.Split(':', StringSplitOptions.None)[1];
						int num5 = Convert.ToInt32(text3.Split(',', StringSplitOptions.None)[0]);
						int num6 = Convert.ToInt32(text3.Split(',', StringSplitOptions.None)[1]);
						if (this.Map != null && this.IsSingleObjectBrush())
						{
							MapFileObjectBlueprint mapFileObjectBlueprint = new MapFileObjectBlueprint(this.Brush.GetOrCreateCellAt(0, 0).Objects[0]);
							this.Map.Cells[num5, num6].Objects.Add(mapFileObjectBlueprint);
							this.UndoStack.Push(new MapEditorView.MapEditorAction("Add", this.Map.Cells[num5, num6], mapFileObjectBlueprint));
							this.RedoStack.Clear();
							this.RenderMap();
						}
						else
						{
							foreach (MapFileObjectReference mapFileObjectReference in this.Brush.AllObjects(null))
							{
								if (mapFileObjectReference.x + num5 <= 79 && mapFileObjectReference.y + num6 <= 24)
								{
									this.Map.Cells[mapFileObjectReference.x + num5, mapFileObjectReference.y + num6].Objects.Add(new MapFileObjectBlueprint(mapFileObjectReference.blueprint));
								}
							}
							this.UndoStack.Push(new MapEditorView.MapEditorAction("AddMulti", null, null, new Rect((float)num5, (float)num6, (float)(this.Brush.width - 1), (float)(this.Brush.height - 1)), this.Brush.Clone()));
							this.RedoStack.Clear();
							this.RenderMap();
						}
					}
					else if (Input.GetKey(global::UnityEngine.KeyCode.LeftShift))
					{
						if (this.Map != null && this.IsSingleObjectBrush())
						{
							string text4 = Command.Split(':', StringSplitOptions.None)[1];
							int num7 = Convert.ToInt32(text4.Split(',', StringSplitOptions.None)[0]);
							int num8 = Convert.ToInt32(text4.Split(',', StringSplitOptions.None)[1]);
							MapFileObjectBlueprint mapFileObjectBlueprint2 = this.Brush.GetOrCreateCellAt(0, 0).Objects[0];
							MapFileRegion mapFileRegion2 = this.Flood(num7, num8, mapFileObjectBlueprint2, new MapFileRegion(80, 25));
							Dictionary<MapFileCellReference, int> dictionary = new Dictionary<MapFileCellReference, int>();
							foreach (MapFileCellReference mapFileCellReference2 in mapFileRegion2.FindCellsWithObjectBlueprint(mapFileObjectBlueprint2))
							{
								dictionary.Add(mapFileCellReference2, 1);
							}
							this.UndoStack.Push(new MapEditorView.MapEditorAction("BulkAdd", null, mapFileObjectBlueprint2, new Rect(0f, 0f, 79f, 24f), dictionary));
						}
					}
					else if (this.Map != null)
					{
						string text5 = Command.Split(':', StringSplitOptions.None)[1];
						int num9 = Convert.ToInt32(text5.Split(',', StringSplitOptions.None)[0]);
						int num10 = Convert.ToInt32(text5.Split(',', StringSplitOptions.None)[1]);
						this.SelectedRegion = new Rect((float)num9, (float)num10, 0f, 0f);
						this.HasSelectedRegion = true;
						this.RenderSelectionFrame();
					}
				}
				if (nFinger == 1)
				{
					string text6 = Command.Split(':', StringSplitOptions.None)[1];
					int num11 = Convert.ToInt32(text6.Split(',', StringSplitOptions.None)[0]);
					int num12 = Convert.ToInt32(text6.Split(',', StringSplitOptions.None)[1]);
					if (this.Map.Cells[num11, num12].Objects.Count > 0)
					{
						this.UndoStack.Push(new MapEditorView.MapEditorAction("Remove", this.Map.Cells[num11, num12], this.Map.Cells[num11, num12].Objects[0]));
						this.RedoStack.Clear();
						this.Map.Cells[num11, num12].Objects.RemoveAt(0);
						this.RenderMap();
					}
				}
			}
			if (Command == "ToggleFilterUsed")
			{
				this.FilterUsed = !this.FilterUsed;
			}
			return true;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000BCE54 File Offset: 0x000BB054
		public void SetFilterUsed(bool value)
		{
			this.FilterUsed = value;
			this.UpdateFilter();
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000BCE63 File Offset: 0x000BB063
		public void SetHoverBlueprint(MapFileObjectBlueprint HoverBlueprint)
		{
			this.HoverBlueprint = HoverBlueprint;
			this._HasRendered = false;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000BCE73 File Offset: 0x000BB073
		public void SetMapID()
		{
			DialogManager.getString("Define map ID", this.Map.ID, delegate(string id)
			{
				this.Map.ID = id;
				this.UpdateTitle();
			}, InputType.String);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000BCE97 File Offset: 0x000BB097
		public void SetPartForContext()
		{
			DialogManager.getString("New part:", this.ContextBlueprint.Part ?? "", delegate(string part)
			{
				if (string.IsNullOrEmpty(part))
				{
					part = null;
				}
				if (!this.HasSelectedRegion)
				{
					this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
				}
				List<MapFileCellReference> list = new List<MapFileCellReference>();
				list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(this.ContextBlueprint));
				foreach (MapFileCellReference mapFileCellReference in list)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
					{
						if (mapFileObjectBlueprint == this.ContextBlueprint)
						{
							mapFileObjectBlueprint.Part = part;
						}
					}
				}
				this.RenderMap();
				this.RenderSelectionFrame();
			}, InputType.String);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000BCEC4 File Offset: 0x000BB0C4
		public void AddPropertyForContext()
		{
			DialogManager.getPair("Define property name and value", "[Name]", "[Value]", delegate(string n, string v)
			{
				if (n.IsNullOrEmpty() || n.StartsWith("["))
				{
					return;
				}
				if (v.IsNullOrEmpty() || v.StartsWith("["))
				{
					return;
				}
				if (!this.HasSelectedRegion)
				{
					this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
				}
				List<MapFileCellReference> list = new List<MapFileCellReference>();
				list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(this.ContextBlueprint));
				foreach (MapFileCellReference mapFileCellReference in list)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
					{
						if (mapFileObjectBlueprint == this.ContextBlueprint)
						{
							MapFileObjectBlueprint mapFileObjectBlueprint2 = mapFileObjectBlueprint;
							if (mapFileObjectBlueprint2.Properties == null)
							{
								mapFileObjectBlueprint2.Properties = new Dictionary<string, string>();
							}
							mapFileObjectBlueprint.Properties[n] = v;
						}
					}
				}
				this.RenderMap();
				this.RenderSelectionFrame();
			});
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000BCEE6 File Offset: 0x000BB0E6
		public void AddIntPropertyForContext()
		{
			DialogManager.getPair("Define property name and value", "[Name]", "1", delegate(string n, string v)
			{
				if (n.IsNullOrEmpty() || n.StartsWith("["))
				{
					return;
				}
				if (v.IsNullOrEmpty())
				{
					return;
				}
				int num;
				if (!int.TryParse(v, out num))
				{
					DialogManager.error("Invalid property value. Value must be an integer.");
					return;
				}
				if (!this.HasSelectedRegion)
				{
					this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
				}
				List<MapFileCellReference> list = new List<MapFileCellReference>();
				list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(this.ContextBlueprint));
				foreach (MapFileCellReference mapFileCellReference in list)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
					{
						if (mapFileObjectBlueprint == this.ContextBlueprint)
						{
							MapFileObjectBlueprint mapFileObjectBlueprint2 = mapFileObjectBlueprint;
							if (mapFileObjectBlueprint2.IntProperties == null)
							{
								mapFileObjectBlueprint2.IntProperties = new Dictionary<string, int>();
							}
							mapFileObjectBlueprint.IntProperties[n] = num;
						}
					}
				}
				this.RenderMap();
				this.RenderSelectionFrame();
			});
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000BCF08 File Offset: 0x000BB108
		public void RemovePropertyForContext()
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.HasSelectedRegion)
			{
				this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
			}
			List<MapFileCellReference> cells = new List<MapFileCellReference>();
			cells.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(this.ContextBlueprint));
			foreach (MapFileCellReference mapFileCellReference in cells)
			{
				foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
				{
					if (!(mapFileObjectBlueprint != this.ContextBlueprint))
					{
						if (!mapFileObjectBlueprint.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
						{
							foreach (KeyValuePair<string, string> keyValuePair in mapFileObjectBlueprint.Properties)
							{
								List<string> list;
								if (!dictionary.TryGetValue(keyValuePair.Key, out list))
								{
									list = (dictionary[keyValuePair.Key] = new List<string>(1));
								}
								stringBuilder.Clear().Append('"').Append(keyValuePair.Value)
									.Append('"');
								if (!list.Any<string>(new Func<string, bool>(stringBuilder.ValueEquals)))
								{
									list.Add(stringBuilder.ToString());
								}
							}
						}
						if (!mapFileObjectBlueprint.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
						{
							foreach (KeyValuePair<string, int> keyValuePair2 in mapFileObjectBlueprint.IntProperties)
							{
								List<string> list2;
								if (!dictionary.TryGetValue(keyValuePair2.Key, out list2))
								{
									list2 = (dictionary[keyValuePair2.Key] = new List<string>(1));
								}
								stringBuilder.Clear().Append(keyValuePair2.Value);
								if (!list2.Any<string>(new Func<string, bool>(stringBuilder.ValueEquals)))
								{
									list2.Add(stringBuilder.ToString());
								}
							}
						}
					}
				}
			}
			if (dictionary.IsNullOrEmpty<KeyValuePair<string, List<string>>>())
			{
				DialogManager.info("No properties defined.");
				return;
			}
			List<string> keys = new List<string>();
			List<string> list3 = new List<string>();
			foreach (KeyValuePair<string, List<string>> keyValuePair3 in dictionary)
			{
				keys.Add(keyValuePair3.Key);
				list3.Add(stringBuilder.Clear().Append(keyValuePair3.Key).Append(" (")
					.AppendJoin<string>(", ", keyValuePair3.Value)
					.Append(')')
					.ToString());
			}
			DialogManager.getChoice("Choose a property to remove", -1, list3, delegate(int i)
			{
				if (i == -1)
				{
					return;
				}
				string text = keys[i];
				foreach (MapFileCellReference mapFileCellReference2 in cells)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint2 in mapFileCellReference2.cell.Objects)
					{
						if (!(mapFileObjectBlueprint2 != this.ContextBlueprint))
						{
							Dictionary<string, string> properties = mapFileObjectBlueprint2.Properties;
							if (properties != null)
							{
								properties.Remove(text);
							}
							Dictionary<string, int> intProperties = mapFileObjectBlueprint2.IntProperties;
							if (intProperties != null)
							{
								intProperties.Remove(text);
							}
						}
					}
				}
				this.RenderMap();
				this.RenderSelectionFrame();
			});
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000BD294 File Offset: 0x000BB494
		public void ToggleOverlay(string resource)
		{
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000BD296 File Offset: 0x000BB496
		public void SetOwnerForContext()
		{
			this.SetOwnerInRegion(this.ContextBlueprint);
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000BD2A4 File Offset: 0x000BB4A4
		public void SetOwnerInRegion(MapFileObjectBlueprint blueprint)
		{
			DialogManager.getString("New owner:", blueprint.Owner ?? "", delegate(string newOwner)
			{
				if (string.IsNullOrEmpty(newOwner))
				{
					newOwner = null;
				}
				if (!this.HasSelectedRegion)
				{
					this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
				}
				List<MapFileCellReference> list = new List<MapFileCellReference>();
				list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(blueprint));
				foreach (MapFileCellReference mapFileCellReference in list)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFileCellReference.cell.Objects)
					{
						if (mapFileObjectBlueprint == blueprint)
						{
							mapFileObjectBlueprint.Owner = newOwner;
						}
					}
				}
				this.RenderMap();
				this.RenderSelectionFrame();
			}, InputType.String);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000BD2F0 File Offset: 0x000BB4F0
		public void DeleteInRegion(MapFileObjectBlueprint blueprint)
		{
			if (!this.HasSelectedRegion)
			{
				this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
			}
			List<MapFileCellReference> list = new List<MapFileCellReference>();
			list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(blueprint));
			bool deleteAll = this.SelectedRegion.width != 0f || this.SelectedRegion.height != 0f;
			int deleteCount = 0;
			Dictionary<MapFileCellReference, int> dictionary = new Dictionary<MapFileCellReference, int>();
			Predicate<MapFileObjectBlueprint> <>9__0;
			foreach (MapFileCellReference mapFileCellReference in list)
			{
				Dictionary<MapFileCellReference, int> dictionary2 = dictionary;
				MapFileCellReference mapFileCellReference2 = mapFileCellReference;
				List<MapFileObjectBlueprint> objects = mapFileCellReference.cell.Objects;
				Predicate<MapFileObjectBlueprint> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = delegate(MapFileObjectBlueprint obj)
					{
						if (!(obj == blueprint))
						{
							return false;
						}
						if (!deleteAll)
						{
							int deleteCount2 = deleteCount;
							deleteCount = deleteCount2 + 1;
							return deleteCount2 == 0;
						}
						return true;
					});
				}
				dictionary2.Add(mapFileCellReference2, objects.RemoveAll(predicate));
			}
			this.RedoStack.Clear();
			this.UndoStack.Push(new MapEditorView.MapEditorAction("BulkDelete", null, blueprint, this.SelectedRegion, dictionary));
			this.RenderMap();
			this.RenderSelectionFrame();
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000BD44C File Offset: 0x000BB64C
		public void DisplayContextInRegion(MapFileObjectBlueprint blueprint)
		{
			this.ContextBlueprint = blueprint;
			this.ContextMenu.show(Input.mousePosition);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000BD46C File Offset: 0x000BB66C
		public void ReplaceInRegion(MapFileObjectBlueprint blueprint)
		{
			if (!this.IsSingleObjectBrush())
			{
				return;
			}
			if (!this.HasSelectedRegion)
			{
				this.SelectedRegion = new Rect(0f, 0f, 79f, 24f);
			}
			List<MapFileCellReference> list = new List<MapFileCellReference>();
			list.AddRange(this.Map.Cells.GetRegion(this.SelectedRegion).FindCellsWithObjectBlueprint(blueprint));
			MapEditorView.ReplaceOperationNotes replaceOperationNotes = default(MapEditorView.ReplaceOperationNotes);
			replaceOperationNotes.Original = blueprint;
			replaceOperationNotes.Replacement = new MapFileObjectBlueprint(this.Brush.GetOrCreateCellAt(0, 0).Objects[0]);
			replaceOperationNotes.replaceCount = new Dictionary<MapFileCellReference, int>();
			Predicate<MapFileObjectBlueprint> <>9__0;
			foreach (MapFileCellReference mapFileCellReference in list)
			{
				List<MapFileObjectBlueprint> objects = mapFileCellReference.cell.Objects;
				Predicate<MapFileObjectBlueprint> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (MapFileObjectBlueprint obj) => obj == blueprint);
				}
				int num = objects.RemoveAll(predicate);
				replaceOperationNotes.replaceCount.Add(mapFileCellReference, num);
				for (int i = 0; i < num; i++)
				{
					mapFileCellReference.cell.Objects.Add(new MapFileObjectBlueprint(replaceOperationNotes.Replacement));
				}
			}
			this.RedoStack.Clear();
			this.UndoStack.Push(new MapEditorView.MapEditorAction("BulkReplace", null, blueprint, this.SelectedRegion, replaceOperationNotes));
			this.RenderSelectionFrame();
			this.RenderMap();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000BD618 File Offset: 0x000BB818
		public MapFileRegion Flood(int x, int y, MapFileObjectBlueprint SelectedBlueprint, MapFileRegion workingRegion)
		{
			if (x < 0)
			{
				return workingRegion;
			}
			if (y < 0)
			{
				return workingRegion;
			}
			if (x >= 80)
			{
				return workingRegion;
			}
			if (y >= 25)
			{
				return workingRegion;
			}
			if (this.Map.Cells[x, y].Objects.Count > 0)
			{
				return workingRegion;
			}
			workingRegion.GetOrCreateCellAt(x, y).Objects.Add(new MapFileObjectBlueprint(SelectedBlueprint));
			this.Map.Cells[x, y].Objects.Add(new MapFileObjectBlueprint(SelectedBlueprint));
			this.Flood(x - 1, y, SelectedBlueprint, workingRegion);
			this.Flood(x + 1, y, SelectedBlueprint, workingRegion);
			this.Flood(x, y - 1, SelectedBlueprint, workingRegion);
			this.Flood(x, y + 1, SelectedBlueprint, workingRegion);
			this._HasRendered = false;
			this._HasSelectionRendered = false;
			return workingRegion;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000BD6E8 File Offset: 0x000BB8E8
		public bool InsideSelection(int x, int y)
		{
			return this.HasSelectedRegion && this.SelectedRegion.xMin <= (float)x && this.SelectedRegion.xMax >= (float)x && this.SelectedRegion.yMin <= (float)y && this.SelectedRegion.yMax >= (float)y;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000BD740 File Offset: 0x000BB940
		public void RenderMap()
		{
			MapFileCellRender renderCell = MapEditorView.RenderCell;
			ConsoleChar[,] buffer = TextConsole.CurrentBuffer.Buffer;
			ex3DSprite2[,] consoleCharacter = GameManager.Instance.ConsoleCharacter;
			string[,] currentTile = GameManager.Instance.CurrentTile;
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					renderCell.Reset();
					currentTile[i, j] = null;
					this.Map.Cells[i, j].Render(renderCell, new MapFileCellReference(this.Map.Cells, i, j, null));
					if (this.InsideSelection(i, j) && this.Map.Cells[i, j].Objects.Contains(this.HoverBlueprint))
					{
						renderCell.Detail = new Color(0.25f, 0.25f, 0f, 1f);
						renderCell.Background = new Color(1f, 1f, 0f, 1f);
						renderCell.Foreground = new Color(0.5f, 0.5f, 0f, 1f);
					}
					ex3DSprite2 ex3DSprite = consoleCharacter[i, j];
					ConsoleChar consoleChar = buffer[i, j];
					if (renderCell.Char == '\0')
					{
						consoleChar.Tile = renderCell.Tile;
						consoleChar.Char = renderCell.Char;
						ex3DSprite.textureInfo = SpriteManager.GetTextureInfo(renderCell.Tile, true);
						consoleChar.Foreground = (ex3DSprite.color = renderCell.Foreground);
						consoleChar.Detail = (ex3DSprite.detailcolor = renderCell.Detail);
						consoleChar.Background = (ex3DSprite.backcolor = renderCell.Background);
					}
					else
					{
						consoleChar.Tile = renderCell.Tile;
						consoleChar.Char = renderCell.Char;
						ex3DSprite.textureInfo = GameManager.Instance.CharInfos[(int)renderCell.Char];
						consoleChar.Foreground = (ex3DSprite.backcolor = renderCell.Foreground);
						consoleChar.Detail = (ex3DSprite.detailcolor = renderCell.Foreground);
						consoleChar.Background = (ex3DSprite.color = renderCell.Background);
					}
				}
			}
		}

		// Token: 0x0400127E RID: 4734
		public static MapEditorView instance = null;

		// Token: 0x0400127F RID: 4735
		private MapFile Map;

		// Token: 0x04001280 RID: 4736
		public string FileName;

		// Token: 0x04001281 RID: 4737
		public Rect SelectedRegion = Rect.zero;

		// Token: 0x04001282 RID: 4738
		public bool HasSelectedRegion;

		// Token: 0x04001283 RID: 4739
		public MapFileRegion Brush = new MapFileRegion(1, 1);

		// Token: 0x04001284 RID: 4740
		private bool _FilterUsed;

		// Token: 0x04001285 RID: 4741
		public string _DataFilter = "";

		// Token: 0x04001286 RID: 4742
		public Stack<MapEditorView.MapEditorAction> UndoStack = new Stack<MapEditorView.MapEditorAction>();

		// Token: 0x04001287 RID: 4743
		public Stack<MapEditorView.MapEditorAction> RedoStack = new Stack<MapEditorView.MapEditorAction>();

		// Token: 0x04001288 RID: 4744
		private float t;

		// Token: 0x04001289 RID: 4745
		private bool _HasRendered;

		// Token: 0x0400128A RID: 4746
		private bool _HasSelectionRendered;

		// Token: 0x0400128B RID: 4747
		private Vector2 _dragStart = Vector2.zero;

		// Token: 0x0400128C RID: 4748
		private Vector2 _dragDelta = Vector2.zero;

		// Token: 0x0400128D RID: 4749
		private bool isDragging;

		// Token: 0x0400128E RID: 4750
		private Vector3 _cameraStart;

		// Token: 0x0400128F RID: 4751
		public MenuBar MenuBar;

		// Token: 0x04001290 RID: 4752
		public Menu ContextMenu;

		// Token: 0x04001291 RID: 4753
		public Menu RecentMenu;

		// Token: 0x04001292 RID: 4754
		public MapEditorSettings Settings;

		// Token: 0x04001293 RID: 4755
		private List<Action> RecentDelegates = new List<Action>();

		// Token: 0x04001294 RID: 4756
		public bool setup;

		// Token: 0x04001295 RID: 4757
		public static MapEditorView Instance;

		// Token: 0x04001296 RID: 4758
		public global::UnityEngine.GameObject SelectionFrame;

		// Token: 0x04001297 RID: 4759
		public global::UnityEngine.GameObject GhostHighlight;

		// Token: 0x04001298 RID: 4760
		private MapFileObjectBlueprint HoverBlueprint;

		// Token: 0x04001299 RID: 4761
		private MapFileObjectBlueprint ContextBlueprint;

		// Token: 0x0400129A RID: 4762
		private static MapFileCellRender RenderCell = new MapFileCellRender();

		// Token: 0x02001799 RID: 6041
		public struct MapEditorAction
		{
			// Token: 0x0600DE19 RID: 56857 RVA: 0x0050E125 File Offset: 0x0050C325
			public MapEditorAction(string Action, MapFileCell Cell, MapFileObjectBlueprint Object)
			{
				this.Action = Action;
				this.Cell = Cell;
				this.Brush = Object;
				this.Region = Rect.zero;
				this.AdditionalData = null;
			}

			// Token: 0x0600DE1A RID: 56858 RVA: 0x0050E14E File Offset: 0x0050C34E
			public MapEditorAction(string Action, MapFileCell Cell, MapFileObjectBlueprint Object, Rect Region, object AdditionalData = null)
			{
				this.Action = Action;
				this.Cell = Cell;
				this.Brush = Object;
				this.Region = Region;
				this.AdditionalData = AdditionalData;
			}

			// Token: 0x04005F12 RID: 24338
			public string Action;

			// Token: 0x04005F13 RID: 24339
			public MapFileCell Cell;

			// Token: 0x04005F14 RID: 24340
			public MapFileObjectBlueprint Brush;

			// Token: 0x04005F15 RID: 24341
			public Rect Region;

			// Token: 0x04005F16 RID: 24342
			public object AdditionalData;
		}

		// Token: 0x0200179A RID: 6042
		private struct ReplaceOperationNotes
		{
			// Token: 0x04005F17 RID: 24343
			public MapFileObjectBlueprint Original;

			// Token: 0x04005F18 RID: 24344
			public MapFileObjectBlueprint Replacement;

			// Token: 0x04005F19 RID: 24345
			public Dictionary<MapFileCellReference, int> replaceCount;
		}

		// Token: 0x0200179B RID: 6043
		public class UpdateComponent : MonoBehaviour
		{
			// Token: 0x0600DE1B RID: 56859 RVA: 0x0050E175 File Offset: 0x0050C375
			public void Update()
			{
				MapEditorView instance = MapEditorView.instance;
				if (instance == null)
				{
					return;
				}
				instance.Update();
			}
		}

		// Token: 0x0200179C RID: 6044
		private class CanTravelHandler : IEventHandler
		{
			// Token: 0x0600DE1D RID: 56861 RVA: 0x0050E18E File Offset: 0x0050C38E
			public bool HandleEvent(CanTravelEvent E)
			{
				return false;
			}
		}

		// Token: 0x0200179D RID: 6045
		private class GetZoneHandler : IEventHandler
		{
			// Token: 0x0600DE1F RID: 56863 RVA: 0x0050E199 File Offset: 0x0050C399
			public bool HandleEvent(GetZoneEvent E)
			{
				if (E.ZoneID.IndexOf('.') != -1)
				{
					E.Result = The.ActiveZone;
					return false;
				}
				return true;
			}
		}
	}
}
