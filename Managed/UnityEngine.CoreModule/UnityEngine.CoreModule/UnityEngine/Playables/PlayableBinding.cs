using System;
using System.ComponentModel;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x02000451 RID: 1105
	public struct PlayableBinding
	{
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000516B0 File Offset: 0x0004F8B0
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x000516C8 File Offset: 0x0004F8C8
		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
			set
			{
				this.m_StreamName = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000516D4 File Offset: 0x0004F8D4
		// (set) Token: 0x06002A60 RID: 10848 RVA: 0x000516EC File Offset: 0x0004F8EC
		public Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
			set
			{
				this.m_SourceObject = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000516F8 File Offset: 0x0004F8F8
		public Type outputTargetType
		{
			get
			{
				return this.m_SourceBindingType;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x00051710 File Offset: 0x0004F910
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("sourceBindingType is no longer supported on PlayableBinding. Use outputBindingType instead to get the required output target type, and the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) to create PlayableBindings", true)]
		public Type sourceBindingType
		{
			get
			{
				return this.m_SourceBindingType;
			}
			set
			{
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x00051728 File Offset: 0x0004F928
		// (set) Token: 0x06002A65 RID: 10853 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("streamType is no longer supported on PlayableBinding. Use the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DataStreamType streamType
		{
			get
			{
				return DataStreamType.None;
			}
			set
			{
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0005173C File Offset: 0x0004F93C
		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				playableOutput = PlayableOutput.Null;
			}
			return playableOutput;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x00051778 File Offset: 0x0004F978
		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}

		// Token: 0x04000C21 RID: 3105
		private string m_StreamName;

		// Token: 0x04000C22 RID: 3106
		private Object m_SourceObject;

		// Token: 0x04000C23 RID: 3107
		private Type m_SourceBindingType;

		// Token: 0x04000C24 RID: 3108
		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		// Token: 0x04000C25 RID: 3109
		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		// Token: 0x04000C26 RID: 3110
		public static readonly double DefaultDuration = double.PositiveInfinity;

		// Token: 0x02000452 RID: 1106
		// (Invoke) Token: 0x06002A6A RID: 10858
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);
	}
}
