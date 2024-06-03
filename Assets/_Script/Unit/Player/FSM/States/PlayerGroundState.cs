using System;
using UnityEngine;

public class PlayerGroundState : IState
{
    //ref
    protected PlayerUnitSO playerData => playerStateMachine.player.data;
    protected Rigidbody rigidbody => playerStateMachine.player.rb;
    protected InputManager input => Singleton<InputManager>.Instance;

    //cache
    protected ItemInstance selectedItem;

    //events
    public event Action<ItemChangedArgs> OnSelectedItemChanged;
    public class ItemChangedArgs : EventArgs
    {
        public ItemInstance item;

        public ItemChangedArgs(ItemInstance item)
        {
            this.item = item;
        }
    }

    protected PlayerStateMachine playerStateMachine;
    public PlayerGroundState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void HandleInput()
    {
        playerStateMachine.shouldRun = input.locomotionToggleInput;


    }

    public virtual void OnUpdate()
    {
        CheckForItem();
    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

    #region Transitions

    #endregion

    #region ReusableMethods
    private void CheckForItem()
    {
        if (Physics.Raycast(playerStateMachine.player.transform.position, playerStateMachine.player.transform.forward, out RaycastHit hitInfo, playerData.interactionDistance, playerData.interactLayer))
        {
            if (hitInfo.collider.TryGetComponent(out ItemInstance selectedItem))
            {
                this.selectedItem = selectedItem;
            }
            else
            {
                this.selectedItem = null;
            }
        }
        else
        {
            this.selectedItem = null;
        }

        OnSelectedItemChanged?.Invoke(new ItemChangedArgs(selectedItem));
    }

    #endregion


}
