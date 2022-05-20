using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J {
    public class TimedChallengeManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] checkpoints;
        Transform[] checkpointPlaces;

        GameObject checkpointClones;

        private void Start()
        {
            for (int i = 0; i > checkpoints.Length; i++)
            {
                checkpointPlaces[i] = checkpoints[i].transform;
            }
        }

        public void SetStartChallengeVisible()
        {
            checkpoints[0].GetComponent<CheckpontEnter>().visible = true;
        }

        public void TimeTrialCompleted()
        {
            Destroy(this.gameObject);
        }

        public void CloneFirstCheckpoint()
        {
            checkpointClones = Instantiate(checkpoints[0], checkpoints[0].transform.position, checkpoints[0].transform.rotation,
                this.gameObject.transform) as GameObject;

            checkpointClones.GetComponent<CheckpontEnter>().visible = false;

            Destroy(checkpoints[0]);
            checkpoints[0] = checkpointClones;
        }
    }
}