using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        /*[UnityTest]
        public IEnumerator ConversionIntBoolArray()
        {
            bool[] testArray = { true, false, false, true, false };
            int converted = Util.BoolArrayToInt(testArray);
            bool[] finalArray = Util.IntToBoolArray(converted, testArray.Length);

            Assert.AreEqual(testArray, finalArray);

            return null;
        }*/

        [UnityTest]
        public IEnumerator SaveLoadScene()
        {
            SceneManager.LoadScene(0);

            yield return null;
        }
    }
}
