using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


  public class BarIndicatorObserver : MonoBehaviour, Observer
    {
      public RectTransform energyTransformP1;
      public RectTransform lifeTransformP1;
      public RectTransform lifeTransformP2;

      public UnityEngine.UI.Image visualEnergyP1;
      public UnityEngine.UI.Image visualHealthP1;
      public UnityEngine.UI.Image visualHealthP2;

      // EnergyBar
      float cachedYEn;
      float maxValueEn;
      float minValueEn;
      int maxEnergy;

      //LifeBar
      float cachedYLf;
      float maxValueLf;
      float minValueLf;
      int maxLife;

      void Start() {
          maxEnergy = 5;
          maxLife= 5;
          setValueToHandleChangeEn(energyTransformP1);
          setValueToHandleChangeLf(lifeTransformP1);
      }

      public void updateBar(PlayerBase player)
      {
          if (player.Name == "Player1")
          {
              HandleenergyBar(player.Energy, energyTransformP1, visualEnergyP1);
              HandleLifeBar(player.Life, lifeTransformP1, visualHealthP1);
          }
          else {
              HandleenergyBar(player.Life, lifeTransformP2, visualHealthP2);
          }
         
      }

      private void setValueToHandleChangeEn(RectTransform rectTransform)
      {
          cachedYEn = rectTransform.position.y;
          maxValueEn = rectTransform.position.x; // start position of energy bar
          minValueEn = rectTransform.rect.xMin - rectTransform.rect.xMax;
      }

      private void setValueToHandleChangeLf(RectTransform rectTransform)
      {
          cachedYLf = rectTransform.position.y;
          maxValueLf = rectTransform.position.x; // start position of energy bar
          minValueLf = maxValueLf + rectTransform.rect.width;
        
      }

      private void HandleenergyBar(int currentEnergy, RectTransform transform, Image visual)
      {
          float currentXValue = MapValuesEn(transform.rect.width, currentEnergy); 
          transform.anchoredPosition = new Vector3(currentXValue, 0.0f);
          if (currentEnergy > maxEnergy / 2)
          { // Alors + que 50 % d'energie
              visual.color = new Color32((byte)MapValues(currentEnergy, maxEnergy / 2, maxEnergy, 255, 0), 255, 0, 255);
          }
          else {
              visual.color = new Color32(255, (byte)MapValues(currentEnergy, 0, maxEnergy / 2, 0, 255), 0, 255);
          }
      }

      private void HandleLifeBar(int currentEnergy, RectTransform transform, Image visual)
      {
          float currentXValue = MapValuesLf(transform.rect.width, currentEnergy); // position of healthBar
          transform.anchoredPosition = new Vector3(currentXValue, 0.0f);
          if (currentEnergy > maxLife / 2)
          { // Alors + que 50 % d'energie
              visual.color = new Color32((byte)MapValues(currentEnergy, maxLife / 2, maxLife, 255, 0), 255, 0, 255);
          }
          else
          {
              visual.color = new Color32(255, (byte)MapValues(currentEnergy, 0, maxLife / 2, 0, 255), 0, 255);
          }
      }

      private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
      {
          return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
      }

      private float MapValuesEn(float width, float currentLife)
      {
          float oneLife = width / maxLife;
          return (width -  currentLife * oneLife) * -1;
      }

      private float MapValuesLf(float width, float currentLife)
      {
          float oneLife = width / maxLife;
          return width - (currentLife * oneLife);
      }

    }

