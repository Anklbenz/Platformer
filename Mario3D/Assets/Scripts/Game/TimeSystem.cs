using System;
using System.Threading.Tasks;
using UnityEngine;

public class TimeSystem
{
   public event Action TickEvent;
   public event Action TimesUpEvent;

   private const int TIMER_DELAY = 400;
   private int _time;
   public int Time => _time;

   public TimeSystem(int time){
      _time = time;
      TimeTick();
   }

   private async void TimeTick(){
      while (_time > 0){
         await Task.Delay(TIMER_DELAY);
         _time--;
         TickEvent?.Invoke();
      }

      TimesUpEvent?.Invoke();
      Debug.LogError("GAME OVER. Time Is Up");
   }
}
