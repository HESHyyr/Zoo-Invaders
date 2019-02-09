using System.Collections;
using System.Collections.Generic;

// This class track the infomation that need to be carried across multiple levels
public static class LevelInfo 
{
    public static int life = 3;
    public static int score = 0;
    public static float moveCD = 1.0f;
    public static List<float> shootCDRange = new List<float>() {2.0f,4.0f};
    public static float enemyHeight = 6.5f;

}
