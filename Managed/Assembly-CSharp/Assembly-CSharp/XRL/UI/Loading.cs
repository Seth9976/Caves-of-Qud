using System;
using Qud.UI;

namespace XRL.UI
{
	// Token: 0x020014BA RID: 5306
	public static class Loading
	{
		// Token: 0x0600D04D RID: 53325 RVA: 0x004CDF4C File Offset: 0x004CC14C
		public static Loading.Status StartTask(string Message, string Description = null, bool Visible = true)
		{
			return new Loading.Status(Message, Description, Visible);
		}

		// Token: 0x0600D04E RID: 53326 RVA: 0x004CDF58 File Offset: 0x004CC158
		public static void LoadTask(string description, Action work, bool showToUser = true)
		{
			using (Loading.StartTask(description, null, showToUser))
			{
				try
				{
					work();
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Failed '" + description + "' task with exception", ex, "game_exception");
				}
			}
		}

		// Token: 0x0600D04F RID: 53327 RVA: 0x004CDFB8 File Offset: 0x004CC1B8
		public static void SetLoadingStatus(string description, bool waitForUiUpdate = false)
		{
			LoadingStatusWindow instance = SingletonWindowBase<LoadingStatusWindow>.instance;
			if (instance == null)
			{
				return;
			}
			instance.SetLoadingStatus(description, waitForUiUpdate);
		}

		// Token: 0x0600D050 RID: 53328 RVA: 0x004CDFCB File Offset: 0x004CC1CB
		public static void SetHideLoadStatus(bool hidden)
		{
			if (SingletonWindowBase<LoadingStatusWindow>.instance == null)
			{
				return;
			}
			SingletonWindowBase<LoadingStatusWindow>.instance.StayHidden = hidden;
		}

		// Token: 0x040051C2 RID: 20930
		private static CleanStack<string> LoadStatuses = new CleanStack<string>();

		// Token: 0x02001DC9 RID: 7625
		public struct Status : IDisposable
		{
			// Token: 0x0600F548 RID: 62792 RVA: 0x0055573C File Offset: 0x0055393C
			public Status(string Message, string Description = null, bool Visible = true)
			{
				if (Message == null)
				{
					throw new ArgumentNullException("Message");
				}
				this.Message = Message;
				string text;
				if ((text = Description) == null)
				{
					text = Message;
					Description = Message;
				}
				this.Description = text;
				this.Visible = Visible;
				if (Visible)
				{
					Loading.LoadStatuses.Push(Message);
					Loading.SetLoadingStatus(Message, false);
				}
				TimeSpan timeSpan = new TimeSpan(GameManager.Time.ElapsedTicks);
				MetricsManager.LogInfo(string.Format("Starting '{0}' task at {1:hh\\:mm\\:ss\\:fff}", Description, timeSpan));
				this.StartTime = (GameManager.Time.IsRunning ? GameManager.Time.ElapsedMilliseconds : (-1L));
			}

			// Token: 0x0600F549 RID: 62793 RVA: 0x005557D4 File Offset: 0x005539D4
			public void Dispose()
			{
				if (this.Message == null)
				{
					return;
				}
				if (this.StartTime != -1L)
				{
					long num = GameManager.Time.ElapsedMilliseconds - this.StartTime;
					MetricsManager.LogInfo(string.Format("Finished '{0}' task in {1}ms", this.Description, num));
				}
				if (this.Visible)
				{
					Loading.LoadStatuses.Pop();
					Loading.SetLoadingStatus(Loading.LoadStatuses.Peek(), false);
				}
				this.Message = null;
			}

			// Token: 0x04007415 RID: 29717
			public string Message;

			// Token: 0x04007416 RID: 29718
			public string Description;

			// Token: 0x04007417 RID: 29719
			public long StartTime;

			// Token: 0x04007418 RID: 29720
			public bool Visible;
		}
	}
}
