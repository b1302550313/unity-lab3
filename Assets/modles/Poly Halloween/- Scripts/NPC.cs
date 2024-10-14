using UnityEngine;
using System.Collections;

namespace Polyperfect.Universal
{
    public class NPC : MonoBehaviour
    {
        public string npcName = "Default NPC"; // NPC 的名称
        public string[] dialogue; // 对话内容

        // 此方法在玩家接近时触发对话
        public void StartConversation()
        {
            if (dialogue != null && dialogue.Length > 0)
            {
                Debug.Log("Talking to " + npcName);
                foreach (string line in dialogue)
                {
                    Debug.Log(line); // 可以将这些对话替换为实际UI显示
                }
            }
            else
            {
                Debug.Log(npcName + " has nothing to say.");
            }
        }
    }
}
