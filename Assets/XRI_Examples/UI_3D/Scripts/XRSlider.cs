using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// An interactable that follows the position of the interactor on a single axis with optional stepped values
    /// </summary>
    public class XRSlider : XRBaseInteractable
    {
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [SerializeField]
        [Tooltip("The object that is visually grabbed and manipulated")]
        Transform m_Handle = null;

        [SerializeField]
        [Tooltip("The value of the slider")]
        [Range(0.0f, 1.0f)]
        float m_Value = 0.5f;

        [SerializeField]
        [Tooltip("The offset of the slider at value '1'")]
        float m_MaxPosition = 0.5f;

        [SerializeField]
        [Tooltip("The offset of the slider at value '0'")]
        float m_MinPosition = -0.5f;

        [SerializeField]
        [Tooltip("Number of steps for the slider (0 for continuous)")]
        int m_Steps = 0;

        [SerializeField]
        [Tooltip("Events to trigger when the slider is moved")]
        ValueChangeEvent m_OnValueChange = new ValueChangeEvent();

        IXRSelectInteractor m_Interactor;

        /// <summary>
        /// The value of the slider
        /// </summary>
        public float value
        {
            get => m_Value;
            set
            {
                SetValue(value);
                SetSliderPosition(value);
            }
        }

        /// <summary>
        /// Number of discrete steps for the slider (0 for continuous movement)
        /// </summary>
        public int steps
        {
            get => m_Steps;
            set => m_Steps = Mathf.Max(0, value);
        }

        /// <summary>
        /// Events to trigger when the slider is moved
        /// </summary>
        public ValueChangeEvent onValueChange => m_OnValueChange;

        void Start()
        {
            SetValue(m_Value);
            SetSliderPosition(m_Value);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(StartGrab);
            selectExited.AddListener(EndGrab);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(StartGrab);
            selectExited.RemoveListener(EndGrab);
            base.OnDisable();
        }

        void StartGrab(SelectEnterEventArgs args)
        {
            m_Interactor = args.interactorObject;
            UpdateSliderPosition();
        }

        void EndGrab(SelectExitEventArgs args)
        {
            m_Interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    UpdateSliderPosition();
                }
            }
        }

        void UpdateSliderPosition()
        {
            // Put anchor position into slider space
            var localPosition = transform.InverseTransformPoint(m_Interactor.GetAttachTransform(this).position);
            var rawValue = Mathf.Clamp01((localPosition.z - m_MinPosition) / (m_MaxPosition - m_MinPosition));
            
            // Apply stepping if steps are defined
            float steppedValue = rawValue;
            if (m_Steps > 0)
            {
                float stepSize = 1f / m_Steps;
                steppedValue = Mathf.Round(rawValue / stepSize) * stepSize;
            }

            SetValue(steppedValue);
            SetSliderPosition(steppedValue);
        }

        void SetSliderPosition(float value)
        {
            if (m_Handle == null)
                return;

            var handlePos = m_Handle.localPosition;
            handlePos.z = Mathf.Lerp(m_MinPosition, m_MaxPosition, value);
            m_Handle.localPosition = handlePos;
        }

        void SetValue(float value)
        {
            m_Value = value;
            m_OnValueChange.Invoke(m_Value);
        }

        void OnDrawGizmosSelected()
        {
            var sliderMinPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, m_MinPosition));
            var sliderMaxPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, m_MaxPosition));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(sliderMinPoint, sliderMaxPoint);

            // Draw step markers if steps are defined
            if (m_Steps > 0)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i <= m_Steps; i++)
                {
                    float stepPosition = Mathf.Lerp(m_MinPosition, m_MaxPosition, (float)i / m_Steps);
                    var stepPoint = transform.TransformPoint(new Vector3(0.0f, 0.0f, stepPosition));
                    Gizmos.DrawSphere(stepPoint, 0.005f);
                }
            }
        }

        void OnValidate()
        {
            m_Steps = Mathf.Max(0, m_Steps);
            SetSliderPosition(m_Value);
        }
    }
}