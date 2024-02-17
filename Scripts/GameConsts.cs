using UnityEngine;
public class GameConsts //Game Constants Class
{
	#region Game Tags

	public const string MAINLIGHT = "MainLight";
	public const string ALARMLIGHT = "AlarmLight";
	public const string MEGAPHONE = "MegaPhone";
	public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";
    public const string INNERDOOR = "InnerDoor";
    public const string LIFT = "Lift";
    #endregion

    #region Game Virtual Botton & Axis

    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string SNEAK = "Sneak";
    public const string SHOUT = "Shout";
    public const string SWITCH = "Switch";
    #endregion

    #region Animator Parameters & States

    public static int SPEED_PARAM;
    public static int ANGULARSPEED_PARAM;
    public static int SNEAK_PARAM;
    public static int SHOUT_PARAM;
    public static int LOCOMOTION_STATE;
    public static int DOOROPEN_PARAM;
    public static int PLAYERINSIGHT_PARAM;
    public static int WEAPONSHOOT_STATE;
    public static int DEAD_STATE;
    public static int ENDGAME_PARM;

    #endregion

    #region Game Parameters

    public const float VIEWPOINT_OFFSET = 0; //头顶观察的距离偏移量
    public const float PLAYER_BODY_OFFSET = 1f; //玩家身体的偏移量
    public const float PLAYER_HEART_OFFSET = 1.5f; //玩家受到伤害的偏移量
    public const float ENEMY_EYES_OFFSET = 1.7f; //机器人眼睛的偏移量

    #endregion

    #region Static Constructor

    static GameConsts()
    {
        SPEED_PARAM = Animator.StringToHash("Speed");
        ANGULARSPEED_PARAM = Animator.StringToHash("AngularSpeed");
        SNEAK_PARAM = Animator.StringToHash("Sneak");
        SHOUT_PARAM = Animator.StringToHash("Shout");
        LOCOMOTION_STATE = Animator.StringToHash("Locomotion");
        DOOROPEN_PARAM = Animator.StringToHash("DoorOpen");
        PLAYERINSIGHT_PARAM = Animator.StringToHash("PlayerInSight");
        WEAPONSHOOT_STATE = Animator.StringToHash("WeaponShoot");
        DEAD_STATE = Animator.StringToHash("Dead");
    }
    #endregion
}
