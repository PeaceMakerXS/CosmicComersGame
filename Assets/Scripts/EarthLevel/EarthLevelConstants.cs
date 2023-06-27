public static class EarthLevelConstants
{
    public struct Player
    {
        public const int initialHealth = 1000;
        public const float initialSpeed = 4.5f;
        public const float initialJumpForce = 7f;
        public const float initialAttackRange = 1.5f;
        public const float initialAttackCoolDown = 0.8f;

        public const float attackAnimationDuration = 0.7f;

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
}
