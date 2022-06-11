using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSceneTest : MonoBehaviour
{
    public bool ShowPhantoms;
    public GameObject BallPrefab;
    public List<GameObject> BallsPrefabs = new List<GameObject>();

    public Material TRMaterial;
    public Material[] TRMaterials;
    public float ForceFactor = 10f;
    public int SimSteps = 50;
    public GameObject real_white;
    public List<GameObject> real_balls = new List<GameObject>();
    public Camera cam;
    public GameObject SceneRoot;


    Scene mainScene;
    Scene physicsScene;
    Vector3 currForce, prevMousePos;
    Vector3 lastHitPoint;
    GameObject sim_whiteball;
    List<GameObject> sim_balls = new List<GameObject>();
    LineRenderer lineRenderer;
    LineRenderer[] lineRenderers;

    // Start is called before the first frame update
    void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        physicsScene = SceneManager.CreateScene("physics-scene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        
        //If we create physicsScene in this way, both scenes will have the same DefaultPhysicsScene:
        //  if we'll disable SceneRoot in the mainScene, real_white ball will continue to collide with sim_sceneRoot!
        //physicsScene = SceneManager.CreateScene("physics-scene");

        GameObject sim_sceneRoot = Instantiate(SceneRoot);
        MeshRenderer[] MRenderers = sim_sceneRoot.GetComponentsInChildren<MeshRenderer>();
        foreach (var MRenderer in MRenderers)
            MRenderer.enabled = false;
        SceneManager.MoveGameObjectToScene(sim_sceneRoot, physicsScene);
        sim_whiteball = Instantiate(BallPrefab);
        sim_whiteball.transform.position = new Vector3(100, 100, 100);
        sim_whiteball.GetComponent<MeshRenderer>().material = TRMaterial;
        SceneManager.MoveGameObjectToScene(sim_whiteball, physicsScene);

        lineRenderers = new LineRenderer[real_balls.Count];
        
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        ClearLines(lineRenderer, real_white);

        for (int i = 0; i < real_balls.Count; i++)
        {
            sim_balls.Add(Instantiate(BallsPrefabs[i]));
            lineRenderers[i] = sim_balls[i].AddComponent<LineRenderer>();
            lineRenderers[i].material = lineRenderer.material;
            lineRenderers[i].widthCurve= lineRenderer.widthCurve;
            lineRenderers[i].colorGradient = lineRenderer.colorGradient;
            ClearLines(lineRenderers[i], real_balls[i]);
            sim_balls[i].transform.position = real_balls[i].transform.position;
            sim_balls[i].GetComponent<MeshRenderer>().material = TRMaterials[i];
            SceneManager.MoveGameObjectToScene(sim_balls[i], physicsScene);

        }


        
    }

    void ClearLines(LineRenderer lR, GameObject source)
    {

        lR.positionCount = 1;
        lR.SetPosition(0, source.transform.position);

    }

  

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition == prevMousePos)
                return;
            prevMousePos = Input.mousePosition;
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                lastHitPoint = hit.point;
                currForce = hit.point - real_white.transform.position;
                currForce = Vector3.Scale(currForce, new Vector3(ForceFactor, 0, ForceFactor));
                ResetBalls();
                SimulatePath();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            real_white.GetComponent<Rigidbody>().velocity = currForce;
            ClearLines(lineRenderer, real_white);   
            for (int i = 0; i < real_balls.Count; i++)
            {
                if (!ShowPhantoms)
                {
                    sim_balls[i].GetComponent<MeshRenderer>().enabled = false;

                }
                ClearLines(lineRenderers[i], real_balls[i]);
            }
        }
    }

    private void ResetBalls()
    {
        for (int i = 0; i < sim_balls.Count; i++)
        {
            sim_balls[i].transform.position = real_balls[i].transform.position;

        }
    }

    private void SimulatePath()
    {
        ClearLines(lineRenderer,real_white);     //removes all previously simulated balls
        for (int i = 0; i < real_balls.Count; i++)
        {

            sim_balls[i].GetComponent<MeshRenderer>().enabled = true;
            ClearLines(lineRenderers[i], real_balls[i]);
        }
       
        //set the simulation white ball back to the original position
        sim_whiteball.transform.position = real_white.transform.position;
        Rigidbody sim_rb = sim_whiteball.GetComponent<Rigidbody>();
        sim_rb.velocity = Vector3.zero;
        //simulate a specific number of frames into the future
        sim_rb.velocity = currForce;
        for (int i = 0; i < SimSteps; i++)
        {
            
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            //Instantiate a whiteball_trajectory_prefab for each step the sim_whiteball takes into the simulation
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(i+1,sim_whiteball.transform.position);

            for (int j = 0; j < sim_balls.Count; j++)
            {

                lineRenderers[j].positionCount++;
                lineRenderers[j].SetPosition(i + 1, sim_balls[j].transform.position);

            }
        }


    }
}

