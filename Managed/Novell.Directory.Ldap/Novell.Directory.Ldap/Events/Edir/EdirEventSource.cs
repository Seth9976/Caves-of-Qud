using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B6 RID: 182
	public class EdirEventSource : LdapEventSource
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600046C RID: 1132 RVA: 0x000148A4 File Offset: 0x00012AA4
		// (remove) Token: 0x0600046D RID: 1133 RVA: 0x000148C3 File Offset: 0x00012AC3
		public event EdirEventSource.EdirEventHandler EdirEvent
		{
			add
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Combine(this.edir_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Remove(this.edir_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000148E4 File Offset: 0x00012AE4
		protected override int GetListeners()
		{
			int num = 0;
			if (this.edir_event != null)
			{
				num = this.edir_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001490A File Offset: 0x00012B0A
		public EdirEventSource(EdirEventSpecifier[] specifier, LdapConnection conn)
		{
			if (specifier == null || conn == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mRequestOperation = new MonitorEventRequest(specifier);
			this.mConnection = conn;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014938 File Offset: 0x00012B38
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.ExtendedOperation(this.mRequestOperation, null, null);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014993 File Offset: 0x00012B93
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000149AC File Offset: 0x00012BAC
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			if (this.edir_event != null && sourceMessage != null && sourceMessage.Type == 25 && sourceMessage is EdirEventIntermediateResponse)
			{
				this.edir_event(this, new EdirEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_EDIR_EVENT));
				flag = true;
			}
			return flag;
		}

		// Token: 0x04000425 RID: 1061
		protected EdirEventSource.EdirEventHandler edir_event;

		// Token: 0x04000426 RID: 1062
		protected LdapConnection mConnection;

		// Token: 0x04000427 RID: 1063
		protected MonitorEventRequest mRequestOperation;

		// Token: 0x04000428 RID: 1064
		protected LdapResponseQueue mQueue;

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000652 RID: 1618
		public delegate void EdirEventHandler(object source, EdirEventArgs objEdirEventArgs);
	}
}
