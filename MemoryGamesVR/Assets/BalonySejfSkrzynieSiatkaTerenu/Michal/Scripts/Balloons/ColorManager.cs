using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balloons
{
    public class ColorManager : MonoBehaviour
    {
        public Material Black, Blue, Yellow, Orange, Red, Green, Purple, White, DarkGreen, Green1, LightGreen, Sea, LightBlue;
        public enum Color
        {
            Black, Blue, Yellow, Orange, Red, Purple, White, Green, DarkGreen, Green1, LightGreen, Sea, LightBlue
        }
        
        public Material GetMaterial(int color)
        {
            switch (color)
            {
                case (int)Color.Black:
                    return Black;
                case (int)Color.Blue:
                    return Blue;
                case (int)Color.Green:
                    return Green;
                case (int)Color.Purple:
                    return Purple;
                case (int)Color.Red:
                    return Red;
                case (int)Color.White:
                    return White;
                case (int)Color.Yellow:
                    return Yellow;
                case (int)Color.DarkGreen:
                    return DarkGreen;
                case (int)Color.Green1:
                    return Green1;
                case (int)Color.LightGreen:
                    return LightGreen;
                case (int)Color.Sea:
                    return Sea;
                case (int)Color.LightBlue:
                    return LightBlue;
                case (int)Color.Orange:
                    return Orange;
                default:
                    return Black;
            }
        }
    }
}
