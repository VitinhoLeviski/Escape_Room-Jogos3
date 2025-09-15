using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float rayDistance = 2f;
    public float rotateSpeed = 200;
    public float rotX, rotY;
    public bool interEnter, interExit;

    public Transform objectViewer;

    public UnityEvent OnView;
    public UnityEvent OnFinishView;

    public Camera myCam;

    private bool isViewing;
    private bool canFinish;

    private Interactables currentInteractable;
    private Item currentItem;
    private Vector3 originPosition;
    private Quaternion originRotation;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        CheckInteractables();
    }

    void CheckInteractables()
    {
        if (isViewing)
        {
            if (currentInteractable.item.grabbable)
            {
                RotateObject();
            }

            if (canFinish && interExit)
            {
                FinishView();
            }

            return;
        }

        RaycastHit hit;
        Vector3 rayOrigin = myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(rayOrigin, myCam.transform.forward, out hit, rayDistance))
        {
            Interactables interactable = hit.collider.GetComponent<Interactables>();
            if (interactable != null)//se for interagivel
            {
                UIManager.instance.SetHandCursor(true);


                //UIManager.instance.SetButtonEnter(true);
                if (interEnter)//inicia a interação
                {
                    if (interactable.isMoving)
                    {
                        return;
                    }

                    currentInteractable = interactable;

                    currentInteractable.OnInteract.Invoke();

                    if (currentInteractable.item != null)
                    {
                        OnView.Invoke();

                        isViewing = true;

                        bool hasPreviousItem = false;

                        for (int i = 0; i < currentInteractable.previousItem.Length; i++)
                        {
                            if (
                                inventory.itens.Contains(
                                    currentInteractable.previousItem[i].requiredItem
                                )
                            )
                            {
                                Interact(currentInteractable.previousItem[i].interactionItem);
                                currentInteractable.previousItem[i].OnInteract.Invoke();
                                hasPreviousItem = true;
                                break;
                            }
                        }

                        if (hasPreviousItem)
                        {
                            return;
                        }

                        Interact(currentInteractable.item);

                        if (currentInteractable.item.grabbable)
                        {
                            originPosition = currentInteractable.transform.position;
                            originRotation = currentInteractable.transform.rotation;
                            StartCoroutine(
                                MovingObject(currentInteractable, objectViewer.position)
                            );
                        }
                    }
                }
            }
            else
            {
                UIManager.instance.SetHandCursor(false);
              //  UIManager.instance.SetButtonEnter(false);
              //  UIManager.instance.SetButtonExit(false);
            }
        }
        else
        {
            UIManager.instance.SetHandCursor(false);
           // UIManager.instance.SetButtonEnter(false);
           // UIManager.instance.SetButtonExit(false);
        }
    }

    void Interact(Item item)
    {
        currentItem = item;

       // UIManager.instance.SetButtonEnter(false);
        UIManager.instance.SetHandCursor(false);

        if (item.image != null)
        {
            Debug.Log("tem imagem");
            UIManager.instance.SetImage(item.image);
        }
        UIManager.instance.SetCaptions(item.text);
        Invoke("CanFinish", 2f);
    }

    void CanFinish()
    {
        canFinish = true;

        if (currentItem.image == null && !currentItem.grabbable)
        {
            FinishView();
        }
        else
        {
          //  UIManager.instance.SetButtonExit(true);
        }

        UIManager.instance.SetCaptions("");
    }

    void FinishView()
    {
        canFinish = false;
        isViewing = false;
       // UIManager.instance.SetButtonExit(false);
       // UIManager.instance.SetButtonEnter(false);
        UIManager.instance.SetHandCursor(false);

        if (currentItem.InventoryItem)
         {
             inventory.AddItem(currentItem);
             currentInteractable.CollectItem.Invoke();
         }
        if (currentItem.grabbable)
        {
            currentInteractable.transform.rotation = originRotation;
            StartCoroutine(MovingObject(currentInteractable, originPosition));
        }

        if (currentItem.requiredItem)
        {
            inventory.AddRequiredItens(currentItem);
        }

        OnFinishView.Invoke();
    }

    IEnumerator MovingObject(Interactables obj, Vector3 position)
    {
        obj.isMoving = true;
        float timer = 0;
        while (timer < 1)
        {
            obj.transform.position = Vector3.Lerp(
                obj.transform.position,
                position,
                Time.deltaTime * 5
            );
            timer += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = position;
        obj.isMoving = false;
    }

    void RotateObject()
    {
        float x = rotX;
        float y = rotY;
        currentInteractable.transform.Rotate(
            myCam.transform.right,
            -Mathf.Deg2Rad * y * rotateSpeed,
            Space.World
        );
        currentInteractable.transform.Rotate(
            myCam.transform.up,
            -Mathf.Deg2Rad * x * rotateSpeed,
            Space.World
        );
    }

    //InputActions
    public void Rotate(InputAction.CallbackContext value)
    {
        rotX = value.ReadValue<Vector2>().x;
        rotY = value.ReadValue<Vector2>().y;
    }
    public void InteractionEnter(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            interEnter = true;
        }
        else if (value.canceled)
        {
            interEnter = false;
        }
    }
    public void InteractionExit(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            interExit = true;
        }
        else if (value.canceled)
        {
            interExit = false;
        }
    }
}

