using UnityEngine;
using System.Collections;

public class GlobalInit : MonoSingleton<GlobalInit>
{

    public RoleCtrl CurRoleCtrl;

    public bool IsPlaySound = true;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
