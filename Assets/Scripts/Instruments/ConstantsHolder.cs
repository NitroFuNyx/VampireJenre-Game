
public class Layers
{
    public const int MapFloorLayer = 6;
    public const int ObstaclesOnMap = 7;
    public const int EnemySkeleton = 9;
    public const int EnemyGhost = 10;
    public const int EnemyZombie = 11;
    public const int MapBoundBox = 12;
    public const int SingleShotSkill = 13;
    public const int WeaponStrikeSkill = 15;

    public const int MeteorPuddle = 16;
    public const int MeteorSkill = 17;
    public const int AuraSkill = 18;
    public const int FireballSkill = 19;
    public const int ChainLightning = 20;
    public const int PulseAuraSkill = 21;
    public const int DeadEnemy = 22;
    public const int MissilesSkill = 23;
    public const int LightningBolt = 24;
    public const int ForceWave = 25;
    public const int Player = 26;

}

public class PlayerAnimations
{
    public const string IsRunning = "IsRunning";
}

public class EnemyAnimations
{
    public const string Move = "Move";
    public const string Laugh = "Laugh";
    public const string Jump = "Jump";
    public const string Die = "Die";
    public const string GetHit = "Get Hit";
    public const string Idle = "Idle";
}

public class TalentSlotAmount
{
    public const int maxSlotAmount = 9;

}

public class SkillsInGameValues
{
    public const int maxSkillLevel = 10;
    public const int maxSkillsInCategoryAmount = 4;
    public const int skillsOptionsForUpgradePerLevelAmount = 3;
}
