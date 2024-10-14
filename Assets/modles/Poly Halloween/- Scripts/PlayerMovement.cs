using UnityEngine;
using System.Collections;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        public float interactionDistance = 3f; // 定义与NPC互动的距离
        private bool isInteracting = false; // 是否正在进行互动

        Vector3 velocity;
        bool isGrounded;

        void Update()
        {
            if (!isInteracting) // 在互动时禁止移动
            {
                HandleMovement();
            }

            CheckForNPCInteraction();
        }

        void HandleMovement()
        {
            controller = GetComponent<CharacterController>();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                controller.slopeLimit = 45.0f;
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (move.magnitude > 1)
                move /= move.magnitude;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                controller.slopeLimit = 100.0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void CheckForNPCInteraction()
        {
            // 寻找场景中的所有NPC对象
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
            
            foreach (GameObject npc in npcs)
            {
                float distance = Vector3.Distance(transform.position, npc.transform.position);

                // 如果玩家与NPC距离小于互动距离，并且没有正在互动
                if (distance <= interactionDistance && !isInteracting)
                {
                    NPC npcScript = npc.GetComponent<NPC>();
                    if (npcScript != null)
                    {
                        StartCoroutine(InitiateConversation(npcScript));
                    }
                    break; // 找到一个NPC后，停止检查
                }
            }
        }

        // 使用Coroutine等待三秒钟
        IEnumerator InitiateConversation(NPC npcScript)
        {
            isInteracting = true; // 设置为互动中，禁止移动
            Debug.Log("Approaching " + npcScript.npcName + "...");

            // 等待三秒
            yield return new WaitForSeconds(3f);

            // 触发NPC的对话
            npcScript.StartConversation();

            // 等待对话结束后恢复移动
            isInteracting = false;
        }
    }
}
