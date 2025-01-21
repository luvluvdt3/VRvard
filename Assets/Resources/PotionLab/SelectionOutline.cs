using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SelectionOutline : MonoBehaviour
{
    [FormerlySerializedAs("renderer")]
    public Renderer Renderer;
    
    float m_Highlighted = 0.0f;
    MaterialPropertyBlock m_Block;
    int m_HighlightActiveID;
    XRBaseInteractable m_Interactable;

    private void Start()
    {
        if(Renderer == null)
        {
            Renderer = GetComponent<Renderer>();
        }
        m_Interactable = GetComponent<XRBaseInteractable>();
        if (m_Interactable != null)
        {
            m_Interactable.firstHoverEntered.AddListener(OnFirstHoverEntered);
            m_Interactable.lastHoverExited.AddListener(OnLastHoverExited);
            m_Interactable.selectEntered.AddListener(OnSelectEntered);
            m_Interactable.selectExited.AddListener(OnSelectExited);
        }
        else
        {
            Debug.LogWarning("No XRBaseInteractable found on " + gameObject.name);
        }

        m_HighlightActiveID = Shader.PropertyToID("HighlightActive");
        m_Block = new MaterialPropertyBlock();
        m_Block.SetFloat(m_HighlightActiveID, m_Highlighted);
        Renderer.SetPropertyBlock(m_Block);
    }

    private void OnDestroy()
    {
        if (m_Interactable != null)
        {
            m_Interactable.firstHoverEntered.RemoveListener(OnFirstHoverEntered);
            m_Interactable.lastHoverExited.RemoveListener(OnLastHoverExited);
            m_Interactable.selectEntered.RemoveListener(OnSelectEntered);
            m_Interactable.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnFirstHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log("First Hover Entered");
        Highlight();
    }

    private void OnLastHoverExited(HoverExitEventArgs args)
    {
        Debug.Log("Last Hover Exited");
        RemoveHighlight();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Select Entered");
        Highlight();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Select Exited");
        RemoveHighlight();
    }

    public void Highlight()
    {
        m_Highlighted = 1.0f;
        Renderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat(m_HighlightActiveID, m_Highlighted);
        Renderer.SetPropertyBlock(m_Block);
    }

    public void RemoveHighlight()
    {
        m_Highlighted = 0.0f;

        Renderer.GetPropertyBlock(m_Block);
        m_Block.SetFloat(m_HighlightActiveID, m_Highlighted);
        Renderer.SetPropertyBlock(m_Block);
    }
}