public static class EarthLevelConstants
{
    public struct Player
    {
        public const int initialHealth = 40;
        public const float initialSpeed = 4.5f;
        public const float initialJumpForce = 8.5f;

        public const float initialAttackRange = 1.5f;
        public const float initialAttackCoolDown = 0.5f;
        public const int initialAttackDamage = 2;

        public const float attackAnimationDuration = 0.4f;

        public struct HitColors
        {
            public const float firstColor = 0.6132f;
            public const float secondColor = 0.418f;
            public const float thirdColor = 0.414f;
        }
    }

    public struct EnemyHitColors
    {
        public struct GrassBlock
        {
            public const float firstColor = 1f;
            public const float secondColor = 0.5898f;
            public const float thirdColor = 0.5898f;
        }

        public struct SlimeBlock
        {
            public const float firstColor = 1f;
            public const float secondColor = 0.40625f;
            public const float thirdColor = 0.40625f;
        }

        public struct SnakeSlime
        {
            public const float firstColor = 1f;
            public const float secondColor = 0.40625f;
            public const float thirdColor = 0.40625f;
        }

        public struct SnakeLava
        {
            public const float firstColor = 0.6484f;
            public const float secondColor = 0.3828f;
            public const float thirdColor = 0.3828f;
        }

        public struct Bee
        {
            public const float firstColor = 0.4609f;
            public const float secondColor = 0.2422f;
            public const float thirdColor = 0.2422f;
        }
    }


    public struct Generation
    {
        public const int cellTypesCount = 3;
        public const int suitPartsCount = 6;
        public const float requiredHeroOffset = 20f;
        public const int neededCoinsForSuitePart = 6;

        public struct SmallCell
        {
            public const int maxEnemiesCount = 1;
            public const int maxOtherObjectsCount = 2;
            public const int maxObjectsCount = 2;
            public const float yCorrection = 0.5f;
        }

        public struct CommonCell
        {
            public const int maxEnemiesCount = 2;
            public const int maxOtherObjectsCount = 2;
            public const int maxObjectsCount = 3;
            public const float yCorrection = 0.5f;
        }

        public struct BigCell
        {
            public const int maxEnemiesCount = 4;
            public const int maxOtherObjectsCount = 3;
            public const int maxObjectsCount = 4;
            public const float yCorrection = 0f;
        }
    }
}
