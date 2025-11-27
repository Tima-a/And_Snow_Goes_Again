using System;
using UnityEngine;
using UnityStandardAssets.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        public float m_WalkSpeed;
        public float m_RunSpeed;
        [SerializeField][Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] public MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        public float speed_modifier = 1.0f;
        public float def_walk_speed;
        public float def_run_speed;
        [SerializeField] public CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private float m_StepInterval;
        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        public AudioSource WickedFascinatinsaudio;
        private float m_NextStep;
        public static FirstPersonController instance;
        public float StartFogTime;
        public float EndFogTime;
        public GameObject door_audio;
        public bool fog_change;
        public GameObject canvas_intergalaxy_dimension;
        public TextMeshProUGUI text_timeline;
        public GameObject Door_collider;
        public GameObject flash_back;
        public bool restore_settings;
        public List<Vector3> battery_spawn_positions = new List<Vector3>();
        public float RestoreStartTime;
        public float RestoreEndTime;
        public GameObject battery;
        public string horizontal_;
        public string vertical_;
        public float StartWickedTime;
        public float EndWickedTime;
        public GameObject door_closed;
        public GameObject door_open;
        public bool wicked_fascinations;
        public List<Vector3> teddy_spawn_positions = new List<Vector3>();
        public GameObject teddy;
        // Use this for initialization
        void Start()
        {
            battery.transform.position = battery_spawn_positions[UnityEngine.Random.Range(0, 10)];
            instance = this;
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_MouseLook.Init(transform, m_Camera.transform);
        }
        public void running_x()
        {
            m_MouseLook.x_run();
        }
        public void other_x()
        {
            m_MouseLook.x_other();
        }
        void decrease_music()
        {
            if (wicked_fascinations)
            {
                WickedFascinatinsaudio.volume -= 0.005f;
                StartWickedTime += Time.deltaTime;
                if (StartWickedTime >= EndWickedTime)
                {
                    StartWickedTime = 0.0f;
                    wicked_fascinations = false;
                    WickedFascinatinsaudio.volume = 0.0f;
                }
            }
        }
        void RestoreSettings()
        {
            if (restore_settings == true)
            {

                RestoreStartTime += Time.deltaTime;
                WheelOfFortune.instance.can_pause = false;
                if (RestoreStartTime >= RestoreEndTime)
                {
                    WheelOfFortune.instance.can_pause = true;
                    if (WheelOfFortune.instance.round > 3)
                    {
                        teddy.transform.position = teddy_spawn_positions[UnityEngine.Random.Range(0, 4)];
                        coin_map.instance.teddy_map.SetActive(true);
                        coin_map.instance.teddy.SetActive(true);
                    }
                    TimelineDetecting.instance.mat_red.SetVector("_EmissionColor", TimelineDetecting.instance._emissionColorValue * TimelineDetecting.instance._intensity);
                    WheelOfFortune.instance.timelinechanged = true;
                    WheelOfFortune.instance.transform_player = true;
                    for (int i = 0; i < 32; i++)
                    {
                        Destroy(TimelineDetecting.instance.texts_obj_timeline[i]);
                    }
                    int f = UnityEngine.Random.Range(0, 10);
                    WheelOfFortune.instance.x = WheelOfFortune.instance.player_spawn_positions[f].x;
                    WheelOfFortune.instance.y = WheelOfFortune.instance.player_spawn_positions[f].y;
                    int b = UnityEngine.Random.Range(0, 6);
                    WheelOfFortune.instance.z = WheelOfFortune.instance.player_spawn_positions[f].z;
                    WheelOfFortune.instance.mmn += 5000;
                    for (int u = 0; u < Time_Brishen.instance.list_texts.Count; u++)
                    {
                        Time_Brishen.instance.list_texts[u].color = new Color32(255, 255, 255, 255);
                    }
                    if (b != 2 && b != 5)
                    {
                        WheelOfFortune.instance.wof.transform.position = WheelOfFortune.instance.wheel_spawn_positions[b];
                        WheelOfFortune.instance.wof.transform.rotation = Quaternion.EulerAngles(0.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        WheelOfFortune.instance.wof.transform.position = WheelOfFortune.instance.wheel_spawn_positions[b];
                        WheelOfFortune.instance.wof.transform.rotation = Quaternion.EulerAngles(0.0f, 90.0f, 0.0f);
                    }
                    WheelOfFortune.instance.pfs += 10000;
                    WheelOfFortune.instance.money_text.text = "CB: " + WheelOfFortune.instance.current_balance.ToString() + "$ / MMN: " + WheelOfFortune.instance.mmn.ToString() + "$ / PFS: " + WheelOfFortune.instance.pfs.ToString() + "$";
                    Time_Brishen.instance.text_time.color = new Color32(255, 255, 255, 255);
                    Time_Brishen.instance.Brishen_will_come_time = 10;
                    WheelOfFortune.instance.atm.transform.position = WheelOfFortune.instance.coin_converter_poses[UnityEngine.Random.Range(0, 6)];
                    WheelOfFortune.instance.text_processing.GetComponent<MeshRenderer>().material.color = Color.green;
                    restore_settings = false;
                    TimelineDetecting.instance.collider_place.SetActive(true);
                    TimelineDetecting.instance.dark_wall.SetActive(false);
                    battery.transform.position = battery_spawn_positions[UnityEngine.Random.Range(0, 10)];
                    TimelineDetecting.instance.is_changing_timeline_now = false;
                    coin_map.instance.battery.SetActive(true);
                    coin_map.instance.battery_rt.SetActive(true);
                    TimelineDetecting.instance.door.SetActive(false);
                    TimelineDetecting.instance.colliders.SetActive(false);
                    TimelineDetecting.instance.snow_floor_timeline.SetActive(false);
                    TimelineDetecting.instance.snow_floor.SetActive(true);
                    (GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
                    TimelineDetecting.instance._intensity = 3.0f;
                    for (int i = 0; i < 9; i++)
                    {
                        transform.position = new Vector3(-8.81f, 2.64f, -99.15f);
                        if (i != 8)
                        {
                            WheelOfFortune.instance.objects_environment_list[i].SetActive(true);
                        }
                    }
                    (GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
                    for (int j = 0; j < 4; j++)
                    {
                        coin_map.instance.coin_objs[j].transform.position = WheelOfFortune.instance.moneypos[UnityEngine.Random.Range(9 * j, (j + 1) * 9)];
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        coin_map.instance.rt_coin_objs[j].SetActive(true);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        coin_map.instance.coin_objs[i].SetActive(true);
                    }
                    TimelineDetecting.instance.text_bug = false;
                    canvas_intergalaxy_dimension.SetActive(false);
                    TimelineDetecting.instance.audio_wicked_fascinations.SetActive(false);
                    Time_Brishen.instance.minutes = 12;
                    TimelineDetecting.instance._intensity = 4.416926f;
                    text_timeline.text = TimelineDetecting.instance.TimelineTexts[UnityEngine.Random.Range(0, 32)];
                    RestoreStartTime = 0.0f;
                }
            }
        }
        // Update is called once per frame
        private void Update()
        {
            if (door_open.activeInHierarchy == true)
            {
                WickedFascinatinsaudio.volume -= 0.005f;
                TimelineDetecting.instance.sfx_manager.SetActive(true);
                TimelineDetecting.instance.sfx_manager2.SetActive(true);
                TimelineDetecting.instance.sfx_manager3.SetActive(true);
            }
            RestoreSettings();
            ChangeFogSettings();
            RotateView();
            // the jump state needs to read here to make sure it is not missed
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    m_Input.x = 0.0f;
                }
            }
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * (m_Input.x / 5.0f);

            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }
        private void GetInput(out float speed)
        {
            float horizontal = Input.GetAxis(horizontal_);
            float vertical = Input.GetAxis(vertical_);
            speed = m_WalkSpeed;
            m_Input = new Vector2(horizontal, vertical);

            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            if (m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }
        public void LockMouse()
        {
            m_MouseLook.falling_x_max();
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }
        void ChangeFogSettings()
        {
            if (fog_change == true)
            {
                StartFogTime += Time.deltaTime;
                RenderSettings.fogDensity += 0.01f;
                if (StartFogTime >= EndFogTime)
                {
                    StartFogTime = 0.0f;
                    fog_change = false;
                    RenderSettings.fogDensity = 0.2f;
                }
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "DoorExit")
            {
                flash_back.SetActive(true);
                restore_settings = true;
                Door_collider.SetActive(true);
                door_audio.SetActive(false);
            }
            if (other.tag == "Door")
            {
                TimelineDetecting.instance.sfx_manager.SetActive(true);
                TimelineDetecting.instance.sfx_manager2.SetActive(true);
                TimelineDetecting.instance.sfx_manager3.SetActive(true);
                RenderSettings.fogDensity = 0.1f;
                door_open.SetActive(true);
                door_closed.SetActive(false);
                m_WalkSpeed = 0.0f;
                DarkEffect.instance.col_restore = true;
                m_RunSpeed = 0.0f;
                door_audio.SetActive(true);
            }
            if (other.tag == "DoorEnter")
            {
                door_open.SetActive(false);
                door_closed.SetActive(true);
            }
        }
    }
}
