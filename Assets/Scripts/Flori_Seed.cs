using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flori_Seed : MonoBehaviour {
    public float maxSeeds, stageOneHeightLimit = 350f, stageTwoHeightLimit = 350f;
    public bool collectable = true, isGrowing = false, stageOne = true, stageTwo = false;
    public GameObject seedPrefab;

    float diameter, floweringSpeed = 1.0f;
    public GameObject flower;
    Vector3 flowerScale;

    // Use this for initialization
    void Start () {

        diameter = transform.localScale.x;
        for (int i = 0; i < maxSeeds; i++)
        {
            GenerateSeed();
        }
    }

    // Update is called once per frame
    void Update () {
        if (isGrowing)
        {
            if (stageOne)
                StageOneGrowth();
            else if (stageTwo)
                StageTwoGrowth();
        }
    }

    //Not sure if need to add in the rest of the code
    public void Sprout()
    {
        Destroy(this.GetComponent<Rigidbody>());
    }

    public void AddWater()
    {
        isGrowing = true;
    }

    void StageOneGrowth()
    {
        flowerScale = flower.transform.localScale;
        flowerScale.y += Time.deltaTime * floweringSpeed;
        if (flowerScale.x < 200f)
        {
            flowerScale.x += Time.deltaTime * floweringSpeed / 3.0f;
            flowerScale.z += Time.deltaTime * floweringSpeed / 3.0f;
        }
        flower.transform.localScale = flowerScale;

        if (flowerScale.y >= stageOneHeightLimit)
        {
            stageOne = false;
            stageTwo = true;
            isGrowing = false;
        }
    }

    void StageTwoGrowth()
    {
        flowerScale = flower.transform.localScale;
        flowerScale.y += Time.deltaTime * floweringSpeed;
        flowerScale.x += Time.deltaTime * floweringSpeed / 5.0f;
        flowerScale.z += Time.deltaTime * floweringSpeed / 5.0f;
        flower.transform.localScale = flowerScale;

        if (flowerScale.y >= stageTwoHeightLimit)
        {
            FinishGrowing();
        }
    }

    public void FinishGrowing()
    {
        stageTwo = false;
        isGrowing = false;
        flower.GetComponent<Flower>().SetCanBePicked(true);
    }

    void GenerateSeed()
    {

        Vector3 seedPosition = transform.position;
        seedPosition.x += RandomOffset(diameter * 4);
        seedPosition.z += RandomOffset(diameter * 4);

        if (Vector3.Distance(transform.position, seedPosition) < diameter)
        {
            GenerateSeed();
        }

        GameObject seedling = Instantiate(seedPrefab, seedPosition, Camera.main.transform.rotation); //TODO: null reference here... fix
        seedling.name = gameObject.name;
        seedling.transform.SetParent(transform);
    }

    float RandomOffset(float offsetSize)
    {
        return Random.Range(-offsetSize / 2.0f, offsetSize / 2.0f);
    }
}
