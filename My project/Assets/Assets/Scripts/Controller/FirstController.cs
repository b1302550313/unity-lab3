using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public CCActionManager actionManager;
    public SideController leftShoreController, rightShoreController;
    public River river;
    public BoatController boatController;
    public RoleController[] roleControllers;
    //public MoveCtrl moveController;
    public bool isRunning;
    public float time;

    public void JudgeCallback(bool isRun, string message)
    {
        this.gameObject.GetComponent<UserGUI>().gameMessage = message;
        this.gameObject.GetComponent<UserGUI>().time = (int)time;
        this.isRunning = isRun;
        
    }
    public void LoadResources()
    {

        //role
        roleControllers = new RoleController[6];
        for (int i = 0; i < 6; ++i)
        {
            roleControllers[i] = new RoleController();
            roleControllers[i].CreateRole(Position.role_side[i], i < 3 ? true : false, i);
        }

        //shore
        leftShoreController = new SideController();
        leftShoreController.CreateShore(Position.left);
        leftShoreController.GetShore().side.name = "left_shore";
        rightShoreController = new SideController();
        rightShoreController.CreateShore(Position.right);
        rightShoreController.GetShore().side.name = "right_shore";

        //将人物添加并定位至左岸  
        foreach (RoleController roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition = leftShoreController.AddRole(roleController.GetRoleModel());
        }
        boatController = new BoatController();
        boatController.CreateBoat(Position.left_boat);

        river = new River(Position.river);

        isRunning = true;
        time = 60;


    }
    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    #region IUserAction implementation
    public void GameOver()
    {
        SSDirector.getInstance().NextScene();
    }
    #endregion

    public void MoveBoat()
    {
        if (isRunning == false || actionManager.IsMoving()) return;

        Vector3 destination = boatController.GetBoatModel().isRight ? Position.left_boat : Position.right_boat;
        actionManager.MoveBoat(boatController.GetBoatModel().boat, destination, 5);

        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;

    }

    public void MoveRole(Role roleModel)
    {
        if (isRunning == false || actionManager.IsMoving()) return;
        Vector3 destination, mid_destination;
        if (roleModel.inBoat)
        {
            Vector3 tmp = new Vector3(0, 2.68f, 0);
            Vector3 tmp2 = new Vector3(-2.6f, 0, 0);
            Vector3 tmp3 = new Vector3(1f, 0, 0);
            Vector3 tmp4 = new Vector3(2f, 0, 0);
            Vector3 dtmp = tmp2+tmp3 * roleModel.id;
            Vector3 dtmp2 = tmp2+tmp4 * roleModel.id;
            if (boatController.GetBoatModel().isRight)
            {
                destination = rightShoreController.AddRole(roleModel);
                mid_destination = new Vector3(roleModel.role.transform.position.x, destination.y+Position.right.y, destination.z);
                actionManager.MoveRole(roleModel.role, mid_destination+tmp, destination + Position.right+tmp+dtmp, 5);
            }
                
            else
            {
                destination = leftShoreController.AddRole(roleModel);
                mid_destination = new Vector3(roleModel.role.transform.position.x, destination.y + Position.left.y, destination.z);
                actionManager.MoveRole(roleModel.role, mid_destination + tmp, destination + Position.left+tmp-dtmp, 5);
            }
            roleModel.onRight = boatController.GetBoatModel().isRight;
            boatController.RemoveRole(roleModel);
        }
        else
        {

            if (boatController.GetBoatModel().isRight == roleModel.onRight)
            {
                Vector3 tmp = new Vector3(2.3f, 2.3f, 0);
                if (roleModel.onRight)
                {
                    rightShoreController.RemoveRole(roleModel);
                    destination = boatController.AddRole(roleModel);
                    mid_destination = new Vector3(destination.x+Position.right_boat.x+tmp.x, roleModel.role.transform.position.y, destination.z);
                    actionManager.MoveRole(roleModel.role, mid_destination, destination+Position.right_boat+tmp, 5);
                }
                else
                {
                    leftShoreController.RemoveRole(roleModel);
                    destination = boatController.AddRole(roleModel);
                    mid_destination = new Vector3(destination.x + Position.left_boat.x+tmp.x, roleModel.role.transform.position.y, destination.z);
                    actionManager.MoveRole(roleModel.role, mid_destination, destination + Position.left_boat+tmp, 5);
                }             
            }
        }
    }

    void Awake()
    {
        SSDirector.getInstance().currentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<JudgeController>();
    }

    void Update()
    {
        if (isRunning)
        {
            time -= Time.deltaTime;
            this.gameObject.GetComponent<UserGUI>().time = (int)time;
        }
    }
}