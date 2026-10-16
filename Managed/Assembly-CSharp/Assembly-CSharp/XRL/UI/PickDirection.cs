using System;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using XRL.Core;
using XRL.Rules;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014A8 RID: 5288
	[UIView("PickDirection", true, false, false, "Adventure", "PickTargetFrame", false, 1, false)]
	public class PickDirection : IWantsTextConsoleInit
	{
		// Token: 0x0600CFA1 RID: 53153 RVA: 0x004C3969 File Offset: 0x004C1B69
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			PickDirection._TextConsole = TextConsole_;
			PickDirection._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0600CFA2 RID: 53154 RVA: 0x004C3978 File Offset: 0x004C1B78
		public static string ShowPicker(string Label = null)
		{
			PickTargetWindow.currentMode = PickTargetWindow.TargetMode.PickDirection;
			GameManager.Instance.PushGameView("PickTarget", true);
			PickDirection.OldBuffer.Copy(TextConsole.CurrentBuffer);
			PickDirection.Buffer.Copy(TextConsole.CurrentBuffer);
			bool physics = The.Player.Physics != null;
			bool flag = false;
			if (physics)
			{
				while (!flag)
				{
					Event.ResetPool(false);
					XRLCore.Core.RenderMapToBuffer(PickDirection.Buffer);
					XRLCore.ParticleManager.Frame();
					XRLCore.ParticleManager.Render(PickDirection.Buffer);
					if (Options.ModernUI)
					{
						PickTargetWindow.currentText = (string.IsNullOrEmpty(Label) ? "" : (Label + " | ")) + "[Select a direction]";
					}
					else
					{
						PickDirection.Buffer.Goto(2, 0);
						PickDirection.Buffer.Write((string.IsNullOrEmpty(Label) ? "" : (Label + " ")) + "[Select a direction]", true, false, false, null, -1);
					}
					PickDirection._TextConsole.DrawBuffer(PickDirection.Buffer, null, false);
					Keys keys = Keyboard.getvk(true, false, true);
					if (keys == Keys.NumPad5 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Meta:Navigate") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTargetSelf"))
					{
						GameManager.Instance.PopGameView(true);
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						return ".";
					}
					if (keys == Keys.Escape)
					{
						GameManager.Instance.PopGameView(true);
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						return null;
					}
					if (keys == Keys.NumPad1)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "SW";
					}
					if (keys == Keys.NumPad2)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "S";
					}
					if (keys == Keys.NumPad3)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "SE";
					}
					if (keys == Keys.NumPad4)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "W";
					}
					if (keys == Keys.NumPad6)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "E";
					}
					if (keys == Keys.NumPad7)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "NW";
					}
					if (keys == Keys.NumPad8)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "N";
					}
					if (keys == Keys.NumPad9)
					{
						PickDirection._TextConsole.DrawBuffer(PickDirection.OldBuffer, null, false);
						GameManager.Instance.PopGameView(true);
						return "NE";
					}
					if (keys == Keys.MouseEvent)
					{
						if (Keyboard.CurrentMouseEvent.Event == "RightClick")
						{
							GameManager.Instance.PopGameView(true);
							return null;
						}
						if (Keyboard.CurrentMouseEvent.Event == "LeftClick" && The.Player.Physics.CurrentCell != null)
						{
							Point2D pos2D = The.Player.Physics.CurrentCell.Pos2D;
							Point2D point2D = new Point2D(Keyboard.CurrentMouseEvent.x, Keyboard.CurrentMouseEvent.y);
							if (pos2D == point2D)
							{
								GameManager.Instance.PopGameView(true);
								return ".";
							}
							for (int i = 0; i < Directions.DirectionList.Length; i++)
							{
								if (point2D == pos2D.FromDirection(Directions.DirectionList[i]))
								{
									GameManager.Instance.PopGameView(true);
									return Directions.DirectionList[i];
								}
							}
						}
					}
				}
			}
			GameManager.Instance.PopGameView(true);
			return null;
		}

		// Token: 0x040050D0 RID: 20688
		public static TextConsole _TextConsole;

		// Token: 0x040050D1 RID: 20689
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x040050D2 RID: 20690
		public static ScreenBuffer OldBuffer = ScreenBuffer.create(80, 25);

		// Token: 0x040050D3 RID: 20691
		public static ScreenBuffer Buffer = ScreenBuffer.create(80, 25);
	}
}
