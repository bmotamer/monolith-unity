using UnityEngine;

namespace Monolith.Unity
{
    
    public static class UnityMathHelper
    {

        public static float VerticalToHorizontalFieldOfView(Camera camera)
        {
            double verticalFieldOfView = camera.fieldOfView * MathHelper.Deg2Rad;
            double horizontalFieldOfView = MathHelper.VerticalToHorizontalFieldOfView(verticalFieldOfView, camera.aspect);
            
            return (float)(horizontalFieldOfView * MathHelper.Rad2Deg);
        }

        public static Vector2 PixelsToDegrees(Camera camera, float width = 1.0F, float height = 1.0F)
        {
            float verticalFieldOfView = camera.fieldOfView;
            float horizontalFieldOfView = VerticalToHorizontalFieldOfView(camera);
            
            return new Vector2(
                width / camera.pixelWidth * horizontalFieldOfView,
                height / camera.pixelHeight * verticalFieldOfView
            );
        }
        
    }
    
}