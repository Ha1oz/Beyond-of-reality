using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    public delegate void TestDelegate();//контейнер
    public TestDelegate testVal;//переменная делегата

    public delegate int Mathenatic(int x,int y);
    public Mathenatic val2;

    public delegate void Printf();
    public Printf val3;

    

    private void Start(){
        /*
        testVal = Message + Message2;

        testVal -= Message2;

        testVal?.Invoke();

        val2 = Sum;
        int result = val2.Invoke(4,5);
        Debug.Log(result);
        */
        //----------------------------
        val3 += print1;
        val3+= print2;
        val3 += print3;
        val3?.Invoke();

    }

    public void print1(){
        Debug.Log("Опять");
    }
    public void print2(){
        Debug.Log("Работа");
    }
    public void print3(){
        Debug.Log("!!!!!");
    }




    /*
public int Sum(int a, b){
        return a+b;
    }

    public void Message(){
        Debug.Log("Test Message");
    }

    public void Message2(){
        Debug.Log("Test Message 2");
    }
    */



}
