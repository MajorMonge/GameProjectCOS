using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public List<GameObject> PropsObjective;
  public string NextScene;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Debug.Log("Remaining Props: " + PropsObjective.Count);
    foreach (GameObject Objective in PropsObjective.ToArray())
    {
      if (Objective == null)
      {
        PropsObjective.Remove(Objective);
      }
    }
    if (PropsObjective.Count == 0)
    {
      SceneManager.LoadScene(NextScene);
    }
  }
}
