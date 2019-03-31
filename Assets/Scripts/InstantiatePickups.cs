using UnityEngine;

public sealed class InstantiatePickups : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numPickups;
    public int GetNumPickups()
    {
        return numPickups;
    }
    // Start is called before the first frame update
    private void Start()
    {
        var existingPrefabs = new Vector3[numPickups];

        for(int i = 0; i < numPickups; i++)
        {   
            //Is this new prefab location already fucked?
            bool itisfucked = false;
            //Not yet
            var posVector = new Vector3(Random.Range(-9f, 9f), 0.5f, Random.Range(-9f, 9f));
            foreach(Vector3 oldVector in existingPrefabs)
            {   
                //You're about to read a bunch of dumb shit, don't hate me lmao
                if(Mathf.Abs(posVector.x - oldVector.x) <= .5 && Mathf.Abs(posVector.z - oldVector.z) <= .5)
                {
                    i--;
                    itisfucked = true;
                    break;
                }
            }
            if (itisfucked)
            {
                continue;
            }
            existingPrefabs[i] = posVector;
            Instantiate(prefab, posVector, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    // I don't need this here but also it doesn't cost me anything to keep it at the bottom I guess?
    private void Update()
    {
        
    }
}
