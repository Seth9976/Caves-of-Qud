using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000132 RID: 306
public class gameplay : MonoBehaviour
{
	// Token: 0x06000B87 RID: 2951 RVA: 0x0004CD10 File Offset: 0x0004AF10
	private gameplay()
	{
		gameplay.Instance = this;
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0004CDBD File Offset: 0x0004AFBD
	private void OnEnable()
	{
		this.EnableMusic(PlayerPrefs.GetInt("Music_Enabled", 1) == 1);
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0004CDD3 File Offset: 0x0004AFD3
	public Sprite GetTileTypeSprite(int tileType)
	{
		return this.m_GemTextures[tileType];
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0004CDE0 File Offset: 0x0004AFE0
	public void InitBoard(int countX, int countY, int tiletypeCount, int moveCount, List<int> levelGoals)
	{
		UAP_AccessibilityManager.BlockInput(true, true);
		this.m_SelectionFrame.SetActive(false);
		this.m_CellCountX = countX;
		this.m_CellCountY = countY;
		this.m_MoveCount = 0;
		this.m_GameDuration = 0f;
		this.m_TileTypeCount = tiletypeCount;
		for (int i = 0; i < this.m_GoalsLabel.Length; i++)
		{
			this.m_GoalsLabel[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < this.m_GoalsImages.Length; j++)
		{
			this.m_GoalsImages[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < this.m_GoalsImages.Length; k++)
		{
			this.m_GoalsCheckmarks[k].SetActive(false);
		}
		this.m_LevelGoals.Clear();
		this.m_LevelGoals = levelGoals;
		this.m_Cleared.Clear();
		for (int l = 0; l < gridpanel.tileTypeCount; l++)
		{
			this.m_Cleared.Add(0);
		}
		while (base.transform.childCount > 0)
		{
			Transform child = base.transform.GetChild(0);
			child.SetParent(null);
			global::UnityEngine.Object.DestroyImmediate(child.gameObject);
		}
		this.m_GridTiles.Clear();
		Vector2 vector = new Vector2(0f, 0f);
		vector.x = (float)this.m_BaseCellSize / (float)countX * (float)this.m_BaseCellCountX;
		vector.y = (float)this.m_BaseCellSize / (float)countY * (float)this.m_BaseCellCountY;
		if (this.m_MakeSquares)
		{
			if (vector.x < vector.y)
			{
				vector.y = vector.x;
			}
			else
			{
				vector.x = vector.y;
			}
		}
		this.grid.cellSize = vector;
		this.grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		this.grid.constraintCount = countX;
		GameObject gameObject = Resources.Load("Panel") as GameObject;
		int num = countX * countY;
		for (int m = 0; m < num; m++)
		{
			GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(base.transform, false);
			gameObject2.GetComponent<gridpanel>().SetIndex(m, countX);
			this.m_GridTiles.Add(gameObject2.GetComponent<gridpanel>());
		}
		this.m_SelectedTile = null;
		this.m_MovesLeft = moveCount;
		this.UpdateMoveLabel();
		this.UpdateLevelGoalsLabels();
		this.RandomizeTiles();
		this.SaveGameState();
		int num2 = 0;
		for (int n = 0; n < gridpanel.tileTypeCount; n++)
		{
			if (this.m_LevelGoals[n] > 0)
			{
				this.m_GoalsImages[num2].gameObject.SetActive(true);
				this.m_GoalsImages[num2].sprite = this.GetTileTypeSprite(n);
				num2++;
			}
		}
		string text = "Level Goals: ";
		int num3 = 0;
		for (int num4 = 0; num4 < gridpanel.tileTypeCount; num4++)
		{
			if (this.m_LevelGoals[num4] >= 0)
			{
				if (this.m_Cleared[num4] > this.m_LevelGoals[num4])
				{
					int num5 = this.m_LevelGoals[num4];
				}
				num3++;
				if (this.m_LevelGoalsString.Length > 0)
				{
					text += "\n ";
				}
				if (num3 == num2 && num2 > 1)
				{
					text += "And ";
				}
				text = string.Concat(new string[]
				{
					text,
					this.m_LevelGoals[num4].ToString("0"),
					" ",
					gridpanel.GetTileTypeName(num4),
					" gems, "
				});
			}
		}
		text += "need to be destroyed.";
		UAP_AccessibilityManager.Say("Game Started. Double Tap with two fingers to pause the game.", false, true, UAP_AudioQueue.EInterrupt.Elements);
		UAP_AccessibilityManager.Say(text + " \nYou have " + this.m_MovesLeft.ToString("0") + " moves remaining.", true, true, UAP_AudioQueue.EInterrupt.Elements);
		UAP_AccessibilityManager.Say("Tap once with two fingers to repeat the level goals.", true, true, UAP_AudioQueue.EInterrupt.Elements);
		UAP_AccessibilityManager.BlockInput(false, true);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0004D1CC File Offset: 0x0004B3CC
	private void Start()
	{
		UAP_AccessibilityManager.RegisterOnPauseToggledCallback(new UAP_AccessibilityManager.OnPauseToggleCallbackFunc(this.OnUserPause));
		UAP_AccessibilityManager.RegisterOnTwoFingerSingleTapCallback(new UAP_AccessibilityManager.OnTapEvent(this.OnRepeatLevelGoals));
		int num = 5;
		int num2 = 10;
		int num3 = 1;
		int num4 = 15;
		if (gameplay.DifficultyLevel == 1)
		{
			num = 6;
			num2 = 15;
			num3 = 2;
			num4 = 20;
		}
		else if (gameplay.DifficultyLevel == 2)
		{
			num = 7;
			num2 = 20;
			num3 = 3;
			num4 = 30;
		}
		List<int> list = new List<int>();
		bool flag = false;
		list.Clear();
		for (int i = 0; i < gridpanel.tileTypeCount; i++)
		{
			list.Add(-1);
		}
		while (!flag)
		{
			int num5 = 0;
			for (int j = 0; j < num; j++)
			{
				if (list[j] < 0 && global::UnityEngine.Random.Range(0, 5) < 3)
				{
					int num6 = global::UnityEngine.Random.Range(5, num4);
					list[j] = num6;
					num5++;
					if (num5 == num3)
					{
						break;
					}
				}
			}
			flag = num5 == num3;
		}
		this.InitBoard(6, 6, num, num2, list);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0004D2B9 File Offset: 0x0004B4B9
	public void OnRepeatLevelGoals()
	{
		UAP_AccessibilityManager.Say(this.m_LevelGoalsString, true, true, UAP_AudioQueue.EInterrupt.Elements);
		UAP_AccessibilityManager.Say(this.m_MovesLabel_Access.m_Text, true, true, UAP_AudioQueue.EInterrupt.Elements);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
	public void OnUserPause()
	{
		if (!this.m_Paused)
		{
			this.m_Paused = true;
			this.m_MusicPlayer.Pause();
			base.transform.parent.parent.gameObject.SetActive(false);
			global::UnityEngine.Object.Instantiate(Resources.Load("Pause Menu"));
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0004D334 File Offset: 0x0004B534
	private void Update()
	{
		this.m_GameDuration += Time.unscaledDeltaTime;
		if (this.m_IsPreviewingSwap)
		{
			this.m_SwapPreviewTimer -= Time.unscaledDeltaTime;
			float num = 1f - this.m_SwapPreviewTimer / this.m_SwapPreviewDuration;
			gridpanel gridTile = this.GetGridTile(this.m_PreviewIndex1);
			gridpanel gridTile2 = this.GetGridTile(this.m_PreviewIndex2);
			gridTile.m_GemImage.transform.position = Vector3.Lerp(this.m_Previewposition1, this.m_Previewposition2, num);
			gridTile2.m_GemImage.transform.position = Vector3.Lerp(this.m_Previewposition2, this.m_Previewposition1, num);
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0004D3E0 File Offset: 0x0004B5E0
	private int GetRandomTile()
	{
		return global::UnityEngine.Random.Range(0, this.m_TileTypeCount);
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0004D3F0 File Offset: 0x0004B5F0
	private void RandomizeTiles()
	{
		for (int i = 0; i < this.m_GridTiles.Count; i++)
		{
			this.m_GridTiles[i].SetTileType(this.GetRandomTile());
		}
		List<int> list;
		while ((list = this.FindMatch3()) != null)
		{
			foreach (int num in list)
			{
				this.m_GridTiles[num].SetTileType(this.GetRandomTile());
			}
		}
		this.SaveGameState();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0004D490 File Offset: 0x0004B690
	private Vector2 ConvertIndexToXYCoordinates(int index)
	{
		Vector2 vector = default(Vector2);
		vector.y = (float)Mathf.FloorToInt((float)index / (float)this.m_CellCountX);
		vector.x = (float)Mathf.FloorToInt((float)index - vector.y * (float)this.m_CellCountX);
		return vector;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0004D4DC File Offset: 0x0004B6DC
	private void AbortSelection()
	{
		this.m_SFXPlayer.Stop();
		this.m_SFXPlayer.PlayOneShot(this.m_SwapAborted);
		this.CancelPreview(false);
		UAP_AccessibilityManager.Say("Swap canceled.", false, false, UAP_AudioQueue.EInterrupt.All);
		this.m_SelectionFrame.SetActive(false);
		this.m_SelectedTile = null;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0004D530 File Offset: 0x0004B730
	public void ActivateTile(int index)
	{
		UAP_AccessibilityManager.Say("", false, false, UAP_AudioQueue.EInterrupt.All);
		Vector2 vector = this.ConvertIndexToXYCoordinates(index);
		gridpanel gridTile = this.GetGridTile(index);
		if (gridTile == null || this.m_SelectedTile == gridTile)
		{
			this.AbortSelection();
			return;
		}
		if (this.m_SelectedTile == null)
		{
			this.m_SFXPlayer.clip = this.m_ActiveTile;
			this.m_SFXPlayer.loop = true;
			this.m_SFXPlayer.Play();
			this.m_SelectedTile = gridTile;
			this.m_SelectionFrame.SetActive(true);
			this.m_SelectionFrame.transform.position = this.m_SelectedTile.transform.position;
			return;
		}
		Vector2 vector2 = this.ConvertIndexToXYCoordinates(this.m_SelectedTile.GetIndex()) - vector;
		if (Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y) > 1f)
		{
			this.AbortSelection();
			return;
		}
		UAP_AccessibilityManager.BlockInput(true, true);
		this.CancelPreview(true);
		int tileType = this.m_SelectedTile.GetTileType();
		this.m_SelectedTile.SetTileType(gridTile.GetTileType());
		gridTile.SetTileType(tileType);
		this.m_SelectedTile = null;
		this.m_SelectionFrame.SetActive(false);
		this.m_SFXPlayer.Stop();
		this.m_MovesGained = 0;
		this.m_MoveCount++;
		this.m_levelGoalUpdatedWithMove = false;
		this.EvaluateBoard();
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0004D692 File Offset: 0x0004B892
	private gridpanel GetGridTile(int index)
	{
		if (index < 0 || index >= this.m_GridTiles.Count)
		{
			return null;
		}
		return this.m_GridTiles[index];
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0004D6B4 File Offset: 0x0004B8B4
	private void UpdateMoveLabel()
	{
		this.m_MovesLabel.text = "Moves: " + this.m_MovesLeft.ToString("0");
		this.m_MovesLabel_Access.m_Text = this.m_MovesLeft.ToString("0") + " moves left.";
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0004D70C File Offset: 0x0004B90C
	private void UpdateLevelGoalsLabels()
	{
		int num = 0;
		this.m_LevelGoalsString = "Level Goals: \n";
		for (int i = 0; i < gridpanel.tileTypeCount; i++)
		{
			if (this.m_LevelGoals[i] >= 0)
			{
				this.m_GoalsLabel[num].gameObject.SetActive(true);
				this.m_GoalsImages[num].gameObject.SetActive(true);
				int num2 = this.m_Cleared[i];
				if (num2 > this.m_LevelGoals[i])
				{
					num2 = this.m_LevelGoals[i];
				}
				this.m_GoalsLabel[num].text = num2.ToString("0") + "/" + this.m_LevelGoals[i].ToString("0");
				this.m_GoalsLabel[num].GetComponent<UAP_BaseElement>().m_Text = string.Concat(new string[]
				{
					gridpanel.GetTileTypeName(i),
					": ",
					num2.ToString("0"),
					" of ",
					this.m_LevelGoals[i].ToString("0")
				});
				if (num2 == this.m_LevelGoals[i])
				{
					this.m_GoalsCheckmarks[num].SetActive(true);
				}
				if (this.m_LevelGoalsString.Length > 0)
				{
					this.m_LevelGoalsString += "\n";
				}
				this.m_LevelGoalsString = string.Concat(new string[]
				{
					this.m_LevelGoalsString,
					gridpanel.GetTileTypeName(i),
					": ",
					num2.ToString("0"),
					" of ",
					this.m_LevelGoals[i].ToString("0"),
					". \n"
				});
				num++;
			}
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0004D8E8 File Offset: 0x0004BAE8
	private int GetLevelGoalIndex(int tileType)
	{
		int num = 0;
		for (int i = 0; i < gridpanel.tileTypeCount; i++)
		{
			if (this.m_LevelGoals[i] >= 0)
			{
				if (i == tileType)
				{
					return num;
				}
				num++;
			}
		}
		return -1;
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0004D924 File Offset: 0x0004BB24
	private void EvaluateBoard()
	{
		List<int> list = this.FindMatch3();
		if (list != null)
		{
			int tileType = this.m_GridTiles[list[0]].GetTileType();
			bool flag = false;
			if (this.m_LevelGoals[tileType] > 0 && this.m_LevelGoals[tileType] > this.m_Cleared[tileType])
			{
				this.m_levelGoalUpdatedWithMove = true;
				flag = true;
				this.m_SFXPlayer.PlayOneShot(this.m_GoalsMatch3);
				int levelGoalIndex = this.GetLevelGoalIndex(tileType);
				if (levelGoalIndex >= 0)
				{
					GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.m_LevelGoalHighlightEffect);
					gameObject.transform.SetParent(this.m_GoalsHighlightPos[levelGoalIndex].transform, false);
					global::UnityEngine.Object.Destroy(gameObject, 1f);
				}
				else
				{
					Debug.LogWarning("Couldn't find level goal index");
				}
				this.UpdateLevelGoalsLabels();
			}
			else
			{
				this.m_SFXPlayer.PlayOneShot(this.m_Match3);
			}
			List<int> cleared = this.m_Cleared;
			int num = tileType;
			cleared[num] += list.Count;
			UAP_AccessibilityManager.Say(string.Concat(new string[]
			{
				"Matched ",
				list.Count.ToString(),
				" ",
				this.m_GridTiles[list[0]].GetTileTypeName(),
				" gems."
			}), true, true, UAP_AudioQueue.EInterrupt.Elements);
			if (flag)
			{
				int num2 = this.m_LevelGoals[tileType] - this.m_Cleared[tileType];
				if (num2 > 0)
				{
					UAP_AccessibilityManager.Say("Need " + num2.ToString("0") + " more.", true, true, UAP_AudioQueue.EInterrupt.Elements);
				}
				else
				{
					UAP_AccessibilityManager.Say(this.m_GridTiles[list[0]].GetTileTypeName() + " level goal completed.", true, true, UAP_AudioQueue.EInterrupt.Elements);
				}
			}
			if (list.Count > 3)
			{
				this.m_MovesGained += list.Count - 1;
			}
			GameObject gameObject2 = Resources.Load("Burst") as GameObject;
			foreach (int num3 in list)
			{
				this.m_GridTiles[num3].SetTileType(-1);
				GameObject gameObject3 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject2);
				gameObject3.transform.SetParent(base.transform.parent, true);
				gameObject3.transform.position = this.m_GridTiles[num3].transform.position;
				global::UnityEngine.Object.Destroy(gameObject3, 0.45f);
			}
			float num4 = 0.25f;
			base.Invoke("EvaluateBoard", num4);
			return;
		}
		base.Invoke("DropDownTiles", 0.05f);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0004DBDC File Offset: 0x0004BDDC
	private void DropDownTiles()
	{
		bool flag = false;
		for (int i = 0; i < this.m_CellCountX; i++)
		{
			for (int j = this.m_CellCountY - 1; j >= 0; j--)
			{
				int index = this.GetIndex(i, j);
				if (this.m_GridTiles[index].GetTileType() == -1)
				{
					flag = true;
					if (j == 0)
					{
						this.m_GridTiles[index].SetTileType(this.GetRandomTile());
					}
					else
					{
						for (int k = j - 1; k >= 0; k--)
						{
							int index2 = this.GetIndex(i, k);
							int tileType = this.m_GridTiles[index2].GetTileType();
							if (tileType < 0 && k == 0)
							{
								this.m_GridTiles[index].SetTileType(this.GetRandomTile());
								break;
							}
							if (tileType >= 0)
							{
								this.m_GridTiles[index].SetTileType(tileType);
								this.m_GridTiles[index2].SetTileType(-1);
								break;
							}
						}
					}
				}
			}
		}
		if (flag)
		{
			this.m_SFXPlayer.PlayOneShot(this.m_FallingPieces);
			float num = 0.25f;
			base.Invoke("EvaluateBoard", num);
			return;
		}
		this.FinishBoardEvaluation();
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0004DD0B File Offset: 0x0004BF0B
	private int GetIndex(int x, int y)
	{
		return x + y * this.m_CellCountX;
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x0004DD18 File Offset: 0x0004BF18
	private void FinishBoardEvaluation()
	{
		if (!this.m_levelGoalUpdatedWithMove)
		{
			this.m_MovesLeft--;
		}
		this.m_levelGoalUpdatedWithMove = false;
		this.m_MovesLeft += this.m_MovesGained;
		if (this.m_MovesGained > 0)
		{
			UAP_AccessibilityManager.Say("Gained " + this.m_MovesGained.ToString("0") + " move" + ((this.m_MovesGained > 1) ? "s." : "."), true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
		UAP_AccessibilityManager.Say(this.m_MovesLeft.ToString("0") + " moves left.", true, true, UAP_AudioQueue.EInterrupt.Elements);
		this.m_MovesGained = 0;
		this.UpdateMoveLabel();
		this.UpdateLevelGoalsLabels();
		if (this.m_MovesLeft <= 0)
		{
			gameover.GameWon = false;
			gameover.GameDuration = this.m_GameDuration;
			gameover.MoveCount = this.m_MoveCount;
			this.DestroyMyself();
			global::UnityEngine.Object.Instantiate(Resources.Load("Game Over Screen"));
			return;
		}
		bool flag = true;
		for (int i = 0; i < gridpanel.tileTypeCount; i++)
		{
			if (this.m_LevelGoals[i] >= 0 && this.m_Cleared[i] < this.m_LevelGoals[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			gameover.GameWon = true;
			gameover.GameDuration = this.m_GameDuration;
			gameover.MoveCount = this.m_MoveCount;
			this.DestroyMyself();
			global::UnityEngine.Object.Instantiate(Resources.Load("Game Over Screen"));
			return;
		}
		this.SaveGameState();
		UAP_AccessibilityManager.BlockInput(false, true);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0004DE8F File Offset: 0x0004C08F
	private void SaveGameState()
	{
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0004DE94 File Offset: 0x0004C094
	private List<int> FindMatch3()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_GridTiles.Count; i++)
		{
			Vector2 vector = this.ConvertIndexToXYCoordinates(i);
			int tileType = this.m_GridTiles[i].GetTileType();
			if (tileType >= 0)
			{
				list.Clear();
				list.Add(i);
				int num = 1;
				for (int j = (int)vector.x + 1; j < this.m_CellCountX; j++)
				{
					int num2 = j + (int)vector.y * this.m_CellCountX;
					if (this.m_GridTiles[num2].GetTileType() != tileType)
					{
						break;
					}
					list.Add(num2);
					num++;
				}
				if (num >= 3)
				{
					int count = list.Count;
					for (int k = 0; k < count; k++)
					{
						int num3 = list[k];
						Vector2 vector2 = this.ConvertIndexToXYCoordinates(num3);
						if (this.GetTileType(vector2.x, vector2.y + 1f) == tileType && this.GetTileType(vector2.x, vector2.y + 2f) == tileType)
						{
							list.Add(num3 + this.m_CellCountX);
							list.Add(num3 + this.m_CellCountX + this.m_CellCountX);
						}
					}
					string text = "";
					foreach (int num4 in list)
					{
						text = text + this.ConvertIndexToXYCoordinates(num4).ToString() + " ";
					}
					return list;
				}
				list.Clear();
				list.Add(i);
				num = 1;
				for (int l = (int)vector.y + 1; l < this.m_CellCountY; l++)
				{
					int num5 = (int)vector.x + l * this.m_CellCountX;
					if (this.m_GridTiles[num5].GetTileType() != tileType)
					{
						break;
					}
					list.Add(num5);
					num++;
				}
				if (num >= 3)
				{
					int count2 = list.Count;
					for (int m = 0; m < count2; m++)
					{
						int num6 = list[m];
						bool flag = false;
						bool flag2 = false;
						Vector2 vector3 = this.ConvertIndexToXYCoordinates(num6);
						if (this.GetTileType(vector3.x - 2f, vector3.y) == tileType && this.GetTileType(vector3.x - 1f, vector3.y) == tileType)
						{
							list.Add(num6 - 2);
							list.Add(num6 - 1);
						}
						if (this.GetTileType(vector3.x + 2f, vector3.y) == tileType && this.GetTileType(vector3.x + 1f, vector3.y) == tileType)
						{
							list.Add(num6 + 2);
							list.Add(num6 + 1);
						}
						if ((!flag || !flag2) && this.GetTileType(vector3.x - 1f, vector3.y) == tileType && this.GetTileType(vector3.x + 1f, vector3.y) == tileType)
						{
							if (!flag)
							{
								list.Add(num6 - 1);
							}
							if (!flag2)
							{
								list.Add(num6 + 1);
							}
						}
					}
					string text2 = "";
					foreach (int num7 in list)
					{
						text2 = text2 + this.ConvertIndexToXYCoordinates(num7).ToString() + " ";
					}
					return list;
				}
			}
		}
		return null;
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0004E24C File Offset: 0x0004C44C
	private int GetTileType(float xCoord, float yCoord)
	{
		if (xCoord < 0f || xCoord >= (float)this.m_CellCountX)
		{
			return -1;
		}
		if (yCoord < 0f || yCoord >= (float)this.m_CellCountY)
		{
			return -1;
		}
		int num = (int)xCoord + (int)yCoord * this.m_CellCountX;
		return this.m_GridTiles[num].GetTileType();
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0004E29F File Offset: 0x0004C49F
	public void AbortGame()
	{
		this.DestroyMyself();
		global::UnityEngine.Object.Instantiate(Resources.Load("Main Menu"));
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0004E2B7 File Offset: 0x0004C4B7
	private void DestroyMyself()
	{
		global::UnityEngine.Object.DestroyImmediate(base.transform.parent.parent.gameObject);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0004E2D3 File Offset: 0x0004C4D3
	public void ResumeGame()
	{
		this.m_Paused = false;
		this.m_MusicPlayer.UnPause();
		base.transform.parent.parent.gameObject.SetActive(true);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0004E302 File Offset: 0x0004C502
	private void OnDestroy()
	{
		UAP_AccessibilityManager.UnregisterOnPauseToggledCallback(new UAP_AccessibilityManager.OnPauseToggleCallbackFunc(this.OnUserPause));
		UAP_AccessibilityManager.UnregisterOnTwoFingerSingleTapCallback(new UAP_AccessibilityManager.OnTapEvent(this.OnRepeatLevelGoals));
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0004E326 File Offset: 0x0004C526
	public void OnSoundToggle()
	{
		this.EnableMusic(!this.m_MusicPlayer.enabled);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0004E33C File Offset: 0x0004C53C
	private void EnableMusic(bool enable)
	{
		this.m_MusicPlayer.enabled = enable;
		this.m_SoundToggle.sprite = (enable ? this.m_SoundOn : this.m_SoundOff);
		PlayerPrefs.SetInt("Music_Enabled", enable ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0004E37C File Offset: 0x0004C57C
	public void PreviewDrag(int index1, int index2)
	{
		this.CancelPreview(false);
		gridpanel gridTile = this.GetGridTile(index1);
		gridpanel gridTile2 = this.GetGridTile(index2);
		if (gridTile == null || gridTile2 == null)
		{
			return;
		}
		this.m_IsPreviewingSwap = true;
		this.m_SwapPreviewTimer = this.m_SwapPreviewDuration;
		this.m_PreviewIndex1 = index1;
		this.m_PreviewIndex2 = index2;
		this.m_Previewposition1 = gridTile.transform.position;
		this.m_Previewposition2 = gridTile2.transform.position;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0004E3F8 File Offset: 0x0004C5F8
	public void CancelPreview(bool swapSuccessful = false)
	{
		if (!this.m_IsPreviewingSwap)
		{
			return;
		}
		this.m_IsPreviewingSwap = false;
		gridpanel gridTile = this.GetGridTile(this.m_PreviewIndex1);
		gridpanel gridTile2 = this.GetGridTile(this.m_PreviewIndex2);
		gridTile.m_GemImage.transform.position = this.m_Previewposition1;
		gridTile2.m_GemImage.transform.position = this.m_Previewposition2;
	}

	// Token: 0x04000817 RID: 2071
	public GridLayoutGroup grid;

	// Token: 0x04000818 RID: 2072
	public AccessibleUIGroupRoot container;

	// Token: 0x04000819 RID: 2073
	public Text m_MovesLabel;

	// Token: 0x0400081A RID: 2074
	public UAP_BaseElement m_MovesLabel_Access;

	// Token: 0x0400081B RID: 2075
	public Text[] m_GoalsLabel;

	// Token: 0x0400081C RID: 2076
	public Image[] m_GoalsImages;

	// Token: 0x0400081D RID: 2077
	public GameObject[] m_GoalsCheckmarks;

	// Token: 0x0400081E RID: 2078
	public GameObject[] m_GoalsHighlightPos;

	// Token: 0x0400081F RID: 2079
	public GameObject m_SelectionFrame;

	// Token: 0x04000820 RID: 2080
	public AudioSource m_SFXPlayer;

	// Token: 0x04000821 RID: 2081
	public Image m_SoundToggle;

	// Token: 0x04000822 RID: 2082
	public Sprite m_SoundOn;

	// Token: 0x04000823 RID: 2083
	public Sprite m_SoundOff;

	// Token: 0x04000824 RID: 2084
	public AudioSource m_MusicPlayer;

	// Token: 0x04000825 RID: 2085
	public AudioClip m_ActiveTile;

	// Token: 0x04000826 RID: 2086
	public AudioClip m_SwapAborted;

	// Token: 0x04000827 RID: 2087
	public AudioClip m_NoMatch3;

	// Token: 0x04000828 RID: 2088
	public AudioClip m_Match3;

	// Token: 0x04000829 RID: 2089
	public AudioClip m_GoalsMatch3;

	// Token: 0x0400082A RID: 2090
	public AudioClip m_FallingPieces;

	// Token: 0x0400082B RID: 2091
	public GameObject m_LevelGoalHighlightEffect;

	// Token: 0x0400082C RID: 2092
	public Sprite[] m_GemTextures = new Sprite[gridpanel.tileTypeCount];

	// Token: 0x0400082D RID: 2093
	private int m_CellCountX = -1;

	// Token: 0x0400082E RID: 2094
	private int m_CellCountY = -1;

	// Token: 0x0400082F RID: 2095
	private List<gridpanel> m_GridTiles = new List<gridpanel>();

	// Token: 0x04000830 RID: 2096
	public bool m_MakeSquares = true;

	// Token: 0x04000831 RID: 2097
	private int m_BaseCellSize = 85;

	// Token: 0x04000832 RID: 2098
	private int m_BaseCellCountX = 7;

	// Token: 0x04000833 RID: 2099
	private int m_BaseCellCountY = 11;

	// Token: 0x04000834 RID: 2100
	public static gameplay Instance;

	// Token: 0x04000835 RID: 2101
	private int m_MovesLeft = 15;

	// Token: 0x04000836 RID: 2102
	private int m_MoveCount;

	// Token: 0x04000837 RID: 2103
	private float m_GameDuration;

	// Token: 0x04000838 RID: 2104
	private List<int> m_Cleared = new List<int>();

	// Token: 0x04000839 RID: 2105
	private List<int> m_LevelGoals = new List<int>();

	// Token: 0x0400083A RID: 2106
	private int m_TileTypeCount;

	// Token: 0x0400083B RID: 2107
	private bool m_Paused;

	// Token: 0x0400083C RID: 2108
	private string m_LevelGoalsString = "";

	// Token: 0x0400083D RID: 2109
	private int m_MovesGained;

	// Token: 0x0400083E RID: 2110
	private gridpanel m_SelectedTile;

	// Token: 0x0400083F RID: 2111
	private bool m_levelGoalUpdatedWithMove;

	// Token: 0x04000840 RID: 2112
	public static int DifficultyLevel;

	// Token: 0x04000841 RID: 2113
	private bool m_IsPreviewingSwap;

	// Token: 0x04000842 RID: 2114
	private float m_SwapPreviewTimer = -1f;

	// Token: 0x04000843 RID: 2115
	private float m_SwapPreviewDuration = 0.1f;

	// Token: 0x04000844 RID: 2116
	private int m_PreviewIndex1 = -1;

	// Token: 0x04000845 RID: 2117
	private int m_PreviewIndex2 = -1;

	// Token: 0x04000846 RID: 2118
	private Vector3 m_Previewposition1;

	// Token: 0x04000847 RID: 2119
	private Vector3 m_Previewposition2;
}
