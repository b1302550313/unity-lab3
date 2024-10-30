using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController
{
    Side sideModel;
    public void CreateShore(Vector3 position)
    {
        if (sideModel == null)
        {
            sideModel = new Side(position);
        }
    }
    public Side GetShore()
    {
        return sideModel;
    }

    //将角色添加到岸上，返回角色在岸上的相对坐标
    public Vector3 AddRole(Role roleModel)
    {
        roleModel.role.transform.parent = sideModel.side.transform;
        roleModel.inBoat = false;
        if (roleModel.isPastor) sideModel.pastorCount++;
        else sideModel.devilCount++;
        return Position.role_side[roleModel.id];
    }

    //将角色从岸上移除
    public void RemoveRole(Role roleModel)
    {
        if (roleModel.isPastor) sideModel.pastorCount--;
        else sideModel.devilCount--;
    }
}