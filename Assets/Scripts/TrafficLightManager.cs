using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
   public GameObject Red;
   public GameObject Green;
   private bool isRun = false;
   public PlayerManager PL;

   private void Start()
   {
      PL = FindObjectOfType<PlayerManager>();
   }

   private void Update()
   {
      if (PL.isStoping == true)
      {
         StartCoroutine(Run());
      }

      if (isRun == true)
      {
         StopCoroutine(Run());
         Green.SetActive(true);
         Red.SetActive(false);
      }
   }

   IEnumerator Run()
   {
      yield return new WaitForSeconds(2f);
      isRun = true;
      PL.isRum = true;
      StartCoroutine(Run());
   }
}
