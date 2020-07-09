using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestMovementController
    {
        private MovementController _movementController;


        // A Test behaves as an ordinary method
        [Test]
        public void TestMovementControllerSimplePasses()
        {
            // Use the Assert class to test conditions
            GameObject go = new GameObject();
            go.AddComponent<Rigidbody>();
            go.AddComponent<MovementController>();
            Assert.IsTrue(go.GetComponent<MovementController>().jumpForce is float);
        }

        // A UnityTest behaves like a coroutine in Play Mode.
        // In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestMovementControllerWithEnumeratorPasses()
        {
            GameObject player =
                MonoBehaviour.Instantiate(
                    Resources.Load<GameObject>("Prefabs/PlayerCube")
                );

            Vector3 init_postion = player.transform.position;
            player.GetComponent<MovementController>().Move(Direction.Down);
            player.GetComponent<MovementController>().Move(Direction.Down);
            player.GetComponent<MovementController>().Move(Direction.Down);
            player.GetComponent<MovementController>().Move(Direction.Down);
            Vector3 new_postion = player.transform.position;
            Assert.IsFalse(init_postion.Equals(new_postion));
            yield return null;
        }
    }
}