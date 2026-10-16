using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000022 RID: 34
	public class InputActionReference : ScriptableObject
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007580 File Offset: 0x00005780
		public InputActionAsset asset
		{
			get
			{
				InputAction action = this.m_Action;
				if (((action != null) ? action.m_ActionMap : null) == null)
				{
					return this.m_Asset;
				}
				return this.m_Action.m_ActionMap.asset;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000075B0 File Offset: 0x000057B0
		public InputAction action
		{
			get
			{
				if (this.m_Action != null && this.m_Action.actionMap != null && this.m_Action.actionMap.asset == this.m_Asset && this.m_Asset)
				{
					return this.m_Action;
				}
				return this.m_Action = (this.m_Asset ? this.m_Asset.FindAction(new Guid(this.m_ActionId)) : null);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007634 File Offset: 0x00005834
		public void Set(InputAction action)
		{
			if (action == null)
			{
				this.m_Asset = null;
				this.m_ActionId = null;
				this.m_Action = null;
				base.name = string.Empty;
				return;
			}
			InputActionMap actionMap = action.actionMap;
			if (actionMap == null || actionMap.asset == null)
			{
				throw new InvalidOperationException(string.Format("Action '{0}' must be part of an InputActionAsset in order to be able to create an InputActionReference for it", action));
			}
			this.SetInternal(actionMap.asset, action);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000769C File Offset: 0x0000589C
		public void Set(InputActionAsset asset, string mapName, string actionName)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(mapName))
			{
				throw new ArgumentNullException("mapName");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentNullException("actionName");
			}
			InputActionMap inputActionMap = asset.FindActionMap(mapName, false);
			if (inputActionMap == null)
			{
				throw new ArgumentException(string.Format("No action map '{0}' in '{1}'", mapName, asset), "mapName");
			}
			InputAction inputAction = inputActionMap.FindAction(actionName, false);
			if (inputAction == null)
			{
				throw new ArgumentException(string.Format("No action '{0}' in map '{1}' of asset '{2}'", actionName, mapName, asset), "actionName");
			}
			this.SetInternal(asset, inputAction);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007730 File Offset: 0x00005930
		private void SetInternal(InputActionAsset assetArg, InputAction actionArg)
		{
			this.CheckImmutableReference();
			this.m_Asset = assetArg;
			this.m_ActionId = actionArg.id.ToString();
			this.m_Action = actionArg;
			base.name = InputActionReference.GetDisplayName(actionArg);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007778 File Offset: 0x00005978
		public override string ToString()
		{
			InputAction action = this.action;
			if (action == null)
			{
				return base.ToString();
			}
			if (action.actionMap != null)
			{
				if (!(this.m_Asset != null))
				{
					return action.actionMap.name + "/" + action.name;
				}
				return string.Concat(new string[]
				{
					this.m_Asset.name,
					":",
					action.actionMap.name,
					"/",
					action.name
				});
			}
			else
			{
				if (!(this.m_Asset != null))
				{
					return this.m_ActionId;
				}
				return this.m_Asset.name + ":" + this.m_ActionId;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000783C File Offset: 0x00005A3C
		private static string GetDisplayName(InputAction action)
		{
			string text;
			if (action == null)
			{
				text = null;
			}
			else
			{
				InputActionMap actionMap = action.actionMap;
				text = ((actionMap != null) ? actionMap.name : null);
			}
			if (!string.IsNullOrEmpty(text))
			{
				InputActionMap actionMap2 = action.actionMap;
				return ((actionMap2 != null) ? actionMap2.name : null) + "/" + action.name;
			}
			if (action == null)
			{
				return null;
			}
			return action.name;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007896 File Offset: 0x00005A96
		internal string ToDisplayName()
		{
			if (!string.IsNullOrEmpty(base.name))
			{
				return base.name;
			}
			return InputActionReference.GetDisplayName(this.action);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000078B7 File Offset: 0x00005AB7
		public static implicit operator InputAction(InputActionReference reference)
		{
			if (reference == null)
			{
				return null;
			}
			return reference.action;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000078C4 File Offset: 0x00005AC4
		public static InputActionReference Create(InputAction action)
		{
			InputActionReference inputActionReference = ScriptableObject.CreateInstance<InputActionReference>();
			inputActionReference.Set(action);
			return inputActionReference;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000078D4 File Offset: 0x00005AD4
		internal static void InvalidateAll()
		{
			Object[] array = Resources.FindObjectsOfTypeAll(typeof(InputActionReference));
			for (int i = 0; i < array.Length; i++)
			{
				((InputActionReference)array[i]).Invalidate();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000790C File Offset: 0x00005B0C
		internal void Invalidate()
		{
			this.m_Action = null;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007915 File Offset: 0x00005B15
		public InputAction ToInputAction()
		{
			return this.action;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000791D File Offset: 0x00005B1D
		private void CheckImmutableReference()
		{
		}

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		internal InputActionAsset m_Asset;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		internal string m_ActionId;

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		private InputAction m_Action;
	}
}
