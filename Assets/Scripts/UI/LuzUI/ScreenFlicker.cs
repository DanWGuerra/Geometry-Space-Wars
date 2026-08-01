using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ScreenFlicker : MonoBehaviour
{
    [SerializeField] private HeatSystem HeatSystem;
    [SerializeField] private Animator Animator;
    [SerializeField] private GameObject WarningUI;
    

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        HeatSystem.OnOverheat += TriggerFlicker;
              
    }

    private void OnDisable()
    {
        HeatSystem.OnOverheat -= TriggerFlicker;
      
    }
    public void TriggerFlicker()
    {

        
        if (HeatSystem.IsOverheated)
        {
            Animator.gameObject.SetActive(true);
            Animator.SetBool("Blink", true);
        }
        StartCoroutine(FlickerRoutine());

    }

    //private void Update()
    //{
    //    if(FlickerRoutine() != null && !HeatSystem.IsOverheated)
    //    {
    //        StopCoroutine(FlickerRoutine());
    //        WarningUI.gameObject.SetActive(false);
    //        //Animator.SetBool("Blink", false);
    //        //Animator.gameObject.SetActive(false);
    //    }
        
    //}

    //public void HideFlicker(float Heat01)
    //{
    //    if (!HeatSystem.IsOverheated)
    //    {

    //        Animator.SetBool("Blink", false);
    //        Animator.gameObject.SetActive(false);
    //    }
    //}
    IEnumerator FlickerRoutine()
    {

        //if (HeatSystem.IsOverheated)
        //{
        //    Animator.gameObject.SetActive(true);
        //    Animator.SetBool("Blink", true);
        //}
        yield return new WaitForSeconds(4.8f);

        Animator.SetBool("Blink", false);
    }
    

}
