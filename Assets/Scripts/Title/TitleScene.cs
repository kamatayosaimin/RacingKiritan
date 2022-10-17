using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBehaviour
{

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    protected override void StartChild()
    {
    }

    protected override void UpdateChild()
    {
    }

    public void GOIS()
    {
        try
        {
            TitleScene gois = null;

            gois.SUZUI();
        }
        catch (System.Exception e)
        {
            ErrorManager.Instance.AddException(e);
        }
    }

    void SUZUI()
    {
    }
}
