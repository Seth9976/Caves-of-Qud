using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using XRL.Core;
using XRL.UI;

namespace ConsoleLib.Console
{
	// Token: 0x02001537 RID: 5431
	public static class Keyboard
	{
		// Token: 0x0600D384 RID: 54148 RVA: 0x004E7F1B File Offset: 0x004E611B
		public static void Init()
		{
		}

		// Token: 0x0600D385 RID: 54149 RVA: 0x004E7F20 File Offset: 0x004E6120
		public static void ResetIfQueuesEmpty()
		{
			Queue<Keyboard.MouseEvent> mouseEventQueue = Keyboard.MouseEventQueue;
			lock (mouseEventQueue)
			{
				CleanQueue<Keyboard.XRLKeyEvent> keyQueue = Keyboard.KeyQueue;
				lock (keyQueue)
				{
					if (Keyboard.MouseEventQueue.Count == 0 && Keyboard.KeyQueue.Count == 0)
					{
						Keyboard.KeyEvent.Reset();
					}
				}
			}
		}

		// Token: 0x0600D386 RID: 54150 RVA: 0x004E7FA4 File Offset: 0x004E61A4
		public static char MapKeyToChar(Event ev)
		{
			if (Keyboard.Keymap == null)
			{
				Keyboard.InitKeymap();
			}
			if (ev.shift)
			{
				if (!Keyboard.uccharmap.ContainsKey(ev.keyCode))
				{
					return '\0';
				}
				return Keyboard.uccharmap[ev.keyCode];
			}
			else
			{
				if (!Keyboard.lccharmap.ContainsKey(ev.keyCode))
				{
					return '\0';
				}
				return Keyboard.lccharmap[ev.keyCode];
			}
		}

		// Token: 0x0600D387 RID: 54151 RVA: 0x004E8010 File Offset: 0x004E6210
		public static Keys ConvertKeyCodeToKeys(KeyCode code)
		{
			Keys keys;
			if (!Keyboard.Keymap.TryGetValue(code, out keys))
			{
				return Keys.None;
			}
			return keys;
		}

		// Token: 0x0600D388 RID: 54152 RVA: 0x004E802F File Offset: 0x004E622F
		public static char UcKeycodeMapper(KeyCode code)
		{
			if (!Keyboard.uccharmap.ContainsKey(code))
			{
				return '\0';
			}
			return Keyboard.uccharmap[code];
		}

