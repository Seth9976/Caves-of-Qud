using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x02000514 RID: 1300
	[HasWishCommand]
	[Serializable]
	public abstract class SifrahGame
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x0010F5D0 File Offset: 0x0010D7D0
		public bool Solved
		{
			get
			{
				if (this.Slots != null)
				{
					int i = 0;
					int count = this.Slots.Count;
					while (i < count)
					{
						if (!this.Slots[i].Solved)
						{
							return false;
						}
						i++;
					}
				}
				return true;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x0010F614 File Offset: 0x0010D814
		public int SlotsSolved
		{
			get
			{
				int num = 0;
				if (this.Slots != null)
				{
					int i = 0;
					int count = this.Slots.Count;
					while (i < count)
					{
						if (this.Slots[i].Solved)
						{
							num++;
						}
						i++;
					}
				}
				return num;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x0010F65B File Offset: 0x0010D85B
		public int PercentSolved
		{
			get
			{
				if (this.Slots == null || this.Slots.Count == 0)
				{
					return 0;
				}
				return this.SlotsSolved * 100 / this.Slots.Count;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x0010F689 File Offset: 0x0010D889
		public int TotalChoicesNeededForTurn
		{
			get
			{
				if (this.Slots == null || this.Slots.Count == 0)
				{
					return 0;
				}
				return this.Slots.Count - this.SlotsSolved;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x0010F6B4 File Offset: 0x0010D8B4
		public int ChoicesMadeForTurn
		{
			get
			{
				int num = 0;
				if (this.Slots != null || this.Slots.Count == 0)
				{
					int i = 0;
					int count = this.Slots.Count;
					while (i < count)
					{
						if (this.Slots[i].CurrentMove != -1 && !this.Slots[i].Solved)
						{
							num++;
						}
						i++;
					}
				}
				return num;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x0010F71C File Offset: 0x0010D91C
		public bool TurnReady
		{
			get
			{
				return this.ChoicesMadeForTurn >= this.TotalChoicesNeededForTurn;
			}
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x0010F730 File Offset: 0x0010D930
		public virtual string[] GetStatus()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			SifrahGame.status[0] = "Chances:";
			SifrahGame.status[1] = " {{g|Success             " + string.Format("{0,3}%", num);
			SifrahGame.status[2] = " {{G|Exceptional success " + string.Format("{0,3}%", num4);
			SifrahGame.status[3] = " {{W|Partial success     " + string.Format("{0,3}%", num3);
			SifrahGame.status[4] = " {{K|Failure             " + string.Format("{0,3}%", num2);
			SifrahGame.status[5] = " {{R|Critical failure    " + string.Format("{0,3}%", num5);
			return SifrahGame.status;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x0010F804 File Offset: 0x0010DA04
		public int GetTimesChosen(SifrahToken Token, SifrahSlot Except = null)
		{
			int num = 0;
			foreach (SifrahSlot sifrahSlot in this.Slots)
			{
				if (sifrahSlot.CurrentMove != -1 && this.Tokens[sifrahSlot.CurrentMove] == Token && sifrahSlot != Except)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x0010F878 File Offset: 0x0010DA78
		public void Render(GameObject ContextObject)
		{
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			scrapBuffer.Clear();
			scrapBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			int num = 48 / this.Slots.Count;
			int num2 = (num - 1) / 2;
			int i = 0;
			int count = this.Slots.Count;
			int num3;
			int num4;
			while (i < count)
			{
				num3 = 1 + i * num + num2;
				num4 = 4;
				SifrahSlot sifrahSlot = this.Slots[i];
				scrapBuffer.Goto(num3, num4);
				sifrahSlot.WriteTo(scrapBuffer);
				sifrahSlot.X = num3;
				sifrahSlot.Y = num4;
				num4 += 2;
				int num5 = Math.Max(0, sifrahSlot.Moves.Count - 12);
				int j = num5;
				int count2 = sifrahSlot.Moves.Count;
				while (j < count2)
				{
					scrapBuffer.Goto(num3, num4 + j - num5);
					int num6 = sifrahSlot.Moves[j];
					SifrahToken sifrahToken = this.Tokens[num6];
					if (num6 == sifrahSlot.Token)
					{
						sifrahToken.WriteTo(scrapBuffer, "&G", 'g');
					}
					else if (sifrahToken.EliminatedAt == -1 || sifrahToken.EliminatedAt > j)
					{
						sifrahToken.WriteTo(scrapBuffer, "&W", 'w');
					}
					else
					{
						sifrahToken.WriteTo(scrapBuffer, "&R", 'r');
					}
					j++;
				}
				num4 = 22;
				scrapBuffer.Goto(num3, num4);
				if (!sifrahSlot.Solved || this.SlotSelected == i)
				{
					if (sifrahSlot.CurrentMove == -1)
					{
						if (this.SlotSelected == i)
						{
							scrapBuffer.Write("&k^Y?", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("&Y?", true, false, false, null, -1);
						}
					}
					else
					{
						SifrahToken sifrahToken2 = this.Tokens[sifrahSlot.CurrentMove];
						if (this.SlotSelected == i)
						{
							sifrahToken2.WriteHighlightedTo(scrapBuffer);
						}
						else
						{
							sifrahToken2.WriteTo(scrapBuffer);
						}
						sifrahToken2.X = num3;
						sifrahToken2.Y = num4;
					}
				}
				GameManager.Instance.AddRegion(num3, 4, num3, 22, "Slot:" + i.ToString(), null, null);
				i++;
			}
			num3 = 49;
			num4 = 2;
			IRenderable renderable = ((this.ShowContextObjectIcon && ContextObject != null) ? ContextObject.RenderForUI(null, false) : null);
			if (!this.Description.IsNullOrEmpty())
			{
				int num7 = 30;
				if (renderable != null)
				{
					num7 -= 3;
				}
				foreach (string text in new TextBlock(this.Description, num7, 6, false, 0).Lines)
				{
					scrapBuffer.Goto(num3, num4++);
					scrapBuffer.Write(text, true, false, false, null, -1);
				}
				num4++;
			}
			if (renderable != null)
			{
				scrapBuffer.Goto(76, 2);
				scrapBuffer.Write("   ", true, false, false, null, -1);
				scrapBuffer.Goto(77, 2);
				scrapBuffer.Write(renderable);
			}
			scrapBuffer.Goto(num3, num4++);
			scrapBuffer.Write("Turn: " + this.Turn.ToString() + "/" + this.MaxTurns.ToString(), true, false, false, null, -1);
			scrapBuffer.Goto(num3, num4++);
			scrapBuffer.Write("Solved: " + this.SlotsSolved.ToString() + "/" + this.Slots.Count.ToString(), true, false, false, null, -1);
			if (this.Finished)
			{
				scrapBuffer.WriteAt(num3, num4++, this.Solved ? "{{G|Completed!}}" : "{{R|Incomplete.}}", true);
				scrapBuffer.WriteAt(24, 24, " press {{W|Space}} or {{W|Enter}} to continue ", true);
			}
			else
			{
				scrapBuffer.WriteAt(num3, num4++, "Choices made: " + this.ChoicesMadeForTurn.ToString() + "/" + this.TotalChoicesNeededForTurn.ToString(), true);
				if (this.TurnReady)
				{
					if (Options.PrereleaseInputManager)
					{
						scrapBuffer.WriteAt(27, 24, " press {{W|Z}} to complete turn ", true);
					}
					else
					{
						scrapBuffer.WriteAt(25, 24, " press {{W|Space}} to complete turn ", true);
					}
				}
				else
				{
					scrapBuffer.WriteAt(2, 24, " [{{W|\u001b\u001a}}-change slot] ", true);
					scrapBuffer.WriteAt(19, 24, " [{{W|Enter}}-choose option] ", true);
					scrapBuffer.WriteAt(46, 24, " [{{W|Space}}-complete turn] ", true);
					scrapBuffer.WriteAt(68, 24, " [{{W|?}}-help] ", true);
				}
			}
			string[] array = this.GetStatus();
			if (array != null && array.Length != 0)
			{
				num4++;
				foreach (string text2 in array)
				{
					scrapBuffer.WriteAt(num3, num4++, text2, true);
				}
			}
			if (this.GetInsight())
			{
				num4++;
				scrapBuffer.WriteAt(num3, num4++, "{{W|I}} to use insight", true);
			}
			scrapBuffer.Draw();
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x0010FD70 File Offset: 0x0010DF70
		public void ProcessTurn(GameObject ContextObject)
		{
			int i = 0;
			int count = this.Slots.Count;
			while (i < count)
			{
				SifrahSlot sifrahSlot = this.Slots[i];
				if (!sifrahSlot.Solved)
				{
					int currentMove = sifrahSlot.CurrentMove;
					if (currentMove != -1)
					{
						sifrahSlot.Moves.Add(currentMove);
						this.Powerup += this.Tokens[currentMove].GetPowerup(this, sifrahSlot, ContextObject);
						this.Tokens[currentMove].UseToken(this, sifrahSlot, ContextObject);
					}
				}
				i++;
			}
			int j = 0;
			int count2 = this.Slots.Count;
			while (j < count2)
			{
				SifrahSlot sifrahSlot2 = this.Slots[j];
				if (!sifrahSlot2.Solved)
				{
					int currentMove2 = sifrahSlot2.CurrentMove;
					if (currentMove2 != -1 && currentMove2 != sifrahSlot2.Token)
					{
						SifrahToken sifrahToken = this.Tokens[currentMove2];
						if (sifrahToken.EliminatedAt == -1)
						{
							bool flag = false;
							int k = 0;
							int count3 = this.Slots.Count;
							while (k < count3)
							{
								if (k != j && !this.Slots[k].Solved && this.Slots[k].Token == currentMove2)
								{
									flag = true;
									break;
								}
								k++;
							}
							if (!flag)
							{
								sifrahToken.EliminatedAt = this.Turn - 1;
							}
						}
					}
				}
				j++;
			}
			int num = 0;
			int l = 0;
			int count4 = this.Slots.Count;
			while (l < count4)
			{
				SifrahSlot slot = this.Slots[l];
				int dy = Math.Max(0, slot.Moves.Count - 12) + slot.Moves.Count + 1;
				if (slot.Solved)
				{
					if (slot.SolvedOnTurn == this.Turn && !this.CorrectTokenSound.IsNullOrEmpty() && Options.Sound)
					{
						if (num > 0)
						{
							Task.Delay(num).ContinueWith(delegate(Task t)
							{
								SoundManager.PlaySound(this.CorrectTokenSound, 0f, this.CorrectTokenSoundVolume, 1f, SoundRequest.SoundEffectType.None, 0f);
								CombatJuice.punch(slot.GetLocation(0, dy), slot.GetLocation(0, dy - 1), 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f);
							});
						}
						else
						{
							SoundManager.PlaySound(this.CorrectTokenSound, 0f, this.CorrectTokenSoundVolume, 1f, SoundRequest.SoundEffectType.None, 0f);
							CombatJuice.punch(slot.GetLocation(0, dy), slot.GetLocation(0, dy - 1), 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f);
						}
						num += this.CorrectTokenSoundDelay;
					}
				}
				else
				{
					if (slot.CurrentMove != -1 && !this.IncorrectTokenSound.IsNullOrEmpty() && Options.Sound)
					{
						if (num > 0)
						{
							Task.Delay(num).ContinueWith(delegate(Task t)
							{
								SoundManager.PlaySound(this.IncorrectTokenSound, 0f, this.IncorrectTokenSoundVolume, 1f, SoundRequest.SoundEffectType.None, 0f);
								CombatJuice.punch(slot.GetLocation(0, dy), slot.GetLocation(0, dy + 1), 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f);
							});
						}
						else
						{
							SoundManager.PlaySound(this.IncorrectTokenSound, 0f, this.IncorrectTokenSoundVolume, 1f, SoundRequest.SoundEffectType.None, 0f);
							CombatJuice.punch(slot.GetLocation(0, dy), slot.GetLocation(0, dy + 1), 0.2f, Easing.Functions.SineEaseInOut, 0f, 0f, 0f, 0f);
						}
						num += this.IncorrectTokenSoundDelay;
					}
					slot.CurrentMove = -1;
				}
				l++;
			}
			int m = 0;
			int count5 = this.Tokens.Count;
			while (m < count5)
			{
				SifrahToken sifrahToken2 = this.Tokens[m];
				sifrahToken2.UsabilityCheckedThisTurn = false;
				sifrahToken2.DisabledThisTurn = false;
				m++;
			}
			if (this.Solved || this.Turn >= this.MaxTurns)
			{
				this.Finished = true;
				this.SlotSelected = -1;
				return;
			}
			this.Turn++;
			this.SlotSelected = this.MoveNeeded();
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x00110178 File Offset: 0x0010E378
		public int MoveNeeded()
		{
			int i = 0;
			int count = this.Slots.Count;
			while (i < count)
			{
				SifrahSlot sifrahSlot = this.Slots[i];
				if (!sifrahSlot.Solved && sifrahSlot.CurrentMove == -1)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x001101C0 File Offset: 0x0010E3C0
		private void MoveRight(bool CheckCurrentMove = false)
		{
			int i = 0;
			int count = this.Slots.Count;
			while (i < count)
			{
				this.SlotSelected++;
				if (this.SlotSelected >= this.Slots.Count)
				{
					this.SlotSelected = 0;
				}
				if (!this.Slots[this.SlotSelected].Solved && (!CheckCurrentMove || this.Slots[this.SlotSelected].CurrentMove == -1))
				{
					return;
				}
				i++;
			}
			if (CheckCurrentMove)
			{
				this.MoveRight(false);
			}
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0011024C File Offset: 0x0010E44C
		private void MoveLeft(bool CheckCurrentMove = false)
		{
			int i = 0;
			int count = this.Slots.Count;
			while (i < count)
			{
				this.SlotSelected--;
				if (this.SlotSelected < 0)
				{
					this.SlotSelected = this.Slots.Count - 1;
				}
				if (!this.Slots[this.SlotSelected].Solved && (!CheckCurrentMove || this.Slots[this.SlotSelected].CurrentMove == -1))
				{
					return;
				}
				i++;
			}
			if (CheckCurrentMove)
			{
				this.MoveLeft(false);
			}
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x001102DC File Offset: 0x0010E4DC
		public bool AnyUsableTokens(GameObject ContextObject)
		{
			int i = 0;
			int count = this.Tokens.Count;
			while (i < count)
			{
				if (!this.Tokens[i].Eliminated && !this.Tokens[i].GetDisabled(this, null, ContextObject))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x00110330 File Offset: 0x0010E530
		public bool MakeMoveForSlot(int SlotNumber, GameObject ContextObject)
		{
			SifrahSlot sifrahSlot = this.Slots[SlotNumber];
			if (sifrahSlot.Solved)
			{
				Popup.ShowFail("You have already chosen the correct option for " + sifrahSlot.Description + ".", true, true, true);
				return false;
			}
			int num3;
			for (;;)
			{
				string[] array = new string[this.Tokens.Count];
				char[] array2 = new char[this.Tokens.Count];
				IRenderable[] array3 = new IRenderable[this.Tokens.Count];
				List<int> list = null;
				char c = 'a';
				int num = sifrahSlot.CurrentMove;
				int i = 0;
				int count = this.Tokens.Count;
				while (i < count)
				{
					SifrahToken sifrahToken = this.Tokens[i];
					string text = sifrahToken.GetDescription(this, sifrahSlot, ContextObject);
					bool flag = false;
					if (sifrahToken.Eliminated)
					{
						text += " [eliminated]";
						flag = true;
					}
					else if (sifrahToken.GetDisabled(this, sifrahSlot, ContextObject))
					{
						flag = true;
						if (sifrahToken.DisabledThisTurn)
						{
							text += " [disabled this turn]";
						}
					}
					else if (num == -1)
					{
						num = i;
					}
					if (flag)
					{
						text = "{{K|" + ColorUtility.StripFormatting(text) + "}}";
						array3[i] = this.Tokens[i].GetAlternate("&K", 'K');
					}
					else
					{
						array3[i] = this.Tokens[i];
					}
					array[i] = text;
					if (c <= 'z')
					{
						char[] array4 = array2;
						int num2 = i;
						char c2 = c;
						c = c2 + '\u0001';
						array4[num2] = c2;
					}
					else
					{
						array2[i] = ' ';
					}
					if (sifrahToken.Eliminated)
					{
						if (list == null)
						{
							list = new List<int>();
						}
						list.Add(i);
					}
					i++;
				}
				num3 = Popup.PickOption("Use which option for " + sifrahSlot.Description + "?", null, "", "Sounds/UI/ui_notification", array, array2, array3, null, null, null, null, 0, 60, num, -1, true, false, false, true, false, null, null);
				if (num3 < 0)
				{
					return true;
				}
				SifrahToken sifrahToken2 = this.Tokens[num3];
				if (list != null && list.Contains(num3))
				{
					Popup.ShowFail("You have already eliminated " + sifrahToken2.Description + " as a possibility.", true, true, true);
				}
				else if (sifrahToken2.CheckTokenUse(this, sifrahSlot, ContextObject))
				{
					if (!sifrahToken2.DisabledThisTurn)
					{
						break;
					}
					Popup.ShowFail("Choosing " + sifrahToken2.Description + " is disabled for this turn.", true, true, true);
				}
			}
			sifrahSlot.CurrentMove = num3;
			this.SlotSelected = SlotNumber;
			this.MoveRight(true);
			return true;
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x001105A1 File Offset: 0x0010E7A1
		public static void ShowHelp()
		{
			BookUI.ShowBookByID("Sifrah", "Sounds/Interact/sfx_interact_book_read", null);
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x001105B3 File Offset: 0x0010E7B3
		public virtual bool CheckIncompleteTurn(GameObject ContextObject)
		{
			return Popup.ShowYesNo("You haven't selected an option for every slot. Are you sure you want to complete the turn?", "Sounds/UI/ui_notification", true, DialogResult.No, null) == DialogResult.Yes;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x001105CA File Offset: 0x0010E7CA
		public virtual bool CheckEarlyExit(GameObject ContextObject)
		{
			return Popup.ShowYesNo("You aren't finished! Are you sure you want to abort the process?", "Sounds/UI/ui_notification", true, DialogResult.No, null) == DialogResult.Yes;
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x001105E1 File Offset: 0x0010E7E1
		public virtual bool CheckOutOfOptions(GameObject ContextObject)
		{
			return true;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x001105E4 File Offset: 0x0010E7E4
		public virtual void Finish(GameObject ContextObject)
		{
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x001105E8 File Offset: 0x0010E7E8
		public void Play(GameObject ContextObject = null)
		{
			if (this.Finished)
			{
				this.Finish(ContextObject);
				return;
			}
			this.HadNoPause = CombatJuiceManager.NoPause;
			if (!this.HadNoPause)
			{
				CombatJuiceManager.NoPause = true;
			}
			Loading.SetHideLoadStatus(true);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer2(false);
			scrapBuffer.Copy(TextConsole.CurrentBuffer);
			GameManager.Instance.PushGameView("Sifrah", true);
			this.Turn = 1;
			this.SlotSelected = 0;
			int num = -1;
			bool flag = false;
			Keyboard.ClearInput(false, false);
			while (!flag)
			{
				GameManager.Instance.ClearRegions();
				this.Render(ContextObject);
				Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (keys == Keys.Right || keys == Keys.NumPad6)
				{
					this.MoveRight(false);
				}
				else if (keys == Keys.Left || keys == Keys.NumPad4)
				{
					this.MoveLeft(false);
				}
				else if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:SifrahEnter" && this.Slots != null && this.SlotSelected >= 0 && this.SlotSelected < this.Slots.Count)
				{
					num = this.SlotSelected;
				}
				else if ((keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:SifrahSpace") || (keys == Keys.Z && Options.PrereleaseInputManager))
				{
					if (this.TurnReady || !this.AnyUsableTokens(ContextObject) || this.CheckIncompleteTurn(ContextObject))
					{
						this.ProcessTurn(ContextObject);
						if (this.Finished)
						{
							flag = true;
							this.Render(ContextObject);
							do
							{
								keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
								if (keys == Keys.Space || keys == Keys.Enter)
								{
									break;
								}
							}
							while (keys != Keys.Escape);
						}
					}
				}
				else if (keys == Keys.Escape)
				{
					if (this.Finished || this.CheckEarlyExit(ContextObject))
					{
						flag = true;
					}
				}
				else if (keys >= Keys.D0 && keys <= Keys.D9 && this.Slots != null)
				{
					int num2 = ((keys == Keys.D0) ? 9 : (keys - Keys.D1));
					if (num2 >= 0 && num2 < this.Slots.Count)
					{
						num = num2;
					}
				}
				else if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Slot:"))
				{
					int num3 = Convert.ToInt32(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
					if (num3 >= 0 && num3 < this.Slots.Count)
					{
						num = num3;
					}
				}
				else if (keys == Keys.F1 || keys == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
				{
					SifrahGame.ShowHelp();
				}
				else if (keys == Keys.I && this.GetInsight())
				{
					this.UseInsight(ContextObject);
				}
				if (num != -1)
				{
					if (this.AnyUsableTokens(ContextObject))
					{
						this.MakeMoveForSlot(num, ContextObject);
					}
					else if (this.CheckOutOfOptions(ContextObject))
					{
						flag = true;
						this.Finished = true;
						this.Render(ContextObject);
					}
					num = -1;
					Keyboard.ClearInput(false, false);
				}
			}
			this.Finish(ContextObject);
			Keyboard.ClearInput(false, false);
			GameManager.Instance.ClearRegions();
			GameManager.Instance.PopGameView(false);
			TextConsole.CurrentBuffer.Copy(scrapBuffer);
			Loading.SetHideLoadStatus(false);
			this.ProcessEffectChances();
			if (!this.HadNoPause)
			{
				CombatJuiceManager.NoPause = false;
			}
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x001108E8 File Offset: 0x0010EAE8
		public static int PrioritySort(SifrahPrioritizable a, SifrahPrioritizable b)
		{
			int num = a.GetPriority().CompareTo(b.GetPriority());
			if (num != 0)
			{
				return -num;
			}
			int num2 = a.GetTiebreakerPriority().CompareTo(b.GetTiebreakerPriority());
			if (num2 != 0)
			{
				return -num2;
			}
			return 0;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x0011092C File Offset: 0x0010EB2C
		protected void AssignPossibleTokens(List<SifrahPrioritizableToken> possibleTokens, List<SifrahToken> tokens, int need, int numTokens)
		{
			if (need >= possibleTokens.Count)
			{
				int i = 0;
				int count = possibleTokens.Count;
				while (i < count)
				{
					tokens.Add(possibleTokens[i]);
					i++;
				}
				return;
			}
			List<SifrahPrioritizableToken> list = new List<SifrahPrioritizableToken>(possibleTokens);
			list.Sort(new Comparison<SifrahPrioritizableToken>(SifrahGame.PrioritySort));
			list.RemoveRange(need, list.Count - need);
			int num = 0;
			int count2 = possibleTokens.Count;
			while (num < count2 && tokens.Count < numTokens)
			{
				if (list.Contains(possibleTokens[num]))
				{
					tokens.Add(possibleTokens[num]);
				}
				num++;
			}
		}

		// Token: 0x060037B8 RID: 14264
		public abstract string GetSifrahCategory();

		// Token: 0x060037B9 RID: 14265 RVA: 0x001109C8 File Offset: 0x0010EBC8
		public void UseInsight(GameObject ContextObject)
		{
			List<SifrahToken> list = null;
			bool flag = false;
			int i = 0;
			int count = this.Tokens.Count;
			while (i < count)
			{
				SifrahToken sifrahToken = this.Tokens[i];
				if (sifrahToken.Eliminated)
				{
					flag = true;
				}
				else
				{
					bool flag2 = false;
					int j = 0;
					int count2 = this.Slots.Count;
					while (j < count2)
					{
						if (this.Slots[j].Token == i)
						{
							flag2 = true;
							break;
						}
						j++;
					}
					if (!flag2)
					{
						if (list == null)
						{
							list = new List<SifrahToken>();
						}
						list.Add(sifrahToken);
						sifrahToken.EliminatedAt = this.Turn;
					}
				}
				i++;
			}
			if (list != null && list.Count > 0)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append("You determine these options to not be valid responses to any requirement:\n");
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					stringBuilder.Append('\n').Append(list[k].GetDescription(this, null, ContextObject));
					k++;
				}
				Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, false, null);
				this.SetInsight(false);
				return;
			}
			if (flag)
			{
				Popup.ShowFail("All options which are not correct responses to any requirement have already been eliminated.", true, true, true);
				return;
			}
			Popup.ShowFail("All options are correct responses to some requirement.", true, true, true);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00110B00 File Offset: 0x0010ED00
		public static void AwardInsight(string Category, string Description)
		{
			if (!SifrahGame.GetInsight(Category))
			{
				GameObject player = The.Player;
				if (((player != null) ? player.CurrentCell : null) != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("You have gained insight into ").Append(Description).Append(". In a future Sifrah task of this kind, you can use this insight to determine which of your ")
						.Append("game options are not correct for any requirement. This will expend your insight, unless there ")
						.Append("are no such options.");
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				SifrahGame.SetInsight(Category, true);
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x00110B7A File Offset: 0x0010ED7A
		public void AwardInsight(string Description)
		{
			SifrahGame.AwardInsight(this.GetSifrahCategory(), Description);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x00110B88 File Offset: 0x0010ED88
		public static string GetInsightKey(string Category)
		{
			return "Sifrah_Insight_" + Category;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x00110B95 File Offset: 0x0010ED95
		public static bool GetInsight(string Category)
		{
			return The.Game.GetBooleanGameState(SifrahGame.GetInsightKey(Category), false);
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x00110BA8 File Offset: 0x0010EDA8
		public bool GetInsight()
		{
			return SifrahGame.GetInsight(this.GetSifrahCategory());
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x00110BB5 File Offset: 0x0010EDB5
		public static void SetInsight(string Category, bool Value)
		{
			The.Game.SetBooleanGameState(SifrahGame.GetInsightKey(Category), Value);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x00110BC8 File Offset: 0x0010EDC8
		public void SetInsight(bool Value)
		{
			SifrahGame.SetInsight(this.GetSifrahCategory(), Value);
		}

		// Token: 0x060037C1 RID: 14273
		public abstract void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure);

		// Token: 0x060037C2 RID: 14274 RVA: 0x00110BD6 File Offset: 0x0010EDD6
		public SifrahGame.GameOutcome DetermineOutcome(int Roll, int Success, int Failure, int PartialSuccess, int CriticalSuccess, int CriticalFailure)
		{
			if (Roll <= CriticalFailure)
			{
				return SifrahGame.GameOutcome.CriticalFailure;
			}
			if (Roll <= CriticalFailure + Failure)
			{
				return SifrahGame.GameOutcome.Failure;
			}
			if (Roll <= CriticalFailure + Failure + PartialSuccess)
			{
				return SifrahGame.GameOutcome.PartialSuccess;
			}
			if (Roll <= CriticalFailure + Failure + PartialSuccess + Success)
			{
				return SifrahGame.GameOutcome.Success;
			}
			return SifrahGame.GameOutcome.CriticalSuccess;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x00110C03 File Offset: 0x0010EE03
		public SifrahGame.GameOutcome DetermineOutcome(int Success, int Failure, int PartialSuccess, int CriticalSuccess, int CriticalFailure)
		{
			return this.DetermineOutcome(Stat.Random(1, 100), Success, Failure, PartialSuccess, CriticalSuccess, CriticalFailure);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x00110C1C File Offset: 0x0010EE1C
		public SifrahGame.GameOutcome DetermineOutcome()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			return this.DetermineOutcome(num, num2, num3, num4, num5);
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x00110C48 File Offset: 0x0010EE48
		public SifrahGame.GameOutcome DetermineOutcome(int Roll)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			return this.DetermineOutcome(Roll, num, num2, num3, num4, num5);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x00110C74 File Offset: 0x0010EE74
		public void PlayOutcomeSound(SifrahGame.GameOutcome Outcome)
		{
			if (Options.Sound)
			{
				string text = null;
				float num = 1f;
				switch (Outcome)
				{
				case SifrahGame.GameOutcome.CriticalFailure:
					text = this.CriticalFailureSound;
					num = this.CriticalFailureSoundVolume;
					break;
				case SifrahGame.GameOutcome.Failure:
					text = this.FailureSound;
					num = this.FailureSoundVolume;
					break;
				case SifrahGame.GameOutcome.PartialSuccess:
					text = this.PartialSuccessSound;
					num = this.PartialSuccessSoundVolume;
					break;
				case SifrahGame.GameOutcome.Success:
					text = this.SuccessSound;
					num = this.SuccessSoundVolume;
					break;
				case SifrahGame.GameOutcome.CriticalSuccess:
					text = this.CriticalSuccessSound;
					num = this.CriticalSuccessSoundVolume;
					break;
				}
				if (!text.IsNullOrEmpty())
				{
					SoundManager.PlaySound(text, 0f, num, 1f, SoundRequest.SoundEffectType.None, 0f);
				}
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x00110D1C File Offset: 0x0010EF1C
		protected static void RecordOutcome(string GameType, SifrahGame.GameOutcome Outcome, int Slots, int Tokens, bool? AsMaster = null, bool? Mastered = null)
		{
			SifrahGame.IncrementGameCount(GameType, null, null, null, null, null);
			SifrahGame.GameOutcome? gameOutcome = new SifrahGame.GameOutcome?(Outcome);
			int? num = null;
			int? num2 = null;
			SifrahGame.IncrementGameCount(GameType, gameOutcome, num, num2, null, null);
			num2 = new int?(Slots);
			int? num3 = new int?(Tokens);
			SifrahGame.IncrementGameCount(GameType, null, num2, num3, null, null);
			SifrahGame.IncrementGameCount(GameType, new SifrahGame.GameOutcome?(Outcome), new int?(Slots), new int?(Tokens), null, null);
			if (AsMaster != null || Mastered != null)
			{
				bool? flag = AsMaster;
				bool? flag2 = Mastered;
				SifrahGame.GameOutcome? gameOutcome2 = null;
				num3 = null;
				int? num4 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome2, num4, num3, flag, flag2);
				flag2 = null;
				flag = Mastered;
				SifrahGame.GameOutcome? gameOutcome3 = null;
				num3 = null;
				int? num5 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome3, num5, num3, flag2, flag);
				flag = AsMaster;
				flag2 = null;
				SifrahGame.GameOutcome? gameOutcome4 = null;
				num3 = null;
				int? num6 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome4, num6, num3, flag, flag2);
				SifrahGame.GameOutcome? gameOutcome5 = new SifrahGame.GameOutcome?(Outcome);
				flag2 = AsMaster;
				flag = Mastered;
				num3 = null;
				int? num7 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome5, num7, num3, flag2, flag);
				SifrahGame.GameOutcome? gameOutcome6 = new SifrahGame.GameOutcome?(Outcome);
				flag = null;
				flag2 = Mastered;
				num3 = null;
				int? num8 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome6, num8, num3, flag, flag2);
				SifrahGame.GameOutcome? gameOutcome7 = new SifrahGame.GameOutcome?(Outcome);
				flag2 = AsMaster;
				flag = null;
				num3 = null;
				int? num9 = num3;
				num3 = null;
				SifrahGame.IncrementGameCount(GameType, gameOutcome7, num9, num3, flag2, flag);
				num3 = new int?(Slots);
				num2 = new int?(Tokens);
				flag = AsMaster;
				flag2 = Mastered;
				SifrahGame.IncrementGameCount(GameType, null, num3, num2, flag, flag2);
				num2 = new int?(Slots);
				num3 = new int?(Tokens);
				flag2 = null;
				flag = Mastered;
				SifrahGame.IncrementGameCount(GameType, null, num2, num3, flag2, flag);
				num3 = new int?(Slots);
				num2 = new int?(Tokens);
				flag = AsMaster;
				flag2 = null;
				SifrahGame.IncrementGameCount(GameType, null, num3, num2, flag, flag2);
				SifrahGame.IncrementGameCount(GameType, new SifrahGame.GameOutcome?(Outcome), new int?(Slots), new int?(Tokens), AsMaster, Mastered);
				SifrahGame.IncrementGameCount(GameType, new SifrahGame.GameOutcome?(Outcome), new int?(Slots), new int?(Tokens), null, Mastered);
				SifrahGame.IncrementGameCount(GameType, new SifrahGame.GameOutcome?(Outcome), new int?(Slots), new int?(Tokens), AsMaster, null);
			}
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x00110FF6 File Offset: 0x0010F1F6
		protected static void RecordOutcome(SifrahGame Game, SifrahGame.GameOutcome Outcome, int Slots, int Tokens, bool AsMaster = false, bool Mastered = false)
		{
			SifrahGame.RecordOutcome(SifrahGame.GetGameType(Game), Outcome, Slots, Tokens, new bool?(AsMaster), new bool?(Mastered));
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x00111014 File Offset: 0x0010F214
		public static string GetGameType(Type Type)
		{
			return Type.Name;
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x0011101C File Offset: 0x0010F21C
		public static string GetGameType(SifrahGame Game)
		{
			return SifrahGame.GetGameType(Game.GetType());
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x00111029 File Offset: 0x0010F229
		public string GetGameType()
		{
			return SifrahGame.GetGameType(this);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x00111034 File Offset: 0x0010F234
		private static string GetGameCountKey(string GameType, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			SifrahGame.KeySB.Clear().Append("Sifrah_").Append(GameType);
			if (Outcome != null)
			{
				SifrahGame.KeySB.Append('_').Append(Outcome.ToString());
			}
			else
			{
				SifrahGame.KeySB.Append("_AnyOutcome");
			}
			if (Mastered != null)
			{
				if (Mastered.GetValueOrDefault())
				{
					SifrahGame.KeySB.Append("_Mastered");
				}
				else
				{
					SifrahGame.KeySB.Append("_Unmastered");
				}
			}
			else
			{
				SifrahGame.KeySB.Append("_BothMasteredAndUnmastered");
			}
			if (AsMaster != null)
			{
				if (AsMaster.GetValueOrDefault())
				{
					SifrahGame.KeySB.Append("_AsMaster");
				}
				else
				{
					SifrahGame.KeySB.Append("_NotAsMaster");
				}
			}
			else
			{
				SifrahGame.KeySB.Append("_BothAsMasterAndNot");
			}
			if (Slots != null && Tokens != null)
			{
				SifrahGame.KeySB.Append("_Type_").Append(Slots).Append('_')
					.Append(Tokens);
			}
			else
			{
				SifrahGame.KeySB.Append("_AnyType");
			}
			return SifrahGame.KeySB.ToString();
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x0011117D File Offset: 0x0010F37D
		private static string GetGameCountKey(Type Type, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return SifrahGame.GetGameCountKey(SifrahGame.GetGameType(Type), Outcome, Slots, Tokens, AsMaster, Mastered);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x00111191 File Offset: 0x0010F391
		private static string GetGameCountKey(SifrahGame Game, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return SifrahGame.GetGameCountKey(SifrahGame.GetGameType(Game), Outcome, Slots, Tokens, AsMaster, Mastered);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x001111A5 File Offset: 0x0010F3A5
		public static int GetGameCount(string GameType, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return The.Game.GetIntGameState(SifrahGame.GetGameCountKey(GameType, Outcome, Slots, Tokens, AsMaster, Mastered), 0);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x001111BF File Offset: 0x0010F3BF
		public static int GetGameCount(Type Type, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return SifrahGame.GetGameCount(SifrahGame.GetGameType(Type), Outcome, Slots, Tokens, AsMaster, Mastered);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x001111D3 File Offset: 0x0010F3D3
		public static int GetGameCount(SifrahGame Game, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return SifrahGame.GetGameCount(SifrahGame.GetGameType(Game), Outcome, Slots, Tokens, AsMaster, Mastered);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x001111E7 File Offset: 0x0010F3E7
		public int GetGameCount(SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			return SifrahGame.GetGameCount(this, Outcome, Slots, Tokens, AsMaster, Mastered);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x001111F6 File Offset: 0x0010F3F6
		private static void IncrementGameCount(string GameType, SifrahGame.GameOutcome? Outcome = null, int? Slots = null, int? Tokens = null, bool? AsMaster = null, bool? Mastered = null)
		{
			The.Game.ModIntGameState(SifrahGame.GetGameCountKey(GameType, Outcome, Slots, Tokens, AsMaster, Mastered), 1);
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x00111214 File Offset: 0x0010F414
		public void AddEffectChance(string EffectName, int Chance, string Duration = null, string DisplayName = null, bool Stack = false, bool Force = false, Effect EffectInstance = null)
		{
			if (this.EffectChances == null)
			{
				this.EffectChances = new List<SifrahEffectChance>();
			}
			SifrahEffectChance sifrahEffectChance = new SifrahEffectChance(EffectName, Chance, Duration, DisplayName, Stack, Force, EffectInstance);
			this.EffectChances.Add(sifrahEffectChance);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x00111254 File Offset: 0x0010F454
		public void ProcessEffectChances()
		{
			if (this.EffectChances != null)
			{
				List<string> list = null;
				foreach (SifrahEffectChance sifrahEffectChance in this.EffectChances)
				{
					if ((sifrahEffectChance.Stack || list == null || !list.Contains(sifrahEffectChance.EffectName)) && sifrahEffectChance.Chance.in100())
					{
						sifrahEffectChance.Apply(The.Player);
						if (!sifrahEffectChance.Stack)
						{
							if (list == null)
							{
								list = new List<string>();
							}
							list.Add(sifrahEffectChance.EffectName);
						}
					}
				}
			}
		}

		// Token: 0x04001BE3 RID: 7139
		public static bool Installed;

		// Token: 0x04001BE4 RID: 7140
		public const int DEFAULT_SLOTS = 5;

		// Token: 0x04001BE5 RID: 7141
		public const int RIGHT_SIDE_WIDTH = 30;

		// Token: 0x04001BE6 RID: 7142
		public string Description;

		// Token: 0x04001BE7 RID: 7143
		public string CorrectTokenSound;

		// Token: 0x04001BE8 RID: 7144
		public string IncorrectTokenSound;

		// Token: 0x04001BE9 RID: 7145
		public string CriticalFailureSound = "Sounds/Creatures/VO/sfx_robot_generic_vo_die";

		// Token: 0x04001BEA RID: 7146
		public string FailureSound = "Sounds/Abilities/sfx_ability_sunderMind_dig_fail";

		// Token: 0x04001BEB RID: 7147
		public string PartialSuccessSound = "sfx_light_refract";

		// Token: 0x04001BEC RID: 7148
		public string SuccessSound = "Sounds/Abilities/sfx_ability_sunderMind_dig_success";

		// Token: 0x04001BED RID: 7149
		public string CriticalSuccessSound = "Sounds/Grenade/sfx_grenade_flashbang_explode";

		// Token: 0x04001BEE RID: 7150
		public int MaxTurns;

		// Token: 0x04001BEF RID: 7151
		public int Turn;

		// Token: 0x04001BF0 RID: 7152
		public int CorrectTokenSoundDelay = 150;

		// Token: 0x04001BF1 RID: 7153
		public int IncorrectTokenSoundDelay = 150;

		// Token: 0x04001BF2 RID: 7154
		public float CorrectTokenSoundVolume = 1f;

		// Token: 0x04001BF3 RID: 7155
		public float IncorrectTokenSoundVolume = 1f;

		// Token: 0x04001BF4 RID: 7156
		public float CriticalFailureSoundVolume = 1f;

		// Token: 0x04001BF5 RID: 7157
		public float FailureSoundVolume = 1f;

		// Token: 0x04001BF6 RID: 7158
		public float PartialSuccessSoundVolume = 1f;

		// Token: 0x04001BF7 RID: 7159
		public float SuccessSoundVolume = 1f;

		// Token: 0x04001BF8 RID: 7160
		public float CriticalSuccessSoundVolume = 1f;

		// Token: 0x04001BF9 RID: 7161
		public bool ShowContextObjectIcon = true;

		// Token: 0x04001BFA RID: 7162
		public bool HadNoPause;

		// Token: 0x04001BFB RID: 7163
		public bool Finished;

		// Token: 0x04001BFC RID: 7164
		public List<SifrahSlot> Slots;

		// Token: 0x04001BFD RID: 7165
		public List<SifrahToken> Tokens;

		// Token: 0x04001BFE RID: 7166
		public int SlotSelected;

		// Token: 0x04001BFF RID: 7167
		public int Powerup;

		// Token: 0x04001C00 RID: 7168
		public List<SifrahEffectChance> EffectChances;

		// Token: 0x04001C01 RID: 7169
		private static string[] status = new string[6];

		// Token: 0x04001C02 RID: 7170
		private static StringBuilder KeySB = new StringBuilder();

		// Token: 0x0200193B RID: 6459
		public enum GameOutcome
		{
			// Token: 0x04006544 RID: 25924
			CriticalFailure,
			// Token: 0x04006545 RID: 25925
			Failure,
			// Token: 0x04006546 RID: 25926
			PartialSuccess,
			// Token: 0x04006547 RID: 25927
			Success,
			// Token: 0x04006548 RID: 25928
			CriticalSuccess
		}
	}
}
