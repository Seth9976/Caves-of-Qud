using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003C RID: 60
	public class PrefabControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00009708 File Offset: 0x00007908
		public static ScriptPlayable<PrefabControlPlayable> Create(PlayableGraph graph, GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				return ScriptPlayable<PrefabControlPlayable>.Null;
			}
			ScriptPlayable<PrefabControlPlayable> scriptPlayable = ScriptPlayable<PrefabControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(prefabGameObject, parentTransform);
			return scriptPlayable;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000973C File Offset: 0x0000793C
		public GameObject prefabInstance
		{
			get
			{
				return this.m_Instance;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009744 File Offset: 0x00007944
		public GameObject Initialize(GameObject prefabGameObject, Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				throw new ArgumentNullException("Prefab cannot be null");
			}
			if (this.m_Instance != null)
			{
				Debug.LogWarningFormat("Prefab Control Playable ({0}) has already been initialized with a Prefab ({1}).", new object[]
				{
					prefabGameObject.name,
					this.m_Instance.name
				});
			}
			else
			{
				this.m_Instance = Object.Instantiate<GameObject>(prefabGameObject, parentTransform, false);
				this.m_Instance.name = prefabGameObject.name + " [Timeline]";
				this.m_Instance.SetActive(false);
				PrefabControlPlayable.SetHideFlagsRecursive(this.m_Instance);
			}
			return this.m_Instance;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000097E2 File Offset: 0x000079E2
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.m_Instance)
			{
				if (Application.isPlaying)
				{
					Object.Destroy(this.m_Instance);
					return;
				}
				Object.DestroyImmediate(this.m_Instance);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000980F File Offset: 0x00007A0F
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_Instance == null)
			{
				return;
			}
			this.m_Instance.SetActive(true);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000982C File Offset: 0x00007A2C
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_Instance != null && info.effectivePlayState == PlayState.Paused)
			{
				this.m_Instance.SetActive(false);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009854 File Offset: 0x00007A54
		private static void SetHideFlagsRecursive(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}
			gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
			if (!Application.isPlaying)
			{
				gameObject.hideFlags |= HideFlags.HideInHierarchy;
			}
			foreach (object obj in gameObject.transform)
			{
				PrefabControlPlayable.SetHideFlagsRecursive(((Transform)obj).gameObject);
			}
		}

		// Token: 0x040000E8 RID: 232
		private GameObject m_Instance;
	}
}
