using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Qud.UI;

namespace XRL.UI
{
	// Token: 0x020014CF RID: 5327
	public class Progress
	{
		// Token: 0x0600D0CC RID: 53452 RVA: 0x004D6C3F File Offset: 0x004D4E3F
		public void setCurrentProgressText(string text)
		{
			this.currentProgressText = text;
			this.draw();
		}

		// Token: 0x0600D0CD RID: 53453 RVA: 0x004D6C4E File Offset: 0x004D4E4E
		public void setCurrentProgress(int pos)
		{
			this.currentProgress = pos;
			this.draw();
		}

		// Token: 0x0600D0CE RID: 53454 RVA: 0x004D6C5D File Offset: 0x004D4E5D
		public bool isCancelled()
		{
			return false;
		}

		// Token: 0x0600D0CF RID: 53455 RVA: 0x004D6C60 File Offset: 0x004D4E60
		public void draw()
		{
			if (Options.ModernUI)
			{
				SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync(this.currentProgressText);
				return;
			}
			this.buffer.Copy(this.original);
			string text = this.currentProgressText;
			int num = 15;
			int num2 = 65;
			int num3 = 10;
			int num4 = 18;
			this.buffer.Fill(num, num3, num2, num4, 32, ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
			this.buffer.SingleBox(num, num3, num2, num4, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
			this.buffer.Goto(num + 5, num3 + 3);
			this.buffer.Write(text, true, false, false, null, -1);
			ScrollbarHelper.Paint(this.buffer, num3 + 5, num + 5, num2 - num - 10, ScrollbarHelper.Orientation.Horizontal, 0, 100, 0, this.currentProgress, null);
			this.buffer.Draw();
		}

		// Token: 0x0600D0D0 RID: 53456 RVA: 0x004D6D34 File Offset: 0x004D4F34
		public void start(Action<Progress> a)
		{
			if (Options.ModernUI)
			{
				this.original.Copy(TextConsole.CurrentBuffer);
				this.buffer = TextConsole.GetScrapBuffer1(false);
			}
			GameManager.Instance.PushGameView("Popup:Progress", true);
			a(this);
			if (Options.ModernUI)
			{
				SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync(null);
			}
			else
			{
				this.original.Draw();
			}
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x0600D0D1 RID: 53457 RVA: 0x004D6DA8 File Offset: 0x004D4FA8
		public Task<T> startAsync<T>(Func<Progress, Task<T>> a)
		{
			Progress.<startAsync>d__9<T> <startAsync>d__;
			<startAsync>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<startAsync>d__.<>4__this = this;
			<startAsync>d__.a = a;
			<startAsync>d__.<>1__state = -1;
			<startAsync>d__.<>t__builder.Start<Progress.<startAsync>d__9<T>>(ref <startAsync>d__);
			return <startAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04005257 RID: 21079
		private string currentProgressText = "initializing...";

		// Token: 0x04005258 RID: 21080
		private int currentProgress;

		// Token: 0x04005259 RID: 21081
		private ScreenBuffer original = ScreenBuffer.create(80, 25);

		// Token: 0x0400525A RID: 21082
		private ScreenBuffer buffer;
	}
}
