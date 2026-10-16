using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003D RID: 61
	public class TimeControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static ScriptPlayable<TimeControlPlayable> Create(PlayableGraph graph, ITimeControl timeControl)
		{
			if (timeControl == null)
			{
				return ScriptPlayable<TimeControlPlayable>.Null;
			}
			ScriptPlayable<TimeControlPlayable> scriptPlayable = ScriptPlayable<TimeControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(timeControl);
			return scriptPlayable;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000990C File Offset: 0x00007B0C
		public void Initialize(ITimeControl timeControl)
		{
			this.m_timeControl = timeControl;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009915 File Offset: 0x00007B15
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.m_timeControl != null)
			{
				this.m_timeControl.SetTime(playable.GetTime<Playable>());
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009930 File Offset: 0x00007B30
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (!this.m_started)
			{
				this.m_timeControl.OnControlTimeStart();
				this.m_started = true;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009955 File Offset: 0x00007B55
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (this.m_started)
			{
				this.m_timeControl.OnControlTimeStop();
				this.m_started = false;
			}
		}

		// Token: 0x040000E9 RID: 233
		private ITimeControl m_timeControl;

		// Token: 0x040000EA RID: 234
		private bool m_started;
	}
}
