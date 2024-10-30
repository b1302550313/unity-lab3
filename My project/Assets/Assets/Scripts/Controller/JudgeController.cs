using UnityEngine;

public class JudgeController : MonoBehaviour
{
    public FirstController mainController;
    public Side leftSideModel;
    public Side rightSideModel;
    public Boat boatModel;
    // Start is called before the first frame update
    void Start()
    {
        mainController = (FirstController)SSDirector.getInstance().currentSceneController;
        this.leftSideModel = mainController.leftShoreController.GetShore();
        this.rightSideModel = mainController.rightShoreController.GetShore();
        this.boatModel = mainController.boatController.GetBoatModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainController.isRunning)
            return;
        if (mainController.time <= 0)
        {
            mainController.JudgeCallback(false, "Game Over!");
            return;
        }
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        //判断是否已经胜利
        if (rightSideModel.pastorCount == 3)
        {
            mainController.JudgeCallback(false, "You Win!");
            return;
        }
        else
        {

            int leftPriestNum, leftDevilNum, rightPriestNum, rightDevilNum;
            leftPriestNum = leftSideModel.pastorCount + (boatModel.isRight ? 0 : boatModel.pastorCount);
            leftDevilNum = leftSideModel.devilCount + (boatModel.isRight ? 0 : boatModel.devilCount);
            if (leftPriestNum != 0 && leftPriestNum < leftDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
            rightPriestNum = rightSideModel.pastorCount + (boatModel.isRight ? boatModel.pastorCount : 0);
            rightDevilNum = rightSideModel.devilCount + (boatModel.isRight ? boatModel.devilCount : 0);
            if (rightPriestNum != 0 && rightPriestNum < rightDevilNum)
            {
                mainController.JudgeCallback(false, "Game Over!");
                return;
            }
        }
    }
}

