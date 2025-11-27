using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationSystem : MonoBehaviour
{
    public Animator heroanim;
    public Animator heroanim_hero;
    public GameObject HeroController;
    public bool is_falling;
    public CharacterController herocharactercontroller;
    public static HeroAnimationSystem instance;
    public bool dont_play_basic_anims;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        heroanim = GetComponent<Animator>();
        herocharactercontroller = HeroController.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (is_falling == true)
        {
            heroanim.SetBool("isStanding", false);
            heroanim.SetBool("isWalking", false);
            heroanim.SetBool("isRunning", false);
            heroanim.SetBool("isWalkingLeft", false);
            heroanim.SetBool("isWalkingRight", false);
            heroanim.SetBool("isWalkingBack", false);
            heroanim.SetBool("isFalling", true);
        }
        if (dont_play_basic_anims == false)
        {
            if (Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow) == true)
            {
                heroanim.SetBool("isStanding", false);
                heroanim.SetBool("isWalking", false);
                heroanim.SetBool("isWalkingLeft", false);
                heroanim.SetBool("isWalkingRight", false);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.other_x();
                heroanim.SetBool("isRunning", false);
                heroanim.SetBool("isWalkingBack", true);
                herocharactercontroller.enabled = true;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.1f;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.1f;
                if (TimelineDetecting.instance.text_bug == false)
                {
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.def_walk_speed;
                }
            }
            else
            {
                heroanim.SetBool("isWalkingBack", false);
            }
            if (Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    heroanim.SetBool("isStanding", false);
                    heroanim.SetBool("isWalking", true);
                    heroanim.SetBool("isRunning", false);
                    heroanim.SetBool("isWalkingBack", false);
                    heroanim.SetBool("isWalkingLeft", false);
                    heroanim.SetBool("isWalkingRight", false);
                    herocharactercontroller.enabled = true;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.1f;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.1f;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.other_x();
                    if (TimelineDetecting.instance.text_bug == false)
                    {
                        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.def_walk_speed;
                    }
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    heroanim.SetBool("isStanding", false);
                    heroanim.SetBool("isWalking", false);
                    heroanim.SetBool("isRunning", true);
                    heroanim.SetBool("isWalkingLeft", false);
                    heroanim.SetBool("isWalkingRight", false);
                    heroanim.SetBool("isWalkingBack", false);
                    herocharactercontroller.enabled = true;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.1f;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.1f;
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.running_x();
                    if (TimelineDetecting.instance.text_bug == false)
                    {
                        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.def_run_speed;
                    }
                }
            }
            if ((Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.LeftArrow) == true) && (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.UpArrow) == false) && (Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.DownArrow) == false))
            {
                herocharactercontroller.enabled = true;
                heroanim.SetBool("isStanding", false);
                heroanim.SetBool("isWalking", false);
                heroanim.SetBool("isRunning", false);
                heroanim.SetBool("isWalkingBack", false);
                heroanim.SetBool("isWalkingLeft", true);
                heroanim.SetBool("isWalkingRight", false);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.other_x();
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.1f;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.1f;
                if (TimelineDetecting.instance.text_bug == false)
                {
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.def_walk_speed;
                }
            }
            if ((Input.GetKey(KeyCode.D) == true || Input.GetKey(KeyCode.RightArrow) == true) && (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.UpArrow) == false) && (Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.DownArrow) == false))
            {
                herocharactercontroller.enabled = true;
                heroanim.SetBool("isStanding", false);
                heroanim.SetBool("isWalking", false);
                heroanim.SetBool("isRunning", false);
                heroanim.SetBool("isWalkingBack", false);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.other_x();
                heroanim.SetBool("isWalkingLeft", false);
                heroanim.SetBool("isWalkingRight", true);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.1f;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.1f;
                if (TimelineDetecting.instance.text_bug == false)
                {
                    UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.def_walk_speed;
                }
            }
            if (Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.DownArrow) == false && Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.DownArrow) == false)
            {
                heroanim.SetBool("isStanding", true);
                heroanim.SetBool("isWalking", false);
                heroanim.SetBool("isRunning", false);
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.other_x();
                heroanim.SetBool("isWalkingBack", false);
                heroanim.SetBool("isWalkingRight", false);
                heroanim.SetBool("isWalkingLeft", false);
                herocharactercontroller.enabled = false;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.HorizontalBobRange = 0.0f;
                UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_HeadBob.VerticalBobRange = 0.0f;
                //UnityStandardAssets.Characters.FirstPerson.FirstPersonController.instance.m_WalkSpeed = 0.0f;
            }
        }
    }
}
