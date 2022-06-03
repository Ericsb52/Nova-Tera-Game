using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionManager : MonoBehaviour
{

    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;


    private GameObject curInteractGameObject;
    private IInteractable curinteractable;

    public TextMeshProUGUI promptText;
    private Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, maxCheckDistance, layerMask))
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curinteractable = hit.collider.GetComponent<IInteractable>();
                    setPromptText();
                }
            }
            else
            {
                curinteractable = null;
                curInteractGameObject = null;
                promptText.gameObject.SetActive(false);

            }




        }
    }

    void setPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]<b>  {0}", curinteractable.getInteractPrompt());


    }

    public void onInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curinteractable != null)
        {
            curinteractable.onInteract();
            curInteractGameObject = null;
            curinteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}


