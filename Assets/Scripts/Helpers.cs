using UnityEngine;

public class Helpers
{
    //CC BY-SA 3.0
    //from https://gamedev.stackexchange.com/a/86999
    public static Bounds GetMaxBounds(GameObject g)
    {
        var b = new Bounds(g.transform.position, Vector3.zero);
        foreach (Renderer r in g.GetComponentsInChildren<Renderer>())
        {
            b.Encapsulate(r.bounds);
        }

        return b;
    }
}
