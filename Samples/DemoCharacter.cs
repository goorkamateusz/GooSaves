using System.Collections;
using UnityEngine;

namespace Goo.Saves.Demo
{
    public class DemoCharacter : MonoBehaviour
    {
        private class TestSerializable : AttributedSaveSerializable
        {
            public string TestString;
            public int TestInt;

            public override string SubKey => "TestKey";

            public TestSerializable(string parentKey) : base(parentKey) { }
        }

        [SerializeField] private string _characterName;

        private TestSerializable _test;

        private IEnumerator Start()
        {
            _test = new TestSerializable(_characterName);
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