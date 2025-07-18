﻿using SplashKitSDK;
using System.Numerics;

namespace CustomProject.Foe
{
    public class FastEnemy : Enemy
    {

        // Internal so cannot be created outside this namespace. Should be created using a factory.
        internal FastEnemy() : base(10, 50, 20)
        {
        }


        /// <summary>
        /// Performs a rotation matrix on 2d vector. 
        /// </summary>
        /// <param name="pos">2D vector that needs to be rotated.</param>
        /// <param name="angle">Rotation angle in degrees.</param>
        /// <returns></returns>
        private static Vector2 RotateVector(Vector2 pos, float angle)
        {
            float angleInRadians = MathF.PI * angle / 180;
            float cosTheta = MathF.Cos(angleInRadians);
            float sinTheta = MathF.Sin(angleInRadians);

            // Expanded Rotation Matrix
            float newX = pos.X * cosTheta - pos.Y * sinTheta;
            float newY = pos.X * sinTheta + pos.Y * cosTheta;

            return new Vector2(newX, newY);
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            // Creates the triangle by finding the Vertices of it. 
            Vector2 head = Direction;
            Vector2 tail1 = RotateVector(head, 120);
            Vector2 tail2 = RotateVector(head, -120);
            int triangleSize = 15;
            head = head * triangleSize + Location;
            tail1 = tail1 * triangleSize + Location;
            tail2 = tail2 * triangleSize + Location;
            instructions.Add(new DrawInstructions(() =>
            {
                SplashKit.FillTriangle(Color.Blue, head.X, head.Y, tail1.X, tail1.Y, tail2.X, tail2.Y);
                SplashKit.FillCircle(Color.Black, head.X, head.Y, 3);
            }, 2));

            base.GetDrawInstructions(instructions);
        }
    }
}
