using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


  public class Energy : MonoBehaviour, Observer
    {
      public float speed;
      public RectTransform energyTransform;
      float cachedY;
      float maxValue;
      float minValue;
      int maxEnergy;
      public UnityEngine.UI.Image visualEnergy;


      void Start() {

          cachedY = energyTransform.position.y;
          maxValue = energyTransform.position.x; // start position of energy bar
          minValue = energyTransform.position.x - energyTransform.rect.width;
          maxEnergy = 5;
      }

      public void updateBar(int data)
      {
          HandleenergyBar(data);
      }

      private void HandleenergyBar(int currentEnergy) {
          float currentXValue = MapValues(currentEnergy, 0, maxEnergy, minValue, maxValue); // position of healthBar
          energyTransform.position = new Vector3(currentXValue, cachedY);

          if (currentEnergy > maxEnergy / 2)
          { // Alors + que 50 % d'energie
              visualEnergy.color = new Color32((byte)MapValues(currentEnergy,maxEnergy/2,maxEnergy,255,0),255,0,255);
          }
          else {
              visualEnergy.color = new Color32(255,(byte)MapValues(currentEnergy, 0, maxEnergy/2, 0, 255), 0, 255);
          }
      }

      private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
      {
          return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
      }

    }