		// Token: 0x0600D389 RID: 54153 RVA: 0x004E804C File Offset: 0x004E624C
		public static KeyCode ParseUnityEngineKeyCode(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return KeyCode.None;
			}
			KeyCode keyCode;
			if (!Keyboard.parseUnityEngineKeyCodeCache.TryGetValue(key, out keyCode))
			{
				keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), key);
				Keyboard.parseUnityEngineKeyCodeCache.Add(key, keyCode);
			}
			return keyCode;
		}

		// Token: 0x0600D38A RID: 54154 RVA: 0x004E8098 File Offset: 0x004E6298
		public static char ConvertKeycodeToLowercaseChar(KeyCode code)
		{
			if (Keyboard.lccharmap == null)
			{
				Keyboard.InitKeymap();
			}
			char c;
			if (Keyboard.lccharmap.TryGetValue(code, out c))
			{
				return c;
			}
			return '\0';
		}

		// Token: 0x0600D38B RID: 54155 RVA: 0x004E80C4 File Offset: 0x004E62C4
		public static void InitKeymap()
		{
			if (Keyboard.Keymap != null)
			{
				return;
			}
			Keyboard.Keymap = new Dictionary<KeyCode, Keys>();
			Keyboard.ReverseKeymap = new Dictionary<Keys, KeyCode>();
			Keyboard.lccharmap = new Dictionary<KeyCode, char>();
			Keyboard.uccharmap = new Dictionary<KeyCode, char>();
			Keyboard.reverselccharmap = new Dictionary<char, List<KeyCode>>();
			Keyboard.reverseuccharmap = new Dictionary<char, List<KeyCode>>();
			Keyboard.Keymap.Add(KeyCode.Escape, Keys.Escape);
			Keyboard.Keymap.Add(KeyCode.LeftWindows, Keys.LWin);
			Keyboard.Keymap.Add(KeyCode.RightWindows, Keys.RWin);
			Keyboard.Keymap.Add(KeyCode.Menu, Keys.Menu);
			Keyboard.Keymap.Add(KeyCode.Numlock, Keys.NumLock);
			Keyboard.Keymap.Add(KeyCode.F1, Keys.F1);
			Keyboard.Keymap.Add(KeyCode.F2, Keys.F2);
			Keyboard.Keymap.Add(KeyCode.F3, Keys.F3);
			Keyboard.Keymap.Add(KeyCode.F4, Keys.F4);
			Keyboard.Keymap.Add(KeyCode.F5, Keys.F5);
			Keyboard.Keymap.Add(KeyCode.F6, Keys.F6);
			Keyboard.Keymap.Add(KeyCode.F7, Keys.F7);
			Keyboard.Keymap.Add(KeyCode.F8, Keys.F8);
			Keyboard.Keymap.Add(KeyCode.F9, Keys.F9);
			Keyboard.Keymap.Add(KeyCode.F10, Keys.F10);
			Keyboard.Keymap.Add(KeyCode.F11, Keys.F11);
			Keyboard.Keymap.Add(KeyCode.F12, Keys.F12);
			Keyboard.Keymap.Add(KeyCode.F13, Keys.F13);
			Keyboard.Keymap.Add(KeyCode.F14, Keys.F14);
			Keyboard.Keymap.Add(KeyCode.F15, Keys.F15);
			Keyboard.Keymap.Add(KeyCode.Help, Keys.F1);
			Keyboard.Keymap.Add(KeyCode.Break, Keys.Pause);
			Keyboard.Keymap.Add(KeyCode.Mouse0, Keys.XButton1);
			Keyboard.Keymap.Add(KeyCode.Mouse1, Keys.XButton2);
			Keyboard.Keymap.Add(KeyCode.Mouse2, Keys.MButton);
			Keyboard.Keymap.Add(KeyCode.Joystick1Button0, Keys.XButton1);
			Keyboard.Keymap.Add(KeyCode.Joystick1Button1, Keys.XButton2);
			Keyboard.Keymap.Add(KeyCode.Joystick1Button2, Keys.MButton);
			Keyboard.Keymap.Add(KeyCode.Print, Keys.PrintScreen);
			Keyboard.Keymap.Add(KeyCode.ScrollLock, Keys.Scroll);
			Keyboard.Keymap.Add(KeyCode.Pause, Keys.Pause);
			Keyboard.Keymap.Add(KeyCode.BackQuote, Keys.Oemtilde);
			Keyboard.lccharmap.Add(KeyCode.BackQuote, '~');
			Keyboard.uccharmap.Add(KeyCode.BackQuote, '`');
			Keyboard.Keymap.Add(KeyCode.Alpha0, Keys.D0);
			Keyboard.lccharmap.Add(KeyCode.Alpha0, '0');
			Keyboard.uccharmap.Add(KeyCode.Alpha0, ')');
			Keyboard.Keymap.Add(KeyCode.Alpha1, Keys.D1);
			Keyboard.lccharmap.Add(KeyCode.Alpha1, '1');
			Keyboard.uccharmap.Add(KeyCode.Alpha1, '!');
			Keyboard.Keymap.Add(KeyCode.Alpha2, Keys.D2);
			Keyboard.lccharmap.Add(KeyCode.Alpha2, '2');
			Keyboard.uccharmap.Add(KeyCode.Alpha2, '@');
			Keyboard.Keymap.Add(KeyCode.Alpha3, Keys.D3);
			Keyboard.lccharmap.Add(KeyCode.Alpha3, '3');
			Keyboard.uccharmap.Add(KeyCode.Alpha3, '#');
			Keyboard.Keymap.Add(KeyCode.Alpha4, Keys.D4);
			Keyboard.lccharmap.Add(KeyCode.Alpha4, '4');
			Keyboard.uccharmap.Add(KeyCode.Alpha4, '$');
			Keyboard.Keymap.Add(KeyCode.Alpha5, Keys.D5);
			Keyboard.lccharmap.Add(KeyCode.Alpha5, '5');
			Keyboard.uccharmap.Add(KeyCode.Alpha5, '%');
			Keyboard.Keymap.Add(KeyCode.Alpha6, Keys.D6);
			Keyboard.lccharmap.Add(KeyCode.Alpha6, '6');
			Keyboard.uccharmap.Add(KeyCode.Alpha6, '^');
			Keyboard.Keymap.Add(KeyCode.Alpha7, Keys.D7);
			Keyboard.lccharmap.Add(KeyCode.Alpha7, '7');
			Keyboard.uccharmap.Add(KeyCode.Alpha7, '&');
			Keyboard.Keymap.Add(KeyCode.Alpha8, Keys.D8);
			Keyboard.lccharmap.Add(KeyCode.Alpha8, '8');
			Keyboard.uccharmap.Add(KeyCode.Alpha8, '*');
			Keyboard.Keymap.Add(KeyCode.Alpha9, Keys.D9);
			Keyboard.lccharmap.Add(KeyCode.Alpha9, '9');
			Keyboard.uccharmap.Add(KeyCode.Alpha9, '(');
			Keyboard.Keymap.Add(KeyCode.Minus, Keys.OemMinus);
			Keyboard.lccharmap.Add(KeyCode.Minus, '-');
			Keyboard.uccharmap.Add(KeyCode.Minus, '_');
			Keyboard.Keymap.Add(KeyCode.Plus, Keys.Oemplus);
			Keyboard.lccharmap.Add(KeyCode.Plus, '+');
			Keyboard.uccharmap.Add(KeyCode.Plus, '+');
			Keyboard.Keymap.Add(KeyCode.Delete, Keys.Delete);
			Keyboard.Keymap.Add(KeyCode.End, Keys.End);
			Keyboard.Keymap.Add(KeyCode.Home, Keys.Home);
			Keyboard.Keymap.Add(KeyCode.Insert, Keys.Insert);
			Keyboard.Keymap.Add(KeyCode.PageUp, Keys.Prior);
			Keyboard.Keymap.Add(KeyCode.PageDown, Keys.Next);
			Keyboard.Keymap.Add(KeyCode.Backspace, Keys.Back);
			Keyboard.lccharmap.Add(KeyCode.Backspace, '\b');
			Keyboard.uccharmap.Add(KeyCode.Backspace, '\b');
			Keyboard.Keymap.Add(KeyCode.Tab, Keys.Tab);
			Keyboard.lccharmap.Add(KeyCode.Tab, '\t');
			Keyboard.uccharmap.Add(KeyCode.Tab, '\t');
			Keyboard.Keymap.Add(KeyCode.LeftBracket, Keys.Oem4);
			Keyboard.lccharmap.Add(KeyCode.LeftBracket, '[');
			Keyboard.uccharmap.Add(KeyCode.LeftBracket, '{');
			Keyboard.Keymap.Add(KeyCode.RightBracket, Keys.Oem6);
			Keyboard.lccharmap.Add(KeyCode.RightBracket, ']');
			Keyboard.uccharmap.Add(KeyCode.RightBracket, '}');
			Keyboard.Keymap.Add(KeyCode.Backslash, Keys.Oem102);
			Keyboard.lccharmap.Add(KeyCode.Backslash, '\\');
			Keyboard.uccharmap.Add(KeyCode.Backslash, '|');
			Keyboard.Keymap.Add(KeyCode.CapsLock, Keys.CapsLock);
			Keyboard.Keymap.Add(KeyCode.Semicolon, Keys.Oem1);
			Keyboard.lccharmap.Add(KeyCode.Semicolon, ';');
			Keyboard.uccharmap.Add(KeyCode.Semicolon, ':');
			Keyboard.Keymap.Add(KeyCode.Quote, Keys.Oem7);
			Keyboard.lccharmap.Add(KeyCode.Quote, '\'');
			Keyboard.uccharmap.Add(KeyCode.Quote, '"');
			Keyboard.Keymap.Add(KeyCode.Comma, Keys.Oemcomma);
			Keyboard.Keymap.Add(KeyCode.Period, Keys.OemPeriod);
			Keyboard.Keymap.Add(KeyCode.Question, Keys.OemQuestion);
			Keyboard.lccharmap.Add(KeyCode.Question, '/');
			Keyboard.uccharmap.Add(KeyCode.Question, '?');
			Keyboard.Keymap.Add(KeyCode.Slash, Keys.OemQuestion);
			Keyboard.lccharmap.Add(KeyCode.Slash, '/');
			Keyboard.uccharmap.Add(KeyCode.Slash, '?');
			Keyboard.Keymap.Add(KeyCode.Space, Keys.Space);
			Keyboard.Keymap.Add(KeyCode.Return, Keys.Enter);
			Keyboard.Keymap.Add(KeyCode.Keypad0, Keys.NumPad0);
			Keyboard.Keymap.Add(KeyCode.Keypad1, Keys.NumPad1);
			Keyboard.Keymap.Add(KeyCode.Keypad2, Keys.NumPad2);
			Keyboard.Keymap.Add(KeyCode.Keypad3, Keys.NumPad3);
			Keyboard.Keymap.Add(KeyCode.Keypad4, Keys.NumPad4);
			Keyboard.Keymap.Add(KeyCode.Keypad5, Keys.NumPad5);
			Keyboard.Keymap.Add(KeyCode.Keypad6, Keys.NumPad6);
			Keyboard.Keymap.Add(KeyCode.Keypad7, Keys.NumPad7);
			Keyboard.Keymap.Add(KeyCode.Keypad8, Keys.NumPad8);
			Keyboard.Keymap.Add(KeyCode.Keypad9, Keys.NumPad9);
			Keyboard.Keymap.Add(KeyCode.KeypadDivide, Keys.Divide);
			Keyboard.Keymap.Add(KeyCode.KeypadMultiply, Keys.Multiply);
			Keyboard.Keymap.Add(KeyCode.KeypadPlus, Keys.Add);
			Keyboard.Keymap.Add(KeyCode.KeypadMinus, Keys.Subtract);
			Keyboard.Keymap.Add(KeyCode.KeypadPeriod, Keys.Decimal);
			Keyboard.lccharmap.Add(KeyCode.KeypadDivide, '/');
			Keyboard.uccharmap.Add(KeyCode.KeypadDivide, '/');
			Keyboard.lccharmap.Add(KeyCode.KeypadMultiply, '*');
			Keyboard.uccharmap.Add(KeyCode.KeypadMultiply, '*');
			Keyboard.lccharmap.Add(KeyCode.KeypadPlus, '+');
			Keyboard.uccharmap.Add(KeyCode.KeypadPlus, '+');
			Keyboard.lccharmap.Add(KeyCode.KeypadMinus, '-');
			Keyboard.uccharmap.Add(KeyCode.KeypadMinus, '-');
			Keyboard.lccharmap.Add(KeyCode.KeypadPeriod, '.');
			Keyboard.uccharmap.Add(KeyCode.KeypadPeriod, '.');
			Keyboard.Keymap.Add(KeyCode.LeftArrow, Keys.Left);
			Keyboard.Keymap.Add(KeyCode.RightArrow, Keys.Right);
			Keyboard.Keymap.Add(KeyCode.DownArrow, Keys.Down);
			Keyboard.Keymap.Add(KeyCode.UpArrow, Keys.Up);
			Keyboard.Keymap.Add(KeyCode.A, Keys.A);
			Keyboard.Keymap.Add(KeyCode.B, Keys.B);
			Keyboard.Keymap.Add(KeyCode.C, Keys.C);
			Keyboard.Keymap.Add(KeyCode.D, Keys.D);
			Keyboard.Keymap.Add(KeyCode.E, Keys.E);
			Keyboard.Keymap.Add(KeyCode.F, Keys.F);
			Keyboard.Keymap.Add(KeyCode.G, Keys.G);
			Keyboard.Keymap.Add(KeyCode.H, Keys.H);
			Keyboard.Keymap.Add(KeyCode.I, Keys.I);
			Keyboard.Keymap.Add(KeyCode.J, Keys.J);
			Keyboard.Keymap.Add(KeyCode.K, Keys.K);
			Keyboard.Keymap.Add(KeyCode.L, Keys.L);
			Keyboard.Keymap.Add(KeyCode.M, Keys.M);
			Keyboard.Keymap.Add(KeyCode.N, Keys.N);
			Keyboard.Keymap.Add(KeyCode.O, Keys.O);
			Keyboard.Keymap.Add(KeyCode.P, Keys.P);
			Keyboard.Keymap.Add(KeyCode.Q, Keys.Q);
			Keyboard.Keymap.Add(KeyCode.R, Keys.R);
			Keyboard.Keymap.Add(KeyCode.S, Keys.S);
			Keyboard.Keymap.Add(KeyCode.T, Keys.T);
			Keyboard.Keymap.Add(KeyCode.U, Keys.U);
			Keyboard.Keymap.Add(KeyCode.V, Keys.V);
			Keyboard.Keymap.Add(KeyCode.W, Keys.W);
			Keyboard.Keymap.Add(KeyCode.X, Keys.X);
			Keyboard.Keymap.Add(KeyCode.Y, Keys.Y);
			Keyboard.Keymap.Add(KeyCode.Z, Keys.Z);
			Keyboard.Keymap.Add(KeyCode.None, Keys.None);
			Keyboard.Keymap.Add(KeyCode.Clear, Keys.Clear);
			Keyboard.Keymap.Add(KeyCode.Exclaim, Keys.D1);
			Keyboard.Keymap.Add(KeyCode.DoubleQuote, Keys.Oem7);
			Keyboard.Keymap.Add(KeyCode.Hash, Keys.D3);
			Keyboard.Keymap.Add(KeyCode.Dollar, Keys.D4);
			Keyboard.Keymap.Add(KeyCode.Ampersand, Keys.D7);
			Keyboard.Keymap.Add(KeyCode.LeftParen, Keys.D9);
			Keyboard.Keymap.Add(KeyCode.RightParen, Keys.D0);
			Keyboard.Keymap.Add(KeyCode.Asterisk, Keys.D8);
			Keyboard.Keymap.Add(KeyCode.Colon, Keys.Oem1);
			Keyboard.Keymap.Add(KeyCode.Less, Keys.Oemcomma);
			Keyboard.Keymap.Add(KeyCode.Equals, Keys.Oemplus);
			Keyboard.Keymap.Add(KeyCode.Greater, Keys.OemPeriod);
			Keyboard.Keymap.Add(KeyCode.At, Keys.D2);
			Keyboard.Keymap.Add(KeyCode.Caret, Keys.D6);
			Keyboard.Keymap.Add(KeyCode.Underscore, Keys.OemMinus);
			Keyboard.Keymap.Add(KeyCode.KeypadEnter, Keys.Enter);
			Keyboard.Keymap.Add(KeyCode.KeypadEquals, Keys.Oemplus);
			Keyboard.lccharmap.Add(KeyCode.KeypadEquals, '=');
			Keyboard.uccharmap.Add(KeyCode.KeypadEquals, '=');
			Keyboard.lccharmap.Add(KeyCode.Underscore, '_');
			Keyboard.uccharmap.Add(KeyCode.Underscore, '_');
			Keyboard.lccharmap.Add(KeyCode.Caret, '^');
			Keyboard.uccharmap.Add(KeyCode.Caret, '^');
			Keyboard.lccharmap.Add(KeyCode.At, '@');
			Keyboard.uccharmap.Add(KeyCode.At, '@');
			Keyboard.lccharmap.Add(KeyCode.Greater, '>');
			Keyboard.uccharmap.Add(KeyCode.Greater, '>');
			Keyboard.lccharmap.Add(KeyCode.Equals, '=');
			Keyboard.uccharmap.Add(KeyCode.Equals, '=');
			Keyboard.lccharmap.Add(KeyCode.Less, '<');
			Keyboard.uccharmap.Add(KeyCode.Less, '<');
			Keyboard.lccharmap.Add(KeyCode.Colon, ':');
			Keyboard.uccharmap.Add(KeyCode.Colon, ':');
			Keyboard.lccharmap.Add(KeyCode.Asterisk, '*');
			Keyboard.uccharmap.Add(KeyCode.Asterisk, '*');
			Keyboard.lccharmap.Add(KeyCode.RightParen, ')');
			Keyboard.uccharmap.Add(KeyCode.RightParen, ')');
			Keyboard.lccharmap.Add(KeyCode.LeftParen, '(');
			Keyboard.uccharmap.Add(KeyCode.LeftParen, '(');
			Keyboard.lccharmap.Add(KeyCode.Ampersand, '&');
			Keyboard.uccharmap.Add(KeyCode.Ampersand, '&');
			Keyboard.lccharmap.Add(KeyCode.Dollar, '$');
			Keyboard.uccharmap.Add(KeyCode.Dollar, '$');
			Keyboard.lccharmap.Add(KeyCode.Hash, '#');
			Keyboard.uccharmap.Add(KeyCode.Hash, '#');
			Keyboard.lccharmap.Add(KeyCode.Exclaim, '!');
			Keyboard.uccharmap.Add(KeyCode.Exclaim, '!');
			Keyboard.lccharmap.Add(KeyCode.Space, ' ');
			Keyboard.uccharmap.Add(KeyCode.Space, ' ');
			Keyboard.lccharmap.Add(KeyCode.KeypadEnter, '\r');
			Keyboard.uccharmap.Add(KeyCode.KeypadEnter, '\r');
			Keyboard.lccharmap.Add(KeyCode.Return, '\r');
			Keyboard.uccharmap.Add(KeyCode.Return, '\r');
			Keyboard.lccharmap.Add(KeyCode.DoubleQuote, '"');
			Keyboard.uccharmap.Add(KeyCode.DoubleQuote, '"');
			Keyboard.lccharmap.Add(KeyCode.A, 'a');
			Keyboard.uccharmap.Add(KeyCode.A, 'A');
			Keyboard.lccharmap.Add(KeyCode.B, 'b');
			Keyboard.uccharmap.Add(KeyCode.B, 'B');
			Keyboard.lccharmap.Add(KeyCode.C, 'c');
			Keyboard.uccharmap.Add(KeyCode.C, 'C');
			Keyboard.lccharmap.Add(KeyCode.D, 'd');
			Keyboard.uccharmap.Add(KeyCode.D, 'D');
			Keyboard.lccharmap.Add(KeyCode.E, 'e');
			Keyboard.uccharmap.Add(KeyCode.E, 'E');
			Keyboard.lccharmap.Add(KeyCode.F, 'f');
			Keyboard.uccharmap.Add(KeyCode.F, 'F');
			Keyboard.lccharmap.Add(KeyCode.G, 'g');
			Keyboard.uccharmap.Add(KeyCode.G, 'G');
			Keyboard.lccharmap.Add(KeyCode.H, 'h');
			Keyboard.uccharmap.Add(KeyCode.H, 'H');
			Keyboard.lccharmap.Add(KeyCode.I, 'i');
			Keyboard.uccharmap.Add(KeyCode.I, 'I');
			Keyboard.lccharmap.Add(KeyCode.J, 'j');
			Keyboard.uccharmap.Add(KeyCode.J, 'J');
			Keyboard.lccharmap.Add(KeyCode.K, 'k');
			Keyboard.uccharmap.Add(KeyCode.K, 'K');
			Keyboard.lccharmap.Add(KeyCode.L, 'l');
			Keyboard.uccharmap.Add(KeyCode.L, 'L');
			Keyboard.lccharmap.Add(KeyCode.M, 'm');
			Keyboard.uccharmap.Add(KeyCode.M, 'M');
			Keyboard.lccharmap.Add(KeyCode.N, 'n');
			Keyboard.uccharmap.Add(KeyCode.N, 'N');
			Keyboard.lccharmap.Add(KeyCode.O, 'o');
			Keyboard.uccharmap.Add(KeyCode.O, 'O');
			Keyboard.lccharmap.Add(KeyCode.P, 'p');
			Keyboard.uccharmap.Add(KeyCode.P, 'P');
			Keyboard.lccharmap.Add(KeyCode.Q, 'q');
			Keyboard.uccharmap.Add(KeyCode.Q, 'Q');
			Keyboard.lccharmap.Add(KeyCode.R, 'r');
			Keyboard.uccharmap.Add(KeyCode.R, 'R');
			Keyboard.lccharmap.Add(KeyCode.S, 's');
			Keyboard.uccharmap.Add(KeyCode.S, 'S');
			Keyboard.lccharmap.Add(KeyCode.T, 't');
			Keyboard.uccharmap.Add(KeyCode.T, 'T');
			Keyboard.lccharmap.Add(KeyCode.U, 'u');
			Keyboard.uccharmap.Add(KeyCode.U, 'U');
			Keyboard.lccharmap.Add(KeyCode.V, 'v');
			Keyboard.uccharmap.Add(KeyCode.V, 'V');
			Keyboard.lccharmap.Add(KeyCode.W, 'w');
			Keyboard.uccharmap.Add(KeyCode.W, 'W');
			Keyboard.lccharmap.Add(KeyCode.X, 'x');
			Keyboard.uccharmap.Add(KeyCode.X, 'X');
			Keyboard.lccharmap.Add(KeyCode.Y, 'y');
			Keyboard.uccharmap.Add(KeyCode.Y, 'Y');
			Keyboard.lccharmap.Add(KeyCode.Z, 'z');
			Keyboard.uccharmap.Add(KeyCode.Z, 'Z');
			foreach (KeyValuePair<KeyCode, Keys> keyValuePair in Keyboard.Keymap)
			{
				if (!Keyboard.ReverseKeymap.ContainsKey(keyValuePair.Value))
				{
					Keyboard.ReverseKeymap.Add(keyValuePair.Value, keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<KeyCode, char> keyValuePair2 in Keyboard.lccharmap)
			{
				if (!Keyboard.reverselccharmap.ContainsKey(keyValuePair2.Value))
				{
					Keyboard.reverselccharmap.Add(keyValuePair2.Value, new List<KeyCode>());
				}
				Keyboard.reverselccharmap[keyValuePair2.Value].Add(keyValuePair2.Key);
			}
			foreach (KeyValuePair<KeyCode, char> keyValuePair3 in Keyboard.uccharmap)
			{
				if (!Keyboard.reverseuccharmap.ContainsKey(keyValuePair3.Value))
				{
					Keyboard.reverseuccharmap.Add(keyValuePair3.Value, new List<KeyCode>());
				}
				Keyboard.reverseuccharmap[keyValuePair3.Value].Add(keyValuePair3.Key);
			}
		}

		// Token: 0x0600D38C RID: 54156 RVA: 0x004E9264 File Offset: 0x004E7464
		public static void PushCommand(string command, object data = null)
		{
			Keyboard.PushMouseEvent(Keyboard.COMMAND_EVENT_PREFIX + command, data);
		}

		// Token: 0x0600D38D RID: 54157 RVA: 0x004E9278 File Offset: 0x004E7478
		public static bool IsCommandKey(string command)
		{
			if (command == "Page Up")
			{
				if (Keyboard.RawCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Page Up")
				{
					return true;
				}
				if (Keyboard.RawCode == Keys.Prior)
				{
					return true;
				}
			}
			if (command == "Page Down")
			{
				if (Keyboard.RawCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Page Down")
				{
					return true;
				}
				if (Keyboard.RawCode == Keys.Next)
				{
					return true;
				}
			}
			if (Keyboard.RawCode == Keys.MouseEvent)
			{
				return Keyboard.CurrentMouseEvent.Event.StartsWith(Keyboard.COMMAND_EVENT_PREFIX) && Keyboard.CurrentMouseEvent.Event.Substring(Keyboard.COMMAND_EVENT_PREFIX.Length) == command;
			}
			ValueTuple<int, int> allKeysFromCommand = CommandBindingManager.GetAllKeysFromCommand(command);
			int item = allKeysFromCommand.Item1;
			int item2 = allKeysFromCommand.Item2;
			return (item != 0 && item == (int)Keyboard.RawCode) || (item2 != 0 && item2 == (int)Keyboard.RawCode);
		}

		// Token: 0x0600D38E RID: 54158 RVA: 0x004E936F File Offset: 0x004E756F
		public static void PushKey(KeyCode key)
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(key, '\0', false, false, false), false);
		}

		// Token: 0x0600D38F RID: 54159 RVA: 0x004E9384 File Offset: 0x004E7584
		public static void PushKey(Keyboard.XRLKeyEvent ev, bool bAllowMap = false)
		{
			if (!TutorialManager.AllowPushKey(ev, bAllowMap))
			{
				return;
			}
			if (Keyboard.Keymap == null)
			{
				Keyboard.InitKeymap();
			}
			ev.allowmap = bAllowMap;
			if (Keyboard.KeyQueue.Count > 2 && GameManager.bCapInputBuffer)
			{
				return;
			}
			CleanQueue<Keyboard.XRLKeyEvent> keyQueue = Keyboard.KeyQueue;
			lock (keyQueue)
			{
				Keyboard.KeyQueue.Enqueue(ev);
			}
			Keyboard.KeyEvent.Set();
		}

		// Token: 0x0600D390 RID: 54160 RVA: 0x004E9408 File Offset: 0x004E7608
		public static bool HasKey()
		{
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
			return Keyboard.KeyQueue.Count > 0;
		}

		// Token: 0x0600D391 RID: 54161 RVA: 0x004E9434 File Offset: 0x004E7634
		public static Keyboard.XRLKeyEvent PopKey()
		{
			CleanQueue<Keyboard.XRLKeyEvent> keyQueue = Keyboard.KeyQueue;
			Keyboard.XRLKeyEvent xrlkeyEvent;
			lock (keyQueue)
			{
				if (Keyboard.KeyQueue.Count == 0)
				{
					xrlkeyEvent = null;
				}
				else
				{
					xrlkeyEvent = Keyboard.KeyQueue.Dequeue();
				}
			}
			return xrlkeyEvent;
		}

		// Token: 0x0600D392 RID: 54162 RVA: 0x004E948C File Offset: 0x004E768C
		public static void PushMouseEvent(string ev, object data = null)
		{
			Keyboard.PushMouseEvent(ev, 0, 0, data);
		}

		// Token: 0x0600D393 RID: 54163 RVA: 0x004E9498 File Offset: 0x004E7698
		public static void PushMouseEvent(string ev, int x, int y, object data = null)
		{
			if (!TutorialManager.AllowMouseEvent(ev, x, y, data))
			{
				return;
			}
			if (Keyboard.MouseEventQueue.Count > 2 && GameManager.bCapInputBuffer)
			{
				return;
			}
			Queue<Keyboard.MouseEvent> mouseEventQueue = Keyboard.MouseEventQueue;
			lock (mouseEventQueue)
			{
				Keyboard.MouseEvent mouseEvent;
				if (Keyboard.MouseEventPool.Count > 0)
				{
					mouseEvent = Keyboard.MouseEventPool.Dequeue();
					mouseEvent.Event = ev;
					mouseEvent.x = x;
					mouseEvent.y = y;
					mouseEvent.Data = data;
				}
				else
				{
					mouseEvent = new Keyboard.MouseEvent(ev, x, y, data);
				}
				Keyboard.MouseEventQueue.Enqueue(mouseEvent);
				Keyboard.KeyEvent.Set();
			}
		}

		// Token: 0x0600D394 RID: 54164 RVA: 0x004E9548 File Offset: 0x004E7748
		public static Keyboard.MouseEvent PopMouseEvent()
		{
			Queue<Keyboard.MouseEvent> mouseEventQueue = Keyboard.MouseEventQueue;
			lock (mouseEventQueue)
			{
				if (Keyboard.MouseEventQueue.Count > 0)
				{
					Keyboard.MouseEvent mouseEvent = Keyboard.MouseEventQueue.Dequeue();
					Keyboard.MouseEventUsed.Enqueue(mouseEvent);
					return mouseEvent;
				}
			}
			return null;
		}

		// Token: 0x0600D395 RID: 54165 RVA: 0x004E95AC File Offset: 0x004E77AC
		public static bool HasMouseEvent(bool filterMetaCommands = false)
		{
			Queue<Keyboard.MouseEvent> mouseEventQueue = Keyboard.MouseEventQueue;
			bool flag2;
			lock (mouseEventQueue)
			{
				if (filterMetaCommands)
				{
					flag2 = Keyboard.MouseEventQueue.Count > 0 && (Keyboard.MouseEventQueue.Peek().Event == null || !Keyboard.metaMousecommands.ContainsKey(Keyboard.MouseEventQueue.Peek().Event));
				}
				else
				{
					flag2 = Keyboard.MouseEventQueue.Count > 0;
				}
			}
			return flag2;
		}

		// Token: 0x0600D396 RID: 54166 RVA: 0x004E963C File Offset: 0x004E783C
		public static void ClearInput(bool LeaveMovementEvents = false, bool clearFrameDown = false)
		{
			CleanQueue<Keyboard.XRLKeyEvent> keyQueue = Keyboard.KeyQueue;
			lock (keyQueue)
			{
				Keyboard.RawCode = Keys.None;
				Keyboard.vkCode = Keys.None;
				Keyboard.CurrentMouseEvent.Event = "";
				Keyboard.ClearMouseEvents(LeaveMovementEvents);
				Keyboard.KeyQueue.Clear();
			}
			if (clearFrameDown)
			{
				ControlManager.ConsumeCurrentInput();
			}
		}

		// Token: 0x0600D397 RID: 54167 RVA: 0x004E96A8 File Offset: 0x004E78A8
		public static void ClearMouseEvents(bool LeaveMovementEvents = false)
		{
			if (LeaveMovementEvents)
			{
				Keyboard.tempMouseEventQueue.Clear();
			}
			Queue<Keyboard.MouseEvent> mouseEventQueue = Keyboard.MouseEventQueue;
			lock (mouseEventQueue)
			{
				while (Keyboard.MouseEventQueue.Count > 0)
				{
					Keyboard.MouseEvent mouseEvent = Keyboard.MouseEventQueue.Dequeue();
					if (LeaveMovementEvents && mouseEvent.Event != null && (mouseEvent.Event.StartsWith("Meta:Navigate") || mouseEvent.Event.StartsWith("Command:CmdMove") || mouseEvent.Event.StartsWith("Command:CmdAutoMove")))
					{
						Keyboard.tempMouseEventQueue.Enqueue(mouseEvent);
					}
					else
					{
						mouseEvent.Data = null;
						Keyboard.MouseEventPool.Enqueue(mouseEvent);
					}
				}
				while (Keyboard.MouseEventUsed.Count > 0)
				{
					Keyboard.MouseEvent mouseEvent2 = Keyboard.MouseEventUsed.Dequeue();
					Keyboard.MouseEventPool.Enqueue(mouseEvent2);
				}
				if (LeaveMovementEvents)
				{
					Queue<Keyboard.MouseEvent> mouseEventQueue2 = Keyboard.MouseEventQueue;
					Keyboard.MouseEventQueue = Keyboard.tempMouseEventQueue;
					Keyboard.tempMouseEventQueue = mouseEventQueue2;
				}
			}
		}

		// Token: 0x0600D398 RID: 54168 RVA: 0x004E97A4 File Offset: 0x004E79A4
		public static void IdleWait()
		{
			if (Options.DisableAllIdleTileAnimations)
			{
				Keyboard.KeyEvent.WaitOne();
				return;
			}
			Keyboard.KeyEvent.WaitOne(Options.ThrottleAnimationRate);
		}

		// Token: 0x0600D399 RID: 54169 RVA: 0x004E97CC File Offset: 0x004E79CC
		public static bool kbhit()
		{
			if (XRLCore.bThreadFocus)
			{
				return Keyboard.KeyQueue.Count > 0 || Keyboard.MouseEventQueue.Count > 0;
			}
			Keyboard.KeyEvent.WaitOne(200);
			return Keyboard.KeyQueue.Count > 0 || Keyboard.MouseEventQueue.Count > 0;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600D39A RID: 54170 RVA: 0x004E9829 File Offset: 0x004E7A29
		// (set) Token: 0x0600D39B RID: 54171 RVA: 0x004E9830 File Offset: 0x004E7A30
		public static bool bCtrl
		{
			get
			{
				return Keyboard._bCtrl;
			}
			set
			{
				Keyboard._bCtrl = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600D39C RID: 54172 RVA: 0x004E9838 File Offset: 0x004E7A38
		// (set) Token: 0x0600D39D RID: 54173 RVA: 0x004E983F File Offset: 0x004E7A3F
		public static bool bShift
		{
			get
			{
				return Keyboard._bShift;
			}
			set
			{
				Keyboard._bShift = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600D39E RID: 54174 RVA: 0x004E9847 File Offset: 0x004E7A47
		// (set) Token: 0x0600D39F RID: 54175 RVA: 0x004E984E File Offset: 0x004E7A4E
		public static bool bAlt
		{
			get
			{
				return Keyboard._bAlt;
			}
			set
			{
				Keyboard._bAlt = value;
			}
		}

		// Token: 0x0600D3A0 RID: 54176 RVA: 0x004E9858 File Offset: 0x004E7A58
		public static void GetNextKey(bool MapDirectionsToArrows, bool pumpActions = false, bool waitForNext = true)
		{
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
			if (TextConsole.Mode == TerminalMode.Unity)
			{
				while (!Keyboard.HasMouseEvent(false))
				{
					if (!Keyboard.HasKey())
					{
						if (!waitForNext)
						{
							Keyboard.vkCode = Keys.None;
							Keyboard.Char = 0;
							Keyboard.MetaKey = 0;
							return;
						}
						if (XRLCore.bThreadFocus)
						{
							if (pumpActions)
							{
								if (!GameManager.Instance.gameQueue.HasTask())
								{
									Keyboard.KeyEvent.WaitOne(20);
								}
							}
							else
							{
								Keyboard.KeyEvent.WaitOne(20);
							}
						}
						else
						{
							Keyboard.KeyEvent.WaitOne(1000);
						}
					}
					Keyboard.ResetIfQueuesEmpty();
					Keyboard.XRLKeyEvent xrlkeyEvent = Keyboard.PopKey();
					if (xrlkeyEvent == null)
					{
						if (pumpActions)
						{
							GameManager.Instance.gameQueue.executeTasks();
						}
					}
					else
					{
						Keys keys = Keys.None;
						if (Keyboard.Keymap == null)
						{
							Debug.Log("Null keymap??");
							return;
						}
						if (!Keyboard.Keymap.ContainsKey(xrlkeyEvent.keyCode))
						{
							Debug.LogWarning("Unknown key code " + xrlkeyEvent.keyCode.ToString());
						}
						else
						{
							keys = Keyboard.Keymap[xrlkeyEvent.keyCode];
						}
						if (keys == Keys.None)
						{
							return;
						}
						Keyboard.vkCode = keys;
						Keyboard.RawCode = Keyboard.vkCode;
						Keyboard.Char = (int)xrlkeyEvent.character;
						if (xrlkeyEvent.shift)
						{
							Keyboard.vkCode += 65536;
						}
						if (xrlkeyEvent.shift)
						{
							Keyboard.RawCode += 65536;
						}
						if (xrlkeyEvent.control)
						{
							Keyboard.vkCode += 131072;
						}
						if (xrlkeyEvent.control)
						{
							Keyboard.RawCode += 131072;
						}
						if (xrlkeyEvent.alt)
						{
							Keyboard.vkCode += 262144;
						}
						if (xrlkeyEvent.alt)
						{
							Keyboard.RawCode += 262144;
						}
						if (Keyboard.vkCode == Keys.Oem1)
						{
							Keyboard.Char = 59;
						}
						if (Keyboard.vkCode == (Keys.RButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 58;
						}
						if (Keyboard.vkCode == Keys.OemPeriod)
						{
							Keyboard.Char = 46;
						}
						if (Keyboard.vkCode == Keys.OemQuestion)
						{
							Keyboard.Char = 47;
						}
						if (Keyboard.vkCode == Keys.OemMinus)
						{
							Keyboard.Char = 45;
						}
						if (Keyboard.vkCode == Keys.Oem1)
						{
							Keyboard.Char = 59;
						}
						if (Keyboard.vkCode == Keys.Oemplus)
						{
							Keyboard.Char = 61;
						}
						if (Keyboard.vkCode == Keys.Oemcomma)
						{
							Keyboard.Char = 44;
						}
						if (Keyboard.vkCode == Keys.Oemtilde)
						{
							Keyboard.Char = 96;
						}
						if (Keyboard.vkCode == Keys.Oem4)
						{
							Keyboard.Char = 91;
						}
						if (Keyboard.vkCode == Keys.OemPipe)
						{
							Keyboard.Char = 124;
						}
						if (Keyboard.vkCode == Keys.Oem6)
						{
							Keyboard.Char = 93;
						}
						if (Keyboard.vkCode == Keys.Oem7)
						{
							Keyboard.Char = 39;
						}
						if (Keyboard.vkCode == Keys.Oem102)
						{
							Keyboard.Char = 92;
						}
						if (Keyboard.vkCode == (Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 62;
						}
						if (Keyboard.vkCode == (Keys.LButton | Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 63;
						}
						if (Keyboard.vkCode == (Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 95;
						}
						if (Keyboard.vkCode == (Keys.RButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 58;
						}
						if (Keyboard.vkCode == (Keys.LButton | Keys.RButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 43;
						}
						if (Keyboard.vkCode == (Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift))
						{
							Keyboard.Char = 60;
						}
						if (Keyboard.vkCode == (Keys)65728)
						{
							Keyboard.Char = 126;
						}
						if (Keyboard.vkCode == (Keys)65755)
						{
							Keyboard.Char = 123;
						}
						if (Keyboard.vkCode == (Keys)65756)
						{
							Keyboard.Char = 124;
						}
						if (Keyboard.vkCode == (Keys)65757)
						{
							Keyboard.Char = 125;
						}
						if (Keyboard.vkCode == (Keys)65758)
						{
							Keyboard.Char = 34;
						}
						if (Keyboard.vkCode == (Keys)65762)
						{
							Keyboard.Char = 124;
						}
						if (Keyboard.vkCode == Keys.Left)
						{
							Keyboard.vkCode = Keys.NumPad4;
							Keyboard.Char = 52;
						}
						if (Keyboard.vkCode == Keys.Right)
						{
							Keyboard.vkCode = Keys.NumPad6;
							Keyboard.Char = 54;
						}
						if (Keyboard.vkCode == Keys.Up)
						{
							Keyboard.vkCode = Keys.NumPad8;
							Keyboard.Char = 56;
						}
						if (Keyboard.vkCode == Keys.Down)
						{
							Keyboard.vkCode = Keys.NumPad2;
							Keyboard.Char = 50;
						}
						Keyboard.MetaKey = (int)Keyboard.vkCode;
						if (!(CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdIgnore"))
						{
							if (MapDirectionsToArrows && xrlkeyEvent.allowmap)
							{
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdWait")
								{
									Keyboard.vkCode = Keys.NumPad5;
									Keyboard.Char = 53;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveN")
								{
									Keyboard.vkCode = Keys.NumPad8;
									Keyboard.Char = 56;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveS")
								{
									Keyboard.vkCode = Keys.NumPad2;
									Keyboard.Char = 50;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveE")
								{
									Keyboard.vkCode = Keys.NumPad6;
									Keyboard.Char = 54;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveW")
								{
									Keyboard.vkCode = Keys.NumPad4;
									Keyboard.Char = 52;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveNW")
								{
									Keyboard.vkCode = Keys.NumPad7;
									Keyboard.Char = 55;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveNE")
								{
									Keyboard.vkCode = Keys.NumPad9;
									Keyboard.Char = 57;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveSW")
								{
									Keyboard.vkCode = Keys.NumPad1;
									Keyboard.Char = 49;
								}
								if (CommandBindingManager.MapKeyToCommand(Keyboard.MetaKey, null) == "CmdMoveSE")
								{
									Keyboard.vkCode = Keys.NumPad3;
									Keyboard.Char = 51;
								}
								if (Keyboard.IsCommandKey("Page Up"))
								{
									Keyboard.MetaKey = 33;
									Keyboard.vkCode = Keys.Prior;
								}
								if (Keyboard.IsCommandKey("Page Down"))
								{
									Keyboard.MetaKey = 34;
									Keyboard.vkCode = Keys.Next;
								}
							}
							if (Options.GetOption("OptionMapShiftDirectionToPage", "") == "Yes" && xrlkeyEvent.shift)
							{
								int num = Keyboard.MetaKey - 65536;
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdWait")
								{
									Keyboard.vkCode = Keys.NumPad5;
									Keyboard.Char = 53;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveN")
								{
									Keyboard.vkCode = Keys.NumPad8;
									Keyboard.Char = 56;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveS")
								{
									Keyboard.vkCode = Keys.NumPad2;
									Keyboard.Char = 50;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveE")
								{
									Keyboard.vkCode = Keys.NumPad6;
									Keyboard.Char = 54;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveW")
								{
									Keyboard.vkCode = Keys.NumPad4;
									Keyboard.Char = 52;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveNW")
								{
									Keyboard.vkCode = Keys.NumPad7;
									Keyboard.Char = 55;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveNE")
								{
									Keyboard.vkCode = Keys.NumPad9;
									Keyboard.Char = 57;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveSW")
								{
									Keyboard.vkCode = Keys.NumPad1;
									Keyboard.Char = 49;
								}
								if (CommandBindingManager.MapKeyToCommand(num, null) == "CmdMoveSE")
								{
									Keyboard.vkCode = Keys.NumPad3;
									Keyboard.Char = 51;
								}
								if (Keyboard.vkCode == Keys.Up || Keyboard.vkCode == Keys.NumPad8 || num == 38 || num == 104)
								{
									Keyboard.MetaKey = 33;
									Keyboard.vkCode = Keys.Prior;
									xrlkeyEvent.shift = false;
								}
								if (Keyboard.vkCode == Keys.Down || Keyboard.vkCode == Keys.NumPad2 || num == 40 || num == 98)
								{
									Keyboard.MetaKey = 34;
									Keyboard.vkCode = Keys.Next;
									xrlkeyEvent.shift = false;
								}
								if (Keyboard.vkCode == Keys.Right || Keyboard.vkCode == Keys.NumPad6 || num == 39 || num == 102)
								{
									Keyboard.MetaKey = 105;
									Keyboard.vkCode = Keys.NumPad9;
									xrlkeyEvent.shift = false;
								}
								if (Keyboard.vkCode == Keys.Left || Keyboard.vkCode == Keys.NumPad4 || num == 37 || num == 100)
								{
									Keyboard.MetaKey = 103;
									Keyboard.vkCode = Keys.NumPad7;
									xrlkeyEvent.shift = false;
								}
							}
							Keyboard.MetaKey = (int)Keyboard.vkCode;
							return;
						}
					}
				}
				Keyboard.CurrentMouseEvent.CopyFrom(Keyboard.PopMouseEvent());
				if (Keyboard.CurrentMouseEvent.Event == null || !Keyboard.metaMousecommands.ContainsKey(Keyboard.CurrentMouseEvent.Event))
				{
					Keyboard.vkCode = Keys.MouseEvent;
					Keyboard.MetaKey = (int)Keyboard.vkCode;
					Keyboard.RawCode = Keyboard.vkCode;
					Keyboard.Char = (int)Keyboard.vkCode;
					return;
				}
				Keyboard.vkCode = Keyboard.metaMousecommands[Keyboard.CurrentMouseEvent.Event];
				Keyboard.MetaKey = (int)Keyboard.vkCode;
				Keyboard.RawCode = Keyboard.vkCode;
				if (Keyboard.Keymap == null)
				{
					Keyboard.InitKeymap();
				}
				KeyCode keyCode;
				if (!Keyboard.ReverseKeymap.TryGetValue(Keyboard.vkCode, out keyCode))
				{
					Keyboard.Char = 0;
					return;
				}
				char c;
				if (Keyboard.lccharmap.TryGetValue(keyCode, out c))
				{
					Keyboard.Char = (int)c;
					return;
				}
				Keyboard.Char = 0;
				return;
			}
		}

		// Token: 0x0600D3A1 RID: 54177 RVA: 0x004EA13A File Offset: 0x004E833A
		public static Keys getvk(bool MapDirectionToArrows, bool pumpActions = false, bool wait = true)
		{
			Keyboard.GetNextKey(MapDirectionToArrows, pumpActions, wait);
			return Keyboard.vkCode;
		}

		// Token: 0x0600D3A2 RID: 54178 RVA: 0x004EA14C File Offset: 0x004E834C
		public static int getchraw()
		{
			Keyboard.GetNextKey(false, false, true);
			return (int)((char)Keyboard.RawCode).ToString().ToLower()[0];
		}

		// Token: 0x0600D3A3 RID: 54179 RVA: 0x004EA17A File Offset: 0x004E837A
		public static int getch()
		{
			Keyboard.GetNextKey(false, false, true);
			return Keyboard.Char;
		}

		// Token: 0x0600D3A4 RID: 54180 RVA: 0x004EA189 File Offset: 0x004E8389
		public static int getmeta(bool MapDirectionToArrows)
		{
			Keyboard.GetNextKey(MapDirectionToArrows, false, true);
			return Keyboard.MetaKey;
		}

		// Token: 0x0600D3A5 RID: 54181 RVA: 0x004EA198 File Offset: 0x004E8398
		public static string MetaToString(int MetaCode)
		{
			string text = "";
			if ((MetaCode & 131072) != 0)
			{
				text += "Ctrl-";
			}
			if ((MetaCode & 262144) != 0)
			{
				text += "Alt-";
			}
			if ((MetaCode & 65536) != 0)
			{
				text += "Shift-";
			}
			MetaCode &= 65535;
			string text2 = text;
			Keys keys = (Keys)MetaCode;
			return text2 + keys.ToString();
		}

		// Token: 0x0600D3A6 RID: 54182 RVA: 0x004EA20C File Offset: 0x004E840C
		public static void MetaToString(int MetaCode, StringBuilder SB)
		{
			if ((MetaCode & 262144) != 0)
			{
				SB.Append("Alt+");
			}
			if ((MetaCode & 131072) != 0)
			{
				SB.Append("Ctrl+");
			}
			if ((MetaCode & 65536) != 0)
			{
				SB.Append("Shift+");
			}
			MetaCode &= 65535;
			if (!Keyboard.metaToString.ContainsKey(MetaCode))
			{
				Dictionary<int, string> dictionary = Keyboard.metaToString;
				int num = MetaCode;
				Keys keys = (Keys)MetaCode;
				dictionary.Add(num, keys.ToString());
			}
			SB.Append(Keyboard.metaToString[MetaCode]);
		}

		// Token: 0x0600D3A7 RID: 54183 RVA: 0x004EA29C File Offset: 0x004E849C
		public static string MetaToStringWithLower(int MetaCode)
		{
			bool flag = false;
			string text = "";
			if ((MetaCode & 131072) != 0)
			{
				text += "Ctrl-";
				flag = true;
			}
			if ((MetaCode & 262144) != 0)
			{
				text += "Alt-";
				flag = true;
			}
			if ((MetaCode & 65536) != 0)
			{
				text += "Shift-";
				flag = true;
			}
			MetaCode &= 65535;
			Keys keys = (Keys)MetaCode;
			string text2 = keys.ToString();
			if (text2 != null)
			{
				if (!flag && text2.Length == 1 && text2[0] >= 'A' && text2[0] >= 'Z')
				{
					text += text2.ToLower();
				}
				else
				{
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x04005415 RID: 21525
		public static CleanQueue<Keyboard.XRLKeyEvent> KeyQueue = new CleanQueue<Keyboard.XRLKeyEvent>();

		// Token: 0x04005416 RID: 21526
		public static ManualResetEvent KeyEvent = new ManualResetEvent(false);

		// Token: 0x04005417 RID: 21527
		public static bool Closed = false;

		// Token: 0x04005418 RID: 21528
		public static Dictionary<Keys, KeyCode> ReverseKeymap = null;

		// Token: 0x04005419 RID: 21529
		public static Dictionary<KeyCode, Keys> Keymap = null;

		// Token: 0x0400541A RID: 21530
		public static Dictionary<KeyCode, char> lccharmap = null;

		// Token: 0x0400541B RID: 21531
		public static Dictionary<KeyCode, char> uccharmap = null;

		// Token: 0x0400541C RID: 21532
		public static Dictionary<char, List<KeyCode>> reverselccharmap = null;

		// Token: 0x0400541D RID: 21533
		public static Dictionary<char, List<KeyCode>> reverseuccharmap = null;

		// Token: 0x0400541E RID: 21534
		private static Dictionary<string, KeyCode> parseUnityEngineKeyCodeCache = new Dictionary<string, KeyCode>();

		// Token: 0x0400541F RID: 21535
		public static readonly string COMMAND_EVENT_PREFIX = "Command:";

		// Token: 0x04005420 RID: 21536
		private static Queue<Keyboard.MouseEvent> MouseEventQueue = new Queue<Keyboard.MouseEvent>();

		// Token: 0x04005421 RID: 21537
		private static Queue<Keyboard.MouseEvent> MouseEventUsed = new Queue<Keyboard.MouseEvent>();

		// Token: 0x04005422 RID: 21538
		private static Queue<Keyboard.MouseEvent> MouseEventPool = new Queue<Keyboard.MouseEvent>();

		// Token: 0x04005423 RID: 21539
		public static Queue<Keyboard.MouseEvent> tempMouseEventQueue = new Queue<Keyboard.MouseEvent>();

		// Token: 0x04005424 RID: 21540
		public static bool _bCtrl;

		// Token: 0x04005425 RID: 21541
		public static bool _bShift;

		// Token: 0x04005426 RID: 21542
		public static bool _bAlt;

		// Token: 0x04005427 RID: 21543
		public static Keys vkCode = Keys.None;

		// Token: 0x04005428 RID: 21544
		public static Keys RawCode = Keys.None;

		// Token: 0x04005429 RID: 21545
		public static int Char = 0;

		// Token: 0x0400542A RID: 21546
		public static int MetaKey = 0;

		// Token: 0x0400542B RID: 21547
		public static int nRepeat = 0;

		// Token: 0x0400542C RID: 21548
		public static Keyboard.MouseEvent CurrentMouseEvent = new Keyboard.MouseEvent("", 0, 0);

		// Token: 0x0400542D RID: 21549
		public static Dictionary<string, Keys> metaMousecommands = new Dictionary<string, Keys>
		{
			{
				"Meta:NavigateNorth",
				Keys.NumPad8
			},
			{
				"Meta:NavigateN",
				Keys.NumPad8
			},
			{
				"Meta:NavigateSouth",
				Keys.NumPad2
			},
			{
				"Meta:NavigateS",
				Keys.NumPad2
			},
			{
				"Meta:NavigateEast",
				Keys.NumPad6
			},
			{
				"Meta:NavigateE",
				Keys.NumPad6
			},
			{
				"Meta:NavigateWest",
				Keys.NumPad4
			},
			{
				"Meta:NavigateW",
				Keys.NumPad4
			},
			{
				"Meta:NavigateNorthwest",
				Keys.NumPad7
			},
			{
				"Meta:NavigateNW",
				Keys.NumPad7
			},
			{
				"Meta:NavigateNortheast",
				Keys.NumPad9
			},
			{
				"Meta:NavigateNE",
				Keys.NumPad9
			},
			{
				"Meta:NavigateSouthwest",
				Keys.NumPad1
			},
			{
				"Meta:NavigateSW",
				Keys.NumPad1
			},
			{
				"Meta:NavigateSoutheast",
				Keys.NumPad3
			},
			{
				"Command:Page Left",
				Keys.NumPad7
			},
			{
				"Command:Page Right",
				Keys.NumPad9
			},
			{
				"Meta:NavigateSE",
				Keys.NumPad3
			},
			{
				"Meta:NavigateUp",
				Keys.Subtract
			},
			{
				"Meta:NavigateDown",
				Keys.Add
			},
			{
				"Meta:CharacterSheet",
				Keys.Tab
			},
			{
				"Meta:Walk",
				Keys.W
			},
			{
				"Meta:Abilities",
				Keys.A
			},
			{
				"Meta:Fire",
				Keys.F
			},
			{
				"Meta:Use",
				Keys.Space
			},
			{
				"Meta:Throw",
				Keys.T
			},
			{
				"Meta:Get",
				Keys.G
			},
			{
				"Meta:Autoexplore",
				Keys.NumPad0
			},
			{
				"Meta:Wait",
				Keys.NumPad5
			},
			{
				"Meta:Rest",
				Keys.Oemtilde
			},
			{
				"Meta:Accept",
				Keys.Enter
			},
			{
				"Meta:Cancel",
				Keys.Escape
			},
			{
				"Meta:System",
				Keys.Escape
			}
		};

		// Token: 0x0400542E RID: 21550
		private static Dictionary<int, string> metaToString = new Dictionary<int, string>();

		// Token: 0x02001E3B RID: 7739
		public class XRLKeyEvent
		{
			// Token: 0x0600F6E7 RID: 63207 RVA: 0x0055ABAC File Offset: 0x00558DAC
			public XRLKeyEvent(Event ke)
			{
				this.keyCode = ke.keyCode;
				this.character = Keyboard.MapKeyToChar(ke);
				this.shift = ke.shift || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);
				this.control = ke.control || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightMeta) || Input.GetKey(KeyCode.LeftMeta);
				this.alt = ke.alt;
			}

			// Token: 0x0600F6E8 RID: 63208 RVA: 0x0055AC56 File Offset: 0x00558E56
			public XRLKeyEvent(KeyCode code, char c = '\0', bool bShift = false, bool bControl = false, bool bAlt = false)
			{
				this.keyCode = code;
				this.character = c;
				this.shift = bShift;
				this.control = bControl;
				this.alt = bAlt;
			}

			// Token: 0x0600F6E9 RID: 63209 RVA: 0x0055AC8C File Offset: 0x00558E8C
			public XRLKeyEvent(Keyboard.XRLKeyEvent ke)
			{
				this.keyCode = ke.keyCode;
				this.character = ke.character;
				this.shift = ke.shift;
				this.control = ke.control;
				this.alt = ke.alt;
			}

			// Token: 0x0600F6EA RID: 63210 RVA: 0x0055ACE2 File Offset: 0x00558EE2
			public XRLKeyEvent()
			{
				this.keyCode = KeyCode.None;
				this.character = '\0';
				this.shift = false;
				this.control = false;
				this.alt = false;
			}

			// Token: 0x0600F6EB RID: 63211 RVA: 0x0055AD14 File Offset: 0x00558F14
			public override string ToString()
			{
				return (this.alt ? "Alt+" : "") + (this.control ? "Control+" : "") + (this.shift ? "Shift+" : "") + this.keyCode.ToString();
			}

			// Token: 0x0400761E RID: 30238
			public KeyCode keyCode;

			// Token: 0x0400761F RID: 30239
			public char character;

			// Token: 0x04007620 RID: 30240
			public bool shift;

			// Token: 0x04007621 RID: 30241
			public bool control;

			// Token: 0x04007622 RID: 30242
			public bool alt;

			// Token: 0x04007623 RID: 30243
			public bool allowmap = true;
		}

		// Token: 0x02001E3C RID: 7740
		public class MouseEvent
		{
			// Token: 0x0600F6EC RID: 63212 RVA: 0x0055AD73 File Offset: 0x00558F73
			public MouseEvent(string e, int x, int y, object data)
			{
				this.Event = e ?? "";
				this.x = x;
				this.y = y;
				this.Data = data;
			}

			// Token: 0x0600F6ED RID: 63213 RVA: 0x0055ADA1 File Offset: 0x00558FA1
			public MouseEvent(string e)
				: this(e, 0, 0)
			{
			}

			// Token: 0x0600F6EE RID: 63214 RVA: 0x0055ADAC File Offset: 0x00558FAC
			public MouseEvent(string e, int x, int y)
				: this(e, x, y, null)
			{
			}

			// Token: 0x0600F6EF RID: 63215 RVA: 0x0055ADB8 File Offset: 0x00558FB8
			public void CopyFrom(Keyboard.MouseEvent source)
			{
				this.Data = ((source != null) ? source.Data : null);
				this.Event = ((source != null) ? source.Event : null) ?? "";
				this.x = ((source != null) ? source.x : int.MinValue);
				this.y = ((source != null) ? source.y : int.MinValue);
			}

			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x0600F6F0 RID: 63216 RVA: 0x0055AE1E File Offset: 0x0055901E
			public Vector2 Vector2
			{
				get
				{
					return new Vector2((float)this.x, (float)this.y);
				}
			}

			// Token: 0x04007624 RID: 30244
			public object Data;

			// Token: 0x04007625 RID: 30245
			public string Event;

			// Token: 0x04007626 RID: 30246
			public int x;

			// Token: 0x04007627 RID: 30247
			public int y;
		}
	}
}
