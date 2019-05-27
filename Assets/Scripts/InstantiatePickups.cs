using UnityEngine;


//TODO: I've given enough editorial options that it's possible (and even easy) to create an inescapable infinite loop at startup.
//      That sucks.
//      I'd add an arbitrary time limit, but a little more logic than that needs to be added to how numPickups works alongside 
public sealed class InstantiatePickups : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private int numPickups = 0;
    [SerializeField, MinMaxSlider(-10f, 10f)] private Vector2 xVector = new Vector2();
    [SerializeField, MinMaxSlider(-10f, 10f)] private Vector2 zVector = new Vector2();
    [SerializeField] private float allowableXPositionDifference = 0.5f;
    [SerializeField] private float allowableZPositionDifference = 0.5f;

    private const float JUST_ABOVE_THE_ORIGIN_Y = 0.5f;
    

    public int GetNumPickups()
    {
        return numPickups;
    }

    // This script will, at startup, place a number of pickups on the board in random positions. Before committing to a position, it will check that it's not too close
    // to a prefab already placed by this method. Note that, in this current implementation, pickups not placed by this Start method are not considered when checking positions
    private void Start()
    {
        var existingPrefabs = new Vector3[numPickups];

        for(int i = 0; i < numPickups; i++)
        {   
            //Is this new prefab location already too close to another placed prefab?
            bool tooCloseToAnotherPrefabOfSameType = false;
            //Not yet it's not
            var posVector = new Vector3(xVector.RandomInRange(), JUST_ABOVE_THE_ORIGIN_Y, zVector.RandomInRange());
            //Now it might be

            //We check the position we've randomly selected against the positions of all of the prefabs we've already placed
            foreach(Vector3 oldVector in existingPrefabs)
            {   
                //If it's too close on either the x or z axis...
                if(Mathf.Abs(posVector.x - oldVector.x) <= allowableXPositionDifference && Mathf.Abs(posVector.z - oldVector.z) <= allowableZPositionDifference)
                {
                    //...We decrement our iterator (so that we won't count this iteration of the loop as successful)...
                    i--;
                    //...And mark this instance as too close...
                    tooCloseToAnotherPrefabOfSameType = true;
                    //...And stop comparing to other existing prefabs, as only one prefab has to be too close for this position to be unacceptable
                    break;
                }
            }

            //If, in the above code block, we found a prefab too close to the position we checked, we should skip the below code (neither place the new prefab,
            //nor add it to the list of placed prefabs)
            if (tooCloseToAnotherPrefabOfSameType)
            {
                continue;
            }


            existingPrefabs[i] = posVector;
            var thisPickup = Instantiate(prefab, posVector, Quaternion.identity);
            thisPickup.transform.parent = GameObject.Find("Pickups").transform;

        }

    }
}
