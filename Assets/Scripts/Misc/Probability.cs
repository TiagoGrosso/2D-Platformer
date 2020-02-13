using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Probability
{
				public static bool Happens(int probability)
				{
								return probability >= Random.Range(0, 101);
				}

				public static T RandomInCollection<T>(T[] array)
				{
								int index = Random.Range(0, array.Length);
								return array[index];
				}

				public static T RandomInCollection<T>(List<T> list)
				{
								int index = Random.Range(0, list.Count);
								return list[index];
				}

				public static float RandomStep(float min, float max, int numSteps)
				{
								int steps = Random.Range(0, numSteps);
								float stepSize = (max - min) / (numSteps - 1);
								return min + (stepSize * steps);
				}

				public static float RandomStep(int min, int max, int numSteps)
				{
								return RandomStep((float)min, (float)max, numSteps);
				}
}
