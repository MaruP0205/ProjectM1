using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISample
{
    void Showinfo();
    void setup(int a);

}
public abstract class SampleAbstract
{
    public int a;
    public abstract void ShowInfo();
    public void Setup()
    {

    }
    public virtual void Sum(int a, int b)
    {

    }

}
public class InterfaceSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class SampleChildAbstract : SampleAbstract
{
    public override void ShowInfo()
    {

    }
    public override void Sum(int a, int b)
    {
        base.Sum(a, b);
    }
}
// implement 
public class SampleInterfaceHandle : ISample
{
    public void setup(int a)
    {
    }

    public void Showinfo()
    {

    }
}
