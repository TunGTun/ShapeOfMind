
using UnityEngine;

public static class Data
{
    public static readonly int cardNumber;
    public static readonly int columnNumber;

    public static readonly float baseOffsetY = -200f;   // khoảng cách giữa các card ngang hàng
    public static readonly float childOffsetY = -130f;  // khoảng cách giữa card con và cha
    public static readonly float globalOffsetY = 2500f; // dịch toàn bộ stack lên

    public static readonly float boardMinHeight = 700f; // dịch toàn bộ stack lên

    public static int StepCount = 0;
}
