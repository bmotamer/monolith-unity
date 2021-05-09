using System;

namespace Monolith
{
    
    public static class MathHelper
    {

        public const double Deg2Rad = Math.PI / 180.0;
        public const double Rad2Deg = 180.0 / Math.PI;
        
        public static double VerticalToHorizontalFieldOfView(double verticalFieldOfView, double aspectRatio)
        {
            return 2.0 * Math.Atan(Math.Tan(0.5 * verticalFieldOfView) * aspectRatio);
        }
        
    }
    
}