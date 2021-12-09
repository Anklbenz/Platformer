using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
{
  private int hitCount;

    ScoreMultiplier() { }


    public int GetMulpiplier() {
        return hitCount;
    }

}
