using System;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000A4 RID: 164
public class TileAreaHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IMoveHandler, IPointerExitHandler, IScrollHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	// Token: 0x06000574 RID: 1396 RVA: 0x000239F6 File Offset: 0x00021BF6
	public void OnMove(AxisEventData eventData)
	{
		this.hoverTimer = 0f;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00023A04 File Offset: 0x00021C04
	public void StartHover(Vector2 position)
	{
		if (!GameManager.Instance.MouseInput)
		{
			return;
		}
		foreach (RaycastHit raycastHit in Physics.RaycastAll(GameManager.MainCamera.GetComponent<Camera>().ScreenPointToRay(position)))
		{
			TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
			if (component != null)
			{
				GameManager.Instance.TileHover(component.x, component.y);
			}
		}
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00023A80 File Offset: 0x00021C80
	public void EndHover()
	{
		this.hoverTimer = 0f;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00023A90 File Offset: 0x00021C90
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			return;
		}
		if (!GameManager.Instance.MouseInput)
		{
			return;
		}
		if (GameManager.MainCamera == null)
		{
			return;
		}
		foreach (RaycastHit raycastHit in Physics.RaycastAll(GameManager.MainCamera.GetComponent<Camera>().ScreenPointToRay(eventData.position)))
		{
			TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
			if (component != null)
			{
				if (eventData.button == PointerEventData.InputButton.Left)
				{
					if (!Input.GetKey(global::UnityEngine.KeyCode.LeftControl) && !Input.GetKey(global::UnityEngine.KeyCode.RightControl))
					{
						GameManager.Instance.OnTileClicked("LeftClick", component.x, component.y);
					}
				}
				else
				{
					GameManager.Instance.OnTileClicked("RightClick", component.x, component.y);
				}
			}
			BorderTileBehavior component2 = raycastHit.collider.gameObject.GetComponent<BorderTileBehavior>();
			if (component2 != null)
			{
				Keyboard.PushMouseEvent(string.Format("CmdMoveToBorder:{0},{1}", component2.x, component2.y), null);
			}
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00023BB5 File Offset: 0x00021DB5
	public void OnScroll(PointerEventData eventData)
	{
		GameManager.Instance.OnScroll(eventData.scrollDelta);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00023BC7 File Offset: 0x00021DC7
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.PointerInside = true;
		this.hoverTimer = 0f;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00023BDB File Offset: 0x00021DDB
	public void OnPointerExit(PointerEventData eventData)
	{
		this.PointerInside = false;
		this.hoverTimer = 0f;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00023BEF File Offset: 0x00021DEF
	private void Start()
	{
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00023BF4 File Offset: 0x00021DF4
	private void Update()
	{
		if (!GameManager.Instance.MouseInput)
		{
			return;
		}
		if (GameManager.MainCamera == null)
		{
			return;
		}
		if (this.mainCamera == null)
		{
			this.mainCamera = GameManager.MainCamera.GetComponent<Camera>();
		}
		if (this.mainCamera == null)
		{
			return;
		}
		if (CursorManager.instance.cursorHidden)
		{
			return;
		}
		if (this.PointerInside)
		{
			bool flag;
			if (this.lastMousePosition == Input.mousePosition)
			{
				this.hoverTimer += Time.deltaTime;
				flag = false;
			}
			else
			{
				this.EndHover();
				flag = true;
			}
			this.lastMousePosition = Input.mousePosition;
			foreach (RaycastHit raycastHit in Physics.RaycastAll(this.mainCamera.ScreenPointToRay(Input.mousePosition)))
			{
				TileBehavior component = raycastHit.collider.gameObject.GetComponent<TileBehavior>();
				if (component != null && flag)
				{
					GameManager.Instance.OnTileOver(component.x, component.y);
				}
				if (component != null && this.skipRelease <= 0)
				{
					if (GameManager.Instance.CurrentGameView == "Stage")
					{
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseContextAction"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseContextAction", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseInteract"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseInteract", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseInteractAll"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseInteractAll", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseLook"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseLook", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseForceAttack"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseForceAttack", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseForceMove"))
						{
							GameManager.Instance.OnTileClicked("AdventureMouseForceMove", component.x, component.y);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureMouseQuickLook"))
						{
							GameManager.Instance.ShowTooltipForTile(component.x, component.y, "rightclick");
							return;
						}
					}
					else
					{
						if (ControlManager.GetButtonReleasedThisFrame("AdventureNavMouseLeftClick"))
						{
							Keyboard.PushMouseEvent("LeftClick", component.x, component.y, null);
						}
						if (ControlManager.GetButtonReleasedThisFrame("AdventureNavMouseRightClick"))
						{
							Keyboard.PushMouseEvent("RightClick", component.x, component.y, null);
						}
					}
				}
			}
			if (this.hoverTimer > this.hoverDelay)
			{
				this.StartHover(Input.mousePosition);
				this.hoverTimer = 0f;
			}
		}
		if (!this.dragging && this.skipRelease > 0)
		{
			this.skipRelease--;
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00023EEB File Offset: 0x000220EB
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.skipRelease = 1;
		this.dragging = true;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00023EFB File Offset: 0x000220FB
	public void OnEndDrag(PointerEventData eventData)
	{
		GameManager.Instance.OnEndDrag(eventData);
		this.dragging = false;
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00023F0F File Offset: 0x0002210F
	public void OnDrag(PointerEventData eventData)
	{
		GameManager.Instance.OnDrag(eventData.delta);
	}

	// Token: 0x04000434 RID: 1076
	public bool PointerInside;

	// Token: 0x04000435 RID: 1077
	public float hoverTimer;

	// Token: 0x04000436 RID: 1078
	public float hoverDelay = 0.5f;

	// Token: 0x04000437 RID: 1079
	private int skipRelease;

	// Token: 0x04000438 RID: 1080
	private Camera mainCamera;

	// Token: 0x04000439 RID: 1081
	private Vector3 lastMousePosition = Vector3.zero;

	// Token: 0x0400043A RID: 1082
	private bool dragging = true;
}
