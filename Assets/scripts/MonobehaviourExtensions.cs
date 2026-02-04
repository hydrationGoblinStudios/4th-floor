using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
 
//  Se usarem o padrao a seguir e possivel injetar funcionalidade aos monobehaviours globalmente:
//
//  public static T NomeDoMetodo(this MonoBehaviour a, {parametros})
//
//  Para utilizar o metodo em algum MonoBehaviour, so chamar:
//  this.NomeDoMetodo({parametros})
public static class MonobehaviourExtensions
{
    /// <summary>
    /// Calls a method after the determined time.
    /// </summary>
    /// <param name="self"> MonoBehaviour calling this extension </param>
    /// <param name="callable"> Method to be called after the coroutine </param>
    /// <param name="time"> Time passed to WaitForSeconds() </param>
    public static void CallWithDelay(this MonoBehaviour self, Action callable, float time)
    {
        self.StartCoroutine(_DelayCoroutine(callable, time));
    }
    private static IEnumerator _DelayCoroutine(Action callable, float time)
    {
        yield return new WaitForSeconds(time);
        callable();
    }
}
