using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Projectiles
{
    public class BetsyBreath : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(687);
            // projectile.aiStyle = 3; This line is not needed since CloneDefaults sets it.
            AIType = ProjectileID.DD2BetsyFlameBreath;
        }
    }
}