using UnityEngine.Assertions;

// Reference https://arxiv.org/pdf/2010.09714.pdf
public class SchlickCurve 
{

    public static float Evaluate(float x, float slope, float threshold)
    {
        Assert.IsTrue((x >= 0 && x <= 1), "Range error: x must be between 0 and 1");
        Assert.IsTrue(slope >= 0, "Slope must be greater or equal to 0");
        Assert.IsTrue((threshold >= 0 && threshold <= 1), "Range error: threshold must be between 0 and 1");

        if (x < threshold)
        {
            return (threshold * x) / (x + slope * (threshold - x) + float.Epsilon);
        }
        else
        {
            return ((1f - threshold) * (x - 1f)) / (1f - x - slope * (threshold - x) + float.Epsilon) + 1f;
        }
    }
}
