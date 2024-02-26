float GetAverage(float[] inputs)
{
    float sum = 0;
    foreach (var input in inputs)
    {
        sum += input;
    }
    return sum / inputs.Length;
}


float[] inputs = new float[] { -21, -22, -3, -4, 5, 6 };
float average = GetAverage(inputs);
Console.WriteLine(average);

if (average >= 10)
{
    Console.WriteLine("Double Digits");
}
else
{
    Console.WriteLine("Single Digits");
}
if (average < 0)
{
    Console.WriteLine("Average value is in the negative");
}