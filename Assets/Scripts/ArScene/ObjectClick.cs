using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectClick : MonoBehaviour
{
    public LayerMask Surfaces;
    public Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = Camera.allCameras.First(e => e.name == "AR Camera");
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, 10000f, Surfaces)) return;
            
            if (hit.transform.gameObject.tag == "AR Object")
            {
                StaticClass.objectName = hit.transform.gameObject.name;
                SceneManager.LoadScene("QuizScreen", LoadSceneMode.Additive);
            }
        }
    }
}
