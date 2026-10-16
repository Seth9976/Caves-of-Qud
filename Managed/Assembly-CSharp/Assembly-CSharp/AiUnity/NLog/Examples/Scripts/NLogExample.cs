using System;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core;
using UnityEngine;

namespace AiUnity.NLog.Examples.Scripts
{
	// Token: 0x02000208 RID: 520
	[AddComponentMenu("AiUnity/NLog/Examples/NLogExample")]
	public class NLogExample : MonoBehaviour
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x0006E724 File Offset: 0x0006C924
		private void Awake()
		{
			Debug.Log("Standard Unity Debug log message, called from NLogExample Awake() method.\nPlease configure NLog GUI with the desired rule, target, and message level.");
			this.logger = Singleton<NLogManager>.Instance.GetLogger(this, null);
			this.logger.Info("Testing a NLog <i>Info</i> message from {0} Awake() method.", new object[] { base.GetType().Name });
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0006E774 File Offset: 0x0006C974
		private void Start()
		{
			this.logger.Assert(false, "Testing a NLog <i>Assert</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Fatal("Testing a NLog <i>Fatal</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Error("Testing a NLog <i>Error</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Warn("Testing a NLog <i>Warn</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Info("Testing a NLog <i>Info</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Debug("Testing a NLog <i>Debug</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			this.logger.Trace("Testing a NLog <i>Trace</i> message from {0} Start() method.", new object[] { base.GetType().Name });
			int num = 1;
			this.logger.Assert(num == 0, "Assertion fired because assertCondition={0}.", new object[] { num });
			this.logger.Info(this.gameObjectContext, "Test NLog message with an explicit gameObject context", Array.Empty<object>());
			try
			{
				throw new Exception("Test Exception");
			}
			catch (Exception ex)
			{
			}
			Exception ex;
			this.logger.Info(ex, "Test NLog message with an exception", Array.Empty<object>());
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		private void Update()
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0006E8F8 File Offset: 0x0006CAF8
		private void OnGUI()
		{
			GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label("If messages missing on expected destination please configure NLog GUI with desired LogLevel (i.e. Everything) and target (i.e. UnityConsole/GameConsole).", Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}

		// Token: 0x04000C88 RID: 3208
		private NLogger logger;

		// Token: 0x04000C89 RID: 3209
		public GameObject gameObjectContext;
	}
}
