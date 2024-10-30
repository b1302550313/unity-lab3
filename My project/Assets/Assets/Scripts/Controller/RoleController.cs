using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : ClickAction
{
    Role roleModel;
    IUserAction userAction;

    public RoleController()
    {
        userAction = SSDirector.getInstance().currentSceneController as IUserAction;

    }

    public void CreateRole(Vector3 position, bool isPastor, int id)
    {
        if (roleModel != null)
        {
            Object.DestroyImmediate(roleModel.role);
        }
        roleModel = new Role(position, isPastor, id);
        roleModel.role.GetComponent<Click>().setClickAction(this);
    }

    public Role GetRoleModel()
    {
        return roleModel;
    }

    public void DealClick()
    {
        userAction.MoveRole(roleModel);
    }
}
