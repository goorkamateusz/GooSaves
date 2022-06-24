using System.Collections;
using UnityEngine;

namespace Goo.Saves.Demo
{
    public class DemoManager : MonoBehaviour
    {
        private class TestSerializable : SaveSerializable
        {
            public string TestString;
            public int TestInt;

            public override string Key => "testKeyName";
        }

        private TestSerializable _test = new TestSerializable
        {
            TestString = "Init value",
            TestInt = 1
        };

        private IEnumerator Start()
        {
            yield return SaveManager.Wait();
            SaveManager.Instance.Load(ref _test);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _test.TestInt++;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _test.TestString = $"{_test.TestString}.";
            }
        }
    }
}